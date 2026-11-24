using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;
using BLL;

namespace CarAgency.BLL
{
    // Lo que la agencia le paga a la persona: la forma de pago pactada en el acuerdo (CUF10), los pagos y el
    // comprobante de compra (CUF11).
    public class PagoCompraBLL
    {
        // Medios con los que la agencia le paga a la persona (CUF10 paso 5). Se filtran por Description,
        // como las tarjetas en AddPaymentForm.
        public const string BankTransferPaymentType = "Bank Transfer";
        public const string CheckPaymentType = "Check";
        public const string CashPaymentType = "Cash";
        public static readonly string[] AcquisitionPaymentTypes = { BankTransferPaymentType, CheckPaymentType, CashPaymentType };
        // El que viene elegido al armar la forma de pago: es el mas comun.
        public const string DefaultPaymentType = BankTransferPaymentType;

        // Emisor del comprobante de compra (CUF11 paso 7). PurchaseReceipts no lo guarda porque es siempre la agencia.
        public const string AgencyName = "CarAgency";
        public const string AgencyCuit = "30-71654328-1";

        // CBU de 22 digitos o alias de 6 a 20 caracteres (letras, numeros, punto o guion).
        private static readonly Regex CbuFormat = new Regex(@"^\d{22}$");
        private static readonly Regex AliasFormat = new Regex(@"^[A-Za-z0-9.\-]{6,20}$");

        private PagoCompraMapper _purchasepaymentmapper;
        private PagoMapper _paymentmapper;
        private CompraMapper _acquisitionmapper;
        public PagoCompraBLL()
        {
            _purchasepaymentmapper = new PagoCompraMapper();
            _paymentmapper = new PagoMapper();
            _acquisitionmapper = new CompraMapper();
        }

        public List<TipoPago> GetAcquisitionPaymentTypes()
        {
            List<TipoPago> types = _paymentmapper.GetAllPaymentTypes();
            return types == null ? new List<TipoPago>() : types.Where(t => AcquisitionPaymentTypes.Contains(t.Descripcion)).ToList();
        }
        public List<FormaPagoCompra> GetPaymentTerms(Guid Acquisition_Id)
        {
            return _purchasepaymentmapper.GetPaymentTerms(Acquisition_Id);
        }
        // CUF10 paso 6: la suma se compara redondeada a centavos. Positiva, falta asignar; negativa, sobra.
        public decimal GetPaymentTermsDifference(double price, List<FormaPagoCompra> terms)
        {
            double total = terms == null ? 0 : terms.Sum(t => t.Monto);
            return (decimal)Math.Round(price, 2) - (decimal)Math.Round(total, 2);
        }

        // CUF10 pasos 5 y 6: medios permitidos, una vez cada uno, con monto, y la suma igual al precio (6.1).
        internal void ValidatePaymentTerms(double price, List<FormaPagoCompra> terms)
        {
            if (terms == null || terms.Count == 0)
                throw new TranslatableException("PleaseAddPaymentTerm", "Agregue al menos un medio de pago para continuar.");
            List<Guid> allowed = GetAcquisitionPaymentTypes().Select(t => t.TipoPago_Id).ToList();
            if (terms.Any(t => !allowed.Contains(t.TipoPago_Id)))
                throw new TranslatableException("InvalidAcquisitionPaymentType", "La compra solo se paga con transferencia, cheque o efectivo.");
            if (terms.GroupBy(t => t.TipoPago_Id).Any(g => g.Count() > 1))
                throw new TranslatableException("PaymentTypeAlreadyAdded", "El medio de pago ya esta en la forma de pago. Quitelo para cambiar el monto.");
            if (terms.Any(t => t.Monto <= 0))
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            decimal difference = GetPaymentTermsDifference(price, terms);
            if (difference != 0)
                throw new TranslatableException("PaymentTermsDifference", "La forma de pago no coincide con el precio acordado. Diferencia: $ {0}.", difference.ToString("N2"));
        }

