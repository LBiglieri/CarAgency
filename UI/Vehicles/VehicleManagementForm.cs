using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
using CarAgency.UI;
using CarAgency.Utilities.Persistence;
using Entities;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Utilities.Session;

namespace UI.Vehicles
{
    public enum VehicleManagementFormAction
    {
        NoAction,
        Add,
        Update,
        Delete
    }
    public partial class VehicleManagementForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehicleBLL _VehicleBLL;
        List<Vehicle> vehicles = new List<Vehicle>();
        Vehicle selected_vehicle;

        VehicleManagementFormAction Form_Action;
        public VehicleManagementForm()
        {
            InitializeComponent();
            _VehicleBLL = new VehicleBLL();

            btnCancelOpp_Click(null, null);
            PerformUpdateMakeCombos();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void VehicleManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("VehicleManagementForm");
            this.Refresh();
            tbDoors.WaterMark = LanguageService.GetTagText(tbDoors.Tag.ToString());
            tbLicense_Plate.WaterMark = LanguageService.GetTagText(tbLicense_Plate.Tag.ToString());
            tbYear.WaterMark = LanguageService.GetTagText(tbYear.Tag.ToString());
            tbKilometers.WaterMark = LanguageService.GetTagText(tbKilometers.Tag.ToString());
            tbPrice.WaterMark = LanguageService.GetTagText(tbPrice.Tag.ToString());
            tbImageLink.WaterMark = LanguageService.GetTagText(tbImageLink.Tag.ToString());
            tbOpcionals.WaterMark = LanguageService.GetTagText(tbOpcionals.Tag.ToString());
            tbObservations.WaterMark = LanguageService.GetTagText(tbObservations.Tag.ToString());
            lblVehivle.Text = LanguageService.GetTagText(lblVehivle.Tag.ToString());
            btnAddUser.Text = LanguageService.GetTagText(btnAddUser.Tag.ToString());
            btnUpdateUser.Text = LanguageService.GetTagText(btnUpdateUser.Tag.ToString());
            btnDeleteUser.Text = LanguageService.GetTagText(btnDeleteUser.Tag.ToString());
            btnApplyOpp.Text = LanguageService.GetTagText(btnApplyOpp.Tag.ToString());
            btnCancelOpp.Text = LanguageService.GetTagText(btnCancelOpp.Tag.ToString());
        }
        void PerformUpdateVehiclesView()
        {
            vehicles = _VehicleBLL.GetAll();
            metroGrid1.DataSource = null;
            metroGrid1.DataSource = vehicles;
            metroGrid1.Columns["Make_Id"].Visible = false;
            metroGrid1.Columns["Model_Id"].Visible = false;
            metroGrid1.Columns["Version_Id"].Visible = false;
            metroGrid1.Columns["Colour_Id"].Visible = false;
            metroGrid1.Columns["ImageLink"].Visible = false;
        }
        private void PerformUpdateColourCombos()
        {
            List<Colour> colour = _VehicleBLL.GetAllColours();
            comboColour.DataSource = colour;
            comboColour.DisplayMember = "Description";
            comboColour.SelectedIndex = -1;
        }
        private void PerformUpdateMakeCombos()
        {
            List<Make> makes = _VehicleBLL.GetAllMakes();
            comboMake.DataSource = makes;
            comboMake.DisplayMember = "Description";
            comboMake.SelectedIndex = -1;
        }
        private void PerformUpdateModelCombos()
        {
            if (comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedValue) != null) 
            {
                List<Model> models = _VehicleBLL.GetAllModelsByMake((Make)comboMake.SelectedValue);
                comboModel.DataSource = models;
                comboModel.DisplayMember = "Description";
                comboModel.SelectedIndex = -1;
            }
        }
        private void PerformUpdateVersionCombo()
        {
            if (comboModel.SelectedIndex != -1 && comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedValue) != null && ((Model)comboModel.SelectedValue) != null) 
            {
                List<CarAgency.Entities.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Make)comboMake.SelectedValue,(Model)comboModel.SelectedValue);
                comboVersions.DataSource = versions;
                comboVersions.DisplayMember = "Description";
                comboVersions.SelectedIndex = -1;
            }
        }
        void PerformEnableDisableTextBoxes(Boolean flag)
        {
            tbLicense_Plate.Enabled = flag;
            tbPrice.Enabled = flag;
            tbYear.Enabled = flag;
            tbDoors.Enabled = flag;
            tbKilometers.Enabled = flag;
            tbImageLink.Enabled = flag;
            tbOpcionals.Enabled = flag;
            tbObservations.Enabled = flag;
            comboColour.Enabled = flag;
            comboMake.Enabled = flag;
            comboModel.Enabled = flag;
            comboVersions.Enabled = flag;
        }
        void PerformClearTextBoxData()
        {
            tbLicense_Plate.Text = "";
            tbPrice.Text = "";
            tbYear.Text = "";
            tbDoors.Text = "";
            tbKilometers.Text = "";
            tbImageLink.Text = "";
            tbOpcionals.Text = "";
            tbObservations.Text = "";
            comboColour.DataSource = null;
            comboMake.DataSource = null;
            comboModel.DataSource = null;
            comboVersions.DataSource = null;
        }
        void PerformPopulateTextBoxData()
        {
            tbLicense_Plate.Text = selected_vehicle.License_Plate;
            tbPrice.Text = selected_vehicle.Price.ToString();
            tbYear.Text = selected_vehicle.Year.ToString();
            tbDoors.Text = selected_vehicle.Doors.ToString();
            tbKilometers.Text = selected_vehicle.Kilometers.ToString();
            tbImageLink.Text = selected_vehicle.ImageLink;
            tbOpcionals.Text = selected_vehicle.Opcionals;
            tbObservations.Text = selected_vehicle.Observations;
            PerformUpdateColourCombos();
            comboColour.SelectedIndex = comboColour.FindStringExact(selected_vehicle.Colour_Description);
            PerformUpdateMakeCombos();
            comboMake.SelectedIndex = comboMake.FindStringExact(selected_vehicle.Make_Description);
            PerformUpdateModelCombos();
            comboModel.SelectedIndex = comboModel.FindStringExact(selected_vehicle.Model_Description);
            PerformUpdateVersionCombo();
            comboVersions.SelectedIndex = comboVersions.FindStringExact(selected_vehicle.Version_Description);
        }
        void PerformCleanFormAction()
        {
            selected_vehicle = null;
            metroGrid1.Enabled = false;
            PerformEnableDisableTextBoxes(false);
            PerformClearTextBoxData();
            Form_Action = VehicleManagementFormAction.NoAction;
            PerformUpdateVehiclesView();
        }

        private void MapTextboxesToVehicle()
        {
            selected_vehicle.License_Plate = tbLicense_Plate.Text;
            selected_vehicle.Price = double.Parse(tbPrice.Text);
            selected_vehicle.Year = int.Parse(tbYear.Text);
            selected_vehicle.Doors = int.Parse(tbDoors.Text);
            selected_vehicle.Kilometers = int.Parse(tbKilometers.Text);
            selected_vehicle.ImageLink = tbImageLink.Text;
            selected_vehicle.Opcionals = tbOpcionals.Text;
            selected_vehicle.Observations = tbObservations.Text;
            selected_vehicle.Colour_Id = ((Colour)comboColour.SelectedItem).Id;
            selected_vehicle.Make_Id = ((Make)comboMake.SelectedItem).Id;
            selected_vehicle.Model_Id = ((Model)comboModel.SelectedItem).Id;
            selected_vehicle.Version_Id = ((CarAgency.Entities.Version)comboVersions.SelectedItem).Id;
        }

        private void ToggleActionButtonsStates(bool state)
        {
            btnAddUser.Enabled = state;
            btnUpdateUser.Enabled = state;
            btnDeleteUser.Enabled = state;
            btnApplyOpp.Enabled = !state;
            btnCancelOpp.Enabled = !state;
        }

        private bool PerformValidateTextBoxData()
        {
            //faltan validaciones
            return true;
        }
        #endregion


        private void comboDeleteVersionMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
        }

        private void comboDeleteVersionModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateVersionCombo();
        }

        private void btnCancelOpp_Click(object sender, EventArgs e)
        {
            PerformCleanFormAction();
            ToggleActionButtonsStates(true);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            selected_vehicle = null;
            metroGrid1.Enabled = false;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            PerformUpdateColourCombos();
            PerformUpdateMakeCombos();
            Form_Action = VehicleManagementFormAction.Add;
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            selected_vehicle = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = VehicleManagementFormAction.Update;
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            selected_vehicle = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = VehicleManagementFormAction.Delete;
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (Form_Action == VehicleManagementFormAction.Update || Form_Action == VehicleManagementFormAction.Delete)
            {
                selected_vehicle = (Vehicle)metroGrid1.CurrentRow.DataBoundItem;
                PerformPopulateTextBoxData();
            }
        }

        private void btnApplyOpp_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            if (Form_Action != VehicleManagementFormAction.Add && selected_vehicle == null)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectVehicle"));
                ToggleActionButtonsStates(true);
                PerformCleanFormAction();
                return;
            }
            try
            {
                switch (Form_Action)
                {
                    case VehicleManagementFormAction.Add:
                        if (PerformValidateTextBoxData())
                        {
                            selected_vehicle = new Vehicle();
                            MapTextboxesToVehicle();
                            result = _VehicleBLL.AddVehicle(selected_vehicle);
                        }
                        break;
                    case VehicleManagementFormAction.Update:
                        if (PerformValidateTextBoxData())
                        {
                            MapTextboxesToVehicle();
                            if (MessageBox.Show(LanguageService.GetTagText("AreYouSureUpdateVehicle") + selected_vehicle.License_Plate + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                result = _VehicleBLL.UpdateVehicle(selected_vehicle);
                            }
                        }
                        break;
                    case VehicleManagementFormAction.Delete:
                        if (MessageBox.Show(LanguageService.GetTagText("AreYouSureDeleteVehicle") + selected_vehicle.License_Plate + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _VehicleBLL.DeleteVehicle(selected_vehicle);
                        }
                        break;
                }

                if (result != null)
                {
                    MessageBox.Show(LanguageService.GetTagText("OperationSuccesfull"));
                }
                PerformCleanFormAction();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                ToggleActionButtonsStates(true);
                PerformCleanFormAction();
            }
        }

    }
}
