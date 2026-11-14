using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Color = CarAgency.BE.Color;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Security.Session;
using CarAgency.Security.Session;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{
    public enum VehiculoManagementFormAction
    {
        NoAction,
        Add,
        Update,
        Delete,
        History
    }
    public partial class VehiculoManagementForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehiculoBLL _VehicleBLL;
        List<Vehiculo> vehicles = new List<Vehiculo>();
        Vehiculo selected_vehicle;

        VehiculoManagementFormAction Form_Action;
        public VehiculoManagementForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _VehicleBLL = new VehiculoBLL();
            btnHistoryVehicle.Visible = SessionHandler.Instance.IsAuthorized(PermisoType.BitacoraCambiosForm);
            GridColumns.TranslateValues(metroGrid1, "Color_Descripcion", LookupText.Colour);

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
            this.Text = LanguageService.GetTagText("VehiculoManagementForm");
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
            btnHistoryVehicle.Text = LanguageService.GetTagText(btnHistoryVehicle.Tag.ToString());
            btnApplyOpp.Text = LanguageService.GetTagText(btnApplyOpp.Tag.ToString());
            btnCancelOpp.Text = LanguageService.GetTagText(btnCancelOpp.Tag.ToString());
            GridColumns.Translate(metroGrid1);
            if (comboColour.DataSource != null)
                PerformUpdateColourCombos(LookupText.Selected<Color>(comboColour)?.Color_Id);
        }
        void PerformUpdateVehiclesView()
        {
            vehicles = _VehicleBLL.GetAll();
            metroGrid1.DataSource = null;
            metroGrid1.DataSource = vehicles;
            GridColumns.Show(metroGrid1, GridColumns.Vehiculo);
        }
        // Sin color pedido viene elegido el mas comun.
        private void PerformUpdateColourCombos(Guid? selected = null)
        {
            LookupText.Fill(comboColour, _VehicleBLL.GetAllColours(), c => LookupText.Colour(c.Descripcion),
                c => selected == null ? c.Descripcion == VehiculoBLL.DefaultColour : c.Color_Id == selected.Value);
        }
        private void PerformUpdateMakeCombos()
        {
            List<Marca> makes = _VehicleBLL.GetAllMakes();
            comboMake.DataSource = makes;
            comboMake.DisplayMember = "Descripcion";
            comboMake.SelectedIndex = -1;
        }
        private void PerformUpdateModelCombos()
        {
            if (comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedValue) != null) 
            {
                List<Modelo> models = _VehicleBLL.GetAllModelsByMake((Marca)comboMake.SelectedValue);
                comboModel.DataSource = models;
                comboModel.DisplayMember = "Descripcion";
                comboModel.SelectedIndex = -1;
            }
        }
        private void PerformUpdateVersionCombo()
        {
            if (comboModel.SelectedIndex != -1 && comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedValue) != null && ((Modelo)comboModel.SelectedValue) != null) 
            {
                List<CarAgency.BE.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Marca)comboMake.SelectedValue,(Modelo)comboModel.SelectedValue);
                comboVersions.DataSource = versions;
                comboVersions.DisplayMember = "Descripcion";
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
            tbLicense_Plate.Text = selected_vehicle.Patente;
            tbPrice.Text = selected_vehicle.Precio.ToString();
            tbYear.Text = selected_vehicle.Anio.ToString();
            tbDoors.Text = selected_vehicle.Puertas.ToString();
            tbKilometers.Text = selected_vehicle.Kilometraje.ToString();
            tbImageLink.Text = selected_vehicle.LinkImagen;
            tbOpcionals.Text = selected_vehicle.Opcionales;
            tbObservations.Text = selected_vehicle.Observaciones;
            PerformUpdateColourCombos(selected_vehicle.Color_Id);
            PerformUpdateMakeCombos();
            comboMake.SelectedIndex = comboMake.FindStringExact(selected_vehicle.Marca_Descripcion);
            PerformUpdateModelCombos();
            comboModel.SelectedIndex = comboModel.FindStringExact(selected_vehicle.Modelo_Descripcion);
            PerformUpdateVersionCombo();
            comboVersions.SelectedIndex = comboVersions.FindStringExact(selected_vehicle.Version_Descripcion);
        }
        void PerformCleanFormAction()
        {
            selected_vehicle = null;
            metroGrid1.Enabled = false;
            PerformEnableDisableTextBoxes(false);
            PerformClearTextBoxData();
            Form_Action = VehiculoManagementFormAction.NoAction;
            PerformUpdateVehiclesView();
        }

        private void MapTextboxesToVehicle()
        {
            selected_vehicle.Patente = tbLicense_Plate.Text;
            selected_vehicle.Precio = double.Parse(tbPrice.Text);
            selected_vehicle.Anio = int.Parse(tbYear.Text);
            selected_vehicle.Puertas = int.Parse(tbDoors.Text);
            selected_vehicle.Kilometraje = int.Parse(tbKilometers.Text);
            selected_vehicle.LinkImagen = tbImageLink.Text;
            selected_vehicle.Opcionales = tbOpcionals.Text;
            selected_vehicle.Observaciones = tbObservations.Text;
            selected_vehicle.Color_Id = LookupText.Selected<Color>(comboColour).Color_Id;
            selected_vehicle.Marca_Id = ((Marca)comboMake.SelectedItem).Marca_Id;
            selected_vehicle.Modelo_Id = ((Modelo)comboModel.SelectedItem).Modelo_Id;
            selected_vehicle.Version_Id = ((CarAgency.BE.Version)comboVersions.SelectedItem).Version_Id;
        }

        private void ToggleActionButtonsStates(bool state)
        {
            btnAddUser.Enabled = state;
            btnUpdateUser.Enabled = state;
            btnDeleteUser.Enabled = state;
            btnHistoryVehicle.Enabled = state;
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
            Form_Action = VehiculoManagementFormAction.Add;
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            selected_vehicle = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = VehiculoManagementFormAction.Update;
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            selected_vehicle = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = VehiculoManagementFormAction.Delete;
        }

        // Igual que Eliminar: se elige el vehiculo en la grilla y Aplicar abre su bitacora de cambios.
        private void btnHistoryVehicle_Click(object sender, EventArgs e)
        {
            selected_vehicle = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = VehiculoManagementFormAction.History;
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (Form_Action == VehiculoManagementFormAction.Update || Form_Action == VehiculoManagementFormAction.Delete
                || Form_Action == VehiculoManagementFormAction.History)
            {
                selected_vehicle = (Vehiculo)metroGrid1.CurrentRow.DataBoundItem;
                PerformPopulateTextBoxData();
            }
        }

        private void btnApplyOpp_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            if (Form_Action != VehiculoManagementFormAction.Add && selected_vehicle == null)
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
                    case VehiculoManagementFormAction.Add:
                        if (PerformValidateTextBoxData())
                        {
                            selected_vehicle = new Vehiculo();
                            MapTextboxesToVehicle();
                            result = _VehicleBLL.AddVehicle(selected_vehicle);
                        }
                        break;
                    case VehiculoManagementFormAction.Update:
                        if (PerformValidateTextBoxData())
                        {
                            MapTextboxesToVehicle();
                            if (MessageBox.Show(LanguageService.GetTagText("AreYouSureUpdateVehicle") + selected_vehicle.Patente + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                result = _VehicleBLL.UpdateVehicle(selected_vehicle);
                            }
                        }
                        break;
                    case VehiculoManagementFormAction.Delete:
                        if (MessageBox.Show(LanguageService.GetTagText("AreYouSureDeleteVehicle") + selected_vehicle.Patente + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _VehicleBLL.DeleteVehicle(selected_vehicle);
                        }
                        break;
                    case VehiculoManagementFormAction.History:
                        BitacoraCambiosForm history = new BitacoraCambiosForm(selected_vehicle.Vehiculo_Id);
                        history.MdiParent = this.MdiParent;
                        history.Show();
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
                MessageBox.Show(LanguageService.GetErrorText(ee));
            }
            finally
            {
                ToggleActionButtonsStates(true);
                PerformCleanFormAction();
            }
        }

    }
}
