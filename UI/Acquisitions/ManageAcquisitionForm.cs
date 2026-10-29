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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Acquisitions.Controls;
using Security.Session;

namespace UI.Vehicles
{
    public partial class ManageAcquisitionForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        AcquisitionBLL _AcquisitionBLL;
        ClientsBLL _ClientsBLL;
        List<PendingAcquisition> pending_acquisitions;
        PendingAcquisition pending;
        Acquisition acquisition;
        Appraisal appraisal;
        Client client;
        List<AcquisitionPaymentTerm> payment_terms = new List<AcquisitionPaymentTerm>();
        // El acuerdo en pantalla es el que esta guardado: recien ahi se puede registrar la firma.
        bool agreement_registered;
        MetroFramework.Controls.MetroTabPage stage_tab;

        public ManageAcquisitionForm()
        {
            InitializeComponent();
            // El Designer no guarda AutoGenerateColumns: la grilla usa las columnas definidas ahi.
            gridPending.AutoGenerateColumns = false;
            _AcquisitionBLL = new AcquisitionBLL();
            _ClientsBLL = new ClientsBLL();
            documentsView1.Stage = AcquisitionDocumentStage.Document;
            transferDocumentsView1.Stage = AcquisitionDocumentStage.Transfer;

            PerformUpdatePaymentTypesCombo();
            PerformUpdatePendingView(null);

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void ManageAcquisitionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("ManageAcquisitionForm");
            this.Refresh();
            lblPendingAcquisitions.Text = LanguageService.GetTagText(lblPendingAcquisitions.Tag.ToString());
            tbFilter.WaterMark = LanguageService.GetTagText(tbFilter.Tag.ToString());
            btnSearch.Text = LanguageService.GetTagText(btnSearch.Tag.ToString());
            colNumber.HeaderText = LanguageService.GetTagText(colNumber.Tag.ToString());
            colDni.HeaderText = LanguageService.GetTagText(colDni.Tag.ToString());
            colLicense_Plate.HeaderText = LanguageService.GetTagText(colLicense_Plate.Tag.ToString());
            colStatus.HeaderText = LanguageService.GetTagText(colStatus.Tag.ToString());
            lblAcquisitionSummary.Text = LanguageService.GetTagText(lblAcquisitionSummary.Tag.ToString());
            tabInspection.Text = LanguageService.GetTagText(tabInspection.Tag.ToString());
            tabFormalize.Text = LanguageService.GetTagText(tabFormalize.Tag.ToString());
            tabDocuments.Text = LanguageService.GetTagText(tabDocuments.Tag.ToString());
            tabTransfer.Text = LanguageService.GetTagText(tabTransfer.Tag.ToString());
            tabFinish.Text = LanguageService.GetTagText(tabFinish.Tag.ToString());
            lblAgreement.Text = LanguageService.GetTagText(lblAgreement.Tag.ToString());
            tbAgreed_Price.WaterMark = LanguageService.GetTagText(tbAgreed_Price.Tag.ToString());
            tbOwner_CUIL_CUIT.WaterMark = LanguageService.GetTagText(tbOwner_CUIL_CUIT.Tag.ToString());
            lblPaymentTerms.Text = LanguageService.GetTagText(lblPaymentTerms.Tag.ToString());
            tbTermAmount.WaterMark = LanguageService.GetTagText(tbTermAmount.Tag.ToString());
            btnAddTerm.Text = LanguageService.GetTagText(btnAddTerm.Tag.ToString());
            btnRemoveTerm.Text = LanguageService.GetTagText(btnRemoveTerm.Tag.ToString());
            colPaymentType.HeaderText = LanguageService.GetTagText(colPaymentType.Tag.ToString());
            colTermAmount.HeaderText = LanguageService.GetTagText(colTermAmount.Tag.ToString());
            btnRejectAppraisal.Text = LanguageService.GetTagText(btnRejectAppraisal.Tag.ToString());
            btnGenerateContract.Text = LanguageService.GetTagText(btnGenerateContract.Tag.ToString());
            btnContractNotSigned.Text = LanguageService.GetTagText(btnContractNotSigned.Tag.ToString());
            btnContractSigned.Text = LanguageService.GetTagText(btnContractSigned.Tag.ToString());
            lblTransfer.Text = LanguageService.GetTagText(lblTransfer.Tag.ToString());
            tbPaperwork_Precharge_Code.WaterMark = LanguageService.GetTagText(tbPaperwork_Precharge_Code.Tag.ToString());
            lblTransferAppointment.Text = LanguageService.GetTagText(lblTransferAppointment.Tag.ToString());
            btnScheduleTransfer.Text = LanguageService.GetTagText(btnScheduleTransfer.Tag.ToString());
            btnRegisterTransfer.Text = LanguageService.GetTagText(btnRegisterTransfer.Tag.ToString());
            lblFinishSummary.Text = LanguageService.GetTagText(lblFinishSummary.Tag.ToString());
            tbSale_Price.WaterMark = LanguageService.GetTagText(tbSale_Price.Tag.ToString());
            tbOpcionals.WaterMark = LanguageService.GetTagText(tbOpcionals.Tag.ToString());
            tbVehicleObservations.WaterMark = LanguageService.GetTagText(tbVehicleObservations.Tag.ToString());
            btnFinishAcquisition.Text = LanguageService.GetTagText(btnFinishAcquisition.Tag.ToString());
            inspectionView1.UpdateLanguage(language);
            documentsView1.UpdateLanguage(language);
            transferDocumentsView1.UpdateLanguage(language);
            PerformUpdatePaymentTypesCombo();
            PerformUpdateTermsView();
            gridPending.Refresh();
            MapAcquisitionToControls();
            MapTransferToControls();
            MapSummaryToControls();
        }
        void PerformUpdatePendingView(Guid? Acquisition_Id)
        {
            try
            {
                pending_acquisitions = _AcquisitionBLL.GetPendingAcquisitions(tbFilter.Text);
                // La compra que se estaba gestionando pudo cambiar de estado: se vuelve a cargar.
                pending = null;
                gridPending.DataSource = null;
                gridPending.DataSource = pending_acquisitions;
                if (Acquisition_Id != null)
                {
                    foreach (DataGridViewRow row in gridPending.Rows)
                    {
                        if (((PendingAcquisition)row.DataBoundItem).Id == Acquisition_Id.Value)
                        {
                            gridPending.CurrentCell = row.Cells[0];
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
        private void PerformUpdatePaymentTypesCombo()
        {
            try
            {
                // Bank Transfer, Check y Cash, con su traduccion.
                List<KeyValuePair<PaymentType, string>> payment_types = _AcquisitionBLL.GetAcquisitionPaymentTypes()
                    .Select(t => new KeyValuePair<PaymentType, string>(t, GetPaymentTypeDescription(t.Description))).ToList();
                comboPaymentType.DataSource = payment_types;
                comboPaymentType.DisplayMember = "Value";
                comboPaymentType.ValueMember = "Key";
                comboPaymentType.SelectedIndex = -1;
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
                PendingAcquisition selected = gridPending.CurrentRow == null ? null : (PendingAcquisition)gridPending.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearAcquisition();
                    return;
                }
                if (pending != null && pending.Id == selected.Id)
                    return;

                ClearAcquisition();
                pending = selected;
                acquisition = _AcquisitionBLL.GetAcquisition(pending.Id);
                appraisal = _AcquisitionBLL.GetLastAppraisal(pending.Id);
                client = _ClientsBLL.GetById(pending.Client_Id);
                inspectionView1.InitWithInspection(pending.Inspection_Id);
                PerformInitStage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // CUF10 paso 2: solo se habilita la etapa que corresponde al estado de la compra.
        private void PerformInitStage()
        {
            stage_tab = null;
            switch (acquisition.Status)
            {
                case AcquisitionStatus.Appraised:
                    // 3.1: con la tasacion vencida no se puede seguir.
                    if (!_AcquisitionBLL.IsAppraisalExpired(appraisal))
                    {
                        stage_tab = tabFormalize;
                        MapAgreementToControls();
                    }
                    break;
                case AcquisitionStatus.PurchaseFormalized:
                    stage_tab = tabDocuments;
                    documentsView1.InitWithAcquisition(acquisition.Id, true);
                    break;
                case AcquisitionStatus.Paid:
                    stage_tab = tabTransfer;
                    transferDocumentsView1.InitWithAcquisition(acquisition.Id, true);
                    tbPaperwork_Precharge_Code.Text = acquisition.Precharge_Code_08;
                    if (acquisition.Transfer_Appointment_Date == null)
                        dtTransfer_Appointment_Date.Checked = false;
                    else
                    {
                        dtTransfer_Appointment_Date.Checked = true;
                        dtTransfer_Appointment_Date.Value = acquisition.Transfer_Appointment_Date.Value;
                    }
                    break;
                case AcquisitionStatus.Transferred:
                    stage_tab = tabFinish;
                    break;
            }
            MapAcquisitionToControls();
            MapTransferToControls();
            MapSummaryToControls();
            tabStages.SelectedTab = stage_tab == null ? tabInspection : stage_tab;
        }
        private void ClearAcquisition()
        {
            pending = null;
            acquisition = null;
            appraisal = null;
            client = null;
            stage_tab = null;
            payment_terms = new List<AcquisitionPaymentTerm>();
            inspectionView1.Clear();
            documentsView1.Clear();
            transferDocumentsView1.Clear();
            tbAgreed_Price.Text = "";
            tbOwner_CUIL_CUIT.Text = "";
            tbTermAmount.Text = "";
            comboPaymentType.SelectedIndex = -1;
            tbPaperwork_Precharge_Code.Text = "";
            dtTransfer_Appointment_Date.Checked = false;
            tbSale_Price.Text = "";
            tbOpcionals.Text = "";
            tbVehicleObservations.Text = "";
            agreement_registered = false;
            PerformUpdateTermsView();
            UpdateAgreementButtons();
            MapAcquisitionToControls();
            MapTransferToControls();
            MapSummaryToControls();
            tabStages.SelectedTab = tabInspection;
        }
        private void MapAcquisitionToControls()
        {
            if (acquisition == null || pending == null || client == null)
            {
                lblSummaryClient.Text = "";
                lblSummaryVehicle.Text = "";
                lblSummaryAppraisal.Text = "";
                lblSummaryStatus.Text = "";
                lblStageMessage.Text = "";
                return;
            }
            lblSummaryClient.Text = LanguageService.GetTagText("lblNameSurname") + " " + client.Name + " " + client.Surname
                + " - " + LanguageService.GetTagText("lblDNI") + " " + client.Dni;
            lblSummaryVehicle.Text = pending.Vehicle_Description + " - " + pending.License_Plate;
            lblSummaryAppraisal.Text = appraisal == null ? "" : string.Format(LanguageService.GetTagText("SummaryAppraisal"),
                appraisal.Amount.ToString("N2"), appraisal.Expiration_Date.ToString("dd/MM/yyyy"));
            lblSummaryStatus.Text = LanguageService.GetTagText("SummaryStatus") + GetStatusDescription(acquisition.Status);
            // 3.1 y 2.1: la compra no tiene etapa para gestionar.
            if (acquisition.Status == AcquisitionStatus.Appraised && _AcquisitionBLL.IsAppraisalExpired(appraisal))
                lblStageMessage.Text = LanguageService.GetTagText("AppraisalExpired");
            else if (acquisition.Status == AcquisitionStatus.DocumentationComplete)
                lblStageMessage.Text = LanguageService.GetTagText("AcquisitionWaitingPayment");
            else
                lblStageMessage.Text = "";
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

        // Etapa A: si el acuerdo ya se registro se muestra el guardado; si no, el precio arranca en el valor tasado (paso 4).
        private void MapAgreementToControls()
        {
            bool registered = _AcquisitionBLL.HasAgreement(acquisition);
            if (registered)
            {
                tbAgreed_Price.Text = acquisition.Agreed_Price.Value.ToString("0.00");
                tbOwner_CUIL_CUIT.Text = acquisition.Owner_CUIL_CUIT;
                List<AcquisitionPaymentTerm> terms = _AcquisitionBLL.GetPaymentTerms(acquisition.Id);
                payment_terms = terms == null ? new List<AcquisitionPaymentTerm>() : terms;
            }
            else
            {
                tbAgreed_Price.Text = appraisal.Amount.ToString("0.00");
                tbOwner_CUIL_CUIT.Text = "";
                payment_terms = new List<AcquisitionPaymentTerm>();
            }
            PerformUpdateTermsView();
            agreement_registered = registered;
            UpdateAgreementButtons();
        }
        private void PerformUpdateTermsView()
        {
            gridTerms.Rows.Clear();
            foreach (AcquisitionPaymentTerm term in payment_terms)
            {
                int index = gridTerms.Rows.Add(GetPaymentTypeDescription(term.PaymentType_Description), term.Amount.ToString("N2"));
                gridTerms.Rows[index].Tag = term;
            }
            UpdateTermsDifference();
        }
        // CUF10 paso 6: la suma y la diferencia con el precio se ven mientras se carga la forma de pago.
        private void UpdateTermsDifference()
        {
            double price;
            if (!double.TryParse(tbAgreed_Price.Text, out price))
                price = 0;
            decimal difference = _AcquisitionBLL.GetPaymentTermsDifference(price, payment_terms);
            lblTermsDifference.Text = string.Format(LanguageService.GetTagText("PaymentTermsTotal"), payment_terms.Sum(t => t.Amount).ToString("N2"),
                difference.ToString("N2"));
        }
        private void PerformAgreementChanged()
        {
            agreement_registered = false;
            UpdateTermsDifference();
            UpdateAgreementButtons();
        }
        private void UpdateAgreementButtons()
        {
            btnContractSigned.Enabled = agreement_registered;
            btnContractNotSigned.Enabled = agreement_registered;
        }
        // CUF10 6.1: se indica la diferencia y se vuelve a la forma de pago.
        private bool ValidateAgreementControls()
        {
            double price;
            if (!double.TryParse(tbAgreed_Price.Text, out price) || price <= 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidAgreedPrice"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbOwner_CUIL_CUIT.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteOwnerCuilCuit"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (payment_terms.Count == 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseAddPaymentTerm"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            decimal difference = _AcquisitionBLL.GetPaymentTermsDifference(price, payment_terms);
            if (difference != 0)
            {
                MessageBox.Show(string.Format(LanguageService.GetTagText("PaymentTermsDifference"), difference.ToString("N2")), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        private Acquisition MapControlsToAgreement()
        {
            Acquisition agreement = new Acquisition();
            agreement.Id = acquisition.Id;
            agreement.Agreed_Price = double.Parse(tbAgreed_Price.Text);
            agreement.Owner_CUIL_CUIT = tbOwner_CUIL_CUIT.Text.Trim();
            return agreement;
        }

        // Etapa C: titular y vehiculo de la transferencia.
        private void MapTransferToControls()
        {
            if (acquisition == null || pending == null || client == null)
            {
                lblTransferOwner.Text = "";
                lblTransferVehicle.Text = "";
                return;
            }
            lblTransferOwner.Text = LanguageService.GetTagText("TransferOwner") + client.Name + " " + client.Surname
                + " - " + LanguageService.GetTagText("CUILCUIT") + " " + acquisition.Owner_CUIL_CUIT;
            lblTransferVehicle.Text = LanguageService.GetTagText("AppraisalVehicle") + pending.Vehicle_Description
                + " - " + LanguageService.GetTagText("LicensePlate") + " " + pending.License_Plate;
        }

        // Etapa D: resumen completo de la operacion (paso 20).
        private void MapSummaryToControls()
        {
            if (acquisition == null || client == null || acquisition.Status != AcquisitionStatus.Transferred)
            {
                tbSummary.Text = "";
                return;
            }
            tbSummary.Text = string.Join(Environment.NewLine, GetSummaryLines(_AcquisitionBLL.GetSummary(acquisition.Id)));
        }
        // Las mismas lineas se muestran en la etapa D y se imprimen en la constancia.
        private List<string> GetSummaryLines(AcquisitionSummary summary)
        {
            List<string> lines = new List<string>();
            lines.Add(LanguageService.GetTagText("AppraisalAcquisitionNumber") + summary.Number + ". " + LanguageService.GetTagText("SummaryStatus")
                + GetStatusDescription(acquisition.Status));
            lines.Add(LanguageService.GetTagText("SummaryClient") + client.Name + " " + client.Surname + ". " + LanguageService.GetTagText("lblDNI") + " " + client.Dni);
            lines.Add(LanguageService.GetTagText("AppraisalVehicle") + summary.Vehicle_Description + ". " + LanguageService.GetTagText("LicensePlate") + " " + summary.License_Plate);
            lines.Add(LanguageService.GetTagText("Chassis") + summary.Chassis_Number + " - " + LanguageService.GetTagText("Engine") + summary.Engine_Number);
            lines.Add(LanguageService.GetTagText("InspectionPerformedDate") + summary.Performed_Date.Value.ToString("dd/MM/yyyy")
                + ". " + LanguageService.GetTagText("VerifiedKilometers") + summary.Verified_Kilometers
                + ". " + LanguageService.GetTagText("lblResult") + " " + inspectionView1.GetResultDescription());
            lines.Add(string.Format(LanguageService.GetTagText("SummaryAcceptedAppraisal"), summary.Appraisal_Amount.Value.ToString("N2"),
                summary.Appraisal_Date.Value.ToString("dd/MM/yyyy")));
            lines.Add(string.Format(LanguageService.GetTagText("SummaryAgreement"), summary.Agreed_Price.Value.ToString("N2"), summary.Owner_CUIL_CUIT,
                summary.Contract_Signed_Date.Value.ToString("dd/MM/yyyy")));
            lines.Add(LanguageService.GetTagText("SummaryPaymentTerms"));
            List<AcquisitionPaymentTerm> terms = _AcquisitionBLL.GetPaymentTerms(summary.Id);
            if (terms != null)
                foreach (AcquisitionPaymentTerm term in terms)
                    lines.Add("      " + string.Format(LanguageService.GetTagText("SummaryPaymentTerm"), GetPaymentTypeDescription(term.PaymentType_Description),
                        term.Amount.ToString("N2"), term.Paid_Amount.ToString("N2")));
            lines.Add(string.Format(LanguageService.GetTagText("SummaryPaid"), summary.Paid_Amount.ToString("N2")));
            lines.Add(string.Format(LanguageService.GetTagText("SummaryTransfer"), summary.Precharge_Code_08,
                summary.Transfer_Appointment_Date.Value.ToString("dd/MM/yyyy")));
            lines.Add(LanguageService.GetTagText("SummaryDocuments"));
            foreach (AcquisitionDocument document in _AcquisitionBLL.GetDocuments(summary.Id))
                lines.Add("      " + string.Format(LanguageService.GetTagText("SummaryDocument"), AcquisitionDocumentsView.GetDocumentTypeDescription(document.Description),
                    document.Issue_Date.ToString("dd/MM/yyyy")));
            return lines;
        }

        // Contrato de compraventa (CUF10 paso 7): dos paginas iguales, original y duplicado, para que firmen las dos partes.
        // Se arma con el acuerdo guardado cada vez que se genera.
        void PerformGenerateContractPDF()
        {
            InspectionDetail detail = inspectionView1.inspection_detail;
            Inspection inspection = inspectionView1.inspection;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency®️ " + LanguageService.GetTagText("PurchaseContractFor") + client.Name + " " + client.Surname;

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
                tf.DrawString(LanguageService.GetTagText("PurchaseContractFor") + client.Name + " " + client.Surname, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString(LanguageService.GetTagText(copy), fontSection, XBrushes.Black, new XRect(0, 100, page.Width.Point - 25, 20), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;

                int rectplace = 125;
                tf.DrawString(LanguageService.GetTagText("AppraisalAcquisitionNumber") + acquisition.Number + ". " + LanguageService.GetTagText("ContractDate") + DateTime.Now.ToString("dd/MM/yyyy"),
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractSeller"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("lblNameSurname") + " " + client.Name + " " + client.Surname + ". " + LanguageService.GetTagText("lblDNI") + " " + client.Dni
                    + ". " + LanguageService.GetTagText("CUILCUIT") + " " + acquisition.Owner_CUIL_CUIT,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractBuyer"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString("CarAgency®️", fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractVehicle"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("AppraisalVehicle") + detail.Vehicle_Description + ". " + LanguageService.GetTagText("LicensePlate") + " " + detail.License_Plate,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("Chassis") + detail.Chassis_Number + " - " + LanguageService.GetTagText("Engine") + detail.Engine_Number
                    + " - " + LanguageService.GetTagText("VerifiedKilometers") + inspection.Verified_Kilometers,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                // Precio y forma de pago pactada.
                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractPriceSection"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("ContractAgreedPrice") + acquisition.Agreed_Price.Value.ToString("N2"), fontBody, XBrushes.Black,
                    new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                foreach (AcquisitionPaymentTerm term in payment_terms)
                {
                    tf.DrawString("                  " + GetPaymentTypeDescription(term.PaymentType_Description) + ": $ " + term.Amount.ToString("N2"),
                        fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                    rectplace += 18;
                }

                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("ContractConditions"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                // Las condiciones ocupan varias lineas: se cortan antes de las firmas.
                tf.DrawString(LanguageService.GetTagText("ContractClauses"), fontBody, XBrushes.Black,
                    new XRect(25, rectplace, page.Width.Point - 70, page.Height.Point - rectplace - 140), XStringFormats.TopLeft);

                gfx.DrawLine(XPens.Black, 60, page.Height.Point - 110, 250, page.Height.Point - 110);
                gfx.DrawLine(XPens.Black, page.Width.Point - 250, page.Height.Point - 110, page.Width.Point - 60, page.Height.Point - 110);
                tf.DrawString(LanguageService.GetTagText("ContractSellerSignature"), fontBody, XBrushes.Black,
                    new XRect(60, page.Height.Point - 105, 190, 20), XStringFormats.TopLeft);
                tf.DrawString(LanguageService.GetTagText("ContractBuyerSignature"), fontBody, XBrushes.Black,
                    new XRect(page.Width.Point - 250, page.Height.Point - 105, 190, 20), XStringFormats.TopLeft);

                rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®️", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            }

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Purchase Contract " + acquisition.Number + " " + client.Surname + " " + client.Name + ".pdf";
            document.Close();
            document.Save(filename);

            Process.Start(filename);
        }

        // Constancia de operacion finalizada (CUF10 paso 25), con el resumen de la etapa D y la fecha de cierre.
        void PerformGenerateCertificatePDF()
        {
            AcquisitionSummary summary = _AcquisitionBLL.GetSummary(acquisition.Id);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency®️ " + LanguageService.GetTagText("PurchaseCertificateFor") + client.Name + " " + client.Surname;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            XFont fontTitle = new XFont("Verdana", 32);
            XFont fontSubTitle = new XFont("Verdana", 20);
            XFont fontSection = new XFont("Verdana", 14);
            XFont fontBody = new XFont("Verdana", 10);

            XRect rect = new XRect(25, 0, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString("CarAgency®️", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(25, 65, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(LanguageService.GetTagText("PurchaseCertificateFor") + client.Name + " " + client.Surname, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            int rectplace = 120;
            tf.DrawString(LanguageService.GetTagText("CertificateSummary"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 30;
            foreach (string line in GetSummaryLines(summary))
            {
                tf.DrawString(line, fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 20;
            }

            rectplace += 25;
            tf.DrawString(string.Format(LanguageService.GetTagText("CertificateClosing"), summary.Closing_Date.Value.ToString("dd/MM/yyyy HH:mm")), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 70, 40), XStringFormats.TopLeft);

            rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®️", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Purchase Certificate " + acquisition.Number + " " + client.Surname + " " + client.Name + ".pdf";
            document.Close();
            document.Save(filename);

            Process.Start(filename);
        }

        // CUF10 paso 25: copias del formulario 08 firmado y de la baja municipal para entregar al cliente.
        void PerformExportTransferCopies()
        {
            foreach (AcquisitionDocument document in _AcquisitionBLL.GetDocuments(acquisition.Id).Where(d => d.Stage == AcquisitionDocumentStage.Transfer))
            {
                AcquisitionDocumentFile file = _AcquisitionBLL.GetDocumentFile(document.Id);
                string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency " + document.Description + " " + acquisition.Number
                    + " " + client.Surname + " " + client.Name + Path.GetExtension(file.FileName);
                File.WriteAllBytes(filename, file.FileContent);
                Process.Start(filename);
            }
        }
        #endregion

        private void gridPending_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectAcquisition();
        }

        private void gridPending_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridPending.Columns[e.ColumnIndex] == colStatus && e.Value is AcquisitionStatus)
            {
                e.Value = GetStatusDescription((AcquisitionStatus)e.Value);
                e.FormattingApplied = true;
            }
        }

        // Solo se entra a la etapa del estado actual; la inspeccion se puede consultar siempre.
        private void tabStages_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != tabInspection && e.TabPage != stage_tab)
                e.Cancel = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformUpdatePendingView(null);
            if (pending_acquisitions == null && tbFilter.Text.Trim() != "")
                MessageBox.Show(LanguageService.GetTagText("NoPendingAcquisitionsFound"), LanguageService.GetTagText("AttentionTitle"));
        }

        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
        }

        private void tbAgreement_TextChanged(object sender, EventArgs e)
        {
            PerformAgreementChanged();
        }

        private void btnAddTerm_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                if (comboPaymentType.SelectedIndex == -1)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseSelectPaymentType"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                double amount;
                if (!double.TryParse(tbTermAmount.Text, out amount) || amount <= 0)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidPaymentAmount"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                PaymentType type = ((KeyValuePair<PaymentType, string>)comboPaymentType.SelectedItem).Key;
                if (payment_terms.Any(t => t.PaymentType_Id == type.Id))
                {
                    MessageBox.Show(LanguageService.GetTagText("PaymentTypeAlreadyAdded"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                AcquisitionPaymentTerm term = new AcquisitionPaymentTerm();
                term.PaymentType_Id = type.Id;
                term.PaymentType_Description = type.Description;
                term.Amount = amount;
                payment_terms.Add(term);
                tbTermAmount.Text = "";
                comboPaymentType.SelectedIndex = -1;
                PerformUpdateTermsView();
                PerformAgreementChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnRemoveTerm_Click(object sender, EventArgs e)
        {
            if (gridTerms.CurrentRow == null)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectPaymentTerm"), LanguageService.GetTagText("AttentionTitle"));
                return;
            }
            payment_terms.Remove((AcquisitionPaymentTerm)gridTerms.CurrentRow.Tag);
            PerformUpdateTermsView();
            PerformAgreementChanged();
        }

        private void btnGenerateContract_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null || !ValidateAgreementControls())
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureGenerateContract"), double.Parse(tbAgreed_Price.Text).ToString("N2"), pending.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterAgreement(MapControlsToAgreement(), payment_terms);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    acquisition = _AcquisitionBLL.GetAcquisition(acquisition.Id);
                    MapAgreementToControls();
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AgreementRegistered"), acquisition.Number));
                    PerformGenerateContractPDF();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnContractSigned_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureContractSigned"), pending.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterContractSigned(acquisition.Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("PurchaseFormalized"), acquisition.Number));
                    PerformUpdatePendingView(acquisition.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnContractNotSigned_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureContractNotSigned"), pending.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterContractNotSigned(acquisition.Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("ContractNotSignedRegistered"), acquisition.Number));
                    PerformUpdatePendingView(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // CUF10 3.3. Si el cliente lo quiere pensar (3.2) no se registra nada: la compra sigue Tasada.
        private void btnRejectAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRejectAppraisal"), pending.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RejectAppraisal(acquisition.Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AppraisalRejectedRegistered"), acquisition.Number));
                    PerformUpdatePendingView(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // CUF10 paso 15: con el ultimo documento cargado la compra pasa a Documentacion completa.
        private void documentsView1_DocumentsChanged(object sender, EventArgs e)
        {
            try
            {
                Acquisition current = _AcquisitionBLL.GetAcquisition(acquisition.Id);
                if (current.Status != acquisition.Status)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("DocumentationCompleted"), current.Number));
                    PerformUpdatePendingView(current.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnScheduleTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                if (tbPaperwork_Precharge_Code.Text.Trim() == "")
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWrite08"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                if (!dtTransfer_Appointment_Date.Checked)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseSelectTransferAppointment"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                Acquisition transfer = new Acquisition();
                transfer.Id = acquisition.Id;
                transfer.Precharge_Code_08 = tbPaperwork_Precharge_Code.Text.Trim();
                transfer.Transfer_Appointment_Date = dtTransfer_Appointment_Date.Value;
                SQLUpdateResult result = _AcquisitionBLL.ScheduleTransfer(transfer);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    acquisition = _AcquisitionBLL.GetAcquisition(acquisition.Id);
                    MessageBox.Show(string.Format(LanguageService.GetTagText("TransferScheduled"), acquisition.Transfer_Appointment_Date.Value.ToString("dd/MM/yyyy")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnRegisterTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRegisterTransfer"), pending.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterTransfer(acquisition.Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AcquisitionTransferredMessage"), acquisition.Number));
                    PerformUpdatePendingView(acquisition.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnFinishAcquisition_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                double price;
                if (!double.TryParse(tbSale_Price.Text, out price) || price <= 0)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidSalePrice"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                // CUF10 21.1: vender por debajo de lo que se le pago al cliente se advierte y se confirma.
                if (price < acquisition.Agreed_Price.Value && MessageBox.Show(string.Format(LanguageService.GetTagText("SalePriceBelowPurchasePrice"), price.ToString("N2"), acquisition.Agreed_Price.Value.ToString("N2")), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureFinishAcquisition"), pending.License_Plate, price.ToString("N2")), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                Vehicle sale = new Vehicle();
                sale.Price = price;
                sale.Opcionals = tbOpcionals.Text;
                sale.Observations = tbVehicleObservations.Text;
                SQLUpdateResult result = _AcquisitionBLL.FinishAcquisition(acquisition.Id, sale);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    acquisition = _AcquisitionBLL.GetAcquisition(acquisition.Id);
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AcquisitionFinishedMessage"), acquisition.Number));
                    PerformGenerateCertificatePDF();
                    PerformExportTransferCopies();
                    PerformUpdatePendingView(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
