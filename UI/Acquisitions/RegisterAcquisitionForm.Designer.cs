namespace UI.Vehicles
{
    partial class RegisterAcquisitionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterAcquisitionForm));
            this.panelAcquisition = new MetroFramework.Controls.MetroPanel();
            this.VehicleDetailsPanel = new MetroFramework.Controls.MetroPanel();
            this.lblVehicleToAppraise = new MetroFramework.Controls.MetroLabel();
            this.lblMake = new MetroFramework.Controls.MetroLabel();
            this.comboMake = new MetroFramework.Controls.MetroComboBox();
            this.lblModel = new MetroFramework.Controls.MetroLabel();
            this.comboModel = new MetroFramework.Controls.MetroComboBox();
            this.lblVersion = new MetroFramework.Controls.MetroLabel();
            this.comboVersions = new MetroFramework.Controls.MetroComboBox();
            this.lblColour = new MetroFramework.Controls.MetroLabel();
            this.comboColour = new MetroFramework.Controls.MetroComboBox();
            this.tbLicense_Plate = new MetroFramework.Controls.MetroTextBox();
            this.tbChassis_Number = new MetroFramework.Controls.MetroTextBox();
            this.tbEngine_Number = new MetroFramework.Controls.MetroTextBox();
            this.tbYear = new MetroFramework.Controls.MetroTextBox();
            this.tbDoors = new MetroFramework.Controls.MetroTextBox();
            this.tbDeclared_Kilometers = new MetroFramework.Controls.MetroTextBox();
            this.InspectionPanel = new MetroFramework.Controls.MetroPanel();
            this.lblInspection = new MetroFramework.Controls.MetroLabel();
            this.lblInspectionDate = new MetroFramework.Controls.MetroLabel();
            this.dtInspection_Date = new MetroFramework.Controls.MetroDateTime();
            this.lblInspectionHour = new MetroFramework.Controls.MetroLabel();
            this.comboHour = new MetroFramework.Controls.MetroComboBox();
            this.lblMechanic = new MetroFramework.Controls.MetroLabel();
            this.comboMechanic = new MetroFramework.Controls.MetroComboBox();
            this.clientView1 = new UI.Clients.Controls.ClientView();
            this.btnRegisterAcquisition = new MetroFramework.Controls.MetroButton();
            this.panelAcquisition.SuspendLayout();
            this.VehicleDetailsPanel.SuspendLayout();
            this.InspectionPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // panelAcquisition
            //
            this.panelAcquisition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAcquisition.Controls.Add(this.InspectionPanel);
            this.panelAcquisition.Controls.Add(this.VehicleDetailsPanel);
            this.panelAcquisition.Controls.Add(this.clientView1);
            this.panelAcquisition.Controls.Add(this.btnRegisterAcquisition);
            this.panelAcquisition.HorizontalScrollbarBarColor = true;
            this.panelAcquisition.HorizontalScrollbarHighlightOnWheel = false;
            this.panelAcquisition.HorizontalScrollbarSize = 10;
            this.panelAcquisition.Location = new System.Drawing.Point(20, 60);
            this.panelAcquisition.Name = "panelAcquisition";
            this.panelAcquisition.Size = new System.Drawing.Size(780, 480);
            this.panelAcquisition.TabIndex = 0;
            this.panelAcquisition.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelAcquisition.VerticalScrollbarBarColor = true;
            this.panelAcquisition.VerticalScrollbarHighlightOnWheel = false;
            this.panelAcquisition.VerticalScrollbarSize = 10;
            //
            // VehicleDetailsPanel
            //
            this.VehicleDetailsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VehicleDetailsPanel.Controls.Add(this.lblVehicleToAppraise);
            this.VehicleDetailsPanel.Controls.Add(this.lblMake);
            this.VehicleDetailsPanel.Controls.Add(this.comboMake);
            this.VehicleDetailsPanel.Controls.Add(this.lblModel);
            this.VehicleDetailsPanel.Controls.Add(this.comboModel);
            this.VehicleDetailsPanel.Controls.Add(this.lblVersion);
            this.VehicleDetailsPanel.Controls.Add(this.comboVersions);
            this.VehicleDetailsPanel.Controls.Add(this.lblColour);
            this.VehicleDetailsPanel.Controls.Add(this.comboColour);
            this.VehicleDetailsPanel.Controls.Add(this.tbLicense_Plate);
            this.VehicleDetailsPanel.Controls.Add(this.tbChassis_Number);
            this.VehicleDetailsPanel.Controls.Add(this.tbEngine_Number);
            this.VehicleDetailsPanel.Controls.Add(this.tbYear);
            this.VehicleDetailsPanel.Controls.Add(this.tbDoors);
            this.VehicleDetailsPanel.Controls.Add(this.tbDeclared_Kilometers);
            this.VehicleDetailsPanel.Enabled = false;
            this.VehicleDetailsPanel.HorizontalScrollbarBarColor = true;
            this.VehicleDetailsPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.VehicleDetailsPanel.HorizontalScrollbarSize = 10;
            this.VehicleDetailsPanel.Location = new System.Drawing.Point(18, 150);
            this.VehicleDetailsPanel.Name = "VehicleDetailsPanel";
            this.VehicleDetailsPanel.Size = new System.Drawing.Size(742, 185);
            this.VehicleDetailsPanel.TabIndex = 1;
            this.VehicleDetailsPanel.VerticalScrollbarBarColor = true;
            this.VehicleDetailsPanel.VerticalScrollbarHighlightOnWheel = false;
            this.VehicleDetailsPanel.VerticalScrollbarSize = 10;
            //
            // lblVehicleToAppraise
            //
            this.lblVehicleToAppraise.AutoSize = true;
            this.lblVehicleToAppraise.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblVehicleToAppraise.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblVehicleToAppraise.Location = new System.Drawing.Point(10, 6);
            this.lblVehicleToAppraise.Name = "lblVehicleToAppraise";
            this.lblVehicleToAppraise.Size = new System.Drawing.Size(145, 25);
            this.lblVehicleToAppraise.TabIndex = 0;
            this.lblVehicleToAppraise.Tag = "lblVehicleToAppraise";
            this.lblVehicleToAppraise.Text = "Vehicle to Appraise";
            this.lblVehicleToAppraise.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblMake
            //
            this.lblMake.AutoSize = true;
            this.lblMake.Location = new System.Drawing.Point(10, 45);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(44, 19);
            this.lblMake.TabIndex = 1;
            this.lblMake.Tag = "lblMake";
            this.lblMake.Text = "Make:";
            this.lblMake.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboMake
            //
            this.comboMake.FormattingEnabled = true;
            this.comboMake.ItemHeight = 23;
            this.comboMake.Location = new System.Drawing.Point(85, 40);
            this.comboMake.Name = "comboMake";
            this.comboMake.Size = new System.Drawing.Size(220, 29);
            this.comboMake.TabIndex = 2;
            this.comboMake.UseSelectable = true;
            this.comboMake.SelectedIndexChanged += new System.EventHandler(this.comboMake_SelectedIndexChanged);
            //
            // lblModel
            //
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(10, 80);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(50, 19);
            this.lblModel.TabIndex = 3;
            this.lblModel.Tag = "lblModel";
            this.lblModel.Text = "Model:";
            this.lblModel.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboModel
            //
            this.comboModel.FormattingEnabled = true;
            this.comboModel.ItemHeight = 23;
            this.comboModel.Location = new System.Drawing.Point(85, 75);
            this.comboModel.Name = "comboModel";
            this.comboModel.Size = new System.Drawing.Size(220, 29);
            this.comboModel.TabIndex = 4;
            this.comboModel.UseSelectable = true;
            this.comboModel.SelectedIndexChanged += new System.EventHandler(this.comboModel_SelectedIndexChanged);
            //
            // lblVersion
            //
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(10, 115);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(54, 19);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Tag = "lblVersion";
            this.lblVersion.Text = "Version:";
            this.lblVersion.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboVersions
            //
            this.comboVersions.FormattingEnabled = true;
            this.comboVersions.ItemHeight = 23;
            this.comboVersions.Location = new System.Drawing.Point(85, 110);
            this.comboVersions.Name = "comboVersions";
            this.comboVersions.Size = new System.Drawing.Size(220, 29);
            this.comboVersions.TabIndex = 6;
            this.comboVersions.UseSelectable = true;
            //
            // lblColour
            //
            this.lblColour.AutoSize = true;
            this.lblColour.Location = new System.Drawing.Point(10, 150);
            this.lblColour.Name = "lblColour";
            this.lblColour.Size = new System.Drawing.Size(53, 19);
            this.lblColour.TabIndex = 7;
            this.lblColour.Tag = "lblColour";
            this.lblColour.Text = "Colour:";
            this.lblColour.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboColour
            //
            this.comboColour.FormattingEnabled = true;
            this.comboColour.ItemHeight = 23;
            this.comboColour.Location = new System.Drawing.Point(85, 145);
            this.comboColour.Name = "comboColour";
            this.comboColour.Size = new System.Drawing.Size(220, 29);
            this.comboColour.TabIndex = 8;
            this.comboColour.UseSelectable = true;
            //
            // tbLicense_Plate
            //
            //
            //
            //
            this.tbLicense_Plate.CustomButton.Image = null;
            this.tbLicense_Plate.CustomButton.Location = new System.Drawing.Point(168, 1);
            this.tbLicense_Plate.CustomButton.Name = "";
            this.tbLicense_Plate.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbLicense_Plate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbLicense_Plate.CustomButton.TabIndex = 1;
            this.tbLicense_Plate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbLicense_Plate.CustomButton.UseSelectable = true;
            this.tbLicense_Plate.CustomButton.Visible = false;
            this.tbLicense_Plate.Icon = global::UI.Properties.Resources.user__1_;
            this.tbLicense_Plate.Lines = new string[0];
            this.tbLicense_Plate.Location = new System.Drawing.Point(330, 43);
            this.tbLicense_Plate.MaxLength = 10;
            this.tbLicense_Plate.Name = "tbLicense_Plate";
            this.tbLicense_Plate.PasswordChar = '\0';
            this.tbLicense_Plate.PromptText = "License Plate";
            this.tbLicense_Plate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbLicense_Plate.SelectedText = "";
            this.tbLicense_Plate.SelectionLength = 0;
            this.tbLicense_Plate.SelectionStart = 0;
            this.tbLicense_Plate.ShortcutsEnabled = true;
            this.tbLicense_Plate.Size = new System.Drawing.Size(190, 23);
            this.tbLicense_Plate.TabIndex = 9;
            this.tbLicense_Plate.Tag = "tbLicense_Plate";
            this.tbLicense_Plate.UseSelectable = true;
            this.tbLicense_Plate.WaterMark = "License Plate";
            this.tbLicense_Plate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbLicense_Plate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbChassis_Number
            //
            //
            //
            //
            this.tbChassis_Number.CustomButton.Image = null;
            this.tbChassis_Number.CustomButton.Location = new System.Drawing.Point(168, 1);
            this.tbChassis_Number.CustomButton.Name = "";
            this.tbChassis_Number.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbChassis_Number.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbChassis_Number.CustomButton.TabIndex = 1;
            this.tbChassis_Number.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbChassis_Number.CustomButton.UseSelectable = true;
            this.tbChassis_Number.CustomButton.Visible = false;
            this.tbChassis_Number.Icon = global::UI.Properties.Resources.user__1_;
            this.tbChassis_Number.Lines = new string[0];
            this.tbChassis_Number.Location = new System.Drawing.Point(330, 78);
            this.tbChassis_Number.MaxLength = 17;
            this.tbChassis_Number.Name = "tbChassis_Number";
            this.tbChassis_Number.PasswordChar = '\0';
            this.tbChassis_Number.PromptText = "Chassis Number";
            this.tbChassis_Number.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbChassis_Number.SelectedText = "";
            this.tbChassis_Number.SelectionLength = 0;
            this.tbChassis_Number.SelectionStart = 0;
            this.tbChassis_Number.ShortcutsEnabled = true;
            this.tbChassis_Number.Size = new System.Drawing.Size(190, 23);
            this.tbChassis_Number.TabIndex = 10;
            this.tbChassis_Number.Tag = "tbChassis_Number";
            this.tbChassis_Number.UseSelectable = true;
            this.tbChassis_Number.WaterMark = "Chassis Number";
            this.tbChassis_Number.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbChassis_Number.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbEngine_Number
            //
            //
            //
            //
            this.tbEngine_Number.CustomButton.Image = null;
            this.tbEngine_Number.CustomButton.Location = new System.Drawing.Point(168, 1);
            this.tbEngine_Number.CustomButton.Name = "";
            this.tbEngine_Number.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbEngine_Number.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbEngine_Number.CustomButton.TabIndex = 1;
            this.tbEngine_Number.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbEngine_Number.CustomButton.UseSelectable = true;
            this.tbEngine_Number.CustomButton.Visible = false;
            this.tbEngine_Number.Icon = global::UI.Properties.Resources.user__1_;
            this.tbEngine_Number.Lines = new string[0];
            this.tbEngine_Number.Location = new System.Drawing.Point(330, 113);
            this.tbEngine_Number.MaxLength = 20;
            this.tbEngine_Number.Name = "tbEngine_Number";
            this.tbEngine_Number.PasswordChar = '\0';
            this.tbEngine_Number.PromptText = "Engine Number";
            this.tbEngine_Number.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbEngine_Number.SelectedText = "";
            this.tbEngine_Number.SelectionLength = 0;
            this.tbEngine_Number.SelectionStart = 0;
            this.tbEngine_Number.ShortcutsEnabled = true;
            this.tbEngine_Number.Size = new System.Drawing.Size(190, 23);
            this.tbEngine_Number.TabIndex = 11;
            this.tbEngine_Number.Tag = "tbEngine_Number";
            this.tbEngine_Number.UseSelectable = true;
            this.tbEngine_Number.WaterMark = "Engine Number";
            this.tbEngine_Number.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbEngine_Number.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbYear
            //
            //
            //
            //
            this.tbYear.CustomButton.Image = null;
            this.tbYear.CustomButton.Location = new System.Drawing.Point(168, 1);
            this.tbYear.CustomButton.Name = "";
            this.tbYear.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbYear.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbYear.CustomButton.TabIndex = 1;
            this.tbYear.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbYear.CustomButton.UseSelectable = true;
            this.tbYear.CustomButton.Visible = false;
            this.tbYear.Icon = global::UI.Properties.Resources.user__1_;
            this.tbYear.Lines = new string[0];
            this.tbYear.Location = new System.Drawing.Point(535, 43);
            this.tbYear.MaxLength = 4;
            this.tbYear.Name = "tbYear";
            this.tbYear.PasswordChar = '\0';
            this.tbYear.PromptText = "Year";
            this.tbYear.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbYear.SelectedText = "";
            this.tbYear.SelectionLength = 0;
            this.tbYear.SelectionStart = 0;
            this.tbYear.ShortcutsEnabled = true;
            this.tbYear.Size = new System.Drawing.Size(190, 23);
            this.tbYear.TabIndex = 12;
            this.tbYear.Tag = "tbYear";
            this.tbYear.UseSelectable = true;
            this.tbYear.WaterMark = "Year";
            this.tbYear.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbYear.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbDoors
            //
            //
            //
            //
            this.tbDoors.CustomButton.Image = null;
            this.tbDoors.CustomButton.Location = new System.Drawing.Point(168, 1);
            this.tbDoors.CustomButton.Name = "";
            this.tbDoors.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbDoors.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDoors.CustomButton.TabIndex = 1;
            this.tbDoors.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDoors.CustomButton.UseSelectable = true;
            this.tbDoors.CustomButton.Visible = false;
            this.tbDoors.Icon = global::UI.Properties.Resources.user__1_;
            this.tbDoors.Lines = new string[0];
            this.tbDoors.Location = new System.Drawing.Point(535, 78);
            this.tbDoors.MaxLength = 1;
            this.tbDoors.Name = "tbDoors";
            this.tbDoors.PasswordChar = '\0';
            this.tbDoors.PromptText = "Doors";
            this.tbDoors.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDoors.SelectedText = "";
            this.tbDoors.SelectionLength = 0;
            this.tbDoors.SelectionStart = 0;
            this.tbDoors.ShortcutsEnabled = true;
            this.tbDoors.Size = new System.Drawing.Size(190, 23);
            this.tbDoors.TabIndex = 13;
            this.tbDoors.Tag = "tbDoors";
            this.tbDoors.UseSelectable = true;
            this.tbDoors.WaterMark = "Doors";
            this.tbDoors.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDoors.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // tbDeclared_Kilometers
            //
            //
            //
            //
            this.tbDeclared_Kilometers.CustomButton.Image = null;
            this.tbDeclared_Kilometers.CustomButton.Location = new System.Drawing.Point(168, 1);
            this.tbDeclared_Kilometers.CustomButton.Name = "";
            this.tbDeclared_Kilometers.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbDeclared_Kilometers.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDeclared_Kilometers.CustomButton.TabIndex = 1;
            this.tbDeclared_Kilometers.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDeclared_Kilometers.CustomButton.UseSelectable = true;
            this.tbDeclared_Kilometers.CustomButton.Visible = false;
            this.tbDeclared_Kilometers.Icon = global::UI.Properties.Resources.user__1_;
            this.tbDeclared_Kilometers.Lines = new string[0];
            this.tbDeclared_Kilometers.Location = new System.Drawing.Point(535, 113);
            this.tbDeclared_Kilometers.MaxLength = 7;
            this.tbDeclared_Kilometers.Name = "tbDeclared_Kilometers";
            this.tbDeclared_Kilometers.PasswordChar = '\0';
            this.tbDeclared_Kilometers.PromptText = "Declared Kilometers";
            this.tbDeclared_Kilometers.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDeclared_Kilometers.SelectedText = "";
            this.tbDeclared_Kilometers.SelectionLength = 0;
            this.tbDeclared_Kilometers.SelectionStart = 0;
            this.tbDeclared_Kilometers.ShortcutsEnabled = true;
            this.tbDeclared_Kilometers.Size = new System.Drawing.Size(190, 23);
            this.tbDeclared_Kilometers.TabIndex = 14;
            this.tbDeclared_Kilometers.Tag = "tbDeclared_Kilometers";
            this.tbDeclared_Kilometers.UseSelectable = true;
            this.tbDeclared_Kilometers.WaterMark = "Declared Kilometers";
            this.tbDeclared_Kilometers.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDeclared_Kilometers.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // InspectionPanel
            //
            this.InspectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InspectionPanel.Controls.Add(this.lblInspection);
            this.InspectionPanel.Controls.Add(this.lblInspectionDate);
            this.InspectionPanel.Controls.Add(this.dtInspection_Date);
            this.InspectionPanel.Controls.Add(this.lblInspectionHour);
            this.InspectionPanel.Controls.Add(this.comboHour);
            this.InspectionPanel.Controls.Add(this.lblMechanic);
            this.InspectionPanel.Controls.Add(this.comboMechanic);
            this.InspectionPanel.Enabled = false;
            this.InspectionPanel.HorizontalScrollbarBarColor = true;
            this.InspectionPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.InspectionPanel.HorizontalScrollbarSize = 10;
            this.InspectionPanel.Location = new System.Drawing.Point(18, 343);
            this.InspectionPanel.Name = "InspectionPanel";
            this.InspectionPanel.Size = new System.Drawing.Size(742, 78);
            this.InspectionPanel.TabIndex = 2;
            this.InspectionPanel.VerticalScrollbarBarColor = true;
            this.InspectionPanel.VerticalScrollbarHighlightOnWheel = false;
            this.InspectionPanel.VerticalScrollbarSize = 10;
            //
            // lblInspection
            //
            this.lblInspection.AutoSize = true;
            this.lblInspection.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblInspection.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblInspection.Location = new System.Drawing.Point(10, 6);
            this.lblInspection.Name = "lblInspection";
            this.lblInspection.Size = new System.Drawing.Size(159, 25);
            this.lblInspection.TabIndex = 0;
            this.lblInspection.Tag = "lblInspection";
            this.lblInspection.Text = "Mechanical Inspection";
            this.lblInspection.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // lblInspectionDate
            //
            this.lblInspectionDate.AutoSize = true;
            this.lblInspectionDate.Location = new System.Drawing.Point(10, 45);
            this.lblInspectionDate.Name = "lblInspectionDate";
            this.lblInspectionDate.Size = new System.Drawing.Size(39, 19);
            this.lblInspectionDate.TabIndex = 1;
            this.lblInspectionDate.Tag = "lblInspectionDate";
            this.lblInspectionDate.Text = "Date:";
            this.lblInspectionDate.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // dtInspection_Date
            //
            this.dtInspection_Date.Location = new System.Drawing.Point(60, 40);
            this.dtInspection_Date.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtInspection_Date.Name = "dtInspection_Date";
            this.dtInspection_Date.Size = new System.Drawing.Size(191, 29);
            this.dtInspection_Date.TabIndex = 2;
            //
            // lblInspectionHour
            //
            this.lblInspectionHour.AutoSize = true;
            this.lblInspectionHour.Location = new System.Drawing.Point(270, 45);
            this.lblInspectionHour.Name = "lblInspectionHour";
            this.lblInspectionHour.Size = new System.Drawing.Size(41, 19);
            this.lblInspectionHour.TabIndex = 3;
            this.lblInspectionHour.Tag = "lblInspectionHour";
            this.lblInspectionHour.Text = "Time:";
            this.lblInspectionHour.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboHour
            //
            this.comboHour.FormattingEnabled = true;
            this.comboHour.ItemHeight = 23;
            this.comboHour.Location = new System.Drawing.Point(320, 40);
            this.comboHour.Name = "comboHour";
            this.comboHour.Size = new System.Drawing.Size(90, 29);
            this.comboHour.TabIndex = 4;
            this.comboHour.UseSelectable = true;
            //
            // lblMechanic
            //
            this.lblMechanic.AutoSize = true;
            this.lblMechanic.Location = new System.Drawing.Point(430, 45);
            this.lblMechanic.Name = "lblMechanic";
            this.lblMechanic.Size = new System.Drawing.Size(66, 19);
            this.lblMechanic.TabIndex = 5;
            this.lblMechanic.Tag = "lblMechanic";
            this.lblMechanic.Text = "Mechanic:";
            this.lblMechanic.Theme = MetroFramework.MetroThemeStyle.Light;
            //
            // comboMechanic
            //
            this.comboMechanic.FormattingEnabled = true;
            this.comboMechanic.ItemHeight = 23;
            this.comboMechanic.Location = new System.Drawing.Point(505, 40);
            this.comboMechanic.Name = "comboMechanic";
            this.comboMechanic.Size = new System.Drawing.Size(220, 29);
            this.comboMechanic.TabIndex = 6;
            this.comboMechanic.UseSelectable = true;
            //
            // clientView1
            //
            this.clientView1.Location = new System.Drawing.Point(18, 10);
            this.clientView1.Name = "clientView1";
            this.clientView1.Size = new System.Drawing.Size(363, 132);
            this.clientView1.TabIndex = 0;
            //
            // btnRegisterAcquisition
            //
            this.btnRegisterAcquisition.Enabled = false;
            this.btnRegisterAcquisition.Location = new System.Drawing.Point(591, 429);
            this.btnRegisterAcquisition.Name = "btnRegisterAcquisition";
            this.btnRegisterAcquisition.Size = new System.Drawing.Size(169, 35);
            this.btnRegisterAcquisition.TabIndex = 3;
            this.btnRegisterAcquisition.Tag = "btnRegisterAcquisition";
            this.btnRegisterAcquisition.Text = "Register Vehicle";
            this.btnRegisterAcquisition.UseSelectable = true;
            this.btnRegisterAcquisition.Click += new System.EventHandler(this.btnRegisterAcquisition_Click);
            //
            // RegisterAcquisitionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 560);
            this.MaximizeBox = false;
            this.Controls.Add(this.panelAcquisition);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterAcquisitionForm";
            this.Resizable = false;
            this.Tag = "RegisterAcquisitionForm";
            this.Text = "Register Vehicle for Appraisal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterAcquisitionForm_FormClosing);
            this.panelAcquisition.ResumeLayout(false);
            this.VehicleDetailsPanel.ResumeLayout(false);
            this.VehicleDetailsPanel.PerformLayout();
            this.InspectionPanel.ResumeLayout(false);
            this.InspectionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel panelAcquisition;
        private MetroFramework.Controls.MetroPanel VehicleDetailsPanel;
        private MetroFramework.Controls.MetroLabel lblVehicleToAppraise;
        private MetroFramework.Controls.MetroLabel lblMake;
        private MetroFramework.Controls.MetroComboBox comboMake;
        private MetroFramework.Controls.MetroLabel lblModel;
        private MetroFramework.Controls.MetroComboBox comboModel;
        private MetroFramework.Controls.MetroLabel lblVersion;
        private MetroFramework.Controls.MetroComboBox comboVersions;
        private MetroFramework.Controls.MetroLabel lblColour;
        private MetroFramework.Controls.MetroComboBox comboColour;
        private MetroFramework.Controls.MetroTextBox tbLicense_Plate;
        private MetroFramework.Controls.MetroTextBox tbChassis_Number;
        private MetroFramework.Controls.MetroTextBox tbEngine_Number;
        private MetroFramework.Controls.MetroTextBox tbYear;
        private MetroFramework.Controls.MetroTextBox tbDoors;
        private MetroFramework.Controls.MetroTextBox tbDeclared_Kilometers;
        private MetroFramework.Controls.MetroPanel InspectionPanel;
        private MetroFramework.Controls.MetroLabel lblInspection;
        private MetroFramework.Controls.MetroLabel lblInspectionDate;
        private MetroFramework.Controls.MetroDateTime dtInspection_Date;
        private MetroFramework.Controls.MetroLabel lblInspectionHour;
        private MetroFramework.Controls.MetroComboBox comboHour;
        private MetroFramework.Controls.MetroLabel lblMechanic;
        private MetroFramework.Controls.MetroComboBox comboMechanic;
        private Clients.Controls.ClientView clientView1;
        private MetroFramework.Controls.MetroButton btnRegisterAcquisition;
    }
}