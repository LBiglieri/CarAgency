using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using BE;

namespace CarAgency.Security.Integrity
{
    public sealed class DigitVerifierBLL
    {
        private readonly DigitVerifierMapper mapper;
        private readonly DigitVerifierCalculator calculator;

        public DigitVerifierBLL(string connectionString) : this(connectionString, DigitVerifierKey.Load()) { }
        public DigitVerifierBLL(string connectionString, byte[] key)
        {
            mapper = new DigitVerifierMapper(connectionString);
            calculator = new DigitVerifierCalculator(key);
        }

        public void EnsureConfigured(params string[] tables)
        {
            foreach (string table in tables)
            {
                DV digit = mapper.GetByTable(table);
                if (digit == null) throw new TranslatableException("DVMissingVerticalRecord", "Falta el registro de digitos de dbo.{0}. La base debe restaurarse desde un backup actualizado con los digitos incluidos.", table);
                if (!string.Equals(digit.KeyId, calculator.KeyId, StringComparison.Ordinal))
                    throw new TranslatableException("DVKeyMismatch", "La clave de digitos no corresponde a esta base. No se modificaron los digitos.");
            }
        }

        // Recibe/devuelve objetos BE. La conversion a parametros pertenece al mapper de escritura.
        public DVPreparedRow PrepareDvh(string table, DVRow row)
        {
            EnsureConfigured(table);
            IReadOnlyList<DVColumn> columns = mapper.GetColumns(table);
            var values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (DVColumn column in columns)
                values.Add(column.Name, DigitVerifierCalculator.Normalize(column, row.GetValue(column.Name)));
            string dvh = calculator.CalculateDvh(table, columns, name => values[name]);
            return new DVPreparedRow(columns, new DVRow(values, dvh));
        }

        public void UpdateDvv(params string[] tables)
        {
            EnsureConfigured(tables);
            foreach (string table in tables.Distinct())
                mapper.Save(CreateVertical(table));
        }

        private DV CreateVertical(string table)
        {
            return new DV {
                SchemaName = "dbo", TableName = table, AlgorithmVersion = 1, KeyId = calculator.KeyId,
                DVV = calculator.CalculateDvv(table, mapper.GetHorizontalDigits(table).Select(d => d.DVH))
            };
        }

        public DVFamilyDeletion PrepareFamilyDeletion(Guid familyId)
        {
            EnsureConfigured("Users", "Permissions", "Permission_Permission");
            IReadOnlyList<DVRow> users = mapper.GetUsersByRole(familyId);
            Guid? baseRole = null;
            if (users.Count > 0)
            {
                IReadOnlyList<Family> roles = mapper.GetBaseRoles();
                if (roles.Count != 1)
                    throw new TranslatableException("DVBaseUserFamilyRequired", "Debe existir una unica familia 'Base User' para reasignar usuarios.");
                baseRole = roles[0].Id;
                if (baseRole == familyId)
                    throw new TranslatableException("DVBaseUserHasAssignedUsers", "No se puede eliminar Base User mientras tenga usuarios asignados.");
            }
            var digests = new List<DVUserDigest>();
            IReadOnlyList<DVColumn> columns = mapper.GetColumns("Users");
            foreach (DVRow user in users)
            {
                string dvh = calculator.CalculateDvh("Users", columns,
                    name => name == "Role_Id" ? (object)baseRole.Value : user.GetValue(name));
                digests.Add(new DVUserDigest { Id = (Guid)user.GetValue("Id"), DVH = dvh });
            }
            return new DVFamilyDeletion(baseRole, digests);
        }

    }
}
