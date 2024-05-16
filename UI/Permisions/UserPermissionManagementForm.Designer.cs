namespace CarAgency.UI
{
    partial class UserPermissionManagementForm
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
            this.Users = new MetroFramework.Controls.MetroPanel();
            this.Patent = new MetroFramework.Controls.MetroPanel();
            this.comboPatent = new MetroFramework.Controls.MetroComboBox();
            this.btnAddPatent = new MetroFramework.Controls.MetroButton();
            this.lblPatent = new MetroFramework.Controls.MetroLabel();
            this.Family = new MetroFramework.Controls.MetroPanel();
            this.btnAddFamily = new MetroFramework.Controls.MetroButton();
            this.lblFamily = new MetroFramework.Controls.MetroLabel();
            this.comboFamily = new MetroFramework.Controls.MetroComboBox();
            this.btnConfigureUser = new MetroFramework.Controls.MetroButton();
            this.comboUsers = new MetroFramework.Controls.MetroComboBox();
            this.lblPatents = new MetroFramework.Controls.MetroLabel();
            this.ConfigureUsers = new MetroFramework.Controls.MetroPanel();
            this.treeUser = new System.Windows.Forms.TreeView();
            this.btnSaveFamily = new MetroFramework.Controls.MetroButton();
            this.lblConfigureUsers = new MetroFramework.Controls.MetroLabel();
            this.Users.SuspendLayout();
            this.Patent.SuspendLayout();
            this.Family.SuspendLayout();
            this.ConfigureUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // Users
            // 
            this.Users.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Users.Controls.Add(this.Patent);
            this.Users.Controls.Add(this.Family);
            this.Users.Controls.Add(this.btnConfigureUser);
            this.Users.Controls.Add(this.comboUsers);
            this.Users.Controls.Add(this.lblPatents);
            this.Users.HorizontalScrollbarBarColor = true;
            this.Users.HorizontalScrollbarHighlightOnWheel = false;
            this.Users.HorizontalScrollbarSize = 10;
            this.Users.Location = new System.Drawing.Point(23, 63);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(357, 511);
            this.Users.TabIndex = 0;
            this.Users.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Users.VerticalScrollbarBarColor = true;
            this.Users.VerticalScrollbarHighlightOnWheel = false;
            this.Users.VerticalScrollbarSize = 10;
            // 
            // Patent
            // 
            this.Patent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Patent.Controls.Add(this.comboPatent);
            this.Patent.Controls.Add(this.btnAddPatent);
            this.Patent.Controls.Add(this.lblPatent);
            this.Patent.HorizontalScrollbarBarColor = true;
            this.Patent.HorizontalScrollbarHighlightOnWheel = false;
            this.Patent.HorizontalScrollbarSize = 10;
            this.Patent.Location = new System.Drawing.Point(23, 141);
            this.Patent.Name = "Patent";
            this.Patent.Size = new System.Drawing.Size(307, 166);
            this.Patent.TabIndex = 6;
            this.Patent.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Patent.VerticalScrollbarBarColor = true;
            this.Patent.VerticalScrollbarHighlightOnWheel = false;
            this.Patent.VerticalScrollbarSize = 10;
            // 
            // comboPatent
            // 
            this.comboPatent.FormattingEnabled = true;
            this.comboPatent.ItemHeight = 23;
            this.comboPatent.Location = new System.Drawing.Point(23, 63);
            this.comboPatent.Name = "comboPatent";
            this.comboPatent.Size = new System.Drawing.Size(259, 29);
            this.comboPatent.TabIndex = 5;
            this.comboPatent.UseSelectable = true;
            // 
            // btnAddPatent
            // 
            this.btnAddPatent.Location = new System.Drawing.Point(75, 118);
            this.btnAddPatent.Name = "btnAddPatent";
            this.btnAddPatent.Size = new System.Drawing.Size(153, 23);
            this.btnAddPatent.TabIndex = 3;
            this.btnAddPatent.Text = "Add";
            this.btnAddPatent.UseSelectable = true;
            this.btnAddPatent.Click += new System.EventHandler(this.btnAddPatent_Click);
            // 
            // lblPatent
            // 
            this.lblPatent.AutoSize = true;
            this.lblPatent.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblPatent.Location = new System.Drawing.Point(23, 12);
            this.lblPatent.Name = "lblPatent";
            this.lblPatent.Size = new System.Drawing.Size(95, 25);
            this.lblPatent.TabIndex = 0;
            this.lblPatent.Text = "Add Patent";
            this.lblPatent.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // Family
            // 
            this.Family.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Family.Controls.Add(this.btnAddFamily);
            this.Family.Controls.Add(this.lblFamily);
            this.Family.Controls.Add(this.comboFamily);
            this.Family.HorizontalScrollbarBarColor = true;
            this.Family.HorizontalScrollbarHighlightOnWheel = false;
            this.Family.HorizontalScrollbarSize = 10;
            this.Family.Location = new System.Drawing.Point(23, 324);
            this.Family.Name = "Family";
            this.Family.Size = new System.Drawing.Size(307, 166);
            this.Family.TabIndex = 6;
            this.Family.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Family.VerticalScrollbarBarColor = true;
            this.Family.VerticalScrollbarHighlightOnWheel = false;
            this.Family.VerticalScrollbarSize = 10;
            // 
            // btnAddFamily
            // 
            this.btnAddFamily.Location = new System.Drawing.Point(72, 118);
            this.btnAddFamily.Name = "btnAddFamily";
            this.btnAddFamily.Size = new System.Drawing.Size(153, 23);
            this.btnAddFamily.TabIndex = 3;
            this.btnAddFamily.Text = "Add";
            this.btnAddFamily.UseSelectable = true;
            this.btnAddFamily.Click += new System.EventHandler(this.btnAddFamily_Click);
            // 
            // lblFamily
            // 
            this.lblFamily.AutoSize = true;
            this.lblFamily.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFamily.Location = new System.Drawing.Point(24, 12);
            this.lblFamily.Name = "lblFamily";
            this.lblFamily.Size = new System.Drawing.Size(95, 25);
            this.lblFamily.TabIndex = 0;
            this.lblFamily.Text = "Add Family";
            this.lblFamily.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // comboFamily
            // 
            this.comboFamily.FormattingEnabled = true;
            this.comboFamily.ItemHeight = 23;
            this.comboFamily.Location = new System.Drawing.Point(24, 63);
            this.comboFamily.Name = "comboFamily";
            this.comboFamily.Size = new System.Drawing.Size(257, 29);
            this.comboFamily.TabIndex = 4;
            this.comboFamily.UseSelectable = true;
            // 
            // btnConfigureUser
            // 
            this.btnConfigureUser.Location = new System.Drawing.Point(99, 100);
            this.btnConfigureUser.Name = "btnConfigureUser";
            this.btnConfigureUser.Size = new System.Drawing.Size(153, 23);
            this.btnConfigureUser.TabIndex = 5;
            this.btnConfigureUser.Text = "Configure";
            this.btnConfigureUser.UseSelectable = true;
            this.btnConfigureUser.Click += new System.EventHandler(this.btnConfigureUser_Click);
            // 
            // comboUsers
            // 
            this.comboUsers.FormattingEnabled = true;
            this.comboUsers.ItemHeight = 23;
            this.comboUsers.Location = new System.Drawing.Point(23, 53);
            this.comboUsers.Name = "comboUsers";
            this.comboUsers.Size = new System.Drawing.Size(307, 29);
            this.comboUsers.TabIndex = 4;
            this.comboUsers.UseSelectable = true;
            // 
            // lblPatents
            // 
            this.lblPatents.AutoSize = true;
            this.lblPatents.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblPatents.Location = new System.Drawing.Point(23, 12);
            this.lblPatents.Name = "lblPatents";
            this.lblPatents.Size = new System.Drawing.Size(53, 25);
            this.lblPatents.TabIndex = 0;
            this.lblPatents.Text = "Users";
            this.lblPatents.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ConfigureUsers
            // 
            this.ConfigureUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureUsers.Controls.Add(this.treeUser);
            this.ConfigureUsers.Controls.Add(this.btnSaveFamily);
            this.ConfigureUsers.Controls.Add(this.lblConfigureUsers);
            this.ConfigureUsers.HorizontalScrollbarBarColor = true;
            this.ConfigureUsers.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureUsers.HorizontalScrollbarSize = 10;
            this.ConfigureUsers.Location = new System.Drawing.Point(400, 63);
            this.ConfigureUsers.Name = "ConfigureUsers";
            this.ConfigureUsers.Size = new System.Drawing.Size(357, 511);
            this.ConfigureUsers.TabIndex = 8;
            this.ConfigureUsers.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureUsers.VerticalScrollbarBarColor = true;
            this.ConfigureUsers.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureUsers.VerticalScrollbarSize = 10;
            // 
            // treeUser
            // 
            this.treeUser.Location = new System.Drawing.Point(23, 53);
            this.treeUser.Name = "treeUser";
            this.treeUser.Size = new System.Drawing.Size(313, 399);
            this.treeUser.TabIndex = 8;
            // 
            // btnSaveFamily
            // 
            this.btnSaveFamily.Location = new System.Drawing.Point(102, 467);
            this.btnSaveFamily.Name = "btnSaveFamily";
            this.btnSaveFamily.Size = new System.Drawing.Size(150, 23);
            this.btnSaveFamily.TabIndex = 7;
            this.btnSaveFamily.Text = "Save Changes";
            this.btnSaveFamily.UseSelectable = true;
            this.btnSaveFamily.Click += new System.EventHandler(this.btnSaveFamily_Click);
            // 
            // lblConfigureUsers
            // 
            this.lblConfigureUsers.AutoSize = true;
            this.lblConfigureUsers.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblConfigureUsers.Location = new System.Drawing.Point(23, 12);
            this.lblConfigureUsers.Name = "lblConfigureUsers";
            this.lblConfigureUsers.Size = new System.Drawing.Size(219, 25);
            this.lblConfigureUsers.TabIndex = 0;
            this.lblConfigureUsers.Text = "Configure User Permissions";
            this.lblConfigureUsers.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // UserPermissionManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 597);
            this.Controls.Add(this.ConfigureUsers);
            this.Controls.Add(this.Users);
            this.MaximizeBox = false;
            this.Name = "UserPermissionManagementForm";
            this.Resizable = false;
            this.Text = "User Permission Configuration";
            this.Users.ResumeLayout(false);
            this.Users.PerformLayout();
            this.Patent.ResumeLayout(false);
            this.Patent.PerformLayout();
            this.Family.ResumeLayout(false);
            this.Family.PerformLayout();
            this.ConfigureUsers.ResumeLayout(false);
            this.ConfigureUsers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel Users;
        private MetroFramework.Controls.MetroLabel lblPatents;
        private MetroFramework.Controls.MetroComboBox comboUsers;
        private MetroFramework.Controls.MetroPanel Patent;
        private MetroFramework.Controls.MetroComboBox comboPatent;
        private MetroFramework.Controls.MetroButton btnAddPatent;
        private MetroFramework.Controls.MetroLabel lblPatent;
        private MetroFramework.Controls.MetroButton btnConfigureUser;
        private MetroFramework.Controls.MetroPanel Family;
        private MetroFramework.Controls.MetroButton btnAddFamily;
        private MetroFramework.Controls.MetroLabel lblFamily;
        private MetroFramework.Controls.MetroComboBox comboFamily;
        private MetroFramework.Controls.MetroPanel ConfigureUsers;
        private System.Windows.Forms.TreeView treeUser;
        private MetroFramework.Controls.MetroButton btnSaveFamily;
        private MetroFramework.Controls.MetroLabel lblConfigureUsers;
    }
}

