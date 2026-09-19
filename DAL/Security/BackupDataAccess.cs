using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Security
{
    // BACKUP/RESTORE no aceptan el nombre de la base como parametro: se arma el texto con Quote.
    // El restore corre desde master porque no puede haber conexiones abiertas contra la base destino.
    public sealed class BackupDataAccess
    {
        private const int BackupTimeout = 300;
        private const int AlterTimeout = 60;
        private readonly string connectionString = new DatabaseConnectionProvider().GetConnectionString();

        public string DatabaseName { get { return new SqlConnectionStringBuilder(connectionString).InitialCatalog; } }

        public void Backup(string file)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = Command("BACKUP DATABASE " + Quote(DatabaseName) + " TO DISK=@Path WITH CHECKSUM", connection, BackupTimeout, file))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable ReadBackupHeader(string file)
        {
            using (var connection = new SqlConnection(MasterConnectionString()))
            using (var command = Command("RESTORE HEADERONLY FROM DISK=@Path", connection, BackupTimeout, file))
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

        public void Verify(string file, int position)
        {
            using (var connection = new SqlConnection(MasterConnectionString()))
            using (var command = Command("RESTORE VERIFYONLY FROM DISK=@Path WITH FILE=@Position", connection, BackupTimeout, file, position))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Pasa la base a SINGLE_USER para reemplazarla y, falle o no, intenta devolverla a MULTI_USER.
        public void Restore(string file, int position)
        {
            string database = Quote(DatabaseName);
            using (var connection = new SqlConnection(MasterConnectionString()))
            {
                connection.Open();
                Exception restoreError = null;
                SqlConnection.ClearAllPools();
                try
                {
                    using (var single = Command("ALTER DATABASE " + database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE", connection, AlterTimeout))
                        single.ExecuteNonQuery();
                    using (var restore = Command("RESTORE DATABASE " + database + " FROM DISK=@Path WITH FILE=@Position, REPLACE", connection, BackupTimeout, file, position))
                        restore.ExecuteNonQuery();
                }
                catch (Exception error) { restoreError = error; }
                finally
                {
                    try
                    {
                        using (var cleanup = new SqlConnection(MasterConnectionString()))
                        using (var multi = Command("ALTER DATABASE " + database + " SET MULTI_USER WITH ROLLBACK IMMEDIATE", cleanup, AlterTimeout))
                        {
                            cleanup.Open();
                            multi.ExecuteNonQuery();
                        }
                    }
                    catch (Exception cleanupError)
                    {
                        restoreError = restoreError == null ? cleanupError : new AggregateException("Fallo el restore y tambien el retorno a MULTI_USER.", restoreError, cleanupError);
                    }
                    SqlConnection.ClearAllPools();
                }
                if (restoreError != null) ExceptionDispatchInfo.Capture(restoreError).Throw();
            }
        }

        private string MasterConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master", Pooling = false };
            return builder.ConnectionString;
        }

        private static SqlCommand Command(string text, SqlConnection connection, int timeout, string file = null, int? position = null)
        {
            var command = new SqlCommand(text, connection) { CommandTimeout = timeout };
            if (file != null) command.Parameters.Add("@Path", SqlDbType.NVarChar, 4000).Value = file;
            if (position != null) command.Parameters.Add("@Position", SqlDbType.Int).Value = position.Value;
            return command;
        }

        private static string Quote(string name) { return "[" + name.Replace("]", "]]") + "]"; }
    }
}
