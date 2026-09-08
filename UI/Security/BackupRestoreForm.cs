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
using CarAgency.Mappers.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using BE;
using Microsoft.Win32;
using UI.Clients.Controls;
using Security.Session;
using CarAgency.Security.Integrity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CarAgency.UI
{
    public partial class BackupRestoreForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        SecurityBLL _SecurityBLL;
        private readonly RecoverySession recovery;
        private bool restoring;
        public BackupRestoreForm() : this(null) { }
        public BackupRestoreForm(RecoverySession recovery)
        {
            if (recovery != null) IntegrityService.Current.AuthorizeRecovery(recovery, true);
            else if (!SessionHandler.Instance.IsAuthorized(PermissionType.BackupRestoreForm))
                throw new TranslatableException("NoBackupRestorePermission", "No tiene la patente de backup/restore.");
            this.recovery = recovery;
            InitializeComponent();
            _SecurityBLL = new SecurityBLL();
            LanguageService.Attach(this);
            UpdateLanguage("");
            if (recovery != null)
            {
                lblBackupDatabase.Visible = false;
                tbBackupPath.Visible = false;
                btnSelectBackupPath.Visible = false;
                btnBackupDatabase.Visible = false;
            }
        }

        private void BackupRestoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (restoring) { e.Cancel = true; return; }
            LanguageService.Detach(this);
        }
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("BackupRestoreForm");
            this.Refresh();
            lblBackupDatabase.Text = LanguageService.GetTagText(lblBackupDatabase.Tag.ToString());
            lblRestoreDatabase.Text = LanguageService.GetTagText(lblRestoreDatabase.Tag.ToString());
            tbBackupPath.WaterMark = LanguageService.GetTagText(tbBackupPath.Tag.ToString());
            tbRestorePath.WaterMark = LanguageService.GetTagText(tbRestorePath.Tag.ToString());
            btnSelectBackupPath.Text = LanguageService.GetTagText(btnSelectBackupPath.Tag.ToString());
            btnSelectRestorePath.Text = LanguageService.GetTagText(btnSelectRestorePath.Tag.ToString());
            btnBackupDatabase.Text = LanguageService.GetTagText(btnBackupDatabase.Tag.ToString());
            btnRestoreDatabase.Text = LanguageService.GetTagText(btnRestoreDatabase.Tag.ToString());
        }

        private void btnSelectBackupPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    tbBackupPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnBackupDatabase_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbBackupPath.Text))
            {
                try
                {
                    _SecurityBLL.RealizarBackup(tbBackupPath.Text);
                    MessageBox.Show(LanguageService.GetTagText("BackupCompletedSuccessfully"));
                    tbBackupPath.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LanguageService.GetTagText("errorBackupDatabase") + LanguageService.GetErrorText(ex));
                }
            }
            else
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectBackupPath"));
            }
        }

        private void btnSelectRestorePath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "SQL Backup Files (*.bak)|*.bak";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tbRestorePath.Text = openFileDialog.FileName;
                }
            }
        }

        private async void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbRestorePath.Text))
            {
                string mensaje = LanguageService.GetTagText("IntegrityConfirmRestore", "Se reemplazara la base con el backup seleccionado y se perderan los cambios posteriores. Se cerrara la sesion. ¿Desea continuar?")
                    + Environment.NewLine + tbRestorePath.Text;

                DialogResult resultado = MessageBox.Show(
                    mensaje,
                    LanguageService.GetTagText("AttentionTitle"),          
                    MessageBoxButtons.YesNo,      
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        restoring = true;
                        UseWaitCursor = true;
                        LoginPanel.Enabled = false;
                        string path = tbRestorePath.Text;
                        var report = await Task.Run(() => recovery == null
                            ? _SecurityBLL.RealizarRestore(path) : _SecurityBLL.RealizarRestore(path, recovery));
                        MessageBox.Show(report.IsConsistent
                            ? LanguageService.GetTagText("IntegrityRepaired", "Operacion completada. Inicie sesion nuevamente.")
                            : LanguageService.GetTagText("IntegrityRestoredInvalid", "El backup fue restaurado pero contiene inconsistencias o una configuracion DV incompatible. El acceso sigue bloqueado."));
                        tbRestorePath.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageService.GetTagText("errorRestoringDatabase") + LanguageService.GetErrorText(ex));
                    }
                    finally
                    {
                        restoring = false;
                        UseWaitCursor = false;
                        LoginPanel.Enabled = true;
                        SessionHandler.Instance.Logout();
                        IntegrityService.Current.EndRecovery();
                        DialogResult = DialogResult.Retry;
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectRestorePath"));
            }
        }
    }
}
