using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
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
using Utilities.Session;

namespace UI.Vehicles
{
    public partial class VehicleModelConfigurationForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehicleBLL _VehicleBLL;
        public VehicleModelConfigurationForm()
        {
            InitializeComponent();
            _VehicleBLL = new VehicleBLL();
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
            this.Text = LanguageService.GetTagText("VehicleModelConfigurationForm");
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
            List<Make> makes1 = _VehicleBLL.GetAllMakes();
            comboDeleteMake.DataSource = makes1;
            comboDeleteMake.DisplayMember = "Description";
            comboDeleteMake.SelectedIndex = -1;
            List<Make> makes2 = _VehicleBLL.GetAllMakes();
            comboDeleteModelMake.DataSource = makes2;
            comboDeleteModelMake.DisplayMember = "Description";
            comboDeleteModelMake.SelectedIndex = -1;
            List<Make> makes3 = _VehicleBLL.GetAllMakes();
            comboDeleteVersionMake.DataSource = makes3;
            comboDeleteVersionMake.DisplayMember = "Description";
            comboDeleteVersionMake.SelectedIndex = -1;
            List<Make> makes4 = _VehicleBLL.GetAllMakes();
            comboNewModelMake.DataSource = makes4;
            comboNewModelMake.DisplayMember = "Description";
            comboNewModelMake.SelectedIndex = -1;
            List<Make> makes5 = _VehicleBLL.GetAllMakes();
            comboNewVersionMake.DataSource = makes5;
            comboNewVersionMake.DisplayMember = "Description";
            comboNewVersionMake.SelectedIndex = -1;
        }
        private void PerformUpdateModelCombos()
        {
            if (comboDeleteModelMake.SelectedIndex != -1 && ((Make)comboDeleteModelMake.SelectedValue) != null) 
            {
                List<Model> models1 = _VehicleBLL.GetAllModelsByMake((Make)comboDeleteModelMake.SelectedValue);
                comboDeleteModel.DataSource = models1;
                comboDeleteMake.DisplayMember = "Description";
                comboDeleteMake.SelectedIndex = -1;
            }
            if (comboNewVersionMake.SelectedIndex != -1 && ((Make)comboNewVersionMake.SelectedValue) != null) 
            {
                List<Model> models2 = _VehicleBLL.GetAllModelsByMake((Make)comboNewVersionMake.SelectedValue);
                comboNewVersionModel.DataSource = models2;
                comboNewVersionModel.DisplayMember = "Description";
                comboNewVersionModel.SelectedIndex = -1;
            }
            if (comboDeleteVersionMake.SelectedIndex != -1 && ((Make)comboDeleteVersionMake.SelectedValue) != null) 
            {
                List<Model> models3 = _VehicleBLL.GetAllModelsByMake((Make)comboDeleteVersionMake.SelectedValue);
                comboDeleteVersionModel.DataSource = models3;
                comboDeleteVersionModel.DisplayMember = "Description";
                comboDeleteVersionModel.SelectedIndex = -1;
            }
        }
        private void PerformUpdateVersionCombo()
        {
            if (comboDeleteVersionModel.SelectedIndex != -1 && comboDeleteVersionMake.SelectedIndex != -1 && ((Make)comboDeleteVersionMake.SelectedValue) != null && ((Model)comboDeleteVersionModel.SelectedValue) != null) 
            {
                List<CarAgency.Entities.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Make)comboDeleteVersionMake.SelectedValue,(Model)comboDeleteVersionModel.SelectedValue);
                comboDeleteVersions.DataSource = versions;
                comboDeleteVersions.DisplayMember = "Description";
                comboDeleteVersions.SelectedIndex = -1;
            }
        }

        #endregion

        private void btnAddNewMake_Click(object sender, EventArgs e)
        {
            if (tbNewMake.Text != "")
            {
                Make make = new Make();
                make.Description= tbNewMake.Text;
                 _VehicleBLL.AddMake(make);
                PerformUpdateMakeCombos();
                tbNewMake.Text = "";
            }
        }

        private void btnDeleteMake_Click(object sender, EventArgs e)
        {
            if (comboDeleteMake.SelectedIndex != -1)
            {
                _VehicleBLL.DeleteMake((Make)comboDeleteMake.SelectedValue);
                PerformUpdateMakeCombos();
            }
        }

        private void btnAddNewModel_Click(object sender, EventArgs e)
        {
            if (tbNewModel.Text != "" && comboNewModelMake.SelectedIndex != -1)
            {
                Model model = new Model();
                model.Make_Id = ((Make)comboNewModelMake.SelectedValue).Id;
                model.Description = tbNewModel.Text;
                _VehicleBLL.AddModel(model);
                comboNewModelMake.SelectedIndex = -1;
                tbNewModel.Text = "";
            }
        }

        private void btnDeleteModel_Click(object sender, EventArgs e)
        {
            if (comboDeleteModelMake.SelectedIndex != -1 && comboDeleteModel.SelectedIndex != -1)
            {
                _VehicleBLL.DeleteModel((Model)comboDeleteModel.SelectedValue);
                comboDeleteModelMake.SelectedIndex = -1;
                comboDeleteModel.SelectedIndex = -1;
            }
        }

        private void btnAddNewVersion_Click(object sender, EventArgs e)
        {
            if (comboNewVersionMake.SelectedIndex != -1 && comboNewVersionModel.SelectedIndex != -1  && tbNewVersion.Text != "")
            {
                CarAgency.Entities.Version version = new CarAgency.Entities.Version();
                version.Make_Id = ((Make)comboNewVersionMake.SelectedValue).Id;
                version.Model_Id = ((Model)comboNewVersionModel.SelectedValue).Id;
                version.Description = tbNewVersion.Text;
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
                _VehicleBLL.DeleteVersion((CarAgency.Entities.Version)comboDeleteVersions.SelectedValue);
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
