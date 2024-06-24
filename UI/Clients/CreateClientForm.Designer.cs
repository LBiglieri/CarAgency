namespace CarAgency.UI
{
    partial class CreateClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateClientForm));
            this.LoginPanel = new MetroFramework.Controls.MetroPanel();
            this.lblDateBirth = new MetroFramework.Controls.MetroLabel();
            this.dtBirth = new MetroFramework.Controls.MetroDateTime();
            this.tbMail = new MetroFramework.Controls.MetroTextBox();
            this.tbPhoneHome = new MetroFramework.Controls.MetroTextBox();
            this.tbPersonalPhone = new MetroFramework.Controls.MetroTextBox();
            this.tbAddress = new MetroFramework.Controls.MetroTextBox();
            this.tbDni = new MetroFramework.Controls.MetroTextBox();
            this.tbName = new MetroFramework.Controls.MetroTextBox();
            this.btnRegisterClient = new MetroFramework.Controls.MetroButton();
            this.tbSurname = new MetroFramework.Controls.MetroTextBox();
            this.LoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginPanel.Controls.Add(this.lblDateBirth);
            this.LoginPanel.Controls.Add(this.dtBirth);
            this.LoginPanel.Controls.Add(this.tbMail);
            this.LoginPanel.Controls.Add(this.tbPhoneHome);
            this.LoginPanel.Controls.Add(this.tbPersonalPhone);
            this.LoginPanel.Controls.Add(this.tbAddress);
            this.LoginPanel.Controls.Add(this.tbDni);
            this.LoginPanel.Controls.Add(this.tbName);
            this.LoginPanel.Controls.Add(this.btnRegisterClient);
            this.LoginPanel.Controls.Add(this.tbSurname);
            this.LoginPanel.HorizontalScrollbarBarColor = true;
            this.LoginPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.LoginPanel.HorizontalScrollbarSize = 10;
            this.LoginPanel.Location = new System.Drawing.Point(23, 63);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(436, 510);
            this.LoginPanel.TabIndex = 0;
            this.LoginPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.LoginPanel.VerticalScrollbarBarColor = true;
            this.LoginPanel.VerticalScrollbarHighlightOnWheel = false;
            this.LoginPanel.VerticalScrollbarSize = 10;
            // 
            // lblDateBirth
            // 
            this.lblDateBirth.AutoSize = true;
            this.lblDateBirth.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDateBirth.Location = new System.Drawing.Point(50, 381);
            this.lblDateBirth.Name = "lblDateBirth";
            this.lblDateBirth.Size = new System.Drawing.Size(74, 15);
            this.lblDateBirth.TabIndex = 7;
            this.lblDateBirth.Text = "Date of Birth:";
            // 
            // dtBirth
            // 
            this.dtBirth.Location = new System.Drawing.Point(130, 373);
            this.dtBirth.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtBirth.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtBirth.Name = "dtBirth";
            this.dtBirth.Size = new System.Drawing.Size(268, 29);
            this.dtBirth.TabIndex = 8;
            // 
            // tbMail
            // 
            // 
            // 
            // 
            this.tbMail.CustomButton.Image = null;
            this.tbMail.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbMail.CustomButton.Name = "";
            this.tbMail.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbMail.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbMail.CustomButton.TabIndex = 1;
            this.tbMail.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbMail.CustomButton.UseSelectable = true;
            this.tbMail.CustomButton.Visible = false;
            this.tbMail.DisplayIcon = true;
            this.tbMail.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbMail.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbMail.Lines = new string[0];
            this.tbMail.Location = new System.Drawing.Point(41, 320);
            this.tbMail.MaxLength = 32767;
            this.tbMail.Name = "tbMail";
            this.tbMail.PasswordChar = '\0';
            this.tbMail.PromptText = "Enter the client\'s Email";
            this.tbMail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbMail.SelectedText = "";
            this.tbMail.SelectionLength = 0;
            this.tbMail.SelectionStart = 0;
            this.tbMail.ShortcutsEnabled = true;
            this.tbMail.Size = new System.Drawing.Size(357, 32);
            this.tbMail.TabIndex = 6;
            this.tbMail.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbMail.UseSelectable = true;
            this.tbMail.WaterMark = "Enter the client\'s Email";
            this.tbMail.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbMail.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbPhoneHome
            // 
            // 
            // 
            // 
            this.tbPhoneHome.CustomButton.Image = null;
            this.tbPhoneHome.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbPhoneHome.CustomButton.Name = "";
            this.tbPhoneHome.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbPhoneHome.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPhoneHome.CustomButton.TabIndex = 1;
            this.tbPhoneHome.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPhoneHome.CustomButton.UseSelectable = true;
            this.tbPhoneHome.CustomButton.Visible = false;
            this.tbPhoneHome.DisplayIcon = true;
            this.tbPhoneHome.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbPhoneHome.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbPhoneHome.Lines = new string[0];
            this.tbPhoneHome.Location = new System.Drawing.Point(41, 271);
            this.tbPhoneHome.MaxLength = 32767;
            this.tbPhoneHome.Name = "tbPhoneHome";
            this.tbPhoneHome.PasswordChar = '\0';
            this.tbPhoneHome.PromptText = "Enter the client\'s Home Phone";
            this.tbPhoneHome.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPhoneHome.SelectedText = "";
            this.tbPhoneHome.SelectionLength = 0;
            this.tbPhoneHome.SelectionStart = 0;
            this.tbPhoneHome.ShortcutsEnabled = true;
            this.tbPhoneHome.Size = new System.Drawing.Size(357, 32);
            this.tbPhoneHome.TabIndex = 5;
            this.tbPhoneHome.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPhoneHome.UseSelectable = true;
            this.tbPhoneHome.WaterMark = "Enter the client\'s Home Phone";
            this.tbPhoneHome.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPhoneHome.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbPersonalPhone
            // 
            // 
            // 
            // 
            this.tbPersonalPhone.CustomButton.Image = null;
            this.tbPersonalPhone.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbPersonalPhone.CustomButton.Name = "";
            this.tbPersonalPhone.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbPersonalPhone.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPersonalPhone.CustomButton.TabIndex = 1;
            this.tbPersonalPhone.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPersonalPhone.CustomButton.UseSelectable = true;
            this.tbPersonalPhone.CustomButton.Visible = false;
            this.tbPersonalPhone.DisplayIcon = true;
            this.tbPersonalPhone.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbPersonalPhone.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbPersonalPhone.Lines = new string[0];
            this.tbPersonalPhone.Location = new System.Drawing.Point(41, 221);
            this.tbPersonalPhone.MaxLength = 32767;
            this.tbPersonalPhone.Name = "tbPersonalPhone";
            this.tbPersonalPhone.PasswordChar = '\0';
            this.tbPersonalPhone.PromptText = "Enter the client\'s Mobile Phone";
            this.tbPersonalPhone.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPersonalPhone.SelectedText = "";
            this.tbPersonalPhone.SelectionLength = 0;
            this.tbPersonalPhone.SelectionStart = 0;
            this.tbPersonalPhone.ShortcutsEnabled = true;
            this.tbPersonalPhone.Size = new System.Drawing.Size(357, 32);
            this.tbPersonalPhone.TabIndex = 4;
            this.tbPersonalPhone.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPersonalPhone.UseSelectable = true;
            this.tbPersonalPhone.WaterMark = "Enter the client\'s Mobile Phone";
            this.tbPersonalPhone.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPersonalPhone.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbAddress
            // 
            // 
            // 
            // 
            this.tbAddress.CustomButton.Image = null;
            this.tbAddress.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbAddress.CustomButton.Name = "";
            this.tbAddress.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbAddress.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbAddress.CustomButton.TabIndex = 1;
            this.tbAddress.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAddress.CustomButton.UseSelectable = true;
            this.tbAddress.CustomButton.Visible = false;
            this.tbAddress.DisplayIcon = true;
            this.tbAddress.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbAddress.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbAddress.Lines = new string[0];
            this.tbAddress.Location = new System.Drawing.Point(41, 172);
            this.tbAddress.MaxLength = 32767;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.PasswordChar = '\0';
            this.tbAddress.PromptText = "Enter the client\'s Address";
            this.tbAddress.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbAddress.SelectedText = "";
            this.tbAddress.SelectionLength = 0;
            this.tbAddress.SelectionStart = 0;
            this.tbAddress.ShortcutsEnabled = true;
            this.tbAddress.Size = new System.Drawing.Size(357, 32);
            this.tbAddress.TabIndex = 3;
            this.tbAddress.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAddress.UseSelectable = true;
            this.tbAddress.WaterMark = "Enter the client\'s Address";
            this.tbAddress.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbAddress.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbDni
            // 
            // 
            // 
            // 
            this.tbDni.CustomButton.Image = null;
            this.tbDni.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbDni.CustomButton.Name = "";
            this.tbDni.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbDni.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDni.CustomButton.TabIndex = 1;
            this.tbDni.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDni.CustomButton.UseSelectable = true;
            this.tbDni.CustomButton.Visible = false;
            this.tbDni.DisplayIcon = true;
            this.tbDni.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbDni.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbDni.Lines = new string[0];
            this.tbDni.Location = new System.Drawing.Point(41, 31);
            this.tbDni.MaxLength = 32767;
            this.tbDni.Name = "tbDni";
            this.tbDni.PasswordChar = '\0';
            this.tbDni.PromptText = "Enter the client\'s DNI";
            this.tbDni.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDni.SelectedText = "";
            this.tbDni.SelectionLength = 0;
            this.tbDni.SelectionStart = 0;
            this.tbDni.ShortcutsEnabled = true;
            this.tbDni.Size = new System.Drawing.Size(357, 32);
            this.tbDni.TabIndex = 0;
            this.tbDni.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDni.UseSelectable = true;
            this.tbDni.WaterMark = "Enter the client\'s DNI";
            this.tbDni.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDni.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbName
            // 
            // 
            // 
            // 
            this.tbName.CustomButton.Image = null;
            this.tbName.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbName.CustomButton.Name = "";
            this.tbName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbName.CustomButton.TabIndex = 1;
            this.tbName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbName.CustomButton.UseSelectable = true;
            this.tbName.CustomButton.Visible = false;
            this.tbName.DisplayIcon = true;
            this.tbName.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbName.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbName.Lines = new string[0];
            this.tbName.Location = new System.Drawing.Point(41, 78);
            this.tbName.MaxLength = 32767;
            this.tbName.Name = "tbName";
            this.tbName.PasswordChar = '\0';
            this.tbName.PromptText = "Enter the client\'s Name";
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbName.SelectedText = "";
            this.tbName.SelectionLength = 0;
            this.tbName.SelectionStart = 0;
            this.tbName.ShortcutsEnabled = true;
            this.tbName.Size = new System.Drawing.Size(357, 32);
            this.tbName.TabIndex = 1;
            this.tbName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbName.UseSelectable = true;
            this.tbName.WaterMark = "Enter the client\'s Name";
            this.tbName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnRegisterClient
            // 
            this.btnRegisterClient.Location = new System.Drawing.Point(130, 445);
            this.btnRegisterClient.Name = "btnRegisterClient";
            this.btnRegisterClient.Size = new System.Drawing.Size(166, 36);
            this.btnRegisterClient.TabIndex = 9;
            this.btnRegisterClient.Text = "Register Client";
            this.btnRegisterClient.UseSelectable = true;
            this.btnRegisterClient.Click += new System.EventHandler(this.btnRegisterClient_Click);
            // 
            // tbSurname
            // 
            // 
            // 
            // 
            this.tbSurname.CustomButton.Image = null;
            this.tbSurname.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbSurname.CustomButton.Name = "";
            this.tbSurname.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbSurname.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbSurname.CustomButton.TabIndex = 1;
            this.tbSurname.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbSurname.CustomButton.UseSelectable = true;
            this.tbSurname.CustomButton.Visible = false;
            this.tbSurname.DisplayIcon = true;
            this.tbSurname.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbSurname.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbSurname.Lines = new string[0];
            this.tbSurname.Location = new System.Drawing.Point(41, 125);
            this.tbSurname.MaxLength = 32767;
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.PasswordChar = '\0';
            this.tbSurname.PromptText = "Enter the client\'s Surname";
            this.tbSurname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbSurname.SelectedText = "";
            this.tbSurname.SelectionLength = 0;
            this.tbSurname.SelectionStart = 0;
            this.tbSurname.ShortcutsEnabled = true;
            this.tbSurname.Size = new System.Drawing.Size(357, 32);
            this.tbSurname.TabIndex = 2;
            this.tbSurname.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbSurname.UseSelectable = true;
            this.tbSurname.WaterMark = "Enter the client\'s Surname";
            this.tbSurname.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbSurname.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // CreateClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 596);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CreateClientForm";
            this.Resizable = false;
            this.Text = "Register Client";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel LoginPanel;
        private MetroFramework.Controls.MetroTextBox tbSurname;
        private MetroFramework.Controls.MetroButton btnRegisterClient;
        private MetroFramework.Controls.MetroTextBox tbDni;
        private MetroFramework.Controls.MetroTextBox tbName;
        private MetroFramework.Controls.MetroTextBox tbPhoneHome;
        private MetroFramework.Controls.MetroTextBox tbPersonalPhone;
        private MetroFramework.Controls.MetroTextBox tbAddress;
        private MetroFramework.Controls.MetroLabel lblDateBirth;
        private MetroFramework.Controls.MetroDateTime dtBirth;
        private MetroFramework.Controls.MetroTextBox tbMail;
    }
}

