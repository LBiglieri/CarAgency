using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;
using MetroFramework.Controls;
using Security.Session;

namespace CarAgency.UI
{
    public partial class PermisoManagementForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        PermisoBLL _permissionBLL;
        Familia selection;
        public PermisoManagementForm()
        {
            InitializeComponent();
            _permissionBLL = new PermisoBLL();
            this.comboNewPatent.DataSource = _permissionBLL.GetAllPermissionTypes();
            this.comboNewPatent.SelectedItem = -1;
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void PermissionManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("PermisoManagementForm");
            this.Refresh();
            tbNewPatent.WaterMark = LanguageService.GetTagText(tbNewPatent.Tag.ToString());
            tbNewFamily.WaterMark = LanguageService.GetTagText(tbNewFamily.Tag.ToString());
            btnAddPatentToFamily.Text = LanguageService.GetTagText(btnAddPatentToFamily.Tag.ToString());
            lblNewPatent.Text = LanguageService.GetTagText(lblNewPatent.Tag.ToString());
            btnAddNewPatent.Text = LanguageService.GetTagText(btnAddNewPatent.Tag.ToString());
            lblDeletePatent.Text = LanguageService.GetTagText(lblDeletePatent.Tag.ToString());
            btnDeletePatent.Text = LanguageService.GetTagText(btnDeletePatent.Tag.ToString());
            lblFamilies.Text = LanguageService.GetTagText(lblFamilies.Tag.ToString());
            btnConfigureFamily.Text = LanguageService.GetTagText(btnConfigureFamily.Tag.ToString());
            btnAddToFamilyToFamily.Text = LanguageService.GetTagText(btnAddToFamilyToFamily.Tag.ToString());
            lblNewFamily.Text = LanguageService.GetTagText(lblNewFamily.Tag.ToString());
            btnAddNewFamily.Text = LanguageService.GetTagText(btnAddNewFamily.Tag.ToString());
            lblDeleteFamily.Text = LanguageService.GetTagText(lblDeleteFamily.Tag.ToString());
            btnDeleteFamily.Text = LanguageService.GetTagText(btnDeleteFamily.Tag.ToString());
            lblConfigureFamilies.Text = LanguageService.GetTagText(lblConfigureFamilies.Tag.ToString());
            btnSaveFamily.Text = LanguageService.GetTagText(btnSaveFamily.Tag.ToString());
            lblPatents.Text = LanguageService.GetTagText(lblPatents.Tag.ToString());
        }
        private void PermissionManagementForm_Load(object sender, EventArgs e)
        {
            FillPatentsFamilies();
        }

        #region  Perform
        private void FillPatentsFamilies()
        {
            this.comboPatente.DataSource = _permissionBLL.GetAllPatents();
            this.comboPatente.SelectedIndex = -1;
            this.comboDeletePatent.DataSource = _permissionBLL.GetAllPatents();
            this.comboDeletePatent.SelectedIndex = -1;
            this.comboFamily.DataSource = _permissionBLL.GetAllFamilies();
            this.comboFamily.SelectedIndex = -1;
            this.comboDeleteFamily.DataSource = _permissionBLL.GetAllFamilies();
            this.comboDeleteFamily.SelectedIndex = -1;
        }
        void ShowFamily(bool init)
        {
            if (selection == null) return;


            IList<ComposedPermiso> family = null;
            if (init)
            {
                family = _permissionBLL.GetAll(selection.Permiso_Id);

                if (family != null)
                {
                    foreach (var i in family)
                        selection.AddPermission(i);
                }
            }
            else
            {
                family = selection.Children;
            }

            this.treeFamily.Nodes.Clear();

            TreeNode root = new TreeNode(selection.Nombre);
            root.Tag = selection;
            this.treeFamily.Nodes.Add(root);

            if (family != null) 
            { 
                foreach (var item in family)
                {
                    ShowOnTreeView(root, item);
                }
            }

            treeFamily.ExpandAll();
        }


        void ShowOnTreeView(TreeNode tn, ComposedPermiso c)
        {
            TreeNode n = new TreeNode(c.Nombre);
            tn.Tag = c;
            tn.Nodes.Add(n);
            if (c.Children != null)
                foreach (var item in c.Children)
                {
                    ShowOnTreeView(n, item);
                }

        }


        #endregion

