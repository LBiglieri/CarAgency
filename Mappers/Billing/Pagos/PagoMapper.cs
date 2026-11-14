using System;
using System.Collections.Generic;
using CarAgency.BE;
using CarAgency.DAL.Billing;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class PagoMapper
    {
        private const string Table = "Pago";
        private readonly PagoDataAccess data = new PagoDataAccess();

        // Sin filas devuelve null.
        public List<TipoPago> GetAllPaymentTypes()
        {
            List<TipoPago> types = MappingHandler.MapTableToEntities<TipoPago>(data.GetAllPaymentTypes());
            return types.Count == 0 ? null : types;
        }

        // Sin filas devuelve null.
        public List<Pago> GetAllByInvoice(Guid Invoice_Id)
        {
            List<Pago> payments = MappingHandler.MapTableToEntities<Pago>(data.GetAllByInvoice(Invoice_Id));
            return payments.Count == 0 ? null : payments;
        }

        public SQLUpdateResult AddPayment(Pago payment)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(payment.Pago_Id, payment.Factura_Id,
                payment.TipoPago_Id, payment.Monto, payment.Detalle, dvh));
        }

        public SQLUpdateResult DeletePayment(Pago payment)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(payment.Pago_Id), Table);
        }
    }
}
