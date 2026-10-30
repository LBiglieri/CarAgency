namespace UI.Vehicles
{
    partial class AddAcquisitionPaymentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAcquisitionPaymentForm));
            this.PaymentPanel = new MetroFramework.Controls.MetroPanel();
            this.lblPaymentMethod = new MetroFramework.Controls.MetroLabel();
            this.lblTermBalance = new MetroFramework.Controls.MetroLabel();
            this.tbAmount = new MetroFramework.Controls.MetroTextBox();
            this.tbBank = new MetroFramework.Controls.MetroTextBox();
            this.TransferPanel = new MetroFramework.Controls.MetroPanel();
            this.tbCbu_Alias = new MetroFramework.Controls.MetroTextBox();
            this.tbOperation_Number = new MetroFramework.Controls.MetroTextBox();
            this.CheckPanel = new MetroFramework.Controls.MetroPanel();
            this.tbCheck_Number = new MetroFramework.Controls.MetroTextBox();
            this.lblCheckPaymentDate = new MetroFramework.Controls.MetroLabel();
            this.dtPayment_Date = new MetroFramework.Controls.MetroDateTime();
            this.btnAddPayment = new MetroFramework.Controls.MetroButton();
            this.PaymentPanel.SuspendLayout();
            this.TransferPanel.SuspendLayout();
            this.CheckPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // PaymentPanel
            //
            this.PaymentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PaymentPanel.Controls.Add(this.lblPaymentMethod);
            this.PaymentPanel.Controls.Add(this.lblTermBalance);
            this.PaymentPanel.Controls.Add(this.tbAmount);
            this.PaymentPanel.Controls.Add(this.tbBank);
            this.PaymentPanel.Controls.Add(this.TransferPanel);
            this.PaymentPanel.Controls.Add(this.CheckPanel);
            this.PaymentPanel.Controls.Add(this.btnAddPayment);
            this.PaymentPanel.HorizontalScrollbarBarColor = true;
            this.PaymentPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.PaymentPanel.HorizontalScrollbarSize = 10;
            this.PaymentPanel.Location = new System.Drawing.Point(20, 60);
            this.PaymentPanel.Name = "PaymentPanel";
            this.PaymentPanel.Size = new System.Drawing.Size(427, 270);
            this.PaymentPanel.TabIndex = 0;
            this.PaymentPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.PaymentPanel.VerticalScrollbarBarColor = true;
            this.PaymentPanel.VerticalScrollbarHighlightOnWheel = false;
            this.PaymentPanel.VerticalScrollbarSize = 10;
            //
            // lblPaymentMethod
            //
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(13, 12);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(0, 0);
            this.lblPaymentMethod.TabIndex = 0;
            this.lblPaymentMethod.Tag = "lblPaymentMethod";
            this.lblPaymentMethod.Text = "Payment Method: ";
            this.lblPaymentMethod.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblTermBalance
            //
            this.lblTermBalance.AutoSize = true;
            this.lblTermBalance.Location = new System.Drawing.Point(13, 36);
            this.lblTermBalance.Name = "lblTermBalance";
            this.lblTermBalance.Size = new System.Drawing.Size(0, 0);
            this.lblTermBalance.TabIndex = 1;
            this.lblTermBalance.Tag = "PaymentTermBalance";
            this.lblTermBalance.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbAmount
            //
            //
            //
            //
            this.tbAmount.CustomButton.Image = null;
            this.tbAmount.CustomButton.Location = new System.Drawing.Point(364, 1);
            this.tbAmount.CustomButton.Name = "";
            this.tbAmount.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbAmount.CustomButton.TabIndex = 1;
            this.tbAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAmount.CustomButton.UseSelectable = true;
            this.tbAmount.CustomButton.Visible = false;
            this.tbAmount.Lines = new string[0];
            this.tbAmount.Location = new System.Drawing.Point(13, 66);
            this.tbAmount.MaxLength = 32767;
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.PasswordChar = '\0';
            this.tbAmount.PromptText = "Amount";
            this.tbAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbAmount.SelectedText = "";
            this.tbAmount.SelectionLength = 0;
            this.tbAmount.SelectionStart = 0;
            this.tbAmount.ShortcutsEnabled = true;
            this.tbAmount.Size = new System.Drawing.Size(386, 23);
            this.tbAmount.TabIndex = 2;
            this.tbAmount.Tag = "tbAmount";
            this.tbAmount.UseSelectable = true;
            this.tbAmount.WaterMark = "Amount";
            this.tbAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbBank
            //
            //
            //
            //
            this.tbBank.CustomButton.Image = null;
            this.tbBank.CustomButton.Location = new System.Drawing.Point(364, 1);
            this.tbBank.CustomButton.Name = "";
            this.tbBank.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbBank.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbBank.CustomButton.TabIndex = 1;
            this.tbBank.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbBank.CustomButton.UseSelectable = true;
            this.tbBank.CustomButton.Visible = false;
            this.tbBank.Lines = new string[0];
            this.tbBank.Location = new System.Drawing.Point(13, 100);
            this.tbBank.MaxLength = 100;
            this.tbBank.Name = "tbBank";
            this.tbBank.PasswordChar = '\0';
            this.tbBank.PromptText = "Bank";
            this.tbBank.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbBank.SelectedText = "";
            this.tbBank.SelectionLength = 0;
            this.tbBank.SelectionStart = 0;
            this.tbBank.ShortcutsEnabled = true;
            this.tbBank.Size = new System.Drawing.Size(386, 23);
            this.tbBank.TabIndex = 3;
            this.tbBank.Tag = "tbBank";
            this.tbBank.UseSelectable = true;
            this.tbBank.WaterMark = "Bank";
            this.tbBank.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbBank.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // TransferPanel
            //
            this.TransferPanel.Controls.Add(this.tbCbu_Alias);
            this.TransferPanel.Controls.Add(this.tbOperation_Number);
            this.TransferPanel.HorizontalScrollbarBarColor = true;
            this.TransferPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.TransferPanel.HorizontalScrollbarSize = 10;
            this.TransferPanel.Location = new System.Drawing.Point(13, 134);
            this.TransferPanel.Name = "TransferPanel";
            this.TransferPanel.Size = new System.Drawing.Size(386, 70);
            this.TransferPanel.TabIndex = 4;
            this.TransferPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TransferPanel.VerticalScrollbarBarColor = true;
            this.TransferPanel.VerticalScrollbarHighlightOnWheel = false;
            this.TransferPanel.VerticalScrollbarSize = 10;
            //
            // tbCbu_Alias
            //
            //
            //
            //
            this.tbCbu_Alias.CustomButton.Image = null;
            this.tbCbu_Alias.CustomButton.Location = new System.Drawing.Point(364, 1);
            this.tbCbu_Alias.CustomButton.Name = "";
            this.tbCbu_Alias.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbCbu_Alias.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCbu_Alias.CustomButton.TabIndex = 1;
            this.tbCbu_Alias.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCbu_Alias.CustomButton.UseSelectable = true;
            this.tbCbu_Alias.CustomButton.Visible = false;
            this.tbCbu_Alias.Lines = new string[0];
            this.tbCbu_Alias.Location = new System.Drawing.Point(0, 0);
            this.tbCbu_Alias.MaxLength = 50;
            this.tbCbu_Alias.Name = "tbCbu_Alias";
            this.tbCbu_Alias.PasswordChar = '\0';
            this.tbCbu_Alias.PromptText = "CBU or Alias";
            this.tbCbu_Alias.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCbu_Alias.SelectedText = "";
            this.tbCbu_Alias.SelectionLength = 0;
            this.tbCbu_Alias.SelectionStart = 0;
            this.tbCbu_Alias.ShortcutsEnabled = true;
            this.tbCbu_Alias.Size = new System.Drawing.Size(386, 23);
            this.tbCbu_Alias.TabIndex = 0;
            this.tbCbu_Alias.Tag = "tbCbu_Alias";
            this.tbCbu_Alias.UseSelectable = true;
            this.tbCbu_Alias.WaterMark = "CBU or Alias";
            this.tbCbu_Alias.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCbu_Alias.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbOperation_Number
            //
            //
            //
            //
            this.tbOperation_Number.CustomButton.Image = null;
            this.tbOperation_Number.CustomButton.Location = new System.Drawing.Point(364, 1);
            this.tbOperation_Number.CustomButton.Name = "";
            this.tbOperation_Number.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbOperation_Number.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbOperation_Number.CustomButton.TabIndex = 1;
            this.tbOperation_Number.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbOperation_Number.CustomButton.UseSelectable = true;
            this.tbOperation_Number.CustomButton.Visible = false;
            this.tbOperation_Number.Lines = new string[0];
            this.tbOperation_Number.Location = new System.Drawing.Point(0, 34);
            this.tbOperation_Number.MaxLength = 50;
            this.tbOperation_Number.Name = "tbOperation_Number";
            this.tbOperation_Number.PasswordChar = '\0';
            this.tbOperation_Number.PromptText = "Operation Number";
            this.tbOperation_Number.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbOperation_Number.SelectedText = "";
            this.tbOperation_Number.SelectionLength = 0;
            this.tbOperation_Number.SelectionStart = 0;
            this.tbOperation_Number.ShortcutsEnabled = true;
            this.tbOperation_Number.Size = new System.Drawing.Size(386, 23);
            this.tbOperation_Number.TabIndex = 1;
            this.tbOperation_Number.Tag = "tbOperation_Number";
            this.tbOperation_Number.UseSelectable = true;
            this.tbOperation_Number.WaterMark = "Operation Number";
            this.tbOperation_Number.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbOperation_Number.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // CheckPanel
            //
            this.CheckPanel.Controls.Add(this.tbCheck_Number);
            this.CheckPanel.Controls.Add(this.lblCheckPaymentDate);
            this.CheckPanel.Controls.Add(this.dtPayment_Date);
            this.CheckPanel.HorizontalScrollbarBarColor = true;
            this.CheckPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.CheckPanel.HorizontalScrollbarSize = 10;
            this.CheckPanel.Location = new System.Drawing.Point(13, 134);
            this.CheckPanel.Name = "CheckPanel";
            this.CheckPanel.Size = new System.Drawing.Size(386, 70);
            this.CheckPanel.TabIndex = 5;
            this.CheckPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.CheckPanel.VerticalScrollbarBarColor = true;
            this.CheckPanel.VerticalScrollbarHighlightOnWheel = false;
            this.CheckPanel.VerticalScrollbarSize = 10;
            //
            // tbCheck_Number
            //
            //
            //
            //
            this.tbCheck_Number.CustomButton.Image = null;
            this.tbCheck_Number.CustomButton.Location = new System.Drawing.Point(364, 1);
            this.tbCheck_Number.CustomButton.Name = "";
            this.tbCheck_Number.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbCheck_Number.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCheck_Number.CustomButton.TabIndex = 1;
            this.tbCheck_Number.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCheck_Number.CustomButton.UseSelectable = true;
            this.tbCheck_Number.CustomButton.Visible = false;
            this.tbCheck_Number.Lines = new string[0];
            this.tbCheck_Number.Location = new System.Drawing.Point(0, 0);
            this.tbCheck_Number.MaxLength = 30;
            this.tbCheck_Number.Name = "tbCheck_Number";
            this.tbCheck_Number.PasswordChar = '\0';
            this.tbCheck_Number.PromptText = "Check Number";
            this.tbCheck_Number.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCheck_Number.SelectedText = "";
            this.tbCheck_Number.SelectionLength = 0;
            this.tbCheck_Number.SelectionStart = 0;
            this.tbCheck_Number.ShortcutsEnabled = true;
            this.tbCheck_Number.Size = new System.Drawing.Size(386, 23);
            this.tbCheck_Number.TabIndex = 0;
            this.tbCheck_Number.Tag = "tbCheck_Number";
            this.tbCheck_Number.UseSelectable = true;
            this.tbCheck_Number.WaterMark = "Check Number";
            this.tbCheck_Number.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCheck_Number.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // lblCheckPaymentDate
            //
            this.lblCheckPaymentDate.AutoSize = true;
            this.lblCheckPaymentDate.Location = new System.Drawing.Point(0, 40);
            this.lblCheckPaymentDate.Name = "lblCheckPaymentDate";
            this.lblCheckPaymentDate.Size = new System.Drawing.Size(95, 19);
            this.lblCheckPaymentDate.TabIndex = 1;
            this.lblCheckPaymentDate.Tag = "lblCheckPaymentDate";
            this.lblCheckPaymentDate.Text = "Payment Date:";
            this.lblCheckPaymentDate.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // dtPayment_Date
            //
            this.dtPayment_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPayment_Date.Location = new System.Drawing.Point(150, 34);
            this.dtPayment_Date.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtPayment_Date.Name = "dtPayment_Date";
            this.dtPayment_Date.Size = new System.Drawing.Size(160, 29);
            this.dtPayment_Date.TabIndex = 2;
            //
            // btnAddPayment
            //
            this.btnAddPayment.Location = new System.Drawing.Point(239, 220);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(160, 35);
            this.btnAddPayment.TabIndex = 6;
            this.btnAddPayment.Tag = "btnRegisterPayment";
            this.btnAddPayment.Text = "Register Payment";
            this.btnAddPayment.UseSelectable = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            //
            // AddAcquisitionPaymentForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 350);
            this.Resizable = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.PaymentPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAcquisitionPaymentForm";
            this.Tag = "AddAcquisitionPaymentForm";
            this.Text = "Register Client Payment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddAcquisitionPaymentForm_FormClosing);
            this.PaymentPanel.ResumeLayout(false);
            this.PaymentPanel.PerformLayout();
            this.TransferPanel.ResumeLayout(false);
            this.TransferPanel.PerformLayout();
            this.CheckPanel.ResumeLayout(false);
            this.CheckPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel PaymentPanel;
        private MetroFramework.Controls.MetroLabel lblPaymentMethod;
        private MetroFramework.Controls.MetroLabel lblTermBalance;
        private MetroFramework.Controls.MetroTextBox tbAmount;
        private MetroFramework.Controls.MetroTextBox tbBank;
        private MetroFramework.Controls.MetroPanel TransferPanel;
        private MetroFramework.Controls.MetroTextBox tbCbu_Alias;
        private MetroFramework.Controls.MetroTextBox tbOperation_Number;
        private MetroFramework.Controls.MetroPanel CheckPanel;
        private MetroFramework.Controls.MetroTextBox tbCheck_Number;
        private MetroFramework.Controls.MetroLabel lblCheckPaymentDate;
        private MetroFramework.Controls.MetroDateTime dtPayment_Date;
        private MetroFramework.Controls.MetroButton btnAddPayment;
    }
}
