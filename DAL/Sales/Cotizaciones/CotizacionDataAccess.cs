using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Sales
{
    public sealed class CotizacionDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Cotizacion_GetById", new SqlParameter("@Cotizacion_Id", id)); }
        public DataTable GetAll() { return Read("Cotizacion_GetAll"); }
        public DataTable GetAllActiveByPerson(Guid personId) { return Read("Cotizacion_GetAllActiveByPerson", new SqlParameter("@Persona_Id", personId)); }

        public DataTable Add(Guid id, Guid vehicleId, Guid personId, double price, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("Cotizacion_Add", dvh,
                new SqlParameter("@Cotizacion_Id", id),
                new SqlParameter("@Vehiculo_Id", vehicleId),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@Precio", price),
                new SqlParameter("@Fecha", creationDate));
        }
    }
}
