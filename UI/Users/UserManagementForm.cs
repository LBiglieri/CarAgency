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
using Entities;
using MetroFramework.Controls;
using Utilities.Session;

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
    public partial class UserManagementForm : MetroFramework.Forms.MetroForm, ILanguageObserver
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
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void UserManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }

        #region  Perform
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("UserManagementForm");
            this.Refresh();
            tbDniUser.WaterMark = LanguageService.GetTagText(tbDniUser.Tag.ToString());
            tbUsername.WaterMark = LanguageService.GetTagText(tbUsername.Tag.ToString());
            tbNameU.WaterMark = LanguageService.GetTagText(tbNameU.Tag.ToString());
            tbSurnameU.WaterMark = LanguageService.GetTagText(tbSurnameU.Tag.ToString());
            lblActiveUSers.Text = LanguageService.GetTagText(lblActiveUSers.Tag.ToString());
            btnAddUser.Text = LanguageService.GetTagText(btnAddUser.Tag.ToString());
            btnUpdateUser.Text = LanguageService.GetTagText(btnUpdateUser.Tag.ToString());
            btnDeleteUser.Text = LanguageService.GetTagText(btnDeleteUser.Tag.ToString());
            btnUnblockUser.Text = LanguageService.GetTagText(btnUnblockUser.Tag.ToString());
            btnApplyOpp.Text = LanguageService.GetTagText(btnApplyOpp.Tag.ToString());
            btnCancelOpp.Text = LanguageService.GetTagText(btnCancelOpp.Tag.ToString());
            chkBlocked.Text = LanguageService.GetTagText(chkBlocked.Tag.ToString());
            chkActive.Text = LanguageService.GetTagText(chkActive.Tag.ToString());
        }
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
            tbNameU.Enabled = flag;
            tbSurnameU.Enabled = flag;
            comboFamily.Enabled = flag;
        }
        void PerformClearTextBoxData()
        {
            tbDniUser.Text = "";
            tbUsername.Text = "";
            tbNameU.Text = "";
            tbSurnameU.Text = "";
            comboFamily.SelectedIndex = -1;
            chkBlocked.Checked = false;
            chkActive.Checked = false;
        }
        void PerformPopulateTextBoxData()
        {
            tbDniUser.Text = selected_user.Dni.ToString();
            tbUsername.Text = selected_user.Username;
            tbNameU.Text = selected_user.Name;
            tbSurnameU.Text = selected_user.Surname;
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
            if (tbDniUser.Text == "" || tbUsername.Text == "" || tbNameU.Text == "" || tbSurnameU.Text == "" || comboFamily.SelectedIndex== -1)
            {
                MessageBox.Show(LanguageService.GetTagText("FillAllFieldsUser"));
                return false;
            }
            if (!int.TryParse(tbDniUser.Text, out _))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseOnlyDigitsUser"));
                return false;
            }

            return true;
        }

        private void MapTextboxesToUser()
        {
            selected_user.Dni = int.Parse(tbDniUser.Text);
            selected_user.Username = tbUsername.Text;
            selected_user.Name = tbNameU.Text;
            selected_user.Surname = tbSurnameU.Text;
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
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectUser"));
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
                            if (MessageBox.Show(LanguageService.GetTagText("AreYouSureUpdate") + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                result = _userBLL.UpdateUser(selected_user);
                            }
                        }
                        break;
                    case UserManagementFormAction.Delete:
                        if (MessageBox.Show(LanguageService.GetTagText("AreYouSureDelete") + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _userBLL.DeleteUser(selected_user);
                        }
                        break;
                    case UserManagementFormAction.Unblock:
                        if (!selected_user.Blocked)
                            MessageBox.Show(LanguageService.GetTagText("IsNotBlocked"));
                        else if (MessageBox.Show(LanguageService.GetTagText("AreYouSureUnblock") + selected_user.Username + "?", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            result = _userBLL.AlterBlockedState(selected_user,false);
                        }
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
