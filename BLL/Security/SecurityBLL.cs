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
            _securitymapper.RealizarBackup(path);
        }

        public IntegrityReport RealizarRestore(string path)
        {
            RequireNormalAccess();
            return RestoreAndReset(path);
        }

        public IntegrityReport RealizarRestore(string path, RecoverySession recovery)
        {
            IntegrityService.Current.AuthorizeRecovery(recovery, true);
            return RestoreAndReset(path);
        }

        private static void RequireNormalAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermissionType.BackupRestoreForm))
                throw new TranslatableException("NoBackupRestorePermission", "No tiene la patente de backup/restore.");
            if (!IntegrityService.Current.Verify().IsConsistent)
                throw new IntegrityAccessDeniedException();
        }

        private IntegrityReport RestoreAndReset(string path)
        {
            try
            {
                _securitymapper.RealizarRestore(path);
                DigitVerifierMapper.ClearMetadataCache();
                return IntegrityService.Current.Verify();
            }
            finally
            {
                SessionHandler.Instance.Logout();
                IntegrityService.Current.EndRecovery();
                DigitVerifierMapper.ClearMetadataCache();
            }
        }
    }
}
