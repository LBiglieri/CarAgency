using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Mappers.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;

namespace CarAgency.BLL
{
    public class PaymentBLL
    {
        private PaymentMapper _paymentmapper;
        public PaymentBLL()
        {
            _paymentmapper = new PaymentMapper();
        }
        public List<PaymentType> GetAllPaymentTypes()
        {
            return _paymentmapper.GetAllPaymentTypes();
        }
        public List<Payment> GetAllByInvoice(Guid Invoice_Id)
        {
            return _paymentmapper.GetAllByInvoice(Invoice_Id);
        }

        public SQLUpdateResult AddPayment(Payment payment)
        {
            return _paymentmapper.AddPayment(payment);
        }
        public SQLUpdateResult DeletePayment(Payment payment)
        {
            return _paymentmapper.DeletePayment(payment);
        }
    }
}
