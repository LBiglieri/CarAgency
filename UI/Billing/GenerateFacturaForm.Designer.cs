namespace UI.Vehiculos
{
    partial class GenerateFacturaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateFacturaForm));
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.ConfigureFamilies = new MetroFramework.Controls.MetroPanel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lblPriceToPay = new MetroFramework.Controls.MetroLabel();
            this.btnDeletePayment = new MetroFramework.Controls.MetroButton();
            this.btnAddPayment = new MetroFramework.Controls.MetroButton();
            this.InvoiceDetailsPanel = new MetroFramework.Controls.MetroPanel();
            this.comboReservations = new MetroFramework.Controls.MetroComboBox();
            this.tbRazonSocial = new MetroFramework.Controls.MetroTextBox();
            this.tbCUIL_CUIT_Person = new MetroFramework.Controls.MetroTextBox();
            this.personView1 = new UI.Personas.Controls.PersonaView();
            this.btnGenerateInvoice = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.ConfigureFamilies.SuspendLayout();
            this.InvoiceDetailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroGrid1
            // 
            this.metroGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(757, 208);
            this.metroGrid1.TabIndex = 5;
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            // 
            // ConfigureFamilies
            // 
            this.ConfigureFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigureFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureFamilies.Controls.Add(this.metroLabel1);
            this.ConfigureFamilies.Controls.Add(this.lblPriceToPay);
            this.ConfigureFamilies.Controls.Add(this.btnDeletePayment);
            this.ConfigureFamilies.Controls.Add(this.btnAddPayment);
            this.ConfigureFamilies.Controls.Add(this.InvoiceDetailsPanel);
            this.ConfigureFamilies.Controls.Add(this.personView1);
            this.ConfigureFamilies.Controls.Add(this.btnGenerateInvoice);
            this.ConfigureFamilies.Controls.Add(this.metroGrid1);
            this.ConfigureFamilies.HorizontalScrollbarBarColor = true;
            this.ConfigureFamilies.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.HorizontalScrollbarSize = 10;
            this.ConfigureFamilies.Location = new System.Drawing.Point(20, 60);
            this.ConfigureFamilies.Name = "ConfigureFamilies";
            this.ConfigureFamilies.Size = new System.Drawing.Size(796, 480);
            this.ConfigureFamilies.TabIndex = 0;
            this.ConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureFamilies.VerticalScrollbarBarColor = true;
            this.ConfigureFamilies.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.VerticalScrollbarSize = 10;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(18, 441);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(106, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "Price Left to Pay:";
            // 
            // lblPriceToPay
            // 
            this.lblPriceToPay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPriceToPay.AutoSize = true;
            this.lblPriceToPay.Location = new System.Drawing.Point(18, 417);
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
            this.btnDeletePayment.Tag = "btnDeletePayment";
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
            this.btnAddPayment.Tag = "btnAddPayment";
            this.btnAddPayment.Text = "Add Payment";
            this.btnAddPayment.UseSelectable = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // InvoiceDetailsPanel
            // 
            this.InvoiceDetailsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InvoiceDetailsPanel.Controls.Add(this.comboReservations);
            this.InvoiceDetailsPanel.Controls.Add(this.tbRazonSocial);
            this.InvoiceDetailsPanel.Controls.Add(this.tbCUIL_CUIT_Person);
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
            this.tbRazonSocial.PromptText = "Enter the person\'s Full Name/Razon Social";
            this.tbRazonSocial.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbRazonSocial.SelectedText = "";
            this.tbRazonSocial.SelectionLength = 0;
            this.tbRazonSocial.SelectionStart = 0;
            this.tbRazonSocial.ShortcutsEnabled = true;
            this.tbRazonSocial.Size = new System.Drawing.Size(357, 32);
            this.tbRazonSocial.TabIndex = 2;
            this.tbRazonSocial.Tag = "tbRazonSocial";
            this.tbRazonSocial.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRazonSocial.UseSelectable = true;
            this.tbRazonSocial.WaterMark = "Enter the person\'s Full Name/Razon Social";
            this.tbRazonSocial.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbRazonSocial.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbCUIL_CUIT_Person
            // 
            // 
            // 
            // 
            this.tbCUIL_CUIT_Person.CustomButton.Image = null;
            this.tbCUIL_CUIT_Person.CustomButton.Location = new System.Drawing.Point(327, 2);
            this.tbCUIL_CUIT_Person.CustomButton.Name = "";
            this.tbCUIL_CUIT_Person.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbCUIL_CUIT_Person.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbCUIL_CUIT_Person.CustomButton.TabIndex = 1;
            this.tbCUIL_CUIT_Person.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCUIL_CUIT_Person.CustomButton.UseSelectable = true;
            this.tbCUIL_CUIT_Person.CustomButton.Visible = false;
            this.tbCUIL_CUIT_Person.DisplayIcon = true;
            this.tbCUIL_CUIT_Person.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbCUIL_CUIT_Person.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbCUIL_CUIT_Person.Lines = new string[0];
            this.tbCUIL_CUIT_Person.Location = new System.Drawing.Point(11, 46);
            this.tbCUIL_CUIT_Person.MaxLength = 32767;
            this.tbCUIL_CUIT_Person.Name = "tbCUIL_CUIT_Person";
            this.tbCUIL_CUIT_Person.PasswordChar = '\0';
            this.tbCUIL_CUIT_Person.PromptText = "Enter the person\'s CUIT/CUIL";
            this.tbCUIL_CUIT_Person.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbCUIL_CUIT_Person.SelectedText = "";
            this.tbCUIL_CUIT_Person.SelectionLength = 0;
            this.tbCUIL_CUIT_Person.SelectionStart = 0;
            this.tbCUIL_CUIT_Person.ShortcutsEnabled = true;
            this.tbCUIL_CUIT_Person.Size = new System.Drawing.Size(357, 32);
            this.tbCUIL_CUIT_Person.TabIndex = 1;
            this.tbCUIL_CUIT_Person.Tag = "tbCUIL_CUIT_Person";
            this.tbCUIL_CUIT_Person.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbCUIL_CUIT_Person.UseSelectable = true;
            this.tbCUIL_CUIT_Person.WaterMark = "Enter the person\'s CUIT/CUIL";
            this.tbCUIL_CUIT_Person.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbCUIL_CUIT_Person.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // personView1
            // 
            this.personView1.Location = new System.Drawing.Point(18, 20);
            this.personView1.Name = "personView1";
            this.personView1.Size = new System.Drawing.Size(363, 132);
            this.personView1.TabIndex = 0;
            // 
            // btnGenerateInvoice
            // 
            this.btnGenerateInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateInvoice.Enabled = false;
            this.btnGenerateInvoice.Location = new System.Drawing.Point(606, 425);
            this.btnGenerateInvoice.Name = "btnGenerateInvoice";
            this.btnGenerateInvoice.Size = new System.Drawing.Size(169, 35);
            this.btnGenerateInvoice.TabIndex = 8;
            this.btnGenerateInvoice.Tag = "btnGenerateInvoice";
            this.btnGenerateInvoice.Text = "Generate Invoice";
            this.btnGenerateInvoice.UseSelectable = true;
            this.btnGenerateInvoice.Click += new System.EventHandler(this.btnGenerateInvoice_Click);
            // 
            // GenerateFacturaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 560);
            this.MinimumSize = new System.Drawing.Size(836, 560);
            this.Controls.Add(this.ConfigureFamilies);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerateFacturaForm";
            this.Tag = "GenerateFacturaForm";
            this.Text = "Generate Invoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GenerateInvoiceForm_FormClosing);
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
        private Personas.Controls.PersonaView personView1;
        private MetroFramework.Controls.MetroPanel InvoiceDetailsPanel;
        private MetroFramework.Controls.MetroTextBox tbCUIL_CUIT_Person;
        private MetroFramework.Controls.MetroTextBox tbRazonSocial;
        private MetroFramework.Controls.MetroComboBox comboReservations;
        private MetroFramework.Controls.MetroButton btnAddPayment;
        private MetroFramework.Controls.MetroButton btnDeletePayment;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel lblPriceToPay;
    }
}