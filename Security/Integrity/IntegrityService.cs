using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using System;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.DAL.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using BE;

namespace CarAgency.Security.Integrity
{
    public sealed class IntegrityAccessDeniedException : TranslatableException
    {
        public IntegrityAccessDeniedException()
            : base("IntegrityAccessDenied", "Se detectaron inconsistencias. El acceso esta bloqueado. Contacte al administrador con permiso para recalcular DV.") { }
    }

    public sealed class RecoverySession
    {
        internal readonly IntegrityService Owner;
        internal readonly Guid UserId;
        internal readonly string PasswordHash;
        internal readonly DateTime ExpiresAt;
        internal bool Revoked;
        public IntegrityReport Report { get; private set; }
        public bool CanRestore { get; private set; }
        internal User AuditUser { get; private set; }

        internal RecoverySession(IntegrityService owner, DVRow user, IntegrityReport report, bool canRestore)
        {
            AuditUser = new User { Id = (Guid)user.GetValue("Id"), Username = (string)user.GetValue("Username"), Name = (string)user.GetValue("Name"), Surname = (string)user.GetValue("Surname") };
            Owner = owner;
            UserId = (Guid)user.GetValue("Id");
            PasswordHash = (string)user.GetValue("Password");
            ExpiresAt = DateTime.UtcNow.AddMinutes(10);
            Report = report;
            CanRestore = canRestore;
        }
    }

    public sealed class IntegrityService
    {
        private static readonly Lazy<IntegrityService> current = new Lazy<IntegrityService>(() =>
            new IntegrityService(new DatabaseConnectionProvider().GetConnectionString()));
        public static IntegrityService Current { get { return current.Value; } }
        private readonly string connectionString;
        private readonly byte[] explicitKey;
        private readonly object gate = new object();
        private RecoverySession active;
        private int failures;
        private DateTime retryAfter;

        public IntegrityService(string connectionString, byte[] key = null)
        {
            this.connectionString = connectionString;
            explicitKey = key == null ? null : (byte[])key.Clone();
        }

        private IntegrityInspection Inspect()
        {
            byte[] key = explicitKey == null ? DigitVerifierKey.Load() : (byte[])explicitKey.Clone();
            try { return new IntegrityInspection(connectionString, key); }
            finally { Array.Clear(key, 0, key.Length); }
        }

        public IntegrityReport Verify()
        {
            using (var inspection = Inspect()) return inspection.Report;
        }

        // Null significa base consistente: el caller puede seguir el login normal.
        public RecoverySession CheckLogin(string username, string password)
        {
            lock (gate)
            {
                EndRecoveryCore();
                using (var inspection = Inspect())
                {
                    if (inspection.Report.IsConsistent) return null;
                    if (!inspection.Report.CanRecalculate)
                        throw new TranslatableException("IntegrityIncompatibleConfig", "La configuracion de digitos es incompatible. Recupere la clave/version original; no se permite iniciar sesion ni recalcular.");
                    if (DateTime.UtcNow < retryAfter) throw new IntegrityAccessDeniedException();
                    DVRow user = inspection.FindUser(username);
                    if (!inspection.HasPermission(user, PermissionType.RecalculateDV)
                        || !CryptographyHandler.VerifyPassword(password, (string)user.GetValue("Password")))
                    {
                        if (++failures >= 3) { retryAfter = DateTime.UtcNow.AddSeconds(30); failures = 0; }
                        throw new IntegrityAccessDeniedException();
                    }
                    failures = 0;
                    active = new RecoverySession(this, user, inspection.Report,
                        inspection.HasPermission(user, PermissionType.BackupRestoreForm));
                    return active;
                }
            }
        }

        private void Authorize(IntegrityInspection inspection, RecoverySession session, bool restore)
        {
            if (session == null || session != active || session.Owner != this || session.Revoked
                || DateTime.UtcNow >= session.ExpiresAt || SessionHandler.Instance.Logged())
                throw new TranslatableException("IntegrityRecoveryInvalid", "El acceso de recuperacion no es valido. Inicie sesion nuevamente.");
            DVRow user = inspection.FindUser(session.UserId);
            if (!inspection.HasPermission(user, PermissionType.RecalculateDV)
                || !string.Equals(user.GetValue("Password") as string, session.PasswordHash, StringComparison.Ordinal)
                || (restore && !inspection.HasPermission(user, PermissionType.BackupRestoreForm)))
            {
                EndRecoveryCore();
                throw new TranslatableException("IntegrityRecoveryRevoked", "Las credenciales o patentes de recuperacion ya no son validas.");
            }
        }

        public void AuthorizeRecovery(RecoverySession session, bool restore = false)
        {
            lock (gate)
            using (var inspection = Inspect()) Authorize(inspection, session, restore);
        }

        public User GetRecoveryAuditUser(RecoverySession session)
        {
            AuthorizeRecovery(session, true);
            return session.AuditUser;
        }

        public void Recalculate(RecoverySession session)
        {
            lock (gate)
            {
                using (var inspection = Inspect())
                {
                    Authorize(inspection, session, false);
                    inspection.Recalculate();
                }
                EndRecoveryCore();
                DigitVerifierMapper.ClearMetadataCache();
                new AuditBLL(connectionString).Write(AuditEventType.IntegrityRecalculated, null, session.AuditUser);
            }
        }

        public void EndRecovery()
        {
            lock (gate) EndRecoveryCore();
        }

        private void EndRecoveryCore()
        {
            if (active != null) active.Revoked = true;
            active = null;
        }
    }
}
