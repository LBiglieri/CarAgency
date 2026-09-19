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
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using BE;
using Microsoft.Win32;
using Security.Session;

namespace CarAgency.UI
{
    public partial class ChangePasswordForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        UserBLL _userBLL;
        public ChangePasswordForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            if (_userBLL.IsUsingDefaultPassword(SessionHandler.Instance.GetId()))
            {
                tbOldPassword.Visible = false;  
                tbOldPassword.Enabled = false;
            }
            LanguageService.Attach(this);
            this.Text = LanguageService.GetTagText(this.Tag.ToString());
            UpdateLanguage(LanguageService.GetCurrentLanguage());
        }

        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var result = default(SQLUpdateResult);
            try
            {
                if(ValidatePassword())
                {
                    result = _userBLL.ChangePassword(SessionHandler.Instance.GetId(), tbNewPassword.Text);

                    if (result != null && result.sqlResult != SQLResultType.success)
                    {
                        MessageBox.Show(result.message);
                        return;
                    }

                    MessageBox.Show(LanguageService.GetTagText("passwordChangedSuccessfully"));
                    Close(); 
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(LanguageService.GetErrorText(ee));
            }
        }

        private bool ValidatePassword()
        {
            if (tbOldPassword.Visible && (tbOldPassword.Text == ""))
            {
                MessageBox.Show(LanguageService.GetTagText("passwordWriteOld"));
                return false;
            }
            if (tbNewPassword.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("passwordWriteNew"));
                return false;
            }
            if (tbNewPassword.Text.Length < 8)
            {
                MessageBox.Show(LanguageService.GetTagText("passwordAtLeast8Chars"));
                return false;
            }
            if (tbRepeatPassword.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("passwordPleaseRepeat"));
                return false;
            }
            if (tbNewPassword.Text != tbRepeatPassword.Text)
            {
                MessageBox.Show(LanguageService.GetTagText("passwordPleaseRepeatCorrectly"));
                return false;
            }
            return true;
        }

        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText(this.Tag.ToString());
            tbOldPassword.WaterMark = LanguageService.GetTagText(tbOldPassword.Tag.ToString());
            tbNewPassword.WaterMark = LanguageService.GetTagText(tbNewPassword.Tag.ToString());
            tbRepeatPassword.WaterMark = LanguageService.GetTagText(tbRepeatPassword.Tag.ToString());
            btnChangePassword.Text = LanguageService.GetTagText(btnChangePassword.Tag.ToString());
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
