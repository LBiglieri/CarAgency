using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using BE;
using Microsoft.Win32;
using UI.Personas.Controls;
using Security.Session;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using UI.Ayuda;

namespace CarAgency.UI
{
    public partial class CreatePersonaForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        PersonaBLL _PeopleBLL;
        public Persona _savedPerson;
        public CreatePersonaForm(int dni)
        {
            InitializeComponent();
            AyudaButton.Add(this);
            tbDni.Text = dni.ToString();
            _PeopleBLL = new PersonaBLL();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void CreatePersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("CreatePersonaForm");
            this.Refresh();
            tbDni.WaterMark = LanguageService.GetTagText(tbDni.Tag.ToString());
            tbName.WaterMark = LanguageService.GetTagText(tbName.Tag.ToString());
            tbSurname.WaterMark = LanguageService.GetTagText(tbSurname.Tag.ToString());
            tbAddress.WaterMark = LanguageService.GetTagText(tbAddress.Tag.ToString());
            tbPersonalPhone.WaterMark = LanguageService.GetTagText(tbPersonalPhone.Tag.ToString());
            tbPhoneHome.WaterMark = LanguageService.GetTagText(tbPhoneHome.Tag.ToString());
            tbMail.WaterMark = LanguageService.GetTagText(tbMail.Tag.ToString());
            lblDateBirth.Text = LanguageService.GetTagText(lblDateBirth.Tag.ToString());
            btnRegisterPerson.Text = LanguageService.GetTagText(btnRegisterPerson.Tag.ToString());
        }

        private void btnRegisterPerson_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            try
            {
                if(ValidatePassword())
                {
                    Persona person = new Persona();
                    person.DNI = int.Parse(tbDni.Text);
                    person.Nombre = tbName.Text;
                    person.Apellido = tbSurname.Text;
                    person.Direccion = tbAddress.Text;
                    person.TelefonoPersonal = tbPersonalPhone.Text;
                    person.TelefonoCasa = tbPhoneHome.Text;
                    person.Email = tbMail.Text;
                    person.FechaNacimiento = dtBirth.Value;

                    result = _PeopleBLL.AddPerson(person);

                    if (result != null && result.sqlResult != SQLResultType.success)
                    {
                        MessageBox.Show(result.message);
                        return;
                    }

                    _savedPerson = person;

                    MessageBox.Show(LanguageService.GetTagText("PersonCreatedSuccessfully"));
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
            if (tbDni.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonDni"));
                return false;
            }
            int dnitest;
            if (!int.TryParse(tbDni.Text, out dnitest))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonDni"));
                return false;
            }
            if (tbName.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonName"));
                return false;
            }
            if (tbSurname.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonSurname"));
                return false;
            }
            if (tbAddress.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonAddress"));
                return false;
            }
            if (tbPersonalPhone.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonPersonalPhone"));
                return false;
            }
            if (tbPhoneHome.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonHomePhone"));
                return false;
            }
            if (tbMail.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWritePersonEmail"));
                return false;
            }
            if (!Regex.IsMatch(tbMail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidEmail"));
                return false;
            }
            if (dtBirth.Value > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show(LanguageService.GetTagText("PersonMustBeAdult"));
                return false;
            }
            return true;
        }
    }
}
