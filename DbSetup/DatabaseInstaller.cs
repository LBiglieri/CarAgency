using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CarAgency.DbSetup
{
    public sealed class DatabaseInstaller
    {
        // El CREATE DATABASE que genera SSMS trae las rutas .mdf/.ldf de la maquina de origen:
        // la base la crea este instalador en la carpeta de datos de la instancia destino.
        private static readonly Regex createDatabase = new Regex(@"^\s*CREATE\s+DATABASE\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private readonly string server;
        private readonly string database;
        private readonly Action<string> log;

        public DatabaseInstaller(string server, string database, Action<string> log)
        {
            this.server = server;
            this.database = database;
            this.log = log;
        }

        public string ApplicationConnectionString
        {
            get { return Build(database, false); }
        }

        public bool Exists()
        {
            using (var connection = OpenMaster())
            using (var command = new SqlCommand("SELECT CASE WHEN DB_ID(@db) IS NULL THEN 0 ELSE 1 END", connection))
            {
                command.Parameters.AddWithValue("@db", database);
                return (int)command.ExecuteScalar() == 1;
            }
        }

        public string Backup()
        {
            // Sin ruta: SQL Server lo deja en la carpeta Backup de la instancia, donde su cuenta de servicio puede escribir.
            string file = database + ".antes-de-reemplazar-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bak";
            ExecuteOnMaster(
                "DECLARE @sql nvarchar(max) = N'BACKUP DATABASE ' + QUOTENAME(@db) + N' TO DISK = @file WITH INIT, COPY_ONLY';" +
                "EXEC sp_executesql @sql, N'@file nvarchar(260)', @file = @file;",
                new SqlParameter("@file", file));
            log("Copia de seguridad de la base anterior: " + file + " (carpeta Backup de la instancia).");
            return file;
        }

        public void Drop()
        {
            SqlConnection.ClearAllPools();
            ExecuteOnMaster(
                "IF DB_ID(@db) IS NOT NULL BEGIN " +
                "DECLARE @sql nvarchar(max) = N'ALTER DATABASE ' + QUOTENAME(@db) + N' SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE ' + QUOTENAME(@db) + N';';" +
                "EXEC sp_executesql @sql; END");
            log("Base " + database + " eliminada.");
        }

        public void Create()
        {
            ExecuteOnMaster("DECLARE @sql nvarchar(max) = N'CREATE DATABASE ' + QUOTENAME(@db); EXEC sp_executesql @sql;");
            log("Base " + database + " creada.");
        }

        public void Run(IReadOnlyList<SqlBatch> batches)
        {
            using (var connection = new SqlConnection(Build(database, true)))
            {
                connection.InfoMessage += (sender, e) => log("  " + e.Message);
                connection.Open();
                for (int i = 0; i < batches.Count; i++)
                {
                    SqlBatch batch = batches[i];
                    if (createDatabase.IsMatch(batch.Text))
                    {
                        log("  Se omite el CREATE DATABASE del script (linea " + batch.FirstLine + ").");
                        continue;
                    }
                    for (int r = 0; r < batch.Repeat; r++)
                    {
                        using (var command = new SqlCommand(batch.Text, connection) { CommandTimeout = 0 })
                        {
                            try { command.ExecuteNonQuery(); }
                            catch (SqlException ex)
                            {
                                throw new InvalidOperationException(
                                    "Fallo el lote " + (i + 1) + " de " + batches.Count + " (linea " + batch.FirstLine + " del script): " + ex.Message, ex);
                            }
                        }
                    }
                }
            }
            log("Script ejecutado: " + batches.Count + " lotes.");
        }

        private SqlConnection OpenMaster()
        {
            var connection = new SqlConnection(Build("master", true));
            connection.Open();
            return connection;
        }

        private void ExecuteOnMaster(string sql, params SqlParameter[] parameters)
        {
            using (var connection = OpenMaster())
            using (var command = new SqlCommand(sql, connection) { CommandTimeout = 0 })
            {
                command.Parameters.AddWithValue("@db", database);
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        private string Build(string catalog, bool forSetup)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = catalog,
                IntegratedSecurity = true
            };
            if (forSetup)
            {
                // Sin pool, para que ninguna conexion del instalador quede abierta y bloquee un DROP posterior.
                builder.Pooling = false;
                builder.ConnectTimeout = 15;
            }
            return builder.ConnectionString;
        }
    }
}
