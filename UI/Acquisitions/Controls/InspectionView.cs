using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
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

namespace UI.Acquisitions.Controls
{
    // Inspeccion registrada en CUF08, de solo lectura: vehiculo, kilometraje, resultado, calificaciones y fotos.
    public partial class InspectionView : UserControl, ILanguageObserver
    {
        public InspectionDetail inspection_detail;
        public Inspection inspection;
        public List<InspectionScoreDetail> inspection_scores;
        List<InspectionPhoto> inspection_photos;
        AcquisitionBLL _AcquisitionBLL;

        public InspectionView()
        {
            InitializeComponent();
            gridPhotos.AutoGenerateColumns = false;
            _AcquisitionBLL = new AcquisitionBLL();
            UpdateLanguage("");
        }

        public void InitWithInspection(Guid Inspection_Id)
        {
            inspection_detail = _AcquisitionBLL.GetInspectionDetail(Inspection_Id);
            inspection = _AcquisitionBLL.GetInspection(Inspection_Id);
            inspection_scores = _AcquisitionBLL.GetInspectionScores(Inspection_Id);
            inspection_photos = _AcquisitionBLL.GetInspectionPhotos(Inspection_Id);
            gridPhotos.DataSource = null;
            gridPhotos.DataSource = inspection_photos;
            UpdateLanguage("");
        }

        public void Clear()
        {
            inspection_detail = null;
            inspection = null;
            inspection_scores = null;
            inspection_photos = null;
            gridPhotos.DataSource = null;
            UpdateLanguage("");
        }

        public void UpdateLanguage(string language)
        {
            lblInspectionResult.Text = LanguageService.GetTagText(lblInspectionResult.Tag.ToString());
            lblGeneralObservations.Text = LanguageService.GetTagText(lblGeneralObservations.Tag.ToString());
            tbInspectionObservations.WaterMark = LanguageService.GetTagText(tbInspectionObservations.Tag.ToString());
            btnOpenPhoto.Text = LanguageService.GetTagText(btnOpenPhoto.Tag.ToString());
            colItem.HeaderText = LanguageService.GetTagText(colItem.Tag.ToString());
            colScore.HeaderText = LanguageService.GetTagText(colScore.Tag.ToString());
            colScoreObservations.HeaderText = LanguageService.GetTagText(colScoreObservations.Tag.ToString());
            colFileName.HeaderText = LanguageService.GetTagText(colFileName.Tag.ToString());
            colUploadedDate.HeaderText = LanguageService.GetTagText(colUploadedDate.Tag.ToString());
            MapInspectionToControls();
            PerformUpdateScoresView();
        }

        // El catalogo y las calificaciones estan en ingles: se muestran con su traduccion.
        public string GetItemDescription(InspectionScoreDetail score)
        {
            return LanguageService.GetTagText("InspectionItem_" + score.Description.Replace(" ", ""), score.Description);
        }
        public string GetRatingDescription(InspectionRating rating)
        {
            return LanguageService.GetTagText("InspectionRating_" + rating);
        }
        public string GetResultDescription()
        {
            return inspection == null || inspection.Result == null ? "" : LanguageService.GetTagText("InspectionResult_" + inspection.Result);
        }

        private void PerformUpdateScoresView()
        {
            gridScores.Rows.Clear();
            if (inspection_scores == null)
                return;
            foreach (InspectionScoreDetail score in inspection_scores)
                gridScores.Rows.Add(GetItemDescription(score), GetRatingDescription(score.Score), score.Observations);
        }

        private void MapInspectionToControls()
        {
            if (inspection_detail == null || inspection == null)
            {
                lblVehicleData.Text = "";
                lblVehicleIdentification.Text = "";
                lblInspectionData.Text = "";
                lblKilometersDifference.Text = "";
                tbInspectionObservations.Text = "";
                return;
            }
            lblVehicleData.Text = inspection_detail.Vehicle_Description + " - " + inspection_detail.License_Plate;
            lblVehicleIdentification.Text = LanguageService.GetTagText("Chassis") + inspection_detail.Chassis_Number
                + " - " + LanguageService.GetTagText("Engine") + inspection_detail.Engine_Number
                + " - " + LanguageService.GetTagText("DeclaredKilometers") + inspection_detail.Declared_Kilometers;
            lblInspectionData.Text = LanguageService.GetTagText("InspectionPerformedDate") + (inspection.Performed_Date == null ? "" : inspection.Performed_Date.Value.ToString("dd/MM/yyyy HH:mm"))
                + " - " + LanguageService.GetTagText("VerifiedKilometers") + inspection.Verified_Kilometers
                + " - " + LanguageService.GetTagText("lblResult") + " " + GetResultDescription();
            // Mismo aviso que en CUF08 6.1: la diferencia con lo declarado tambien pesa en la tasacion.
            if (inspection.Verified_Kilometers != null && inspection.Verified_Kilometers != inspection_detail.Declared_Kilometers)
                lblKilometersDifference.Text = string.Format(LanguageService.GetTagText("KilometersDifference"),
                    (inspection.Verified_Kilometers.Value - inspection_detail.Declared_Kilometers).ToString("+#;-#"));
            else
                lblKilometersDifference.Text = "";
            tbInspectionObservations.Text = inspection.Observations;
        }

        private void btnOpenPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridPhotos.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = gridPhotos.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        InspectionPhoto photo = (InspectionPhoto)row.DataBoundItem;
                        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(photo.FileName));
                        File.WriteAllBytes(tempFilePath, photo.FileContent);
                        Process.Start(tempFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
