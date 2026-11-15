using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Vehiculos
{
    // Solo lectura: las filas de la bitacora de cambios las escriben los triggers de Vehiculo y Tramite.
    public sealed class BitacoraCambiosDataAccess : DataAccessBase
    {
        public DataTable Query(Guid? vehicleId, DateTime from, DateTime until, int? operation)
        {
            return Read("BitacoraCambios_Query",
                new SqlParameter("@Vehiculo_Id", SqlDbType.UniqueIdentifier) { Value = (object)vehicleId ?? DBNull.Value },
                new SqlParameter("@From", SqlDbType.DateTime) { Value = from },
                new SqlParameter("@Until", SqlDbType.DateTime) { Value = until },
                new SqlParameter("@Operacion", SqlDbType.Int) { Value = (object)operation ?? DBNull.Value });
        }
    }
}
