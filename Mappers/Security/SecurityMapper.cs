using CarAgency.DAL.Persistence;
using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;

using CarAgency.Mappers.Persistence;
using BE;

namespace CarAgency.Mappers
{
    public class SecurityMapper : MapperBase
    {
        private readonly string configuredConnection;
        public SecurityMapper(string connectionString = null) { configuredConnection = connectionString; }
        private string ConnectionString { get { return configuredConnection ?? base.GetConnectionString(); } }

        public void RealizarBackup(string path)
        {
            string nombreArchivo = $"CarAgencyBCK_{DateTime.Now:yyyyMMdd_HHmmss_fff}.bak";
            string rutaCompleta = System.IO.Path.Combine(path, nombreArchivo);
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Quote(builder.InitialCatalog) + " TO DISK=@Path WITH CHECKSUM", conn))
            {
                cmd.Parameters.Add("@Path", SqlDbType.NVarChar, 4000).Value = rutaCompleta;
                cmd.CommandTimeout = 300;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RealizarRestore(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !string.Equals(Path.GetExtension(path), ".bak", StringComparison.OrdinalIgnoreCase))
                throw new TranslatableException("RestoreSelectBakFile", "Seleccione un archivo .bak.");
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            string database = builder.InitialCatalog;
            if (string.IsNullOrWhiteSpace(database) || new[] { "master", "model", "msdb", "tempdb" }.Contains(database.ToLowerInvariant()))
                throw new TranslatableException("RestoreInvalidTargetDatabase", "La base de destino no es valida para restaurar.");
            builder.InitialCatalog = "master";
            builder.Pooling = false;
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                int position;
                using (var header = new SqlCommand("RESTORE HEADERONLY FROM DISK=@Path", conn))
                {
                    header.Parameters.Add("@Path", SqlDbType.NVarChar, 4000).Value = path;
                    header.CommandTimeout = 300;
                    using (var reader = header.ExecuteReader())
                    {
                        var candidates = new System.Collections.Generic.List<Tuple<int, DateTime>>();
                        while (reader.Read())
                            if (string.Equals(reader["DatabaseName"] as string, database, StringComparison.OrdinalIgnoreCase)
                                && Convert.ToInt32(reader["BackupType"]) == 1)
                                candidates.Add(Tuple.Create(Convert.ToInt32(reader["Position"]), Convert.ToDateTime(reader["BackupFinishDate"])));
                        if (candidates.Count == 0) throw new TranslatableException("RestoreIncompleteBackup", "El archivo no contiene un backup completo de {0}.", database);
                        position = candidates.OrderByDescending(c => c.Item2).First().Item1;
                    }
                }
                using (var verify = new SqlCommand("RESTORE VERIFYONLY FROM DISK=@Path WITH FILE=@Position", conn))
                {
                    verify.Parameters.Add("@Path", SqlDbType.NVarChar, 4000).Value = path;
                    verify.Parameters.Add("@Position", SqlDbType.Int).Value = position;
                    verify.CommandTimeout = 300;
                    verify.ExecuteNonQuery();
                }
                Exception restoreError = null;
                SqlConnection.ClearAllPools();
                try
                {
                    using (var single = new SqlCommand("ALTER DATABASE " + Quote(database) + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conn))
                    { single.CommandTimeout = 60; single.ExecuteNonQuery(); }
                    using (var restore = new SqlCommand("RESTORE DATABASE " + Quote(database) + " FROM DISK=@Path WITH FILE=@Position, REPLACE", conn))
                    {
                        restore.Parameters.Add("@Path", SqlDbType.NVarChar, 4000).Value = path;
                        restore.Parameters.Add("@Position", SqlDbType.Int).Value = position;
                        restore.CommandTimeout = 300;
                        restore.ExecuteNonQuery();
                    }
                }
                catch (Exception error) { restoreError = error; }
                finally
                {
                    try
                    {
                        using (var cleanup = new SqlConnection(builder.ConnectionString))
                        using (var multi = new SqlCommand("ALTER DATABASE " + Quote(database) + " SET MULTI_USER WITH ROLLBACK IMMEDIATE", cleanup))
                        { cleanup.Open(); multi.CommandTimeout = 60; multi.ExecuteNonQuery(); }
                    }
                    catch (Exception cleanupError)
                    {
                        restoreError = restoreError == null ? cleanupError : new AggregateException("Fallo el restore y tambien el retorno a MULTI_USER.", restoreError, cleanupError);
                    }
                    SqlConnection.ClearAllPools();
                }
                if (restoreError != null) throw new TranslatableException("RestoreFailed", "No se pudo completar la restauracion: {0}", restoreError, restoreError.Message);
            }
        }
        private static string Quote(string name) { return "[" + name.Replace("]", "]]") + "]"; }
    }
}
