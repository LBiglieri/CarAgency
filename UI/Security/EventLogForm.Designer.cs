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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.layoutUser = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new MetroFramework.Controls.MetroLabel();
            this.tbName = new MetroFramework.Controls.MetroTextBox();
            this.lblSurname = new MetroFramework.Controls.MetroLabel();
            this.tbSurname = new MetroFramework.Controls.MetroTextBox();
            this.layoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogin = new MetroFramework.Controls.MetroLabel();
            this.comboLogin = new MetroFramework.Controls.MetroComboBox();
            this.lblFrom = new MetroFramework.Controls.MetroLabel();
            this.dtFrom = new MetroFramework.Controls.MetroDateTime();
            this.lblTo = new MetroFramework.Controls.MetroLabel();
            this.dtTo = new MetroFramework.Controls.MetroDateTime();
            this.lblModule = new MetroFramework.Controls.MetroLabel();
            this.comboModule = new MetroFramework.Controls.MetroComboBox();
            this.lblEvent = new MetroFramework.Controls.MetroLabel();
            this.comboEvent = new MetroFramework.Controls.MetroComboBox();
            this.lblCriticality = new MetroFramework.Controls.MetroLabel();
            this.comboCriticality = new MetroFramework.Controls.MetroComboBox();
            this.lblCount = new MetroFramework.Controls.MetroLabel();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.btnPrint = new MetroFramework.Controls.MetroButton();
            this.btnApply = new MetroFramework.Controls.MetroButton();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            this.colLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCriticality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.layoutUser.SuspendLayout();
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
            this.layoutMain.Controls.Add(this.layoutUser, 0, 1);
            this.layoutMain.Controls.Add(this.layoutFilters, 0, 2);
            this.layoutMain.Controls.Add(this.lblCount, 0, 3);
            this.layoutMain.Controls.Add(this.panelButtons, 0, 4);
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Location = new System.Drawing.Point(20, 60);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.RowCount = 5;
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
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
            this.colLogin,
            this.colDate,
            this.colTime,
            this.colModule,
            this.colEvent,
            this.colCriticality});
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
            this.metroGrid1.Size = new System.Drawing.Size(1054, 244);
            this.metroGrid1.TabIndex = 0;
            this.metroGrid1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.metroGrid1_CellFormatting);
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            //
            // colLogin
            //
            this.colLogin.DataPropertyName = "Login";
            this.colLogin.HeaderText = "Login";
            this.colLogin.Name = "colLogin";
            this.colLogin.ReadOnly = true;
            this.colLogin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colDate
            //
            this.colDate.DataPropertyName = "OccurredAt";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            this.colDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDate.HeaderText = "Fecha";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colTime
            //
            this.colTime.DataPropertyName = "OccurredAt";
            dataGridViewCellStyle5.Format = "HH:mm:ss";
            this.colTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTime.HeaderText = "Hora";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colModule
            //
            this.colModule.DataPropertyName = "Module";
            this.colModule.HeaderText = "Módulo";
            this.colModule.Name = "colModule";
            this.colModule.ReadOnly = true;
            this.colModule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colEvent
            //
            this.colEvent.DataPropertyName = "EventType";
            this.colEvent.FillWeight = 190F;
            this.colEvent.HeaderText = "Evento";
            this.colEvent.Name = "colEvent";
            this.colEvent.ReadOnly = true;
            this.colEvent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colCriticality
            //
            this.colCriticality.DataPropertyName = "Criticality";
            this.colCriticality.HeaderText = "Criticidad";
            this.colCriticality.Name = "colCriticality";
            this.colCriticality.ReadOnly = true;
            this.colCriticality.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // layoutUser
            // 
            this.layoutUser.BackColor = System.Drawing.Color.Transparent;
            this.layoutUser.ColumnCount = 4;
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutUser.Controls.Add(this.lblName, 0, 0);
            this.layoutUser.Controls.Add(this.tbName, 1, 0);
            this.layoutUser.Controls.Add(this.lblSurname, 2, 0);
            this.layoutUser.Controls.Add(this.tbSurname, 3, 0);
            this.layoutUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutUser.Location = new System.Drawing.Point(3, 253);
            this.layoutUser.Name = "layoutUser";
            this.layoutUser.Padding = new System.Windows.Forms.Padding(10, 8, 10, 4);
            this.layoutUser.RowCount = 1;
            this.layoutUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutUser.Size = new System.Drawing.Size(1054, 42);
            this.layoutUser.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(13, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(94, 30);
            this.lblName.TabIndex = 0;
            this.lblName.Tag = "AuditName";
            this.lblName.Text = "Nombre";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbName
            // 
            // 
            // 
            // 
            this.tbName.CustomButton.Image = null;
            this.tbName.CustomButton.Location = new System.Drawing.Point(389, 2);
            this.tbName.CustomButton.Name = "";
            this.tbName.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.tbName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbName.CustomButton.TabIndex = 1;
            this.tbName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbName.CustomButton.UseSelectable = true;
            this.tbName.CustomButton.Visible = false;
            this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbName.Lines = new string[0];
            this.tbName.Location = new System.Drawing.Point(113, 11);
            this.tbName.MaxLength = 32767;
            this.tbName.Name = "tbName";
            this.tbName.PasswordChar = '\0';
            this.tbName.ReadOnly = true;
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbName.SelectedText = "";
            this.tbName.SelectionLength = 0;
            this.tbName.SelectionStart = 0;
            this.tbName.ShortcutsEnabled = true;
            this.tbName.Size = new System.Drawing.Size(411, 24);
            this.tbName.TabIndex = 1;
            this.tbName.UseSelectable = true;
            this.tbName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblSurname
            // 
            this.lblSurname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSurname.Location = new System.Drawing.Point(530, 8);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(94, 30);
            this.lblSurname.TabIndex = 2;
            this.lblSurname.Tag = "AuditSurname";
            this.lblSurname.Text = "Apellido";
            this.lblSurname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSurname
            // 
            // 
            // 
            // 
            this.tbSurname.CustomButton.Image = null;
            this.tbSurname.CustomButton.Location = new System.Drawing.Point(389, 2);
            this.tbSurname.CustomButton.Name = "";
            this.tbSurname.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.tbSurname.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbSurname.CustomButton.TabIndex = 1;
            this.tbSurname.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbSurname.CustomButton.UseSelectable = true;
            this.tbSurname.CustomButton.Visible = false;
            this.tbSurname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSurname.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbSurname.Lines = new string[0];
            this.tbSurname.Location = new System.Drawing.Point(630, 11);
            this.tbSurname.MaxLength = 32767;
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.PasswordChar = '\0';
            this.tbSurname.ReadOnly = true;
            this.tbSurname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbSurname.SelectedText = "";
            this.tbSurname.SelectionLength = 0;
            this.tbSurname.SelectionStart = 0;
            this.tbSurname.ShortcutsEnabled = true;
            this.tbSurname.Size = new System.Drawing.Size(411, 24);
            this.tbSurname.TabIndex = 3;
            this.tbSurname.UseSelectable = true;
            this.tbSurname.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbSurname.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
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
            this.layoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFilters.Location = new System.Drawing.Point(3, 301);
            this.layoutFilters.Name = "layoutFilters";
            this.layoutFilters.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.layoutFilters.RowCount = 2;
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutFilters.Size = new System.Drawing.Size(1054, 94);
            this.layoutFilters.TabIndex = 2;
            // 
            // lblLogin
            // 
            this.lblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogin.Location = new System.Drawing.Point(13, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(104, 47);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Tag = "AuditLogin";
            this.lblLogin.Text = "Login";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboLogin
            // 
            this.comboLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboLogin.FormattingEnabled = true;
            this.comboLogin.ItemHeight = 23;
            this.comboLogin.Location = new System.Drawing.Point(125, 7);
            this.comboLogin.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboLogin.Name = "comboLogin";
            this.comboLogin.Size = new System.Drawing.Size(221, 29);
            this.comboLogin.TabIndex = 0;
            this.comboLogin.UseSelectable = true;
            this.comboLogin.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            // 
            // lblFrom
            // 
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.Location = new System.Drawing.Point(361, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(104, 47);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Tag = "AuditFrom";
            this.lblFrom.Text = "Fecha inicial";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd/MM/yyyy";
            this.dtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(473, 7);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.dtFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(221, 29);
            this.dtFrom.TabIndex = 2;
            // 
            // lblTo
            // 
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.Location = new System.Drawing.Point(709, 0);
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
            this.dtTo.Location = new System.Drawing.Point(821, 7);
            this.dtTo.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.dtTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(221, 29);
            this.dtTo.TabIndex = 4;
            // 
            // lblModule
            // 
            this.lblModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModule.Location = new System.Drawing.Point(13, 47);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(104, 47);
            this.lblModule.TabIndex = 5;
            this.lblModule.Tag = "AuditModule";
            this.lblModule.Text = "Módulo";
            this.lblModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboModule
            // 
            this.comboModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboModule.FormattingEnabled = true;
            this.comboModule.ItemHeight = 23;
            this.comboModule.Location = new System.Drawing.Point(125, 54);
            this.comboModule.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboModule.Name = "comboModule";
            this.comboModule.Size = new System.Drawing.Size(221, 29);
            this.comboModule.TabIndex = 1;
            this.comboModule.UseSelectable = true;
            this.comboModule.SelectedIndexChanged += new System.EventHandler(this.comboModule_SelectedIndexChanged);
            this.comboModule.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            // 
            // lblEvent
            // 
            this.lblEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEvent.Location = new System.Drawing.Point(361, 47);
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Size = new System.Drawing.Size(104, 47);
            this.lblEvent.TabIndex = 6;
            this.lblEvent.Tag = "AuditEvent";
            this.lblEvent.Text = "Evento";
            this.lblEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboEvent
            // 
            this.comboEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboEvent.FormattingEnabled = true;
            this.comboEvent.ItemHeight = 23;
            this.comboEvent.Location = new System.Drawing.Point(473, 54);
            this.comboEvent.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboEvent.Name = "comboEvent";
            this.comboEvent.Size = new System.Drawing.Size(221, 29);
            this.comboEvent.TabIndex = 2;
            this.comboEvent.UseSelectable = true;
            this.comboEvent.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            // 
            // lblCriticality
            // 
            this.lblCriticality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriticality.Location = new System.Drawing.Point(709, 47);
            this.lblCriticality.Name = "lblCriticality";
            this.lblCriticality.Size = new System.Drawing.Size(104, 47);
            this.lblCriticality.TabIndex = 7;
            this.lblCriticality.Tag = "AuditCriticality";
            this.lblCriticality.Text = "Criticidad";
            this.lblCriticality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboCriticality
            // 
            this.comboCriticality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboCriticality.FormattingEnabled = true;
            this.comboCriticality.ItemHeight = 23;
            this.comboCriticality.Items.AddRange(new object[] {
            "",
            1,
            2,
            3,
            4,
            5});
            this.comboCriticality.Location = new System.Drawing.Point(821, 54);
            this.comboCriticality.Margin = new System.Windows.Forms.Padding(5, 7, 12, 7);
            this.comboCriticality.Name = "comboCriticality";
            this.comboCriticality.Size = new System.Drawing.Size(221, 29);
            this.comboCriticality.TabIndex = 3;
            this.comboCriticality.UseSelectable = true;
            this.comboCriticality.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboFilters_Format);
            // 
            // lblCount
            // 
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(3, 398);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(1054, 32);
            this.lblCount.TabIndex = 3;
            this.lblCount.Tag = "AuditCount";
            this.lblCount.Text = "0 eventos";
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
            this.panelButtons.Location = new System.Drawing.Point(3, 433);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1054, 44);
            this.panelButtons.TabIndex = 4;
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
            // EventLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 560);
            this.Controls.Add(this.layoutMain);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "EventLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "EventLogForm";
            this.Text = "Bitácora de eventos";
            this.Shown += new System.EventHandler(this.EventLogForm_Shown);
            this.Disposed += new System.EventHandler(this.EventLogForm_Disposed);
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
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
