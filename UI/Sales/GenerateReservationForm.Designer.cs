namespace UI.Vehicles
{
    partial class GenerateReservationForm
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
            this.lblMake = new MetroFramework.Controls.MetroLabel();
            this.comboMake = new MetroFramework.Controls.MetroComboBox();
            this.comboModel = new MetroFramework.Controls.MetroComboBox();
            this.comboVersions = new MetroFramework.Controls.MetroComboBox();
            this.comboColour = new MetroFramework.Controls.MetroComboBox();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.ConfigureFamilies = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.lblFilters = new MetroFramework.Controls.MetroLabel();
            this.tbDoorsTo = new MetroFramework.Controls.MetroTextBox();
            this.tbDoorsFrom = new MetroFramework.Controls.MetroTextBox();
            this.tbYearFrom = new MetroFramework.Controls.MetroTextBox();
            this.tbPriceTo = new MetroFramework.Controls.MetroTextBox();
            this.tbPriceFrom = new MetroFramework.Controls.MetroTextBox();
            this.tbKilometersTo = new MetroFramework.Controls.MetroTextBox();
            this.tbKilometersFrom = new MetroFramework.Controls.MetroTextBox();
            this.tbYearTo = new MetroFramework.Controls.MetroTextBox();
            this.lblModel = new MetroFramework.Controls.MetroLabel();
            this.lblColour = new MetroFramework.Controls.MetroLabel();
            this.lblVersion = new MetroFramework.Controls.MetroLabel();
            this.clientView1 = new UI.Clients.Controls.ClientView();
            this.btnGenerateReservation = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.ConfigureFamilies.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Location = new System.Drawing.Point(144, 7);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(44, 19);
            this.lblMake.TabIndex = 1;
            this.lblMake.Text = "Make:";
            this.lblMake.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // comboMake
            // 
            this.comboMake.FormattingEnabled = true;
            this.comboMake.ItemHeight = 23;
            this.comboMake.Location = new System.Drawing.Point(191, 5);
            this.comboMake.Name = "comboMake";
            this.comboMake.Size = new System.Drawing.Size(259, 29);
            this.comboMake.TabIndex = 2;
            this.comboMake.UseSelectable = true;
            this.comboMake.SelectedIndexChanged += new System.EventHandler(this.comboDeleteVersionMake_SelectedIndexChanged);
            // 
            // comboModel
            // 
            this.comboModel.FormattingEnabled = true;
            this.comboModel.ItemHeight = 23;
            this.comboModel.Location = new System.Drawing.Point(191, 40);
            this.comboModel.Name = "comboModel";
            this.comboModel.Size = new System.Drawing.Size(259, 29);
            this.comboModel.TabIndex = 4;
            this.comboModel.UseSelectable = true;
            this.comboModel.SelectedIndexChanged += new System.EventHandler(this.comboDeleteVersionModel_SelectedIndexChanged);
            // 
            // comboVersions
            // 
            this.comboVersions.FormattingEnabled = true;
            this.comboVersions.ItemHeight = 23;
            this.comboVersions.Location = new System.Drawing.Point(191, 75);
            this.comboVersions.Name = "comboVersions";
            this.comboVersions.Size = new System.Drawing.Size(259, 29);
            this.comboVersions.TabIndex = 6;
            this.comboVersions.UseSelectable = true;
            this.comboVersions.SelectedIndexChanged += new System.EventHandler(this.comboVersions_SelectedIndexChanged);
            // 
            // comboColour
            // 
            this.comboColour.FormattingEnabled = true;
            this.comboColour.ItemHeight = 23;
            this.comboColour.Location = new System.Drawing.Point(191, 110);
            this.comboColour.Name = "comboColour";
            this.comboColour.Size = new System.Drawing.Size(259, 29);
            this.comboColour.TabIndex = 8;
            this.comboColour.UseSelectable = true;
            this.comboColour.SelectedIndexChanged += new System.EventHandler(this.comboColour_SelectedIndexChanged);
            // 
            // metroGrid1
            // 
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
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(18, 171);
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
            this.metroGrid1.Size = new System.Drawing.Size(1153, 528);
            this.metroGrid1.TabIndex = 2;
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            // 
            // ConfigureFamilies
            // 
            this.ConfigureFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureFamilies.Controls.Add(this.metroPanel1);
            this.ConfigureFamilies.Controls.Add(this.clientView1);
            this.ConfigureFamilies.Controls.Add(this.btnGenerateReservation);
            this.ConfigureFamilies.Controls.Add(this.metroGrid1);
            this.ConfigureFamilies.HorizontalScrollbarBarColor = true;
            this.ConfigureFamilies.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.HorizontalScrollbarSize = 10;
            this.ConfigureFamilies.Location = new System.Drawing.Point(23, 63);
            this.ConfigureFamilies.Name = "ConfigureFamilies";
            this.ConfigureFamilies.Size = new System.Drawing.Size(1192, 777);
            this.ConfigureFamilies.TabIndex = 0;
            this.ConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureFamilies.VerticalScrollbarBarColor = true;
            this.ConfigureFamilies.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.lblFilters);
            this.metroPanel1.Controls.Add(this.comboMake);
            this.metroPanel1.Controls.Add(this.comboVersions);
            this.metroPanel1.Controls.Add(this.comboColour);
            this.metroPanel1.Controls.Add(this.comboModel);
            this.metroPanel1.Controls.Add(this.tbDoorsTo);
            this.metroPanel1.Controls.Add(this.lblMake);
            this.metroPanel1.Controls.Add(this.tbDoorsFrom);
            this.metroPanel1.Controls.Add(this.tbYearFrom);
            this.metroPanel1.Controls.Add(this.tbPriceTo);
            this.metroPanel1.Controls.Add(this.tbPriceFrom);
            this.metroPanel1.Controls.Add(this.tbKilometersTo);
            this.metroPanel1.Controls.Add(this.tbKilometersFrom);
            this.metroPanel1.Controls.Add(this.tbYearTo);
            this.metroPanel1.Controls.Add(this.lblModel);
            this.metroPanel1.Controls.Add(this.lblColour);
            this.metroPanel1.Controls.Add(this.lblVersion);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(421, 20);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(750, 145);
            this.metroPanel1.TabIndex = 1;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // lblFilters
            // 
            this.lblFilters.AutoSize = true;
            this.lblFilters.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFilters.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFilters.Location = new System.Drawing.Point(18, 6);
            this.lblFilters.Name = "lblFilters";
            this.lblFilters.Size = new System.Drawing.Size(117, 25);
            this.lblFilters.TabIndex = 0;
            this.lblFilters.Text = "Vehicle Filters";
            this.lblFilters.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // tbDoorsTo
            // 
            // 
            // 
            // 
            this.tbDoorsTo.CustomButton.Image = null;
            this.tbDoorsTo.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbDoorsTo.CustomButton.Name = "";
            this.tbDoorsTo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbDoorsTo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDoorsTo.CustomButton.TabIndex = 1;
            this.tbDoorsTo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDoorsTo.CustomButton.UseSelectable = true;
            this.tbDoorsTo.CustomButton.Visible = false;
            this.tbDoorsTo.Icon = global::UI.Properties.Resources.user__1_;
            this.tbDoorsTo.Lines = new string[0];
            this.tbDoorsTo.Location = new System.Drawing.Point(614, 114);
            this.tbDoorsTo.MaxLength = 32767;
            this.tbDoorsTo.Name = "tbDoorsTo";
            this.tbDoorsTo.PasswordChar = '\0';
            this.tbDoorsTo.PromptText = "To Doors";
            this.tbDoorsTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDoorsTo.SelectedText = "";
            this.tbDoorsTo.SelectionLength = 0;
            this.tbDoorsTo.SelectionStart = 0;
            this.tbDoorsTo.ShortcutsEnabled = true;
            this.tbDoorsTo.Size = new System.Drawing.Size(126, 23);
            this.tbDoorsTo.TabIndex = 16;
            this.tbDoorsTo.UseSelectable = true;
            this.tbDoorsTo.WaterMark = "To Doors";
            this.tbDoorsTo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDoorsTo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbDoorsTo.Leave += new System.EventHandler(this.tbDoorsTo_Leave);
            // 
            // tbDoorsFrom
            // 
            // 
            // 
            // 
            this.tbDoorsFrom.CustomButton.Image = null;
            this.tbDoorsFrom.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbDoorsFrom.CustomButton.Name = "";
            this.tbDoorsFrom.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbDoorsFrom.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDoorsFrom.CustomButton.TabIndex = 1;
            this.tbDoorsFrom.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDoorsFrom.CustomButton.UseSelectable = true;
            this.tbDoorsFrom.CustomButton.Visible = false;
            this.tbDoorsFrom.Icon = global::UI.Properties.Resources.user__1_;
            this.tbDoorsFrom.Lines = new string[0];
            this.tbDoorsFrom.Location = new System.Drawing.Point(480, 114);
            this.tbDoorsFrom.MaxLength = 32767;
            this.tbDoorsFrom.Name = "tbDoorsFrom";
            this.tbDoorsFrom.PasswordChar = '\0';
            this.tbDoorsFrom.PromptText = "From Doors";
            this.tbDoorsFrom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDoorsFrom.SelectedText = "";
            this.tbDoorsFrom.SelectionLength = 0;
            this.tbDoorsFrom.SelectionStart = 0;
            this.tbDoorsFrom.ShortcutsEnabled = true;
            this.tbDoorsFrom.Size = new System.Drawing.Size(126, 23);
            this.tbDoorsFrom.TabIndex = 15;
            this.tbDoorsFrom.UseSelectable = true;
            this.tbDoorsFrom.WaterMark = "From Doors";
            this.tbDoorsFrom.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDoorsFrom.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbDoorsFrom.Leave += new System.EventHandler(this.tbDoorsFrom_Leave);
            // 
            // tbYearFrom
            // 
            // 
            // 
            // 
            this.tbYearFrom.CustomButton.Image = null;
            this.tbYearFrom.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbYearFrom.CustomButton.Name = "";
            this.tbYearFrom.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbYearFrom.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbYearFrom.CustomButton.TabIndex = 1;
            this.tbYearFrom.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbYearFrom.CustomButton.UseSelectable = true;
            this.tbYearFrom.CustomButton.Visible = false;
            this.tbYearFrom.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbYearFrom.Icon = global::UI.Properties.Resources.user__1_;
            this.tbYearFrom.Lines = new string[0];
            this.tbYearFrom.Location = new System.Drawing.Point(480, 7);
            this.tbYearFrom.MaxLength = 4;
            this.tbYearFrom.Name = "tbYearFrom";
            this.tbYearFrom.PasswordChar = '\0';
            this.tbYearFrom.PromptText = "From Year";
            this.tbYearFrom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbYearFrom.SelectedText = "";
            this.tbYearFrom.SelectionLength = 0;
            this.tbYearFrom.SelectionStart = 0;
            this.tbYearFrom.ShortcutsEnabled = true;
            this.tbYearFrom.Size = new System.Drawing.Size(126, 23);
            this.tbYearFrom.TabIndex = 9;
            this.tbYearFrom.UseSelectable = true;
            this.tbYearFrom.WaterMark = "From Year";
            this.tbYearFrom.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbYearFrom.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbYearFrom.Leave += new System.EventHandler(this.tbYearFrom_Leave);
            // 
            // tbPriceTo
            // 
            // 
            // 
            // 
            this.tbPriceTo.CustomButton.Image = null;
            this.tbPriceTo.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbPriceTo.CustomButton.Name = "";
            this.tbPriceTo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbPriceTo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPriceTo.CustomButton.TabIndex = 1;
            this.tbPriceTo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPriceTo.CustomButton.UseSelectable = true;
            this.tbPriceTo.CustomButton.Visible = false;
            this.tbPriceTo.Icon = global::UI.Properties.Resources.user__1_;
            this.tbPriceTo.Lines = new string[0];
            this.tbPriceTo.Location = new System.Drawing.Point(614, 79);
            this.tbPriceTo.MaxLength = 32767;
            this.tbPriceTo.Name = "tbPriceTo";
            this.tbPriceTo.PasswordChar = '\0';
            this.tbPriceTo.PromptText = "To Price";
            this.tbPriceTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPriceTo.SelectedText = "";
            this.tbPriceTo.SelectionLength = 0;
            this.tbPriceTo.SelectionStart = 0;
            this.tbPriceTo.ShortcutsEnabled = true;
            this.tbPriceTo.Size = new System.Drawing.Size(126, 23);
            this.tbPriceTo.TabIndex = 14;
            this.tbPriceTo.UseSelectable = true;
            this.tbPriceTo.WaterMark = "To Price";
            this.tbPriceTo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPriceTo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbPriceTo.Leave += new System.EventHandler(this.tbPriceTo_Leave);
            // 
            // tbPriceFrom
            // 
            // 
            // 
            // 
            this.tbPriceFrom.CustomButton.Image = null;
            this.tbPriceFrom.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbPriceFrom.CustomButton.Name = "";
            this.tbPriceFrom.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbPriceFrom.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPriceFrom.CustomButton.TabIndex = 1;
            this.tbPriceFrom.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPriceFrom.CustomButton.UseSelectable = true;
            this.tbPriceFrom.CustomButton.Visible = false;
            this.tbPriceFrom.Icon = global::UI.Properties.Resources.user__1_;
            this.tbPriceFrom.Lines = new string[0];
            this.tbPriceFrom.Location = new System.Drawing.Point(480, 79);
            this.tbPriceFrom.MaxLength = 32767;
            this.tbPriceFrom.Name = "tbPriceFrom";
            this.tbPriceFrom.PasswordChar = '\0';
            this.tbPriceFrom.PromptText = "From Price";
            this.tbPriceFrom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPriceFrom.SelectedText = "";
            this.tbPriceFrom.SelectionLength = 0;
            this.tbPriceFrom.SelectionStart = 0;
            this.tbPriceFrom.ShortcutsEnabled = true;
            this.tbPriceFrom.Size = new System.Drawing.Size(126, 23);
            this.tbPriceFrom.TabIndex = 13;
            this.tbPriceFrom.UseSelectable = true;
            this.tbPriceFrom.WaterMark = "From Price";
            this.tbPriceFrom.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPriceFrom.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbPriceFrom.Leave += new System.EventHandler(this.tbPriceFrom_Leave);
            // 
            // tbKilometersTo
            // 
            // 
            // 
            // 
            this.tbKilometersTo.CustomButton.Image = null;
            this.tbKilometersTo.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbKilometersTo.CustomButton.Name = "";
            this.tbKilometersTo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbKilometersTo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbKilometersTo.CustomButton.TabIndex = 1;
            this.tbKilometersTo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbKilometersTo.CustomButton.UseSelectable = true;
            this.tbKilometersTo.CustomButton.Visible = false;
            this.tbKilometersTo.Icon = global::UI.Properties.Resources.user__1_;
            this.tbKilometersTo.Lines = new string[0];
            this.tbKilometersTo.Location = new System.Drawing.Point(614, 43);
            this.tbKilometersTo.MaxLength = 32767;
            this.tbKilometersTo.Name = "tbKilometersTo";
            this.tbKilometersTo.PasswordChar = '\0';
            this.tbKilometersTo.PromptText = "To Kilometers";
            this.tbKilometersTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbKilometersTo.SelectedText = "";
            this.tbKilometersTo.SelectionLength = 0;
            this.tbKilometersTo.SelectionStart = 0;
            this.tbKilometersTo.ShortcutsEnabled = true;
            this.tbKilometersTo.Size = new System.Drawing.Size(126, 23);
            this.tbKilometersTo.TabIndex = 12;
            this.tbKilometersTo.UseSelectable = true;
            this.tbKilometersTo.WaterMark = "To Kilometers";
            this.tbKilometersTo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbKilometersTo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbKilometersTo.Leave += new System.EventHandler(this.tbKilometersTo_Leave);
            // 
            // tbKilometersFrom
            // 
            // 
            // 
            // 
            this.tbKilometersFrom.CustomButton.Image = null;
            this.tbKilometersFrom.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbKilometersFrom.CustomButton.Name = "";
            this.tbKilometersFrom.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbKilometersFrom.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbKilometersFrom.CustomButton.TabIndex = 1;
            this.tbKilometersFrom.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbKilometersFrom.CustomButton.UseSelectable = true;
            this.tbKilometersFrom.CustomButton.Visible = false;
            this.tbKilometersFrom.Icon = global::UI.Properties.Resources.user__1_;
            this.tbKilometersFrom.Lines = new string[0];
            this.tbKilometersFrom.Location = new System.Drawing.Point(480, 43);
            this.tbKilometersFrom.MaxLength = 32767;
            this.tbKilometersFrom.Name = "tbKilometersFrom";
            this.tbKilometersFrom.PasswordChar = '\0';
            this.tbKilometersFrom.PromptText = "From Kilometers";
            this.tbKilometersFrom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbKilometersFrom.SelectedText = "";
            this.tbKilometersFrom.SelectionLength = 0;
            this.tbKilometersFrom.SelectionStart = 0;
            this.tbKilometersFrom.ShortcutsEnabled = true;
            this.tbKilometersFrom.Size = new System.Drawing.Size(126, 23);
            this.tbKilometersFrom.TabIndex = 11;
            this.tbKilometersFrom.UseSelectable = true;
            this.tbKilometersFrom.WaterMark = "From Kilometers";
            this.tbKilometersFrom.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbKilometersFrom.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbKilometersFrom.Leave += new System.EventHandler(this.tbKilometersFrom_Leave);
            // 
            // tbYearTo
            // 
            // 
            // 
            // 
            this.tbYearTo.CustomButton.Image = null;
            this.tbYearTo.CustomButton.Location = new System.Drawing.Point(104, 1);
            this.tbYearTo.CustomButton.Name = "";
            this.tbYearTo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbYearTo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbYearTo.CustomButton.TabIndex = 1;
            this.tbYearTo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbYearTo.CustomButton.UseSelectable = true;
            this.tbYearTo.CustomButton.Visible = false;
            this.tbYearTo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbYearTo.Icon = global::UI.Properties.Resources.user__1_;
            this.tbYearTo.Lines = new string[0];
            this.tbYearTo.Location = new System.Drawing.Point(614, 7);
            this.tbYearTo.MaxLength = 4;
            this.tbYearTo.Name = "tbYearTo";
            this.tbYearTo.PasswordChar = '\0';
            this.tbYearTo.PromptText = "To Year";
            this.tbYearTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbYearTo.SelectedText = "";
            this.tbYearTo.SelectionLength = 0;
            this.tbYearTo.SelectionStart = 0;
            this.tbYearTo.ShortcutsEnabled = true;
            this.tbYearTo.Size = new System.Drawing.Size(126, 23);
            this.tbYearTo.TabIndex = 10;
            this.tbYearTo.UseSelectable = true;
            this.tbYearTo.WaterMark = "To Year";
            this.tbYearTo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbYearTo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbYearTo.Leave += new System.EventHandler(this.tbYearTo_Leave);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(141, 45);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(50, 19);
            this.lblModel.TabIndex = 3;
            this.lblModel.Text = "Model:";
            this.lblModel.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblColour
            // 
            this.lblColour.AutoSize = true;
            this.lblColour.Location = new System.Drawing.Point(137, 114);
            this.lblColour.Name = "lblColour";
            this.lblColour.Size = new System.Drawing.Size(53, 19);
            this.lblColour.TabIndex = 7;
            this.lblColour.Text = "Colour:";
            this.lblColour.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(136, 80);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(54, 19);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Version:";
            this.lblVersion.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // clientView1
            // 
            this.clientView1.Location = new System.Drawing.Point(18, 20);
            this.clientView1.Name = "clientView1";
            this.clientView1.Size = new System.Drawing.Size(363, 132);
            this.clientView1.TabIndex = 0;
            // 
            // btnGenerateReservation
            // 
            this.btnGenerateReservation.Location = new System.Drawing.Point(1004, 716);
            this.btnGenerateReservation.Name = "btnGenerateReservation";
            this.btnGenerateReservation.Size = new System.Drawing.Size(169, 35);
            this.btnGenerateReservation.TabIndex = 3;
            this.btnGenerateReservation.Text = "Generate Reservation";
            this.btnGenerateReservation.UseSelectable = true;
            this.btnGenerateReservation.Click += new System.EventHandler(this.btnGenerateQuotation_Click);
            // 
            // GenerateReservationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 865);
            this.Controls.Add(this.ConfigureFamilies);
            this.Name = "GenerateReservationForm";
            this.Text = "Generate Reservation";
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ConfigureFamilies.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox tbKilometersFrom;
        private MetroFramework.Controls.MetroTextBox tbYearFrom;
        private MetroFramework.Controls.MetroTextBox tbPriceFrom;
        private MetroFramework.Controls.MetroComboBox comboColour;
        private MetroFramework.Controls.MetroComboBox comboVersions;
        private MetroFramework.Controls.MetroComboBox comboModel;
        private MetroFramework.Controls.MetroComboBox comboMake;
        private MetroFramework.Controls.MetroLabel lblMake;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroPanel ConfigureFamilies;
        private MetroFramework.Controls.MetroLabel lblColour;
        private MetroFramework.Controls.MetroLabel lblVersion;
        private MetroFramework.Controls.MetroLabel lblModel;
        private MetroFramework.Controls.MetroTextBox tbKilometersTo;
        private MetroFramework.Controls.MetroTextBox tbYearTo;
        private MetroFramework.Controls.MetroTextBox tbDoorsTo;
        private MetroFramework.Controls.MetroTextBox tbDoorsFrom;
        private MetroFramework.Controls.MetroTextBox tbPriceTo;
        private MetroFramework.Controls.MetroButton btnGenerateReservation;
        private Clients.Controls.ClientView clientView1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel lblFilters;
    }
}