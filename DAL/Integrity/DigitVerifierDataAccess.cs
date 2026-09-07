using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CarAgency.DAL.Integrity
{
    public sealed class DigitVerifierDataAccess
    {
        private readonly string connectionString;
        public DigitVerifierDataAccess(string connectionString) { this.connectionString = connectionString; }

        public DataTable GetColumns(string table) { return Read("dbo.DV_GetColumns", TableParameter(table)); }
        public DataTable GetByTable(string table) { return Read("dbo.DV_GetByTable", TableParameter(table)); }
        public DataTable ReadUsersByRole(Guid roleId)
        {
            return Read("dbo.DV_GetUsersByRole", new SqlParameter("@RoleId", SqlDbType.UniqueIdentifier) { Value = roleId });
        }
        public DataTable ReadBaseRoles() { return Read("dbo.DV_GetBaseRoles"); }

        public IEnumerable<IDataRecord> ReadDvh(string table)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("dbo.DV_GetHorizontal", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(TableParameter(table));
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) yield return reader;
            }
        }

        public void SaveDvv(string table, string dvv, string keyId)
        {
            Execute("dbo.DV_Save", TableParameter(table),
                new SqlParameter("@DVV", SqlDbType.Char, 64) { Value = dvv },
                new SqlParameter("@KeyId", SqlDbType.Char, 64) { Value = keyId });
        }

        private static SqlParameter TableParameter(string table)
        {
            return new SqlParameter("@TableName", SqlDbType.NVarChar, 128) { Value = (object)table ?? DBNull.Value };
        }

        private DataTable Read(string procedure, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(procedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }
        }

        private int Execute(string procedure, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(procedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
