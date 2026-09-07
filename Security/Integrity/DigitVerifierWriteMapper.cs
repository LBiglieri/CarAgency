using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CarAgency.BE.Integrity;

namespace CarAgency.Security.Integrity
{
    public sealed class DigitVerifierWriteMapper
    {
        private readonly DigitVerifierBLL bll;

        public DigitVerifierWriteMapper(string connectionString) { bll = new DigitVerifierBLL(connectionString); }

        public void EnsureConfigured(params string[] tables) { bll.EnsureConfigured(tables); }
        public void UpdateDvv(params string[] tables) { bll.UpdateDvv(tables); }

        public void PrepareDvh(SqlCommand command, string table)
        {
            var values = command.Parameters.Cast<SqlParameter>().ToDictionary(
                p => p.ParameterName.TrimStart('@'), p => p.Value == DBNull.Value ? null : p.Value, StringComparer.OrdinalIgnoreCase);
            DVPreparedRow prepared = bll.PrepareDvh(table, new DVRow(values));
            foreach (DVColumn column in prepared.Columns)
            {
                SqlParameter parameter = command.Parameters.Cast<SqlParameter>().Single(p =>
                    string.Equals(p.ParameterName.TrimStart('@'), column.Name, StringComparison.OrdinalIgnoreCase));
                parameter.Value = prepared.Row.GetValue(column.Name) ?? DBNull.Value;
                switch (column.SqlType)
                {
                    case "varchar": parameter.SqlDbType = SqlDbType.VarChar; parameter.Size = column.MaxLength; break;
                    case "nvarchar": parameter.SqlDbType = SqlDbType.NVarChar; parameter.Size = column.MaxLength < 0 ? -1 : column.MaxLength / 2; break;
                    case "uniqueidentifier": parameter.SqlDbType = SqlDbType.UniqueIdentifier; break;
                    case "int": parameter.SqlDbType = SqlDbType.Int; break;
                    case "float": parameter.SqlDbType = SqlDbType.Float; break;
                    case "bit": parameter.SqlDbType = SqlDbType.Bit; break;
                    case "datetime": parameter.SqlDbType = SqlDbType.DateTime; break;
                }
            }
            command.Parameters.Add("@DVH", SqlDbType.Char, 64).Value = prepared.Row.DVH;
        }

        public void PrepareFamilyDeletion(SqlCommand command, Guid familyId)
        {
            DVFamilyDeletion deletion = bll.PrepareFamilyDeletion(familyId);
            var digests = new DataTable();
            digests.Columns.Add("Id", typeof(Guid));
            digests.Columns.Add("DVH", typeof(string));
            foreach (DVUserDigest digest in deletion.UserDigests) digests.Rows.Add(digest.Id, digest.DVH);
            command.Parameters.Add("@BaseRoleId", SqlDbType.UniqueIdentifier).Value = (object)deletion.BaseRoleId ?? DBNull.Value;
            SqlParameter parameter = command.Parameters.Add("@UserDigests", SqlDbType.Structured);
            parameter.TypeName = "dbo.UserDvhUpdates";
            parameter.Value = digests;
        }
    }
}
