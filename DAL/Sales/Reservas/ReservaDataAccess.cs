using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Sales
{
    public sealed class ReservaDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Reserva_GetById", new SqlParameter("@Reserva_Id", id)); }
        public DataTable GetAll() { return Read("Reserva_GetAll"); }
        public DataTable GetAllActiveByPerson(Guid personId) { return Read("Reserva_GetAllActiveByPerson", new SqlParameter("@Persona_Id", personId)); }

        public DataTable Add(Guid id, Guid vehicleId, Guid personId, double price, DateTime creationDate,
            DateTime expirationDate, DvhCalculator dvh)
        {
            return Read("Reserva_Add", dvh,
                new SqlParameter("@Reserva_Id", id),
                new SqlParameter("@Vehiculo_Id", vehicleId),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@Precio", price),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@FechaVencimiento", expirationDate));
        }
    }
}
