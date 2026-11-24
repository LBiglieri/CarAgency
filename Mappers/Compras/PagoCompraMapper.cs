using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Compras;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class PagoCompraMapper
    {
        private readonly PagoCompraDataAccess data = new PagoCompraDataAccess();

        // Sin forma de pago pactada devuelve null.
        public List<FormaPagoCompra> GetPaymentTerms(Guid Acquisition_Id)
        {
            List<FormaPagoCompra> terms = MappingHandler.MapTableToEntities<FormaPagoCompra>(data.GetPaymentTerms(Acquisition_Id));
            return terms.Count == 0 ? null : terms;
        }

        public SQLUpdateResult AddPaymentTerm(FormaPagoCompra term)
        {
            return DigitVerifierWriteMapper.Save("FormaPagoCompra", dvh => data.AddPaymentTerm(term.Compra_Id, term.TipoPago_Id,
                term.Monto, dvh));
        }

        public SQLUpdateResult DeletePaymentTerms(Guid Acquisition_Id)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeletePaymentTerms(Acquisition_Id), "FormaPagoCompra");
        }

        // Sin compras para pagar ni comprobantes emitidos devuelve null.
        public List<PayableCompra> GetPendingPayment(Guid Person_Id)
        {
            List<PayableCompra> acquisitions = new List<PayableCompra>();
            using (DataTable table = data.GetPendingPayment(Person_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    PayableCompra acquisition = new PayableCompra();
                    acquisition.Compra_Id = (Guid)row["Compra_Id"];
                    acquisition.Numero = (int)row["Numero"];
                    acquisition.Patente = (string)row["Patente"];
                    acquisition.Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"];
                    acquisition.PrecioAcordado = (double)row["PrecioAcordado"];
                    acquisition.MontoPagado = (double)row["MontoPagado"];
                    acquisition.ComprobanteCompra_Numero = row["ComprobanteCompra_Numero"] as int?;
                    acquisition.Estado = (CompraStatus)(int)row["Estado"];
                    acquisitions.Add(acquisition);
                }
            }
            return acquisitions.Count == 0 ? null : acquisitions;
        }

        // Los datos de cada medio son NULL en los otros: los pagos se arman a mano.
        public List<PagoCompra> GetPayments(Guid Acquisition_Id)
        {
            List<PagoCompra> payments = new List<PagoCompra>();
            using (DataTable table = data.GetPayments(Acquisition_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    PagoCompra payment = new PagoCompra();
                    payment.PagoCompra_Id = (Guid)row["PagoCompra_Id"];
                    payment.Compra_Id = (Guid)row["Compra_Id"];
                    payment.TipoPago_Id = (Guid)row["TipoPago_Id"];
                    payment.TipoPago_Descripcion = (string)row["TipoPago_Descripcion"];
                    payment.Cajero_Id = (Guid)row["Cajero_Id"];
                    payment.Cajero = (string)row["Cajero"];
                    payment.Monto = (double)row["Monto"];
                    payment.Banco = row["Banco"] as string;
                    payment.CbuAlias = row["CbuAlias"] as string;
                    payment.NumeroOperacion = row["NumeroOperacion"] as string;
                    payment.NumeroCheque = row["NumeroCheque"] as string;
                    payment.FechaPago = row["FechaPago"] as DateTime?;
                    payment.FechaCreacion = (DateTime)row["FechaCreacion"];
                    payments.Add(payment);
                }
            }
            return payments;
        }

        public SQLUpdateResult AddPayment(PagoCompra payment)
        {
            return DigitVerifierWriteMapper.Save("PagoCompra", dvh => data.AddPayment(payment.PagoCompra_Id, payment.Compra_Id,
                payment.TipoPago_Id, payment.Cajero_Id, payment.Monto, payment.Banco, payment.CbuAlias, payment.NumeroOperacion,
                payment.NumeroCheque, payment.FechaPago, payment.FechaCreacion, dvh));
        }

        public int GetNextReceiptNumber()
        {
            using (DataTable table = data.GetNextReceiptNumber())
                return (int)table.Rows[0]["Numero"];
        }

        // Comprobante y adquisicion se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult PayAcquisition(ComprobanteCompra receipt, Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.Pay(ReceiptRow(receipt), CompraMapper.AcquisitionRow(acquisition),
                digitVerifier.Dvh("ComprobanteCompra"), digitVerifier.Dvh("Compra")), "ComprobanteCompra", "Compra");
        }

        // Sin comprobante emitido devuelve null.
        public ComprobanteCompra GetReceipt(Guid Acquisition_Id)
        {
            return MappingHandler.MapTableToEntities<ComprobanteCompra>(data.GetReceipt(Acquisition_Id)).FirstOrDefault();
        }

        private static IDictionary<string, object> ReceiptRow(ComprobanteCompra receipt)
        {
            return new Dictionary<string, object>
            {
                { "ComprobanteCompra_Id", receipt.ComprobanteCompra_Id },
                { "Numero", receipt.Numero },
                { "Compra_Id", receipt.Compra_Id },
                { "Fecha", receipt.Fecha },
                { "NombrePersona", receipt.NombrePersona },
                { "CuilCuit", receipt.CuilCuit },
                { "Detalle", receipt.Detalle },
                { "MontoNeto", receipt.MontoNeto },
                { "MontoImpuesto", receipt.MontoImpuesto },
                { "MontoTotal", receipt.MontoTotal }
            };
        }
    }
}
