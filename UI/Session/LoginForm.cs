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
        public LoginForm()
        {
            _userBLL = new UserBLL();
            InitializeComponent();
            LanguageService.Attach(this);
            UpdateLanguageCombo();
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
                LanguageService.LoadLanguage("en");
                MessageBox.Show(LanguageService.GetTagText("successfullLogin"));
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void UpdateLanguageCombo()
        {
            languages = new List<Languages>
            {
                new Languages("en", "English"),
                new Languages("es", "Español"),
                new Languages("po", "Português")
            };
            comboLanguage.DataSource = languages;
            comboLanguage.ValueMember = "Language_Description";
            foreach (Languages language in languages)
            {
                if (language.Language_Code == LanguageService.GetCurrentLanguage())
                    comboLanguage.SelectedIndex = comboLanguage.FindStringExact(language.Language_Description);
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
            lblLanguage.Text = LanguageService.GetTagText(lblLanguage.Tag.ToString());
        }

        #endregion

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLanguage.SelectedIndex != -1 && (Languages)comboLanguage.SelectedItem != null)
            {
                LanguageService.LoadLanguage(((Languages)comboLanguage.SelectedItem).Language_Code);
            }
        }
    }
}