        // CUF10 paso 7: la forma de pago se reemplaza completa cada vez que se genera el contrato. Sigue la cadena
        // del acuerdo: cada paso corre solo si el anterior (empezando por el de la adquisicion) salio bien.
        internal SQLUpdateResult ReplacePaymentTerms(Guid Acquisition_Id, List<FormaPagoCompra> terms, SQLUpdateResult result)
        {
            if (result != null && result.sqlResult == SQLResultType.success && GetPaymentTerms(Acquisition_Id) != null)
                result = _purchasepaymentmapper.DeletePaymentTerms(Acquisition_Id);
            foreach (FormaPagoCompra term in terms)
            {
                term.Compra_Id = Acquisition_Id;
                if (result != null && result.sqlResult == SQLResultType.success)
                    result = _purchasepaymentmapper.AddPaymentTerm(term);
            }
            return result;
        }

        // CUF11 paso 2: compras de la persona con la documentacion completa y las que ya tienen comprobante.
        public List<PayableCompra> GetPayableAcquisitions(Guid Person_Id)
        {
            return _purchasepaymentmapper.GetPendingPayment(Person_Id);
        }
        public List<PagoCompra> GetPayments(Guid Acquisition_Id)
        {
            return _purchasepaymentmapper.GetPayments(Acquisition_Id);
        }
        public ComprobanteCompra GetPurchaseReceipt(Guid Acquisition_Id)
        {
            return _purchasepaymentmapper.GetReceipt(Acquisition_Id);
        }
        // Saldo de un medio: lo pactado menos lo pagado, redondeado a centavos como la forma de pago.
        public decimal GetPaymentTermBalance(FormaPagoCompra term)
        {
            return (decimal)Math.Round(term.Monto, 2) - (decimal)Math.Round(term.MontoPagado, 2);
        }
        public decimal GetBalance(List<FormaPagoCompra> terms)
        {
            return terms == null ? 0 : terms.Sum(t => GetPaymentTermBalance(t));
        }

        // CUF11 pasos 3 a 5: el pago es con un medio pactado y no puede superar su saldo (4.1). Cada medio
        // guarda solo sus datos: la transferencia banco, CBU o alias y operacion; el cheque banco, numero y fecha.
        public SQLUpdateResult AddPayment(PagoCompra payment)
        {
            CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, payment.Compra_Id, CompraStatus.DocumentationComplete);
            List<FormaPagoCompra> terms = GetPaymentTerms(payment.Compra_Id);
            FormaPagoCompra term = terms == null ? null : terms.FirstOrDefault(t => t.TipoPago_Id == payment.TipoPago_Id);
            if (term == null)
                throw new TranslatableException("PaymentTypeNotAgreed", "El medio de pago no esta en la forma de pago pactada con la persona.");
            if (payment.Monto <= 0)
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            decimal balance = GetPaymentTermBalance(term);
            if ((decimal)Math.Round(payment.Monto, 2) > balance)
                throw new TranslatableException("PaymentExceedsBalance", "El monto supera el saldo del medio pactado: $ {0}.", balance.ToString("N2"));

            payment.Monto = Math.Round(payment.Monto, 2);
            NormalizePaymentDetails(payment, term.TipoPago_Descripcion);
            payment.Cajero_Id = SessionHandler.Instance.GetId();
            payment.FechaCreacion = DateTime.Now;
            return RecordResult(_purchasepaymentmapper.AddPayment(payment), TipoEvento.AcquisitionPaymentCreated, payment.PagoCompra_Id);
        }

        // CUF11 pasos 7 y 9: con el saldo en cero se emite el comprobante y la compra pasa a Pagada, en una sola
        // transaccion. Si fallara despues del ultimo pago, la compra sigue en Documentacion completa y se reintenta.
        public SQLUpdateResult IssuePurchaseReceipt(Guid Acquisition_Id)
        {
            Compra acquisition = CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, Acquisition_Id, CompraStatus.DocumentationComplete);
            decimal balance = GetBalance(GetPaymentTerms(Acquisition_Id));
            if (balance != 0)
                throw new TranslatableException("PurchaseBalancePending", "La compra todavia tiene saldo pendiente: $ {0}.", balance.ToString("N2"));

