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
using CarAgency.Entities;
using Entities;
using MetroFramework.Controls;
using Utilities.Session;

namespace CarAgency.UI
{
    public partial class PermissionManagementForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        PermissionBLL _permissionBLL;
        Family selection;
        public PermissionManagementForm()
        {
            InitializeComponent();
            _permissionBLL = new PermissionBLL();
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
            this.Text = LanguageService.GetTagText("PermissionManagementForm");
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


            IList<ComposedPermission> family = null;
            if (init)
            {
                family = _permissionBLL.GetAll(selection.Id);

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

            TreeNode root = new TreeNode(selection.Name);
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


        void ShowOnTreeView(TreeNode tn, ComposedPermission c)
        {
            TreeNode n = new TreeNode(c.Name);
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
                Patent p = new Patent()
                {
                    Id = Guid.NewGuid(),
                    Name = this.tbNewPatent.Text,
                    Type = (PermissionType)this.comboNewPatent.SelectedItem

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
                Family p = new Family()
                {
                    Id = Guid.NewGuid(),
                    Name = this.tbNewFamily.Text
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
                var patent = (Patent)comboPatente.SelectedItem;
                if (patent != null)
                {
                    var esta = _permissionBLL.Exists(selection, patent.Id);
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
            var tmp = (Family)this.comboFamily.SelectedItem;
            selection = new Family();
            selection.Id = tmp.Id;
            selection.Name = tmp.Name;

            ShowFamily(true);
        }

        private void btnAddToFamilyToFamily_Click(object sender, EventArgs e)
        {
            if (selection != null)
            {
                var familia = (Family)comboFamily.SelectedItem;
                if (familia != null)
                {

                    var esta = _permissionBLL.Exists(selection, familia.Id);
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
                _permissionBLL.DeletePatent((Patent)this.comboDeletePatent.SelectedItem);
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
                if (((ComposedPermission)this.comboDeleteFamily.SelectedItem).Name == "Base User")
                {
                    MessageBox.Show(LanguageService.GetTagText("CantDeleteBaseUser"));
                    return;
                }
                _permissionBLL.DeleteFamily((ComposedPermission)this.comboDeleteFamily.SelectedItem);
                FillPatentsFamilies();
                if (selection != null)
                    ShowFamily(true);
                MessageBox.Show(LanguageService.GetTagText("FamilyDeleted"));
            }
        }
        #endregion

    }
}
