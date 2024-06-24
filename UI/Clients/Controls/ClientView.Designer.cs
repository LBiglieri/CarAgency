namespace UI.Clients.Controls
{
    partial class ClientView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbDni = new MetroFramework.Controls.MetroTextBox();
            this.lblDNI = new MetroFramework.Controls.MetroLabel();
            this.lblNameSurname = new MetroFramework.Controls.MetroLabel();
            this.lblPhonePersonal = new MetroFramework.Controls.MetroLabel();
            this.lblPhoneHome = new MetroFramework.Controls.MetroLabel();
            this.lblEmail = new MetroFramework.Controls.MetroLabel();
            this.lblPlease = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // tbDni
            // 
            // 
            // 
            // 
            this.tbDni.CustomButton.Image = null;
            this.tbDni.CustomButton.Location = new System.Drawing.Point(119, 1);
            this.tbDni.CustomButton.Name = "";
            this.tbDni.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbDni.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDni.CustomButton.TabIndex = 1;
            this.tbDni.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDni.CustomButton.UseSelectable = true;
            this.tbDni.CustomButton.Visible = false;
            this.tbDni.Lines = new string[0];
            this.tbDni.Location = new System.Drawing.Point(26, 53);
            this.tbDni.MaxLength = 32767;
            this.tbDni.Name = "tbDni";
            this.tbDni.PasswordChar = '\0';
            this.tbDni.PromptText = "DNI";
            this.tbDni.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDni.SelectedText = "";
            this.tbDni.SelectionLength = 0;
            this.tbDni.SelectionStart = 0;
            this.tbDni.ShortcutsEnabled = true;
            this.tbDni.Size = new System.Drawing.Size(141, 23);
            this.tbDni.TabIndex = 0;
            this.tbDni.UseSelectable = true;
            this.tbDni.WaterMark = "DNI";
            this.tbDni.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDni.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbDni.Leave += new System.EventHandler(this.tbDni_Leave);
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(17, 15);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(98, 19);
            this.lblDNI.TabIndex = 1;
            this.lblDNI.Text = "DNI:  42432432";
            this.lblDNI.Visible = false;
            // 
            // lblNameSurname
            // 
            this.lblNameSurname.AutoSize = true;
            this.lblNameSurname.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblNameSurname.Location = new System.Drawing.Point(15, 39);
            this.lblNameSurname.Name = "lblNameSurname";
            this.lblNameSurname.Size = new System.Drawing.Size(211, 25);
            this.lblNameSurname.TabIndex = 2;
            this.lblNameSurname.Text = "Full Name: Lautaro Biglieri";
            this.lblNameSurname.Visible = false;
            // 
            // lblPhonePersonal
            // 
            this.lblPhonePersonal.AutoSize = true;
            this.lblPhonePersonal.Location = new System.Drawing.Point(17, 93);
            this.lblPhonePersonal.Name = "lblPhonePersonal";
            this.lblPhonePersonal.Size = new System.Drawing.Size(185, 19);
            this.lblPhonePersonal.TabIndex = 3;
            this.lblPhonePersonal.Text = "Personal Phone:  15442432432";
            this.lblPhonePersonal.Visible = false;
            // 
            // lblPhoneHome
            // 
            this.lblPhoneHome.AutoSize = true;
            this.lblPhoneHome.Location = new System.Drawing.Point(17, 72);
            this.lblPhoneHome.Name = "lblPhoneHome";
            this.lblPhoneHome.Size = new System.Drawing.Size(153, 19);
            this.lblPhoneHome.TabIndex = 4;
            this.lblPhoneHome.Text = "Home Phone:  42937796";
            this.lblPhoneHome.Visible = false;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(17, 113);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(200, 19);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email:  lautarobiglieri@mail.com";
            this.lblEmail.Visible = false;
            // 
            // lblPlease
            // 
            this.lblPlease.AutoSize = true;
            this.lblPlease.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblPlease.Location = new System.Drawing.Point(17, 14);
            this.lblPlease.Name = "lblPlease";
            this.lblPlease.Size = new System.Drawing.Size(325, 25);
            this.lblPlease.TabIndex = 6;
            this.lblPlease.Text = "Please write the DNI to identify the Client.";
            // 
            // ClientView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPlease);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhoneHome);
            this.Controls.Add(this.lblPhonePersonal);
            this.Controls.Add(this.lblNameSurname);
            this.Controls.Add(this.lblDNI);
            this.Controls.Add(this.tbDni);
            this.Name = "ClientView";
            this.Size = new System.Drawing.Size(363, 149);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbDni;
        private MetroFramework.Controls.MetroLabel lblDNI;
        private MetroFramework.Controls.MetroLabel lblNameSurname;
        private MetroFramework.Controls.MetroLabel lblPhonePersonal;
        private MetroFramework.Controls.MetroLabel lblPhoneHome;
        private MetroFramework.Controls.MetroLabel lblEmail;
        private MetroFramework.Controls.MetroLabel lblPlease;
    }
}
