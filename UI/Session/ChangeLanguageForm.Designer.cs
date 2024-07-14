namespace CarAgency.UI
{
    partial class ChangeLanguageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeLanguageForm));
            this.LoginPanel = new MetroFramework.Controls.MetroPanel();
            this.comboLanguage = new MetroFramework.Controls.MetroComboBox();
            this.lblLanguage = new MetroFramework.Controls.MetroLabel();
            this.lblChangeLanguage = new MetroFramework.Controls.MetroLabel();
            this.LoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginPanel.Controls.Add(this.comboLanguage);
            this.LoginPanel.Controls.Add(this.lblLanguage);
            this.LoginPanel.Controls.Add(this.lblChangeLanguage);
            this.LoginPanel.HorizontalScrollbarBarColor = true;
            this.LoginPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.LoginPanel.HorizontalScrollbarSize = 10;
            this.LoginPanel.Location = new System.Drawing.Point(23, 63);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(528, 134);
            this.LoginPanel.TabIndex = 0;
            this.LoginPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.LoginPanel.VerticalScrollbarBarColor = true;
            this.LoginPanel.VerticalScrollbarHighlightOnWheel = false;
            this.LoginPanel.VerticalScrollbarSize = 10;
            // 
            // comboLanguage
            // 
            this.comboLanguage.FormattingEnabled = true;
            this.comboLanguage.ItemHeight = 23;
            this.comboLanguage.Location = new System.Drawing.Point(209, 69);
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Size = new System.Drawing.Size(170, 29);
            this.comboLanguage.TabIndex = 5;
            this.comboLanguage.UseSelectable = true;
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(136, 73);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(69, 19);
            this.lblLanguage.TabIndex = 4;
            this.lblLanguage.Tag = "lblLanguage";
            this.lblLanguage.Text = "Language:";
            // 
            // lblChangeLanguage
            // 
            this.lblChangeLanguage.AutoSize = true;
            this.lblChangeLanguage.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblChangeLanguage.Location = new System.Drawing.Point(160, 21);
            this.lblChangeLanguage.Name = "lblChangeLanguage";
            this.lblChangeLanguage.Size = new System.Drawing.Size(219, 25);
            this.lblChangeLanguage.TabIndex = 0;
            this.lblChangeLanguage.Text = "Change System\'s Language";
            this.lblChangeLanguage.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ChangeLanguageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 220);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChangeLanguageForm";
            this.Resizable = false;
            this.Text = "CarAgency";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangeLanguageForm_FormClosing);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel LoginPanel;
        private MetroFramework.Controls.MetroLabel lblChangeLanguage;
        private MetroFramework.Controls.MetroLabel lblLanguage;
        private MetroFramework.Controls.MetroComboBox comboLanguage;
    }
}

