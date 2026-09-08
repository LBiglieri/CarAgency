namespace CarAgency.UI
{
    partial class EventLogForm
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
            this.components = new System.ComponentModel.Container();
            this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.layoutUser = new System.Windows.Forms.TableLayoutPanel();
            this.layoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.comboLogin = new MetroFramework.Controls.MetroComboBox();
            this.comboModule = new MetroFramework.Controls.MetroComboBox();
            this.comboEvent = new MetroFramework.Controls.MetroComboBox();
            this.comboCriticality = new MetroFramework.Controls.MetroComboBox();
            this.dtFrom = new MetroFramework.Controls.MetroDateTime();
            this.dtTo = new MetroFramework.Controls.MetroDateTime();
            this.tbName = new MetroFramework.Controls.MetroTextBox();
            this.tbSurname = new MetroFramework.Controls.MetroTextBox();
            this.lblLogin = new MetroFramework.Controls.MetroLabel();
            this.lblFrom = new MetroFramework.Controls.MetroLabel();
            this.lblTo = new MetroFramework.Controls.MetroLabel();
            this.lblModule = new MetroFramework.Controls.MetroLabel();
            this.lblEvent = new MetroFramework.Controls.MetroLabel();
            this.lblCriticality = new MetroFramework.Controls.MetroLabel();
            this.lblName = new MetroFramework.Controls.MetroLabel();
            this.lblSurname = new MetroFramework.Controls.MetroLabel();
            this.lblCount = new MetroFramework.Controls.MetroLabel();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            this.btnApply = new MetroFramework.Controls.MetroButton();
            this.btnPrint = new MetroFramework.Controls.MetroButton();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.colLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriticality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.components.Add(this.printDocument1);
            this.components.Add(this.printDialog1);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.layoutMain.SuspendLayout();
            this.layoutUser.SuspendLayout();
            this.layoutFilters.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // layoutMain
            //
            this.layoutMain.BackColor = System.Drawing.Color.Transparent;
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Name = "layoutMain";
            //
            // layoutUser
            //
            this.layoutUser.BackColor = System.Drawing.Color.Transparent;
            this.layoutUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutUser.Name = "layoutUser";
            //
            // layoutFilters
            //
            this.layoutFilters.BackColor = System.Drawing.Color.Transparent;
            this.layoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFilters.Name = "layoutFilters";
            this.layoutMain.ColumnCount = 1;
            this.layoutMain.RowCount = 5;
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layoutMain.Controls.Add(this.metroGrid1, 0, 0);
            this.layoutMain.Controls.Add(this.layoutUser, 0, 1);
            this.layoutMain.Controls.Add(this.layoutFilters, 0, 2);
            this.layoutMain.Controls.Add(this.lblCount, 0, 3);
            this.layoutMain.Controls.Add(this.panelButtons, 0, 4);
            this.layoutUser.ColumnCount = 4;
            this.layoutUser.RowCount = 1;
            this.layoutUser.Padding = new System.Windows.Forms.Padding(10, 8, 10, 4);
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutUser.Controls.Add(this.lblName, 0, 0);
            this.layoutUser.Controls.Add(this.tbName, 1, 0);
            this.layoutUser.Controls.Add(this.lblSurname, 2, 0);
            this.layoutUser.Controls.Add(this.tbSurname, 3, 0);
            this.layoutFilters.ColumnCount = 6;
            this.layoutFilters.RowCount = 2;
            this.layoutFilters.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFilters.Controls.Add(this.lblLogin, 0, 0);
            this.layoutFilters.Controls.Add(this.comboLogin, 1, 0);
            this.layoutFilters.Controls.Add(this.lblFrom, 2, 0);
            this.layoutFilters.Controls.Add(this.dtFrom, 3, 0);
            this.layoutFilters.Controls.Add(this.lblTo, 4, 0);
            this.layoutFilters.Controls.Add(this.dtTo, 5, 0);
            this.layoutFilters.Controls.Add(this.lblModule, 0, 1);
            this.layoutFilters.Controls.Add(this.comboModule, 1, 1);
            this.layoutFilters.Controls.Add(this.lblEvent, 2, 1);
            this.layoutFilters.Controls.Add(this.comboEvent, 3, 1);
            this.layoutFilters.Controls.Add(this.lblCriticality, 4, 1);
            this.layoutFilters.Controls.Add(this.comboCriticality, 5, 1);
            //
            // panelButtons
            //
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Controls.Add(this.btnExit);
            this.panelButtons.Controls.Add(this.btnPrint);
            this.panelButtons.Controls.Add(this.btnApply);
            this.panelButtons.Controls.Add(this.btnClear);
            //
            // metroGrid1
            //
            this.metroGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.ReadOnly = true;
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToOrderColumns = false;
            this.metroGrid1.MultiSelect = false;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.RowHeadersVisible = false;
            this.metroGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.metroGrid1.AutoGenerateColumns = false;
            this.metroGrid1.TabIndex = 0;
            this.metroGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colLogin, this.colDate, this.colTime, this.colModule, this.colEvent, this.colCriticality });
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            this.metroGrid1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.metroGrid1_CellFormatting);
            //
            // colLogin
            //
            this.colLogin.DataPropertyName = "Login";
            this.colLogin.HeaderText = "Login";
            this.colLogin.Name = "colLogin";
            this.colLogin.ReadOnly = true;
            this.colLogin.FillWeight = 100F;
            this.colLogin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDate
            //
            this.colDate.DataPropertyName = "OccurredAt";
            this.colDate.HeaderText = "Fecha";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.FillWeight = 100F;
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            //
            // colTime
            //
            this.colTime.DataPropertyName = "OccurredAt";
            this.colTime.HeaderText = "Hora";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.FillWeight = 100F;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.DefaultCellStyle.Format = "HH:mm:ss";
            //
            // colModule
            //
            this.colModule.DataPropertyName = "Module";
            this.colModule.HeaderText = "Módulo";
            this.colModule.Name = "colModule";
            this.colModule.ReadOnly = true;
            this.colModule.FillWeight = 100F;
            this.colModule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colEvent
            //
            this.colEvent.DataPropertyName = "EventType";
            this.colEvent.HeaderText = "Evento";
            this.colEvent.Name = "colEvent";
            this.colEvent.ReadOnly = true;
            this.colEvent.FillWeight = 190F;
            this.colEvent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colCriticality
            //
            this.colCriticality.DataPropertyName = "Criticality";
            this.colCriticality.HeaderText = "Criticidad";
            this.colCriticality.Name = "colCriticality";
            this.colCriticality.ReadOnly = true;
            this.colCriticality.FillWeight = 100F;
            this.colCriticality.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // comboLogin
            //
            this.comboLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLogin.FormattingEnabled = true;
            this.comboLogin.UseSelectable = true;
            this.comboLogin.Name = "comboLogin";
            this.comboLogin.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboLogin.TabIndex = 0;
            this.comboLogin.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            //
            // comboModule
            //
            this.comboModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboModule.FormattingEnabled = true;
            this.comboModule.UseSelectable = true;
            this.comboModule.Name = "comboModule";
            this.comboModule.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboModule.TabIndex = 1;
            this.comboModule.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            this.comboModule.SelectedIndexChanged += new System.EventHandler(this.comboModule_SelectedIndexChanged);
            //
            // comboEvent
            //
            this.comboEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEvent.FormattingEnabled = true;
            this.comboEvent.UseSelectable = true;
            this.comboEvent.Name = "comboEvent";
            this.comboEvent.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboEvent.TabIndex = 2;
            this.comboEvent.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            //
            // comboCriticality
            //
            this.comboCriticality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboCriticality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCriticality.FormattingEnabled = true;
            this.comboCriticality.UseSelectable = true;
            this.comboCriticality.Name = "comboCriticality";
            this.comboCriticality.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboCriticality.TabIndex = 3;
            this.comboCriticality.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            this.comboCriticality.Items.AddRange(new object[] { "", 1, 2, 3, 4, 5 });
            //
            // dtFrom
            //
            this.dtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.CustomFormat = "dd/MM/yyyy";
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            //
            // dtTo
            //
            this.dtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.CustomFormat = "dd/MM/yyyy";
            this.dtTo.Name = "dtTo";
            this.dtTo.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            //
            // tbName
            //
            this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbName.ReadOnly = true;
            this.tbName.UseSelectable = true;
            this.tbName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbName.Name = "tbName";
            //
            // tbSurname
            //
            this.tbSurname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSurname.ReadOnly = true;
            this.tbSurname.UseSelectable = true;
            this.tbSurname.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbSurname.Name = "tbSurname";
            //
            // lblLogin
            //
            this.lblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogin.AutoSize = false;
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Tag = "AuditLogin";
            this.lblLogin.Text = "Login";
            //
            // lblFrom
            //
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.AutoSize = false;
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Tag = "AuditFrom";
            this.lblFrom.Text = "Fecha inicial";
            //
            // lblTo
            //
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.AutoSize = false;
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTo.Name = "lblTo";
            this.lblTo.Tag = "AuditTo";
            this.lblTo.Text = "Fecha final";
            //
            // lblModule
            //
            this.lblModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModule.AutoSize = false;
            this.lblModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblModule.Name = "lblModule";
            this.lblModule.Tag = "AuditModule";
            this.lblModule.Text = "Módulo";
            //
            // lblEvent
            //
            this.lblEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEvent.AutoSize = false;
            this.lblEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Tag = "AuditEvent";
            this.lblEvent.Text = "Evento";
            //
            // lblCriticality
            //
            this.lblCriticality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriticality.AutoSize = false;
            this.lblCriticality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCriticality.Name = "lblCriticality";
            this.lblCriticality.Tag = "AuditCriticality";
            this.lblCriticality.Text = "Criticidad";
            //
            // lblName
            //
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.AutoSize = false;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.Name = "lblName";
            this.lblName.Tag = "AuditName";
            this.lblName.Text = "Nombre";
            //
            // lblSurname
            //
            this.lblSurname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSurname.AutoSize = false;
            this.lblSurname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Tag = "AuditSurname";
            this.lblSurname.Text = "Apellido";
            //
            // lblCount
            //
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.AutoSize = false;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCount.Name = "lblCount";
            this.lblCount.Tag = "AuditCount";
            this.lblCount.Text = "0 eventos";
            //
            // btnClear
            //
            this.btnClear.Size = new System.Drawing.Size(130, 35);
            this.btnClear.UseSelectable = true;
            this.btnClear.Name = "btnClear";
            this.btnClear.Tag = "AuditClear";
            this.btnClear.Text = "Limpiar";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // btnApply
            //
            this.btnApply.Size = new System.Drawing.Size(130, 35);
            this.btnApply.UseSelectable = true;
            this.btnApply.Name = "btnApply";
            this.btnApply.Tag = "AuditApply";
            this.btnApply.Text = "Aplicar";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            //
            // btnPrint
            //
            this.btnPrint.Size = new System.Drawing.Size(130, 35);
            this.btnPrint.UseSelectable = true;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Tag = "AuditPrint";
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.Enabled = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //
            // btnExit
            //
            this.btnExit.Size = new System.Drawing.Size(130, 35);
            this.btnExit.UseSelectable = true;
            this.btnExit.Name = "btnExit";
            this.btnExit.Tag = "AuditClose";
            this.btnExit.Text = "Salir";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            //
            // printDocument1
            //
            this.printDocument1.DefaultPageSettings.Landscape = true;
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            //
            // printDialog1
            //
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            this.printDialog1.AllowSomePages = false;
            //
            // EventLogForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "EventLogForm";
            this.Tag = "EventLogForm";
            this.Text = "Bitácora de eventos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Controls.Add(this.layoutMain);
            this.Shown += new System.EventHandler(this.EventLogForm_Shown);
            this.Disposed += new System.EventHandler(this.EventLogForm_Disposed);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.layoutMain.ResumeLayout(false);
            this.layoutUser.ResumeLayout(false);
            this.layoutFilters.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutMain;
        private System.Windows.Forms.TableLayoutPanel layoutUser;
        private System.Windows.Forms.TableLayoutPanel layoutFilters;
        private System.Windows.Forms.FlowLayoutPanel panelButtons;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroComboBox comboLogin;
        private MetroFramework.Controls.MetroComboBox comboModule;
        private MetroFramework.Controls.MetroComboBox comboEvent;
        private MetroFramework.Controls.MetroComboBox comboCriticality;
        private MetroFramework.Controls.MetroDateTime dtFrom;
        private MetroFramework.Controls.MetroDateTime dtTo;
        private MetroFramework.Controls.MetroTextBox tbName;
        private MetroFramework.Controls.MetroTextBox tbSurname;
        private MetroFramework.Controls.MetroLabel lblLogin;
        private MetroFramework.Controls.MetroLabel lblFrom;
        private MetroFramework.Controls.MetroLabel lblTo;
        private MetroFramework.Controls.MetroLabel lblModule;
        private MetroFramework.Controls.MetroLabel lblEvent;
        private MetroFramework.Controls.MetroLabel lblCriticality;
        private MetroFramework.Controls.MetroLabel lblName;
        private MetroFramework.Controls.MetroLabel lblSurname;
        private MetroFramework.Controls.MetroLabel lblCount;
        private MetroFramework.Controls.MetroButton btnClear;
        private MetroFramework.Controls.MetroButton btnApply;
        private MetroFramework.Controls.MetroButton btnPrint;
        private MetroFramework.Controls.MetroButton btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCriticality;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}
