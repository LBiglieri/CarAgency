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
using CarAgency.Utilities.Persistence;

namespace CarAgency.UI
{
    public enum UserManagementFormAction
    {
        NoAction,
        Add,
        Update,
        Delete,
        Unblock
    }
    public partial class UserManagementForm : MetroFramework.Forms.MetroForm
    {
        UserBLL _userBLL;
        List<User> users = new List<User>();
        User selected_user;
        PermissionBLL _permissionBLL;

        UserManagementFormAction Form_Action;

        public UserManagementForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _permissionBLL = new PermissionBLL();

            PerformFillFamiliesCombo();
            btnCancelOpp_Click(null, null);
        }

        #region  Perform
        void PerformUpdateUsersView()
        {
            try
            {
                users = _userBLL.GetAllByState(toggleActivos.Checked);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = users;
                metroGrid1.Columns["Password"].Visible = false;
                metroGrid1.Columns["Role_Id"].Visible = false;
                metroGrid1.Columns["Role"].Visible = false;
                metroGrid1.Columns["Id"].Visible = false;
                metroGrid1.Columns["Available_Login_Attempts"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void PerformFillFamiliesCombo()
        {
            try
            {
                this.comboFamily.DataSource = _permissionBLL.GetAllFamilies();
                this.comboFamily.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void PerformEnableDisableTextBoxes(Boolean flag)
        {
            tbDniUser.Enabled = flag;
            tbUsername.Enabled = flag;
            tbName.Enabled = flag;
            tbSurname.Enabled = flag;
            comboFamily.Enabled = flag;
        }
        void PerformClearTextBoxData()
        {
            tbDniUser.Text = "";
            tbUsername.Text = "";
            tbName.Text = "";
            tbSurname.Text = "";
            comboFamily.SelectedIndex = -1;
            chkBlocked.Checked = false;
            chkActive.Checked = false;
        }
        void PerformPopulateTextBoxData()
        {
            tbDniUser.Text = selected_user.Dni.ToString();
            tbUsername.Text = selected_user.Username;
            tbName.Text = selected_user.Name;
            tbSurname.Text = selected_user.Surname;
            foreach (Family family in comboFamily.Items)
            {
                if (family.Id.Equals(selected_user.Role_Id))
                {
                    comboFamily.SelectedIndex = comboFamily.FindStringExact(family.Name); 
                }
            }
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
            if (tbDniUser.Text == "" || tbUsername.Text == "" || tbName.Text == "" || tbSurname.Text == "" || comboFamily.SelectedIndex== -1)
            {
                MessageBox.Show("Please fill all the fields in the form to perform the selected action.");
                return false;
            }
            if (!int.TryParse(tbDniUser.Text, out _))
            {
                MessageBox.Show("Please only use numeric digits in the DNI field.");
                return false;
            }

            return true;
        }

        private void MapTextboxesToUser()
        {
            selected_user.Dni = int.Parse(tbDniUser.Text);
            selected_user.Username = tbUsername.Text;
            selected_user.Name = tbName.Text;
            selected_user.Surname = tbSurname.Text;
            selected_user.Role_Id = ((Family)comboFamily.SelectedItem).Id;
        }

        private void ToggleActionButtonsStates(bool state)
        {
            btnAddUser.Enabled = state;
            btnUpdateUser.Enabled = state;
            btnDeleteUser.Enabled = state;
            btnUnblockUser.Enabled = state;
            btnApplyOpp.Enabled = !state;
            btnCancelOpp.Enabled = !state;
        }
        #endregion

        #region  Form Events 
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = false;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Add;
        }
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(true);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Update;

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
            PerformClearTextBoxData();
            Form_Action = UserManagementFormAction.Delete;
        }
        private void btnUnblockUser_Click(object sender, EventArgs e)
        {
            selected_user = null;
            metroGrid1.Enabled = true;

            PerformEnableDisableTextBoxes(false);
            ToggleActionButtonsStates(false);
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
            if (Form_Action != UserManagementFormAction.Add && selected_user == null)
            {
                MessageBox.Show("Please select a user to perform the action.");
                ToggleActionButtonsStates(true);
                PerformCleanFormAction();
                return;
            }
            try
            {
                switch (Form_Action)
                {
                    case UserManagementFormAction.Add:
                        if (PerformValidateTextBoxData())
                        {
                            selected_user = new User();
                            MapTextboxesToUser();
                            result = _userBLL.AddUser(selected_user);
                        }
                        break;
                    case UserManagementFormAction.Update:
                        if (PerformValidateTextBoxData())
                        {
                            MapTextboxesToUser(); 
                            if (MessageBox.Show("Are you sure you want to update the user " + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                result = _userBLL.UpdateUser(selected_user);
                            }
                        }
                        break;
                    case UserManagementFormAction.Delete:
                        if (MessageBox.Show("Are you sure you want to delete the user " + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _userBLL.DeleteUser(selected_user);
                        }
                        break;
                    case UserManagementFormAction.Unblock:
                        if (!selected_user.Blocked)
                            MessageBox.Show("The selected User is not blocked.");
                        else if (MessageBox.Show("Are you sure you want to unblock the user " + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void btnCancelOpp_Click(object sender, EventArgs e)
        {
            PerformCleanFormAction();
            ToggleActionButtonsStates(true);
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
