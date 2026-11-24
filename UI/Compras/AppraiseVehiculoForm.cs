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
using UI.Compras.Controls;
using Security.Session;
using UI.Ayuda;

namespace UI.Vehiculos
{
    public partial class AppraiseVehiculoForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        TasacionBLL _AppraisalBLL;
        PersonaBLL _PeopleBLL;
        List<PendingTasacion> pending_appraisals;
        PendingTasacion acquisition;
        Tasacion previous_appraisal;

        public AppraiseVehiculoForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            // El Designer no guarda AutoGenerateColumns: la grilla usa las columnas definidas ahi.
            gridPending.AutoGenerateColumns = false;
            _AppraisalBLL = new TasacionBLL();
            _PeopleBLL = new PersonaBLL();

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
            this.Text = LanguageService.GetTagText("AppraiseVehiculoForm");
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
                pending_appraisals = _AppraisalBLL.GetPendingAppraisals(tbFilter.Text);
                gridPending.DataSource = null;
                gridPending.DataSource = pending_appraisals;
                PerformSelectAcquisition();
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
                    previous_appraisal.Valor.ToString("N2"), previous_appraisal.Vigencia.ToString("dd/MM/yyyy"));
        }
        private void PerformSelectAcquisition()
        {
            try
            {
                PendingTasacion selected = gridPending.CurrentRow == null ? null : (PendingTasacion)gridPending.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearAcquisition();
                    return;
                }
                if (acquisition != null && acquisition.Compra_Id == selected.Compra_Id)
                    return;

                ClearAcquisition();
                acquisition = selected;
                inspectionView1.InitWithInspection(acquisition.Inspeccion_Id);
                if (acquisition.TasacionesPrevias > 0)
                    previous_appraisal = _AppraisalBLL.GetLastAppraisal(acquisition.Compra_Id);
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
        private Tasacion MapControlsToAppraisal()
        {
            Tasacion appraisal = new Tasacion();
            appraisal.Compra_Id = acquisition.Compra_Id;
            appraisal.Inspeccion_Id = acquisition.Inspeccion_Id;
            appraisal.Valor = double.Parse(tbAppraisalAmount.Text);
            appraisal.Justificacion = tbJustification.Text.Trim();
            return appraisal;
        }
        // La tasacion no se guarda en pdf: se arma con la fila de Appraisals cada vez que se pide.
        void PerformGenerateAppraisalPDF(Tasacion appraisal)
        {
            Persona person = _PeopleBLL.GetById(acquisition.Persona_Id);
            InspeccionDetail detail = inspectionView1.inspection_detail;
            Inspeccion inspection = inspectionView1.inspection;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency® " + LanguageService.GetTagText("VehicleAppraisalFor") + person.Nombre + " " + person.Apellido;
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
            tf.DrawString("CarAgency®", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(25, 65, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(LanguageService.GetTagText("VehicleAppraisalFor") + person.Nombre + " " + person.Apellido, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            int rectplace = 120;
            tf.DrawString(LanguageService.GetTagText("AppraisalAcquisitionNumber") + detail.Numero + ". " + LanguageService.GetTagText("lblDNI") + " " + person.DNI,
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalVehicle") + detail.Vehiculo_Descripcion + ". " + LanguageService.GetTagText("LicensePlate") + " " + detail.Patente,
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("Chassis") + detail.NumeroChasis + " - " + LanguageService.GetTagText("Engine") + detail.NumeroMotor,
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

            // Resumen de la inspeccion: fecha, kilometraje, resultado y calificacion de cada item.
            rectplace += 40;
            tf.DrawString(LanguageService.GetTagText("AppraisalInspectionSummary"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("InspectionPerformedDate") + inspection.FechaRealizada.Value.ToString("dd/MM/yyyy")
                + ". " + LanguageService.GetTagText("VerifiedKilometers") + inspection.KilometrajeVerificado
                + ". " + LanguageService.GetTagText("lblResult") + " " + inspectionView1.GetResultDescription(),
                fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            foreach (PuntajeInspeccionDetail score in inspectionView1.inspection_scores)
            {
                tf.DrawString("                  " + inspectionView1.GetItemDescription(score) + ": " + inspectionView1.GetRatingDescription(score.Puntaje),
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 18;
            }

            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalSection"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalAmount") + appraisal.Valor.ToString("N2"), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalIssuedOn") + appraisal.Fecha.ToString("dd/MM/yyyy") + ". "
                + LanguageService.GetTagText("AppraisalValidUntil") + appraisal.Vigencia.ToString("dd/MM/yyyy"), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 25;
            tf.DrawString(LanguageService.GetTagText("AppraisalAppraiser") + appraisal.Tasador, fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 30;
            tf.DrawString(LanguageService.GetTagText("AppraisalJustification"), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 20;
            // La justificacion puede ocupar varias lineas: se corta antes del pie.
            tf.DrawString(appraisal.Justificacion, fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 70, page.Height.Point - rectplace - 70), XStringFormats.TopLeft);

            rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Appraisal " + detail.Numero + " " + person.Apellido + " " + person.Nombre + ".pdf";
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

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureGenerateAppraisal"), double.Parse(tbAppraisalAmount.Text).ToString("N2"), acquisition.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AppraisalBLL.AddAppraisal(MapControlsToAppraisal());
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    Tasacion appraisal = _AppraisalBLL.GetLastAppraisal(acquisition.Compra_Id);
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AppraisalGenerated"), acquisition.Numero, appraisal.Vigencia.ToString("dd/MM/yyyy")));
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
