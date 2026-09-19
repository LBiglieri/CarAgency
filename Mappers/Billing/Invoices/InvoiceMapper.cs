using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Billing;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class InvoiceMapper
    {
        private const string Table = "Invoice";
        private readonly InvoiceDataAccess data = new InvoiceDataAccess();

        public Invoice GetById(Guid id)
        {
            return MappingHandler.MapTableToEntities<Invoice>(data.GetById(id)).FirstOrDefault();
        }

        // Sin facturas pendientes devuelve null.
        public List<Invoice> GetAllPendingOfPaperworkByClient(Guid Client_id)
        {
            List<Invoice> invoices = MappingHandler.MapTableToEntities<Invoice>(data.GetAllPendingOfPaperworkByClient(Client_id));
            return invoices.Count == 0 ? null : invoices;
        }

        public SQLUpdateResult AddInvoice(Invoice invoice)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(invoice.Id, invoice.Vehicle_Id, invoice.Client_Id, invoice.Reservation_Id, invoice.Detail,
                invoice.CUIL_CUIT_Client, invoice.Razon_Social, invoice.Amount, invoice.Payment_Status, invoice.Creation_Date, dvh));
        }

        public SQLUpdateResult UpdateInvoice(Invoice invoice)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Update(invoice.Id, invoice.Vehicle_Id, invoice.Client_Id, invoice.Reservation_Id, invoice.Detail,
                invoice.CUIL_CUIT_Client, invoice.Razon_Social, invoice.Amount, invoice.Payment_Status, invoice.Creation_Date, dvh));
        }

        // Invoice_Delete borra solo la factura: no hay cascada sobre Payments.
        public SQLUpdateResult DeleteInvoice(Invoice invoice)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(invoice.Id), Table);
        }
    }
}
