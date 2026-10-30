using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Clients.Controls;
using Security.Session;

namespace UI.Vehicles
{
    public partial class PayAcquisitionForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        AcquisitionBLL _AcquisitionBLL;
        Client client;
        List<PayableAcquisition> payable_acquisitions;
        PayableAcquisition payable;
        Acquisition acquisition;
        PurchaseReceipt receipt;
        List<AcquisitionPaymentTerm> payment_terms = new List<AcquisitionPaymentTerm>();
        List<AcquisitionPayment> payments = new List<AcquisitionPayment>();

        public PayAcquisitionForm()
        {
            InitializeComponent();
            // El Designer no guarda AutoGenerateColumns: la grilla usa las columnas definidas ahi.
            gridAcquisitions.AutoGenerateColumns = false;
            _AcquisitionBLL = new AcquisitionBLL();

            this.clientView1.ClientFound += new EventHandler(clientView1_ClientFound);

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void PayAcquisitionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("PayAcquisitionForm");
            this.Refresh();
            btnChangeClient.Text = LanguageService.GetTagText(btnChangeClient.Tag.ToString());
            lblClientAcquisitions.Text = LanguageService.GetTagText(lblClientAcquisitions.Tag.ToString());
            colNumber.HeaderText = LanguageService.GetTagText(colNumber.Tag.ToString());
            colLicense_Plate.HeaderText = LanguageService.GetTagText(colLicense_Plate.Tag.ToString());
            colAgreed_Price.HeaderText = LanguageService.GetTagText(colAgreed_Price.Tag.ToString());
            colBalance.HeaderText = LanguageService.GetTagText(colBalance.Tag.ToString());
            colStatus.HeaderText = LanguageService.GetTagText(colStatus.Tag.ToString());
            lblPaymentTerms.Text = LanguageService.GetTagText(lblPaymentTerms.Tag.ToString());
            colPaymentType.HeaderText = LanguageService.GetTagText(colPaymentType.Tag.ToString());
            colTermAmount.HeaderText = LanguageService.GetTagText(colTermAmount.Tag.ToString());
            colPaidAmount.HeaderText = LanguageService.GetTagText(colPaidAmount.Tag.ToString());
            colTermBalance.HeaderText = LanguageService.GetTagText(colTermBalance.Tag.ToString());
            btnAddPayment.Text = LanguageService.GetTagText(btnAddPayment.Tag.ToString());
            lblPayments.Text = LanguageService.GetTagText(lblPayments.Tag.ToString());
            colPaymentDate.HeaderText = LanguageService.GetTagText(colPaymentDate.Tag.ToString());
            colPaymentMethod.HeaderText = LanguageService.GetTagText(colPaymentMethod.Tag.ToString());
            colPaymentAmount.HeaderText = LanguageService.GetTagText(colPaymentAmount.Tag.ToString());
            colPaymentDetail.HeaderText = LanguageService.GetTagText(colPaymentDetail.Tag.ToString());
            btnPrintReceipt.Text = LanguageService.GetTagText(btnPrintReceipt.Tag.ToString());
            btnIssueReceipt.Text = LanguageService.GetTagText(btnIssueReceipt.Tag.ToString());
            clientView1.UpdateLanguage(language);
            PerformUpdateTermsView();
            PerformUpdatePaymentsView();
            gridAcquisitions.Refresh();
            MapAcquisitionToControls();
        }
        void PerformUpdateAcquisitionsView(Guid? Acquisition_Id)
        {
            try
            {
                payable_acquisitions = client == null ? null : _AcquisitionBLL.GetPayableAcquisitions(client.Id);
                // Despues de un pago cambian lo pagado y el saldo: la compra se vuelve a cargar.
                payable = null;
                gridAcquisitions.DataSource = null;
                gridAcquisitions.DataSource = payable_acquisitions;
                if (Acquisition_Id != null)
                {
                    foreach (DataGridViewRow row in gridAcquisitions.Rows)
                    {
                        if (((PayableAcquisition)row.DataBoundItem).Id == Acquisition_Id.Value)
                        {
                            gridAcquisitions.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
                PerformSelectAcquisition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformSelectAcquisition()
        {
            try
            {
                PayableAcquisition selected = gridAcquisitions.CurrentRow == null ? null : (PayableAcquisition)gridAcquisitions.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearAcquisition();
                    return;
                }
                if (payable != null && payable.Id == selected.Id)
                    return;

                ClearAcquisition();
                payable = selected;
                acquisition = _AcquisitionBLL.GetAcquisition(payable.Id);
                List<AcquisitionPaymentTerm> terms = _AcquisitionBLL.GetPaymentTerms(payable.Id);
                payment_terms = terms == null ? new List<AcquisitionPaymentTerm>() : terms;
                payments = _AcquisitionBLL.GetPayments(payable.Id);
                receipt = _AcquisitionBLL.GetPurchaseReceipt(payable.Id);
                PerformUpdateTermsView();
                PerformUpdatePaymentsView();
                MapAcquisitionToControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void ClearAcquisition()
        {
            payable = null;
            acquisition = null;
            receipt = null;
            payment_terms = new List<AcquisitionPaymentTerm>();
            payments = new List<AcquisitionPayment>();
            PerformUpdateTermsView();
            PerformUpdatePaymentsView();
            MapAcquisitionToControls();
        }
        // CUF11 paso 2: lo pactado en CUF10, lo pagado y el saldo de cada medio.
        private void PerformUpdateTermsView()
        {
            gridTerms.Rows.Clear();
            foreach (AcquisitionPaymentTerm term in payment_terms)
            {
                int index = gridTerms.Rows.Add(GetPaymentTypeDescription(term.PaymentType_Description), term.Amount.ToString("N2"),
                    term.Paid_Amount.ToString("N2"), _AcquisitionBLL.GetPaymentTermBalance(term).ToString("N2"));
                gridTerms.Rows[index].Tag = term;
            }
        }
        private void PerformUpdatePaymentsView()
        {
            gridPayments.Rows.Clear();
            foreach (AcquisitionPayment payment in payments)
            {
                int index = gridPayments.Rows.Add(payment.Creation_Date.ToString("dd/MM/yyyy HH:mm"), GetPaymentTypeDescription(payment.PaymentType_Description),
                    payment.Amount.ToString("N2"), GetPaymentDetail(payment));
                gridPayments.Rows[index].Tag = payment;
            }
        }
        private void MapAcquisitionToControls()
        {
            if (acquisition == null || payable == null)
            {
                lblAcquisitionDetail.Text = "";
                lblAgreement.Text = "";
                lblBalance.Text = "";
                lblStageMessage.Text = "";
                UpdatePaymentButtons();
                return;
            }
            lblAcquisitionDetail.Text = LanguageService.GetTagText("AppraisalAcquisitionNumber") + payable.Number + ". " + payable.Vehicle_Description
                + " - " + payable.License_Plate;
            lblAgreement.Text = string.Format(LanguageService.GetTagText("PaymentAgreement"), acquisition.Agreed_Price.Value.ToString("N2"),
                acquisition.Owner_CUIL_CUIT);
            lblBalance.Text = string.Format(LanguageService.GetTagText("PurchaseBalance"), _AcquisitionBLL.GetBalance(payment_terms).ToString("N2"));
            if (receipt != null)
                lblStageMessage.Text = string.Format(LanguageService.GetTagText("PurchaseReceiptIssuedOn"), receipt.Number,
                    receipt.Issue_Date.ToString("dd/MM/yyyy"));
            else if (_AcquisitionBLL.GetBalance(payment_terms) == 0)
                lblStageMessage.Text = LanguageService.GetTagText("PurchaseBalanceCancelled");
            else
                lblStageMessage.Text = "";
            UpdatePaymentButtons();
        }
        // Se paga mientras hay saldo; en cero sin comprobante (se corto antes del cierre) solo queda emitirlo.
        private void UpdatePaymentButtons()
        {
            bool pending = acquisition != null && acquisition.Status == AcquisitionStatus.DocumentationComplete && receipt == null;
            decimal balance = _AcquisitionBLL.GetBalance(payment_terms);
            btnAddPayment.Enabled = pending && balance > 0;
            btnIssueReceipt.Enabled = pending && payment_terms.Count > 0 && balance == 0;
            btnPrintReceipt.Enabled = receipt != null;
        }

        // Los estados y los medios de pago estan en ingles: se muestran con su traduccion.
        private string GetStatusDescription(AcquisitionStatus status)
        {
            return LanguageService.GetTagText("AcquisitionStatus_" + status);
        }
        private string GetPaymentTypeDescription(string description)
        {
            return LanguageService.GetTagText("PaymentType_" + description.Replace(" ", ""), description);
        }
        private string GetPaymentDetail(AcquisitionPayment payment)
        {
            if (payment.PaymentType_Description == AcquisitionBLL.BankTransferPaymentType)
                return string.Format(LanguageService.GetTagText("PaymentDetailTransfer"), payment.Bank, payment.Cbu_Alias, payment.Operation_Number);
            if (payment.PaymentType_Description == AcquisitionBLL.CheckPaymentType)
                return string.Format(LanguageService.GetTagText("PaymentDetailCheck"), payment.Bank, payment.Check_Number,
                    payment.Payment_Date.Value.ToString("dd/MM/yyyy"));
            return "";
        }

        // CUF11 pasos 7 y 9: comprobante y pase a Pagada. Despues se imprime con lo que quedo guardado.
        private void PerformIssueReceipt()
        {
            Guid Acquisition_Id = acquisition.Id;
            SQLUpdateResult result = _AcquisitionBLL.IssuePurchaseReceipt(Acquisition_Id);
            if (result != null && result.sqlResult == SQLResultType.success)
            {
                PerformUpdateAcquisitionsView(Acquisition_Id);
                if (receipt == null)
                    return;
                MessageBox.Show(string.Format(LanguageService.GetTagText("PurchaseReceiptIssuedMessage"), receipt.Number, payable.Number));
                PerformGeneratePurchaseReceiptPDF();
            }
        }

        // Comprobante de compra (CUF11 paso 7): dos paginas iguales, una para el cliente y otra para archivar.
        // Se arma con la fila de PurchaseReceipts, asi una reimpresion sale igual a la que se entrego.
        void PerformGeneratePurchaseReceiptPDF()
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency®️ " + LanguageService.GetTagText("PurchaseReceiptNumber") + receipt.Number;

            XFont fontTitle = new XFont("Verdana", 32);
            XFont fontSubTitle = new XFont("Verdana", 20);
            XFont fontSection = new XFont("Verdana", 14);
            XFont fontBody = new XFont("Verdana", 10);

            foreach (string copy in new[] { "ContractOriginal", "ContractDuplicate" })
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);

                XRect rect = new XRect(25, 0, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Center;
                tf.DrawString("CarAgency®️", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                rect = new XRect(25, 65, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(LanguageService.GetTagText("PurchaseReceiptNumber") + receipt.Number, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString(LanguageService.GetTagText(copy), fontSection, XBrushes.Black, new XRect(0, 100, page.Width.Point - 25, 20), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;

                int rectplace = 125;
                tf.DrawString(LanguageService.GetTagText("ReceiptIssueDate") + receipt.Issue_Date.ToString("dd/MM/yyyy") + ". "
                    + LanguageService.GetTagText("AppraisalAcquisitionNumber") + acquisition.Number,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ReceiptIssuer"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(AcquisitionBLL.AgencyName + "®️. " + LanguageService.GetTagText("CUILCUIT") + " " + AcquisitionBLL.AgencyCuit,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ReceiptRecipient"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("lblNameSurname") + " " + receipt.Client_Name + ". " + LanguageService.GetTagText("CUILCUIT") + " " + receipt.Client_CUIL_CUIT,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractVehicle"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                // El detalle puede ocupar dos lineas.
                tf.DrawString(receipt.Detail, fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 70, 40), XStringFormats.TopLeft);

                // Pagos con los que se cancelo la compra.
                rectplace += 50;
                tf.DrawString(LanguageService.GetTagText("ReceiptPayments"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                foreach (AcquisitionPayment payment in payments)
                {
                    string detail = GetPaymentDetail(payment);
                    // Con el detalle del medio la linea puede ocupar dos renglones.
                    tf.DrawString(GetPaymentTypeDescription(payment.PaymentType_Description) + ": $ " + payment.Amount.ToString("N2")
                        + (detail == "" ? "" : " - " + detail), fontBody, XBrushes.Black, new XRect(88, rectplace, page.Width.Point - 133, 36), XStringFormats.TopLeft);
                    rectplace += detail == "" ? 18 : 36;
                }

                rectplace += 30;
                rect = new XRect(0, rectplace, page.Width.Point - 20, 20);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString(LanguageService.GetTagText("ReceiptNetAmount") + receipt.Net_Amount.ToString("N2"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                rectplace += 20;
                rect = new XRect(0, rectplace, page.Width.Point - 20, 20);
                tf.DrawString(LanguageService.GetTagText("ReceiptTaxAmount") + receipt.Tax_Amount.ToString("N2"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                rectplace += 25;
                rect = new XRect(0, rectplace, page.Width.Point - 20, 20);
                tf.DrawString("TOTAL $ " + receipt.Total_Amount.ToString("N2"), fontSection, XBrushes.Black, rect, XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;

                gfx.DrawLine(XPens.Black, page.Width.Point - 250, page.Height.Point - 110, page.Width.Point - 60, page.Height.Point - 110);
                tf.DrawString(LanguageService.GetTagText("ReceiptClientSignature"), fontBody, XBrushes.Black,
                    new XRect(page.Width.Point - 250, page.Height.Point - 105, 190, 20), XStringFormats.TopLeft);

                rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®️", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            }

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Purchase Receipt " + receipt.Number + " " + client.Surname + " " + client.Name + ".pdf";
            document.Close();
            document.Save(filename);

            Process.Start(filename);
        }
        #endregion

        public void clientView1_ClientFound(object sender, EventArgs e)
        {
            clientView1.Enabled = false;
            btnChangeClient.Enabled = true;
            client = clientView1.client;

            PerformUpdateAcquisitionsView(null);

            // 2.1: sin compras con la documentacion completa no hay nada que pagar. Si tiene comprobantes
            // emitidos se dejan en la grilla para reimprimirlos.
            if (payable_acquisitions == null || !payable_acquisitions.Any(a => a.Status == AcquisitionStatus.DocumentationComplete))
            {
                MessageBox.Show(LanguageService.GetTagText("NoPendingPaymentAcquisitions"), LanguageService.GetTagText("AttentionTitle"));
                if (payable_acquisitions == null)
                    PerformChangeClient();
            }
        }

        private void PerformChangeClient()
        {
            client = null;
            clientView1.Clear();
            clientView1.Enabled = true;
            btnChangeClient.Enabled = false;
            PerformUpdateAcquisitionsView(null);
        }

        private void btnChangeClient_Click(object sender, EventArgs e)
        {
            PerformChangeClient();
        }

        private void gridAcquisitions_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectAcquisition();
        }

        private void gridAcquisitions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridAcquisitions.Columns[e.ColumnIndex] == colStatus && e.Value is AcquisitionStatus)
            {
                e.Value = GetStatusDescription((AcquisitionStatus)e.Value);
                e.FormattingApplied = true;
            }
            else if ((gridAcquisitions.Columns[e.ColumnIndex] == colAgreed_Price || gridAcquisitions.Columns[e.ColumnIndex] == colBalance) && e.Value is double)
            {
                e.Value = ((double)e.Value).ToString("N2");
                e.FormattingApplied = true;
            }
        }

        // CUF11 pasos 3 a 6: se elige uno de los medios pactados y se cargan sus datos hasta cancelar el saldo.
        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                if (gridTerms.CurrentRow == null)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseSelectPaymentTerm"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                AcquisitionPaymentTerm term = (AcquisitionPaymentTerm)gridTerms.CurrentRow.Tag;
                decimal balance = _AcquisitionBLL.GetPaymentTermBalance(term);
                if (balance <= 0)
                {
                    MessageBox.Show(LanguageService.GetTagText("PaymentTermAlreadyPaid"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                AddAcquisitionPaymentForm frm = new AddAcquisitionPaymentForm(acquisition, term, balance);
                frm.ShowDialog();
                if (frm._payment != null)
                {
                    PerformUpdateAcquisitionsView(acquisition.Id);
                    // Paso 7: con el ultimo pago el saldo queda en cero y se emite el comprobante.
                    if (acquisition != null && receipt == null && _AcquisitionBLL.GetBalance(payment_terms) == 0)
                        PerformIssueReceipt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // Si la emision fallo despues del ultimo pago, la compra quedo con saldo cero y se reintenta desde aca.
        private void btnIssueReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                PerformIssueReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (receipt == null)
                    return;
                PerformGeneratePurchaseReceiptPDF();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
