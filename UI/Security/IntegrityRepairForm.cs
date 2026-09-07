using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using CarAgency.Security.Integrity;
using Security.Session;

namespace CarAgency.UI
{
    public partial class IntegrityRepairForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        private readonly RecoverySession recovery;
        private bool busy;

        public IntegrityRepairForm(RecoverySession recovery)
        {
            IntegrityService.Current.AuthorizeRecovery(recovery);
            this.recovery = recovery;
            InitializeComponent();
            LanguageService.Attach(this);
            UpdateLanguage("");
            tbIssues.Text = string.Join(Environment.NewLine, recovery.Report.Issues);
            btnRestore.Enabled = recovery.CanRestore;
        }

        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText(this.Tag.ToString());
            this.Refresh();
            lblIntegrityDetected.Text = LanguageService.GetTagText(lblIntegrityDetected.Tag.ToString());
            btnRecalculate.Text = LanguageService.GetTagText(btnRecalculate.Tag.ToString());
            btnRestore.Text = LanguageService.GetTagText(btnRestore.Tag.ToString());
            btnExit.Text = LanguageService.GetTagText(btnExit.Tag.ToString());
        }

        private void SetBusy(bool value)
        {
            busy = value;
            UseWaitCursor = value;
            btnRecalculate.Enabled = !value;
            btnRestore.Enabled = !value && recovery.CanRestore;
            btnExit.Enabled = !value;
        }

        private async void btnRecalculate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, LanguageService.GetTagText("IntegrityConfirmRecalculate", "Esta accion acepta los datos actuales como validos. No recupera ni corrige datos alterados. ¿Desea continuar?"), Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            SetBusy(true);
            try
            {
                await Task.Run(() => IntegrityService.Current.Recalculate(recovery));
                MessageBox.Show(this, LanguageService.GetTagText("IntegrityRepaired", "Operacion completada. Inicie sesion nuevamente."));
                SetBusy(false);
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception error) { MessageBox.Show(this, LanguageService.GetErrorText(error), Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            IntegrityService.Current.EndRecovery();
            if (DialogResult != DialogResult.Retry) DialogResult = DialogResult.Abort;
        }
    }
}
