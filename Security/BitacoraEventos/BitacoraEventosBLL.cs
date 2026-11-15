using System;
using System.Collections.Generic;
using BE;
using CarAgency.BE;
using CarAgency.BE.BitacoraEventos;
using CarAgency.Security.Session;

namespace CarAgency.Security.BitacoraEventos
{
    public sealed class BitacoraEventosBLL
    {
        private readonly BitacoraEventosMapper mapper;
        public BitacoraEventosBLL() { mapper = new BitacoraEventosMapper(); }
        public BitacoraEventosBLL(string connectionString) { mapper = new BitacoraEventosMapper(connectionString); }

        public static void RequireAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermisoType.BitacoraEventosForm))
                throw new TranslatableException("AuditAccessDenied", "No tiene permiso para consultar la bitácora.");
        }
        public static void Record(TipoEvento type, Guid? targetId = null, Usuario actor = null, string attemptedLogin = null)
        { new BitacoraEventosBLL().Write(type, targetId, actor ?? SessionHandler.Instance.User, attemptedLogin); }

        public void Write(TipoEvento type, Guid? targetId, Usuario actor, string attemptedLogin = null)
        {
            var definition = BitacoraEventosCatalog.Get(type);
            try
            {
                Guid? userId = actor == null ? (Guid?)null : actor.Usuario_Id;
                string login = attemptedLogin;
                // Una restauracion puede dejar sin fila al actor. Se resuelve aca, antes del DVH:
                // el procedimiento no puede corregir la referencia sin invalidar el digito.
                if (userId.HasValue && !mapper.ActorExists(userId.Value))
                {
                    login = actor.NombreUsuario;
                    userId = null;
                }
                mapper.Insert(new BitacoraEvento {
                    BitacoraEventos_Id = Guid.NewGuid(), Usuario_Id = userId, LoginIntentado = login, Fecha = DateTime.Now,
                    Modulo = definition.Module, TipoEvento = type, Criticidad = definition.Criticality, Objetivo_Id = targetId
                });
            }
            catch (Exception error)
            {
                throw new TranslatableException("AuditWriteFailed",
                    "No se pudo guardar la bitácora. La operación anterior puede haberse completado; verifique su estado antes de repetirla.", error);
            }
        }
        public List<BitacoraEvento> Query(BitacoraEventosFilter filter)
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
        public void Export(List<BitacoraEvento> events, string path)
        {
            RequireAccess();
            try { BitacoraEventosXmlSerializer.Write(events, path); }
            catch (Exception error) { throw new TranslatableException("AuditExportFailed", "No se pudo exportar la bitácora.", error); }
            // Se registra recién cuando el archivo quedó escrito. La importación no se registra.
            Record(TipoEvento.AuditExported);
        }
        public List<BitacoraEvento> Import(string path)
        {
            RequireAccess();
            try { return BitacoraEventosXmlSerializer.Read(path); }
            catch (Exception error)
            {
                throw new TranslatableException("AuditImportFailed",
                    "No se pudo importar el archivo. Verifique que sea una bitácora exportada desde el sistema.", error);
            }
        }
        public static void Validate(BitacoraEventosFilter filter)
        {
            if (filter == null || filter.From.Date > filter.To.Date || filter.To.Date == DateTime.MaxValue.Date
                || (filter.Criticidad.HasValue && (filter.Criticidad < 1 || filter.Criticidad > 5))
                || (filter.Modulo.HasValue && !Enum.IsDefined(typeof(BitacoraEventosModule), filter.Modulo.Value))
                || (filter.TipoEvento.HasValue && !Enum.IsDefined(typeof(TipoEvento), filter.TipoEvento.Value))
                || (filter.Login != null && filter.Login.Length > 256))
                throw new TranslatableException("AuditInvalidFilter", "Revise las fechas y los filtros de la bitácora.");
        }
    }
}
