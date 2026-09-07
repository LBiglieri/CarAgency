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
using Security.Session;
using CarAgency.Security.Integrity;

namespace CarAgency.UI
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        UserBLL _userBLL;
        List<Languages> languages;
        string initial_language;
        public LoginForm()
        {
            _userBLL = new UserBLL();
            InitializeComponent();
            initial_language = LanguageService.GetCurrentLanguage();
            LanguageService.Attach(this);
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            UseWaitCursor = true;
            try
            {
                RecoverySession recovery = _userBLL.Login(this.tbUser.Text, this.tbPassword.Text);
                if (recovery != null)
                {
                    tbPassword.Clear();
                    UseWaitCursor = false;
                    using (var repair = new IntegrityRepairForm(recovery))
                    {
                        if (repair.ShowDialog(this) != DialogResult.Retry)
                        {
                            DialogResult = DialogResult.Abort;
                            Close();
                            return;
                        }
                    }
                    tbUser.Clear();
                    tbPassword.Clear();
                    tbUser.Focus();
                    return;
                }
                MessageBox.Show(LanguageService.GetTagText("successfullLogin"));
                this.Close();
            }
            catch (IntegrityAccessDeniedException)
            {
                MessageBox.Show(LanguageService.GetTagText("IntegrityAccessDenied", "Se detectaron inconsistencias. Acceso bloqueado. Contacte al administrador con patente de recalculo DV."));
                tbPassword.Clear();
                DialogResult = DialogResult.Abort;
                Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(LanguageService.GetErrorText(ee));
            }
            finally
            {
                UseWaitCursor = false;
                btnLogin.Enabled = true;
            }
        }
#region  Form Events 
        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbPassword.Select();
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Select();
                btnLogin.PerformClick();
            }
        }

        public void UpdateLanguage(string language)
        {
            lblWelcome.Text = LanguageService.GetTagText("Welcome");
            tbUser.WaterMark = LanguageService.GetTagText(tbUser.Tag.ToString());
            tbPassword.WaterMark = LanguageService.GetTagText(tbPassword.Tag.ToString());
            btnLogin.Text = LanguageService.GetTagText(btnLogin.Tag.ToString());
        }

        #endregion


        private void btnAdmin_Click(object sender, EventArgs e)
        {
            tbUser.Text = "Admin";
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            tbUser.Text = "GNigote";
        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            tbUser.Text = "EQuito";
        }

        private void btnAdministrative_Click(object sender, EventArgs e)
        {
            tbUser.Text = "RDMoniado";
        }
    }
}
