using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Billing
{
    public sealed class PaymentDataAccess : DataAccessBase
    {
        public DataTable GetAllPaymentTypes() { return Read("PaymentTypes_GetAll"); }
        public DataTable GetAllByInvoice(Guid invoiceId) { return Read("Payments_GetAllByInvoice", new SqlParameter("@Invoice_Id", invoiceId)); }

        public DataTable Add(Guid id, Guid invoiceId, Guid paymentTypeId, double amount, string detail, DvhCalculator dvh)
        {
            return Read("Payments_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Invoice_Id", invoiceId),
                new SqlParameter("@PaymentType_Id", paymentTypeId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Detail", detail));
        }

        public DataTable Delete(Guid id) { return Read("Payments_Delete", new SqlParameter("@Id", id)); }
    }
}
