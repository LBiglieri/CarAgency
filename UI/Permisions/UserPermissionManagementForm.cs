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
using CarAgency.Utilities.Permissions;

namespace CarAgency.UI
{
    public partial class UserPermissionManagementForm : MetroFramework.Forms.MetroForm
    {
        PermissionBLL _permissionBLL;
        UserBLL _userBLL;
        User selection;
        User tmp;
        public UserPermissionManagementForm()
        {
            InitializeComponent();
            _permissionBLL = new PermissionBLL();
            _userBLL = new UserBLL();
            this.comboUsers.DataSource = _userBLL.GetAll();
            this.comboUsers.DisplayMember = "Username";
            this.comboFamily.DataSource = _permissionBLL.GetAllFamilies();
            this.comboPatent.DataSource = _permissionBLL.GetAllPatents();
            this.comboUsers.SelectedIndex = -1;
            this.comboFamily.SelectedIndex = -1;
            this.comboPatent.SelectedIndex = -1;
        }

        #region  Perform
        void ShowPermissions(User u)
        {
            this.treeUser.Nodes.Clear();
            TreeNode root = new TreeNode(u.Username);

            foreach (var item in u.Permissions)
            {
                ShowOnTreeView(root, item);
            }

            this.treeUser.Nodes.Add(root);
            this.treeUser.ExpandAll();
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

        #endregion

        private void btnConfigureUser_Click(object sender, EventArgs e)
        {
            selection = (User)this.comboUsers.SelectedItem;

            tmp = new User();
            tmp.Id = selection.Id;
            tmp.Username = selection.Username;
            _permissionBLL.FillUserComponents(tmp);

            ShowPermissions(tmp);
        }

        private void btnAddPatent_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var patent = (Patent)comboPatent.SelectedItem;
                if (patent != null)
                {
                    var exists = false;

                    foreach (var item in tmp.Permissions)
                    {
                        if (_permissionBLL.Exists(item, patent.Id))
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (exists)
                        MessageBox.Show("The user already has the selected patent.");
                    else
                    {
                        {
                            tmp.Permissions.Add(patent);
                            ShowPermissions(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Please select a user and press 'Configure' before adding a Patent.");
        }

        private void btnAddFamily_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var family = (Family)comboFamily.SelectedItem;
                if (family != null)
                {
                    var exists = false;
                    foreach (var item in tmp.Permissions)
                    {
                        if (_permissionBLL.Exists(item, family.Id))
                        {
                            exists = true;
                        }
                    }

                    if (exists)
                        MessageBox.Show("The user already has the selected family.");
                    else
                    {
                        {
                            _permissionBLL.FillFamilyComponents(family);

                            tmp.Permissions.Add(family);
                            ShowPermissions(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Please select a user and press 'Configure' before adding a Family.");
        }

        private void btnSaveFamily_Click(object sender, EventArgs e)
        {
            try
            {
                _userBLL.SavePermissions(tmp);
                MessageBox.Show("User saved succesfully.");
            }
            catch (Exception)
            {

                MessageBox.Show("Error saving the user.");
            }
        }
    }
}
