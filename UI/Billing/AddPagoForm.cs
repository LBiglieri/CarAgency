using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using MetroFramework.Controls;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Personas.Controls;
using Security.Session;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{
    public partial class AddPagoForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        public Pago _payment;
        Factura _invoice;
        PagoBLL _PaymentBLL;
        List<string> cardPaymentTypes = new List<string> { "Debit Card", "Credit Card"};
        // Mientras se recarga el combo (cambio de idioma) no se limpian los datos de la tarjeta.
        bool _fillingPaymentTypes;

        public AddPagoForm(Factura invoice)
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _PaymentBLL = new PagoBLL();

            _invoice = invoice;

            // UpdateLanguage carga el combo de medios de pago con sus textos traducidos.
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void AddPaymentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("AddPagoForm");
            this.Refresh();
            tbAmount.WaterMark = LanguageService.GetTagText(tbAmount.Tag.ToString());
            tbDetails.WaterMark = LanguageService.GetTagText(tbDetails.Tag.ToString());
            tbCardNumber.WaterMark = LanguageService.GetTagText(tbCardNumber.Tag.ToString());
            tbCardName.WaterMark = LanguageService.GetTagText(tbCardName.Tag.ToString());
            tbExpireDate.WaterMark = LanguageService.GetTagText(tbExpireDate.Tag.ToString());
            tbCVV.WaterMark = LanguageService.GetTagText(tbCVV.Tag.ToString());
            btnAddPayment.Text = LanguageService.GetTagText(btnAddPayment.Tag.ToString());
            PerformUpdateReservationCombo();
        }
        private void PerformUpdateReservationCombo()
        {
            try
            {
                // Al cambiar de idioma se conserva el medio elegido; la primera vez viene el mas comun.
                TipoPago selected = SelectedPaymentType;
                _fillingPaymentTypes = selected != null;
                LookupText.Fill(comboPaymentType, _PaymentBLL.GetAllPaymentTypes(), t => LookupText.PaymentType(t.Descripcion),
                    t => selected == null ? t.Descripcion == PagoBLL.DefaultPaymentType : t.TipoPago_Id == selected.TipoPago_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            finally
            {
                _fillingPaymentTypes = false;
            }
        }
        TipoPago SelectedPaymentType { get { return LookupText.Selected<TipoPago>(comboPaymentType); } }

        bool IsCardPayment { get { return SelectedPaymentType != null && cardPaymentTypes.Contains(SelectedPaymentType.Descripcion); } }

        private Boolean ValidatePaymentCreation()
        {
            if (comboPaymentType.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectPaymentType"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbAmount.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePaymentAmount"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            double amount = 0;
            if (!double.TryParse(tbAmount.Text, out amount))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidPaymentAmount"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbDetails.Text == "" && !IsCardPayment)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePaymentDetails"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        private Boolean ValidateCard()
        {
            return IsCardNumberValid(tbCardNumber.Text) &&
               IsCardNameValid(tbCardName.Text) &&
               IsExpireDateValid(tbExpireDate.Text) &&
               IsCvvValid(tbCVV.Text);
        }
        private bool IsCardNumberValid(string cardNumber)
        {
            // Check if card number is numeric and has the correct length
            bool res = Regex.IsMatch(cardNumber, @"^\d{13,19}$");

            if (!res)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidCardNumber"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }

            // Use Luhn algorithm to validate card number
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }
            res = (sum % 10 == 0);

            if (!res)
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidCardNumber"), LanguageService.GetTagText("AttentionTitle"));

            return res;
        }

        private bool IsCardNameValid(string cardName)
        {
            // Check if card name is not empty and contains only letters and spaces
            bool res = !string.IsNullOrWhiteSpace(cardName) && cardName.All(c => char.IsLetter(c) || c == ' ');

            if (!res)
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidCardHolder"), LanguageService.GetTagText("AttentionTitle"));

            return res;
        }

        private bool IsExpireDateValid(string expireDate)
        {
            // Check if expire date is in MM/YY format
            bool res = (Regex.IsMatch(expireDate, @"^(0[1-9]|1[0-2])\/\d{2}$"));

            if (!res)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidCardExpiration"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }

            // Check if the card has not expired
            var now = DateTime.Now;
            var expiry = DateTime.ParseExact(expireDate, "MM/yy", null);
            var lastDateOfExpiryMonth = new DateTime(expiry.Year, expiry.Month, DateTime.DaysInMonth(expiry.Year, expiry.Month));

            res = lastDateOfExpiryMonth >= now;

            if (!res)
                MessageBox.Show(LanguageService.GetTagText("PleaseUseNonExpiredCard"), LanguageService.GetTagText("AttentionTitle"));

            return res;
        }

        private bool IsCvvValid(string cvv)
        {
            // Check if CVV is numeric and has the correct length (3 or 4 digits)
            bool res = Regex.IsMatch(cvv, @"^\d{3,4}$");

            if (!res)
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidCardCvv"), LanguageService.GetTagText("AttentionTitle"));
            return res;
        }
        #endregion

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatePaymentCreation())
                    return;
                if (IsCardPayment && !ValidateCard())
                    return;

                Pago payment = new Pago();
                payment.Pago_Id = Guid.NewGuid();
                payment.Factura_Id = _invoice.Factura_Id;
                payment.TipoPago_Id = SelectedPaymentType.TipoPago_Id;
                payment.Monto = double.Parse(tbAmount.Text);
                if (IsCardPayment)
                    payment.Detalle = string.Format(LanguageService.GetTagText("PaymentCardDetail"), tbCardNumber.Text.Substring(tbCardNumber.Text.Length - 4), tbCardName.Text);
                else
                    payment.Detalle = tbDetails.Text;

                SQLUpdateResult result = _PaymentBLL.AddPayment(payment, _invoice);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    _payment = payment;
                    this.Close();
                }
                else
                    return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void comboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_fillingPaymentTypes && SelectedPaymentType != null)
            {
                if (IsCardPayment)
                {
                    CardPanel.Enabled = true;
                    tbDetails.Enabled = false;
                    tbDetails.Text = "";
                }
                else
                {
                    tbCardName.Text = "";
                    tbCardNumber.Text = "";
                    tbExpireDate.Text = "";
                    tbCVV.Text = "";
                    CardPanel.Enabled = false;
                    tbDetails.Enabled = true;
                }
            }
        }

    }
}
