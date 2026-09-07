using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.DAL.Integrity;
using BE;

namespace CarAgency.Security.Integrity
{
    // Instantanea interna; la UI solo recibe un informe sin datos de credenciales.
    internal sealed class IntegrityInspection : IDisposable
    {
        private readonly IntegrityTransaction data;
        private readonly DigitVerifierCalculator calculator;
        private readonly Dictionary<string, IReadOnlyList<DVColumn>> columns = new Dictionary<string, IReadOnlyList<DVColumn>>();
        private readonly Dictionary<string, List<DVRow>> rows = new Dictionary<string, List<DVRow>>();
        private readonly HashSet<DVRow> trusted = new HashSet<DVRow>();
        public IntegrityReport Report { get; private set; }

        public IntegrityInspection(string connectionString, byte[] key)
        {
            calculator = new DigitVerifierCalculator(key);
            data = new IntegrityTransaction(connectionString);
            try { Inspect(); }
            catch { data.Dispose(); throw; }
        }

        private void Inspect()
        {
            var issues = new List<string>();
            bool compatible = true;
            int keyReferences = 0;
            foreach (string table in DVTables.All)
            {
                using (DataTable metadata = data.GetColumns(table))
                    columns[table] = metadata.Rows.Cast<DataRow>().Select(r => new DVColumn {
                        Name = (string)r["Name"], SqlType = (string)r["SqlType"], MaxLength = (int)r["MaxLength"],
                        IsNullable = (bool)r["IsNullable"], CodePage = (int)r["CodePage"], IsKey = (bool)r["IsKey"]
                    }).ToList();
                if (columns[table].Count == 0) throw new TranslatableException("DVTableNotFound", "No se encontro dbo.{0}", table);
                rows[table] = new List<DVRow>();
                var calculated = new List<string>();
                int invalidRows = 0;
                using (DataTable content = data.GetRows(table))
                {
                    foreach (DataRow item in content.Rows)
                    {
                        var values = columns[table].ToDictionary(c => c.Name, c => item.IsNull(c.Name) ? null : item[c.Name]);
                        var row = new DVRow(values, item["DVH"] as string);
                        rows[table].Add(row);
                        string digest = calculator.CalculateDvh(table, columns[table], row.GetValue);
                        calculated.Add(digest);
                        if (string.Equals(row.DVH, digest, StringComparison.Ordinal)) trusted.Add(row);
                        else
                        {
                            invalidRows++;
                            if (invalidRows <= 20)
                                issues.Add(table + ": DVH incorrecto. " + string.Join(", ", columns[table].Where(c => c.IsKey)
                                    .Select(c => c.Name + "=" + row.GetValue(c.Name))));
                        }
                    }
                }
                if (invalidRows > 20) issues.Add(table + ": " + invalidRows + " registros con DVH incorrecto (detalle limitado a 20).");
                using (DataTable digit = data.GetDigit(table))
                {
                    if (digit.Rows.Count != 1)
                    {
                        issues.Add(table + ": falta un registro DV unico.");
                        if (digit.Rows.Count > 1) compatible = false;
                        continue;
                    }
                    DataRow dv = digit.Rows[0];
                    if ((string)dv["SchemaName"] != "dbo" || Convert.ToInt32(dv["AlgorithmVersion"]) != 1
                        || !string.Equals(dv["KeyId"] as string, calculator.KeyId, StringComparison.Ordinal))
                    {
                        compatible = false;
                        issues.Add(table + ": clave, esquema o version DV incompatible. Recuperar la configuracion original.");
                    }
                    else keyReferences++;
                    if (!string.Equals(dv["DVV"] as string, calculator.CalculateDvv(table, calculated), StringComparison.Ordinal))
                        issues.Add(table + ": DVV incorrecto.");
                }
            }
            Report = new IntegrityReport(issues, compatible && keyReferences > 0);
        }

        public DVRow FindUser(string username)
        {
            return rows["Users"].SingleOrDefault(r => string.Equals((string)r.GetValue("Username"), username, StringComparison.OrdinalIgnoreCase));
        }
        public DVRow FindUser(Guid id) { return rows["Users"].SingleOrDefault(r => (Guid)r.GetValue("Id") == id); }

        public bool IsTrustedUser(DVRow user)
        {
            return Report.CanRecalculate && user != null && trusted.Contains(user)
                && (bool)user.GetValue("Active") && !(bool)user.GetValue("Blocked");
        }

        public bool HasPermission(DVRow user, PermissionType permission)
        {
            return IsTrustedUser(user) && HasPermission((Guid)user.GetValue("Role_Id"), permission, new HashSet<Guid>());
        }

        private bool HasPermission(Guid id, PermissionType permission, HashSet<Guid> visited)
        {
            if (!visited.Add(id)) return false;
            DVRow component = rows["Permissions"].SingleOrDefault(r => (Guid)r.GetValue("Id") == id);
            if (component == null || !trusted.Contains(component)) return false;
            string type = component.GetValue("Type") as string;
            if (!string.IsNullOrEmpty(type)) return type == permission.ToString();
            foreach (DVRow link in rows["Permission_Permission"].Where(r => (Guid)r.GetValue("Father_Id") == id))
                if (trusted.Contains(link) && HasPermission((Guid)link.GetValue("Child_Id"), permission, visited)) return true;
            return false;
        }

        public void Recalculate()
        {
            if (!Report.CanRecalculate) throw new TranslatableException("IntegrityCannotRecalculate", "La configuracion DV no permite recalcular con seguridad.");
            foreach (string table in DVTables.All)
            {
                var horizontal = new List<string>();
                foreach (DVRow row in rows[table])
                {
                    string digest = calculator.CalculateDvh(table, columns[table], row.GetValue);
                    data.SaveRow(table, columns[table].Where(c => c.IsKey).ToDictionary(c => c.Name, c => row.GetValue(c.Name)), digest);
                    horizontal.Add(digest);
                }
                data.SaveVertical(table, calculator.CalculateDvv(table, horizontal), calculator.KeyId);
            }
            columns.Clear(); rows.Clear(); trusted.Clear();
            Inspect();
            if (!Report.IsConsistent) throw new TranslatableException("IntegrityRecalculateVerifyFailed", "La verificacion posterior fallo; se revierte el recalculo.");
            data.Commit();
        }
        public void Dispose() { data.Dispose(); }
    }
}
