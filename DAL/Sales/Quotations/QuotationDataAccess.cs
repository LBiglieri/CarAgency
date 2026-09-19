using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Sales
{
    public sealed class QuotationDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Quotations_GetById", new SqlParameter("@Id", id)); }
        public DataTable GetAll() { return Read("Quotations_GetAll"); }
        public DataTable GetAllActiveByClient(Guid clientId) { return Read("Quotations_GetAllActiveByClient", new SqlParameter("@Client_Id", clientId)); }

        public DataTable Add(Guid id, Guid vehicleId, Guid clientId, double price, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("Quotations_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Vehicle_Id", vehicleId),
                new SqlParameter("@Client_Id", clientId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Creation_Date", creationDate));
        }
    }
}
