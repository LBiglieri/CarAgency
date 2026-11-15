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
using BE;

namespace CarAgency.BLL
{
    public class PagoBLL
    {
        // Medio de pago que viene elegido al cobrar una factura: es el mas comun.
        public const string DefaultPaymentType = "Bank Transfer";
        // Los importes son double: un saldo de menos de un centavo cuenta como pagado.
        public const double AmountTolerance = 0.005;

        private PagoMapper _paymentmapper;
        public PagoBLL()
        {
            _paymentmapper = new PagoMapper();
        }
        public List<TipoPago> GetAllPaymentTypes()
        {
            return _paymentmapper.GetAllPaymentTypes();
        }
        public List<Pago> GetAllByInvoice(Guid Invoice_Id)
        {
            return _paymentmapper.GetAllByInvoice(Invoice_Id);
        }

        // Lo que falta pagar de la factura. Con pagos de mas da negativo.
        public static double Balance(double total, List<Pago> payments)
        {
            return total - (payments == null ? 0 : payments.Sum(p => p.Monto));
        }

        // Un pago no puede ser cero ni superar lo que falta pagar de la factura.
        public SQLUpdateResult AddPayment(Pago payment, Factura invoice)
        {
            if (payment.Monto <= 0)
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            double balance = Balance(invoice.Valor, GetAllByInvoice(invoice.Factura_Id));
            if (payment.Monto > balance + AmountTolerance)
                throw new TranslatableException("InvoicePaymentExceedsBalance", "El pago supera lo que falta pagar de la factura: $ {0}.", balance.ToString("N2"));
            return RecordResult(_paymentmapper.AddPayment(payment), TipoEvento.PaymentCreated, payment.Pago_Id);
        }
        public SQLUpdateResult DeletePayment(Pago payment)
        {
            return RecordResult(_paymentmapper.DeletePayment(payment), TipoEvento.PaymentDeleted, payment.Pago_Id);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
