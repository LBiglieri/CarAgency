namespace CarAgency.UI
{
    partial class ChangePasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            this.LoginPanel = new MetroFramework.Controls.MetroPanel();
            this.tbOldPassword = new MetroFramework.Controls.MetroTextBox();
            this.tbNewPassword = new MetroFramework.Controls.MetroTextBox();
            this.btnChangePassword = new MetroFramework.Controls.MetroButton();
            this.tbRepeatPassword = new MetroFramework.Controls.MetroTextBox();
            this.LoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginPanel.Controls.Add(this.tbOldPassword);
            this.LoginPanel.Controls.Add(this.tbNewPassword);
            this.LoginPanel.Controls.Add(this.btnChangePassword);
            this.LoginPanel.Controls.Add(this.tbRepeatPassword);
            this.LoginPanel.HorizontalScrollbarBarColor = true;
            this.LoginPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.LoginPanel.HorizontalScrollbarSize = 10;
            this.LoginPanel.Location = new System.Drawing.Point(23, 63);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(528, 264);
            this.LoginPanel.TabIndex = 0;
            this.LoginPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.LoginPanel.VerticalScrollbarBarColor = true;
            this.LoginPanel.VerticalScrollbarHighlightOnWheel = false;
            this.LoginPanel.VerticalScrollbarSize = 10;
            // 
            // tbOldPassword
            // 
            // 
            // 
            // 
            this.tbOldPassword.CustomButton.Image = null;
            this.tbOldPassword.CustomButton.Location = new System.Drawing.Point(211, 2);
            this.tbOldPassword.CustomButton.Name = "";
            this.tbOldPassword.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbOldPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbOldPassword.CustomButton.TabIndex = 1;
            this.tbOldPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbOldPassword.CustomButton.UseSelectable = true;
            this.tbOldPassword.CustomButton.Visible = false;
            this.tbOldPassword.DisplayIcon = true;
            this.tbOldPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbOldPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbOldPassword.Icon = global::UI.Properties.Resources.secured_lock__2_;
            this.tbOldPassword.Lines = new string[0];
            this.tbOldPassword.Location = new System.Drawing.Point(139, 40);
            this.tbOldPassword.MaxLength = 32767;
            this.tbOldPassword.Name = "tbOldPassword";
            this.tbOldPassword.PasswordChar = '●';
            this.tbOldPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbOldPassword.SelectedText = "";
            this.tbOldPassword.SelectionLength = 0;
            this.tbOldPassword.SelectionStart = 0;
            this.tbOldPassword.ShortcutsEnabled = true;
            this.tbOldPassword.Size = new System.Drawing.Size(241, 32);
            this.tbOldPassword.TabIndex = 0;
            this.tbOldPassword.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbOldPassword.UseSelectable = true;
            this.tbOldPassword.UseSystemPasswordChar = true;
            this.tbOldPassword.WaterMark = "Enter your Old Password";
            this.tbOldPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbOldPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbOldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbOldPassword_KeyDown);
            // 
            // tbNewPassword
            // 
            // 
            // 
            // 
            this.tbNewPassword.CustomButton.Image = null;
            this.tbNewPassword.CustomButton.Location = new System.Drawing.Point(211, 2);
            this.tbNewPassword.CustomButton.Name = "";
            this.tbNewPassword.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbNewPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbNewPassword.CustomButton.TabIndex = 1;
            this.tbNewPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbNewPassword.CustomButton.UseSelectable = true;
            this.tbNewPassword.CustomButton.Visible = false;
            this.tbNewPassword.DisplayIcon = true;
            this.tbNewPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbNewPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbNewPassword.Icon = global::UI.Properties.Resources.secured_lock__2_;
            this.tbNewPassword.Lines = new string[0];
            this.tbNewPassword.Location = new System.Drawing.Point(139, 87);
            this.tbNewPassword.MaxLength = 32767;
            this.tbNewPassword.Name = "tbNewPassword";
            this.tbNewPassword.PasswordChar = '●';
            this.tbNewPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbNewPassword.SelectedText = "";
            this.tbNewPassword.SelectionLength = 0;
            this.tbNewPassword.SelectionStart = 0;
            this.tbNewPassword.ShortcutsEnabled = true;
            this.tbNewPassword.Size = new System.Drawing.Size(241, 32);
            this.tbNewPassword.TabIndex = 1;
            this.tbNewPassword.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbNewPassword.UseSelectable = true;
            this.tbNewPassword.UseSystemPasswordChar = true;
            this.tbNewPassword.WaterMark = "Enter your New Password";
            this.tbNewPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbNewPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNewPassword_KeyDown);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(182, 199);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(153, 29);
            this.btnChangePassword.TabIndex = 3;
            this.btnChangePassword.Text = " Change Password";
            this.btnChangePassword.UseSelectable = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // tbRepeatPassword
            // 
            // 
            // 
            // 
            this.tbRepeatPassword.CustomButton.Image = null;
            this.tbRepeatPassword.CustomButton.Location = new System.Drawing.Point(211, 2);
            this.tbRepeatPassword.CustomButton.Name = "";
            this.tbRepeatPassword.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbRepeatPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbRepeatPassword.CustomButton.TabIndex = 1;
            this.tbRepeatPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRepeatPassword.CustomButton.UseSelectable = true;
            this.tbRepeatPassword.CustomButton.Visible = false;
            this.tbRepeatPassword.DisplayIcon = true;
            this.tbRepeatPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbRepeatPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbRepeatPassword.Icon = global::UI.Properties.Resources.secured_lock__2_;
            this.tbRepeatPassword.Lines = new string[0];
            this.tbRepeatPassword.Location = new System.Drawing.Point(139, 134);
            this.tbRepeatPassword.MaxLength = 32767;
            this.tbRepeatPassword.Name = "tbRepeatPassword";
            this.tbRepeatPassword.PasswordChar = '●';
            this.tbRepeatPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbRepeatPassword.SelectedText = "";
            this.tbRepeatPassword.SelectionLength = 0;
            this.tbRepeatPassword.SelectionStart = 0;
            this.tbRepeatPassword.ShortcutsEnabled = true;
            this.tbRepeatPassword.Size = new System.Drawing.Size(241, 32);
            this.tbRepeatPassword.TabIndex = 2;
            this.tbRepeatPassword.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRepeatPassword.UseSelectable = true;
            this.tbRepeatPassword.UseSystemPasswordChar = true;
            this.tbRepeatPassword.WaterMark = "Repeat your new Password";
            this.tbRepeatPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbRepeatPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbRepeatPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRepeatPassword_KeyDown);
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 350);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChangePasswordForm";
            this.Resizable = false;
            this.Text = "Please change your password";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.LoginPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel LoginPanel;
        private MetroFramework.Controls.MetroTextBox tbRepeatPassword;
        private MetroFramework.Controls.MetroButton btnChangePassword;
        private MetroFramework.Controls.MetroTextBox tbOldPassword;
        private MetroFramework.Controls.MetroTextBox tbNewPassword;
    }
}

