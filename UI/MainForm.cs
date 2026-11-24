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
using UI.Vehiculos;
using UI.Reportes;
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

                acquisitionsToolStripMenuItem.Visible = false;
                registerAcquisitionToolStripMenuItem.Visible = false;
                registerInspectionToolStripMenuItem.Visible = false;
                appraiseVehicleToolStripMenuItem.Visible = false;
                manageAcquisitionToolStripMenuItem.Visible = false;
                payAcquisitionToolStripMenuItem.Visible = false;

                billingToolStripMenuItem.Visible = false;
                generateInvoiceToolStripMenuItem.Visible = false;

                managementToolStripMenuItem.Visible = false;
                managePaperworkToolStripMenuItem.Visible = false;

                reportsToolStripMenuItem.Visible = false;
                salesForecastToolStripMenuItem.Visible = false;
                purchaseForecastToolStripMenuItem.Visible = false;

                configurationToolStripMenuItem.Visible = false;
                permissionConfigurationToolStripMenuItem.Visible = false;
                userManagementToolStripMenuItem.Visible = false;
                vehicleModelConfigurationToolStripMenuItem.Visible = false;
                vehicleManagementToolStripMenuItem.Visible = false;
                backupRestoreDatabaseToolStripMenuItem.Visible = false;
                eventLogToolStripMenuItem.Visible = false;
                changeLogToolStripMenuItem.Visible = false;
                integrityRepairToolStripMenuItem.Visible = false;
                return;
            }

            changePasswordToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.ChangePasswordForm));

            salesToolStripMenuItem.Visible = ((SessionHandler.Instance.IsAuthorized(BE.PermisoType.GenerateCotizacionForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.GenerateReservaForm)));
            newQuotationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.GenerateCotizacionForm));
            newReservationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.GenerateReservaForm));

            acquisitionsToolStripMenuItem.Visible = ((SessionHandler.Instance.IsAuthorized(BE.PermisoType.RegisterCompraForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.RegisterInspeccionForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.AppraiseVehiculoForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.ManageCompraForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.PayCompraForm)));
            registerAcquisitionToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.RegisterCompraForm));
            registerInspectionToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.RegisterInspeccionForm));
            appraiseVehicleToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.AppraiseVehiculoForm));
            manageAcquisitionToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.ManageCompraForm));
            payAcquisitionToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.PayCompraForm));

            billingToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.GenerateFacturaForm));
            generateInvoiceToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.GenerateFacturaForm));

            managementToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.ManageTramiteForm));
            managePaperworkToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.ManageTramiteForm));

            // El getter de ToolStripItem.Visible devuelve si el item se esta mostrando, no lo que se le asigno:
            // con el desplegable cerrado da false. Por eso los menus padre se calculan con los permisos.
            bool canForecastSales = SessionHandler.Instance.IsAuthorized(BE.PermisoType.ProyeccionVentasForm);
            bool canForecastPurchases = SessionHandler.Instance.IsAuthorized(BE.PermisoType.ProyeccionComprasForm);
            salesForecastToolStripMenuItem.Visible = canForecastSales;
            purchaseForecastToolStripMenuItem.Visible = canForecastPurchases;
            reportsToolStripMenuItem.Visible = canForecastSales || canForecastPurchases;

            configurationToolStripMenuItem.Visible = ((SessionHandler.Instance.IsAuthorized(BE.PermisoType.PermisoManagementForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.UsuarioManagementForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.VehiculoModeloConfigurationForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.VehiculoManagementForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.BackupRestoreForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.BitacoraEventosForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.BitacoraCambiosForm)) || (SessionHandler.Instance.IsAuthorized(BE.PermisoType.RecalculateDV)));
            permissionConfigurationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.PermisoManagementForm));
            userManagementToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.UsuarioManagementForm));
            vehicleModelConfigurationToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.VehiculoModeloConfigurationForm));
            vehicleManagementToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.VehiculoManagementForm));
            backupRestoreDatabaseToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.BackupRestoreForm));
            eventLogToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.BitacoraEventosForm));
            changeLogToolStripMenuItem.Visible = (SessionHandler.Instance.IsAuthorized(BE.PermisoType.BitacoraCambiosForm));
            integrityRepairToolStripMenuItem.Visible = SessionHandler.Instance.IsAuthorized(BE.PermisoType.RecalculateDV);

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
                    if (form.ShowDialog(this) == DialogResult.Abort)
                    {
                        // Diferido: Close() dentro de MainForm_Load descarta el formulario antes de que
                        // Application.Run lo muestre y tira ObjectDisposedException.
                        BeginInvoke(new Action(Close));
                        return;
                    }
                }
                if (SessionHandler.Instance.Logged())
                {
                    UpdateTitle();
                    UsuarioBLL _userBLL = new UsuarioBLL();
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
                if (MessageBox.Show(LanguageService.GetTagText("AreYouSureLogout"), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

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
            PermisoManagementForm frm = new PermisoManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }
        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuarioManagementForm frm = new UsuarioManagementForm();
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
            GenerateCotizacionForm frm = new GenerateCotizacionForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void newReservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateReservaForm frm = new GenerateReservaForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void registerAcquisitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterCompraForm frm = new RegisterCompraForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void registerInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterInspeccionForm frm = new RegisterInspeccionForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void appraiseVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppraiseVehiculoForm frm = new AppraiseVehiculoForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void manageAcquisitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCompraForm frm = new ManageCompraForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void payAcquisitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PayCompraForm frm = new PayCompraForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void generateInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateFacturaForm frm = new GenerateFacturaForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void managePaperworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTramiteForm frm = new ManageTramiteForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vehicleModelConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VehiculoModeloConfigurationForm frm = new VehiculoModeloConfigurationForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vehicleManagementToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            VehiculoManagementForm frm = new VehiculoManagementForm();
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
                BitacoraEventosForm frm = new BitacoraEventosForm();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }

        private void changeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BitacoraCambiosForm frm = new BitacoraCambiosForm();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }

        private void salesForecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProyeccionVentasForm frm = new ProyeccionVentasForm();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }

        private void purchaseForecastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProyeccionComprasForm frm = new ProyeccionComprasForm();
                frm.MdiParent = this;
                frm.Show();
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }

        // Siempre visible (UpdateAuthorizedMenus no lo toca): cualquier rol, y tambien sin sesion iniciada.
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global::UI.Ayuda.AyudaF1.Abrir();
        }

        private void integrityRepairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = MdiChildren.OfType<IntegrityRepairForm>().FirstOrDefault();
                if (frm != null)
                {
                    frm.Activate();
                    return;
                }
                frm = new IntegrityRepairForm();
                frm.MdiParent = this;
                frm.FormClosed += IntegrityRepairForm_FormClosed;
                frm.Show();
            }
            catch (Exception error) { MessageBox.Show(LanguageService.GetErrorText(error)); }
        }

        private void IntegrityRepairForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = (IntegrityRepairForm)sender;
            frm.FormClosed -= IntegrityRepairForm_FormClosed;
            if (frm.DialogResult != DialogResult.Retry || e.CloseReason == CloseReason.MdiFormClosing
                || IsDisposed || Disposing) return;

            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed && !Disposing && !SessionHandler.Instance.Logged())
                {
                    foreach (Form child in MdiChildren.ToArray()) child.Close();
                    UpdateTitle();
                    UpdateAuthorizedMenus();
                    Login();
                }
            }));
        }
        #endregion
    }
}
