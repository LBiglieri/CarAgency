using System;
using System.Collections.Generic;
using BE;
using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Security.Session;

namespace BLL
{
    // Historial de cada vehiculo. Las filas las escriben los triggers de la base: aca solo se consultan.
    public class BitacoraCambiosBLL
    {
        private readonly BitacoraCambiosMapper _changelogmapper = new BitacoraCambiosMapper();

        public static void RequireAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermisoType.BitacoraCambiosForm))
                throw new TranslatableException("ChangeLogAccessDenied", "No tiene permiso para consultar la bitácora de cambios.");
        }

        public List<BitacoraCambio> Query(BitacoraCambiosFilter filter)
        {
            RequireAccess();
            Validate(filter);
            try { return _changelogmapper.Query(filter); }
            catch (Exception error) { throw new TranslatableException("ChangeLogReadFailed", "No se pudo consultar la bitácora de cambios.", error); }
        }

        public static void Validate(BitacoraCambiosFilter filter)
        {
            if (filter == null || filter.From.Date > filter.To.Date || filter.To.Date == DateTime.MaxValue.Date
                || (filter.Operacion.HasValue && !Enum.IsDefined(typeof(BitacoraCambiosOperation), filter.Operacion.Value)))
                throw new TranslatableException("ChangeLogInvalidFilter", "Revise las fechas y los filtros de la bitácora de cambios.");
        }
    }
}
