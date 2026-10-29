namespace UI.Vehicles
{
    partial class ManageAcquisitionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageAcquisitionForm));
            this.panelManagement = new MetroFramework.Controls.MetroPanel();
            this.lblPendingAcquisitions = new MetroFramework.Controls.MetroLabel();
            this.tbFilter = new MetroFramework.Controls.MetroTextBox();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.gridPending = new MetroFramework.Controls.MetroGrid();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLicense_Plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SummaryPanel = new MetroFramework.Controls.MetroPanel();
            this.lblAcquisitionSummary = new MetroFramework.Controls.MetroLabel();
            this.lblSummaryClient = new MetroFramework.Controls.MetroLabel();
            this.lblSummaryVehicle = new MetroFramework.Controls.MetroLabel();
            this.lblSummaryAppraisal = new MetroFramework.Controls.MetroLabel();
            this.lblSummaryStatus = new MetroFramework.Controls.MetroLabel();
            this.lblStageMessage = new MetroFramework.Controls.MetroLabel();
            this.tabStages = new MetroFramework.Controls.MetroTabControl();
            this.tabInspection = new MetroFramework.Controls.MetroTabPage();
            this.inspectionView1 = new UI.Acquisitions.Controls.InspectionView();
            this.tabFormalize = new MetroFramework.Controls.MetroTabPage();
            this.lblAgreement = new MetroFramework.Controls.MetroLabel();
            this.tbAgreed_Price = new MetroFramework.Controls.MetroTextBox();
            this.tbOwner_CUIL_CUIT = new MetroFramework.Controls.MetroTextBox();
            this.lblPaymentTerms = new MetroFramework.Controls.MetroLabel();
            this.comboPaymentType = new MetroFramework.Controls.MetroComboBox();
            this.tbTermAmount = new MetroFramework.Controls.MetroTextBox();
            this.btnAddTerm = new MetroFramework.Controls.MetroButton();
            this.btnRemoveTerm = new MetroFramework.Controls.MetroButton();
            this.gridTerms = new MetroFramework.Controls.MetroGrid();
            this.colPaymentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTermAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTermsDifference = new MetroFramework.Controls.MetroLabel();
            this.btnRejectAppraisal = new MetroFramework.Controls.MetroButton();
            this.btnGenerateContract = new MetroFramework.Controls.MetroButton();
            this.btnContractNotSigned = new MetroFramework.Controls.MetroButton();
            this.btnContractSigned = new MetroFramework.Controls.MetroButton();
            this.tabDocuments = new MetroFramework.Controls.MetroTabPage();
            this.documentsView1 = new UI.Acquisitions.Controls.AcquisitionDocumentsView();
            this.tabTransfer = new MetroFramework.Controls.MetroTabPage();
            this.lblTransfer = new MetroFramework.Controls.MetroLabel();
            this.lblTransferOwner = new MetroFramework.Controls.MetroLabel();
            this.lblTransferVehicle = new MetroFramework.Controls.MetroLabel();
            this.tbPaperwork_Precharge_Code = new MetroFramework.Controls.MetroTextBox();
            this.lblTransferAppointment = new MetroFramework.Controls.MetroLabel();
            this.dtTransfer_Appointment_Date = new MetroFramework.Controls.MetroDateTime();
            this.btnScheduleTransfer = new MetroFramework.Controls.MetroButton();
            this.transferDocumentsView1 = new UI.Acquisitions.Controls.AcquisitionDocumentsView();
            this.btnRegisterTransfer = new MetroFramework.Controls.MetroButton();
            this.tabFinish = new MetroFramework.Controls.MetroTabPage();
            this.lblFinishSummary = new MetroFramework.Controls.MetroLabel();
            this.tbSummary = new MetroFramework.Controls.MetroTextBox();
            this.tbSale_Price = new MetroFramework.Controls.MetroTextBox();
            this.tbOpcionals = new MetroFramework.Controls.MetroTextBox();
            this.tbVehicleObservations = new MetroFramework.Controls.MetroTextBox();
            this.btnFinishAcquisition = new MetroFramework.Controls.MetroButton();
            this.panelManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPending)).BeginInit();
            this.SummaryPanel.SuspendLayout();
            this.tabStages.SuspendLayout();
            this.tabInspection.SuspendLayout();
            this.tabFormalize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTerms)).BeginInit();
            this.tabDocuments.SuspendLayout();
            this.tabTransfer.SuspendLayout();
            this.tabFinish.SuspendLayout();
            this.SuspendLayout();
            //
            // panelManagement
            //
            this.panelManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelManagement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelManagement.Controls.Add(this.lblPendingAcquisitions);
            this.panelManagement.Controls.Add(this.tbFilter);
            this.panelManagement.Controls.Add(this.btnSearch);
            this.panelManagement.Controls.Add(this.gridPending);
            this.panelManagement.Controls.Add(this.SummaryPanel);
            this.panelManagement.Controls.Add(this.tabStages);
            this.panelManagement.HorizontalScrollbarBarColor = true;
            this.panelManagement.HorizontalScrollbarHighlightOnWheel = false;
            this.panelManagement.HorizontalScrollbarSize = 10;
            this.panelManagement.Location = new System.Drawing.Point(20, 60);
            this.panelManagement.Name = "panelManagement";
            this.panelManagement.Size = new System.Drawing.Size(1180, 480);
            this.panelManagement.TabIndex = 0;
            this.panelManagement.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelManagement.VerticalScrollbarBarColor = true;
            this.panelManagement.VerticalScrollbarHighlightOnWheel = false;
            this.panelManagement.VerticalScrollbarSize = 10;
            //
            // lblPendingAcquisitions
            //
            this.lblPendingAcquisitions.AutoSize = true;
            this.lblPendingAcquisitions.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblPendingAcquisitions.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPendingAcquisitions.Location = new System.Drawing.Point(18, 10);
            this.lblPendingAcquisitions.Name = "lblPendingAcquisitions";
            this.lblPendingAcquisitions.Size = new System.Drawing.Size(196, 25);
            this.lblPendingAcquisitions.TabIndex = 0;
            this.lblPendingAcquisitions.Tag = "lblPendingAcquisitions";
            this.lblPendingAcquisitions.Text = "Purchases in Progress";
            this.lblPendingAcquisitions.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbFilter
            //
            //
            //
            //
            this.tbFilter.CustomButton.Image = null;
            this.tbFilter.CustomButton.Location = new System.Drawing.Point(228, 1);
            this.tbFilter.CustomButton.Name = "";
            this.tbFilter.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbFilter.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbFilter.CustomButton.TabIndex = 1;
            this.tbFilter.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbFilter.CustomButton.UseSelectable = true;
            this.tbFilter.CustomButton.Visible = false;
            this.tbFilter.Lines = new string[0];
            this.tbFilter.Location = new System.Drawing.Point(18, 45);
            this.tbFilter.MaxLength = 20;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.PasswordChar = '\0';
            this.tbFilter.PromptText = "DNI or License Plate";
            this.tbFilter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbFilter.SelectedText = "";
            this.tbFilter.SelectionLength = 0;
            this.tbFilter.SelectionStart = 0;
            this.tbFilter.ShortcutsEnabled = true;
            this.tbFilter.Size = new System.Drawing.Size(250, 23);
            this.tbFilter.TabIndex = 1;
            this.tbFilter.Tag = "tbFilter";
            this.tbFilter.UseSelectable = true;
            this.tbFilter.WaterMark = "DNI or License Plate";
            this.tbFilter.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbFilter.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFilter_KeyDown);
            //
            // btnSearch
            //
            this.btnSearch.Location = new System.Drawing.Point(276, 44);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(122, 25);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Tag = "btnSearch";
            this.btnSearch.Text = "Search";
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // gridPending
            //
            this.gridPending.AllowUserToAddRows = false;
            this.gridPending.AllowUserToDeleteRows = false;
            this.gridPending.AllowUserToResizeRows = false;
            this.gridPending.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridPending.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPending.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPending.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPending.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridPending.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridPending.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPending.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.colDni,
            this.colLicense_Plate,
            this.colStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPending.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridPending.EnableHeadersVisualStyles = false;
            this.gridPending.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridPending.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPending.Location = new System.Drawing.Point(18, 76);
            this.gridPending.MultiSelect = false;
            this.gridPending.Name = "gridPending";
            this.gridPending.ReadOnly = true;
            this.gridPending.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPending.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridPending.RowHeadersVisible = false;
            this.gridPending.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridPending.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPending.Size = new System.Drawing.Size(380, 200);
            this.gridPending.TabIndex = 3;
            this.gridPending.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridPending_CellFormatting);
            this.gridPending.SelectionChanged += new System.EventHandler(this.gridPending_SelectionChanged);
            //
            // colNumber
            //
            this.colNumber.DataPropertyName = "Number";
            this.colNumber.FillWeight = 14F;
            this.colNumber.HeaderText = "No.";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Tag = "colNumber";
            //
            // colDni
            //
            this.colDni.DataPropertyName = "Dni";
            this.colDni.FillWeight = 24F;
            this.colDni.HeaderText = "DNI";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            this.colDni.Tag = "colDni";
            //
            // colLicense_Plate
            //
            this.colLicense_Plate.DataPropertyName = "License_Plate";
            this.colLicense_Plate.FillWeight = 24F;
            this.colLicense_Plate.HeaderText = "License Plate";
            this.colLicense_Plate.Name = "colLicense_Plate";
            this.colLicense_Plate.ReadOnly = true;
            this.colLicense_Plate.Tag = "colLicense_Plate";
            //
            // colStatus
            //
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.FillWeight = 38F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Tag = "colStatus";
            //
            // SummaryPanel
            //
            this.SummaryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SummaryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SummaryPanel.Controls.Add(this.lblAcquisitionSummary);
            this.SummaryPanel.Controls.Add(this.lblSummaryClient);
            this.SummaryPanel.Controls.Add(this.lblSummaryVehicle);
            this.SummaryPanel.Controls.Add(this.lblSummaryAppraisal);
            this.SummaryPanel.Controls.Add(this.lblSummaryStatus);
            this.SummaryPanel.Controls.Add(this.lblStageMessage);
            this.SummaryPanel.HorizontalScrollbarBarColor = true;
            this.SummaryPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.SummaryPanel.HorizontalScrollbarSize = 10;
            this.SummaryPanel.Location = new System.Drawing.Point(18, 284);
            this.SummaryPanel.Name = "SummaryPanel";
            this.SummaryPanel.Size = new System.Drawing.Size(380, 184);
            this.SummaryPanel.TabIndex = 4;
            this.SummaryPanel.VerticalScrollbarBarColor = true;
            this.SummaryPanel.VerticalScrollbarHighlightOnWheel = false;
            this.SummaryPanel.VerticalScrollbarSize = 10;
            //
            // lblAcquisitionSummary
            //
            this.lblAcquisitionSummary.AutoSize = true;
            this.lblAcquisitionSummary.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblAcquisitionSummary.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblAcquisitionSummary.Location = new System.Drawing.Point(10, 6);
            this.lblAcquisitionSummary.Name = "lblAcquisitionSummary";
            this.lblAcquisitionSummary.Size = new System.Drawing.Size(171, 25);
            this.lblAcquisitionSummary.TabIndex = 0;
            this.lblAcquisitionSummary.Tag = "lblAcquisitionSummary";
            this.lblAcquisitionSummary.Text = "Selected Purchase";
            this.lblAcquisitionSummary.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblSummaryClient
            //
            this.lblSummaryClient.AutoSize = true;
            this.lblSummaryClient.Location = new System.Drawing.Point(10, 38);
            this.lblSummaryClient.Name = "lblSummaryClient";
            this.lblSummaryClient.Size = new System.Drawing.Size(0, 0);
            this.lblSummaryClient.TabIndex = 1;
            this.lblSummaryClient.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblSummaryVehicle
            //
            this.lblSummaryVehicle.AutoSize = true;
            this.lblSummaryVehicle.Location = new System.Drawing.Point(10, 60);
            this.lblSummaryVehicle.Name = "lblSummaryVehicle";
            this.lblSummaryVehicle.Size = new System.Drawing.Size(0, 0);
            this.lblSummaryVehicle.TabIndex = 2;
            this.lblSummaryVehicle.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblSummaryAppraisal
            //
            this.lblSummaryAppraisal.AutoSize = true;
            this.lblSummaryAppraisal.Location = new System.Drawing.Point(10, 82);
            this.lblSummaryAppraisal.Name = "lblSummaryAppraisal";
            this.lblSummaryAppraisal.Size = new System.Drawing.Size(0, 0);
            this.lblSummaryAppraisal.TabIndex = 3;
            this.lblSummaryAppraisal.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblSummaryStatus
            //
            this.lblSummaryStatus.AutoSize = true;
            this.lblSummaryStatus.Location = new System.Drawing.Point(10, 104);
            this.lblSummaryStatus.Name = "lblSummaryStatus";
            this.lblSummaryStatus.Size = new System.Drawing.Size(0, 0);
            this.lblSummaryStatus.TabIndex = 4;
            this.lblSummaryStatus.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblStageMessage
            //
            this.lblStageMessage.Location = new System.Drawing.Point(10, 128);
            this.lblStageMessage.Name = "lblStageMessage";
            this.lblStageMessage.Size = new System.Drawing.Size(358, 48);
            this.lblStageMessage.Style = MetroFramework.MetroColorStyle.Red;
            this.lblStageMessage.TabIndex = 5;
            this.lblStageMessage.Theme = MetroFramework.MetroThemeStyle.Light;
            this.lblStageMessage.UseStyleColors = true;
            this.lblStageMessage.WrapToLine = true;
            //
            // tabStages
            //
            this.tabStages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabStages.Controls.Add(this.tabInspection);
            this.tabStages.Controls.Add(this.tabFormalize);
            this.tabStages.Controls.Add(this.tabDocuments);
            this.tabStages.Controls.Add(this.tabTransfer);
            this.tabStages.Controls.Add(this.tabFinish);
            this.tabStages.Location = new System.Drawing.Point(410, 10);
            this.tabStages.Name = "tabStages";
            this.tabStages.SelectedIndex = 0;
            this.tabStages.Size = new System.Drawing.Size(752, 458);
            this.tabStages.TabIndex = 5;
            this.tabStages.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tabStages.UseSelectable = true;
            this.tabStages.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabStages_Selecting);
            //
            // tabInspection
            //
            this.tabInspection.Controls.Add(this.inspectionView1);
            this.tabInspection.HorizontalScrollbarBarColor = true;
            this.tabInspection.HorizontalScrollbarHighlightOnWheel = false;
            this.tabInspection.HorizontalScrollbarSize = 10;
            this.tabInspection.Location = new System.Drawing.Point(4, 38);
            this.tabInspection.Name = "tabInspection";
            this.tabInspection.Size = new System.Drawing.Size(744, 416);
            this.tabInspection.TabIndex = 0;
            this.tabInspection.Tag = "tabInspection";
            this.tabInspection.Text = "Inspection";
            this.tabInspection.VerticalScrollbarBarColor = true;
            this.tabInspection.VerticalScrollbarHighlightOnWheel = false;
            this.tabInspection.VerticalScrollbarSize = 10;
            //
            // inspectionView1
            //
            this.inspectionView1.BackColor = System.Drawing.Color.White;
            this.inspectionView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inspectionView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inspectionView1.Location = new System.Drawing.Point(0, 0);
            this.inspectionView1.Name = "inspectionView1";
            this.inspectionView1.Size = new System.Drawing.Size(744, 416);
            this.inspectionView1.TabIndex = 0;
            //
            // tabFormalize
            //
            this.tabFormalize.Controls.Add(this.lblAgreement);
            this.tabFormalize.Controls.Add(this.tbAgreed_Price);
            this.tabFormalize.Controls.Add(this.tbOwner_CUIL_CUIT);
            this.tabFormalize.Controls.Add(this.lblPaymentTerms);
            this.tabFormalize.Controls.Add(this.comboPaymentType);
            this.tabFormalize.Controls.Add(this.tbTermAmount);
            this.tabFormalize.Controls.Add(this.btnAddTerm);
            this.tabFormalize.Controls.Add(this.btnRemoveTerm);
            this.tabFormalize.Controls.Add(this.gridTerms);
            this.tabFormalize.Controls.Add(this.lblTermsDifference);
            this.tabFormalize.Controls.Add(this.btnRejectAppraisal);
            this.tabFormalize.Controls.Add(this.btnGenerateContract);
            this.tabFormalize.Controls.Add(this.btnContractNotSigned);
            this.tabFormalize.Controls.Add(this.btnContractSigned);
            this.tabFormalize.HorizontalScrollbarBarColor = true;
            this.tabFormalize.HorizontalScrollbarHighlightOnWheel = false;
            this.tabFormalize.HorizontalScrollbarSize = 10;
            this.tabFormalize.Location = new System.Drawing.Point(4, 38);
            this.tabFormalize.Name = "tabFormalize";
            this.tabFormalize.Size = new System.Drawing.Size(744, 416);
            this.tabFormalize.TabIndex = 1;
            this.tabFormalize.Tag = "tabFormalize";
            this.tabFormalize.Text = "Formalize";
            this.tabFormalize.VerticalScrollbarBarColor = true;
            this.tabFormalize.VerticalScrollbarHighlightOnWheel = false;
            this.tabFormalize.VerticalScrollbarSize = 10;
            //
            // lblAgreement
            //
            this.lblAgreement.AutoSize = true;
            this.lblAgreement.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblAgreement.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblAgreement.Location = new System.Drawing.Point(10, 8);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(185, 25);
            this.lblAgreement.TabIndex = 0;
            this.lblAgreement.Tag = "lblAgreement";
            this.lblAgreement.Text = "Purchase Agreement";
            this.lblAgreement.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbAgreed_Price
            //
            //
            //
            //
            this.tbAgreed_Price.CustomButton.Image = null;
            this.tbAgreed_Price.CustomButton.Location = new System.Drawing.Point(178, 1);
            this.tbAgreed_Price.CustomButton.Name = "";
            this.tbAgreed_Price.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbAgreed_Price.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbAgreed_Price.CustomButton.TabIndex = 1;
            this.tbAgreed_Price.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAgreed_Price.CustomButton.UseSelectable = true;
            this.tbAgreed_Price.CustomButton.Visible = false;
            this.tbAgreed_Price.Lines = new string[0];
            this.tbAgreed_Price.Location = new System.Drawing.Point(10, 44);
            this.tbAgreed_Price.MaxLength = 12;
            this.tbAgreed_Price.Name = "tbAgreed_Price";
            this.tbAgreed_Price.PasswordChar = '\0';
            this.tbAgreed_Price.PromptText = "Agreed Price";
            this.tbAgreed_Price.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbAgreed_Price.SelectedText = "";
            this.tbAgreed_Price.SelectionLength = 0;
            this.tbAgreed_Price.SelectionStart = 0;
            this.tbAgreed_Price.ShortcutsEnabled = true;
            this.tbAgreed_Price.Size = new System.Drawing.Size(200, 23);
            this.tbAgreed_Price.TabIndex = 1;
            this.tbAgreed_Price.Tag = "tbAgreed_Price";
            this.tbAgreed_Price.UseSelectable = true;
            this.tbAgreed_Price.WaterMark = "Agreed Price";
            this.tbAgreed_Price.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbAgreed_Price.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbAgreed_Price.TextChanged += new System.EventHandler(this.tbAgreement_TextChanged);
            //
            // tbOwner_CUIL_CUIT
            //
            //
            //
            //
            this.tbOwner_CUIL_CUIT.CustomButton.Image = null;
            this.tbOwner_CUIL_CUIT.CustomButton.Location = new System.Drawing.Point(198, 1);
            this.tbOwner_CUIL_CUIT.CustomButton.Name = "";
            this.tbOwner_CUIL_CUIT.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbOwner_CUIL_CUIT.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbOwner_CUIL_CUIT.CustomButton.TabIndex = 1;
            this.tbOwner_CUIL_CUIT.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbOwner_CUIL_CUIT.CustomButton.UseSelectable = true;
            this.tbOwner_CUIL_CUIT.CustomButton.Visible = false;
            this.tbOwner_CUIL_CUIT.Lines = new string[0];
            this.tbOwner_CUIL_CUIT.Location = new System.Drawing.Point(220, 44);
            this.tbOwner_CUIL_CUIT.MaxLength = 13;
            this.tbOwner_CUIL_CUIT.Name = "tbOwner_CUIL_CUIT";
            this.tbOwner_CUIL_CUIT.PasswordChar = '\0';
            this.tbOwner_CUIL_CUIT.PromptText = "Holder\'s CUIL/CUIT";
            this.tbOwner_CUIL_CUIT.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbOwner_CUIL_CUIT.SelectedText = "";
            this.tbOwner_CUIL_CUIT.SelectionLength = 0;
            this.tbOwner_CUIL_CUIT.SelectionStart = 0;
            this.tbOwner_CUIL_CUIT.ShortcutsEnabled = true;
            this.tbOwner_CUIL_CUIT.Size = new System.Drawing.Size(220, 23);
            this.tbOwner_CUIL_CUIT.TabIndex = 2;
            this.tbOwner_CUIL_CUIT.Tag = "tbOwner_CUIL_CUIT";
            this.tbOwner_CUIL_CUIT.UseSelectable = true;
            this.tbOwner_CUIL_CUIT.WaterMark = "Holder\'s CUIL/CUIT";
            this.tbOwner_CUIL_CUIT.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbOwner_CUIL_CUIT.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbOwner_CUIL_CUIT.TextChanged += new System.EventHandler(this.tbAgreement_TextChanged);
            //
            // lblPaymentTerms
            //
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(10, 80);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(150, 19);
            this.lblPaymentTerms.TabIndex = 3;
            this.lblPaymentTerms.Tag = "lblPaymentTerms";
            this.lblPaymentTerms.Text = "Agreed Payment Terms:";
            this.lblPaymentTerms.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboPaymentType
            //
            this.comboPaymentType.FormattingEnabled = true;
            this.comboPaymentType.ItemHeight = 23;
            this.comboPaymentType.Location = new System.Drawing.Point(10, 104);
            this.comboPaymentType.Name = "comboPaymentType";
            this.comboPaymentType.Size = new System.Drawing.Size(200, 29);
            this.comboPaymentType.TabIndex = 4;
            this.comboPaymentType.UseSelectable = true;
            //
            // tbTermAmount
            //
            //
            //
            //
            this.tbTermAmount.CustomButton.Image = null;
            this.tbTermAmount.CustomButton.Location = new System.Drawing.Point(128, 1);
            this.tbTermAmount.CustomButton.Name = "";
            this.tbTermAmount.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbTermAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbTermAmount.CustomButton.TabIndex = 1;
            this.tbTermAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbTermAmount.CustomButton.UseSelectable = true;
            this.tbTermAmount.CustomButton.Visible = false;
            this.tbTermAmount.Lines = new string[0];
            this.tbTermAmount.Location = new System.Drawing.Point(220, 107);
            this.tbTermAmount.MaxLength = 12;
            this.tbTermAmount.Name = "tbTermAmount";
            this.tbTermAmount.PasswordChar = '\0';
            this.tbTermAmount.PromptText = "Amount";
            this.tbTermAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbTermAmount.SelectedText = "";
            this.tbTermAmount.SelectionLength = 0;
            this.tbTermAmount.SelectionStart = 0;
            this.tbTermAmount.ShortcutsEnabled = true;
            this.tbTermAmount.Size = new System.Drawing.Size(150, 23);
            this.tbTermAmount.TabIndex = 5;
            this.tbTermAmount.Tag = "tbTermAmount";
            this.tbTermAmount.UseSelectable = true;
            this.tbTermAmount.WaterMark = "Amount";
            this.tbTermAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbTermAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // btnAddTerm
            //
            this.btnAddTerm.Location = new System.Drawing.Point(380, 106);
            this.btnAddTerm.Name = "btnAddTerm";
            this.btnAddTerm.Size = new System.Drawing.Size(130, 25);
            this.btnAddTerm.TabIndex = 6;
            this.btnAddTerm.Tag = "btnAddTerm";
            this.btnAddTerm.Text = "Add Payment Method";
            this.btnAddTerm.UseSelectable = true;
            this.btnAddTerm.Click += new System.EventHandler(this.btnAddTerm_Click);
            //
            // btnRemoveTerm
            //
            this.btnRemoveTerm.Location = new System.Drawing.Point(520, 106);
            this.btnRemoveTerm.Name = "btnRemoveTerm";
            this.btnRemoveTerm.Size = new System.Drawing.Size(130, 25);
            this.btnRemoveTerm.TabIndex = 7;
            this.btnRemoveTerm.Tag = "btnRemoveTerm";
            this.btnRemoveTerm.Text = "Remove Payment Method";
            this.btnRemoveTerm.UseSelectable = true;
            this.btnRemoveTerm.Click += new System.EventHandler(this.btnRemoveTerm_Click);
            //
            // gridTerms
            //
            this.gridTerms.AllowUserToAddRows = false;
            this.gridTerms.AllowUserToDeleteRows = false;
            this.gridTerms.AllowUserToResizeRows = false;
            this.gridTerms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.colTermAmount});
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
            this.gridTerms.Location = new System.Drawing.Point(10, 142);
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
            this.gridTerms.Size = new System.Drawing.Size(724, 170);
            this.gridTerms.TabIndex = 8;
            //
            // colPaymentType
            //
            this.colPaymentType.FillWeight = 60F;
            this.colPaymentType.HeaderText = "Payment Method";
            this.colPaymentType.Name = "colPaymentType";
            this.colPaymentType.ReadOnly = true;
            this.colPaymentType.Tag = "colPaymentType";
            //
            // colTermAmount
            //
            this.colTermAmount.FillWeight = 40F;
            this.colTermAmount.HeaderText = "Amount";
            this.colTermAmount.Name = "colTermAmount";
            this.colTermAmount.ReadOnly = true;
            this.colTermAmount.Tag = "colTermAmount";
            //
            // lblTermsDifference
            //
            this.lblTermsDifference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTermsDifference.AutoSize = true;
            this.lblTermsDifference.Location = new System.Drawing.Point(10, 322);
            this.lblTermsDifference.Name = "lblTermsDifference";
            this.lblTermsDifference.Size = new System.Drawing.Size(0, 0);
            this.lblTermsDifference.TabIndex = 9;
            this.lblTermsDifference.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // btnRejectAppraisal
            //
            this.btnRejectAppraisal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRejectAppraisal.Location = new System.Drawing.Point(10, 368);
            this.btnRejectAppraisal.Name = "btnRejectAppraisal";
            this.btnRejectAppraisal.Size = new System.Drawing.Size(160, 35);
            this.btnRejectAppraisal.TabIndex = 10;
            this.btnRejectAppraisal.Tag = "btnRejectAppraisal";
            this.btnRejectAppraisal.Text = "Client Rejects Appraisal";
            this.btnRejectAppraisal.UseSelectable = true;
            this.btnRejectAppraisal.Click += new System.EventHandler(this.btnRejectAppraisal_Click);
            //
            // btnGenerateContract
            //
            this.btnGenerateContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateContract.Location = new System.Drawing.Point(234, 368);
            this.btnGenerateContract.Name = "btnGenerateContract";
            this.btnGenerateContract.Size = new System.Drawing.Size(160, 35);
            this.btnGenerateContract.TabIndex = 11;
            this.btnGenerateContract.Tag = "btnGenerateContract";
            this.btnGenerateContract.Text = "Generate Contract";
            this.btnGenerateContract.UseSelectable = true;
            this.btnGenerateContract.Click += new System.EventHandler(this.btnGenerateContract_Click);
            //
            // btnContractNotSigned
            //
            this.btnContractNotSigned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContractNotSigned.Enabled = false;
            this.btnContractNotSigned.Location = new System.Drawing.Point(404, 368);
            this.btnContractNotSigned.Name = "btnContractNotSigned";
            this.btnContractNotSigned.Size = new System.Drawing.Size(160, 35);
            this.btnContractNotSigned.TabIndex = 12;
            this.btnContractNotSigned.Tag = "btnContractNotSigned";
            this.btnContractNotSigned.Text = "Client Does Not Sign";
            this.btnContractNotSigned.UseSelectable = true;
            this.btnContractNotSigned.Click += new System.EventHandler(this.btnContractNotSigned_Click);
            //
            // btnContractSigned
            //
            this.btnContractSigned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContractSigned.Enabled = false;
            this.btnContractSigned.Location = new System.Drawing.Point(574, 368);
            this.btnContractSigned.Name = "btnContractSigned";
            this.btnContractSigned.Size = new System.Drawing.Size(160, 35);
            this.btnContractSigned.TabIndex = 13;
            this.btnContractSigned.Tag = "btnContractSigned";
            this.btnContractSigned.Text = "Register Signature";
            this.btnContractSigned.UseSelectable = true;
            this.btnContractSigned.Click += new System.EventHandler(this.btnContractSigned_Click);
            //
            // tabDocuments
            //
            this.tabDocuments.Controls.Add(this.documentsView1);
            this.tabDocuments.HorizontalScrollbarBarColor = true;
            this.tabDocuments.HorizontalScrollbarHighlightOnWheel = false;
            this.tabDocuments.HorizontalScrollbarSize = 10;
            this.tabDocuments.Location = new System.Drawing.Point(4, 38);
            this.tabDocuments.Name = "tabDocuments";
            this.tabDocuments.Size = new System.Drawing.Size(744, 416);
            this.tabDocuments.TabIndex = 2;
            this.tabDocuments.Tag = "tabDocuments";
            this.tabDocuments.Text = "Documents";
            this.tabDocuments.VerticalScrollbarBarColor = true;
            this.tabDocuments.VerticalScrollbarHighlightOnWheel = false;
            this.tabDocuments.VerticalScrollbarSize = 10;
            //
            // documentsView1
            //
            this.documentsView1.BackColor = System.Drawing.Color.White;
            this.documentsView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentsView1.Location = new System.Drawing.Point(0, 0);
            this.documentsView1.Name = "documentsView1";
            this.documentsView1.Size = new System.Drawing.Size(744, 416);
            this.documentsView1.TabIndex = 0;
            this.documentsView1.DocumentsChanged += new System.EventHandler(this.documentsView1_DocumentsChanged);
            //
            // tabTransfer
            //
            this.tabTransfer.Controls.Add(this.lblTransfer);
            this.tabTransfer.Controls.Add(this.lblTransferOwner);
            this.tabTransfer.Controls.Add(this.lblTransferVehicle);
            this.tabTransfer.Controls.Add(this.tbPaperwork_Precharge_Code);
            this.tabTransfer.Controls.Add(this.lblTransferAppointment);
            this.tabTransfer.Controls.Add(this.dtTransfer_Appointment_Date);
            this.tabTransfer.Controls.Add(this.btnScheduleTransfer);
            this.tabTransfer.Controls.Add(this.transferDocumentsView1);
            this.tabTransfer.Controls.Add(this.btnRegisterTransfer);
            this.tabTransfer.HorizontalScrollbarBarColor = true;
            this.tabTransfer.HorizontalScrollbarHighlightOnWheel = false;
            this.tabTransfer.HorizontalScrollbarSize = 10;
            this.tabTransfer.Location = new System.Drawing.Point(4, 38);
            this.tabTransfer.Name = "tabTransfer";
            this.tabTransfer.Size = new System.Drawing.Size(744, 416);
            this.tabTransfer.TabIndex = 3;
            this.tabTransfer.Tag = "tabTransfer";
            this.tabTransfer.Text = "Transfer";
            this.tabTransfer.VerticalScrollbarBarColor = true;
            this.tabTransfer.VerticalScrollbarHighlightOnWheel = false;
            this.tabTransfer.VerticalScrollbarSize = 10;
            //
            // lblTransfer
            //
            this.lblTransfer.AutoSize = true;
            this.lblTransfer.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTransfer.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblTransfer.Location = new System.Drawing.Point(10, 8);
            this.lblTransfer.Name = "lblTransfer";
            this.lblTransfer.Size = new System.Drawing.Size(150, 25);
            this.lblTransfer.TabIndex = 0;
            this.lblTransfer.Tag = "lblTransfer";
            this.lblTransfer.Text = "Ownership Transfer";
            this.lblTransfer.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblTransferOwner
            //
            this.lblTransferOwner.AutoSize = true;
            this.lblTransferOwner.Location = new System.Drawing.Point(10, 40);
            this.lblTransferOwner.Name = "lblTransferOwner";
            this.lblTransferOwner.Size = new System.Drawing.Size(0, 0);
            this.lblTransferOwner.TabIndex = 1;
            this.lblTransferOwner.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblTransferVehicle
            //
            this.lblTransferVehicle.AutoSize = true;
            this.lblTransferVehicle.Location = new System.Drawing.Point(10, 62);
            this.lblTransferVehicle.Name = "lblTransferVehicle";
            this.lblTransferVehicle.Size = new System.Drawing.Size(0, 0);
            this.lblTransferVehicle.TabIndex = 2;
            this.lblTransferVehicle.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbPaperwork_Precharge_Code
            //
            //
            //
            //
            this.tbPaperwork_Precharge_Code.CustomButton.Image = null;
            this.tbPaperwork_Precharge_Code.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbPaperwork_Precharge_Code.CustomButton.Name = "";
            this.tbPaperwork_Precharge_Code.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbPaperwork_Precharge_Code.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPaperwork_Precharge_Code.CustomButton.TabIndex = 1;
            this.tbPaperwork_Precharge_Code.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPaperwork_Precharge_Code.CustomButton.UseSelectable = true;
            this.tbPaperwork_Precharge_Code.CustomButton.Visible = false;
            this.tbPaperwork_Precharge_Code.Lines = new string[0];
            this.tbPaperwork_Precharge_Code.Location = new System.Drawing.Point(10, 92);
            this.tbPaperwork_Precharge_Code.MaxLength = 50;
            this.tbPaperwork_Precharge_Code.Name = "tbPaperwork_Precharge_Code";
            this.tbPaperwork_Precharge_Code.PasswordChar = '\0';
            this.tbPaperwork_Precharge_Code.PromptText = "Enter the 08 form precharge code";
            this.tbPaperwork_Precharge_Code.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPaperwork_Precharge_Code.SelectedText = "";
            this.tbPaperwork_Precharge_Code.SelectionLength = 0;
            this.tbPaperwork_Precharge_Code.SelectionStart = 0;
            this.tbPaperwork_Precharge_Code.ShortcutsEnabled = true;
            this.tbPaperwork_Precharge_Code.Size = new System.Drawing.Size(260, 23);
            this.tbPaperwork_Precharge_Code.TabIndex = 3;
            this.tbPaperwork_Precharge_Code.Tag = "tbPaperwork_Precharge_Code";
            this.tbPaperwork_Precharge_Code.UseSelectable = true;
            this.tbPaperwork_Precharge_Code.WaterMark = "Enter the 08 form precharge code";
            this.tbPaperwork_Precharge_Code.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPaperwork_Precharge_Code.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // lblTransferAppointment
            //
            this.lblTransferAppointment.AutoSize = true;
            this.lblTransferAppointment.Location = new System.Drawing.Point(280, 94);
            this.lblTransferAppointment.Name = "lblTransferAppointment";
            this.lblTransferAppointment.Size = new System.Drawing.Size(115, 19);
            this.lblTransferAppointment.TabIndex = 4;
            this.lblTransferAppointment.Tag = "lblTransferAppointment";
            this.lblTransferAppointment.Text = "Appointment Date:";
            this.lblTransferAppointment.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // dtTransfer_Appointment_Date
            //
            this.dtTransfer_Appointment_Date.Checked = false;
            this.dtTransfer_Appointment_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTransfer_Appointment_Date.Location = new System.Drawing.Point(400, 88);
            this.dtTransfer_Appointment_Date.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtTransfer_Appointment_Date.Name = "dtTransfer_Appointment_Date";
            this.dtTransfer_Appointment_Date.ShowCheckBox = true;
            this.dtTransfer_Appointment_Date.Size = new System.Drawing.Size(160, 29);
            this.dtTransfer_Appointment_Date.TabIndex = 5;
            //
            // btnScheduleTransfer
            //
            this.btnScheduleTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScheduleTransfer.Location = new System.Drawing.Point(574, 90);
            this.btnScheduleTransfer.Name = "btnScheduleTransfer";
            this.btnScheduleTransfer.Size = new System.Drawing.Size(160, 25);
            this.btnScheduleTransfer.TabIndex = 6;
            this.btnScheduleTransfer.Tag = "btnScheduleTransfer";
            this.btnScheduleTransfer.Text = "Save Appointment";
            this.btnScheduleTransfer.UseSelectable = true;
            this.btnScheduleTransfer.Click += new System.EventHandler(this.btnScheduleTransfer_Click);
            //
            // transferDocumentsView1
            //
            this.transferDocumentsView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferDocumentsView1.BackColor = System.Drawing.Color.White;
            this.transferDocumentsView1.Location = new System.Drawing.Point(0, 124);
            this.transferDocumentsView1.Name = "transferDocumentsView1";
            this.transferDocumentsView1.Size = new System.Drawing.Size(744, 240);
            this.transferDocumentsView1.TabIndex = 7;
            //
            // btnRegisterTransfer
            //
            this.btnRegisterTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegisterTransfer.Location = new System.Drawing.Point(574, 372);
            this.btnRegisterTransfer.Name = "btnRegisterTransfer";
            this.btnRegisterTransfer.Size = new System.Drawing.Size(160, 35);
            this.btnRegisterTransfer.TabIndex = 8;
            this.btnRegisterTransfer.Tag = "btnRegisterTransfer";
            this.btnRegisterTransfer.Text = "Register Transfer";
            this.btnRegisterTransfer.UseSelectable = true;
            this.btnRegisterTransfer.Click += new System.EventHandler(this.btnRegisterTransfer_Click);
            //
            // tabFinish
            //
            this.tabFinish.Controls.Add(this.lblFinishSummary);
            this.tabFinish.Controls.Add(this.tbSummary);
            this.tabFinish.Controls.Add(this.tbSale_Price);
            this.tabFinish.Controls.Add(this.tbOpcionals);
            this.tabFinish.Controls.Add(this.tbVehicleObservations);
            this.tabFinish.Controls.Add(this.btnFinishAcquisition);
            this.tabFinish.HorizontalScrollbarBarColor = true;
            this.tabFinish.HorizontalScrollbarHighlightOnWheel = false;
            this.tabFinish.HorizontalScrollbarSize = 10;
            this.tabFinish.Location = new System.Drawing.Point(4, 38);
            this.tabFinish.Name = "tabFinish";
            this.tabFinish.Size = new System.Drawing.Size(744, 416);
            this.tabFinish.TabIndex = 4;
            this.tabFinish.Tag = "tabFinish";
            this.tabFinish.Text = "Finish";
            this.tabFinish.VerticalScrollbarBarColor = true;
            this.tabFinish.VerticalScrollbarHighlightOnWheel = false;
            this.tabFinish.VerticalScrollbarSize = 10;
            //
            // lblFinishSummary
            //
            this.lblFinishSummary.AutoSize = true;
            this.lblFinishSummary.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFinishSummary.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFinishSummary.Location = new System.Drawing.Point(10, 8);
            this.lblFinishSummary.Name = "lblFinishSummary";
            this.lblFinishSummary.Size = new System.Drawing.Size(180, 25);
            this.lblFinishSummary.TabIndex = 0;
            this.lblFinishSummary.Tag = "lblFinishSummary";
            this.lblFinishSummary.Text = "Operation Summary";
            this.lblFinishSummary.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbSummary
            //
            //
            //
            //
            this.tbSummary.CustomButton.Image = null;
            this.tbSummary.CustomButton.Location = new System.Drawing.Point(80, 2);
            this.tbSummary.CustomButton.Name = "";
            this.tbSummary.CustomButton.Size = new System.Drawing.Size(361, 361);
            this.tbSummary.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbSummary.CustomButton.TabIndex = 1;
            this.tbSummary.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbSummary.CustomButton.UseSelectable = true;
            this.tbSummary.CustomButton.Visible = false;
            this.tbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSummary.Lines = new string[0];
            this.tbSummary.Location = new System.Drawing.Point(10, 40);
            this.tbSummary.MaxLength = 32767;
            this.tbSummary.Multiline = true;
            this.tbSummary.Name = "tbSummary";
            this.tbSummary.PasswordChar = '\0';
            this.tbSummary.ReadOnly = true;
            this.tbSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSummary.SelectedText = "";
            this.tbSummary.SelectionLength = 0;
            this.tbSummary.SelectionStart = 0;
            this.tbSummary.ShortcutsEnabled = true;
            this.tbSummary.Size = new System.Drawing.Size(444, 366);
            this.tbSummary.TabIndex = 1;
            this.tbSummary.UseSelectable = true;
            this.tbSummary.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbSummary.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbSale_Price
            //
            //
            //
            //
            this.tbSale_Price.CustomButton.Image = null;
            this.tbSale_Price.CustomButton.Location = new System.Drawing.Point(248, 1);
            this.tbSale_Price.CustomButton.Name = "";
            this.tbSale_Price.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbSale_Price.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbSale_Price.CustomButton.TabIndex = 1;
            this.tbSale_Price.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbSale_Price.CustomButton.UseSelectable = true;
            this.tbSale_Price.CustomButton.Visible = false;
            this.tbSale_Price.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSale_Price.Lines = new string[0];
            this.tbSale_Price.Location = new System.Drawing.Point(464, 40);
            this.tbSale_Price.MaxLength = 12;
            this.tbSale_Price.Name = "tbSale_Price";
            this.tbSale_Price.PasswordChar = '\0';
            this.tbSale_Price.PromptText = "Retail Price";
            this.tbSale_Price.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbSale_Price.SelectedText = "";
            this.tbSale_Price.SelectionLength = 0;
            this.tbSale_Price.SelectionStart = 0;
            this.tbSale_Price.ShortcutsEnabled = true;
            this.tbSale_Price.Size = new System.Drawing.Size(270, 23);
            this.tbSale_Price.TabIndex = 2;
            this.tbSale_Price.Tag = "tbSale_Price";
            this.tbSale_Price.UseSelectable = true;
            this.tbSale_Price.WaterMark = "Retail Price";
            this.tbSale_Price.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbSale_Price.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbOpcionals
            //
            //
            //
            //
            this.tbOpcionals.CustomButton.Image = null;
            this.tbOpcionals.CustomButton.Location = new System.Drawing.Point(248, 1);
            this.tbOpcionals.CustomButton.Name = "";
            this.tbOpcionals.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbOpcionals.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbOpcionals.CustomButton.TabIndex = 1;
            this.tbOpcionals.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbOpcionals.CustomButton.UseSelectable = true;
            this.tbOpcionals.CustomButton.Visible = false;
            this.tbOpcionals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOpcionals.Lines = new string[0];
            this.tbOpcionals.Location = new System.Drawing.Point(464, 72);
            this.tbOpcionals.MaxLength = 255;
            this.tbOpcionals.Name = "tbOpcionals";
            this.tbOpcionals.PasswordChar = '\0';
            this.tbOpcionals.PromptText = "Optionals";
            this.tbOpcionals.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbOpcionals.SelectedText = "";
            this.tbOpcionals.SelectionLength = 0;
            this.tbOpcionals.SelectionStart = 0;
            this.tbOpcionals.ShortcutsEnabled = true;
            this.tbOpcionals.Size = new System.Drawing.Size(270, 23);
            this.tbOpcionals.TabIndex = 3;
            this.tbOpcionals.Tag = "tbOpcionals";
            this.tbOpcionals.UseSelectable = true;
            this.tbOpcionals.WaterMark = "Optionals";
            this.tbOpcionals.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbOpcionals.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbVehicleObservations
            //
            //
            //
            //
            this.tbVehicleObservations.CustomButton.Image = null;
            this.tbVehicleObservations.CustomButton.Location = new System.Drawing.Point(122, 2);
            this.tbVehicleObservations.CustomButton.Name = "";
            this.tbVehicleObservations.CustomButton.Size = new System.Drawing.Size(145, 145);
            this.tbVehicleObservations.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbVehicleObservations.CustomButton.TabIndex = 1;
            this.tbVehicleObservations.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbVehicleObservations.CustomButton.UseSelectable = true;
            this.tbVehicleObservations.CustomButton.Visible = false;
            this.tbVehicleObservations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVehicleObservations.Lines = new string[0];
            this.tbVehicleObservations.Location = new System.Drawing.Point(464, 104);
            this.tbVehicleObservations.MaxLength = 32767;
            this.tbVehicleObservations.Multiline = true;
            this.tbVehicleObservations.Name = "tbVehicleObservations";
            this.tbVehicleObservations.PasswordChar = '\0';
            this.tbVehicleObservations.PromptText = "Vehicle Observations";
            this.tbVehicleObservations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbVehicleObservations.SelectedText = "";
            this.tbVehicleObservations.SelectionLength = 0;
            this.tbVehicleObservations.SelectionStart = 0;
            this.tbVehicleObservations.ShortcutsEnabled = true;
            this.tbVehicleObservations.Size = new System.Drawing.Size(270, 150);
            this.tbVehicleObservations.TabIndex = 4;
            this.tbVehicleObservations.Tag = "tbVehicleObservations";
            this.tbVehicleObservations.UseSelectable = true;
            this.tbVehicleObservations.WaterMark = "Vehicle Observations";
            this.tbVehicleObservations.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbVehicleObservations.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // btnFinishAcquisition
            //
            this.btnFinishAcquisition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinishAcquisition.Location = new System.Drawing.Point(574, 371);
            this.btnFinishAcquisition.Name = "btnFinishAcquisition";
            this.btnFinishAcquisition.Size = new System.Drawing.Size(160, 35);
            this.btnFinishAcquisition.TabIndex = 5;
            this.btnFinishAcquisition.Tag = "btnFinishAcquisition";
            this.btnFinishAcquisition.Text = "Finish Purchase";
            this.btnFinishAcquisition.UseSelectable = true;
            this.btnFinishAcquisition.Click += new System.EventHandler(this.btnFinishAcquisition_Click);
            //
            // ManageAcquisitionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 560);
            this.MinimumSize = new System.Drawing.Size(1220, 560);
            this.Controls.Add(this.panelManagement);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageAcquisitionForm";
            this.Tag = "ManageAcquisitionForm";
            this.Text = "Manage Vehicle Purchase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageAcquisitionForm_FormClosing);
            this.panelManagement.ResumeLayout(false);
            this.panelManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPending)).EndInit();
            this.SummaryPanel.ResumeLayout(false);
            this.SummaryPanel.PerformLayout();
            this.tabStages.ResumeLayout(false);
            this.tabInspection.ResumeLayout(false);
            this.tabFormalize.ResumeLayout(false);
            this.tabFormalize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTerms)).EndInit();
            this.tabDocuments.ResumeLayout(false);
            this.tabTransfer.ResumeLayout(false);
            this.tabTransfer.PerformLayout();
            this.tabFinish.ResumeLayout(false);
            this.tabFinish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel panelManagement;
        private MetroFramework.Controls.MetroLabel lblPendingAcquisitions;
        private MetroFramework.Controls.MetroTextBox tbFilter;
        private MetroFramework.Controls.MetroButton btnSearch;
        private MetroFramework.Controls.MetroGrid gridPending;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicense_Plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private MetroFramework.Controls.MetroPanel SummaryPanel;
        private MetroFramework.Controls.MetroLabel lblAcquisitionSummary;
        private MetroFramework.Controls.MetroLabel lblSummaryClient;
        private MetroFramework.Controls.MetroLabel lblSummaryVehicle;
        private MetroFramework.Controls.MetroLabel lblSummaryAppraisal;
        private MetroFramework.Controls.MetroLabel lblSummaryStatus;
        private MetroFramework.Controls.MetroLabel lblStageMessage;
        private MetroFramework.Controls.MetroTabControl tabStages;
        private MetroFramework.Controls.MetroTabPage tabInspection;
        private UI.Acquisitions.Controls.InspectionView inspectionView1;
        private MetroFramework.Controls.MetroTabPage tabFormalize;
        private MetroFramework.Controls.MetroLabel lblAgreement;
        private MetroFramework.Controls.MetroTextBox tbAgreed_Price;
        private MetroFramework.Controls.MetroTextBox tbOwner_CUIL_CUIT;
        private MetroFramework.Controls.MetroLabel lblPaymentTerms;
        private MetroFramework.Controls.MetroComboBox comboPaymentType;
        private MetroFramework.Controls.MetroTextBox tbTermAmount;
        private MetroFramework.Controls.MetroButton btnAddTerm;
        private MetroFramework.Controls.MetroButton btnRemoveTerm;
        private MetroFramework.Controls.MetroGrid gridTerms;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTermAmount;
        private MetroFramework.Controls.MetroLabel lblTermsDifference;
        private MetroFramework.Controls.MetroButton btnRejectAppraisal;
        private MetroFramework.Controls.MetroButton btnGenerateContract;
        private MetroFramework.Controls.MetroButton btnContractNotSigned;
        private MetroFramework.Controls.MetroButton btnContractSigned;
        private MetroFramework.Controls.MetroTabPage tabDocuments;
        private UI.Acquisitions.Controls.AcquisitionDocumentsView documentsView1;
        private MetroFramework.Controls.MetroTabPage tabTransfer;
        private MetroFramework.Controls.MetroLabel lblTransfer;
        private MetroFramework.Controls.MetroLabel lblTransferOwner;
        private MetroFramework.Controls.MetroLabel lblTransferVehicle;
        private MetroFramework.Controls.MetroTextBox tbPaperwork_Precharge_Code;
        private MetroFramework.Controls.MetroLabel lblTransferAppointment;
        private MetroFramework.Controls.MetroDateTime dtTransfer_Appointment_Date;
        private MetroFramework.Controls.MetroButton btnScheduleTransfer;
        private UI.Acquisitions.Controls.AcquisitionDocumentsView transferDocumentsView1;
        private MetroFramework.Controls.MetroButton btnRegisterTransfer;
        private MetroFramework.Controls.MetroTabPage tabFinish;
        private MetroFramework.Controls.MetroLabel lblFinishSummary;
        private MetroFramework.Controls.MetroTextBox tbSummary;
        private MetroFramework.Controls.MetroTextBox tbSale_Price;
        private MetroFramework.Controls.MetroTextBox tbOpcionals;
        private MetroFramework.Controls.MetroTextBox tbVehicleObservations;
        private MetroFramework.Controls.MetroButton btnFinishAcquisition;
    }
}
