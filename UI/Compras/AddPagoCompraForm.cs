using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Security.Session;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{
    // Datos del pago segun el medio pactado (CUF11 paso 4), como AddPaymentForm en la facturacion.
    public partial class AddPagoCompraForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        public PagoCompra _payment;
        Compra _acquisition;
        FormaPagoCompra _term;
        decimal _balance;
        PagoCompraBLL _PurchasePaymentBLL;

        public AddPagoCompraForm(Compra acquisition, FormaPagoCompra term, decimal balance)
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _PurchasePaymentBLL = new PagoCompraBLL();

            _acquisition = acquisition;
            _term = term;
            _balance = balance;

            // El monto arranca en el saldo del medio; para un pago parcial se cambia.
            tbAmount.Text = _balance.ToString("0.00");
            tbBank.Visible = IsBankTransfer() || IsCheck();
            TransferPanel.Visible = IsBankTransfer();
            CheckPanel.Visible = IsCheck();
            dtPayment_Date.Value = DateTime.Today;

            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void AddAcquisitionPaymentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("AddPagoCompraForm");
            this.Refresh();
            lblPaymentMethod.Text = LanguageService.GetTagText(lblPaymentMethod.Tag.ToString())
                + LookupText.PaymentType(_term.TipoPago_Descripcion);
            lblTermBalance.Text = string.Format(LanguageService.GetTagText(lblTermBalance.Tag.ToString()), _balance.ToString("N2"));
            tbAmount.WaterMark = LanguageService.GetTagText(tbAmount.Tag.ToString());
            tbBank.WaterMark = LanguageService.GetTagText(tbBank.Tag.ToString());
            tbCbu_Alias.WaterMark = LanguageService.GetTagText(tbCbu_Alias.Tag.ToString());
            tbOperation_Number.WaterMark = LanguageService.GetTagText(tbOperation_Number.Tag.ToString());
            tbCheck_Number.WaterMark = LanguageService.GetTagText(tbCheck_Number.Tag.ToString());
            lblCheckPaymentDate.Text = LanguageService.GetTagText(lblCheckPaymentDate.Tag.ToString());
            btnAddPayment.Text = LanguageService.GetTagText(btnAddPayment.Tag.ToString());
        }
        private bool IsBankTransfer()
        {
            return _term.TipoPago_Descripcion == PagoCompraBLL.BankTransferPaymentType;
        }
        private bool IsCheck()
        {
            return _term.TipoPago_Descripcion == PagoCompraBLL.CheckPaymentType;
        }
        private Boolean ValidatePaymentCreation()
        {
            double amount;
            if (!double.TryParse(tbAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidPaymentAmount"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if ((IsBankTransfer() || IsCheck()) && tbBank.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteBank"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (IsBankTransfer() && tbCbu_Alias.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidCbuAlias"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (IsBankTransfer() && tbOperation_Number.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteOperationNumber"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (IsCheck() && tbCheck_Number.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteCheckNumber"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        #endregion

        // 4.1: si el monto supera el saldo del medio, la BLL lo rechaza y se sigue en este formulario.
        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatePaymentCreation())
                    return;

                PagoCompra payment = new PagoCompra();
                payment.PagoCompra_Id = Guid.NewGuid();
                payment.Compra_Id = _acquisition.Compra_Id;
                payment.TipoPago_Id = _term.TipoPago_Id;
                payment.TipoPago_Descripcion = _term.TipoPago_Descripcion;
                payment.Monto = double.Parse(tbAmount.Text);
                payment.Banco = tbBank.Text;
                payment.CbuAlias = tbCbu_Alias.Text;
                payment.NumeroOperacion = tbOperation_Number.Text;
                payment.NumeroCheque = tbCheck_Number.Text;
                payment.FechaPago = dtPayment_Date.Value;

                SQLUpdateResult result = _PurchasePaymentBLL.AddPayment(payment);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    _payment = payment;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
