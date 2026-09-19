using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Billing
{
    public sealed class InvoiceDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Invoice_GetById", new SqlParameter("@Id", id)); }

        public DataTable GetAllPendingOfPaperworkByClient(Guid clientId)
        {
            return Read("Invoice_GetAllPendingOfPaperworkByClient", new SqlParameter("@Client_Id", clientId));
        }

        public DataTable Add(Guid id, Guid vehicleId, Guid clientId, Guid reservationId, string detail, string cuilCuitClient,
            string razonSocial, double amount, bool paymentStatus, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("Invoice_Add", dvh, InvoiceParameters(id, vehicleId, clientId, reservationId, detail, cuilCuitClient,
                razonSocial, amount, paymentStatus, creationDate));
        }

        public DataTable Update(Guid id, Guid vehicleId, Guid clientId, Guid reservationId, string detail, string cuilCuitClient,
            string razonSocial, double amount, bool paymentStatus, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("Invoice_Update", dvh, InvoiceParameters(id, vehicleId, clientId, reservationId, detail, cuilCuitClient,
                razonSocial, amount, paymentStatus, creationDate));
        }

        public DataTable Delete(Guid id) { return Read("Invoice_Delete", new SqlParameter("@Id", id)); }

        private static SqlParameter[] InvoiceParameters(Guid id, Guid vehicleId, Guid clientId, Guid reservationId, string detail,
            string cuilCuitClient, string razonSocial, double amount, bool paymentStatus, DateTime creationDate)
        {
            return new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Vehicle_Id", vehicleId),
                new SqlParameter("@Client_Id", clientId),
                new SqlParameter("@Reservation_Id", reservationId),
                new SqlParameter("@Detail", detail),
                new SqlParameter("@CUIL_CUIT_Client", cuilCuitClient),
                new SqlParameter("@Razon_Social", razonSocial),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Payment_Status", paymentStatus),
                new SqlParameter("@Creation_Date", creationDate)
            };
        }
    }
}
