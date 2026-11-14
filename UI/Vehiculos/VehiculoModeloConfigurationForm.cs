using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using BE;
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
using Security.Session;
using UI.Ayuda;

namespace UI.Vehiculos
{
    public partial class VehiculoModeloConfigurationForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehiculoBLL _VehicleBLL;
        public VehiculoModeloConfigurationForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _VehicleBLL = new VehiculoBLL();
            PerformUpdateMakeCombos();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void VehicleModelConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("VehiculoModeloConfigurationForm");
            this.Refresh();
            tbNewMake.WaterMark = LanguageService.GetTagText(tbNewMake.Tag.ToString());
            tbNewModel.WaterMark = LanguageService.GetTagText(tbNewModel.Tag.ToString());
            tbNewVersion.WaterMark = LanguageService.GetTagText(tbNewVersion.Tag.ToString());
            lblMakes.Text = LanguageService.GetTagText(lblMakes.Tag.ToString());
            lblNewMake.Text = LanguageService.GetTagText(lblNewMake.Tag.ToString());
            btnAddNewMake.Text = LanguageService.GetTagText(btnAddNewMake.Tag.ToString());
            lblDeleteMake.Text = LanguageService.GetTagText(lblDeleteMake.Tag.ToString());
            btnDeleteMake.Text = LanguageService.GetTagText(btnDeleteMake.Tag.ToString());
            lblModels.Text = LanguageService.GetTagText(lblModels.Tag.ToString());
            lblNewModel.Text = LanguageService.GetTagText(lblNewModel.Tag.ToString());
            btnAddNewModel.Text = LanguageService.GetTagText(btnAddNewModel.Tag.ToString());
            lblDeleteModel.Text = LanguageService.GetTagText(lblDeleteModel.Tag.ToString());
            btnDeleteModel.Text = LanguageService.GetTagText(btnDeleteModel.Tag.ToString());
            comboDeleteVersion.Text = LanguageService.GetTagText(comboDeleteVersion.Tag.ToString());
            lblNewVersion.Text = LanguageService.GetTagText(lblNewVersion.Tag.ToString());
            btnAddNewVersion.Text = LanguageService.GetTagText(btnAddNewVersion.Tag.ToString());
            lblDeleteVersion.Text = LanguageService.GetTagText(lblDeleteVersion.Tag.ToString());
            btnDeleteVersion.Text = LanguageService.GetTagText(btnDeleteVersion.Tag.ToString());
        }
        private void PerformUpdateMakeCombos()
        {
            List<Marca> makes1 = _VehicleBLL.GetAllMakes();
            comboDeleteMake.DataSource = makes1;
            comboDeleteMake.DisplayMember = "Descripcion";
            comboDeleteMake.SelectedIndex = -1;
            List<Marca> makes2 = _VehicleBLL.GetAllMakes();
            comboDeleteModelMake.DataSource = makes2;
            comboDeleteModelMake.DisplayMember = "Descripcion";
            comboDeleteModelMake.SelectedIndex = -1;
            List<Marca> makes3 = _VehicleBLL.GetAllMakes();
            comboDeleteVersionMake.DataSource = makes3;
            comboDeleteVersionMake.DisplayMember = "Descripcion";
            comboDeleteVersionMake.SelectedIndex = -1;
            List<Marca> makes4 = _VehicleBLL.GetAllMakes();
            comboNewModelMake.DataSource = makes4;
            comboNewModelMake.DisplayMember = "Descripcion";
            comboNewModelMake.SelectedIndex = -1;
            List<Marca> makes5 = _VehicleBLL.GetAllMakes();
            comboNewVersionMake.DataSource = makes5;
            comboNewVersionMake.DisplayMember = "Descripcion";
            comboNewVersionMake.SelectedIndex = -1;
        }
        private void PerformUpdateModelCombos()
        {
            if (comboDeleteModelMake.SelectedIndex != -1 && ((Marca)comboDeleteModelMake.SelectedValue) != null) 
            {
                List<Modelo> models1 = _VehicleBLL.GetAllModelsByMake((Marca)comboDeleteModelMake.SelectedValue);
                comboDeleteModel.DataSource = models1;
                comboDeleteMake.DisplayMember = "Descripcion";
                comboDeleteMake.SelectedIndex = -1;
            }
            if (comboNewVersionMake.SelectedIndex != -1 && ((Marca)comboNewVersionMake.SelectedValue) != null) 
            {
                List<Modelo> models2 = _VehicleBLL.GetAllModelsByMake((Marca)comboNewVersionMake.SelectedValue);
                comboNewVersionModel.DataSource = models2;
                comboNewVersionModel.DisplayMember = "Descripcion";
                comboNewVersionModel.SelectedIndex = -1;
            }
            if (comboDeleteVersionMake.SelectedIndex != -1 && ((Marca)comboDeleteVersionMake.SelectedValue) != null) 
            {
                List<Modelo> models3 = _VehicleBLL.GetAllModelsByMake((Marca)comboDeleteVersionMake.SelectedValue);
                comboDeleteVersionModel.DataSource = models3;
                comboDeleteVersionModel.DisplayMember = "Descripcion";
                comboDeleteVersionModel.SelectedIndex = -1;
            }
        }
        private void PerformUpdateVersionCombo()
        {
            if (comboDeleteVersionModel.SelectedIndex != -1 && comboDeleteVersionMake.SelectedIndex != -1 && ((Marca)comboDeleteVersionMake.SelectedValue) != null && ((Modelo)comboDeleteVersionModel.SelectedValue) != null) 
            {
                List<CarAgency.BE.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Marca)comboDeleteVersionMake.SelectedValue,(Modelo)comboDeleteVersionModel.SelectedValue);
                comboDeleteVersions.DataSource = versions;
                comboDeleteVersions.DisplayMember = "Descripcion";
                comboDeleteVersions.SelectedIndex = -1;
            }
        }

        #endregion

        private void btnAddNewMake_Click(object sender, EventArgs e)
        {
            if (tbNewMake.Text != "")
            {
                Marca make = new Marca();
                make.Descripcion= tbNewMake.Text;
                 _VehicleBLL.AddMake(make);
                PerformUpdateMakeCombos();
                tbNewMake.Text = "";
            }
        }

        private void btnDeleteMake_Click(object sender, EventArgs e)
        {
            if (comboDeleteMake.SelectedIndex != -1)
            {
                _VehicleBLL.DeleteMake((Marca)comboDeleteMake.SelectedValue);
                PerformUpdateMakeCombos();
            }
        }

        private void btnAddNewModel_Click(object sender, EventArgs e)
        {
            if (tbNewModel.Text != "" && comboNewModelMake.SelectedIndex != -1)
            {
                Modelo model = new Modelo();
                model.Marca_Id = ((Marca)comboNewModelMake.SelectedValue).Marca_Id;
                model.Descripcion = tbNewModel.Text;
                _VehicleBLL.AddModel(model);
                comboNewModelMake.SelectedIndex = -1;
                tbNewModel.Text = "";
            }
        }

        private void btnDeleteModel_Click(object sender, EventArgs e)
        {
            if (comboDeleteModelMake.SelectedIndex != -1 && comboDeleteModel.SelectedIndex != -1)
            {
                _VehicleBLL.DeleteModel((Modelo)comboDeleteModel.SelectedValue);
                comboDeleteModelMake.SelectedIndex = -1;
                comboDeleteModel.SelectedIndex = -1;
            }
        }

        private void btnAddNewVersion_Click(object sender, EventArgs e)
        {
            if (comboNewVersionMake.SelectedIndex != -1 && comboNewVersionModel.SelectedIndex != -1  && tbNewVersion.Text != "")
            {
                CarAgency.BE.Version version = new CarAgency.BE.Version();
                version.Marca_Id = ((Marca)comboNewVersionMake.SelectedValue).Marca_Id;
                version.Modelo_Id = ((Modelo)comboNewVersionModel.SelectedValue).Modelo_Id;
                version.Descripcion = tbNewVersion.Text;
                _VehicleBLL.AddVersion(version);
                comboNewVersionMake.SelectedIndex = -1;
                comboNewVersionModel.SelectedIndex = -1;
                tbNewVersion.Text = "";
            }
        }

        private void btnDeleteVersion_Click(object sender, EventArgs e)
        {
            if (comboDeleteVersionMake.SelectedIndex != -1 && comboDeleteVersionModel.SelectedIndex != -1 && comboDeleteVersions.SelectedIndex != -1)
            {
                _VehicleBLL.DeleteVersion((CarAgency.BE.Version)comboDeleteVersions.SelectedValue);
                comboDeleteVersionMake.SelectedIndex = -1;
                comboDeleteVersionModel.SelectedIndex = -1;
                comboDeleteVersions.SelectedIndex = -1;
            }
        }

        private void comboDeleteVersionMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
        }

        private void comboDeleteVersionModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateVersionCombo();
        }

        private void comboNewVersionMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
        }

        private void comboNewVersionModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateVersionCombo();
        }

        private void comboDeleteModelMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
        }

    }
}
