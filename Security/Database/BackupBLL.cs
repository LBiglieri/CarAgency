using System;
using BE;
using CarAgency.BE;
using CarAgency.BE.BitacoraEventos;
using CarAgency.BE.Integrity;
using CarAgency.Security.BitacoraEventos;
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
            BitacoraEventosBLL.Record(TipoEvento.BackupCreated);
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
            if (!SessionHandler.Instance.IsAuthorized(PermisoType.BackupRestoreForm))
                throw new TranslatableException("NoBackupRestorePermission", "No tiene la patente de backup/restore.");
            if (!IntegrityService.Current.Verify().IsConsistent)
                throw new IntegrityAccessDeniedException();
        }

        private IntegrityReport RestoreAndReset(string path, Usuario actor)
        {
            BitacoraEventosBLL.Record(TipoEvento.RestoreStarted, actor: actor);
            try
            {
                try { _backupMapper.RealizarRestore(path); }
                catch (Exception error)
                {
                    BitacoraEventosBLL.Record(TipoEvento.RestoreFailed, actor: actor);
                    if (error is TranslatableException) throw;
                    throw new TranslatableException("AuditRestoreFailed", "No se pudo restaurar la base de datos.", error);
                }
                BitacoraEventosBLL.Record(TipoEvento.RestoreCompleted, actor: actor);
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
