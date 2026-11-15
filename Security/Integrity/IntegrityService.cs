using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
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
        internal Usuario AuditUser { get; private set; }

        internal RecoverySession(IntegrityService owner, DVRow user, IntegrityReport report, bool canRestore)
        {
            AuditUser = new Usuario { Usuario_Id = (Guid)user.GetValue("Usuario_Id"), NombreUsuario = (string)user.GetValue("NombreUsuario"), Nombre = (string)user.GetValue("Nombre"), Apellido = (string)user.GetValue("Apellido") };
            Owner = owner;
            UserId = (Guid)user.GetValue("Usuario_Id");
            PasswordHash = (string)user.GetValue("Clave");
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

        public IntegrityReport VerifyForCurrentUser()
        {
            RequireRecalculatePermission();
            return Verify();
        }

        private static Usuario RequireRecalculatePermission()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermisoType.RecalculateDV))
                throw new TranslatableException("IntegrityPermissionDenied", "No tiene permiso para verificar o recalcular digitos verificadores.");
            return SessionHandler.Instance.User;
        }

        public void Recalculate()
        {
            lock (gate)
            {
                Usuario actor = RequireRecalculatePermission();
                using (var inspection = Inspect())
                {
                    DVRow user = inspection.FindUser(actor.Usuario_Id);
                    if (!inspection.HasPermission(user, PermisoType.RecalculateDV)
                        || !string.Equals(user.GetValue("Clave") as string, actor.Clave, StringComparison.Ordinal))
                        throw new TranslatableException("IntegrityPermissionDenied", "No tiene permiso para verificar o recalcular digitos verificadores.");
                    inspection.Recalculate();
                }
                EndRecoveryCore();
                DigitVerifierMapper.ClearMetadataCache();
                try { new BitacoraEventosBLL(connectionString).Write(TipoEvento.IntegrityRecalculated, null, actor); }
                finally { SessionHandler.Instance.Logout(); }
            }
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
                        throw new TranslatableException("IntegrityIncompatibleConfig", "La configuracion de digitos es incompatible. Recupere la version original; no se permite iniciar sesion ni recalcular.");
                    if (DateTime.UtcNow < retryAfter) throw new IntegrityAccessDeniedException();
                    DVRow user = inspection.FindUser(username);
                    if (!inspection.HasPermission(user, PermisoType.RecalculateDV)
                        || !CryptographyHandler.VerifyPassword(password, (string)user.GetValue("Clave")))
                    {
                        if (++failures >= 3) { retryAfter = DateTime.UtcNow.AddSeconds(30); failures = 0; }
                        throw new IntegrityAccessDeniedException();
                    }
                    failures = 0;
                    active = new RecoverySession(this, user, inspection.Report,
                        inspection.HasPermission(user, PermisoType.BackupRestoreForm));
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
            if (!inspection.HasPermission(user, PermisoType.RecalculateDV)
                || !string.Equals(user.GetValue("Clave") as string, session.PasswordHash, StringComparison.Ordinal)
                || (restore && !inspection.HasPermission(user, PermisoType.BackupRestoreForm)))
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

        public Usuario GetRecoveryAuditUser(RecoverySession session)
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
                new BitacoraEventosBLL(connectionString).Write(TipoEvento.IntegrityRecalculated, null, session.AuditUser);
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
