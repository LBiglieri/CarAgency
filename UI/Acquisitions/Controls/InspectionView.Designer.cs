namespace UI.Acquisitions.Controls
{
    partial class InspectionView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblInspectionResult = new MetroFramework.Controls.MetroLabel();
            this.lblVehicleData = new MetroFramework.Controls.MetroLabel();
            this.lblVehicleIdentification = new MetroFramework.Controls.MetroLabel();
            this.lblInspectionData = new MetroFramework.Controls.MetroLabel();
            this.lblKilometersDifference = new MetroFramework.Controls.MetroLabel();
            this.gridScores = new MetroFramework.Controls.MetroGrid();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScoreObservations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGeneralObservations = new MetroFramework.Controls.MetroLabel();
            this.tbInspectionObservations = new MetroFramework.Controls.MetroTextBox();
            this.gridPhotos = new MetroFramework.Controls.MetroGrid();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUploadedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpenPhoto = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridScores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPhotos)).BeginInit();
            this.SuspendLayout();
            //
            // lblInspectionResult
            //
            this.lblInspectionResult.AutoSize = true;
            this.lblInspectionResult.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblInspectionResult.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblInspectionResult.Location = new System.Drawing.Point(10, 6);
            this.lblInspectionResult.Name = "lblInspectionResult";
            this.lblInspectionResult.Size = new System.Drawing.Size(158, 25);
            this.lblInspectionResult.TabIndex = 0;
            this.lblInspectionResult.Tag = "lblInspectionResult";
            this.lblInspectionResult.Text = "Inspection Result";
            this.lblInspectionResult.Theme = MetroFramework.MetroThemeStyle.Light;
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
            // lblInspectionData
            //
            this.lblInspectionData.AutoSize = true;
            this.lblInspectionData.Location = new System.Drawing.Point(10, 82);
            this.lblInspectionData.Name = "lblInspectionData";
            this.lblInspectionData.Size = new System.Drawing.Size(0, 0);
            this.lblInspectionData.TabIndex = 3;
            this.lblInspectionData.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblKilometersDifference
            //
            this.lblKilometersDifference.AutoSize = true;
            this.lblKilometersDifference.Location = new System.Drawing.Point(10, 104);
            this.lblKilometersDifference.Name = "lblKilometersDifference";
            this.lblKilometersDifference.Size = new System.Drawing.Size(0, 0);
            this.lblKilometersDifference.Style = MetroFramework.MetroColorStyle.Red;
            this.lblKilometersDifference.TabIndex = 4;
            this.lblKilometersDifference.Theme = MetroFramework.MetroThemeStyle.Light;
            this.lblKilometersDifference.UseStyleColors = true;
            //
            // gridScores
            //
            this.gridScores.AllowUserToAddRows = false;
            this.gridScores.AllowUserToDeleteRows = false;
            this.gridScores.AllowUserToResizeRows = false;
            this.gridScores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridScores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridScores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridScores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridScores.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridScores.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridScores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridScores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridScores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridScores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItem,
            this.colScore,
            this.colScoreObservations});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridScores.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridScores.EnableHeadersVisualStyles = false;
            this.gridScores.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridScores.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridScores.Location = new System.Drawing.Point(10, 128);
            this.gridScores.MultiSelect = false;
            this.gridScores.Name = "gridScores";
            this.gridScores.ReadOnly = true;
            this.gridScores.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridScores.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridScores.RowHeadersVisible = false;
            this.gridScores.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridScores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridScores.Size = new System.Drawing.Size(668, 182);
            this.gridScores.TabIndex = 5;
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
            this.colScore.HeaderText = "Rating";
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            this.colScore.Tag = "colScore";
            //
            // colScoreObservations
            //
            this.colScoreObservations.FillWeight = 58F;
            this.colScoreObservations.HeaderText = "Technical Observations";
            this.colScoreObservations.Name = "colScoreObservations";
            this.colScoreObservations.ReadOnly = true;
            this.colScoreObservations.Tag = "colScoreObservations";
            //
            // lblGeneralObservations
            //
            this.lblGeneralObservations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGeneralObservations.AutoSize = true;
            this.lblGeneralObservations.Location = new System.Drawing.Point(10, 316);
            this.lblGeneralObservations.Name = "lblGeneralObservations";
            this.lblGeneralObservations.Size = new System.Drawing.Size(140, 19);
            this.lblGeneralObservations.TabIndex = 6;
            this.lblGeneralObservations.Tag = "lblGeneralObservations";
            this.lblGeneralObservations.Text = "General Observations:";
            this.lblGeneralObservations.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // tbInspectionObservations
            //
            //
            //
            //
            this.tbInspectionObservations.CustomButton.Image = null;
            this.tbInspectionObservations.CustomButton.Location = new System.Drawing.Point(630, 2);
            this.tbInspectionObservations.CustomButton.Name = "";
            this.tbInspectionObservations.CustomButton.Size = new System.Drawing.Size(35, 35);
            this.tbInspectionObservations.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbInspectionObservations.CustomButton.TabIndex = 1;
            this.tbInspectionObservations.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbInspectionObservations.CustomButton.UseSelectable = true;
            this.tbInspectionObservations.CustomButton.Visible = false;
            this.tbInspectionObservations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInspectionObservations.Lines = new string[0];
            this.tbInspectionObservations.Location = new System.Drawing.Point(10, 336);
            this.tbInspectionObservations.MaxLength = 32767;
            this.tbInspectionObservations.Multiline = true;
            this.tbInspectionObservations.Name = "tbInspectionObservations";
            this.tbInspectionObservations.PasswordChar = '\0';
            this.tbInspectionObservations.PromptText = "General Observations";
            this.tbInspectionObservations.ReadOnly = true;
            this.tbInspectionObservations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInspectionObservations.SelectedText = "";
            this.tbInspectionObservations.SelectionLength = 0;
            this.tbInspectionObservations.SelectionStart = 0;
            this.tbInspectionObservations.ShortcutsEnabled = true;
            this.tbInspectionObservations.Size = new System.Drawing.Size(668, 40);
            this.tbInspectionObservations.TabIndex = 7;
            this.tbInspectionObservations.Tag = "tbInspectionObservations";
            this.tbInspectionObservations.UseSelectable = true;
            this.tbInspectionObservations.WaterMark = "General Observations";
            this.tbInspectionObservations.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbInspectionObservations.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // gridPhotos
            //
            this.gridPhotos.AllowUserToAddRows = false;
            this.gridPhotos.AllowUserToDeleteRows = false;
            this.gridPhotos.AllowUserToResizeRows = false;
            this.gridPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPhotos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPhotos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPhotos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPhotos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridPhotos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridPhotos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPhotos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridPhotos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPhotos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colUploadedDate});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPhotos.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridPhotos.EnableHeadersVisualStyles = false;
            this.gridPhotos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.gridPhotos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridPhotos.Location = new System.Drawing.Point(10, 382);
            this.gridPhotos.Name = "gridPhotos";
            this.gridPhotos.ReadOnly = true;
            this.gridPhotos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPhotos.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridPhotos.RowHeadersVisible = false;
            this.gridPhotos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridPhotos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPhotos.Size = new System.Drawing.Size(538, 66);
            this.gridPhotos.TabIndex = 8;
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
            dataGridViewCellStyle7.Format = "dd/MM/yyyy HH:mm";
            this.colUploadedDate.DefaultCellStyle = dataGridViewCellStyle7;
            this.colUploadedDate.FillWeight = 35F;
            this.colUploadedDate.HeaderText = "Uploaded";
            this.colUploadedDate.Name = "colUploadedDate";
            this.colUploadedDate.ReadOnly = true;
            this.colUploadedDate.Tag = "colUploadedDate";
            //
            // btnOpenPhoto
            //
            this.btnOpenPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPhoto.Location = new System.Drawing.Point(558, 382);
            this.btnOpenPhoto.Name = "btnOpenPhoto";
            this.btnOpenPhoto.Size = new System.Drawing.Size(120, 24);
            this.btnOpenPhoto.TabIndex = 9;
            this.btnOpenPhoto.Tag = "btnOpenPhoto";
            this.btnOpenPhoto.Text = "View Photo";
            this.btnOpenPhoto.UseSelectable = true;
            this.btnOpenPhoto.Click += new System.EventHandler(this.btnOpenPhoto_Click);
            //
            // InspectionView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblInspectionResult);
            this.Controls.Add(this.lblVehicleData);
            this.Controls.Add(this.lblVehicleIdentification);
            this.Controls.Add(this.lblInspectionData);
            this.Controls.Add(this.lblKilometersDifference);
            this.Controls.Add(this.gridScores);
            this.Controls.Add(this.lblGeneralObservations);
            this.Controls.Add(this.tbInspectionObservations);
            this.Controls.Add(this.gridPhotos);
            this.Controls.Add(this.btnOpenPhoto);
            this.Name = "InspectionView";
            this.Size = new System.Drawing.Size(690, 458);
            ((System.ComponentModel.ISupportInitialize)(this.gridScores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPhotos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblInspectionResult;
        private MetroFramework.Controls.MetroLabel lblVehicleData;
        private MetroFramework.Controls.MetroLabel lblVehicleIdentification;
        private MetroFramework.Controls.MetroLabel lblInspectionData;
        private MetroFramework.Controls.MetroLabel lblKilometersDifference;
        private MetroFramework.Controls.MetroGrid gridScores;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScoreObservations;
        private MetroFramework.Controls.MetroLabel lblGeneralObservations;
        private MetroFramework.Controls.MetroTextBox tbInspectionObservations;
        private MetroFramework.Controls.MetroGrid gridPhotos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUploadedDate;
        private MetroFramework.Controls.MetroButton btnOpenPhoto;
    }
}
