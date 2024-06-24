namespace UI.Vehicles
{
    partial class AddPaymentForm
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
            this.ConfigureFamilies = new MetroFramework.Controls.MetroPanel();
            this.CardPanel = new MetroFramework.Controls.MetroPanel();
            this.tbCVV = new MetroFramework.Controls.MetroTextBox();
            this.tbExpireDate = new MetroFramework.Controls.MetroTextBox();
            this.tbCardName = new MetroFramework.Controls.MetroTextBox();
            this.tbCardNumber = new MetroFramework.Controls.MetroTextBox();
            this.InvoiceDetailsPanel = new MetroFramework.Controls.MetroPanel();
            this.comboPaymentType = new MetroFramework.Controls.MetroComboBox();
            this.tbDetails = new MetroFramework.Controls.MetroTextBox();
            this.tbAmount = new MetroFramework.Controls.MetroTextBox();
            this.btnAddPayment = new MetroFramework.Controls.MetroButton();
            this.ConfigureFamilies.SuspendLayout();
            this.CardPanel.SuspendLayout();
            this.InvoiceDetailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfigureFamilies
            // 
            this.ConfigureFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureFamilies.Controls.Add(this.CardPanel);
            this.ConfigureFamilies.Controls.Add(this.InvoiceDetailsPanel);
            this.ConfigureFamilies.Controls.Add(this.btnAddPayment);
            this.ConfigureFamilies.HorizontalScrollbarBarColor = true;
            this.ConfigureFamilies.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.HorizontalScrollbarSize = 10;
            this.ConfigureFamilies.Location = new System.Drawing.Point(23, 63);
            this.ConfigureFamilies.Name = "ConfigureFamilies";
            this.ConfigureFamilies.Size = new System.Drawing.Size(421, 377);
            this.ConfigureFamilies.TabIndex = 0;
            this.ConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureFamilies.VerticalScrollbarBarColor = true;
            this.ConfigureFamilies.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.VerticalScrollbarSize = 10;
            // 
            // CardPanel
            // 
            this.CardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CardPanel.Controls.Add(this.tbCVV);
            this.CardPanel.Controls.Add(this.tbExpireDate);
            this.CardPanel.Controls.Add(this.tbCardName);
            this.CardPanel.Controls.Add(this.tbCardNumber);
            this.CardPanel.Enabled = false;
            this.CardPanel.HorizontalScrollbarBarColor = true;
            this.CardPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.CardPanel.HorizontalScrollbarSize = 10;
            this.CardPanel.Location = new System.Drawing.Point(13, 163);
            this.CardPanel.Name = "CardPanel";
            this.CardPanel.Size = new System.Drawing.Size(386, 141);
            this.CardPanel.TabIndex = 1;
            this.CardPanel.VerticalScrollbarBarColor = true;
            this.CardPanel.VerticalScrollbarHighlightOnWheel = false;
            this.CardPanel.VerticalScrollbarSize = 10;
            // 
            // tbCVV
            // 
            // 
            // 
            // 
            this.tbCVV.CustomButton.Image = null;
            this.tbCVV.CustomButton.Location = new System.Drawing.Point(128, 2);
            this.tbCVV.CustomButton.Name = "";
            this.tbCVV.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbCVV.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCVV.CustomButton.TabIndex = 1;
            this.tbCVV.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCVV.CustomButton.UseSelectable = true;
            this.tbCVV.CustomButton.Visible = false;
            this.tbCVV.DisplayIcon = true;
            this.tbCVV.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbCVV.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbCVV.Lines = new string[0];
            this.tbCVV.Location = new System.Drawing.Point(210, 94);
            this.tbCVV.MaxLength = 32767;
            this.tbCVV.Name = "tbCVV";
            this.tbCVV.PasswordChar = '\0';
            this.tbCVV.PromptText = "Enter the card\'s CVV";
            this.tbCVV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCVV.SelectedText = "";
            this.tbCVV.SelectionLength = 0;
            this.tbCVV.SelectionStart = 0;
            this.tbCVV.ShortcutsEnabled = true;
            this.tbCVV.Size = new System.Drawing.Size(158, 32);
            this.tbCVV.TabIndex = 3;
            this.tbCVV.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCVV.UseSelectable = true;
            this.tbCVV.WaterMark = "Enter the card\'s CVV";
            this.tbCVV.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCVV.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbExpireDate
            // 
            // 
            // 
            // 
            this.tbExpireDate.CustomButton.Image = null;
            this.tbExpireDate.CustomButton.Location = new System.Drawing.Point(163, 2);
            this.tbExpireDate.CustomButton.Name = "";
            this.tbExpireDate.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbExpireDate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbExpireDate.CustomButton.TabIndex = 1;
            this.tbExpireDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbExpireDate.CustomButton.UseSelectable = true;
            this.tbExpireDate.CustomButton.Visible = false;
            this.tbExpireDate.DisplayIcon = true;
            this.tbExpireDate.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbExpireDate.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbExpireDate.Lines = new string[0];
            this.tbExpireDate.Location = new System.Drawing.Point(11, 94);
            this.tbExpireDate.MaxLength = 32767;
            this.tbExpireDate.Name = "tbExpireDate";
            this.tbExpireDate.PasswordChar = '\0';
            this.tbExpireDate.PromptText = "Enter the card\'s Expiration date";
            this.tbExpireDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbExpireDate.SelectedText = "";
            this.tbExpireDate.SelectionLength = 0;
            this.tbExpireDate.SelectionStart = 0;
            this.tbExpireDate.ShortcutsEnabled = true;
            this.tbExpireDate.Size = new System.Drawing.Size(193, 32);
            this.tbExpireDate.TabIndex = 2;
            this.tbExpireDate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbExpireDate.UseSelectable = true;
            this.tbExpireDate.WaterMark = "Enter the card\'s Expiration date";
            this.tbExpireDate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbExpireDate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbCardName
            // 
            // 
            // 
            // 
            this.tbCardName.CustomButton.Image = null;
            this.tbCardName.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbCardName.CustomButton.Name = "";
            this.tbCardName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbCardName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCardName.CustomButton.TabIndex = 1;
            this.tbCardName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCardName.CustomButton.UseSelectable = true;
            this.tbCardName.CustomButton.Visible = false;
            this.tbCardName.DisplayIcon = true;
            this.tbCardName.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbCardName.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbCardName.Lines = new string[0];
            this.tbCardName.Location = new System.Drawing.Point(11, 56);
            this.tbCardName.MaxLength = 32767;
            this.tbCardName.Name = "tbCardName";
            this.tbCardName.PasswordChar = '\0';
            this.tbCardName.PromptText = "Enter the card\'s Name";
            this.tbCardName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCardName.SelectedText = "";
            this.tbCardName.SelectionLength = 0;
            this.tbCardName.SelectionStart = 0;
            this.tbCardName.ShortcutsEnabled = true;
            this.tbCardName.Size = new System.Drawing.Size(357, 32);
            this.tbCardName.TabIndex = 1;
            this.tbCardName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCardName.UseSelectable = true;
            this.tbCardName.WaterMark = "Enter the card\'s Name";
            this.tbCardName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCardName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbCardNumber
            // 
            // 
            // 
            // 
            this.tbCardNumber.CustomButton.Image = null;
            this.tbCardNumber.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbCardNumber.CustomButton.Name = "";
            this.tbCardNumber.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbCardNumber.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCardNumber.CustomButton.TabIndex = 1;
            this.tbCardNumber.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCardNumber.CustomButton.UseSelectable = true;
            this.tbCardNumber.CustomButton.Visible = false;
            this.tbCardNumber.DisplayIcon = true;
            this.tbCardNumber.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbCardNumber.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbCardNumber.Lines = new string[0];
            this.tbCardNumber.Location = new System.Drawing.Point(11, 18);
            this.tbCardNumber.MaxLength = 32767;
            this.tbCardNumber.Name = "tbCardNumber";
            this.tbCardNumber.PasswordChar = '\0';
            this.tbCardNumber.PromptText = "Enter the card\'s number";
            this.tbCardNumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCardNumber.SelectedText = "";
            this.tbCardNumber.SelectionLength = 0;
            this.tbCardNumber.SelectionStart = 0;
            this.tbCardNumber.ShortcutsEnabled = true;
            this.tbCardNumber.Size = new System.Drawing.Size(357, 32);
            this.tbCardNumber.TabIndex = 0;
            this.tbCardNumber.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCardNumber.UseSelectable = true;
            this.tbCardNumber.WaterMark = "Enter the card\'s number";
            this.tbCardNumber.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCardNumber.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // InvoiceDetailsPanel
            // 
            this.InvoiceDetailsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InvoiceDetailsPanel.Controls.Add(this.comboPaymentType);
            this.InvoiceDetailsPanel.Controls.Add(this.tbDetails);
            this.InvoiceDetailsPanel.Controls.Add(this.tbAmount);
            this.InvoiceDetailsPanel.HorizontalScrollbarBarColor = true;
            this.InvoiceDetailsPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.InvoiceDetailsPanel.HorizontalScrollbarSize = 10;
            this.InvoiceDetailsPanel.Location = new System.Drawing.Point(13, 18);
            this.InvoiceDetailsPanel.Name = "InvoiceDetailsPanel";
            this.InvoiceDetailsPanel.Size = new System.Drawing.Size(386, 130);
            this.InvoiceDetailsPanel.TabIndex = 0;
            this.InvoiceDetailsPanel.VerticalScrollbarBarColor = true;
            this.InvoiceDetailsPanel.VerticalScrollbarHighlightOnWheel = false;
            this.InvoiceDetailsPanel.VerticalScrollbarSize = 10;
            // 
            // comboPaymentType
            // 
            this.comboPaymentType.FormattingEnabled = true;
            this.comboPaymentType.ItemHeight = 23;
            this.comboPaymentType.Location = new System.Drawing.Point(11, 11);
            this.comboPaymentType.Name = "comboPaymentType";
            this.comboPaymentType.Size = new System.Drawing.Size(357, 29);
            this.comboPaymentType.TabIndex = 0;
            this.comboPaymentType.UseSelectable = true;
            this.comboPaymentType.SelectedIndexChanged += new System.EventHandler(this.comboPaymentType_SelectedIndexChanged);
            // 
            // tbDetails
            // 
            // 
            // 
            // 
            this.tbDetails.CustomButton.Image = null;
            this.tbDetails.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbDetails.CustomButton.Name = "";
            this.tbDetails.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbDetails.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDetails.CustomButton.TabIndex = 1;
            this.tbDetails.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDetails.CustomButton.UseSelectable = true;
            this.tbDetails.CustomButton.Visible = false;
            this.tbDetails.DisplayIcon = true;
            this.tbDetails.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbDetails.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbDetails.Lines = new string[0];
            this.tbDetails.Location = new System.Drawing.Point(11, 84);
            this.tbDetails.MaxLength = 32767;
            this.tbDetails.Name = "tbDetails";
            this.tbDetails.PasswordChar = '\0';
            this.tbDetails.PromptText = "Enter the payment Details";
            this.tbDetails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDetails.SelectedText = "";
            this.tbDetails.SelectionLength = 0;
            this.tbDetails.SelectionStart = 0;
            this.tbDetails.ShortcutsEnabled = true;
            this.tbDetails.Size = new System.Drawing.Size(357, 32);
            this.tbDetails.TabIndex = 2;
            this.tbDetails.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDetails.UseSelectable = true;
            this.tbDetails.WaterMark = "Enter the payment Details";
            this.tbDetails.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDetails.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbAmount
            // 
            // 
            // 
            // 
            this.tbAmount.CustomButton.Image = null;
            this.tbAmount.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbAmount.CustomButton.Name = "";
            this.tbAmount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbAmount.CustomButton.TabIndex = 1;
            this.tbAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAmount.CustomButton.UseSelectable = true;
            this.tbAmount.CustomButton.Visible = false;
            this.tbAmount.DisplayIcon = true;
            this.tbAmount.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbAmount.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbAmount.Lines = new string[0];
            this.tbAmount.Location = new System.Drawing.Point(11, 46);
            this.tbAmount.MaxLength = 32767;
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.PasswordChar = '\0';
            this.tbAmount.PromptText = "Enter the payment amount";
            this.tbAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbAmount.SelectedText = "";
            this.tbAmount.SelectionLength = 0;
            this.tbAmount.SelectionStart = 0;
            this.tbAmount.ShortcutsEnabled = true;
            this.tbAmount.Size = new System.Drawing.Size(357, 32);
            this.tbAmount.TabIndex = 1;
            this.tbAmount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAmount.UseSelectable = true;
            this.tbAmount.WaterMark = "Enter the payment amount";
            this.tbAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Location = new System.Drawing.Point(118, 321);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(169, 35);
            this.btnAddPayment.TabIndex = 2;
            this.btnAddPayment.Text = "Add Payment";
            this.btnAddPayment.UseSelectable = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // AddPaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 465);
            this.Controls.Add(this.ConfigureFamilies);
            this.Name = "AddPaymentForm";
            this.Text = "Add Payment";
            this.ConfigureFamilies.ResumeLayout(false);
            this.CardPanel.ResumeLayout(false);
            this.InvoiceDetailsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel ConfigureFamilies;
        private MetroFramework.Controls.MetroButton btnAddPayment;
        private MetroFramework.Controls.MetroPanel InvoiceDetailsPanel;
        private MetroFramework.Controls.MetroTextBox tbAmount;
        private MetroFramework.Controls.MetroTextBox tbDetails;
        private MetroFramework.Controls.MetroComboBox comboPaymentType;
        private MetroFramework.Controls.MetroPanel CardPanel;
        private MetroFramework.Controls.MetroTextBox tbCardName;
        private MetroFramework.Controls.MetroTextBox tbCardNumber;
        private MetroFramework.Controls.MetroTextBox tbCVV;
        private MetroFramework.Controls.MetroTextBox tbExpireDate;
    }
}