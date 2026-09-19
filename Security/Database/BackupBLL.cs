using System;
using BE;
using CarAgency.BE;
using CarAgency.BE.Audit;
using CarAgency.BE.Integrity;
using CarAgency.Security.Audit;
using CarAgency.Security.Integrity;
using CarAgency.Security.Session;

namespace CarAgency.Security.Database
{
    public class BackupBLL
    {
        private readonly BackupMapper _backupMapper = new BackupMapper();

        public void RealizarBackup(string path)
        {
            RequireNormalAccess();
            try { _backupMapper.RealizarBackup(path); }
            catch (TranslatableException) { throw; }
            catch (Exception error) { throw new TranslatableException("AuditBackupFailed", "No se pudo crear el backup.", error); }
            AuditBLL.Record(AuditEventType.BackupCreated);
        }

        public IntegrityReport RealizarRestore(string path)
        {
            RequireNormalAccess();
            return RestoreAndReset(path, SessionHandler.Instance.User);
        }

        public IntegrityReport RealizarRestore(string path, RecoverySession recovery)
        {
            IntegrityService.Current.AuthorizeRecovery(recovery, true);
            return RestoreAndReset(path, IntegrityService.Current.GetRecoveryAuditUser(recovery));
        }

        private static void RequireNormalAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermissionType.BackupRestoreForm))
                throw new TranslatableException("NoBackupRestorePermission", "No tiene la patente de backup/restore.");
            if (!IntegrityService.Current.Verify().IsConsistent)
                throw new IntegrityAccessDeniedException();
        }

        private IntegrityReport RestoreAndReset(string path, User actor)
        {
            AuditBLL.Record(AuditEventType.RestoreStarted, actor: actor);
            try
            {
                try { _backupMapper.RealizarRestore(path); }
                catch (Exception error)
                {
                    AuditBLL.Record(AuditEventType.RestoreFailed, actor: actor);
                    if (error is TranslatableException) throw;
                    throw new TranslatableException("AuditRestoreFailed", "No se pudo restaurar la base de datos.", error);
                }
                AuditBLL.Record(AuditEventType.RestoreCompleted, actor: actor);
                DigitVerifierMapper.ClearMetadataCache();
                return IntegrityService.Current.Verify();
            }
            finally
            {
                try { SessionHandler.Instance.Logout(); }
                finally
                {
                    IntegrityService.Current.EndRecovery();
                    DigitVerifierMapper.ClearMetadataCache();
                }
            }
        }
    }
}
