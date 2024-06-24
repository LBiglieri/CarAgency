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
using CarAgency.Utilities;
using CarAgency.Utilities.Session;
using UI.Vehicles;

namespace CarAgency.UI
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Login();
        }

#region  Form UI Changes
        private void UpdateTitle()
        {
            if (SessionHandler.Logged())
                lblTitle.Text = "Welcome " + SessionHandler.GetUsername();
            else
                lblTitle.Text = "CarAgency";
            lblTitle.Refresh();
        }

#endregion

#region  Form Behavior 
        private void Login()
        {
            if (!SessionHandler.Logged())
            {
                LoginForm form = new LoginForm();
                form.ShowDialog();
                if (SessionHandler.Logged())
                {
                    UpdateTitle();
                    UserBLL _userBLL = new UserBLL();
                    if (_userBLL.IsUsingDefaultPassword(SessionHandler.GetId()))
                    {
                        ChangePasswordForm frm = new ChangePasswordForm();
                        frm.Show();
                        frm.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("You are already logged in.");
            }
        }
        private void Logout()
        {
            if (SessionHandler.Logged())
            {
                SessionHandler.Logout();
                
                foreach (Form frmClose in MdiChildren.ToArray())
                {
                    frmClose.Close();
                }

                if (!SessionHandler.Logged())
                    UpdateTitle();

                MessageBox.Show("Session closed correctly.");
            }
            else
            {
                MessageBox.Show("You are not logged in.");
            }
        }
        #endregion

#region  Form Events 
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
        private void permissionConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PermissionManagementForm frm = new PermissionManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }
        private void userPermissionConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserPermissionManagementForm frm = new UserPermissionManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }
        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManagementForm frm = new UserManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SessionHandler.Logged())
            {
                ChangePasswordForm frm = new ChangePasswordForm();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                MessageBox.Show("You are not logged in.");
            }
        }
        #endregion

        private void vehicleModelManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VehicleModelConfigurationForm frm = new VehicleModelConfigurationForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vehicleManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VehicleManagementForm frm = new VehicleManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void newQuotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateQuotationForm frm = new GenerateQuotationForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void newReservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateReservationForm frm = new GenerateReservationForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void generateInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateInvoiceForm frm = new GenerateInvoiceForm();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
