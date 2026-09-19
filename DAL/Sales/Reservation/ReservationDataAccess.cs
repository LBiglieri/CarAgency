using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Sales
{
    public sealed class ReservationDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Reservation_GetById", new SqlParameter("@Id", id)); }
        public DataTable GetAll() { return Read("Reservation_GetAll"); }
        public DataTable GetAllActiveByClient(Guid clientId) { return Read("Reservation_GetAllActiveByClient", new SqlParameter("@Client_Id", clientId)); }

        public DataTable Add(Guid id, Guid vehicleId, Guid clientId, double price, DateTime creationDate,
            DateTime expirationDate, DvhCalculator dvh)
        {
            return Read("Reservation_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Vehicle_Id", vehicleId),
                new SqlParameter("@Client_Id", clientId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Creation_Date", creationDate),
                new SqlParameter("@Expiration_Date", expirationDate));
        }
    }
}
