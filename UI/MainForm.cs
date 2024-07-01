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
using CarAgency.Utilities;
using CarAgency.Utilities.Session;
using Entities;
using UI.Vehicles;
using Utilities.Session;

namespace CarAgency.UI
{
    public partial class MainForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        public MainForm()
        {
            InitializeComponent();
            LanguageService.Attach(this); 
            LanguageService.LoadLanguage("en");
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateAuthorizedMenus();
            Login();
        }

#region  Form UI Changes
        private void UpdateTitle()
        {
            if (SessionHandler.Logged())
                lblTitle.Text = LanguageService.GetTagText("Welcome") + " " + SessionHandler.GetUsername();
            else
                lblTitle.Text = "CarAgency";
            lblTitle.Refresh();
        }
        
        private void UpdateAuthorizedMenus()
        {
            if (!SessionHandler.Logged())
            {
                changePasswordToolStripMenuItem.Visible = false;

                salesToolStripMenuItem.Visible = false;
                newQuotationToolStripMenuItem.Visible = false;
                newReservationToolStripMenuItem.Visible = false;

                billingToolStripMenuItem.Visible = false;
                generateInvoiceToolStripMenuItem.Visible = false;

                managementToolStripMenuItem.Visible = false;
                managePaperworkToolStripMenuItem.Visible = false;

                configurationToolStripMenuItem.Visible = false;
                permissionConfigurationToolStripMenuItem.Visible = false;
                userManagementToolStripMenuItem.Visible = false;
                vehicleModelConfigurationToolStripMenuItem.Visible = false;
                vehicleManagementToolStripMenuItem.Visible = false;
                return;
            }

            changePasswordToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.ChangePasswordForm));

            salesToolStripMenuItem.Visible = ((SessionHandler.IsAuthorized(Entities.PermissionType.GenerateQuotationForm)) || (SessionHandler.IsAuthorized(Entities.PermissionType.GenerateReservationForm)));
            newQuotationToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.GenerateQuotationForm));
            newReservationToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.GenerateReservationForm));

            billingToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.GenerateInvoiceForm));
            generateInvoiceToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.GenerateInvoiceForm));

            managementToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.ManagePaperworkForm));
            managePaperworkToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.ManagePaperworkForm));

            configurationToolStripMenuItem.Visible = ((SessionHandler.IsAuthorized(Entities.PermissionType.PermissionManagementForm)) || (SessionHandler.IsAuthorized(Entities.PermissionType.UserManagementForm)) || (SessionHandler.IsAuthorized(Entities.PermissionType.VehicleModelConfigurationForm)) || (SessionHandler.IsAuthorized(Entities.PermissionType.VehicleManagementForm)));
            permissionConfigurationToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.PermissionManagementForm));
            userManagementToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.UserManagementForm));
            vehicleModelConfigurationToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.VehicleModelConfigurationForm));
            vehicleManagementToolStripMenuItem.Visible = (SessionHandler.IsAuthorized(Entities.PermissionType.VehicleManagementForm));

        }

        public void UpdateLanguage(string language)
        {
            UpdateTitle();
            UpdateMenuLanguage();
        }

        private void UpdateMenuLanguage()
        {
            foreach (ToolStripMenuItem control in menuStrip1.Items)
            {
                control.Text = LanguageService.GetTagText(control.Tag.ToString());
                if (control.DropDownItems.Count > 1)
                {
                    foreach (ToolStripMenuItem innercontrol in control.DropDownItems)
                    {
                        innercontrol.Text = LanguageService.GetTagText(innercontrol.Tag.ToString());
                    }
                }
            }
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
                UpdateAuthorizedMenus();
            }
            else
            {
                MessageBox.Show(LanguageService.GetTagText("alreadyLogged"));
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

                UpdateAuthorizedMenus();
                MessageBox.Show(LanguageService.GetTagText("sessionClosed"));
            }
            else
            {
                MessageBox.Show(LanguageService.GetTagText("notLogged"));
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
                MessageBox.Show(LanguageService.GetTagText("notLogged"));
            }
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

        private void managePaperworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePaperworkForm frm = new ManagePaperworkForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vehicleModelConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VehicleModelConfigurationForm frm = new VehicleModelConfigurationForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vehicleManagementToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            VehicleManagementForm frm = new VehicleManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion
    }
}
