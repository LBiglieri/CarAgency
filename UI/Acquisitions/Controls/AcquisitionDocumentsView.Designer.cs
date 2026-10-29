namespace UI.Acquisitions.Controls
{
    partial class AcquisitionDocumentsView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridDocuments = new MetroFramework.Controls.MetroGrid();
            this.colDocumentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocumentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIssue_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocumentObservation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblIssueDate = new MetroFramework.Controls.MetroLabel();
            this.dtIssue_Date = new MetroFramework.Controls.MetroDateTime();
            this.btnAddFile = new MetroFramework.Controls.MetroButton();
            this.btnOpenFile = new MetroFramework.Controls.MetroButton();
            this.btnDeleteFile = new MetroFramework.Controls.MetroButton();
            this.tbDocumentObservation = new MetroFramework.Controls.MetroTextBox();
            this.btnObserveDocument = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).BeginInit();
            this.SuspendLayout();
            //
            // gridDocuments
            //
            this.gridDocuments.AllowUserToAddRows = false;
            this.gridDocuments.AllowUserToDeleteRows = false;
            this.gridDocuments.AllowUserToResizeRows = false;
            this.gridDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDocuments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridDocuments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridDocuments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridDocuments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridDocuments.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridDocuments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDocumentType,
            this.colDocumentStatus,
            this.colIssue_Date,
            this.colDocumentObservation});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDocuments.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridDocuments.EnableHeadersVisualStyles = false;
            this.gridDocuments.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridDocuments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridDocuments.Location = new System.Drawing.Point(10, 10);
            this.gridDocuments.MultiSelect = false;
            this.gridDocuments.Name = "gridDocuments";
            this.gridDocuments.ReadOnly = true;
            this.gridDocuments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDocuments.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridDocuments.RowHeadersVisible = false;
            this.gridDocuments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDocuments.Size = new System.Drawing.Size(714, 270);
            this.gridDocuments.TabIndex = 0;
            //
            // colDocumentType
            //
            this.colDocumentType.FillWeight = 34F;
            this.colDocumentType.HeaderText = "Document";
            this.colDocumentType.Name = "colDocumentType";
            this.colDocumentType.ReadOnly = true;
            this.colDocumentType.Tag = "colDocumentType";
            //
            // colDocumentStatus
            //
            this.colDocumentStatus.FillWeight = 16F;
            this.colDocumentStatus.HeaderText = "Status";
            this.colDocumentStatus.Name = "colDocumentStatus";
            this.colDocumentStatus.ReadOnly = true;
            this.colDocumentStatus.Tag = "colDocumentStatus";
            //
            // colIssue_Date
            //
            this.colIssue_Date.FillWeight = 16F;
            this.colIssue_Date.HeaderText = "Issue Date";
            this.colIssue_Date.Name = "colIssue_Date";
            this.colIssue_Date.ReadOnly = true;
            this.colIssue_Date.Tag = "colIssue_Date";
            //
            // colDocumentObservation
            //
            this.colDocumentObservation.FillWeight = 34F;
            this.colDocumentObservation.HeaderText = "Observation";
            this.colDocumentObservation.Name = "colDocumentObservation";
            this.colDocumentObservation.ReadOnly = true;
            this.colDocumentObservation.Tag = "colDocumentObservation";
            //
            // lblIssueDate
            //
            this.lblIssueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Location = new System.Drawing.Point(10, 294);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(73, 19);
            this.lblIssueDate.TabIndex = 1;
            this.lblIssueDate.Tag = "lblIssueDate";
            this.lblIssueDate.Text = "Issue Date:";
            this.lblIssueDate.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // dtIssue_Date
            //
            this.dtIssue_Date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtIssue_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtIssue_Date.Location = new System.Drawing.Point(130, 288);
            this.dtIssue_Date.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtIssue_Date.Name = "dtIssue_Date";
            this.dtIssue_Date.Size = new System.Drawing.Size(160, 29);
            this.dtIssue_Date.TabIndex = 2;
            //
            // btnAddFile
            //
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFile.Location = new System.Drawing.Point(304, 290);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(130, 25);
            this.btnAddFile.TabIndex = 3;
            this.btnAddFile.Tag = "btnAddFile";
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.UseSelectable = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            //
            // btnOpenFile
            //
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.Location = new System.Drawing.Point(444, 290);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(130, 25);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Tag = "btnOpenFile";
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseSelectable = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            //
            // btnDeleteFile
            //
            this.btnDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteFile.Location = new System.Drawing.Point(584, 290);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(130, 25);
            this.btnDeleteFile.TabIndex = 5;
            this.btnDeleteFile.Tag = "btnDeleteFile";
            this.btnDeleteFile.Text = "Delete File";
            this.btnDeleteFile.UseSelectable = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            //
            // tbDocumentObservation
            //
            //
            //
            //
            this.tbDocumentObservation.CustomButton.Image = null;
            this.tbDocumentObservation.CustomButton.Location = new System.Drawing.Point(504, 2);
            this.tbDocumentObservation.CustomButton.Name = "";
            this.tbDocumentObservation.CustomButton.Size = new System.Drawing.Size(55, 55);
            this.tbDocumentObservation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDocumentObservation.CustomButton.TabIndex = 1;
            this.tbDocumentObservation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDocumentObservation.CustomButton.UseSelectable = true;
            this.tbDocumentObservation.CustomButton.Visible = false;
            this.tbDocumentObservation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDocumentObservation.Lines = new string[0];
            this.tbDocumentObservation.Location = new System.Drawing.Point(10, 326);
            this.tbDocumentObservation.MaxLength = 32767;
            this.tbDocumentObservation.Multiline = true;
            this.tbDocumentObservation.Name = "tbDocumentObservation";
            this.tbDocumentObservation.PasswordChar = '\0';
            this.tbDocumentObservation.PromptText = "Observation (holder is not the client, lien reported...)";
            this.tbDocumentObservation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDocumentObservation.SelectedText = "";
            this.tbDocumentObservation.SelectionLength = 0;
            this.tbDocumentObservation.SelectionStart = 0;
            this.tbDocumentObservation.ShortcutsEnabled = true;
            this.tbDocumentObservation.Size = new System.Drawing.Size(564, 60);
            this.tbDocumentObservation.TabIndex = 6;
            this.tbDocumentObservation.Tag = "tbDocumentObservation";
            this.tbDocumentObservation.UseSelectable = true;
            this.tbDocumentObservation.WaterMark = "Observation (holder is not the client, lien reported...)";
            this.tbDocumentObservation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDocumentObservation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // btnObserveDocument
            //
            this.btnObserveDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObserveDocument.Location = new System.Drawing.Point(584, 326);
            this.btnObserveDocument.Name = "btnObserveDocument";
            this.btnObserveDocument.Size = new System.Drawing.Size(130, 25);
            this.btnObserveDocument.TabIndex = 7;
            this.btnObserveDocument.Tag = "btnObserveDocument";
            this.btnObserveDocument.Text = "Observe Document";
            this.btnObserveDocument.UseSelectable = true;
            this.btnObserveDocument.Click += new System.EventHandler(this.btnObserveDocument_Click);
            //
            // AcquisitionDocumentsView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gridDocuments);
            this.Controls.Add(this.lblIssueDate);
            this.Controls.Add(this.dtIssue_Date);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnDeleteFile);
            this.Controls.Add(this.tbDocumentObservation);
            this.Controls.Add(this.btnObserveDocument);
            this.Name = "AcquisitionDocumentsView";
            this.Size = new System.Drawing.Size(734, 396);
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroGrid gridDocuments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumentStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIssue_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumentObservation;
        private MetroFramework.Controls.MetroLabel lblIssueDate;
        private MetroFramework.Controls.MetroDateTime dtIssue_Date;
        private MetroFramework.Controls.MetroButton btnAddFile;
        private MetroFramework.Controls.MetroButton btnOpenFile;
        private MetroFramework.Controls.MetroButton btnDeleteFile;
        private MetroFramework.Controls.MetroTextBox tbDocumentObservation;
        private MetroFramework.Controls.MetroButton btnObserveDocument;
    }
}
