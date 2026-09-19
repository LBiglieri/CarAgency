using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Audit
{
    public sealed class AuditDataAccess
    {
        private readonly string connectionString;
        public AuditDataAccess() : this(new DatabaseConnectionProvider().GetConnectionString()) { }
        public AuditDataAccess(string connectionString) { this.connectionString = connectionString; }

        // El calculo del DVH vive en la capa de seguridad; aca solo se arma el comando parametrizado.
        public void Insert(Guid id, Guid? userId, string attemptedLogin, DateTime occurredAt,
            string module, string eventType, int criticality, Guid? targetId, DvhCalculator dvh)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("dbo.Events_Insert", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
                command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (object)userId ?? DBNull.Value;
                command.Parameters.Add("@AttemptedLogin", SqlDbType.NVarChar, 256).Value = (object)attemptedLogin ?? DBNull.Value;
                command.Parameters.Add("@OccurredAt", SqlDbType.DateTime).Value = occurredAt;
                command.Parameters.Add("@Module", SqlDbType.VarChar, 32).Value = module;
                command.Parameters.Add("@EventType", SqlDbType.VarChar, 64).Value = eventType;
                command.Parameters.Add("@Criticality", SqlDbType.Int).Value = criticality;
                command.Parameters.Add("@TargetId", SqlDbType.UniqueIdentifier).Value = (object)targetId ?? DBNull.Value;
                DvhCommand.Apply(command, dvh);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool ActorExists(Guid userId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("dbo.Events_ActorExists", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
                connection.Open();
                return Convert.ToBoolean(command.ExecuteScalar());
            }
        }

        public DataTable Query(string login, DateTime from, DateTime until, string module, string eventType, int? criticality)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("dbo.Events_Query", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Login", SqlDbType.NVarChar, 256).Value = (object)login ?? DBNull.Value;
                command.Parameters.Add("@From", SqlDbType.DateTime).Value = from;
                command.Parameters.Add("@Until", SqlDbType.DateTime).Value = until;
                command.Parameters.Add("@Module", SqlDbType.VarChar, 32).Value = (object)module ?? DBNull.Value;
                command.Parameters.Add("@EventType", SqlDbType.VarChar, 64).Value = (object)eventType ?? DBNull.Value;
                command.Parameters.Add("@Criticality", SqlDbType.Int).Value = (object)criticality ?? DBNull.Value;
                return Read(connection, command);
            }
        }

        public DataTable GetLogins()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("dbo.Events_GetLogins", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                return Read(connection, command);
            }
        }

        private static DataTable Read(SqlConnection connection, SqlCommand command)
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                var table = new DataTable();
                table.Load(reader);
                return table;
            }
        }
    }
}
