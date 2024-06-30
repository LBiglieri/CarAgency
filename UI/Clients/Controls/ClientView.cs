using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
using CarAgency.UI;
using CarAgency.Utilities.Persistence;
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

namespace UI.Clients.Controls
{
    public enum ClientViewMode
    {
        WithNoRegistration,
        WithRegistration
    }
    public partial class ClientView : UserControl
    {
        public Client client;
        ClientsBLL _clientbll;
        public ClientViewMode clientViewMode = ClientViewMode.WithNoRegistration;

        public event EventHandler ClientFound;

        public ClientView()
        {
            InitializeComponent();
            _clientbll = new ClientsBLL();
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
                lblDNI.Text = "DNI: " + client.Dni.ToString();
                lblNameSurname.Text = "Full Name: " + client.Name.ToString() + " " + client.Surname.ToString();
                lblPhoneHome.Text = "Home Phone: " + client.Phone_Number_House.ToString();
                lblPhonePersonal.Text = "Personal Phone: " + client.Phone_Number_Personal.ToString();
                lblEmail.Text = "Email: " + client.Email;

                lblDNI.Visible = true;
                lblNameSurname.Visible = true;
                lblPhoneHome.Visible = true;
                lblPhonePersonal.Visible = true;
                lblEmail.Visible = true;
                lblPlease.Visible = false;
                tbDni.Visible = false;
                tbDni.Enabled = false;

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
                        if (clientViewMode == ClientViewMode.WithRegistration && MessageBox.Show("The DNI you entered was not found on the clients database. Do you want to register a new client with this DNI? ", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
    }
}
