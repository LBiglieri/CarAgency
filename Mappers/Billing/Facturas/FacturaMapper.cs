using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Billing;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class FacturaMapper
    {
        private const string Table = "Factura";
        private readonly FacturaDataAccess data = new FacturaDataAccess();

        public Factura GetById(Guid id)
        {
            return MappingHandler.MapTableToEntities<Factura>(data.GetById(id)).FirstOrDefault();
        }

        // Sin facturas pendientes devuelve null.
        public List<Factura> GetAllPendingOfPaperworkByPerson(Guid Person_id)
        {
            List<Factura> invoices = MappingHandler.MapTableToEntities<Factura>(data.GetAllPendingOfPaperworkByPerson(Person_id));
            return invoices.Count == 0 ? null : invoices;
        }

        public SQLUpdateResult AddInvoice(Factura invoice)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(invoice.Factura_Id, invoice.Vehiculo_Id, invoice.Persona_Id, invoice.Reserva_Id, invoice.Detalle,
                invoice.CuilCuit, invoice.RazonSocial, invoice.Valor, invoice.EstadoPago, invoice.Fecha, dvh));
        }

        public SQLUpdateResult UpdateInvoice(Factura invoice)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Update(invoice.Factura_Id, invoice.Vehiculo_Id, invoice.Persona_Id, invoice.Reserva_Id, invoice.Detalle,
                invoice.CuilCuit, invoice.RazonSocial, invoice.Valor, invoice.EstadoPago, invoice.Fecha, dvh));
        }

        // Invoice_Delete borra solo la factura: no hay cascada sobre Payments.
        public SQLUpdateResult DeleteInvoice(Factura invoice)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(invoice.Factura_Id), Table);
        }
    }
}
