using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.Security.Integrity;
using CarAgency.Security.Session;
using Security.Session;

namespace CarAgency.UI
{
    public partial class IntegrityRepairForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        private readonly RecoverySession recovery;
        private bool busy;
        private IntegrityReport report;

        public IntegrityRepairForm() : this(null) { }

        public IntegrityRepairForm(RecoverySession recovery)
        {
            if (recovery != null) IntegrityService.Current.AuthorizeRecovery(recovery);
            else report = IntegrityService.Current.VerifyForCurrentUser();
            this.recovery = recovery;
            if (recovery != null) report = recovery.Report;
            InitializeComponent();
            if (recovery == null) Tag = "IntegrityMaintenanceTitle";
            btnVerify.Visible = recovery == null;
            LanguageService.Attach(this);
            UpdateLanguage("");
            SetBusy(false);
        }

        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText(this.Tag.ToString());
            this.Refresh();
            lblIntegrityDetected.Text = LanguageService.GetTagText(report.IsConsistent
                ? "IntegrityConsistent" : recovery == null ? "IntegrityIssuesFound" : "IntegrityDetected");
            lblIntegrityDetected.ForeColor = report.IsConsistent ? System.Drawing.Color.DarkGreen : System.Drawing.Color.DarkRed;
            tbIssues.Text = report.IsConsistent ? LanguageService.GetTagText("IntegrityConsistent")
                : string.Join(Environment.NewLine, report.Issues);
            btnVerify.Text = LanguageService.GetTagText(btnVerify.Tag.ToString());
            btnRecalculate.Text = LanguageService.GetTagText(btnRecalculate.Tag.ToString());
            btnRestore.Text = LanguageService.GetTagText(btnRestore.Tag.ToString());
            btnExit.Text = LanguageService.GetTagText(btnExit.Tag.ToString());
        }

        private void SetBusy(bool value)
        {
            busy = value;
            UseWaitCursor = value;
            btnVerify.Enabled = !value;
            btnRecalculate.Enabled = !value && report.CanRecalculate;
            btnRestore.Enabled = !value && (recovery != null ? recovery.CanRestore
                : SessionHandler.Instance.IsAuthorized(PermissionType.BackupRestoreForm));
            btnExit.Enabled = !value;
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            SetBusy(true);
            try
            {
                report = await Task.Run(() => IntegrityService.Current.VerifyForCurrentUser());
                UpdateLanguage("");
            }
            catch (Exception error) { MessageBox.Show(this, LanguageService.GetErrorText(error), Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { if (!IsDisposed) SetBusy(false); }
        }

        private async void btnRecalculate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, LanguageService.GetTagText("IntegrityConfirmRecalculate", "Esta accion acepta los datos actuales como validos. No recupera ni corrige datos alterados. ¿Desea continuar?"), Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            SetBusy(true);
            try
            {
                await Task.Run(() =>
                {
                    if (recovery == null) IntegrityService.Current.Recalculate();
                    else IntegrityService.Current.Recalculate(recovery);
                });
                MessageBox.Show(this, LanguageService.GetTagText("IntegrityRepaired", "Operacion completada. Inicie sesion nuevamente."));
                SetBusy(false);
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, LanguageService.GetErrorText(error), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (recovery == null && !SessionHandler.Instance.Logged())
                {
                    SetBusy(false);
                    DialogResult = DialogResult.Retry;
                    Close();
                }
            }
            finally { if (!IsDisposed) SetBusy(false); }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new BackupRestoreForm(recovery))
                    if (form.ShowDialog(this) == DialogResult.Retry)
                    {
                        DialogResult = DialogResult.Retry;
                        Close();
                    }
            }
            catch (Exception error) { MessageBox.Show(this, LanguageService.GetErrorText(error), Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IntegrityRepairForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy) { e.Cancel = true; return; }
            LanguageService.Detach(this);
            if (recovery != null)
            {
                IntegrityService.Current.EndRecovery();
                if (DialogResult != DialogResult.Retry) DialogResult = DialogResult.Abort;
            }
        }
    }
}
