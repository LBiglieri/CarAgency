namespace UI.Vehicles
{
    partial class GenerateInvoiceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateInvoiceForm));
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.ConfigureFamilies = new MetroFramework.Controls.MetroPanel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lblPriceToPay = new MetroFramework.Controls.MetroLabel();
            this.btnDeletePayment = new MetroFramework.Controls.MetroButton();
            this.btnAddPayment = new MetroFramework.Controls.MetroButton();
            this.InvoiceDetailsPanel = new MetroFramework.Controls.MetroPanel();
            this.comboReservations = new MetroFramework.Controls.MetroComboBox();
            this.tbRazonSocial = new MetroFramework.Controls.MetroTextBox();
            this.tbCUIL_CUIT_Client = new MetroFramework.Controls.MetroTextBox();
            this.clientView1 = new UI.Clients.Controls.ClientView();
            this.btnGenerateInvoice = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.ConfigureFamilies.SuspendLayout();
            this.InvoiceDetailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid1.Enabled = false;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(18, 202);
            this.metroGrid1.MultiSelect = false;
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.ReadOnly = true;
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(750, 246);
            this.metroGrid1.TabIndex = 5;
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            // 
            // ConfigureFamilies
            // 
            this.ConfigureFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureFamilies.Controls.Add(this.metroLabel1);
            this.ConfigureFamilies.Controls.Add(this.lblPriceToPay);
            this.ConfigureFamilies.Controls.Add(this.btnDeletePayment);
            this.ConfigureFamilies.Controls.Add(this.btnAddPayment);
            this.ConfigureFamilies.Controls.Add(this.InvoiceDetailsPanel);
            this.ConfigureFamilies.Controls.Add(this.clientView1);
            this.ConfigureFamilies.Controls.Add(this.btnGenerateInvoice);
            this.ConfigureFamilies.Controls.Add(this.metroGrid1);
            this.ConfigureFamilies.Controls.Add(this.metroButton1);
            this.ConfigureFamilies.HorizontalScrollbarBarColor = true;
            this.ConfigureFamilies.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.HorizontalScrollbarSize = 10;
            this.ConfigureFamilies.Location = new System.Drawing.Point(23, 63);
            this.ConfigureFamilies.Name = "ConfigureFamilies";
            this.ConfigureFamilies.Size = new System.Drawing.Size(789, 516);
            this.ConfigureFamilies.TabIndex = 0;
            this.ConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureFamilies.VerticalScrollbarBarColor = true;
            this.ConfigureFamilies.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.VerticalScrollbarSize = 10;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(18, 479);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(106, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "Price Left to Pay:";
            // 
            // lblPriceToPay
            // 
            this.lblPriceToPay.AutoSize = true;
            this.lblPriceToPay.Location = new System.Drawing.Point(18, 455);
            this.lblPriceToPay.Name = "lblPriceToPay";
            this.lblPriceToPay.Size = new System.Drawing.Size(81, 19);
            this.lblPriceToPay.TabIndex = 6;
            this.lblPriceToPay.Text = "Price to Pay:";
            // 
            // btnDeletePayment
            // 
            this.btnDeletePayment.Enabled = false;
            this.btnDeletePayment.Location = new System.Drawing.Point(114, 172);
            this.btnDeletePayment.Name = "btnDeletePayment";
            this.btnDeletePayment.Size = new System.Drawing.Size(90, 24);
            this.btnDeletePayment.TabIndex = 4;
            this.btnDeletePayment.Text = "Delete Payment";
            this.btnDeletePayment.UseSelectable = true;
            this.btnDeletePayment.Click += new System.EventHandler(this.btnDeletePayment_Click);
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Enabled = false;
            this.btnAddPayment.Location = new System.Drawing.Point(18, 172);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(90, 24);
            this.btnAddPayment.TabIndex = 3;
            this.btnAddPayment.Text = "Add Payment";
            this.btnAddPayment.UseSelectable = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // InvoiceDetailsPanel
            // 
            this.InvoiceDetailsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InvoiceDetailsPanel.Controls.Add(this.comboReservations);
            this.InvoiceDetailsPanel.Controls.Add(this.tbRazonSocial);
            this.InvoiceDetailsPanel.Controls.Add(this.tbCUIL_CUIT_Client);
            this.InvoiceDetailsPanel.Enabled = false;
            this.InvoiceDetailsPanel.HorizontalScrollbarBarColor = true;
            this.InvoiceDetailsPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.InvoiceDetailsPanel.HorizontalScrollbarSize = 10;
            this.InvoiceDetailsPanel.Location = new System.Drawing.Point(387, 22);
            this.InvoiceDetailsPanel.Name = "InvoiceDetailsPanel";
            this.InvoiceDetailsPanel.Size = new System.Drawing.Size(381, 130);
            this.InvoiceDetailsPanel.TabIndex = 1;
            this.InvoiceDetailsPanel.VerticalScrollbarBarColor = true;
            this.InvoiceDetailsPanel.VerticalScrollbarHighlightOnWheel = false;
            this.InvoiceDetailsPanel.VerticalScrollbarSize = 10;
            // 
            // comboReservations
            // 
            this.comboReservations.FormattingEnabled = true;
            this.comboReservations.ItemHeight = 23;
            this.comboReservations.Location = new System.Drawing.Point(11, 11);
            this.comboReservations.Name = "comboReservations";
            this.comboReservations.Size = new System.Drawing.Size(357, 29);
            this.comboReservations.TabIndex = 0;
            this.comboReservations.UseSelectable = true;
            this.comboReservations.SelectedIndexChanged += new System.EventHandler(this.comboReservations_SelectedIndexChanged);
            // 
            // tbRazonSocial
            // 
            // 
            // 
            // 
            this.tbRazonSocial.CustomButton.Image = null;
            this.tbRazonSocial.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbRazonSocial.CustomButton.Name = "";
            this.tbRazonSocial.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbRazonSocial.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbRazonSocial.CustomButton.TabIndex = 1;
            this.tbRazonSocial.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRazonSocial.CustomButton.UseSelectable = true;
            this.tbRazonSocial.CustomButton.Visible = false;
            this.tbRazonSocial.DisplayIcon = true;
            this.tbRazonSocial.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbRazonSocial.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbRazonSocial.Lines = new string[0];
            this.tbRazonSocial.Location = new System.Drawing.Point(11, 84);
            this.tbRazonSocial.MaxLength = 32767;
            this.tbRazonSocial.Name = "tbRazonSocial";
            this.tbRazonSocial.PasswordChar = '\0';
            this.tbRazonSocial.PromptText = "Enter the client\'s Full Name/Razon Social";
            this.tbRazonSocial.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbRazonSocial.SelectedText = "";
            this.tbRazonSocial.SelectionLength = 0;
            this.tbRazonSocial.SelectionStart = 0;
            this.tbRazonSocial.ShortcutsEnabled = true;
            this.tbRazonSocial.Size = new System.Drawing.Size(357, 32);
            this.tbRazonSocial.TabIndex = 2;
            this.tbRazonSocial.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRazonSocial.UseSelectable = true;
            this.tbRazonSocial.WaterMark = "Enter the client\'s Full Name/Razon Social";
            this.tbRazonSocial.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbRazonSocial.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbCUIL_CUIT_Client
            // 
            // 
            // 
            // 
            this.tbCUIL_CUIT_Client.CustomButton.Image = null;
            this.tbCUIL_CUIT_Client.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbCUIL_CUIT_Client.CustomButton.Name = "";
            this.tbCUIL_CUIT_Client.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbCUIL_CUIT_Client.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCUIL_CUIT_Client.CustomButton.TabIndex = 1;
            this.tbCUIL_CUIT_Client.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCUIL_CUIT_Client.CustomButton.UseSelectable = true;
            this.tbCUIL_CUIT_Client.CustomButton.Visible = false;
            this.tbCUIL_CUIT_Client.DisplayIcon = true;
            this.tbCUIL_CUIT_Client.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbCUIL_CUIT_Client.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbCUIL_CUIT_Client.Lines = new string[0];
            this.tbCUIL_CUIT_Client.Location = new System.Drawing.Point(11, 46);
            this.tbCUIL_CUIT_Client.MaxLength = 32767;
            this.tbCUIL_CUIT_Client.Name = "tbCUIL_CUIT_Client";
            this.tbCUIL_CUIT_Client.PasswordChar = '\0';
            this.tbCUIL_CUIT_Client.PromptText = "Enter the client\'s CUIT/CUIL";
            this.tbCUIL_CUIT_Client.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCUIL_CUIT_Client.SelectedText = "";
            this.tbCUIL_CUIT_Client.SelectionLength = 0;
            this.tbCUIL_CUIT_Client.SelectionStart = 0;
            this.tbCUIL_CUIT_Client.ShortcutsEnabled = true;
            this.tbCUIL_CUIT_Client.Size = new System.Drawing.Size(357, 32);
            this.tbCUIL_CUIT_Client.TabIndex = 1;
            this.tbCUIL_CUIT_Client.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCUIL_CUIT_Client.UseSelectable = true;
            this.tbCUIL_CUIT_Client.WaterMark = "Enter the client\'s CUIT/CUIL";
            this.tbCUIL_CUIT_Client.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCUIL_CUIT_Client.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // clientView1
            // 
            this.clientView1.Location = new System.Drawing.Point(18, 20);
            this.clientView1.Name = "clientView1";
            this.clientView1.Size = new System.Drawing.Size(363, 132);
            this.clientView1.TabIndex = 0;
            // 
            // btnGenerateInvoice
            // 
            this.btnGenerateInvoice.Enabled = false;
            this.btnGenerateInvoice.Location = new System.Drawing.Point(599, 463);
            this.btnGenerateInvoice.Name = "btnGenerateInvoice";
            this.btnGenerateInvoice.Size = new System.Drawing.Size(169, 35);
            this.btnGenerateInvoice.TabIndex = 8;
            this.btnGenerateInvoice.Text = "Generate Invoice";
            this.btnGenerateInvoice.UseSelectable = true;
            this.btnGenerateInvoice.Click += new System.EventHandler(this.btnGenerateInvoice_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(762, 128);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(10, 24);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "Delete Payment";
            this.metroButton1.UseSelectable = true;
            // 
            // GenerateInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 600);
            this.Controls.Add(this.ConfigureFamilies);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerateInvoiceForm";
            this.Text = "Generate Invoice";
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ConfigureFamilies.ResumeLayout(false);
            this.ConfigureFamilies.PerformLayout();
            this.InvoiceDetailsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroPanel ConfigureFamilies;
        private MetroFramework.Controls.MetroButton btnGenerateInvoice;
        private Clients.Controls.ClientView clientView1;
        private MetroFramework.Controls.MetroPanel InvoiceDetailsPanel;
        private MetroFramework.Controls.MetroTextBox tbCUIL_CUIT_Client;
        private MetroFramework.Controls.MetroTextBox tbRazonSocial;
        private MetroFramework.Controls.MetroComboBox comboReservations;
        private MetroFramework.Controls.MetroButton btnAddPayment;
        private MetroFramework.Controls.MetroButton btnDeletePayment;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel lblPriceToPay;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}