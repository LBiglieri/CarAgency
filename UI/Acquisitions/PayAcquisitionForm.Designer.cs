namespace UI.Vehicles
{
    partial class PayAcquisitionForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayAcquisitionForm));
            this.panelPayment = new MetroFramework.Controls.MetroPanel();
            this.clientView1 = new UI.Clients.Controls.ClientView();
            this.btnChangeClient = new MetroFramework.Controls.MetroButton();
            this.lblClientAcquisitions = new MetroFramework.Controls.MetroLabel();
            this.gridAcquisitions = new MetroFramework.Controls.MetroGrid();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLicense_Plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAgreed_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAcquisitionDetail = new MetroFramework.Controls.MetroLabel();
            this.lblAgreement = new MetroFramework.Controls.MetroLabel();
            this.lblPaymentTerms = new MetroFramework.Controls.MetroLabel();
            this.gridTerms = new MetroFramework.Controls.MetroGrid();
            this.colPaymentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTermAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTermBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblBalance = new MetroFramework.Controls.MetroLabel();
            this.btnAddPayment = new MetroFramework.Controls.MetroButton();
            this.lblPayments = new MetroFramework.Controls.MetroLabel();
            this.gridPayments = new MetroFramework.Controls.MetroGrid();
            this.colPaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStageMessage = new MetroFramework.Controls.MetroLabel();
            this.btnPrintReceipt = new MetroFramework.Controls.MetroButton();
            this.btnIssueReceipt = new MetroFramework.Controls.MetroButton();
            this.panelPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAcquisitions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTerms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayments)).BeginInit();
            this.SuspendLayout();
            //
            // panelPayment
            //
            this.panelPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPayment.Controls.Add(this.clientView1);
            this.panelPayment.Controls.Add(this.btnChangeClient);
            this.panelPayment.Controls.Add(this.lblClientAcquisitions);
            this.panelPayment.Controls.Add(this.gridAcquisitions);
            this.panelPayment.Controls.Add(this.lblAcquisitionDetail);
            this.panelPayment.Controls.Add(this.lblAgreement);
            this.panelPayment.Controls.Add(this.lblPaymentTerms);
            this.panelPayment.Controls.Add(this.gridTerms);
            this.panelPayment.Controls.Add(this.lblBalance);
            this.panelPayment.Controls.Add(this.btnAddPayment);
            this.panelPayment.Controls.Add(this.lblPayments);
            this.panelPayment.Controls.Add(this.gridPayments);
            this.panelPayment.Controls.Add(this.lblStageMessage);
            this.panelPayment.Controls.Add(this.btnPrintReceipt);
            this.panelPayment.Controls.Add(this.btnIssueReceipt);
            this.panelPayment.HorizontalScrollbarBarColor = true;
            this.panelPayment.HorizontalScrollbarHighlightOnWheel = false;
            this.panelPayment.HorizontalScrollbarSize = 10;
            this.panelPayment.Location = new System.Drawing.Point(20, 60);
            this.panelPayment.Name = "panelPayment";
            this.panelPayment.Size = new System.Drawing.Size(1180, 480);
            this.panelPayment.TabIndex = 0;
            this.panelPayment.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelPayment.VerticalScrollbarBarColor = true;
            this.panelPayment.VerticalScrollbarHighlightOnWheel = false;
            this.panelPayment.VerticalScrollbarSize = 10;
            //
            // clientView1
            //
            this.clientView1.Location = new System.Drawing.Point(18, 10);
            this.clientView1.Name = "clientView1";
            this.clientView1.Size = new System.Drawing.Size(363, 132);
            this.clientView1.TabIndex = 0;
            //
            // btnChangeClient
            //
            this.btnChangeClient.Enabled = false;
            this.btnChangeClient.Location = new System.Drawing.Point(18, 146);
            this.btnChangeClient.Name = "btnChangeClient";
            this.btnChangeClient.Size = new System.Drawing.Size(122, 25);
            this.btnChangeClient.TabIndex = 1;
            this.btnChangeClient.Tag = "btnChangeClient";
            this.btnChangeClient.Text = "Another Client";
            this.btnChangeClient.UseSelectable = true;
            this.btnChangeClient.Click += new System.EventHandler(this.btnChangeClient_Click);
            //
            // lblClientAcquisitions
            //
            this.lblClientAcquisitions.AutoSize = true;
            this.lblClientAcquisitions.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblClientAcquisitions.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblClientAcquisitions.Location = new System.Drawing.Point(18, 184);
            this.lblClientAcquisitions.Name = "lblClientAcquisitions";
            this.lblClientAcquisitions.Size = new System.Drawing.Size(165, 25);
            this.lblClientAcquisitions.TabIndex = 2;
            this.lblClientAcquisitions.Tag = "lblClientAcquisitions";
            this.lblClientAcquisitions.Text = "Client Purchases";
            this.lblClientAcquisitions.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // gridAcquisitions
            //
            this.gridAcquisitions.AllowUserToAddRows = false;
            this.gridAcquisitions.AllowUserToDeleteRows = false;
            this.gridAcquisitions.AllowUserToResizeRows = false;
            this.gridAcquisitions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left));
            this.gridAcquisitions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAcquisitions.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridAcquisitions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridAcquisitions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridAcquisitions.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridAcquisitions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAcquisitions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridAcquisitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAcquisitions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.colLicense_Plate,
            this.colAgreed_Price,
            this.colBalance,
            this.colStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridAcquisitions.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridAcquisitions.EnableHeadersVisualStyles = false;
            this.gridAcquisitions.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridAcquisitions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridAcquisitions.Location = new System.Drawing.Point(18, 214);
            this.gridAcquisitions.MultiSelect = false;
            this.gridAcquisitions.Name = "gridAcquisitions";
            this.gridAcquisitions.ReadOnly = true;
            this.gridAcquisitions.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAcquisitions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridAcquisitions.RowHeadersVisible = false;
            this.gridAcquisitions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridAcquisitions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAcquisitions.Size = new System.Drawing.Size(470, 254);
            this.gridAcquisitions.TabIndex = 3;
            this.gridAcquisitions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridAcquisitions_CellFormatting);
            this.gridAcquisitions.SelectionChanged += new System.EventHandler(this.gridAcquisitions_SelectionChanged);
            //
            // colNumber
            //
            this.colNumber.DataPropertyName = "Number";
            this.colNumber.FillWeight = 12F;
            this.colNumber.HeaderText = "No.";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Tag = "colNumber";
            //
            // colLicense_Plate
            //
            this.colLicense_Plate.DataPropertyName = "License_Plate";
            this.colLicense_Plate.FillWeight = 20F;
            this.colLicense_Plate.HeaderText = "License Plate";
            this.colLicense_Plate.Name = "colLicense_Plate";
            this.colLicense_Plate.ReadOnly = true;
            this.colLicense_Plate.Tag = "colLicense_Plate";
            //
            // colAgreed_Price
            //
            this.colAgreed_Price.DataPropertyName = "Agreed_Price";
            this.colAgreed_Price.FillWeight = 24F;
            this.colAgreed_Price.HeaderText = "Agreed Price";
            this.colAgreed_Price.Name = "colAgreed_Price";
            this.colAgreed_Price.ReadOnly = true;
            this.colAgreed_Price.Tag = "colAgreed_Price";
            //
            // colBalance
            //
            this.colBalance.DataPropertyName = "Balance";
            this.colBalance.FillWeight = 20F;
            this.colBalance.HeaderText = "Balance";
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            this.colBalance.Tag = "colBalance";
            //
            // colStatus
            //
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.FillWeight = 24F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Tag = "colStatus";
            //
            // lblAcquisitionDetail
            //
            this.lblAcquisitionDetail.AutoSize = true;
            this.lblAcquisitionDetail.Location = new System.Drawing.Point(506, 10);
            this.lblAcquisitionDetail.Name = "lblAcquisitionDetail";
            this.lblAcquisitionDetail.Size = new System.Drawing.Size(0, 0);
            this.lblAcquisitionDetail.TabIndex = 4;
            this.lblAcquisitionDetail.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblAgreement
            //
            this.lblAgreement.AutoSize = true;
            this.lblAgreement.Location = new System.Drawing.Point(506, 32);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(0, 0);
            this.lblAgreement.TabIndex = 5;
            this.lblAgreement.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblPaymentTerms
            //
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(506, 58);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(150, 19);
            this.lblPaymentTerms.TabIndex = 6;
            this.lblPaymentTerms.Tag = "lblPaymentTerms";
            this.lblPaymentTerms.Text = "Agreed Payment Terms:";
            this.lblPaymentTerms.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // gridTerms
            //
            this.gridTerms.AllowUserToAddRows = false;
            this.gridTerms.AllowUserToDeleteRows = false;
            this.gridTerms.AllowUserToResizeRows = false;
            this.gridTerms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            this.gridTerms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTerms.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridTerms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridTerms.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridTerms.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridTerms.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTerms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridTerms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTerms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPaymentType,
            this.colTermAmount,
            this.colPaidAmount,
            this.colTermBalance});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTerms.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridTerms.EnableHeadersVisualStyles = false;
            this.gridTerms.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridTerms.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridTerms.Location = new System.Drawing.Point(506, 82);
            this.gridTerms.MultiSelect = false;
            this.gridTerms.Name = "gridTerms";
            this.gridTerms.ReadOnly = true;
            this.gridTerms.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTerms.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridTerms.RowHeadersVisible = false;
            this.gridTerms.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridTerms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTerms.Size = new System.Drawing.Size(656, 110);
            this.gridTerms.TabIndex = 7;
            //
            // colPaymentType
            //
            this.colPaymentType.FillWeight = 34F;
            this.colPaymentType.HeaderText = "Payment Method";
            this.colPaymentType.Name = "colPaymentType";
            this.colPaymentType.ReadOnly = true;
            this.colPaymentType.Tag = "colPaymentType";
            //
            // colTermAmount
            //
            this.colTermAmount.FillWeight = 22F;
            this.colTermAmount.HeaderText = "Agreed";
            this.colTermAmount.Name = "colTermAmount";
            this.colTermAmount.ReadOnly = true;
            this.colTermAmount.Tag = "colAgreedAmount";
            //
            // colPaidAmount
            //
            this.colPaidAmount.FillWeight = 22F;
            this.colPaidAmount.HeaderText = "Paid";
            this.colPaidAmount.Name = "colPaidAmount";
            this.colPaidAmount.ReadOnly = true;
            this.colPaidAmount.Tag = "colPaidAmount";
            //
            // colTermBalance
            //
            this.colTermBalance.FillWeight = 22F;
            this.colTermBalance.HeaderText = "Balance";
            this.colTermBalance.Name = "colTermBalance";
            this.colTermBalance.ReadOnly = true;
            this.colTermBalance.Tag = "colBalance";
            //
            // lblBalance
            //
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(506, 200);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(0, 0);
            this.lblBalance.TabIndex = 8;
            this.lblBalance.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // btnAddPayment
            //
            this.btnAddPayment.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnAddPayment.Enabled = false;
            this.btnAddPayment.Location = new System.Drawing.Point(1002, 200);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(160, 35);
            this.btnAddPayment.TabIndex = 9;
            this.btnAddPayment.Tag = "btnRegisterPayment";
            this.btnAddPayment.Text = "Register Payment";
            this.btnAddPayment.UseSelectable = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            //
            // lblPayments
            //
            this.lblPayments.AutoSize = true;
            this.lblPayments.Location = new System.Drawing.Point(506, 250);
            this.lblPayments.Name = "lblPayments";
            this.lblPayments.Size = new System.Drawing.Size(140, 19);
            this.lblPayments.TabIndex = 10;
            this.lblPayments.Tag = "lblPayments";
            this.lblPayments.Text = "Registered Payments:";
            this.lblPayments.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // gridPayments
            //
            this.gridPayments.AllowUserToAddRows = false;
            this.gridPayments.AllowUserToDeleteRows = false;
            this.gridPayments.AllowUserToResizeRows = false;
            this.gridPayments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            this.gridPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPayments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPayments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridPayments.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridPayments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPaymentDate,
            this.colPaymentMethod,
            this.colPaymentAmount,
            this.colPaymentDetail});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPayments.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridPayments.EnableHeadersVisualStyles = false;
            this.gridPayments.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridPayments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPayments.Location = new System.Drawing.Point(506, 274);
            this.gridPayments.MultiSelect = false;
            this.gridPayments.Name = "gridPayments";
            this.gridPayments.ReadOnly = true;
            this.gridPayments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPayments.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridPayments.RowHeadersVisible = false;
            this.gridPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPayments.Size = new System.Drawing.Size(656, 146);
            this.gridPayments.TabIndex = 11;
            //
            // colPaymentDate
            //
            this.colPaymentDate.FillWeight = 18F;
            this.colPaymentDate.HeaderText = "Date";
            this.colPaymentDate.Name = "colPaymentDate";
            this.colPaymentDate.ReadOnly = true;
            this.colPaymentDate.Tag = "colPaymentDate";
            //
            // colPaymentMethod
            //
            this.colPaymentMethod.FillWeight = 20F;
            this.colPaymentMethod.HeaderText = "Payment Method";
            this.colPaymentMethod.Name = "colPaymentMethod";
            this.colPaymentMethod.ReadOnly = true;
            this.colPaymentMethod.Tag = "colPaymentType";
            //
            // colPaymentAmount
            //
            this.colPaymentAmount.FillWeight = 16F;
            this.colPaymentAmount.HeaderText = "Amount";
            this.colPaymentAmount.Name = "colPaymentAmount";
            this.colPaymentAmount.ReadOnly = true;
            this.colPaymentAmount.Tag = "colTermAmount";
            //
            // colPaymentDetail
            //
            this.colPaymentDetail.FillWeight = 46F;
            this.colPaymentDetail.HeaderText = "Detail";
            this.colPaymentDetail.Name = "colPaymentDetail";
            this.colPaymentDetail.ReadOnly = true;
            this.colPaymentDetail.Tag = "colPaymentDetail";
            //
            // lblStageMessage
            //
            this.lblStageMessage.AutoSize = true;
            this.lblStageMessage.Location = new System.Drawing.Point(506, 222);
            this.lblStageMessage.Name = "lblStageMessage";
            this.lblStageMessage.Size = new System.Drawing.Size(0, 0);
            this.lblStageMessage.TabIndex = 12;
            this.lblStageMessage.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // btnPrintReceipt
            //
            this.btnPrintReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnPrintReceipt.Enabled = false;
            this.btnPrintReceipt.Location = new System.Drawing.Point(832, 432);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(160, 35);
            this.btnPrintReceipt.TabIndex = 13;
            this.btnPrintReceipt.Tag = "btnPrintReceipt";
            this.btnPrintReceipt.Text = "Reprint Receipt";
            this.btnPrintReceipt.UseSelectable = true;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            //
            // btnIssueReceipt
            //
            this.btnIssueReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnIssueReceipt.Enabled = false;
            this.btnIssueReceipt.Location = new System.Drawing.Point(1002, 432);
            this.btnIssueReceipt.Name = "btnIssueReceipt";
            this.btnIssueReceipt.Size = new System.Drawing.Size(160, 35);
            this.btnIssueReceipt.TabIndex = 14;
            this.btnIssueReceipt.Tag = "btnIssueReceipt";
            this.btnIssueReceipt.Text = "Issue Receipt";
            this.btnIssueReceipt.UseSelectable = true;
            this.btnIssueReceipt.Click += new System.EventHandler(this.btnIssueReceipt_Click);
            //
            // PayAcquisitionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 560);
            this.MinimumSize = new System.Drawing.Size(1220, 560);
            this.Controls.Add(this.panelPayment);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PayAcquisitionForm";
            this.Tag = "PayAcquisitionForm";
            this.Text = "Pay Purchase to Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PayAcquisitionForm_FormClosing);
            this.panelPayment.ResumeLayout(false);
            this.panelPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAcquisitions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTerms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel panelPayment;
        private UI.Clients.Controls.ClientView clientView1;
        private MetroFramework.Controls.MetroButton btnChangeClient;
        private MetroFramework.Controls.MetroLabel lblClientAcquisitions;
        private MetroFramework.Controls.MetroGrid gridAcquisitions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicense_Plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAgreed_Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private MetroFramework.Controls.MetroLabel lblAcquisitionDetail;
        private MetroFramework.Controls.MetroLabel lblAgreement;
        private MetroFramework.Controls.MetroLabel lblPaymentTerms;
        private MetroFramework.Controls.MetroGrid gridTerms;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTermAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaidAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTermBalance;
        private MetroFramework.Controls.MetroLabel lblBalance;
        private MetroFramework.Controls.MetroButton btnAddPayment;
        private MetroFramework.Controls.MetroLabel lblPayments;
        private MetroFramework.Controls.MetroGrid gridPayments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentDetail;
        private MetroFramework.Controls.MetroLabel lblStageMessage;
        private MetroFramework.Controls.MetroButton btnPrintReceipt;
        private MetroFramework.Controls.MetroButton btnIssueReceipt;
    }
}
