namespace UI.Vehicles
{
    partial class VehicleManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehicleManagementForm));
            this.ConfigureFamilies = new MetroFramework.Controls.MetroPanel();
            this.btnDeleteUser = new MetroFramework.Controls.MetroButton();
            this.btnUpdateUser = new MetroFramework.Controls.MetroButton();
            this.btnAddUser = new MetroFramework.Controls.MetroButton();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.tbDoors = new MetroFramework.Controls.MetroTextBox();
            this.btnCancelOpp = new MetroFramework.Controls.MetroButton();
            this.btnApplyOpp = new MetroFramework.Controls.MetroButton();
            this.tbObservations = new MetroFramework.Controls.MetroTextBox();
            this.tbOpcionals = new MetroFramework.Controls.MetroTextBox();
            this.tbLicense_Plate = new MetroFramework.Controls.MetroTextBox();
            this.tbImageLink = new MetroFramework.Controls.MetroTextBox();
            this.tbKilometers = new MetroFramework.Controls.MetroTextBox();
            this.tbYear = new MetroFramework.Controls.MetroTextBox();
            this.tbPrice = new MetroFramework.Controls.MetroTextBox();
            this.comboColour = new MetroFramework.Controls.MetroComboBox();
            this.comboVersions = new MetroFramework.Controls.MetroComboBox();
            this.comboModel = new MetroFramework.Controls.MetroComboBox();
            this.comboMake = new MetroFramework.Controls.MetroComboBox();
            this.lblDeleteVersion = new MetroFramework.Controls.MetroLabel();
            this.ConfigureFamilies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.metroPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfigureFamilies
            // 
            this.ConfigureFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureFamilies.Controls.Add(this.btnDeleteUser);
            this.ConfigureFamilies.Controls.Add(this.btnUpdateUser);
            this.ConfigureFamilies.Controls.Add(this.btnAddUser);
            this.ConfigureFamilies.Controls.Add(this.metroGrid1);
            this.ConfigureFamilies.Controls.Add(this.metroPanel3);
            this.ConfigureFamilies.HorizontalScrollbarBarColor = true;
            this.ConfigureFamilies.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.HorizontalScrollbarSize = 10;
            this.ConfigureFamilies.Location = new System.Drawing.Point(23, 63);
            this.ConfigureFamilies.Name = "ConfigureFamilies";
            this.ConfigureFamilies.Size = new System.Drawing.Size(1130, 622);
            this.ConfigureFamilies.TabIndex = 11;
            this.ConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureFamilies.VerticalScrollbarBarColor = true;
            this.ConfigureFamilies.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.VerticalScrollbarSize = 10;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(958, 99);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(153, 35);
            this.btnDeleteUser.TabIndex = 14;
            this.btnDeleteUser.Text = "Delete";
            this.btnDeleteUser.UseSelectable = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Location = new System.Drawing.Point(958, 58);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(153, 35);
            this.btnUpdateUser.TabIndex = 13;
            this.btnUpdateUser.Text = "Update";
            this.btnUpdateUser.UseSelectable = true;
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(958, 17);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(153, 35);
            this.btnAddUser.TabIndex = 12;
            this.btnAddUser.Text = "Add";
            this.btnAddUser.UseSelectable = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
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
            this.metroGrid1.Location = new System.Drawing.Point(16, 17);
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
            this.metroGrid1.Size = new System.Drawing.Size(927, 366);
            this.metroGrid1.TabIndex = 11;
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            // 
            // metroPanel3
            // 
            this.metroPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel3.Controls.Add(this.tbDoors);
            this.metroPanel3.Controls.Add(this.btnCancelOpp);
            this.metroPanel3.Controls.Add(this.btnApplyOpp);
            this.metroPanel3.Controls.Add(this.tbObservations);
            this.metroPanel3.Controls.Add(this.tbOpcionals);
            this.metroPanel3.Controls.Add(this.tbLicense_Plate);
            this.metroPanel3.Controls.Add(this.tbImageLink);
            this.metroPanel3.Controls.Add(this.tbKilometers);
            this.metroPanel3.Controls.Add(this.tbYear);
            this.metroPanel3.Controls.Add(this.tbPrice);
            this.metroPanel3.Controls.Add(this.comboColour);
            this.metroPanel3.Controls.Add(this.comboVersions);
            this.metroPanel3.Controls.Add(this.comboModel);
            this.metroPanel3.Controls.Add(this.comboMake);
            this.metroPanel3.Controls.Add(this.lblDeleteVersion);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(16, 389);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(1095, 211);
            this.metroPanel3.TabIndex = 10;
            this.metroPanel3.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // tbDoors
            // 
            // 
            // 
            // 
            this.tbDoors.CustomButton.Image = null;
            this.tbDoors.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbDoors.CustomButton.Name = "";
            this.tbDoors.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbDoors.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDoors.CustomButton.TabIndex = 1;
            this.tbDoors.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDoors.CustomButton.UseSelectable = true;
            this.tbDoors.CustomButton.Visible = false;
            this.tbDoors.Icon = global::UI.Properties.Resources.user__1_;
            this.tbDoors.Lines = new string[0];
            this.tbDoors.Location = new System.Drawing.Point(23, 167);
            this.tbDoors.MaxLength = 32767;
            this.tbDoors.Name = "tbDoors";
            this.tbDoors.PasswordChar = '\0';
            this.tbDoors.PromptText = "Doors";
            this.tbDoors.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDoors.SelectedText = "";
            this.tbDoors.SelectionLength = 0;
            this.tbDoors.SelectionStart = 0;
            this.tbDoors.ShortcutsEnabled = true;
            this.tbDoors.Size = new System.Drawing.Size(260, 23);
            this.tbDoors.TabIndex = 18;
            this.tbDoors.UseSelectable = true;
            this.tbDoors.WaterMark = "Doors";
            this.tbDoors.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDoors.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnCancelOpp
            // 
            this.btnCancelOpp.Location = new System.Drawing.Point(941, 154);
            this.btnCancelOpp.Name = "btnCancelOpp";
            this.btnCancelOpp.Size = new System.Drawing.Size(134, 35);
            this.btnCancelOpp.TabIndex = 17;
            this.btnCancelOpp.Text = "Cancel";
            this.btnCancelOpp.UseSelectable = true;
            this.btnCancelOpp.Click += new System.EventHandler(this.btnCancelOpp_Click);
            // 
            // btnApplyOpp
            // 
            this.btnApplyOpp.Location = new System.Drawing.Point(941, 113);
            this.btnApplyOpp.Name = "btnApplyOpp";
            this.btnApplyOpp.Size = new System.Drawing.Size(134, 35);
            this.btnApplyOpp.TabIndex = 16;
            this.btnApplyOpp.Text = "Apply";
            this.btnApplyOpp.UseSelectable = true;
            this.btnApplyOpp.Click += new System.EventHandler(this.btnApplyOpp_Click);
            // 
            // tbObservations
            // 
            // 
            // 
            // 
            this.tbObservations.CustomButton.Image = null;
            this.tbObservations.CustomButton.Location = new System.Drawing.Point(288, 2);
            this.tbObservations.CustomButton.Name = "";
            this.tbObservations.CustomButton.Size = new System.Drawing.Size(61, 61);
            this.tbObservations.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbObservations.CustomButton.TabIndex = 1;
            this.tbObservations.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbObservations.CustomButton.UseSelectable = true;
            this.tbObservations.CustomButton.Visible = false;
            this.tbObservations.Icon = global::UI.Properties.Resources.user__1_;
            this.tbObservations.Lines = new string[0];
            this.tbObservations.Location = new System.Drawing.Point(574, 124);
            this.tbObservations.MaxLength = 32767;
            this.tbObservations.Multiline = true;
            this.tbObservations.Name = "tbObservations";
            this.tbObservations.PasswordChar = '\0';
            this.tbObservations.PromptText = "Observations";
            this.tbObservations.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbObservations.SelectedText = "";
            this.tbObservations.SelectionLength = 0;
            this.tbObservations.SelectionStart = 0;
            this.tbObservations.ShortcutsEnabled = true;
            this.tbObservations.Size = new System.Drawing.Size(352, 66);
            this.tbObservations.TabIndex = 15;
            this.tbObservations.UseSelectable = true;
            this.tbObservations.WaterMark = "Observations";
            this.tbObservations.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbObservations.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbOpcionals
            // 
            // 
            // 
            // 
            this.tbOpcionals.CustomButton.Image = null;
            this.tbOpcionals.CustomButton.Location = new System.Drawing.Point(288, 2);
            this.tbOpcionals.CustomButton.Name = "";
            this.tbOpcionals.CustomButton.Size = new System.Drawing.Size(61, 61);
            this.tbOpcionals.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbOpcionals.CustomButton.TabIndex = 1;
            this.tbOpcionals.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbOpcionals.CustomButton.UseSelectable = true;
            this.tbOpcionals.CustomButton.Visible = false;
            this.tbOpcionals.Icon = global::UI.Properties.Resources.user__1_;
            this.tbOpcionals.Lines = new string[0];
            this.tbOpcionals.Location = new System.Drawing.Point(574, 52);
            this.tbOpcionals.MaxLength = 32767;
            this.tbOpcionals.Multiline = true;
            this.tbOpcionals.Name = "tbOpcionals";
            this.tbOpcionals.PasswordChar = '\0';
            this.tbOpcionals.PromptText = "Options";
            this.tbOpcionals.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbOpcionals.SelectedText = "";
            this.tbOpcionals.SelectionLength = 0;
            this.tbOpcionals.SelectionStart = 0;
            this.tbOpcionals.ShortcutsEnabled = true;
            this.tbOpcionals.Size = new System.Drawing.Size(352, 66);
            this.tbOpcionals.TabIndex = 14;
            this.tbOpcionals.UseSelectable = true;
            this.tbOpcionals.WaterMark = "Options";
            this.tbOpcionals.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbOpcionals.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbLicense_Plate
            // 
            // 
            // 
            // 
            this.tbLicense_Plate.CustomButton.Image = null;
            this.tbLicense_Plate.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbLicense_Plate.CustomButton.Name = "";
            this.tbLicense_Plate.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbLicense_Plate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbLicense_Plate.CustomButton.TabIndex = 1;
            this.tbLicense_Plate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbLicense_Plate.CustomButton.UseSelectable = true;
            this.tbLicense_Plate.CustomButton.Visible = false;
            this.tbLicense_Plate.Icon = global::UI.Properties.Resources.user__1_;
            this.tbLicense_Plate.Lines = new string[0];
            this.tbLicense_Plate.Location = new System.Drawing.Point(308, 52);
            this.tbLicense_Plate.MaxLength = 32767;
            this.tbLicense_Plate.Name = "tbLicense_Plate";
            this.tbLicense_Plate.PasswordChar = '\0';
            this.tbLicense_Plate.PromptText = "License Plate";
            this.tbLicense_Plate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbLicense_Plate.SelectedText = "";
            this.tbLicense_Plate.SelectionLength = 0;
            this.tbLicense_Plate.SelectionStart = 0;
            this.tbLicense_Plate.ShortcutsEnabled = true;
            this.tbLicense_Plate.Size = new System.Drawing.Size(260, 23);
            this.tbLicense_Plate.TabIndex = 9;
            this.tbLicense_Plate.UseSelectable = true;
            this.tbLicense_Plate.WaterMark = "License Plate";
            this.tbLicense_Plate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbLicense_Plate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbImageLink
            // 
            // 
            // 
            // 
            this.tbImageLink.CustomButton.Image = null;
            this.tbImageLink.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbImageLink.CustomButton.Name = "";
            this.tbImageLink.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbImageLink.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbImageLink.CustomButton.TabIndex = 1;
            this.tbImageLink.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbImageLink.CustomButton.UseSelectable = true;
            this.tbImageLink.CustomButton.Visible = false;
            this.tbImageLink.Icon = global::UI.Properties.Resources.user__1_;
            this.tbImageLink.Lines = new string[0];
            this.tbImageLink.Location = new System.Drawing.Point(308, 167);
            this.tbImageLink.MaxLength = 32767;
            this.tbImageLink.Name = "tbImageLink";
            this.tbImageLink.PasswordChar = '\0';
            this.tbImageLink.PromptText = "Image Link";
            this.tbImageLink.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbImageLink.SelectedText = "";
            this.tbImageLink.SelectionLength = 0;
            this.tbImageLink.SelectionStart = 0;
            this.tbImageLink.ShortcutsEnabled = true;
            this.tbImageLink.Size = new System.Drawing.Size(260, 23);
            this.tbImageLink.TabIndex = 13;
            this.tbImageLink.UseSelectable = true;
            this.tbImageLink.WaterMark = "Image Link";
            this.tbImageLink.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbImageLink.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbKilometers
            // 
            // 
            // 
            // 
            this.tbKilometers.CustomButton.Image = null;
            this.tbKilometers.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbKilometers.CustomButton.Name = "";
            this.tbKilometers.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbKilometers.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbKilometers.CustomButton.TabIndex = 1;
            this.tbKilometers.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbKilometers.CustomButton.UseSelectable = true;
            this.tbKilometers.CustomButton.Visible = false;
            this.tbKilometers.Icon = global::UI.Properties.Resources.user__1_;
            this.tbKilometers.Lines = new string[0];
            this.tbKilometers.Location = new System.Drawing.Point(308, 110);
            this.tbKilometers.MaxLength = 32767;
            this.tbKilometers.Name = "tbKilometers";
            this.tbKilometers.PasswordChar = '\0';
            this.tbKilometers.PromptText = "Kilometers";
            this.tbKilometers.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbKilometers.SelectedText = "";
            this.tbKilometers.SelectionLength = 0;
            this.tbKilometers.SelectionStart = 0;
            this.tbKilometers.ShortcutsEnabled = true;
            this.tbKilometers.Size = new System.Drawing.Size(260, 23);
            this.tbKilometers.TabIndex = 12;
            this.tbKilometers.UseSelectable = true;
            this.tbKilometers.WaterMark = "Kilometers";
            this.tbKilometers.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbKilometers.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbYear
            // 
            // 
            // 
            // 
            this.tbYear.CustomButton.Image = null;
            this.tbYear.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbYear.CustomButton.Name = "";
            this.tbYear.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbYear.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbYear.CustomButton.TabIndex = 1;
            this.tbYear.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbYear.CustomButton.UseSelectable = true;
            this.tbYear.CustomButton.Visible = false;
            this.tbYear.Icon = global::UI.Properties.Resources.user__1_;
            this.tbYear.Lines = new string[0];
            this.tbYear.Location = new System.Drawing.Point(308, 81);
            this.tbYear.MaxLength = 32767;
            this.tbYear.Name = "tbYear";
            this.tbYear.PasswordChar = '\0';
            this.tbYear.PromptText = "Year";
            this.tbYear.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbYear.SelectedText = "";
            this.tbYear.SelectionLength = 0;
            this.tbYear.SelectionStart = 0;
            this.tbYear.ShortcutsEnabled = true;
            this.tbYear.Size = new System.Drawing.Size(260, 23);
            this.tbYear.TabIndex = 11;
            this.tbYear.UseSelectable = true;
            this.tbYear.WaterMark = "Year";
            this.tbYear.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbYear.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbPrice
            // 
            // 
            // 
            // 
            this.tbPrice.CustomButton.Image = null;
            this.tbPrice.CustomButton.Location = new System.Drawing.Point(238, 1);
            this.tbPrice.CustomButton.Name = "";
            this.tbPrice.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbPrice.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPrice.CustomButton.TabIndex = 1;
            this.tbPrice.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPrice.CustomButton.UseSelectable = true;
            this.tbPrice.CustomButton.Visible = false;
            this.tbPrice.Icon = global::UI.Properties.Resources.user__1_;
            this.tbPrice.Lines = new string[0];
            this.tbPrice.Location = new System.Drawing.Point(308, 139);
            this.tbPrice.MaxLength = 32767;
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.PasswordChar = '\0';
            this.tbPrice.PromptText = "Price";
            this.tbPrice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPrice.SelectedText = "";
            this.tbPrice.SelectionLength = 0;
            this.tbPrice.SelectionStart = 0;
            this.tbPrice.ShortcutsEnabled = true;
            this.tbPrice.Size = new System.Drawing.Size(260, 23);
            this.tbPrice.TabIndex = 10;
            this.tbPrice.UseSelectable = true;
            this.tbPrice.WaterMark = "Price";
            this.tbPrice.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPrice.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // comboColour
            // 
            this.comboColour.FormattingEnabled = true;
            this.comboColour.ItemHeight = 23;
            this.comboColour.Location = new System.Drawing.Point(23, 139);
            this.comboColour.Name = "comboColour";
            this.comboColour.Size = new System.Drawing.Size(259, 29);
            this.comboColour.TabIndex = 8;
            this.comboColour.UseSelectable = true;
            // 
            // comboVersions
            // 
            this.comboVersions.FormattingEnabled = true;
            this.comboVersions.ItemHeight = 23;
            this.comboVersions.Location = new System.Drawing.Point(23, 110);
            this.comboVersions.Name = "comboVersions";
            this.comboVersions.Size = new System.Drawing.Size(259, 29);
            this.comboVersions.TabIndex = 7;
            this.comboVersions.UseSelectable = true;
            // 
            // comboModel
            // 
            this.comboModel.FormattingEnabled = true;
            this.comboModel.ItemHeight = 23;
            this.comboModel.Location = new System.Drawing.Point(23, 81);
            this.comboModel.Name = "comboModel";
            this.comboModel.Size = new System.Drawing.Size(259, 29);
            this.comboModel.TabIndex = 6;
            this.comboModel.UseSelectable = true;
            this.comboModel.SelectedIndexChanged += new System.EventHandler(this.comboDeleteVersionModel_SelectedIndexChanged);
            // 
            // comboMake
            // 
            this.comboMake.FormattingEnabled = true;
            this.comboMake.ItemHeight = 23;
            this.comboMake.Location = new System.Drawing.Point(23, 52);
            this.comboMake.Name = "comboMake";
            this.comboMake.Size = new System.Drawing.Size(259, 29);
            this.comboMake.TabIndex = 5;
            this.comboMake.UseSelectable = true;
            this.comboMake.SelectedIndexChanged += new System.EventHandler(this.comboDeleteVersionMake_SelectedIndexChanged);
            // 
            // lblDeleteVersion
            // 
            this.lblDeleteVersion.AutoSize = true;
            this.lblDeleteVersion.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDeleteVersion.Location = new System.Drawing.Point(23, 14);
            this.lblDeleteVersion.Name = "lblDeleteVersion";
            this.lblDeleteVersion.Size = new System.Drawing.Size(65, 25);
            this.lblDeleteVersion.TabIndex = 0;
            this.lblDeleteVersion.Text = "Vehicle";
            this.lblDeleteVersion.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // VehicleManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 706);
            this.Controls.Add(this.ConfigureFamilies);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VehicleManagementForm";
            this.Text = "Vehicle Management";
            this.ConfigureFamilies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel ConfigureFamilies;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroComboBox comboModel;
        private MetroFramework.Controls.MetroComboBox comboMake;
        private MetroFramework.Controls.MetroLabel lblDeleteVersion;
        private MetroFramework.Controls.MetroComboBox comboVersions;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroButton btnDeleteUser;
        private MetroFramework.Controls.MetroButton btnUpdateUser;
        private MetroFramework.Controls.MetroButton btnAddUser;
        private MetroFramework.Controls.MetroComboBox comboColour;
        private MetroFramework.Controls.MetroTextBox tbLicense_Plate;
        private MetroFramework.Controls.MetroTextBox tbImageLink;
        private MetroFramework.Controls.MetroTextBox tbKilometers;
        private MetroFramework.Controls.MetroTextBox tbYear;
        private MetroFramework.Controls.MetroTextBox tbPrice;
        private MetroFramework.Controls.MetroTextBox tbObservations;
        private MetroFramework.Controls.MetroTextBox tbOpcionals;
        private MetroFramework.Controls.MetroButton btnCancelOpp;
        private MetroFramework.Controls.MetroButton btnApplyOpp;
        private MetroFramework.Controls.MetroTextBox tbDoors;
    }
}