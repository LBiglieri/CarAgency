using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CarAgency.DAL.Integrity
{
    public sealed class IntegrityTransaction : IDisposable
    {
        private readonly SqlConnection connection;
        private readonly SqlTransaction transaction;
        private readonly Dictionary<string, DataTable> columns = new Dictionary<string, DataTable>();

        public IntegrityTransaction(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction(IsolationLevel.Serializable);
            }
            catch { connection.Dispose(); throw; }
        }

        private SqlCommand Command(string text, params SqlParameter[] parameters)
        {
            var command = new SqlCommand(text, connection, transaction) { CommandTimeout = 60 };
            command.Parameters.AddRange(parameters);
            return command;
        }

        private DataTable Read(string text, params SqlParameter[] parameters)
        {
            using (var command = Command(text, parameters))
            using (var reader = command.ExecuteReader())
            {
                var result = new DataTable();
                result.Load(reader);
                return result;
            }
        }

        public DataTable GetColumns(string table)
        {
            // El SP valida la lista de tablas protegidas antes de usar el identificador.
            if (!columns.ContainsKey(table))
                columns[table] = Read("EXEC dbo.DV_GetColumns @TableName", new SqlParameter("@TableName", table));
            return columns[table].Copy();
        }

        public DataTable GetRows(string table)
        {
            using (GetColumns(table)) { }
            return Read("SELECT * FROM dbo." + Quote(table));
        }

        public DataTable GetDigit(string table)
        {
            return Read("EXEC dbo.DV_GetByTable @TableName", new SqlParameter("@TableName", table));
        }

        public void SaveRow(string table, IDictionary<string, object> keys, string dvh)
        {
            using (DataTable metadata = GetColumns(table))
            {
                string[] names = metadata.Rows.Cast<DataRow>().Where(r => (bool)r["IsKey"])
                    .Select(r => (string)r["Name"]).ToArray();
                if (names.Length == 0 || keys.Count != names.Length || names.Any(n => !keys.ContainsKey(n)))
                    throw new InvalidOperationException("Se requiere la clave primaria completa de " + table);
                using (var command = Command("UPDATE dbo." + Quote(table) + " SET DVH=@DVH WHERE "
                    + string.Join(" AND ", names.Select((name, index) => Quote(name) + "=@K" + index))))
                {
                    command.Parameters.Add("@DVH", SqlDbType.Char, 64).Value = dvh;
                    for (int index = 0; index < names.Length; index++)
                        command.Parameters.AddWithValue("@K" + index, keys[names[index]]);
                    if (command.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("No se pudo actualizar un unico registro en " + table);
                }
            }
        }

        public void SaveVertical(string table, string dvv, string keyId)
        {
            using (var command = Command("dbo.DV_Save"))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TableName", SqlDbType.NVarChar, 128).Value = table;
                command.Parameters.Add("@DVV", SqlDbType.Char, 64).Value = dvv;
                command.Parameters.Add("@KeyId", SqlDbType.Char, 64).Value = keyId;
                command.ExecuteNonQuery();
            }
        }

        private static string Quote(string name) { return "[" + name.Replace("]", "]]") + "]"; }
        public void Commit() { transaction.Commit(); }
        public void Dispose()
        {
            foreach (DataTable item in columns.Values) item.Dispose();
            transaction.Dispose();
            connection.Dispose();
        }
    }
}
