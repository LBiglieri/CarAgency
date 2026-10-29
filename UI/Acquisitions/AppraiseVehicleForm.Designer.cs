namespace UI.Vehicles
{
    partial class AppraiseVehicleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppraiseVehicleForm));
            this.panelAppraisals = new MetroFramework.Controls.MetroPanel();
            this.lblPendingAppraisals = new MetroFramework.Controls.MetroLabel();
            this.tbFilter = new MetroFramework.Controls.MetroTextBox();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.gridPending = new MetroFramework.Controls.MetroGrid();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLicense_Plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehicle_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrevious_Appraisals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppraisalPanel = new MetroFramework.Controls.MetroPanel();
            this.lblAppraisal = new MetroFramework.Controls.MetroLabel();
            this.btnViewPreviousAppraisal = new MetroFramework.Controls.MetroButton();
            this.lblPreviousAppraisal = new MetroFramework.Controls.MetroLabel();
            this.tbAppraisalAmount = new MetroFramework.Controls.MetroTextBox();
            this.tbJustification = new MetroFramework.Controls.MetroTextBox();
            this.btnGenerateAppraisal = new MetroFramework.Controls.MetroButton();
            this.inspectionView1 = new UI.Acquisitions.Controls.InspectionView();
            this.panelAppraisals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPending)).BeginInit();
            this.AppraisalPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // panelAppraisals
            //
            this.panelAppraisals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAppraisals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAppraisals.Controls.Add(this.lblPendingAppraisals);
            this.panelAppraisals.Controls.Add(this.tbFilter);
            this.panelAppraisals.Controls.Add(this.btnSearch);
            this.panelAppraisals.Controls.Add(this.gridPending);
            this.panelAppraisals.Controls.Add(this.AppraisalPanel);
            this.panelAppraisals.Controls.Add(this.inspectionView1);
            this.panelAppraisals.HorizontalScrollbarBarColor = true;
            this.panelAppraisals.HorizontalScrollbarHighlightOnWheel = false;
            this.panelAppraisals.HorizontalScrollbarSize = 10;
            this.panelAppraisals.Location = new System.Drawing.Point(20, 60);
            this.panelAppraisals.Name = "panelAppraisals";
            this.panelAppraisals.Size = new System.Drawing.Size(1180, 480);
            this.panelAppraisals.TabIndex = 0;
            this.panelAppraisals.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelAppraisals.VerticalScrollbarBarColor = true;
            this.panelAppraisals.VerticalScrollbarHighlightOnWheel = false;
            this.panelAppraisals.VerticalScrollbarSize = 10;
            //
            // lblPendingAppraisals
            //
            this.lblPendingAppraisals.AutoSize = true;
            this.lblPendingAppraisals.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblPendingAppraisals.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPendingAppraisals.Location = new System.Drawing.Point(18, 10);
            this.lblPendingAppraisals.Name = "lblPendingAppraisals";
            this.lblPendingAppraisals.Size = new System.Drawing.Size(170, 25);
            this.lblPendingAppraisals.TabIndex = 0;
            this.lblPendingAppraisals.Tag = "lblPendingAppraisals";
            this.lblPendingAppraisals.Text = "Pending Appraisals";
            this.lblPendingAppraisals.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbFilter
            //
            //
            //
            //
            this.tbFilter.CustomButton.Image = null;
            this.tbFilter.CustomButton.Location = new System.Drawing.Point(278, 1);
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
            this.tbFilter.Size = new System.Drawing.Size(300, 23);
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
            this.btnSearch.Location = new System.Drawing.Point(326, 44);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(132, 25);
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
            this.colVehicle_Description,
            this.colPrevious_Appraisals});
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
            this.gridPending.Size = new System.Drawing.Size(440, 200);
            this.gridPending.TabIndex = 3;
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
            this.colDni.FillWeight = 22F;
            this.colDni.HeaderText = "DNI";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            this.colDni.Tag = "colDni";
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
            this.colVehicle_Description.FillWeight = 52F;
            this.colVehicle_Description.HeaderText = "Vehicle";
            this.colVehicle_Description.Name = "colVehicle_Description";
            this.colVehicle_Description.ReadOnly = true;
            this.colVehicle_Description.Tag = "colVehicle_Description";
            //
            // colPrevious_Appraisals
            //
            this.colPrevious_Appraisals.DataPropertyName = "Previous_Appraisals";
            this.colPrevious_Appraisals.FillWeight = 20F;
            this.colPrevious_Appraisals.HeaderText = "Previous Appraisals";
            this.colPrevious_Appraisals.Name = "colPrevious_Appraisals";
            this.colPrevious_Appraisals.ReadOnly = true;
            this.colPrevious_Appraisals.Tag = "colPrevious_Appraisals";
            //
            // AppraisalPanel
            //
            this.AppraisalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AppraisalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AppraisalPanel.Controls.Add(this.lblAppraisal);
            this.AppraisalPanel.Controls.Add(this.btnViewPreviousAppraisal);
            this.AppraisalPanel.Controls.Add(this.lblPreviousAppraisal);
            this.AppraisalPanel.Controls.Add(this.tbAppraisalAmount);
            this.AppraisalPanel.Controls.Add(this.tbJustification);
            this.AppraisalPanel.Controls.Add(this.btnGenerateAppraisal);
            this.AppraisalPanel.Enabled = false;
            this.AppraisalPanel.HorizontalScrollbarBarColor = true;
            this.AppraisalPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.AppraisalPanel.HorizontalScrollbarSize = 10;
            this.AppraisalPanel.Location = new System.Drawing.Point(18, 284);
            this.AppraisalPanel.Name = "AppraisalPanel";
            this.AppraisalPanel.Size = new System.Drawing.Size(440, 184);
            this.AppraisalPanel.TabIndex = 4;
            this.AppraisalPanel.VerticalScrollbarBarColor = true;
            this.AppraisalPanel.VerticalScrollbarHighlightOnWheel = false;
            this.AppraisalPanel.VerticalScrollbarSize = 10;
            //
            // lblAppraisal
            //
            this.lblAppraisal.AutoSize = true;
            this.lblAppraisal.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblAppraisal.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblAppraisal.Location = new System.Drawing.Point(10, 6);
            this.lblAppraisal.Name = "lblAppraisal";
            this.lblAppraisal.Size = new System.Drawing.Size(86, 25);
            this.lblAppraisal.TabIndex = 0;
            this.lblAppraisal.Tag = "lblAppraisal";
            this.lblAppraisal.Text = "Appraisal";
            this.lblAppraisal.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // btnViewPreviousAppraisal
            //
            this.btnViewPreviousAppraisal.Location = new System.Drawing.Point(259, 8);
            this.btnViewPreviousAppraisal.Name = "btnViewPreviousAppraisal";
            this.btnViewPreviousAppraisal.Size = new System.Drawing.Size(169, 24);
            this.btnViewPreviousAppraisal.TabIndex = 1;
            this.btnViewPreviousAppraisal.Tag = "btnViewPreviousAppraisal";
            this.btnViewPreviousAppraisal.Text = "View Previous Appraisal";
            this.btnViewPreviousAppraisal.UseSelectable = true;
            this.btnViewPreviousAppraisal.Visible = false;
            this.btnViewPreviousAppraisal.Click += new System.EventHandler(this.btnViewPreviousAppraisal_Click);
            //
            // lblPreviousAppraisal
            //
            this.lblPreviousAppraisal.AutoSize = true;
            this.lblPreviousAppraisal.Location = new System.Drawing.Point(10, 36);
            this.lblPreviousAppraisal.Name = "lblPreviousAppraisal";
            this.lblPreviousAppraisal.Size = new System.Drawing.Size(0, 0);
            this.lblPreviousAppraisal.TabIndex = 2;
            this.lblPreviousAppraisal.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbAppraisalAmount
            //
            //
            //
            //
            this.tbAppraisalAmount.CustomButton.Image = null;
            this.tbAppraisalAmount.CustomButton.Location = new System.Drawing.Point(148, 1);
            this.tbAppraisalAmount.CustomButton.Name = "";
            this.tbAppraisalAmount.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbAppraisalAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbAppraisalAmount.CustomButton.TabIndex = 1;
            this.tbAppraisalAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbAppraisalAmount.CustomButton.UseSelectable = true;
            this.tbAppraisalAmount.CustomButton.Visible = false;
            this.tbAppraisalAmount.Lines = new string[0];
            this.tbAppraisalAmount.Location = new System.Drawing.Point(10, 60);
            this.tbAppraisalAmount.MaxLength = 12;
            this.tbAppraisalAmount.Name = "tbAppraisalAmount";
            this.tbAppraisalAmount.PasswordChar = '\0';
            this.tbAppraisalAmount.PromptText = "Appraisal Value";
            this.tbAppraisalAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbAppraisalAmount.SelectedText = "";
            this.tbAppraisalAmount.SelectionLength = 0;
            this.tbAppraisalAmount.SelectionStart = 0;
            this.tbAppraisalAmount.ShortcutsEnabled = true;
            this.tbAppraisalAmount.Size = new System.Drawing.Size(170, 23);
            this.tbAppraisalAmount.TabIndex = 3;
            this.tbAppraisalAmount.Tag = "tbAppraisalAmount";
            this.tbAppraisalAmount.UseSelectable = true;
            this.tbAppraisalAmount.WaterMark = "Appraisal Value";
            this.tbAppraisalAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbAppraisalAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbJustification
            //
            //
            //
            //
            this.tbJustification.CustomButton.Image = null;
            this.tbJustification.CustomButton.Location = new System.Drawing.Point(370, 2);
            this.tbJustification.CustomButton.Name = "";
            this.tbJustification.CustomButton.Size = new System.Drawing.Size(45, 45);
            this.tbJustification.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbJustification.CustomButton.TabIndex = 1;
            this.tbJustification.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbJustification.CustomButton.UseSelectable = true;
            this.tbJustification.CustomButton.Visible = false;
            this.tbJustification.Lines = new string[0];
            this.tbJustification.Location = new System.Drawing.Point(10, 90);
            this.tbJustification.MaxLength = 32767;
            this.tbJustification.Multiline = true;
            this.tbJustification.Name = "tbJustification";
            this.tbJustification.PasswordChar = '\0';
            this.tbJustification.PromptText = "Justification";
            this.tbJustification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbJustification.SelectedText = "";
            this.tbJustification.SelectionLength = 0;
            this.tbJustification.SelectionStart = 0;
            this.tbJustification.ShortcutsEnabled = true;
            this.tbJustification.Size = new System.Drawing.Size(418, 50);
            this.tbJustification.TabIndex = 4;
            this.tbJustification.Tag = "tbJustification";
            this.tbJustification.UseSelectable = true;
            this.tbJustification.WaterMark = "Justification";
            this.tbJustification.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbJustification.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // btnGenerateAppraisal
            //
            this.btnGenerateAppraisal.Location = new System.Drawing.Point(259, 146);
            this.btnGenerateAppraisal.Name = "btnGenerateAppraisal";
            this.btnGenerateAppraisal.Size = new System.Drawing.Size(169, 35);
            this.btnGenerateAppraisal.TabIndex = 5;
            this.btnGenerateAppraisal.Tag = "btnGenerateAppraisal";
            this.btnGenerateAppraisal.Text = "Generate Appraisal";
            this.btnGenerateAppraisal.UseSelectable = true;
            this.btnGenerateAppraisal.Click += new System.EventHandler(this.btnGenerateAppraisal_Click);
            //
            // inspectionView1
            //
            this.inspectionView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectionView1.BackColor = System.Drawing.Color.White;
            this.inspectionView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inspectionView1.Location = new System.Drawing.Point(472, 10);
            this.inspectionView1.Name = "inspectionView1";
            this.inspectionView1.Size = new System.Drawing.Size(690, 458);
            this.inspectionView1.TabIndex = 5;
            //
            // AppraiseVehicleForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 560);
            this.MinimumSize = new System.Drawing.Size(1220, 560);
            this.Controls.Add(this.panelAppraisals);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AppraiseVehicleForm";
            this.Tag = "AppraiseVehicleForm";
            this.Text = "Appraise Vehicle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppraiseVehicleForm_FormClosing);
            this.panelAppraisals.ResumeLayout(false);
            this.panelAppraisals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPending)).EndInit();
            this.AppraisalPanel.ResumeLayout(false);
            this.AppraisalPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel panelAppraisals;
        private MetroFramework.Controls.MetroLabel lblPendingAppraisals;
        private MetroFramework.Controls.MetroTextBox tbFilter;
        private MetroFramework.Controls.MetroButton btnSearch;
        private MetroFramework.Controls.MetroGrid gridPending;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicense_Plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicle_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrevious_Appraisals;
        private MetroFramework.Controls.MetroPanel AppraisalPanel;
        private MetroFramework.Controls.MetroLabel lblAppraisal;
        private MetroFramework.Controls.MetroButton btnViewPreviousAppraisal;
        private MetroFramework.Controls.MetroLabel lblPreviousAppraisal;
        private MetroFramework.Controls.MetroTextBox tbAppraisalAmount;
        private MetroFramework.Controls.MetroTextBox tbJustification;
        private MetroFramework.Controls.MetroButton btnGenerateAppraisal;
        private UI.Acquisitions.Controls.InspectionView inspectionView1;
    }
}
