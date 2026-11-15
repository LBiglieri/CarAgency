namespace UI.Vehiculos
{
    partial class BitacoraCambiosForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLicensePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNewValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehicle = new MetroFramework.Controls.MetroLabel();
            this.comboVehicle = new MetroFramework.Controls.MetroComboBox();
            this.lblOperation = new MetroFramework.Controls.MetroLabel();
            this.comboOperation = new MetroFramework.Controls.MetroComboBox();
            this.lblFrom = new MetroFramework.Controls.MetroLabel();
            this.dtFrom = new MetroFramework.Controls.MetroDateTime();
            this.lblTo = new MetroFramework.Controls.MetroLabel();
            this.dtTo = new MetroFramework.Controls.MetroDateTime();
            this.lblCount = new MetroFramework.Controls.MetroLabel();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.btnPrint = new MetroFramework.Controls.MetroButton();
            this.btnApply = new MetroFramework.Controls.MetroButton();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.layoutFilters.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // printDocument1
            //
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            //
            // printDialog1
            //
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            //
            // layoutMain
            //
            this.layoutMain.BackColor = System.Drawing.Color.Transparent;
            this.layoutMain.ColumnCount = 1;
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.Controls.Add(this.metroGrid1, 0, 0);
            this.layoutMain.Controls.Add(this.layoutFilters, 0, 1);
            this.layoutMain.Controls.Add(this.lblCount, 0, 2);
            this.layoutMain.Controls.Add(this.panelButtons, 0, 3);
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Location = new System.Drawing.Point(20, 60);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.RowCount = 4;
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layoutMain.Size = new System.Drawing.Size(1060, 480);
            this.layoutMain.TabIndex = 0;
            //
            // metroGrid1
            //
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.metroGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colTime,
            this.colLicensePlate,
            this.colOperation,
            this.colField,
            this.colOldValue,
            this.colNewValue,
            this.colUser});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(3, 3);
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
            this.metroGrid1.RowHeadersVisible = false;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(1054, 342);
            this.metroGrid1.TabIndex = 0;
            this.metroGrid1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.metroGrid1_CellFormatting);
            //
            // colDate
            //
            this.colDate.DataPropertyName = "Fecha";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            this.colDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDate.FillWeight = 80F;
            this.colDate.HeaderText = "Fecha";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colTime
            //
            this.colTime.DataPropertyName = "Fecha";
            dataGridViewCellStyle5.Format = "HH:mm:ss";
            this.colTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTime.FillWeight = 70F;
            this.colTime.HeaderText = "Hora";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colLicensePlate
            //
            this.colLicensePlate.DataPropertyName = "Patente";
            this.colLicensePlate.FillWeight = 80F;
            this.colLicensePlate.HeaderText = "Patente";
            this.colLicensePlate.Name = "colLicensePlate";
            this.colLicensePlate.ReadOnly = true;
            this.colLicensePlate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOperation
            //
            this.colOperation.DataPropertyName = "Operacion";
            this.colOperation.FillWeight = 90F;
            this.colOperation.HeaderText = "Operación";
            this.colOperation.Name = "colOperation";
            this.colOperation.ReadOnly = true;
            this.colOperation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colField
            //
            this.colField.DataPropertyName = "Campo";
            this.colField.FillWeight = 90F;
            this.colField.HeaderText = "Campo";
            this.colField.Name = "colField";
            this.colField.ReadOnly = true;
            this.colField.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colOldValue
            //
            this.colOldValue.DataPropertyName = "ValorAnterior";
            this.colOldValue.FillWeight = 150F;
            this.colOldValue.HeaderText = "Valor anterior";
            this.colOldValue.Name = "colOldValue";
            this.colOldValue.ReadOnly = true;
            this.colOldValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colNewValue
            //
            this.colNewValue.DataPropertyName = "ValorNuevo";
            this.colNewValue.FillWeight = 150F;
            this.colNewValue.HeaderText = "Valor nuevo";
            this.colNewValue.Name = "colNewValue";
            this.colNewValue.ReadOnly = true;
            this.colNewValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colUser
            //
            this.colUser.DataPropertyName = "Login";
            this.colUser.FillWeight = 90F;
            this.colUser.HeaderText = "Usuario";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            this.colUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // layoutFilters
            //
            this.layoutFilters.BackColor = System.Drawing.Color.Transparent;
            this.layoutFilters.ColumnCount = 6;
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutFilters.Controls.Add(this.lblVehicle, 0, 0);
            this.layoutFilters.Controls.Add(this.comboVehicle, 1, 0);
            this.layoutFilters.Controls.Add(this.lblOperation, 4, 0);
            this.layoutFilters.Controls.Add(this.comboOperation, 5, 0);
            this.layoutFilters.Controls.Add(this.lblFrom, 0, 1);
            this.layoutFilters.Controls.Add(this.dtFrom, 1, 1);
            this.layoutFilters.Controls.Add(this.lblTo, 2, 1);
            this.layoutFilters.Controls.Add(this.dtTo, 3, 1);
            this.layoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFilters.Location = new System.Drawing.Point(3, 351);
            this.layoutFilters.Name = "layoutFilters";
            this.layoutFilters.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.layoutFilters.RowCount = 2;
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFilters.Size = new System.Drawing.Size(1054, 94);
            this.layoutFilters.TabIndex = 1;
            //
            // lblVehicle
            //
            this.lblVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle.Location = new System.Drawing.Point(13, 0);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(104, 47);
            this.lblVehicle.TabIndex = 0;
            this.lblVehicle.Tag = "ChangeLogVehicle";
            this.lblVehicle.Text = "Vehículo";
            this.lblVehicle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // comboVehicle
            //
            this.layoutFilters.SetColumnSpan(this.comboVehicle, 3);
            this.comboVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboVehicle.FormattingEnabled = true;
            this.comboVehicle.ItemHeight = 23;
            this.comboVehicle.Location = new System.Drawing.Point(125, 7);
            this.comboVehicle.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboVehicle.Name = "comboVehicle";
            this.comboVehicle.Size = new System.Drawing.Size(569, 29);
            this.comboVehicle.TabIndex = 0;
            this.comboVehicle.UseSelectable = true;
            this.comboVehicle.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            //
            // lblOperation
            //
            this.lblOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperation.Location = new System.Drawing.Point(709, 0);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(104, 47);
            this.lblOperation.TabIndex = 1;
            this.lblOperation.Tag = "ChangeLogOperation";
            this.lblOperation.Text = "Operación";
            this.lblOperation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // comboOperation
            //
            this.comboOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboOperation.FormattingEnabled = true;
            this.comboOperation.ItemHeight = 23;
            this.comboOperation.Location = new System.Drawing.Point(821, 7);
            this.comboOperation.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboOperation.Name = "comboOperation";
            this.comboOperation.Size = new System.Drawing.Size(221, 29);
            this.comboOperation.TabIndex = 1;
            this.comboOperation.UseSelectable = true;
            this.comboOperation.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            //
            // lblFrom
            //
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.Location = new System.Drawing.Point(13, 47);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(104, 47);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Tag = "AuditFrom";
            this.lblFrom.Text = "Fecha inicial";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dtFrom
            //
            this.dtFrom.CustomFormat = "dd/MM/yyyy";
            this.dtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(125, 54);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.dtFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(221, 29);
            this.dtFrom.TabIndex = 2;
            //
            // lblTo
            //
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.Location = new System.Drawing.Point(361, 47);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(104, 47);
            this.lblTo.TabIndex = 3;
            this.lblTo.Tag = "AuditTo";
            this.lblTo.Text = "Fecha final";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dtTo
            //
            this.dtTo.CustomFormat = "dd/MM/yyyy";
            this.dtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(473, 54);
            this.dtTo.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.dtTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(221, 29);
            this.dtTo.TabIndex = 3;
            //
            // lblCount
            //
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(3, 448);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(1054, 32);
            this.lblCount.TabIndex = 2;
            this.lblCount.Tag = "ChangeLogCount";
            this.lblCount.Text = "0 cambios";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelButtons
            //
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnExit);
            this.panelButtons.Controls.Add(this.btnPrint);
            this.panelButtons.Controls.Add(this.btnApply);
            this.panelButtons.Controls.Add(this.btnClear);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtons.Location = new System.Drawing.Point(3, 483);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1054, 44);
            this.panelButtons.TabIndex = 3;
            //
            // btnExit
            //
            this.btnExit.Location = new System.Drawing.Point(921, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(130, 35);
            this.btnExit.TabIndex = 0;
            this.btnExit.Tag = "AuditClose";
            this.btnExit.Text = "Salir";
            this.btnExit.UseSelectable = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            //
            // btnPrint
            //
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(785, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(130, 35);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Tag = "AuditPrint";
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.UseSelectable = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //
            // btnApply
            //
            this.btnApply.Location = new System.Drawing.Point(649, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(130, 35);
            this.btnApply.TabIndex = 2;
            this.btnApply.Tag = "AuditApply";
            this.btnApply.Text = "Aplicar";
            this.btnApply.UseSelectable = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(513, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(130, 35);
            this.btnClear.TabIndex = 3;
            this.btnClear.Tag = "AuditClear";
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseSelectable = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // BitacoraCambiosForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 560);
            this.Controls.Add(this.layoutMain);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "BitacoraCambiosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "BitacoraCambiosForm";
            this.Text = "Bitácora de cambios";
            this.Shown += new System.EventHandler(this.BitacoraCambiosForm_Shown);
            this.Disposed += new System.EventHandler(this.BitacoraCambiosForm_Disposed);
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.layoutFilters.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutMain;
        private System.Windows.Forms.TableLayoutPanel layoutFilters;
        private System.Windows.Forms.FlowLayoutPanel panelButtons;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroComboBox comboVehicle;
        private MetroFramework.Controls.MetroComboBox comboOperation;
        private MetroFramework.Controls.MetroDateTime dtFrom;
        private MetroFramework.Controls.MetroDateTime dtTo;
        private MetroFramework.Controls.MetroLabel lblVehicle;
        private MetroFramework.Controls.MetroLabel lblOperation;
        private MetroFramework.Controls.MetroLabel lblFrom;
        private MetroFramework.Controls.MetroLabel lblTo;
        private MetroFramework.Controls.MetroLabel lblCount;
        private MetroFramework.Controls.MetroButton btnClear;
        private MetroFramework.Controls.MetroButton btnApply;
        private MetroFramework.Controls.MetroButton btnPrint;
        private MetroFramework.Controls.MetroButton btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicensePlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOldValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}
