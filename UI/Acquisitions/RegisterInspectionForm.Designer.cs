namespace UI.Vehicles
{
    partial class RegisterInspectionForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterInspectionForm));
            this.panelInspections = new MetroFramework.Controls.MetroPanel();
            this.lblScheduledInspections = new MetroFramework.Controls.MetroLabel();
            this.gridScheduled = new MetroFramework.Controls.MetroGrid();
            this.colScheduled_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLicense_Plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehicle_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRegisterNoShow = new MetroFramework.Controls.MetroButton();
            this.InspectionPanel = new MetroFramework.Controls.MetroPanel();
            this.lblInspectedVehicle = new MetroFramework.Controls.MetroLabel();
            this.lblVehicleData = new MetroFramework.Controls.MetroLabel();
            this.lblVehicleIdentification = new MetroFramework.Controls.MetroLabel();
            this.gridItems = new MetroFramework.Controls.MetroGrid();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colScoreObservations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridPhotos = new MetroFramework.Controls.MetroGrid();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUploadedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddPhoto = new MetroFramework.Controls.MetroButton();
            this.btnOpenPhoto = new MetroFramework.Controls.MetroButton();
            this.btnDeletePhoto = new MetroFramework.Controls.MetroButton();
            this.tbVerified_Kilometers = new MetroFramework.Controls.MetroTextBox();
            this.lblKilometersDifference = new MetroFramework.Controls.MetroLabel();
            this.lblResult = new MetroFramework.Controls.MetroLabel();
            this.comboResult = new MetroFramework.Controls.MetroComboBox();
            this.tbInspectionObservations = new MetroFramework.Controls.MetroTextBox();
            this.btnConfirmInspection = new MetroFramework.Controls.MetroButton();
            this.panelInspections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridScheduled)).BeginInit();
            this.InspectionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPhotos)).BeginInit();
            this.SuspendLayout();
            //
            // panelInspections
            //
            this.panelInspections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInspections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInspections.Controls.Add(this.lblScheduledInspections);
            this.panelInspections.Controls.Add(this.gridScheduled);
            this.panelInspections.Controls.Add(this.btnRegisterNoShow);
            this.panelInspections.Controls.Add(this.InspectionPanel);
            this.panelInspections.HorizontalScrollbarBarColor = true;
            this.panelInspections.HorizontalScrollbarHighlightOnWheel = false;
            this.panelInspections.HorizontalScrollbarSize = 10;
            this.panelInspections.Location = new System.Drawing.Point(20, 60);
            this.panelInspections.Name = "panelInspections";
            this.panelInspections.Size = new System.Drawing.Size(1180, 480);
            this.panelInspections.TabIndex = 0;
            this.panelInspections.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelInspections.VerticalScrollbarBarColor = true;
            this.panelInspections.VerticalScrollbarHighlightOnWheel = false;
            this.panelInspections.VerticalScrollbarSize = 10;
            //
            // lblScheduledInspections
            //
            this.lblScheduledInspections.AutoSize = true;
            this.lblScheduledInspections.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblScheduledInspections.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblScheduledInspections.Location = new System.Drawing.Point(18, 10);
            this.lblScheduledInspections.Name = "lblScheduledInspections";
            this.lblScheduledInspections.Size = new System.Drawing.Size(182, 25);
            this.lblScheduledInspections.TabIndex = 0;
            this.lblScheduledInspections.Tag = "lblScheduledInspections";
            this.lblScheduledInspections.Text = "Scheduled Inspections";
            this.lblScheduledInspections.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // gridScheduled
            //
            this.gridScheduled.AllowUserToAddRows = false;
            this.gridScheduled.AllowUserToDeleteRows = false;
            this.gridScheduled.AllowUserToResizeRows = false;
            this.gridScheduled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridScheduled.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridScheduled.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridScheduled.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridScheduled.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridScheduled.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridScheduled.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridScheduled.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridScheduled.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridScheduled.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colScheduled_Date,
            this.colNumber,
            this.colLicense_Plate,
            this.colVehicle_Description});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridScheduled.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridScheduled.EnableHeadersVisualStyles = false;
            this.gridScheduled.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridScheduled.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridScheduled.Location = new System.Drawing.Point(18, 45);
            this.gridScheduled.MultiSelect = false;
            this.gridScheduled.Name = "gridScheduled";
            this.gridScheduled.ReadOnly = true;
            this.gridScheduled.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridScheduled.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridScheduled.RowHeadersVisible = false;
            this.gridScheduled.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridScheduled.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridScheduled.Size = new System.Drawing.Size(440, 377);
            this.gridScheduled.TabIndex = 1;
            this.gridScheduled.SelectionChanged += new System.EventHandler(this.gridScheduled_SelectionChanged);
            //
            // colScheduled_Date
            //
            this.colScheduled_Date.DataPropertyName = "Scheduled_Date";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm";
            this.colScheduled_Date.DefaultCellStyle = dataGridViewCellStyle4;
            this.colScheduled_Date.FillWeight = 32F;
            this.colScheduled_Date.HeaderText = "Date";
            this.colScheduled_Date.Name = "colScheduled_Date";
            this.colScheduled_Date.ReadOnly = true;
            this.colScheduled_Date.Tag = "colScheduled_Date";
            //
            // colNumber
            //
            this.colNumber.DataPropertyName = "Number";
            this.colNumber.FillWeight = 16F;
            this.colNumber.HeaderText = "No.";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Tag = "colNumber";
            //
            // colLicense_Plate
            //
            this.colLicense_Plate.DataPropertyName = "License_Plate";
            this.colLicense_Plate.FillWeight = 22F;
            this.colLicense_Plate.HeaderText = "License Plate";
            this.colLicense_Plate.Name = "colLicense_Plate";
            this.colLicense_Plate.ReadOnly = true;
            this.colLicense_Plate.Tag = "colLicense_Plate";
            //
            // colVehicle_Description
            //
            this.colVehicle_Description.DataPropertyName = "Vehicle_Description";
            this.colVehicle_Description.FillWeight = 60F;
            this.colVehicle_Description.HeaderText = "Vehicle";
            this.colVehicle_Description.Name = "colVehicle_Description";
            this.colVehicle_Description.ReadOnly = true;
            this.colVehicle_Description.Tag = "colVehicle_Description";
            //
            // btnRegisterNoShow
            //
            this.btnRegisterNoShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRegisterNoShow.Enabled = false;
            this.btnRegisterNoShow.Location = new System.Drawing.Point(18, 430);
            this.btnRegisterNoShow.Name = "btnRegisterNoShow";
            this.btnRegisterNoShow.Size = new System.Drawing.Size(169, 35);
            this.btnRegisterNoShow.TabIndex = 2;
            this.btnRegisterNoShow.Tag = "btnRegisterNoShow";
            this.btnRegisterNoShow.Text = "Record No-show";
            this.btnRegisterNoShow.UseSelectable = true;
            this.btnRegisterNoShow.Click += new System.EventHandler(this.btnRegisterNoShow_Click);
            //
            // InspectionPanel
            //
            this.InspectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InspectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InspectionPanel.Controls.Add(this.lblInspectedVehicle);
            this.InspectionPanel.Controls.Add(this.lblVehicleData);
            this.InspectionPanel.Controls.Add(this.lblVehicleIdentification);
            this.InspectionPanel.Controls.Add(this.gridItems);
            this.InspectionPanel.Controls.Add(this.gridPhotos);
            this.InspectionPanel.Controls.Add(this.btnAddPhoto);
            this.InspectionPanel.Controls.Add(this.btnOpenPhoto);
            this.InspectionPanel.Controls.Add(this.btnDeletePhoto);
            this.InspectionPanel.Controls.Add(this.tbVerified_Kilometers);
            this.InspectionPanel.Controls.Add(this.lblKilometersDifference);
            this.InspectionPanel.Controls.Add(this.lblResult);
            this.InspectionPanel.Controls.Add(this.comboResult);
            this.InspectionPanel.Controls.Add(this.tbInspectionObservations);
            this.InspectionPanel.Controls.Add(this.btnConfirmInspection);
            this.InspectionPanel.Enabled = false;
            this.InspectionPanel.HorizontalScrollbarBarColor = true;
            this.InspectionPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.InspectionPanel.HorizontalScrollbarSize = 10;
            this.InspectionPanel.Location = new System.Drawing.Point(472, 10);
            this.InspectionPanel.Name = "InspectionPanel";
            this.InspectionPanel.Size = new System.Drawing.Size(690, 458);
            this.InspectionPanel.TabIndex = 3;
            this.InspectionPanel.VerticalScrollbarBarColor = true;
            this.InspectionPanel.VerticalScrollbarHighlightOnWheel = false;
            this.InspectionPanel.VerticalScrollbarSize = 10;
            //
            // lblInspectedVehicle
            //
            this.lblInspectedVehicle.AutoSize = true;
            this.lblInspectedVehicle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblInspectedVehicle.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblInspectedVehicle.Location = new System.Drawing.Point(10, 6);
            this.lblInspectedVehicle.Name = "lblInspectedVehicle";
            this.lblInspectedVehicle.Size = new System.Drawing.Size(136, 25);
            this.lblInspectedVehicle.TabIndex = 0;
            this.lblInspectedVehicle.Tag = "lblInspectedVehicle";
            this.lblInspectedVehicle.Text = "Vehicle to Inspect";
            this.lblInspectedVehicle.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblVehicleData
            //
            this.lblVehicleData.AutoSize = true;
            this.lblVehicleData.Location = new System.Drawing.Point(10, 38);
            this.lblVehicleData.Name = "lblVehicleData";
            this.lblVehicleData.Size = new System.Drawing.Size(0, 0);
            this.lblVehicleData.TabIndex = 1;
            this.lblVehicleData.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblVehicleIdentification
            //
            this.lblVehicleIdentification.AutoSize = true;
            this.lblVehicleIdentification.Location = new System.Drawing.Point(10, 60);
            this.lblVehicleIdentification.Name = "lblVehicleIdentification";
            this.lblVehicleIdentification.Size = new System.Drawing.Size(0, 0);
            this.lblVehicleIdentification.TabIndex = 2;
            this.lblVehicleIdentification.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // gridItems
            //
            this.gridItems.AllowUserToAddRows = false;
            this.gridItems.AllowUserToDeleteRows = false;
            this.gridItems.AllowUserToResizeRows = false;
            this.gridItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridItems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItem,
            this.colScore,
            this.colScoreObservations});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridItems.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridItems.EnableHeadersVisualStyles = false;
            this.gridItems.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridItems.Location = new System.Drawing.Point(10, 86);
            this.gridItems.MultiSelect = false;
            this.gridItems.Name = "gridItems";
            this.gridItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridItems.RowHeadersVisible = false;
            this.gridItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridItems.Size = new System.Drawing.Size(668, 210);
            this.gridItems.TabIndex = 3;
            this.gridItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridItems_CurrentCellDirtyStateChanged);
            //
            // colItem
            //
            this.colItem.FillWeight = 30F;
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            this.colItem.Tag = "colItem";
            //
            // colScore
            //
            this.colScore.FillWeight = 22F;
            this.colScore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colScore.HeaderText = "Rating";
            this.colScore.Name = "colScore";
            this.colScore.Tag = "colScore";
            //
            // colScoreObservations
            //
            this.colScoreObservations.FillWeight = 58F;
            this.colScoreObservations.HeaderText = "Technical Observations";
            this.colScoreObservations.Name = "colScoreObservations";
            this.colScoreObservations.Tag = "colScoreObservations";
            //
            // gridPhotos
            //
            this.gridPhotos.AllowUserToAddRows = false;
            this.gridPhotos.AllowUserToDeleteRows = false;
            this.gridPhotos.AllowUserToResizeRows = false;
            this.gridPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPhotos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPhotos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPhotos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPhotos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridPhotos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridPhotos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPhotos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gridPhotos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPhotos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colUploadedDate});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPhotos.DefaultCellStyle = dataGridViewCellStyle9;
            this.gridPhotos.EnableHeadersVisualStyles = false;
            this.gridPhotos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridPhotos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPhotos.Location = new System.Drawing.Point(10, 304);
            this.gridPhotos.Name = "gridPhotos";
            this.gridPhotos.ReadOnly = true;
            this.gridPhotos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPhotos.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gridPhotos.RowHeadersVisible = false;
            this.gridPhotos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridPhotos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPhotos.Size = new System.Drawing.Size(538, 76);
            this.gridPhotos.TabIndex = 4;
            //
            // colFileName
            //
            this.colFileName.DataPropertyName = "FileName";
            this.colFileName.FillWeight = 65F;
            this.colFileName.HeaderText = "Photo";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Tag = "colFileName";
            //
            // colUploadedDate
            //
            this.colUploadedDate.DataPropertyName = "UploadedDate";
            dataGridViewCellStyle11.Format = "dd/MM/yyyy HH:mm";
            this.colUploadedDate.DefaultCellStyle = dataGridViewCellStyle11;
            this.colUploadedDate.FillWeight = 35F;
            this.colUploadedDate.HeaderText = "Uploaded";
            this.colUploadedDate.Name = "colUploadedDate";
            this.colUploadedDate.ReadOnly = true;
            this.colUploadedDate.Tag = "colUploadedDate";
            //
            // btnAddPhoto
            //
            this.btnAddPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPhoto.Location = new System.Drawing.Point(558, 304);
            this.btnAddPhoto.Name = "btnAddPhoto";
            this.btnAddPhoto.Size = new System.Drawing.Size(120, 24);
            this.btnAddPhoto.TabIndex = 5;
            this.btnAddPhoto.Tag = "btnAddPhoto";
            this.btnAddPhoto.Text = "Add Photo";
            this.btnAddPhoto.UseSelectable = true;
            this.btnAddPhoto.Click += new System.EventHandler(this.btnAddPhoto_Click);
            //
            // btnOpenPhoto
            //
            this.btnOpenPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPhoto.Location = new System.Drawing.Point(558, 330);
            this.btnOpenPhoto.Name = "btnOpenPhoto";
            this.btnOpenPhoto.Size = new System.Drawing.Size(120, 24);
            this.btnOpenPhoto.TabIndex = 6;
            this.btnOpenPhoto.Tag = "btnOpenPhoto";
            this.btnOpenPhoto.Text = "View Photo";
            this.btnOpenPhoto.UseSelectable = true;
            this.btnOpenPhoto.Click += new System.EventHandler(this.btnOpenPhoto_Click);
            //
            // btnDeletePhoto
            //
            this.btnDeletePhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePhoto.Location = new System.Drawing.Point(558, 356);
            this.btnDeletePhoto.Name = "btnDeletePhoto";
            this.btnDeletePhoto.Size = new System.Drawing.Size(120, 24);
            this.btnDeletePhoto.TabIndex = 7;
            this.btnDeletePhoto.Tag = "btnDeletePhoto";
            this.btnDeletePhoto.Text = "Remove Photo";
            this.btnDeletePhoto.UseSelectable = true;
            this.btnDeletePhoto.Click += new System.EventHandler(this.btnDeletePhoto_Click);
            //
            // tbVerified_Kilometers
            //
            //
            //
            //
            this.tbVerified_Kilometers.CustomButton.Image = null;
            this.tbVerified_Kilometers.CustomButton.Location = new System.Drawing.Point(148, 1);
            this.tbVerified_Kilometers.CustomButton.Name = "";
            this.tbVerified_Kilometers.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbVerified_Kilometers.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbVerified_Kilometers.CustomButton.TabIndex = 1;
            this.tbVerified_Kilometers.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbVerified_Kilometers.CustomButton.UseSelectable = true;
            this.tbVerified_Kilometers.CustomButton.Visible = false;
            this.tbVerified_Kilometers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbVerified_Kilometers.Icon = global::UI.Properties.Resources.user__1_;
            this.tbVerified_Kilometers.Lines = new string[0];
            this.tbVerified_Kilometers.Location = new System.Drawing.Point(10, 392);
            this.tbVerified_Kilometers.MaxLength = 7;
            this.tbVerified_Kilometers.Name = "tbVerified_Kilometers";
            this.tbVerified_Kilometers.PasswordChar = '\0';
            this.tbVerified_Kilometers.PromptText = "Verified Kilometers";
            this.tbVerified_Kilometers.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbVerified_Kilometers.SelectedText = "";
            this.tbVerified_Kilometers.SelectionLength = 0;
            this.tbVerified_Kilometers.SelectionStart = 0;
            this.tbVerified_Kilometers.ShortcutsEnabled = true;
            this.tbVerified_Kilometers.Size = new System.Drawing.Size(170, 23);
            this.tbVerified_Kilometers.TabIndex = 8;
            this.tbVerified_Kilometers.Tag = "tbVerified_Kilometers";
            this.tbVerified_Kilometers.UseSelectable = true;
            this.tbVerified_Kilometers.WaterMark = "Verified Kilometers";
            this.tbVerified_Kilometers.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbVerified_Kilometers.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbVerified_Kilometers.TextChanged += new System.EventHandler(this.tbVerified_Kilometers_TextChanged);
            //
            // lblKilometersDifference
            //
            this.lblKilometersDifference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblKilometersDifference.AutoSize = true;
            this.lblKilometersDifference.Location = new System.Drawing.Point(190, 394);
            this.lblKilometersDifference.Name = "lblKilometersDifference";
            this.lblKilometersDifference.Size = new System.Drawing.Size(0, 0);
            this.lblKilometersDifference.Style = MetroFramework.MetroColorStyle.Red;
            this.lblKilometersDifference.TabIndex = 9;
            this.lblKilometersDifference.Theme = MetroFramework.MetroThemeStyle.Light;
            this.lblKilometersDifference.UseStyleColors = true;
            //
            // lblResult
            //
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(440, 394);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(47, 19);
            this.lblResult.TabIndex = 10;
            this.lblResult.Tag = "lblResult";
            this.lblResult.Text = "Result:";
            this.lblResult.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboResult
            //
            this.comboResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboResult.FormattingEnabled = true;
            this.comboResult.ItemHeight = 23;
            this.comboResult.Location = new System.Drawing.Point(508, 389);
            this.comboResult.Name = "comboResult";
            this.comboResult.Size = new System.Drawing.Size(170, 29);
            this.comboResult.TabIndex = 11;
            this.comboResult.UseSelectable = true;
            //
            // tbInspectionObservations
            //
            //
            //
            //
            this.tbInspectionObservations.CustomButton.Image = null;
            this.tbInspectionObservations.CustomButton.Location = new System.Drawing.Point(458, 1);
            this.tbInspectionObservations.CustomButton.Name = "";
            this.tbInspectionObservations.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbInspectionObservations.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbInspectionObservations.CustomButton.TabIndex = 1;
            this.tbInspectionObservations.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbInspectionObservations.CustomButton.UseSelectable = true;
            this.tbInspectionObservations.CustomButton.Visible = false;
            this.tbInspectionObservations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInspectionObservations.Icon = global::UI.Properties.Resources.user__1_;
            this.tbInspectionObservations.Lines = new string[0];
            this.tbInspectionObservations.Location = new System.Drawing.Point(10, 424);
            this.tbInspectionObservations.MaxLength = 32767;
            this.tbInspectionObservations.Name = "tbInspectionObservations";
            this.tbInspectionObservations.PasswordChar = '\0';
            this.tbInspectionObservations.PromptText = "General Observations";
            this.tbInspectionObservations.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbInspectionObservations.SelectedText = "";
            this.tbInspectionObservations.SelectionLength = 0;
            this.tbInspectionObservations.SelectionStart = 0;
            this.tbInspectionObservations.ShortcutsEnabled = true;
            this.tbInspectionObservations.Size = new System.Drawing.Size(480, 23);
            this.tbInspectionObservations.TabIndex = 12;
            this.tbInspectionObservations.Tag = "tbInspectionObservations";
            this.tbInspectionObservations.UseSelectable = true;
            this.tbInspectionObservations.WaterMark = "General Observations";
            this.tbInspectionObservations.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbInspectionObservations.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // btnConfirmInspection
            //
            this.btnConfirmInspection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmInspection.Location = new System.Drawing.Point(509, 418);
            this.btnConfirmInspection.Name = "btnConfirmInspection";
            this.btnConfirmInspection.Size = new System.Drawing.Size(169, 35);
            this.btnConfirmInspection.TabIndex = 13;
            this.btnConfirmInspection.Tag = "btnConfirmInspection";
            this.btnConfirmInspection.Text = "Confirm Inspection";
            this.btnConfirmInspection.UseSelectable = true;
            this.btnConfirmInspection.Click += new System.EventHandler(this.btnConfirmInspection_Click);
            //
            // RegisterInspectionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 560);
            this.MinimumSize = new System.Drawing.Size(1220, 560);
            this.Controls.Add(this.panelInspections);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterInspectionForm";
            this.Tag = "RegisterInspectionForm";
            this.Text = "Register Mechanical Inspection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterInspectionForm_FormClosing);
            this.panelInspections.ResumeLayout(false);
            this.panelInspections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridScheduled)).EndInit();
            this.InspectionPanel.ResumeLayout(false);
            this.InspectionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPhotos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel panelInspections;
        private MetroFramework.Controls.MetroLabel lblScheduledInspections;
        private MetroFramework.Controls.MetroGrid gridScheduled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScheduled_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicense_Plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicle_Description;
        private MetroFramework.Controls.MetroButton btnRegisterNoShow;
        private MetroFramework.Controls.MetroPanel InspectionPanel;
        private MetroFramework.Controls.MetroLabel lblInspectedVehicle;
        private MetroFramework.Controls.MetroLabel lblVehicleData;
        private MetroFramework.Controls.MetroLabel lblVehicleIdentification;
        private MetroFramework.Controls.MetroGrid gridItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewComboBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScoreObservations;
        private MetroFramework.Controls.MetroGrid gridPhotos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUploadedDate;
        private MetroFramework.Controls.MetroButton btnAddPhoto;
        private MetroFramework.Controls.MetroButton btnOpenPhoto;
        private MetroFramework.Controls.MetroButton btnDeletePhoto;
        private MetroFramework.Controls.MetroTextBox tbVerified_Kilometers;
        private MetroFramework.Controls.MetroLabel lblKilometersDifference;
        private MetroFramework.Controls.MetroLabel lblResult;
        private MetroFramework.Controls.MetroComboBox comboResult;
        private MetroFramework.Controls.MetroTextBox tbInspectionObservations;
        private MetroFramework.Controls.MetroButton btnConfirmInspection;
    }
}