            CompraSummary summary = _acquisitionmapper.GetSummary(Acquisition_Id);
            Persona person = new PersonaBLL().GetById(acquisition.Persona_Id);
            ComprobanteCompra receipt = new ComprobanteCompra();
            receipt.ComprobanteCompra_Id = Guid.NewGuid();
            receipt.Numero = _purchasepaymentmapper.GetNextReceiptNumber();
            receipt.Compra_Id = acquisition.Compra_Id;
            receipt.Fecha = DateTime.Now;
            receipt.NombrePersona = person.Nombre + " " + person.Apellido;
            receipt.CuilCuit = acquisition.CuilCuitTitular;
            receipt.Detalle = summary.Vehiculo_Descripcion + " - " + summary.Patente + " - " + summary.NumeroChasis + " - " + summary.NumeroMotor;
            // La compra a un particular no lleva impuestos: el total es el precio acordado.
            receipt.MontoNeto = Math.Round(acquisition.PrecioAcordado.Value, 2);
            receipt.MontoImpuesto = 0;
            receipt.MontoTotal = receipt.MontoNeto + receipt.MontoImpuesto;
            acquisition.Estado = CompraStatus.Paid;

            return RecordResult(_purchasepaymentmapper.PayAcquisition(receipt, acquisition), TipoEvento.PurchaseReceiptIssued, acquisition.Compra_Id);
        }

        // CUF11 paso 4: datos obligatorios de cada medio. Los del resto quedan en NULL.
        private static void NormalizePaymentDetails(PagoCompra payment, string paymentType)
        {
            string bank = payment.Banco == null ? "" : payment.Banco.Trim();
            string cbu_alias = payment.CbuAlias == null ? "" : payment.CbuAlias.Trim();
            string operation_number = payment.NumeroOperacion == null ? "" : payment.NumeroOperacion.Trim();
            string check_number = payment.NumeroCheque == null ? "" : payment.NumeroCheque.Trim();
            DateTime? payment_date = payment.FechaPago;
            payment.Banco = null;
            payment.CbuAlias = null;
            payment.NumeroOperacion = null;
            payment.NumeroCheque = null;
            payment.FechaPago = null;

            if (paymentType == BankTransferPaymentType)
            {
                if (bank == "")
                    throw new TranslatableException("PleaseWriteBank", "Escriba el banco para continuar.");
                if (!CbuFormat.IsMatch(cbu_alias) && !(AliasFormat.IsMatch(cbu_alias) && !cbu_alias.All(char.IsDigit)))
                    throw new TranslatableException("PleaseWriteValidCbuAlias", "Escriba un CBU de 22 digitos o un alias de 6 a 20 caracteres (letras, numeros, punto o guion).");
                if (operation_number == "")
                    throw new TranslatableException("PleaseWriteOperationNumber", "Escriba el numero de operacion de la transferencia para continuar.");
                payment.Banco = bank;
                payment.CbuAlias = cbu_alias;
                payment.NumeroOperacion = operation_number;
            }
            else if (paymentType == CheckPaymentType)
            {
                if (bank == "")
                    throw new TranslatableException("PleaseWriteBank", "Escriba el banco para continuar.");
                if (check_number == "")
                    throw new TranslatableException("PleaseWriteCheckNumber", "Escriba el numero de cheque para continuar.");
                if (payment_date == null)
                    throw new TranslatableException("PleaseSelectCheckPaymentDate", "Seleccione la fecha de pago del cheque para continuar.");
                if (payment_date.Value.Date < DateTime.Today)
                    throw new TranslatableException("CheckPaymentDateInPast", "La fecha de pago del cheque no puede ser anterior a hoy.");
                payment.Banco = bank;
                payment.NumeroCheque = check_number;
                payment.FechaPago = payment_date.Value.Date;
            }
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
