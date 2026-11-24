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
using Security.Session;

namespace UI.Compras.Controls
{
    // Inspeccion registrada en CUF08, de solo lectura: vehiculo, kilometraje, resultado, calificaciones y fotos.
    public partial class InspeccionView : UserControl, ILanguageObserver
    {
        public InspeccionDetail inspection_detail;
        public Inspeccion inspection;
        public List<PuntajeInspeccionDetail> inspection_scores;
        List<FotografiaInspeccion> inspection_photos;
        InspeccionBLL _InspectionBLL;

        public InspeccionView()
        {
            InitializeComponent();
            gridPhotos.AutoGenerateColumns = false;
            _InspectionBLL = new InspeccionBLL();
            UpdateLanguage("");
        }

        public void InitWithInspection(Guid Inspection_Id)
        {
            inspection_detail = _InspectionBLL.GetInspectionDetail(Inspection_Id);
            inspection = _InspectionBLL.GetInspection(Inspection_Id);
            inspection_scores = _InspectionBLL.GetInspectionScores(Inspection_Id);
            inspection_photos = _InspectionBLL.GetInspectionPhotos(Inspection_Id);
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
        public string GetItemDescription(PuntajeInspeccionDetail score)
        {
            return LanguageService.GetTagText("InspectionItem_" + score.Descripcion.Replace(" ", ""), score.Descripcion);
        }
        public string GetRatingDescription(InspeccionRating rating)
        {
            return LanguageService.GetTagText("InspectionRating_" + rating);
        }
        public string GetResultDescription()
        {
            return inspection == null || inspection.Resultado == null ? "" : LanguageService.GetTagText("InspectionResult_" + inspection.Resultado);
        }

        private void PerformUpdateScoresView()
        {
            gridScores.Rows.Clear();
            if (inspection_scores == null)
                return;
            foreach (PuntajeInspeccionDetail score in inspection_scores)
                gridScores.Rows.Add(GetItemDescription(score), GetRatingDescription(score.Puntaje), score.Observaciones);
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
            lblVehicleData.Text = inspection_detail.Vehiculo_Descripcion + " - " + inspection_detail.Patente;
            lblVehicleIdentification.Text = LanguageService.GetTagText("Chassis") + inspection_detail.NumeroChasis
                + " - " + LanguageService.GetTagText("Engine") + inspection_detail.NumeroMotor
                + " - " + LanguageService.GetTagText("DeclaredKilometers") + inspection_detail.Kilometraje;
            lblInspectionData.Text = LanguageService.GetTagText("InspectionPerformedDate") + (inspection.FechaRealizada == null ? "" : inspection.FechaRealizada.Value.ToString("dd/MM/yyyy HH:mm"))
                + " - " + LanguageService.GetTagText("VerifiedKilometers") + inspection.KilometrajeVerificado
                + " - " + LanguageService.GetTagText("lblResult") + " " + GetResultDescription();
            // Mismo aviso que en CUF08 6.1: la diferencia con lo declarado tambien pesa en la tasacion.
            if (inspection.KilometrajeVerificado != null && inspection.KilometrajeVerificado != inspection_detail.Kilometraje)
                lblKilometersDifference.Text = string.Format(LanguageService.GetTagText("KilometersDifference"),
                    (inspection.KilometrajeVerificado.Value - inspection_detail.Kilometraje).ToString("+#;-#"));
            else
                lblKilometersDifference.Text = "";
            tbInspectionObservations.Text = inspection.Observaciones;
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
                        FotografiaInspeccion photo = (FotografiaInspeccion)row.DataBoundItem;
                        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(photo.NombreArchivo));
                        File.WriteAllBytes(tempFilePath, photo.Contenido);
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
