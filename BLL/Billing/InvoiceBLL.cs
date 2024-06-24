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
    public class InvoiceBLL
    {
        private InvoiceRepository _invoicerepository;
        public InvoiceBLL()
        {
            _invoicerepository = new InvoiceRepository();
        }
        public Invoice GetById(Guid id)
        {
            return _invoicerepository.GetById(id);
        }
        public List<Invoice> GetAllPendingOfPaperwork()
        {
            return _invoicerepository.GetAllPendingOfPaperwork();
        }

        public SQLUpdateResult AddInvoice(Invoice invoice)
        {
            return _invoicerepository.AddInvoice(invoice);
        }

        public SQLUpdateResult UpdateInvoice(Invoice invoice)
        {
            return _invoicerepository.UpdateInvoice(invoice);
        }

        public SQLUpdateResult DeleteInvoice(Invoice invoice)
        {
            return _invoicerepository.DeleteInvoice(invoice);
        }
    }
}
