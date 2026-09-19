using System;
using System.Collections.Generic;
using CarAgency.BE;
using CarAgency.DAL.Billing;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class PaymentMapper
    {
        private const string Table = "Payments";
        private readonly PaymentDataAccess data = new PaymentDataAccess();

        // Sin filas devuelve null.
        public List<PaymentType> GetAllPaymentTypes()
        {
            List<PaymentType> types = MappingHandler.MapTableToEntities<PaymentType>(data.GetAllPaymentTypes());
            return types.Count == 0 ? null : types;
        }

        // Sin filas devuelve null.
        public List<Payment> GetAllByInvoice(Guid Invoice_Id)
        {
            List<Payment> payments = MappingHandler.MapTableToEntities<Payment>(data.GetAllByInvoice(Invoice_Id));
            return payments.Count == 0 ? null : payments;
        }

        public SQLUpdateResult AddPayment(Payment payment)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(payment.Id, payment.Invoice_Id,
                payment.PaymentType_Id, payment.Amount, payment.Detail, dvh));
        }

        public SQLUpdateResult DeletePayment(Payment payment)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(payment.Id), Table);
        }
    }
}
