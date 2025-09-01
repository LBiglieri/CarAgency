namespace CarAgency.UI
{
    partial class BackupRestoreForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupRestoreForm));
            this.LoginPanel = new MetroFramework.Controls.MetroPanel();
            this.tbBackupPath = new MetroFramework.Controls.MetroTextBox();
            this.lblBackupDatabase = new MetroFramework.Controls.MetroLabel();
            this.btnSelectBackupPath = new MetroFramework.Controls.MetroButton();
            this.btnBackupDatabase = new MetroFramework.Controls.MetroButton();
            this.btnRestoreDatabase = new MetroFramework.Controls.MetroButton();
            this.btnSelectRestorePath = new MetroFramework.Controls.MetroButton();
            this.lblRestoreDatabase = new MetroFramework.Controls.MetroLabel();
            this.tbRestorePath = new MetroFramework.Controls.MetroTextBox();
            this.LoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginPanel.Controls.Add(this.btnRestoreDatabase);
            this.LoginPanel.Controls.Add(this.btnSelectRestorePath);
            this.LoginPanel.Controls.Add(this.lblRestoreDatabase);
            this.LoginPanel.Controls.Add(this.tbRestorePath);
            this.LoginPanel.Controls.Add(this.btnBackupDatabase);
            this.LoginPanel.Controls.Add(this.btnSelectBackupPath);
            this.LoginPanel.Controls.Add(this.lblBackupDatabase);
            this.LoginPanel.Controls.Add(this.tbBackupPath);
            this.LoginPanel.HorizontalScrollbarBarColor = true;
            this.LoginPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.LoginPanel.HorizontalScrollbarSize = 10;
            this.LoginPanel.Location = new System.Drawing.Point(23, 63);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(839, 401);
            this.LoginPanel.TabIndex = 0;
            this.LoginPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.LoginPanel.VerticalScrollbarBarColor = true;
            this.LoginPanel.VerticalScrollbarHighlightOnWheel = false;
            this.LoginPanel.VerticalScrollbarSize = 10;
            // 
            // tbBackupPath
            // 
            // 
            // 
            // 
            this.tbBackupPath.CustomButton.Image = null;
            this.tbBackupPath.CustomButton.Location = new System.Drawing.Point(592, 2);
            this.tbBackupPath.CustomButton.Name = "";
            this.tbBackupPath.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbBackupPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbBackupPath.CustomButton.TabIndex = 1;
            this.tbBackupPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbBackupPath.CustomButton.UseSelectable = true;
            this.tbBackupPath.CustomButton.Visible = false;
            this.tbBackupPath.DisplayIcon = true;
            this.tbBackupPath.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbBackupPath.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbBackupPath.Lines = new string[0];
            this.tbBackupPath.Location = new System.Drawing.Point(41, 78);
            this.tbBackupPath.MaxLength = 32767;
            this.tbBackupPath.Name = "tbBackupPath";
            this.tbBackupPath.PasswordChar = '\0';
            this.tbBackupPath.PromptText = "Enter the backup path";
            this.tbBackupPath.ReadOnly = true;
            this.tbBackupPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbBackupPath.SelectedText = "";
            this.tbBackupPath.SelectionLength = 0;
            this.tbBackupPath.SelectionStart = 0;
            this.tbBackupPath.ShortcutsEnabled = true;
            this.tbBackupPath.ShowClearButton = true;
            this.tbBackupPath.Size = new System.Drawing.Size(622, 32);
            this.tbBackupPath.TabIndex = 1;
            this.tbBackupPath.Tag = "tbBackupPath";
            this.tbBackupPath.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbBackupPath.UseSelectable = true;
            this.tbBackupPath.WaterMark = "Enter the backup path";
            this.tbBackupPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbBackupPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblBackupDatabase
            // 
            this.lblBackupDatabase.AutoSize = true;
            this.lblBackupDatabase.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblBackupDatabase.Location = new System.Drawing.Point(41, 39);
            this.lblBackupDatabase.Name = "lblBackupDatabase";
            this.lblBackupDatabase.Size = new System.Drawing.Size(146, 25);
            this.lblBackupDatabase.TabIndex = 10;
            this.lblBackupDatabase.Tag = "lblBackupDatabase";
            this.lblBackupDatabase.Text = "Backup Database:";
            // 
            // btnSelectBackupPath
            // 
            this.btnSelectBackupPath.Location = new System.Drawing.Point(683, 78);
            this.btnSelectBackupPath.Name = "btnSelectBackupPath";
            this.btnSelectBackupPath.Size = new System.Drawing.Size(121, 32);
            this.btnSelectBackupPath.TabIndex = 11;
            this.btnSelectBackupPath.Tag = "btnSelectBackupPath";
            this.btnSelectBackupPath.Text = "Select Path";
            this.btnSelectBackupPath.UseSelectable = true;
            this.btnSelectBackupPath.Click += new System.EventHandler(this.btnSelectBackupPath_Click);
            // 
            // btnBackupDatabase
            // 
            this.btnBackupDatabase.Location = new System.Drawing.Point(335, 141);
            this.btnBackupDatabase.Name = "btnBackupDatabase";
            this.btnBackupDatabase.Size = new System.Drawing.Size(166, 36);
            this.btnBackupDatabase.TabIndex = 12;
            this.btnBackupDatabase.Tag = "btnBackupDatabase";
            this.btnBackupDatabase.Text = "Backup Database";
            this.btnBackupDatabase.UseSelectable = true;
            this.btnBackupDatabase.Click += new System.EventHandler(this.btnBackupDatabase_Click);
            // 
            // btnRestoreDatabase
            // 
            this.btnRestoreDatabase.Location = new System.Drawing.Point(335, 318);
            this.btnRestoreDatabase.Name = "btnRestoreDatabase";
            this.btnRestoreDatabase.Size = new System.Drawing.Size(166, 36);
            this.btnRestoreDatabase.TabIndex = 16;
            this.btnRestoreDatabase.Tag = "btnRestoreDatabase";
            this.btnRestoreDatabase.Text = "Restore Database";
            this.btnRestoreDatabase.UseSelectable = true;
            this.btnRestoreDatabase.Click += new System.EventHandler(this.btnRestoreDatabase_Click);
            // 
            // btnSelectRestorePath
            // 
            this.btnSelectRestorePath.Location = new System.Drawing.Point(683, 255);
            this.btnSelectRestorePath.Name = "btnSelectRestorePath";
            this.btnSelectRestorePath.Size = new System.Drawing.Size(121, 32);
            this.btnSelectRestorePath.TabIndex = 15;
            this.btnSelectRestorePath.Tag = "btnSelectRestorePath";
            this.btnSelectRestorePath.Text = "Select Path";
            this.btnSelectRestorePath.UseSelectable = true;
            this.btnSelectRestorePath.Click += new System.EventHandler(this.btnSelectRestorePath_Click);
            // 
            // lblRestoreDatabase
            // 
            this.lblRestoreDatabase.AutoSize = true;
            this.lblRestoreDatabase.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblRestoreDatabase.Location = new System.Drawing.Point(41, 216);
            this.lblRestoreDatabase.Name = "lblRestoreDatabase";
            this.lblRestoreDatabase.Size = new System.Drawing.Size(147, 25);
            this.lblRestoreDatabase.TabIndex = 14;
            this.lblRestoreDatabase.Tag = "lblRestoreDatabase";
            this.lblRestoreDatabase.Text = "Restore Database:";
            // 
            // tbRestorePath
            // 
            // 
            // 
            // 
            this.tbRestorePath.CustomButton.Image = null;
            this.tbRestorePath.CustomButton.Location = new System.Drawing.Point(592, 2);
            this.tbRestorePath.CustomButton.Name = "";
            this.tbRestorePath.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbRestorePath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbRestorePath.CustomButton.TabIndex = 1;
            this.tbRestorePath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRestorePath.CustomButton.UseSelectable = true;
            this.tbRestorePath.CustomButton.Visible = false;
            this.tbRestorePath.DisplayIcon = true;
            this.tbRestorePath.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbRestorePath.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbRestorePath.Lines = new string[0];
            this.tbRestorePath.Location = new System.Drawing.Point(41, 255);
            this.tbRestorePath.MaxLength = 32767;
            this.tbRestorePath.Name = "tbRestorePath";
            this.tbRestorePath.PasswordChar = '\0';
            this.tbRestorePath.PromptText = "Enter the restore path";
            this.tbRestorePath.ReadOnly = true;
            this.tbRestorePath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbRestorePath.SelectedText = "";
            this.tbRestorePath.SelectionLength = 0;
            this.tbRestorePath.SelectionStart = 0;
            this.tbRestorePath.ShortcutsEnabled = true;
            this.tbRestorePath.ShowClearButton = true;
            this.tbRestorePath.Size = new System.Drawing.Size(622, 32);
            this.tbRestorePath.TabIndex = 13;
            this.tbRestorePath.Tag = "tbRestorePath";
            this.tbRestorePath.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRestorePath.UseSelectable = true;
            this.tbRestorePath.WaterMark = "Enter the restore path";
            this.tbRestorePath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbRestorePath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // BackupRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 487);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BackupRestoreForm";
            this.Resizable = false;
            this.Tag = "BackupRestoreForm";
            this.Text = "Backup / Restore Database";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackupRestoreForm_FormClosing);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel LoginPanel;
        private MetroFramework.Controls.MetroTextBox tbBackupPath;
        private MetroFramework.Controls.MetroLabel lblBackupDatabase;
        private MetroFramework.Controls.MetroButton btnSelectBackupPath;
        private MetroFramework.Controls.MetroButton btnRestoreDatabase;
        private MetroFramework.Controls.MetroButton btnSelectRestorePath;
        private MetroFramework.Controls.MetroLabel lblRestoreDatabase;
        private MetroFramework.Controls.MetroTextBox tbRestorePath;
        private MetroFramework.Controls.MetroButton btnBackupDatabase;
    }
}

