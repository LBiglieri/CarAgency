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
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;
using Microsoft.Win32;

namespace CarAgency.UI
{
    public partial class ChangePasswordForm : MetroFramework.Forms.MetroForm
    {
        UserBLL _userBLL;
        public ChangePasswordForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            if (_userBLL.IsUsingDefaultPassword(SessionHandler.GetId()))
            {
                tbOldPassword.Visible = false;  
                tbOldPassword.Enabled = false;  
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            try
            {
                if(ValidatePassword())
                {
                    string NewPassword = CryptographyHandler.GenerateSHA512Hash(tbNewPassword.Text);
                    if (!SessionHandler.ValidatePassword(NewPassword))
                    {
                        MessageBox.Show("The new password you entered is the same as the one you had before.");
                        return;
                    }

                    result = _userBLL.ChangePassword(SessionHandler.GetId(), NewPassword);

                    if (result != null && result.sqlResult != SQLResultType.success)
                    {
                        MessageBox.Show(result.message);
                        return;
                    }

                    MessageBox.Show("Password changed successfully!");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private bool ValidatePassword()
        {
            if (tbOldPassword.Visible && (tbOldPassword.Text == ""))
            {
                MessageBox.Show("Please write your old password.");
                return false;
            }
            if (tbNewPassword.Text == "")
            {
                MessageBox.Show("Please write your new password.");
                return false;
            }
            if (tbNewPassword.Text.Length < 8)
            {
                MessageBox.Show("Your new password needs to be at least 8 characters.");
                return false;
            }
            if (tbRepeatPassword.Text == "")
            {
                MessageBox.Show("Please repeat your new password.");
                return false;
            }
            if (tbNewPassword.Text != tbRepeatPassword.Text)
            {
                MessageBox.Show("Please repeat your new password correctly.");
                return false;
            }
            return true;
        }
        #region  Form Events 


        private void tbRepeatPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChangePassword.Select();
                btnChangePassword.PerformClick();
            }
        }

        #endregion

        private void tbOldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbNewPassword.Select();
                tbNewPassword.Focus();
            }
        }

        private void tbNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbRepeatPassword.Select();
                tbRepeatPassword.Focus();
            }
        }
    }
}
