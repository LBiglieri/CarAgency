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
using UI.Acquisitions.Controls;
using Security.Session;

namespace UI.Vehicles
{
    public partial class AppraiseVehicleForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        AcquisitionBLL _AcquisitionBLL;
        ClientsBLL _ClientsBLL;
        List<PendingAppraisal> pending_appraisals;
        PendingAppraisal acquisition;
        Appraisal previous_appraisal;

        public AppraiseVehicleForm()
        {
            InitializeComponent();
            // El Designer no guarda AutoGenerateColumns: la grilla usa las columnas definidas ahi.
            gridPending.AutoGenerateColumns = false;
            _AcquisitionBLL = new AcquisitionBLL();
            _ClientsBLL = new ClientsBLL();

            PerformUpdatePendingView();

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void AppraiseVehicleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("AppraiseVehicleForm");
            this.Refresh();
            lblPendingAppraisals.Text = LanguageService.GetTagText(lblPendingAppraisals.Tag.ToString());
            tbFilter.WaterMark = LanguageService.GetTagText(tbFilter.Tag.ToString());
            btnSearch.Text = LanguageService.GetTagText(btnSearch.Tag.ToString());
            colNumber.HeaderText = LanguageService.GetTagText(colNumber.Tag.ToString());
            colDni.HeaderText = LanguageService.GetTagText(colDni.Tag.ToString());
            colLicense_Plate.HeaderText = LanguageService.GetTagText(colLicense_Plate.Tag.ToString());
            colVehicle_Description.HeaderText = LanguageService.GetTagText(colVehicle_Description.Tag.ToString());
            colPrevious_Appraisals.HeaderText = LanguageService.GetTagText(colPrevious_Appraisals.Tag.ToString());
            lblAppraisal.Text = LanguageService.GetTagText(lblAppraisal.Tag.ToString());
            btnViewPreviousAppraisal.Text = LanguageService.GetTagText(btnViewPreviousAppraisal.Tag.ToString());
            tbAppraisalAmount.WaterMark = LanguageService.GetTagText(tbAppraisalAmount.Tag.ToString());
            tbJustification.WaterMark = LanguageService.GetTagText(tbJustification.Tag.ToString());
            btnGenerateAppraisal.Text = LanguageService.GetTagText(btnGenerateAppraisal.Tag.ToString());
            inspectionView1.UpdateLanguage(language);
            MapPreviousAppraisalToControls();
        }
        void PerformUpdatePendingView()
        {
            try
            {
                pending_appraisals = _AcquisitionBLL.GetPendingAppraisals(tbFilter.Text);
                gridPending.DataSource = null;
                gridPending.DataSource = pending_appraisals;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // CUF09 2.1: la tasacion vencida queda a la vista como referencia.
        private void MapPreviousAppraisalToControls()
        {
            btnViewPreviousAppraisal.Visible = previous_appraisal != null;
            if (previous_appraisal == null)
                lblPreviousAppraisal.Text = "";
            else
                lblPreviousAppraisal.Text = string.Format(LanguageService.GetTagText("PreviousAppraisalExpired"),
                    previous_appraisal.Amount.ToString("N2"), previous_appraisal.Expiration_Date.ToString("dd/MM/yyyy"));
        }
        private void PerformSelectAcquisition()
        {
            try
            {
                PendingAppraisal selected = gridPending.CurrentRow == null ? null : (PendingAppraisal)gridPending.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearAcquisition();
                    return;
                }
                if (acquisition != null && acquisition.Id == selected.Id)
                    return;

                ClearAcquisition();
                acquisition = selected;
                inspectionView1.InitWithInspection(acquisition.Inspection_Id);
                if (acquisition.Previous_Appraisals > 0)
                    previous_appraisal = _AcquisitionBLL.GetLastAppraisal(acquisition.Id);
                MapPreviousAppraisalToControls();
                AppraisalPanel.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void ClearAcquisition()
        {
            acquisition = null;
            previous_appraisal = null;
            inspectionView1.Clear();
            AppraisalPanel.Enabled = false;
            tbAppraisalAmount.Text = "";
            tbJustification.Text = "";
            MapPreviousAppraisalToControls();
        }
        // CUF09 5.1: se indica el dato faltante y se vuelve a la carga.
        private bool ValidateControls()
        {
            double amount;
            if (!double.TryParse(tbAppraisalAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidAppraisalAmount"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbJustification.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteAppraisalJustification"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        private Appraisal MapControlsToAppraisal()
        {
            Appraisal appraisal = new Appraisal();
            appraisal.Acquisition_Id = acquisition.Id;
            appraisal.Inspection_Id = acquisition.Inspection_Id;
            appraisal.Amount = double.Parse(tbAppraisalAmount.Text);
            appraisal.Justification = tbJustification.Text.Trim();
            return appraisal;
        }
        // La tasacion no se guarda en pdf: se arma con la fila de Appraisals cada vez que se pide.
        void PerformGenerateAppraisalPDF(Appraisal appraisal)
        {
            Client client = _ClientsBLL.GetById(acquisition.Client_Id);
            InspectionDetail detail = inspectionView1.inspection_detail;
            Inspection inspection = inspectionView1.inspection;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency®️ " + LanguageService.GetTagText("VehicleAppraisalFor") + client.Name + " " + client.Surname;
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
            tf.DrawString(LanguageService.GetTagText("VehicleAppraisalFor") + client.Name + " " + client.Surname, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            int rectplace = 120;
            tf.DrawString(LanguageService.GetTagText("AppraisalAcquisitionNumber") + detail.Number + ". " + LanguageService.GetTagText("lblDNI") + " " + client.Dni,
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalVehicle") + detail.Vehicle_Description + ". " + LanguageService.GetTagText("LicensePlate") + " " + detail.License_Plate,
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("Chassis") + detail.Chassis_Number + " - " + LanguageService.GetTagText("Engine") + detail.Engine_Number,
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

            // Resumen de la inspeccion: fecha, kilometraje, resultado y calificacion de cada item.
            rectplace += 40;
            tf.DrawString(LanguageService.GetTagText("AppraisalInspectionSummary"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("InspectionPerformedDate") + inspection.Performed_Date.Value.ToString("dd/MM/yyyy")
                + ". " + LanguageService.GetTagText("VerifiedKilometers") + inspection.Verified_Kilometers
                + ". " + LanguageService.GetTagText("lblResult") + " " + inspectionView1.GetResultDescription(),
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            foreach (InspectionScoreDetail score in inspectionView1.inspection_scores)
            {
                tf.DrawString("                  " + inspectionView1.GetItemDescription(score) + ": " + inspectionView1.GetRatingDescription(score.Score),
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 18;
            }

            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalSection"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalAmount") + appraisal.Amount.ToString("N2"), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalIssuedOn") + appraisal.Creation_Date.ToString("dd/MM/yyyy") + ". "
                + LanguageService.GetTagText("AppraisalValidUntil") + appraisal.Expiration_Date.ToString("dd/MM/yyyy"), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalAppraiser") + appraisal.Appraiser, fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 30;
            tf.DrawString(LanguageService.GetTagText("AppraisalJustification"), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 20;
            // La justificacion puede ocupar varias lineas: se corta antes del pie.
            tf.DrawString(appraisal.Justification, fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 70, page.Height.Point - rectplace - 70), XStringFormats.TopLeft);

            rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®️", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Appraisal " + detail.Number + " " + client.Surname + " " + client.Name + ".pdf";
            document.Close();
            document.Save(filename);

            Process.Start(filename);
        }
        #endregion

        private void gridPending_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectAcquisition();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformUpdatePendingView();
            if (pending_appraisals == null && tbFilter.Text.Trim() != "")
                MessageBox.Show(LanguageService.GetTagText("NoPendingAppraisalsFound"), LanguageService.GetTagText("AttentionTitle"));
        }

        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
        }

        private void btnViewPreviousAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition != null && previous_appraisal != null)
                    PerformGenerateAppraisalPDF(previous_appraisal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnGenerateAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null || !ValidateControls())
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureGenerateAppraisal"), double.Parse(tbAppraisalAmount.Text).ToString("N2"), acquisition.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.AddAppraisal(MapControlsToAppraisal());
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    Appraisal appraisal = _AcquisitionBLL.GetLastAppraisal(acquisition.Id);
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AppraisalGenerated"), acquisition.Number, appraisal.Expiration_Date.ToString("dd/MM/yyyy")));
                    PerformGenerateAppraisalPDF(appraisal);
                    PerformUpdatePendingView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