        #region  Form Events 
        private void btnAddNewPatent_Click(object sender, EventArgs e)
        {   
            if (this.tbNewPatent.Text != "")
            {
                Patente p = new Patente()
                {
                    Permiso_Id = Guid.NewGuid(),
                    Nombre = this.tbNewPatent.Text,
                    Tipo = (PermisoType)this.comboNewPatent.SelectedItem

                };

                _permissionBLL.InsertComposedPermission(p, false);
                FillPatentsFamilies();
                this.tbNewPatent.Text = "";
                this.comboNewPatent.SelectedItem = -1;
                MessageBox.Show(LanguageService.GetTagText("PatentSaved"));
            }
        }

        private void btnAddNewFamily_Click(object sender, EventArgs e)
        {
            if (this.tbNewFamily.Text != "")
            {
                Familia p = new Familia()
                {
                    Permiso_Id = Guid.NewGuid(),
                    Nombre = this.tbNewFamily.Text
                };

                _permissionBLL.InsertComposedPermission(p, true);
                FillPatentsFamilies();
                this.tbNewFamily.Text = "";
                MessageBox.Show(LanguageService.GetTagText("FamilySaved"));
            }
        }

        private void btnAddPatentToFamily_Click(object sender, EventArgs e)
        {
            if (selection != null)
            {
                var patent = (Patente)comboPatente.SelectedItem;
                if (patent != null)
                {
                    var esta = _permissionBLL.Exists(selection, patent.Permiso_Id);
                    if (esta)
                        MessageBox.Show(LanguageService.GetTagText("PatentAlreadyExistsInCurrentFamily"));
                    else
                    {

                        {
                            selection.AddPermission(patent);
                            ShowFamily(false);
                        }
                    }
                }
            }
        }

        private void btnConfigureFamily_Click(object sender, EventArgs e)
        {
            var tmp = (Familia)this.comboFamily.SelectedItem;
            selection = new Familia();
            selection.Permiso_Id = tmp.Permiso_Id;
            selection.Nombre = tmp.Nombre;

            ShowFamily(true);
        }

        private void btnAddToFamilyToFamily_Click(object sender, EventArgs e)
        {
            if (selection != null)
            {
                var familia = (Familia)comboFamily.SelectedItem;
                if (familia != null)
                {

                    var esta = _permissionBLL.Exists(selection, familia.Permiso_Id);
                    if (esta)
                        MessageBox.Show(LanguageService.GetTagText("FamilyAlreadyExistsInCurrentFamily"));
                    else
                    {

                        _permissionBLL.FillFamilyComponents(familia);
                        selection.AddPermission(familia);
                        ShowFamily(false);
                    }


                }
            }
        }

        private void btnSaveFamily_Click(object sender, EventArgs e)
        {
            try
            {
                _permissionBLL.SaveFamily(selection);
                MessageBox.Show(LanguageService.GetTagText("FamilySaved"));
            }
            catch (Exception)
            {

                MessageBox.Show(LanguageService.GetTagText("ErrorSavingFamily"));
            }

        }

        private void btnDeletePatent_Click(object sender, EventArgs e)
        {
            if (this.comboDeletePatent.SelectedIndex != -1)
            {
                Patente patent = (Patente)this.comboDeletePatent.SelectedItem;
                // Borrar no tiene vuelta atras: la patente sale de todas las familias que la incluyen.
                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureDeletePatent"), patent.Nombre),
                        LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                _permissionBLL.DeletePatent(patent);
                FillPatentsFamilies();
                if (selection != null)
                    ShowFamily(true);
                MessageBox.Show(LanguageService.GetTagText("PatentDeleted"));
            }
        }

        private void btnDeleteFamily_Click(object sender, EventArgs e)
        {
            if (this.comboDeleteFamily.SelectedIndex != -1)
            {
                if (((ComposedPermiso)this.comboDeleteFamily.SelectedItem).Nombre == "Base User")
                {
                    MessageBox.Show(LanguageService.GetTagText("CantDeleteBaseUser"));
                    return;
                }
                ComposedPermiso family = (ComposedPermiso)this.comboDeleteFamily.SelectedItem;
                // Los usuarios con esa familia como rol pasan a Base User (Permiso_DeleteCompleteFamily).
                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureDeleteFamily"), family.Nombre),
                        LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                _permissionBLL.DeleteFamily(family);
                FillPatentsFamilies();
                if (selection != null)
                    ShowFamily(true);
                MessageBox.Show(LanguageService.GetTagText("FamilyDeleted"));
            }
        }
        #endregion

    }
}
