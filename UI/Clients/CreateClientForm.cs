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
using UI.Clients.Controls;
using Security.Session;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CarAgency.UI
{
    public partial class CreateClientForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        ClientsBLL _ClientBLL;
        public Client _savedClient;
        public CreateClientForm(int dni)
        {
            InitializeComponent();
            tbDni.Text = dni.ToString();
            _ClientBLL = new ClientsBLL();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void CreateClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("CreateClientForm");
            this.Refresh();
            tbDni.WaterMark = LanguageService.GetTagText(tbDni.Tag.ToString());
            tbName.WaterMark = LanguageService.GetTagText(tbName.Tag.ToString());
            tbSurname.WaterMark = LanguageService.GetTagText(tbSurname.Tag.ToString());
            tbAddress.WaterMark = LanguageService.GetTagText(tbAddress.Tag.ToString());
            tbPersonalPhone.WaterMark = LanguageService.GetTagText(tbPersonalPhone.Tag.ToString());
            tbPhoneHome.WaterMark = LanguageService.GetTagText(tbPhoneHome.Tag.ToString());
            tbMail.WaterMark = LanguageService.GetTagText(tbMail.Tag.ToString());
            lblDateBirth.Text = LanguageService.GetTagText(lblDateBirth.Tag.ToString());
            btnRegisterClient.Text = LanguageService.GetTagText(btnRegisterClient.Tag.ToString());
        }

        private void btnRegisterClient_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            try
            {
                if(ValidatePassword())
                {
                    Client client = new Client();
                    client.Dni = int.Parse(tbDni.Text);
                    client.Name = tbName.Text;
                    client.Surname = tbSurname.Text;
                    client.Address = tbAddress.Text;
                    client.Phone_Number_Personal = tbPersonalPhone.Text;
                    client.Phone_Number_House = tbPhoneHome.Text;
                    client.Email = tbMail.Text;
                    client.Date_Of_Birth = dtBirth.Value;

                    result = _ClientBLL.AddClient(client);

                    if (result != null && result.sqlResult != SQLResultType.success)
                    {
                        MessageBox.Show(result.message);
                        return;
                    }

                    _savedClient = client;

                    MessageBox.Show(LanguageService.GetTagText("ClientCreatedSuccessfully"));
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
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientDni"));
                return false;
            }
            int dnitest;
            if (!int.TryParse(tbDni.Text, out dnitest))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientDni"));
                return false;
            }
            if (tbName.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientName"));
                return false;
            }
            if (tbSurname.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientSurname"));
                return false;
            }
            if (tbAddress.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientAddress"));
                return false;
            }
            if (tbPersonalPhone.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientPersonalPhone"));
                return false;
            }
            if (tbPhoneHome.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientHomePhone"));
                return false;
            }
            if (tbMail.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteClientEmail"));
                return false;
            }
            if (!Regex.IsMatch(tbMail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidEmail"));
                return false;
            }
            if (dtBirth.Value > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show(LanguageService.GetTagText("ClientMustBeAdult"));
                return false;
            }
            return true;
        }
    }
}
