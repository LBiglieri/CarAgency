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

namespace UI.Vehicles
{
    public partial class RegisterInspectionForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        AcquisitionBLL _AcquisitionBLL;
        List<ScheduledInspection> scheduled_inspections;
        List<InspectionItem> inspection_items;
        List<InspectionPhoto> inspection_photos;
        ScheduledInspection inspection;
        InspectionDetail inspection_detail;

        public RegisterInspectionForm()
        {
            InitializeComponent();
            // El Designer no guarda AutoGenerateColumns: las grillas usan las columnas definidas ahi.
            gridScheduled.AutoGenerateColumns = false;
            gridPhotos.AutoGenerateColumns = false;
            _AcquisitionBLL = new AcquisitionBLL();

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
            this.Text = LanguageService.GetTagText("RegisterInspectionForm");
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
                scheduled_inspections = _AcquisitionBLL.GetScheduledInspections();
                gridScheduled.DataSource = null;
                gridScheduled.DataSource = scheduled_inspections;
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
                inspection_items = _AcquisitionBLL.GetInspectionItems();
                gridItems.Rows.Clear();
                foreach (InspectionItem item in inspection_items)
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
                    inspection_photos = _AcquisitionBLL.GetInspectionPhotos(inspection.Id);
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
        private string GetItemDescription(InspectionItem item)
        {
            return LanguageService.GetTagText("InspectionItem_" + item.Description.Replace(" ", ""), item.Description);
        }
        private void PerformTranslateItems()
        {
            foreach (DataGridViewRow row in gridItems.Rows)
                row.Cells[colItem.Index].Value = GetItemDescription((InspectionItem)row.Tag);
        }
        private void PerformUpdateScoreCombo()
        {
            List<KeyValuePair<InspectionRating, string>> scores = new List<KeyValuePair<InspectionRating, string>>();
            foreach (InspectionRating score in Enum.GetValues(typeof(InspectionRating)))
                scores.Add(new KeyValuePair<InspectionRating, string>(score, LanguageService.GetTagText("InspectionRating_" + score)));
            colScore.DisplayMember = "Value";
            colScore.ValueMember = "Key";
            colScore.DataSource = scores;
        }
        // La ausencia no se elige aca: tiene su propio boton.
        private void PerformUpdateResultCombo()
        {
            int selectedIndex = comboResult.SelectedIndex;
            List<KeyValuePair<InspectionResult, string>> results = new List<KeyValuePair<InspectionResult, string>>();
            results.Add(new KeyValuePair<InspectionResult, string>(InspectionResult.Fit, LanguageService.GetTagText("InspectionResult_Fit")));
            results.Add(new KeyValuePair<InspectionResult, string>(InspectionResult.Unfit, LanguageService.GetTagText("InspectionResult_Unfit")));
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
            lblVehicleData.Text = inspection_detail.Vehicle_Description + " - " + inspection_detail.License_Plate;
            lblVehicleIdentification.Text = LanguageService.GetTagText("Chassis") + inspection_detail.Chassis_Number
                + " - " + LanguageService.GetTagText("Engine") + inspection_detail.Engine_Number
                + " - " + LanguageService.GetTagText("DeclaredKilometers") + inspection_detail.Declared_Kilometers;
        }
        // CUF08 6.1: la diferencia con lo declarado en CUF07 se ve mientras se escribe el kilometraje.
        private void PerformUpdateKilometersDifference()
        {
            int kilometers;
            if (inspection_detail != null && int.TryParse(tbVerified_Kilometers.Text, out kilometers) && kilometers != inspection_detail.Declared_Kilometers)
                lblKilometersDifference.Text = string.Format(LanguageService.GetTagText("KilometersDifference"),
                    (kilometers - inspection_detail.Declared_Kilometers).ToString("+#;-#"));
            else
                lblKilometersDifference.Text = "";
        }
        private void PerformSelectInspection()
        {
            try
            {
                ScheduledInspection selected = gridScheduled.CurrentRow == null ? null : (ScheduledInspection)gridScheduled.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearInspection();
                    return;
                }
                if (inspection != null && inspection.Id == selected.Id)
                    return;

                ClearInspection();
                inspection = selected;
                inspection_detail = _AcquisitionBLL.GetInspectionDetail(inspection.Id);
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
                    unscored.Add(GetItemDescription((InspectionItem)row.Tag));
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
        private Inspection MapSelectionToInspection()
        {
            Inspection _inspection = new Inspection();
            _inspection.Id = inspection.Id;
            _inspection.Acquisition_Id = inspection.Acquisition_Id;
            _inspection.Mechanic_Id = inspection.Mechanic_Id;
            _inspection.Scheduled_Date = inspection.Scheduled_Date;
            _inspection.Observations = tbInspectionObservations.Text.Trim();
            return _inspection;
        }
        private Inspection MapControlsToInspection()
        {
            Inspection _inspection = MapSelectionToInspection();
            _inspection.Verified_Kilometers = int.Parse(tbVerified_Kilometers.Text);
            _inspection.Result = ((KeyValuePair<InspectionResult, string>)comboResult.SelectedItem).Key;
            _inspection.Scores = new List<InspectionScore>();
            foreach (DataGridViewRow row in gridItems.Rows)
            {
                InspectionScore score = new InspectionScore();
                score.InspectionItem_Id = ((InspectionItem)row.Tag).Id;
                score.Score = (InspectionRating)row.Cells[colScore.Index].Value;
                score.Observations = Convert.ToString(row.Cells[colScoreObservations.Index].Value).Trim();
                _inspection.Scores.Add(score);
            }
            // CUF08 6.1: si no coincide con lo declarado, queda anotado en las observaciones con ambos valores.
            if (_inspection.Verified_Kilometers != inspection_detail.Declared_Kilometers)
                _inspection.Observations = (_inspection.Observations + Environment.NewLine + string.Format(LanguageService.GetTagText("KilometersDifferenceObservation"),
                    inspection_detail.Declared_Kilometers, _inspection.Verified_Kilometers)).Trim();
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
                            InspectionPhoto photo = new InspectionPhoto();
                            photo.Inspection_Id = inspection.Id;
                            photo.FileName = Path.GetFileName(filePath);
                            photo.FileContent = File.ReadAllBytes(filePath);
                            _AcquisitionBLL.AddInspectionPhoto(photo);
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

        private void btnDeletePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridPhotos.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = gridPhotos.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        InspectionPhoto photo = (InspectionPhoto)row.DataBoundItem;
                        _AcquisitionBLL.DeleteInspectionPhoto(photo);
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

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRecordInspection"), inspection.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                Inspection _inspection = MapControlsToInspection();
                SQLUpdateResult result = _AcquisitionBLL.RecordInspection(_inspection);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    if (_inspection.Result == InspectionResult.Fit)
                        MessageBox.Show(string.Format(LanguageService.GetTagText("InspectionRecordedFit"), inspection.Number));
                    else
                        MessageBox.Show(string.Format(LanguageService.GetTagText("InspectionRecordedUnfit"), inspection.Number));
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

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRegisterNoShow"), inspection.License_Plate), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterNoShow(MapSelectionToInspection());
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("NoShowRegistered"), inspection.Number));
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
