using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.DAL.Integrity;
using BE;

namespace CarAgency.Security.Integrity
{
    public sealed class DigitVerifierMapper
    {
        private readonly DigitVerifierDataAccess data;
        private readonly string connectionString;
        private static readonly ConcurrentDictionary<string, IReadOnlyList<DVColumn>> columnsCache =
            new ConcurrentDictionary<string, IReadOnlyList<DVColumn>>();

        public static void ClearMetadataCache() { columnsCache.Clear(); }

        public DigitVerifierMapper(string connectionString)
        {
            this.connectionString = connectionString;
            data = new DigitVerifierDataAccess(connectionString);
        }

        public IReadOnlyList<DVColumn> GetColumns(string table)
        {
            DVTables.RequireProtected(table);
            return columnsCache.GetOrAdd(connectionString + "|" + table, _ =>
            {
                List<DVColumn> columns = Map<DVColumn>(data.GetColumns(table));
                if (columns.Count == 0) throw new TranslatableException("DVTableNotFound", "No se encontro dbo.{0}", table);
                return columns.AsReadOnly();
            });
        }

        public DV GetByTable(string table)
        {
            DVTables.RequireProtected(table);
            return Map<DV>(data.GetByTable(table)).SingleOrDefault();
        }

        public void Save(DV digit)
        {
            DVTables.RequireProtected(digit.TableName);
            if (digit.SchemaName != "dbo" || digit.AlgorithmVersion != 1)
                throw new TranslatableException("DVUnsupportedSchema", "Esquema o version de digitos no soportados.");
            data.SaveDvv(digit.TableName, digit.DVV, digit.KeyId);
        }

        public IEnumerable<DVHorizontal> GetHorizontalDigits(string table)
        {
            DVTables.RequireProtected(table);
            foreach (IDataRecord record in data.ReadDvh(table))
                yield return new DVHorizontal { DVH = record.IsDBNull(0) ? null : record.GetString(0) };
        }

        public IReadOnlyList<DVRow> GetUsersByRole(Guid role) { return MapRows(data.ReadUsersByRole(role)); }

        public IReadOnlyList<Family> GetBaseRoles()
        {
            using (DataTable result = data.ReadBaseRoles())
                return result.AsEnumerable().Select(row => new Family {
                    Id = row.Field<Guid>("Id"), Name = row.Field<string>("Name")
                }).ToList().AsReadOnly();
        }

        private static List<T> Map<T>(DataTable table) where T : class, new()
        {
            using (table)
            using (IDataReader reader = table.CreateDataReader())
                return MappingHandler.MapReaderToEntities<T>(reader);
        }

        private static IReadOnlyList<DVRow> MapRows(DataTable table)
        {
            using (table)
            {
                var result = new List<DVRow>();
                foreach (DataRow row in table.Rows)
                {
                    var values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    string dvh = null;
                    foreach (DataColumn column in table.Columns)
                    {
                        object value = row.IsNull(column) ? null : row[column];
                        if (column.ColumnName == "DVH") dvh = (string)value;
                        else values.Add(column.ColumnName, value);
                    }
                    result.Add(new DVRow(values, dvh));
                }
                return result.AsReadOnly();
            }
        }
    }
}
