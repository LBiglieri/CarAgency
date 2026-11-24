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
using Color = CarAgency.BE.Color;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Personas.Controls;
using Security.Session;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{
    public partial class RegisterCompraForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehiculoBLL _VehicleBLL;
        CompraBLL _AcquisitionBLL;
        InspeccionBLL _InspectionBLL;

        public RegisterCompraForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _VehicleBLL = new VehiculoBLL();
            _AcquisitionBLL = new CompraBLL();
            _InspectionBLL = new InspeccionBLL();

            personView1.personViewMode = PersonaViewMode.WithRegistration;
            this.personView1.PersonFound += new EventHandler(personView1_PersonFound);

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
            this.Text = LanguageService.GetTagText("RegisterCompraForm");
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
            if (comboColour.DataSource != null)
                PerformUpdateColourCombos();
        }
        // Los colores estan en ingles en la base: se muestran con su traduccion y se conserva el elegido.
        // La primera vez viene elegido el color mas comun.
        private void PerformUpdateColourCombos()
        {
            try
            {
                Guid? selected = comboColour.SelectedIndex == -1 ? (Guid?)null : ((KeyValuePair<Color, string>)comboColour.SelectedItem).Key.Color_Id;
                List<KeyValuePair<Color, string>> colours = _VehicleBLL.GetAllColours()
                    .Select(c => new KeyValuePair<Color, string>(c, LookupText.Colour(c.Descripcion))).ToList();
                comboColour.DataSource = colours;
                comboColour.DisplayMember = "Value";
                comboColour.ValueMember = "Key";
                comboColour.SelectedIndex = selected == null
                    ? colours.FindIndex(c => c.Key.Descripcion == VehiculoBLL.DefaultColour)
                    : colours.FindIndex(c => c.Key.Color_Id == selected.Value);
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
                List<Marca> makes = _VehicleBLL.GetAllMakes();
                comboMake.DataSource = makes;
                comboMake.DisplayMember = "Descripcion";
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
                if (comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedValue) != null)
                {
                    List<Modelo> models = _VehicleBLL.GetAllModelsByMake((Marca)comboMake.SelectedValue);
                    comboModel.DataSource = models;
                    comboModel.DisplayMember = "Descripcion";
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
                if (comboModel.SelectedIndex != -1 && comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedValue) != null && ((Modelo)comboModel.SelectedValue) != null)
                {
                    List<CarAgency.BE.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Marca)comboMake.SelectedValue, (Modelo)comboModel.SelectedValue);
                    comboVersions.DataSource = versions;
                    comboVersions.DisplayMember = "Descripcion";
                    comboVersions.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // Con un solo mecanico no hay nada que elegir: viene seleccionado.
        private void PerformUpdateMechanicCombo()
        {
            try
            {
                List<Usuario> mechanics = _InspectionBLL.GetMechanics();
                comboMechanic.DataSource = mechanics;
                comboMechanic.DisplayMember = "NombreUsuario";
                comboMechanic.SelectedIndex = mechanics.Count == 1 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // Viene elegido el primer turno que todavia no paso, con su fecha.
        private void PerformUpdateHourCombo()
        {
            comboHour.DataSource = _InspectionBLL.GetInspectionHours().Select(h => h.ToString(@"hh\:mm")).ToList();
            MapSlotToControls(_InspectionBLL.GetFirstInspectionSlot(DateTime.Now));
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
        private VehiculoProspecto MapControlsToProspectVehicle()
        {
            VehiculoProspecto vehicle = new VehiculoProspecto();
            vehicle.Patente = tbLicense_Plate.Text;
            vehicle.NumeroChasis = tbChassis_Number.Text;
            vehicle.NumeroMotor = tbEngine_Number.Text;
            vehicle.Marca_Id = ((Marca)comboMake.SelectedItem).Marca_Id;
            vehicle.Modelo_Id = ((Modelo)comboModel.SelectedItem).Modelo_Id;
            vehicle.Version_Id = ((CarAgency.BE.Version)comboVersions.SelectedItem).Version_Id;
            vehicle.Color_Id = ((KeyValuePair<Color, string>)comboColour.SelectedItem).Key.Color_Id;
            vehicle.Anio = int.Parse(tbYear.Text);
            vehicle.Puertas = int.Parse(tbDoors.Text);
            vehicle.Kilometraje = int.Parse(tbDeclared_Kilometers.Text);
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
        private bool PerformValidateDuplicates(VehiculoProspecto vehicle)
        {
            List<DuplicateVehiculo> duplicates = _AcquisitionBLL.GetDuplicates(vehicle);
            if (duplicates == null)
                return true;

            string message = LanguageService.GetTagText("VehicleAlreadyRegistered") + Environment.NewLine;
            foreach (DuplicateVehiculo duplicate in duplicates)
            {
                message += Environment.NewLine;
                if (duplicate.Numero == 0)
                    message += LanguageService.GetTagText("DuplicateInInventory") + duplicate.Vehiculo_Descripcion;
                else
                    message += LanguageService.GetTagText("DuplicateInAcquisition") + duplicate.Numero + ": " + duplicate.Vehiculo_Descripcion
                        + " - " + LanguageService.GetTagText("Chassis") + duplicate.NumeroChasis
                        + " - " + LanguageService.GetTagText("Engine") + duplicate.NumeroMotor;
            }
            MessageBox.Show(message, LanguageService.GetTagText("AttentionTitle"));
            return false;
        }
        // CUF07 6.1: si el mecanico esta ocupado se propone el proximo horario libre y se vuelve a elegir.
        private bool PerformValidateMechanicAvailability(Usuario mechanic, DateTime scheduledDate)
        {
            if (_InspectionBLL.IsMechanicAvailable(mechanic.Usuario_Id, scheduledDate))
                return true;

            DateTime nextSlot = _InspectionBLL.GetNextFreeSlot(mechanic.Usuario_Id, scheduledDate);
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

                VehiculoProspecto vehicle = MapControlsToProspectVehicle();
                if (!PerformValidateDuplicates(vehicle))
                    return;

                Usuario mechanic = (Usuario)comboMechanic.SelectedItem;
                DateTime scheduledDate = GetScheduledDate();
                if (!PerformValidateMechanicAvailability(mechanic, scheduledDate))
                    return;

                if (MessageBox.Show(LanguageService.GetTagText("AreYouSureRegisterAcquisition") + vehicle.Patente + "?", LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                Compra acquisition = new Compra();
                acquisition.Persona_Id = personView1.person.Persona_Id;
                acquisition.VehiculoProspecto = vehicle;
                acquisition.Inspeccion = new Inspeccion();
                acquisition.Inspeccion.Mecanico_Id = mechanic.Usuario_Id;
                acquisition.Inspeccion.FechaProgramada = scheduledDate;

                SQLUpdateResult result = _AcquisitionBLL.AddAcquisition(acquisition);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(LanguageService.GetTagText("AcquisitionRegistered") + acquisition.Numero);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        public void personView1_PersonFound(object sender, EventArgs e)
        {
            if (personView1.person != null)
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
