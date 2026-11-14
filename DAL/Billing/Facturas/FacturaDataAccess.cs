using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Billing
{
    public sealed class FacturaDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Factura_GetById", new SqlParameter("@Factura_Id", id)); }

        public DataTable GetAllPendingOfPaperworkByPerson(Guid personId)
        {
            return Read("Factura_GetAllPendingOfPaperworkByPerson", new SqlParameter("@Persona_Id", personId));
        }

        public DataTable Add(Guid id, Guid vehicleId, Guid personId, Guid reservationId, string detail, string cuilCuitPerson,
            string razonSocial, double amount, bool paymentStatus, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("Factura_Add", dvh, InvoiceParameters(id, vehicleId, personId, reservationId, detail, cuilCuitPerson,
                razonSocial, amount, paymentStatus, creationDate));
        }

        public DataTable Update(Guid id, Guid vehicleId, Guid personId, Guid reservationId, string detail, string cuilCuitPerson,
            string razonSocial, double amount, bool paymentStatus, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("Factura_Update", dvh, InvoiceParameters(id, vehicleId, personId, reservationId, detail, cuilCuitPerson,
                razonSocial, amount, paymentStatus, creationDate));
        }

        public DataTable Delete(Guid id) { return Read("Factura_Delete", new SqlParameter("@Factura_Id", id)); }

        private static SqlParameter[] InvoiceParameters(Guid id, Guid vehicleId, Guid personId, Guid reservationId, string detail,
            string cuilCuitPerson, string razonSocial, double amount, bool paymentStatus, DateTime creationDate)
        {
            return new[]
            {
                new SqlParameter("@Factura_Id", id),
                new SqlParameter("@Vehiculo_Id", vehicleId),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@Reserva_Id", reservationId),
                new SqlParameter("@Detalle", detail),
                new SqlParameter("@CuilCuit", cuilCuitPerson),
                new SqlParameter("@RazonSocial", razonSocial),
                new SqlParameter("@Valor", amount),
                new SqlParameter("@EstadoPago", paymentStatus),
                new SqlParameter("@Fecha", creationDate)
            };
        }
    }
}
