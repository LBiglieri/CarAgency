using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
using CarAgency.UI;
using CarAgency.Utilities.Persistence;
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
using UI.Clients.Controls;

namespace UI.Vehicles
{
    public partial class AddPaymentForm : MetroFramework.Forms.MetroForm
    {
        public Payment _payment;
        Invoice _invoice;
        PaymentBLL _PaymentBLL;
        List<string> cardPaymentTypes = new List<string> { "Debit Card", "Credit Card"};

        public AddPaymentForm(Invoice invoice)
        {
            InitializeComponent();
            _PaymentBLL = new PaymentBLL();

            _invoice = invoice;

            PerformUpdateReservationCombo();
        }
        #region "Performs"
        private void PerformUpdateReservationCombo()
        {
            try
            {
                List<PaymentType> paymenttypes = _PaymentBLL.GetAllPaymentTypes();
                comboPaymentType.DataSource = paymenttypes;
                comboPaymentType.DisplayMember = "Description";
                comboPaymentType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Boolean ValidatePaymentCreation()
        {
            if (comboPaymentType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Payment Type to continue.", "¡ATENTION!");
                return false;
            }
            if (tbAmount.Text == "")
            {
                MessageBox.Show("Please write the payment amount to continue.", "¡ATENTION!");
                return false;
            }
            double amount = 0;
            if (!double.TryParse(tbAmount.Text, out amount))
            {
                MessageBox.Show("Please write a valid payment amount to continue.", "¡ATENTION!");
                return false;
            }
            if (tbDetails.Text == "" && !cardPaymentTypes.Contains(((PaymentType)comboPaymentType.SelectedItem).Description))
            {
                MessageBox.Show("Please write the payment details to continue.", "¡ATENTION!");
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
                MessageBox.Show("Please write a valid card number to continue.", "¡ATENTION!");
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
                MessageBox.Show("Please write a valid card number to continue.", "¡ATENTION!");

            return res;
        }

        private bool IsCardNameValid(string cardName)
        {
            // Check if card name is not empty and contains only letters and spaces
            bool res = !string.IsNullOrWhiteSpace(cardName) && cardName.All(c => char.IsLetter(c) || c == ' ');

            if (!res)
                MessageBox.Show("Please write a valid card holder's name to continue.", "¡ATENTION!");

            return res;
        }

        private bool IsExpireDateValid(string expireDate)
        {
            // Check if expire date is in MM/YY format
            bool res = (Regex.IsMatch(expireDate, @"^(0[1-9]|1[0-2])\/\d{2}$"));

            if (!res)
            {
                MessageBox.Show("Please write a valid card valid expiration date to continue.", "¡ATENTION!");
                return false;
            }

            // Check if the card has not expired
            var now = DateTime.Now;
            var expiry = DateTime.ParseExact(expireDate, "MM/yy", null);
            var lastDateOfExpiryMonth = new DateTime(expiry.Year, expiry.Month, DateTime.DaysInMonth(expiry.Year, expiry.Month));

            res = lastDateOfExpiryMonth >= now;

            if (!res)
                MessageBox.Show("Please use a non expired card to continue.", "¡ATENTION!");

            return res;
        }

        private bool IsCvvValid(string cvv)
        {
            // Check if CVV is numeric and has the correct length (3 or 4 digits)
            bool res = Regex.IsMatch(cvv, @"^\d{3,4}$");

            if (!res)
                MessageBox.Show("Please write a valid card CVV to continue.", "¡ATENTION!");
            return res;
        }
        #endregion

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatePaymentCreation())
                    return;
                if (cardPaymentTypes.Contains(((PaymentType)comboPaymentType.SelectedItem).Description) && !ValidateCard())
                    return;

                Payment payment = new Payment();
                payment.Id = Guid.NewGuid();
                payment.Invoice_Id = _invoice.Id;
                payment.PaymentType_Id = ((PaymentType)comboPaymentType.SelectedItem).Id;
                payment.Amount = double.Parse(tbAmount.Text);
                if (cardPaymentTypes.Contains(((PaymentType)comboPaymentType.SelectedItem).Description))
                    payment.Detail = "Card that ends with " + tbCardNumber.Text.Substring(tbCardNumber.Text.Length - 4) + " Card holder: " + tbCardName.Text;
                else
                    payment.Detail = tbDetails.Text;

                SQLUpdateResult result = _PaymentBLL.AddPayment(payment);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void comboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPaymentType.Items.Count > 0 && comboPaymentType.SelectedItem != null)
            {
                if (cardPaymentTypes.Contains(((PaymentType)comboPaymentType.SelectedItem).Description))
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
