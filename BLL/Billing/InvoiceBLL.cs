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
    public class InvoiceBLL
    {
        private InvoiceMapper _invoicemapper;
        public InvoiceBLL()
        {
            _invoicemapper = new InvoiceMapper();
        }
        public Invoice GetById(Guid id)
        {
            return _invoicemapper.GetById(id);
        }
        public List<Invoice> GetAllPendingOfPaperworkByClient(Guid Client_id)
        {
            return _invoicemapper.GetAllPendingOfPaperworkByClient(Client_id);
        }

        public SQLUpdateResult AddInvoice(Invoice invoice)
        {
            return _invoicemapper.AddInvoice(invoice);
        }

        public SQLUpdateResult UpdateInvoice(Invoice invoice)
        {
            return _invoicemapper.UpdateInvoice(invoice);
        }

        public SQLUpdateResult DeleteInvoice(Invoice invoice)
        {
            return _invoicemapper.DeleteInvoice(invoice);
        }
    }
}
