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
using CarAgency.Repository.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;
using Entities;
using Microsoft.Win32;
using UI.Clients.Controls;
using Utilities.Session;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CarAgency.UI
{
    public partial class BackupRestoreForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        SecurityBLL _SecurityBLL;
        public BackupRestoreForm()
        {
            InitializeComponent();
            _SecurityBLL = new SecurityBLL();
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void BackupRestoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void btnRegisterClient_Click(object sender, EventArgs e)
        {
            SQLUpdateResult result = null;
            try
            {

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
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
                    MessageBox.Show(LanguageService.GetTagText("errorBackupDatabase") + $"{ex.Message}");
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

        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbRestorePath.Text))
            {
                string mensaje = LanguageService.GetTagText("AreYouSureYouWantToRestore");

                DialogResult resultado = MessageBox.Show(
                    mensaje,
                    "Atención!",          
                    MessageBoxButtons.YesNo,      
                    MessageBoxIcon.Warning      
                );

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        _SecurityBLL.RealizarRestore(tbRestorePath.Text);
                        MessageBox.Show(LanguageService.GetTagText("RestoreCompletedSuccessfully"));
                        tbRestorePath.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageService.GetTagText("errorRestoringDatabase") + $"{ex.Message}");
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
