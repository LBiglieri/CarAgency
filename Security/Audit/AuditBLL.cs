using System;
using System.Collections.Generic;
using BE;
using CarAgency.BE;
using CarAgency.BE.Audit;
using CarAgency.Security.Session;

namespace CarAgency.Security.Audit
{
    public sealed class AuditBLL
    {
        private readonly AuditMapper mapper;
        public AuditBLL() { mapper = new AuditMapper(); }
        public AuditBLL(string connectionString) { mapper = new AuditMapper(connectionString); }

        public static void RequireAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermissionType.EventLogForm))
                throw new TranslatableException("AuditAccessDenied", "No tiene permiso para consultar la bitácora.");
        }
        public static void Record(AuditEventType type, Guid? targetId = null, User actor = null, string attemptedLogin = null)
        { new AuditBLL().Write(type, targetId, actor ?? SessionHandler.Instance.User, attemptedLogin); }

        public void Write(AuditEventType type, Guid? targetId, User actor, string attemptedLogin = null)
        {
            var definition = AuditCatalog.Get(type);
            try
            {
                Guid? userId = actor == null ? (Guid?)null : actor.Id;
                string login = attemptedLogin;
                // Una restauracion puede dejar sin fila al actor. Se resuelve aca, antes del DVH:
                // el procedimiento no puede corregir la referencia sin invalidar el digito.
                if (userId.HasValue && !mapper.ActorExists(userId.Value))
                {
                    login = actor.Username;
                    userId = null;
                }
                mapper.Insert(new AuditEvent {
                    Id = Guid.NewGuid(), UserId = userId, AttemptedLogin = login, OccurredAt = DateTime.Now,
                    Module = definition.Module, EventType = type, Criticality = definition.Criticality, TargetId = targetId
                });
            }
            catch (Exception error)
            {
                throw new TranslatableException("AuditWriteFailed",
                    "No se pudo guardar la bitácora. La operación anterior puede haberse completado; verifique su estado antes de repetirla.", error);
            }
        }
        public List<AuditEvent> Query(AuditFilter filter)
        {
            RequireAccess();
            Validate(filter);
            try { return mapper.Query(filter); }
            catch (Exception error) { throw new TranslatableException("AuditReadFailed", "No se pudo consultar la bitácora.", error); }
        }
        public List<string> GetLogins()
        {
            RequireAccess();
            try { return mapper.GetLogins(); }
            catch (Exception error) { throw new TranslatableException("AuditReadFailed", "No se pudo consultar la bitácora.", error); }
        }
        public static void Validate(AuditFilter filter)
        {
            if (filter == null || filter.From.Date > filter.To.Date || filter.To.Date == DateTime.MaxValue.Date
                || (filter.Criticality.HasValue && (filter.Criticality < 1 || filter.Criticality > 5))
                || (filter.Module.HasValue && !Enum.IsDefined(typeof(AuditModule), filter.Module.Value))
                || (filter.EventType.HasValue && !Enum.IsDefined(typeof(AuditEventType), filter.EventType.Value))
                || (filter.Login != null && filter.Login.Length > 256))
                throw new TranslatableException("AuditInvalidFilter", "Revise las fechas y los filtros de la bitácora.");
        }
    }
}
