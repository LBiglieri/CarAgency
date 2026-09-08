using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Mappers.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using CarAgency.Security.Integrity;
using CarAgency.BE.Integrity;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SecurityBLL
    {
        private SecurityMapper _securitymapper;
        public SecurityBLL()
        {
            _securitymapper = new SecurityMapper();
        }

        public void RealizarBackup(string path)
        {
            RequireNormalAccess();
            try { _securitymapper.RealizarBackup(path); }
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
                try { _securitymapper.RealizarRestore(path); }
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
