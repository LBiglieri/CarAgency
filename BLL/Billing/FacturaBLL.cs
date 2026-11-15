using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Security;
using CarAgency.Security.Session;

namespace CarAgency.BLL
{
    public class FacturaBLL
    {
        private FacturaMapper _invoicemapper;
        public FacturaBLL()
        {
            _invoicemapper = new FacturaMapper();
        }
        public Factura GetById(Guid id)
        {
            return _invoicemapper.GetById(id);
        }
        public List<Factura> GetAllPendingOfPaperworkByPerson(Guid Person_id)
        {
            return _invoicemapper.GetAllPendingOfPaperworkByPerson(Person_id);
        }

        public SQLUpdateResult AddInvoice(Factura invoice)
        {
            return RecordResult(_invoicemapper.AddInvoice(invoice), TipoEvento.InvoiceCreated, invoice.Factura_Id);
        }

        public SQLUpdateResult UpdateInvoice(Factura invoice)
        {
            return RecordResult(_invoicemapper.UpdateInvoice(invoice), TipoEvento.InvoiceUpdated, invoice.Factura_Id);
        }

        public SQLUpdateResult DeleteInvoice(Factura invoice)
        {
            return RecordResult(_invoicemapper.DeleteInvoice(invoice), TipoEvento.InvoiceDeleted, invoice.Factura_Id);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
