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

namespace CarAgency.UI
{
    public partial class PermissionManagementForm : MetroFramework.Forms.MetroForm
    {
        PermissionBLL _permissionBLL;
        Family selection;
        public PermissionManagementForm()
        {
            InitializeComponent();
            _permissionBLL = new PermissionBLL();
            this.comboNewPatent.DataSource = _permissionBLL.GetAllPermissionTypes();
            this.comboNewPatent.SelectedItem = -1;
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
            this.comboFamily.DataSource = _permissionBLL.GetAllFamilies();
            this.comboFamily.SelectedIndex = -1;
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
                MessageBox.Show("Patent saved!");
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
                MessageBox.Show("Family saved!");
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
                        MessageBox.Show("The selected patent already exists in the current Family!");
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
                        MessageBox.Show("The selected Family already exists in the current Family!");
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
                MessageBox.Show("Family saved!");
            }
            catch (Exception)
            {

                MessageBox.Show("Error saving family");
            }

        }
        #endregion
    }
}
