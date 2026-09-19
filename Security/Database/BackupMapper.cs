using System;
using System.Data;
using System.IO;
using System.Linq;
using CarAgency.DAL.Security;
using BE;

namespace CarAgency.Security.Database
{
    public class BackupMapper
    {
        private static readonly string[] SystemDatabases = { "master", "model", "msdb", "tempdb" };
        private readonly BackupDataAccess data = new BackupDataAccess();

        public void RealizarBackup(string path)
        {
            data.Backup(Path.Combine(path, $"CarAgencyBCK_{DateTime.Now:yyyyMMdd_HHmmss_fff}.bak"));
        }

        public void RealizarRestore(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !string.Equals(Path.GetExtension(path), ".bak", StringComparison.OrdinalIgnoreCase))
                throw new TranslatableException("RestoreSelectBakFile", "Seleccione un archivo .bak.");
            string database = data.DatabaseName;
            if (string.IsNullOrWhiteSpace(database) || SystemDatabases.Contains(database.ToLowerInvariant()))
                throw new TranslatableException("RestoreInvalidTargetDatabase", "La base de destino no es valida para restaurar.");

            int position = LatestFullBackup(path, database);
            data.Verify(path, position);
            try { data.Restore(path, position); }
            catch (Exception error)
            {
                throw new TranslatableException("RestoreFailed", "No se pudo completar la restauracion: {0}", error, error.Message);
            }
        }

        // Un .bak puede traer varios backups: se restaura el completo (BackupType 1) mas reciente de esta base.
        private int LatestFullBackup(string path, string database)
        {
            using (DataTable header = data.ReadBackupHeader(path))
            {
                DataRow latest = header.Rows.Cast<DataRow>()
                    .Where(row => string.Equals(row["DatabaseName"] as string, database, StringComparison.OrdinalIgnoreCase)
                        && Convert.ToInt32(row["BackupType"]) == 1)
                    .OrderByDescending(row => Convert.ToDateTime(row["BackupFinishDate"]))
                    .FirstOrDefault();
                if (latest == null)
                    throw new TranslatableException("RestoreIncompleteBackup", "El archivo no contiene un backup completo de {0}.", database);
                return Convert.ToInt32(latest["Position"]);
            }
        }
    }
}
