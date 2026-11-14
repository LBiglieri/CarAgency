using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Billing
{
    public sealed class PagoDataAccess : DataAccessBase
    {
        public DataTable GetAllPaymentTypes() { return Read("TipoPago_GetAll"); }
        public DataTable GetAllByInvoice(Guid invoiceId) { return Read("Pago_GetAllByInvoice", new SqlParameter("@Factura_Id", invoiceId)); }

        public DataTable Add(Guid id, Guid invoiceId, Guid paymentTypeId, double amount, string detail, DvhCalculator dvh)
        {
            return Read("Pago_Add", dvh,
                new SqlParameter("@Pago_Id", id),
                new SqlParameter("@Factura_Id", invoiceId),
                new SqlParameter("@TipoPago_Id", paymentTypeId),
                new SqlParameter("@Monto", amount),
                new SqlParameter("@Detalle", detail));
        }

        public DataTable Delete(Guid id) { return Read("Pago_Delete", new SqlParameter("@Pago_Id", id)); }
    }
}
