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
using CarAgency.Utilities.Persistence;

namespace CarAgency.UI
{
    public enum UserManagementFormAction
    {
        Add,
        Update,
        Delete,
        Unblock,
        NoAction
    }
    public partial class UserManagementForm : MetroFramework.Forms.MetroForm
    {
        UserBLL _userBLL;
        List<User> users = new List<User>();
        User selected_user;

        UserManagementFormAction Form_Action;

        public UserManagementForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();

            btnCancelOpp_Click(null, null);
        }

        #region  Perform
        void PerformUpdateUsersView()
        {
            users = _userBLL.GetAllByState(toggleActivos.Checked);
            metroGrid1.DataSource = null;
            metroGrid1.DataSource = users;
            metroGrid1.Columns["Password"].Visible = false;
            metroGrid1.Columns["Permissions"].Visible = false;
            metroGrid1.Columns["Id"].Visible = false;
            metroGrid1.Columns["Available_Login_Attempts"].Visible = false;
        }
        void PerformEnableDisableTextBoxes(Boolean flag)
        {
            tbDniUser.Enabled = flag;
            tbUsername.Enabled = flag;
            tbName.Enabled = flag;
            tbSurname.Enabled = flag;
            tbRole.Enabled = flag;
        }
        void PerformClearTextBoxData()
        {
            tbDniUser.Text = "";
            tbUsername.Text = "";
            tbName.Text = "";
            tbSurname.Text = "";
            tbRole.Text = "";
            chkBlocked.Checked = false;
            chkActive.Checked = false;
        }
        void PerformPopulateTextBoxData()
        {
            tbDniUser.Text = selected_user.Dni.ToString();
            tbUsername.Text = selected_user.Username;
            tbName.Text = selected_user.Name;
            tbSurname.Text = selected_user.Surname;
            tbRole.Text = selected_user.Role; 
            chkBlocked.Checked = selected_user.Blocked;
            chkActive.Checked = selected_user.Active;
        }
        void PerformCleanFormAction()
        {
            selected_user = null;
            metroGrid1.Enabled = false;
            PerformEnableDisableTextBoxes(false);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.NoAction;
            PerformUpdateUsersView();
        }
        Boolean PerformValidateTextBoxData()
        {
            //falta validar los datos
            return true;
        }

        private void MapTextboxesToUser()
        {
            selected_user.Dni = int.Parse(tbDniUser.Text);
            selected_user.Username = tbUsername.Text;
            selected_user.Name = tbName.Text;
            selected_user.Surname = tbSurname.Text;
            selected_user.Role = tbRole.Text;
        }
        #endregion

        #region  Form Events 
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = false;

            PerformEnableDisableTextBoxes(true);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Add;
        }
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(true);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Update;

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Delete;
        }
        private void btnUnblockUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Unblock;
        }

        private void toggleActivos_CheckedChanged(object sender, EventArgs e)
        {
            PerformUpdateUsersView();
        }

        private void btnApplyOpp_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            switch (Form_Action)
            {
                case UserManagementFormAction.Add:
                    PerformValidateTextBoxData();
                    selected_user = new User();
                    MapTextboxesToUser();
                    result = _userBLL.AddUser(selected_user);
                    break;
                case UserManagementFormAction.Update:
                    PerformValidateTextBoxData();
                    MapTextboxesToUser(); 
                    if (MessageBox.Show("Are you sure you want to update the user " + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        result = _userBLL.UpdateUser(selected_user);
                    }
                    break;
                case UserManagementFormAction.Delete:
                    if (MessageBox.Show("Are you sure you want to delete the user " + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        result = _userBLL.DeleteUser(selected_user);
                    }
                    break;
                case UserManagementFormAction.Unblock:
                    if (MessageBox.Show("Are you sure you want to unblock the user " + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        result = _userBLL.AlterBlockedState(selected_user,false);
                    }
                    break;
            }

            if (result != null)
            {
                MessageBox.Show(result.message);
            }
            PerformCleanFormAction();
        }

        private void btnCancelOpp_Click(object sender, EventArgs e)
        {
            PerformCleanFormAction();
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (Form_Action == UserManagementFormAction.Update || Form_Action == UserManagementFormAction.Delete || Form_Action == UserManagementFormAction.Unblock) 
            {
                selected_user = (User)metroGrid1.CurrentRow.DataBoundItem;
                PerformPopulateTextBoxData();
            }
        }
        #endregion

    }
}
