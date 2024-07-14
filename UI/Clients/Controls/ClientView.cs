using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
using CarAgency.UI;
using CarAgency.Utilities.Persistence;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Vehicles;
using Utilities.Session;

namespace UI.Clients.Controls
{
    public enum ClientViewMode
    {
        WithNoRegistration,
        WithRegistration
    }
    public partial class ClientView : UserControl, ILanguageObserver
    {
        public Client client;
        ClientsBLL _clientbll;
        public ClientViewMode clientViewMode = ClientViewMode.WithNoRegistration;

        public event EventHandler ClientFound;

        public ClientView()
        {
            InitializeComponent();
            _clientbll = new ClientsBLL();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        public void DetachLanguageObserver()
        {
            LanguageService.Detach(this);
        }

        public void InitWithClient(Client _client)
        {
            client = _client;
            UpdateControlView();
        }

        private void UpdateControlView()
        {
            if (client != null)
            {
                lblDNI.Visible = true;
                lblNameSurname.Visible = true;
                lblPhoneHome.Visible = true;
                lblPhonePersonal.Visible = true;
                lblEmail.Visible = true;
                lblPlease.Visible = false;
                tbDni.Visible = false;
                tbDni.Enabled = false;
                UpdateLanguage("");

                this.ClientFound(this, new EventArgs());
            }
        }

        private void tbDni_Leave(object sender, EventArgs e)
        {
            try
            {
                int dni;
                if (client == null && tbDni.Text != "" && int.TryParse(tbDni.Text, out dni))
                {

                    client = _clientbll.GetByDni(dni);
                    if (client != null)
                    {
                        UpdateControlView();
                    }
                    else
                    {
                        if (clientViewMode == ClientViewMode.WithRegistration && MessageBox.Show(LanguageService.GetTagText("AttentionDNINotFound"), "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            CreateClientForm frm = new CreateClientForm(dni);
                            frm.ShowDialog();
                            if (frm._savedClient != null)
                            {
                                client = _clientbll.GetByDni(dni);
                                UpdateControlView();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateLanguage(string language)
        {
            if (client == null)
            {
                lblPlease.Text = LanguageService.GetTagText(lblPlease.Tag.ToString());
            }
            else
            {
                lblDNI.Text = LanguageService.GetTagText(lblDNI.Tag.ToString()) + client.Dni.ToString();
                lblNameSurname.Text = LanguageService.GetTagText(lblNameSurname.Tag.ToString()) + client.Name.ToString() + " " + client.Surname.ToString();
                lblPhoneHome.Text = LanguageService.GetTagText(lblPhoneHome.Tag.ToString()) + client.Phone_Number_House.ToString();
                lblPhonePersonal.Text = LanguageService.GetTagText(lblPhonePersonal.Tag.ToString()) + client.Phone_Number_Personal.ToString();
                lblEmail.Text = LanguageService.GetTagText(lblEmail.Tag.ToString()) + client.Email;

                lblDNI.Visible = true;
                lblNameSurname.Visible = true;
                lblPhoneHome.Visible = true;
                lblPhonePersonal.Visible = true;
                lblEmail.Visible = true;
                lblPlease.Visible = false;
                tbDni.Visible = false;
                tbDni.Enabled = false;
            }
        }
    }
}
