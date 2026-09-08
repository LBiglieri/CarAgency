using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
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
            return RecordResult(_invoicemapper.AddInvoice(invoice), AuditEventType.InvoiceCreated, invoice.Id);
        }

        public SQLUpdateResult UpdateInvoice(Invoice invoice)
        {
            return RecordResult(_invoicemapper.UpdateInvoice(invoice), AuditEventType.InvoiceUpdated, invoice.Id);
        }

        public SQLUpdateResult DeleteInvoice(Invoice invoice)
        {
            return RecordResult(_invoicemapper.DeleteInvoice(invoice), AuditEventType.InvoiceDeleted, invoice.Id);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, AuditEventType type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(type, targetId);
            return result;
        }
    }
}
