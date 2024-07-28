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
    public partial class ChangeLanguageForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        UserBLL _userBLL;
        List<Languages> languages;
        string initial_language;
        public ChangeLanguageForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            initial_language = LanguageService.GetCurrentLanguage();
            LanguageService.Attach(this);
            UpdateLanguageCombo();
            UpdateLanguage("");
        }

        private void ChangeLanguageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }

        private void UpdateLanguageCombo()
        {
            languages = LanguageService.GetAvailableLanguages();
            comboLanguage.DataSource = languages;
            comboLanguage.ValueMember = "Language_Description";
            foreach (Languages language in languages)
            {
                if (language.Language_Code == initial_language)
                {
                    comboLanguage.SelectedIndex = comboLanguage.FindStringExact(language.Language_Description);
                }
            }
        }
#region  Form Events 


        public void UpdateLanguage(string language)
        {
            lblChangeLanguage.Text = LanguageService.GetTagText("lblChangeLanguage");
            lblLanguage.Text = LanguageService.GetTagText(lblLanguage.Tag.ToString());
        }

        #endregion

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLanguage.SelectedIndex != -1 && (Languages)comboLanguage.SelectedItem != null)
            {
                if (SessionHandler.Logged())
                    _userBLL.ChangeLanguage(SessionHandler.GetId(), ((Languages)comboLanguage.SelectedItem).Language_Code);
                else
                    LanguageService.LoadLanguage(((Languages)comboLanguage.SelectedItem).Language_Code);
            }
        }
    }
}
