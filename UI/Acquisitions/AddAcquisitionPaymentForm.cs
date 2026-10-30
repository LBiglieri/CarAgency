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

namespace UI.Vehicles
{
    // Datos del pago segun el medio pactado (CUF11 paso 4), como AddPaymentForm en la facturacion.
    public partial class AddAcquisitionPaymentForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        public AcquisitionPayment _payment;
        Acquisition _acquisition;
        AcquisitionPaymentTerm _term;
        decimal _balance;
        AcquisitionBLL _AcquisitionBLL;

        public AddAcquisitionPaymentForm(Acquisition acquisition, AcquisitionPaymentTerm term, decimal balance)
        {
            InitializeComponent();
            _AcquisitionBLL = new AcquisitionBLL();

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
            this.Text = LanguageService.GetTagText("AddAcquisitionPaymentForm");
            this.Refresh();
            lblPaymentMethod.Text = LanguageService.GetTagText(lblPaymentMethod.Tag.ToString())
                + LanguageService.GetTagText("PaymentType_" + _term.PaymentType_Description.Replace(" ", ""), _term.PaymentType_Description);
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
            return _term.PaymentType_Description == AcquisitionBLL.BankTransferPaymentType;
        }
        private bool IsCheck()
        {
            return _term.PaymentType_Description == AcquisitionBLL.CheckPaymentType;
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

                AcquisitionPayment payment = new AcquisitionPayment();
                payment.Id = Guid.NewGuid();
                payment.Acquisition_Id = _acquisition.Id;
                payment.PaymentType_Id = _term.PaymentType_Id;
                payment.PaymentType_Description = _term.PaymentType_Description;
                payment.Amount = double.Parse(tbAmount.Text);
                payment.Bank = tbBank.Text;
                payment.Cbu_Alias = tbCbu_Alias.Text;
                payment.Operation_Number = tbOperation_Number.Text;
                payment.Check_Number = tbCheck_Number.Text;
                payment.Payment_Date = dtPayment_Date.Value;

                SQLUpdateResult result = _AcquisitionBLL.AddPayment(payment);
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
