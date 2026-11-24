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
using UI.Ayuda;

namespace UI.Vehiculos
{
    public partial class RegisterInspeccionForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        InspeccionBLL _InspectionBLL;
        List<ScheduledInspeccion> scheduled_inspections;
        List<ItemInspeccion> inspection_items;
        List<FotografiaInspeccion> inspection_photos;
        ScheduledInspeccion inspection;
        InspeccionDetail inspection_detail;

        public RegisterInspeccionForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            // El Designer no guarda AutoGenerateColumns: las grillas usan las columnas definidas ahi.
            gridScheduled.AutoGenerateColumns = false;
            gridPhotos.AutoGenerateColumns = false;
            _InspectionBLL = new InspeccionBLL();

            PerformUpdateItemsView();
            PerformUpdateScheduledView();

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void RegisterInspectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("RegisterInspeccionForm");
            this.Refresh();
            lblScheduledInspections.Text = LanguageService.GetTagText(lblScheduledInspections.Tag.ToString());
            lblInspectedVehicle.Text = LanguageService.GetTagText(lblInspectedVehicle.Tag.ToString());
            lblResult.Text = LanguageService.GetTagText(lblResult.Tag.ToString());
            tbVerified_Kilometers.WaterMark = LanguageService.GetTagText(tbVerified_Kilometers.Tag.ToString());
            tbInspectionObservations.WaterMark = LanguageService.GetTagText(tbInspectionObservations.Tag.ToString());
            btnAddPhoto.Text = LanguageService.GetTagText(btnAddPhoto.Tag.ToString());
            btnOpenPhoto.Text = LanguageService.GetTagText(btnOpenPhoto.Tag.ToString());
            btnDeletePhoto.Text = LanguageService.GetTagText(btnDeletePhoto.Tag.ToString());
            btnConfirmInspection.Text = LanguageService.GetTagText(btnConfirmInspection.Tag.ToString());
            btnRegisterNoShow.Text = LanguageService.GetTagText(btnRegisterNoShow.Tag.ToString());
            colScheduled_Date.HeaderText = LanguageService.GetTagText(colScheduled_Date.Tag.ToString());
            colNumber.HeaderText = LanguageService.GetTagText(colNumber.Tag.ToString());
            colLicense_Plate.HeaderText = LanguageService.GetTagText(colLicense_Plate.Tag.ToString());
            colVehicle_Description.HeaderText = LanguageService.GetTagText(colVehicle_Description.Tag.ToString());
            colItem.HeaderText = LanguageService.GetTagText(colItem.Tag.ToString());
            colScore.HeaderText = LanguageService.GetTagText(colScore.Tag.ToString());
            colScoreObservations.HeaderText = LanguageService.GetTagText(colScoreObservations.Tag.ToString());
            colFileName.HeaderText = LanguageService.GetTagText(colFileName.Tag.ToString());
            colUploadedDate.HeaderText = LanguageService.GetTagText(colUploadedDate.Tag.ToString());
            PerformUpdateScoreCombo();
            PerformUpdateResultCombo();
            PerformTranslateItems();
            MapDetailToControls();
            PerformUpdateKilometersDifference();
        }
        void PerformUpdateScheduledView()
        {
            try
            {
                scheduled_inspections = _InspectionBLL.GetScheduledInspections();
                gridScheduled.DataSource = null;
                gridScheduled.DataSource = scheduled_inspections;
                PerformSelectInspection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // Una fila por item del catalogo. El item va en el Tag de la fila y se califica en la grilla.
        void PerformUpdateItemsView()
        {
            try
            {
                inspection_items = _InspectionBLL.GetInspectionItems();
                gridItems.Rows.Clear();
                foreach (ItemInspeccion item in inspection_items)
                {
                    int index = gridItems.Rows.Add();
                    gridItems.Rows[index].Tag = item;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        void PerformUpdatePhotosView()
        {
            try
            {
                if (inspection != null)
                {
                    inspection_photos = _InspectionBLL.GetInspectionPhotos(inspection.Inspeccion_Id);
                    gridPhotos.DataSource = null;
                    gridPhotos.DataSource = inspection_photos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // El catalogo esta en ingles: cada item se muestra con su traduccion, o con la descripcion si no la tiene.
        private string GetItemDescription(ItemInspeccion item)
        {
            return LanguageService.GetTagText("InspectionItem_" + item.Descripcion.Replace(" ", ""), item.Descripcion);
        }
        private void PerformTranslateItems()
        {
            foreach (DataGridViewRow row in gridItems.Rows)
                row.Cells[colItem.Index].Value = GetItemDescription((ItemInspeccion)row.Tag);
        }
        private void PerformUpdateScoreCombo()
        {
            List<KeyValuePair<InspeccionRating, string>> scores = new List<KeyValuePair<InspeccionRating, string>>();
            foreach (InspeccionRating score in Enum.GetValues(typeof(InspeccionRating)))
                scores.Add(new KeyValuePair<InspeccionRating, string>(score, LanguageService.GetTagText("InspectionRating_" + score)));
            colScore.DisplayMember = "Value";
            colScore.ValueMember = "Key";
            colScore.DataSource = scores;
        }
        // La ausencia no se elige aca: tiene su propio boton.
        private void PerformUpdateResultCombo()
        {
            int selectedIndex = comboResult.SelectedIndex;
            List<KeyValuePair<InspeccionResult, string>> results = new List<KeyValuePair<InspeccionResult, string>>();
            results.Add(new KeyValuePair<InspeccionResult, string>(InspeccionResult.Fit, LanguageService.GetTagText("InspectionResult_Fit")));
            results.Add(new KeyValuePair<InspeccionResult, string>(InspeccionResult.Unfit, LanguageService.GetTagText("InspectionResult_Unfit")));
            comboResult.DataSource = results;
            comboResult.DisplayMember = "Value";
            comboResult.ValueMember = "Key";
            comboResult.SelectedIndex = selectedIndex;
        }
        private void MapDetailToControls()
        {
            if (inspection_detail == null)
            {
                lblVehicleData.Text = "";
                lblVehicleIdentification.Text = "";
                return;
            }
            lblVehicleData.Text = inspection_detail.Vehiculo_Descripcion + " - " + inspection_detail.Patente;
            lblVehicleIdentification.Text = LanguageService.GetTagText("Chassis") + inspection_detail.NumeroChasis
                + " - " + LanguageService.GetTagText("Engine") + inspection_detail.NumeroMotor
                + " - " + LanguageService.GetTagText("DeclaredKilometers") + inspection_detail.Kilometraje;
        }
        // CUF08 6.1: la diferencia con lo declarado en CUF07 se ve mientras se escribe el kilometraje.
        private void PerformUpdateKilometersDifference()
        {
            int kilometers;
            if (inspection_detail != null && int.TryParse(tbVerified_Kilometers.Text, out kilometers) && kilometers != inspection_detail.Kilometraje)
                lblKilometersDifference.Text = string.Format(LanguageService.GetTagText("KilometersDifference"),
                    (kilometers - inspection_detail.Kilometraje).ToString("+#;-#"));
            else
                lblKilometersDifference.Text = "";
        }
        private void PerformSelectInspection()
        {
            try
            {
                ScheduledInspeccion selected = gridScheduled.CurrentRow == null ? null : (ScheduledInspeccion)gridScheduled.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearInspection();
                    return;
                }
                if (inspection != null && inspection.Inspeccion_Id == selected.Inspeccion_Id)
                    return;

                ClearInspection();
                inspection = selected;
                inspection_detail = _InspectionBLL.GetInspectionDetail(inspection.Inspeccion_Id);
                MapDetailToControls();
                PerformUpdatePhotosView();
                InspectionPanel.Enabled = true;
                btnRegisterNoShow.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void ClearInspection()
        {
            inspection = null;
            inspection_detail = null;
            inspection_photos = null;
            InspectionPanel.Enabled = false;
            btnRegisterNoShow.Enabled = false;
            gridItems.CancelEdit();
            foreach (DataGridViewRow row in gridItems.Rows)
            {
                row.Cells[colScore.Index].Value = null;
                row.Cells[colScoreObservations.Index].Value = "";
            }
            gridPhotos.DataSource = null;
            tbVerified_Kilometers.Text = "";
            tbInspectionObservations.Text = "";
            comboResult.SelectedIndex = -1;
            MapDetailToControls();
        }
        // CUF08 7.1: se indican los items sin calificar y se vuelve a la grilla.
        private bool ValidateControls()
        {
            int number;
            gridItems.EndEdit();
            List<string> unscored = new List<string>();
            foreach (DataGridViewRow row in gridItems.Rows)
            {
                if (row.Cells[colScore.Index].Value == null)
                    unscored.Add(GetItemDescription((ItemInspeccion)row.Tag));
            }
            if (unscored.Count > 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseScoreInspectionItems") + string.Join(", ", unscored), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (inspection_photos == null || inspection_photos.Count == 0)
            {
                MessageBox.Show(LanguageService.GetTagText("InspectionPhotoRequired"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (!int.TryParse(tbVerified_Kilometers.Text, out number) || number < 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidKilometers"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (comboResult.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectInspectionResult"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        private Inspeccion MapSelectionToInspection()
        {
            Inspeccion _inspection = new Inspeccion();
            _inspection.Inspeccion_Id = inspection.Inspeccion_Id;
            _inspection.Compra_Id = inspection.Compra_Id;
            _inspection.Mecanico_Id = inspection.Mecanico_Id;
            _inspection.FechaProgramada = inspection.FechaProgramada;
            _inspection.Observaciones = tbInspectionObservations.Text.Trim();
            return _inspection;
        }
        private Inspeccion MapControlsToInspection()
        {
            Inspeccion _inspection = MapSelectionToInspection();
            _inspection.KilometrajeVerificado = int.Parse(tbVerified_Kilometers.Text);
            _inspection.Resultado = ((KeyValuePair<InspeccionResult, string>)comboResult.SelectedItem).Key;
            _inspection.Puntajes = new List<PuntajeInspeccion>();
            foreach (DataGridViewRow row in gridItems.Rows)
            {
                PuntajeInspeccion score = new PuntajeInspeccion();
                score.ItemInspeccion_Id = ((ItemInspeccion)row.Tag).ItemInspeccion_Id;
                score.Puntaje = (InspeccionRating)row.Cells[colScore.Index].Value;
                score.Observaciones = Convert.ToString(row.Cells[colScoreObservations.Index].Value).Trim();
                _inspection.Puntajes.Add(score);
            }
            // CUF08 6.1: si no coincide con lo declarado, queda anotado en las observaciones con ambos valores.
            if (_inspection.KilometrajeVerificado != inspection_detail.Kilometraje)
                _inspection.Observaciones = (_inspection.Observaciones + Environment.NewLine + string.Format(LanguageService.GetTagText("KilometersDifferenceObservation"),
                    inspection_detail.Kilometraje, _inspection.KilometrajeVerificado)).Trim();
            return _inspection;
        }
        #endregion

        private void gridScheduled_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectInspection();
        }

        // Los combos de la grilla confirman el valor apenas se elige, sin esperar a salir de la celda.
        private void gridItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridItems.IsCurrentCellDirty && gridItems.CurrentCell is DataGridViewComboBoxCell)
                gridItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tbVerified_Kilometers_TextChanged(object sender, EventArgs e)
        {
            PerformUpdateKilometersDifference();
        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (inspection == null)
                    return;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = LanguageService.GetTagText("PhotoFileFilter");
                    openFileDialog.Title = LanguageService.GetTagText("SelectPhotoTitle");
                    openFileDialog.Multiselect = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string filePath in openFileDialog.FileNames)
                        {
                            FotografiaInspeccion photo = new FotografiaInspeccion();
                            photo.Inspeccion_Id = inspection.Inspeccion_Id;
                            photo.NombreArchivo = Path.GetFileName(filePath);
                            photo.Contenido = File.ReadAllBytes(filePath);
                            _InspectionBLL.AddInspectionPhoto(photo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            PerformUpdatePhotosView();
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

        private void btnDeletePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridPhotos.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = gridPhotos.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        FotografiaInspeccion photo = (FotografiaInspeccion)row.DataBoundItem;
                        _InspectionBLL.DeleteInspectionPhoto(photo);
                    }
                    PerformUpdatePhotosView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnConfirmInspection_Click(object sender, EventArgs e)
        {
            try
            {
                if (inspection == null || !ValidateControls())
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRecordInspection"), inspection.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                Inspeccion _inspection = MapControlsToInspection();
                SQLUpdateResult result = _InspectionBLL.RecordInspection(_inspection);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    if (_inspection.Resultado == InspeccionResult.Fit)
                        MessageBox.Show(string.Format(LanguageService.GetTagText("InspectionRecordedFit"), inspection.Numero));
                    else
                        MessageBox.Show(string.Format(LanguageService.GetTagText("InspectionRecordedUnfit"), inspection.Numero));
                    PerformUpdateScheduledView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // CUF08 2.1: el vehiculo no se presento.
        private void btnRegisterNoShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (inspection == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRegisterNoShow"), inspection.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _InspectionBLL.RegisterNoShow(MapSelectionToInspection());
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("NoShowRegistered"), inspection.Numero));
                    PerformUpdateScheduledView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
