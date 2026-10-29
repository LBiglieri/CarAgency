using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Clients.Controls;
using Security.Session;

namespace UI.Vehicles
{
    public partial class RegisterAcquisitionForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehicleBLL _VehicleBLL;
        AcquisitionBLL _AcquisitionBLL;

        public RegisterAcquisitionForm()
        {
            InitializeComponent();
            _VehicleBLL = new VehicleBLL();
            _AcquisitionBLL = new AcquisitionBLL();

            clientView1.clientViewMode = ClientViewMode.WithRegistration;
            this.clientView1.ClientFound += new EventHandler(clientView1_ClientFound);

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void RegisterAcquisitionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("RegisterAcquisitionForm");
            this.Refresh();
            lblVehicleToAppraise.Text = LanguageService.GetTagText(lblVehicleToAppraise.Tag.ToString());
            lblMake.Text = LanguageService.GetTagText(lblMake.Tag.ToString());
            lblModel.Text = LanguageService.GetTagText(lblModel.Tag.ToString());
            lblVersion.Text = LanguageService.GetTagText(lblVersion.Tag.ToString());
            lblColour.Text = LanguageService.GetTagText(lblColour.Tag.ToString());
            tbLicense_Plate.WaterMark = LanguageService.GetTagText(tbLicense_Plate.Tag.ToString());
            tbChassis_Number.WaterMark = LanguageService.GetTagText(tbChassis_Number.Tag.ToString());
            tbEngine_Number.WaterMark = LanguageService.GetTagText(tbEngine_Number.Tag.ToString());
            tbYear.WaterMark = LanguageService.GetTagText(tbYear.Tag.ToString());
            tbDoors.WaterMark = LanguageService.GetTagText(tbDoors.Tag.ToString());
            tbDeclared_Kilometers.WaterMark = LanguageService.GetTagText(tbDeclared_Kilometers.Tag.ToString());
            lblInspection.Text = LanguageService.GetTagText(lblInspection.Tag.ToString());
            lblInspectionDate.Text = LanguageService.GetTagText(lblInspectionDate.Tag.ToString());
            lblInspectionHour.Text = LanguageService.GetTagText(lblInspectionHour.Tag.ToString());
            lblMechanic.Text = LanguageService.GetTagText(lblMechanic.Tag.ToString());
            btnRegisterAcquisition.Text = LanguageService.GetTagText(btnRegisterAcquisition.Tag.ToString());
        }
        private void PerformUpdateColourCombos()
        {
            try
            {
                List<Colour> colour = _VehicleBLL.GetAllColours();
                comboColour.DataSource = colour;
                comboColour.DisplayMember = "Description";
                comboColour.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateMakeCombos()
        {
            try
            {
                List<Make> makes = _VehicleBLL.GetAllMakes();
                comboMake.DataSource = makes;
                comboMake.DisplayMember = "Description";
                comboMake.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateModelCombos()
        {
            try
            {
                if (comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedValue) != null)
                {
                    List<Model> models = _VehicleBLL.GetAllModelsByMake((Make)comboMake.SelectedValue);
                    comboModel.DataSource = models;
                    comboModel.DisplayMember = "Description";
                    comboModel.SelectedIndex = -1;
                    comboVersions.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateVersionCombo()
        {
            try
            {
                if (comboModel.SelectedIndex != -1 && comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedValue) != null && ((Model)comboModel.SelectedValue) != null)
                {
                    List<CarAgency.BE.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Make)comboMake.SelectedValue, (Model)comboModel.SelectedValue);
                    comboVersions.DataSource = versions;
                    comboVersions.DisplayMember = "Description";
                    comboVersions.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateMechanicCombo()
        {
            try
            {
                List<User> mechanics = _AcquisitionBLL.GetMechanics();
                comboMechanic.DataSource = mechanics;
                comboMechanic.DisplayMember = "Username";
                comboMechanic.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateHourCombo()
        {
            comboHour.DataSource = _AcquisitionBLL.GetInspectionHours().Select(h => h.ToString(@"hh\:mm")).ToList();
            comboHour.SelectedIndex = -1;
        }
        private bool ValidateControls()
        {
            int number;
            if (comboMake.SelectedIndex == -1 || comboModel.SelectedIndex == -1 || comboVersions.SelectedIndex == -1 || comboColour.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectVehicleModel"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbLicense_Plate.Text.Trim() == "" || tbChassis_Number.Text.Trim() == "" || tbEngine_Number.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteVehicleIdentification"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (!int.TryParse(tbYear.Text, out number) || number < 1900 || number > DateTime.Now.Year + 1)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidYear"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (!int.TryParse(tbDoors.Text, out number) || number < 2 || number > 5)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidDoors"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (!int.TryParse(tbDeclared_Kilometers.Text, out number) || number < 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidKilometers"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (comboHour.SelectedIndex == -1 || comboMechanic.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectInspectionMechanic"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        private ProspectVehicle MapControlsToProspectVehicle()
        {
            ProspectVehicle vehicle = new ProspectVehicle();
            vehicle.License_Plate = tbLicense_Plate.Text;
            vehicle.Chassis_Number = tbChassis_Number.Text;
            vehicle.Engine_Number = tbEngine_Number.Text;
            vehicle.Make_Id = ((Make)comboMake.SelectedItem).Id;
            vehicle.Model_Id = ((Model)comboModel.SelectedItem).Id;
            vehicle.Version_Id = ((CarAgency.BE.Version)comboVersions.SelectedItem).Id;
            vehicle.Colour_Id = ((Colour)comboColour.SelectedItem).Id;
            vehicle.Year = int.Parse(tbYear.Text);
            vehicle.Doors = int.Parse(tbDoors.Text);
            vehicle.Declared_Kilometers = int.Parse(tbDeclared_Kilometers.Text);
            return vehicle;
        }
        private DateTime GetScheduledDate()
        {
            return dtInspection_Date.Value.Date + TimeSpan.Parse(comboHour.SelectedItem.ToString());
        }
        private void MapSlotToControls(DateTime slot)
        {
            dtInspection_Date.Value = slot.Date;
            comboHour.SelectedIndex = comboHour.FindStringExact(slot.ToString("HH:mm"));
        }
        // CUF07 4.1: se muestra el registro existente y no se continua hasta corregir el dato.
        private bool PerformValidateDuplicates(ProspectVehicle vehicle)
        {
            List<DuplicateVehicle> duplicates = _AcquisitionBLL.GetDuplicates(vehicle);
            if (duplicates == null)
                return true;

            string message = LanguageService.GetTagText("VehicleAlreadyRegistered") + Environment.NewLine;
            foreach (DuplicateVehicle duplicate in duplicates)
            {
                message += Environment.NewLine;
                if (duplicate.Number == 0)
                    message += LanguageService.GetTagText("DuplicateInInventory") + duplicate.Vehicle_Description;
                else
                    message += LanguageService.GetTagText("DuplicateInAcquisition") + duplicate.Number + ": " + duplicate.Vehicle_Description
                        + " - " + LanguageService.GetTagText("Chassis") + duplicate.Chassis_Number
                        + " - " + LanguageService.GetTagText("Engine") + duplicate.Engine_Number;
            }
            MessageBox.Show(message, LanguageService.GetTagText("AttentionTitle"));
            return false;
        }
        // CUF07 6.1: si el mecanico esta ocupado se propone el proximo horario libre y se vuelve a elegir.
        private bool PerformValidateMechanicAvailability(User mechanic, DateTime scheduledDate)
        {
            if (_AcquisitionBLL.IsMechanicAvailable(mechanic.Id, scheduledDate))
                return true;

            DateTime nextSlot = _AcquisitionBLL.GetNextFreeSlot(mechanic.Id, scheduledDate);
            MapSlotToControls(nextSlot);
            MessageBox.Show(LanguageService.GetTagText("MechanicBusyNextSlot") + nextSlot.ToString("dd/MM/yyyy HH:mm"), LanguageService.GetTagText("AttentionTitle"));
            return false;
        }
        #endregion

        private void comboMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
        }

        private void comboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateVersionCombo();
        }

        private void btnRegisterAcquisition_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateControls())
                    return;

                ProspectVehicle vehicle = MapControlsToProspectVehicle();
                if (!PerformValidateDuplicates(vehicle))
                    return;

                User mechanic = (User)comboMechanic.SelectedItem;
                DateTime scheduledDate = GetScheduledDate();
                if (!PerformValidateMechanicAvailability(mechanic, scheduledDate))
                    return;

                if (MessageBox.Show(LanguageService.GetTagText("AreYouSureRegisterAcquisition") + vehicle.License_Plate + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                Acquisition acquisition = new Acquisition();
                acquisition.Client_Id = clientView1.client.Id;
                acquisition.ProspectVehicle = vehicle;
                acquisition.Inspection = new Inspection();
                acquisition.Inspection.Mechanic_Id = mechanic.Id;
                acquisition.Inspection.Scheduled_Date = scheduledDate;

                SQLUpdateResult result = _AcquisitionBLL.AddAcquisition(acquisition);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(LanguageService.GetTagText("AcquisitionRegistered") + acquisition.Number);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        public void clientView1_ClientFound(object sender, EventArgs e)
        {
            if (clientView1.client != null)
            {
                VehicleDetailsPanel.Enabled = true;
                InspectionPanel.Enabled = true;
                btnRegisterAcquisition.Enabled = true;
                PerformUpdateMakeCombos();
                PerformUpdateColourCombos();
                PerformUpdateHourCombo();
                PerformUpdateMechanicCombo();
            }
        }
    }
}
