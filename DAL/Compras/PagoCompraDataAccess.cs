using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Compras
{
    // Lo que la agencia le paga a la persona (PN2): forma de pago pactada, pagos y comprobante de compra.
    public sealed class PagoCompraDataAccess : DataAccessBase
    {
        public DataTable GetPaymentTerms(Guid acquisitionId)
        {
            return Read("FormaPagoCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable AddPaymentTerm(Guid acquisitionId, Guid paymentTypeId, double amount, DvhCalculator dvh)
        {
            return Read("FormaPagoCompra_Add", dvh,
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@TipoPago_Id", paymentTypeId),
                new SqlParameter("@Monto", amount));
        }

        public DataTable DeletePaymentTerms(Guid acquisitionId)
        {
            return Read("FormaPagoCompra_DeleteByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable GetPendingPayment(Guid personId) { return Read("Compra_GetPendingPayment", new SqlParameter("@Persona_Id", personId)); }

        public DataTable GetPayments(Guid acquisitionId)
        {
            return Read("PagoCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        // Las columnas de los otros medios viajan en NULL: el DVH se calcula con la fila completa.
        public DataTable AddPayment(Guid id, Guid acquisitionId, Guid paymentTypeId, Guid cashierId, double amount, string bank, string cbuAlias,
            string operationNumber, string checkNumber, DateTime? paymentDate, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("PagoCompra_Add", dvh,
                new SqlParameter("@PagoCompra_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@TipoPago_Id", paymentTypeId),
                new SqlParameter("@Cajero_Id", cashierId),
                new SqlParameter("@Monto", amount),
                new SqlParameter("@Banco", (object)bank ?? DBNull.Value),
                new SqlParameter("@CbuAlias", (object)cbuAlias ?? DBNull.Value),
                new SqlParameter("@NumeroOperacion", (object)operationNumber ?? DBNull.Value),
                new SqlParameter("@NumeroCheque", (object)checkNumber ?? DBNull.Value),
                new SqlParameter("@FechaPago", (object)paymentDate ?? DBNull.Value),
                new SqlParameter("@FechaCreacion", creationDate));
        }

        public DataTable GetNextReceiptNumber() { return Read("ComprobanteCompra_GetNextNumber"); }

        // Comprobante y pase a Pagada en un solo SP, dentro de una transaccion. Las filas llegan completas para
        // el DVH y de la adquisicion solo viaja lo que cambia.
        public DataTable Pay(IDictionary<string, object> receipt, IDictionary<string, object> acquisition, DvhCalculator receiptDvh,
            DvhCalculator acquisitionDvh)
        {
            DvhResult receiptRow = receiptDvh(receipt);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            return Read("Compra_Pay",
                new SqlParameter("@ComprobanteCompra_Id", Value(receiptRow, "ComprobanteCompra_Id")),
                new SqlParameter("@Numero", SqlDbType.Int) { Value = Value(receiptRow, "Numero") },
                new SqlParameter("@Compra_Id", Value(receiptRow, "Compra_Id")),
                new SqlParameter("@Fecha", SqlDbType.DateTime) { Value = Value(receiptRow, "Fecha") },
                new SqlParameter("@NombrePersona", SqlDbType.VarChar, 255) { Value = Value(receiptRow, "NombrePersona") },
                new SqlParameter("@CuilCuit", SqlDbType.VarChar, 13) { Value = Value(receiptRow, "CuilCuit") },
                new SqlParameter("@Detalle", SqlDbType.VarChar, -1) { Value = Value(receiptRow, "Detalle") },
                new SqlParameter("@MontoNeto", SqlDbType.Float) { Value = Value(receiptRow, "MontoNeto") },
                new SqlParameter("@MontoImpuesto", SqlDbType.Float) { Value = Value(receiptRow, "MontoImpuesto") },
                new SqlParameter("@MontoTotal", SqlDbType.Float) { Value = Value(receiptRow, "MontoTotal") },
                new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = receiptRow.Dvh },
                new SqlParameter("@Estado", SqlDbType.Int) { Value = Value(acquisitionRow, "Estado") },
                new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
        }

        public DataTable GetReceipt(Guid acquisitionId)
        {
            return Read("ComprobanteCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }
    }
}
