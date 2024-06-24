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
using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CarAgency.UI
{
    public partial class CreateClientForm : MetroFramework.Forms.MetroForm
    {
        ClientsBLL _ClientBLL;
        public Client _savedClient;
        public CreateClientForm(int dni)
        {
            InitializeComponent();
            tbDni.Text = dni.ToString();
            _ClientBLL = new ClientsBLL();
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

                    MessageBox.Show("Client created successfully!");
                    Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private bool ValidatePassword()
        {
            if (tbDni.Text == "")
            {
                MessageBox.Show("Please write the clients Dni.");
                return false;
            }
            int dnitest;
            if (!int.TryParse(tbDni.Text, out dnitest))
            {
                MessageBox.Show("Please write the clients Dni.");
                return false;
            }
            if (tbName.Text == "")
            {
                MessageBox.Show("Please write the clients Name.");
                return false;
            }
            if (tbSurname.Text == "")
            {
                MessageBox.Show("Please write the clients Surname.");
                return false;
            }
            if (tbAddress.Text == "")
            {
                MessageBox.Show("Please write the clients Address.");
                return false;
            }
            if (tbPersonalPhone.Text == "")
            {
                MessageBox.Show("Please write the clients Personal Phone.");
                return false;
            }
            if (tbPhoneHome.Text == "")
            {
                MessageBox.Show("Please write the clients Home Phone.");
                return false;
            }
            if (tbMail.Text == "")
            {
                MessageBox.Show("Please write the clients email.");
                return false;
            }
            if (!Regex.IsMatch(tbMail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Please write a valid email.");
                return false;
            }
            if (dtBirth.Value > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show("The client cant be less than 18 years old to buy a car by himself.");
                return false;
            }
            return true;
        }

    }
}
