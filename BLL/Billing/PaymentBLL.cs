using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;

namespace CarAgency.BLL
{
    public class PaymentBLL
    {
        private PaymentRepository _paymentrepository;
        public PaymentBLL()
        {
            _paymentrepository = new PaymentRepository();
        }
        public List<PaymentType> GetAllPaymentTypes()
        {
            return _paymentrepository.GetAllPaymentTypes();
        }
        public List<Payment> GetAllByInvoice(Guid Invoice_Id)
        {
            return _paymentrepository.GetAllByInvoice(Invoice_Id);
        }

        public SQLUpdateResult AddPayment(Payment payment)
        {
            return _paymentrepository.AddPayment(payment);
        }
        public SQLUpdateResult DeletePayment(Payment payment)
        {
            return _paymentrepository.DeletePayment(payment);
        }
    }
}
