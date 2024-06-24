namespace CarAgency.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newQuotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newReservationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehicleModelManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehicleManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permissionConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userPermissionConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.billingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTitle.Location = new System.Drawing.Point(58, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(94, 25);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "CarAgency";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionToolStripMenuItem,
            this.salesToolStripMenuItem,
            this.billingToolStripMenuItem,
            this.masterToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1015, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SessionToolStripMenuItem
            // 
            this.SessionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.SessionToolStripMenuItem.Name = "SessionToolStripMenuItem";
            this.SessionToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.SessionToolStripMenuItem.Text = "Session";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // salesToolStripMenuItem
            // 
            this.salesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newQuotationToolStripMenuItem,
            this.newReservationToolStripMenuItem});
            this.salesToolStripMenuItem.Name = "salesToolStripMenuItem";
            this.salesToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.salesToolStripMenuItem.Text = "Sales";
            // 
            // newQuotationToolStripMenuItem
            // 
            this.newQuotationToolStripMenuItem.Name = "newQuotationToolStripMenuItem";
            this.newQuotationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.newQuotationToolStripMenuItem.Text = "New Quotation";
            this.newQuotationToolStripMenuItem.Click += new System.EventHandler(this.newQuotationToolStripMenuItem_Click);
            // 
            // newReservationToolStripMenuItem
            // 
            this.newReservationToolStripMenuItem.Name = "newReservationToolStripMenuItem";
            this.newReservationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.newReservationToolStripMenuItem.Text = "New Reservation";
            this.newReservationToolStripMenuItem.Click += new System.EventHandler(this.newReservationToolStripMenuItem_Click);
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vehicleModelManagementToolStripMenuItem,
            this.vehicleManagementToolStripMenuItem});
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.masterToolStripMenuItem.Text = "Master";
            // 
            // vehicleModelManagementToolStripMenuItem
            // 
            this.vehicleModelManagementToolStripMenuItem.Name = "vehicleModelManagementToolStripMenuItem";
            this.vehicleModelManagementToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.vehicleModelManagementToolStripMenuItem.Text = "Vehicle Model Configuration";
            this.vehicleModelManagementToolStripMenuItem.Click += new System.EventHandler(this.vehicleModelManagementToolStripMenuItem_Click);
            // 
            // vehicleManagementToolStripMenuItem
            // 
            this.vehicleManagementToolStripMenuItem.Name = "vehicleManagementToolStripMenuItem";
            this.vehicleManagementToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.vehicleManagementToolStripMenuItem.Text = "Vehicle Management";
            this.vehicleManagementToolStripMenuItem.Click += new System.EventHandler(this.vehicleManagementToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.permissionConfigurationToolStripMenuItem,
            this.userPermissionConfigurationToolStripMenuItem,
            this.userManagementToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // permissionConfigurationToolStripMenuItem
            // 
            this.permissionConfigurationToolStripMenuItem.Name = "permissionConfigurationToolStripMenuItem";
            this.permissionConfigurationToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.permissionConfigurationToolStripMenuItem.Text = "Permission Configuration";
            this.permissionConfigurationToolStripMenuItem.Click += new System.EventHandler(this.permissionConfigurationToolStripMenuItem_Click);
            // 
            // userPermissionConfigurationToolStripMenuItem
            // 
            this.userPermissionConfigurationToolStripMenuItem.Name = "userPermissionConfigurationToolStripMenuItem";
            this.userPermissionConfigurationToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.userPermissionConfigurationToolStripMenuItem.Text = "User Permission Configuration";
            this.userPermissionConfigurationToolStripMenuItem.Click += new System.EventHandler(this.userPermissionConfigurationToolStripMenuItem_Click);
            // 
            // userManagementToolStripMenuItem
            // 
            this.userManagementToolStripMenuItem.Name = "userManagementToolStripMenuItem";
            this.userManagementToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.userManagementToolStripMenuItem.Text = "User Management";
            this.userManagementToolStripMenuItem.Click += new System.EventHandler(this.userManagementToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UI.Properties.Resources.logo32px;
            this.pictureBox1.Location = new System.Drawing.Point(23, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 39);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateInvoiceToolStripMenuItem});
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.billingToolStripMenuItem.Text = "Billing";
            // 
            // generateInvoiceToolStripMenuItem
            // 
            this.generateInvoiceToolStripMenuItem.Name = "generateInvoiceToolStripMenuItem";
            this.generateInvoiceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generateInvoiceToolStripMenuItem.Text = "Generate Invoice";
            this.generateInvoiceToolStripMenuItem.Click += new System.EventHandler(this.generateInvoiceToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 647);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lblTitle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permissionConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userPermissionConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userManagementToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehicleModelManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehicleManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newQuotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newReservationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem billingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateInvoiceToolStripMenuItem;
    }
}

