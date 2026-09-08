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
using CarAgency.BE;
using CarAgency.Security;
using CarAgency.Security.Session;
using BE;
using UI.Vehicles;
using Security.Session;

namespace CarAgency.UI
{
    public partial class MainForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        public MainForm()
        {
            InitializeComponent();
            LanguageService.Attach(this); 
            LanguageService.LoadLanguage("es");
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateAuthorizedMenus();
            Login();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { SessionHandler.Instance.Logout(); }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
            finally { LanguageService.Detach(this); }
        }

#region  Form UI Changes
        private void UpdateTitle()
        {
            if (SessionHandler.Instance.Logged())
                lblTitle.Text = LanguageService.GetTagText("Welcome") + " " + SessionHandler.Instance.GetUsername();
            else
                lblTitle.Text = "CarAgency";
            lblTitle.Refresh();
        }
        
        private void UpdateAuthorizedMenus()
        {
            if (!SessionHandler.Instance.Logged())
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
                backupRestoreDatabaseToolStripMenuItem.Visible = false;
                eventLogToolStripMenuItem.Visible = false;
                return;
            }

            changePasswordToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.ChangePasswordForm));

            salesToolStripMenuItem.Visible = ((SessionHandler.Instance.IsAuthorized(BE.PermissionType.GenerateQuotationForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermissionType.GenerateReservationForm)));
            newQuotationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.GenerateQuotationForm));
            newReservationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.GenerateReservationForm));

            billingToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.GenerateInvoiceForm));
            generateInvoiceToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.GenerateInvoiceForm));

            managementToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.ManagePaperworkForm));
            managePaperworkToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.ManagePaperworkForm));

            configurationToolStripMenuItem.Visible = ((SessionHandler.Instance.IsAuthorized(BE.PermissionType.PermissionManagementForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermissionType.UserManagementForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermissionType.VehicleModelConfigurationForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermissionType.VehicleManagementForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermissionType.BackupRestoreForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermissionType.EventLogForm)));
            permissionConfigurationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.PermissionManagementForm));
            userManagementToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.UserManagementForm));
            vehicleModelConfigurationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.VehicleModelConfigurationForm));
            vehicleManagementToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.VehicleManagementForm));
            backupRestoreDatabaseToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.BackupRestoreForm));
            eventLogToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermissionType.EventLogForm));

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
                if (control.DropDownItems.Count > 0)
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
            if (!SessionHandler.Instance.Logged())
            {
                using (LoginForm form = new LoginForm())
                {
                    if (form.ShowDialog(this) == DialogResult.Abort) { Close(); return; }
                }
                if (SessionHandler.Instance.Logged())
                {
                    UpdateTitle();
                    UserBLL _userBLL = new UserBLL();
                    if (_userBLL.IsUsingDefaultPassword(SessionHandler.Instance.GetId()))
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
            if (SessionHandler.Instance.Logged())
            {
                try { SessionHandler.Instance.Logout(); }
                catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
                
                foreach (Form frmClose in MdiChildren.ToArray())
                {
                    frmClose.Close();
                }

                if (!SessionHandler.Instance.Logged())
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
            if (SessionHandler.Instance.Logged())
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
        private void changeSystemsLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguageForm frm = new ChangeLanguageForm();
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

        private void backupRestoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (BackupRestoreForm frm = new BackupRestoreForm()) frm.ShowDialog(this);
                if (!SessionHandler.Instance.Logged())
                {
                    foreach (Form child in MdiChildren.ToArray()) child.Close();
                    UpdateTitle();
                    UpdateAuthorizedMenus();
                    Login();
                }
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }

        private void eventLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EventLogForm frm = new EventLogForm();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }
        #endregion
    }
}
