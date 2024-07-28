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
using CarAgency.Utilities.Session;
using Entities;
using Utilities.Session;

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
            try
            {
                _userBLL.Login(this.tbUser.Text, this.tbPassword.Text);
                MessageBox.Show(LanguageService.GetTagText("successfullLogin"));
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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
