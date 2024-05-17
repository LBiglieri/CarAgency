namespace CarAgency.UI
{
    partial class UserManagementForm
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
            this.Users = new MetroFramework.Controls.MetroPanel();
            this.tvUsername = new MetroFramework.Controls.MetroTextBox();
            this.btnCancelOpp = new MetroFramework.Controls.MetroButton();
            this.btnApplyOpp = new MetroFramework.Controls.MetroButton();
            this.btnUnblockUser = new MetroFramework.Controls.MetroButton();
            this.btnDeleteUser = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.toggleActivos = new MetroFramework.Controls.MetroToggle();
            this.lblActiveUSers = new MetroFramework.Controls.MetroLabel();
            this.btnUpdateUser = new MetroFramework.Controls.MetroButton();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.btnAddUser = new MetroFramework.Controls.MetroButton();
            this.tbName = new MetroFramework.Controls.MetroTextBox();
            this.tbSurname = new MetroFramework.Controls.MetroTextBox();
            this.tbRole = new MetroFramework.Controls.MetroTextBox();
            this.chkBlocked = new MetroFramework.Controls.MetroCheckBox();
            this.chkActive = new MetroFramework.Controls.MetroCheckBox();
            this.Users.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // Users
            // 
            this.Users.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Users.Controls.Add(this.chkActive);
            this.Users.Controls.Add(this.chkBlocked);
            this.Users.Controls.Add(this.tbRole);
            this.Users.Controls.Add(this.tbSurname);
            this.Users.Controls.Add(this.tbName);
            this.Users.Controls.Add(this.tvUsername);
            this.Users.Controls.Add(this.btnCancelOpp);
            this.Users.Controls.Add(this.btnApplyOpp);
            this.Users.Controls.Add(this.btnUnblockUser);
            this.Users.Controls.Add(this.btnDeleteUser);
            this.Users.Controls.Add(this.metroPanel1);
            this.Users.Controls.Add(this.btnUpdateUser);
            this.Users.Controls.Add(this.metroGrid1);
            this.Users.Controls.Add(this.btnAddUser);
            this.Users.HorizontalScrollbarBarColor = true;
            this.Users.HorizontalScrollbarHighlightOnWheel = false;
            this.Users.HorizontalScrollbarSize = 10;
            this.Users.Location = new System.Drawing.Point(23, 63);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(891, 472);
            this.Users.TabIndex = 0;
            this.Users.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Users.VerticalScrollbarBarColor = true;
            this.Users.VerticalScrollbarHighlightOnWheel = false;
            this.Users.VerticalScrollbarSize = 10;
            // 
            // tvUsername
            // 
            // 
            // 
            // 
            this.tvUsername.CustomButton.Image = null;
            this.tvUsername.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.tvUsername.CustomButton.Name = "";
            this.tvUsername.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tvUsername.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tvUsername.CustomButton.TabIndex = 1;
            this.tvUsername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tvUsername.CustomButton.UseSelectable = true;
            this.tvUsername.CustomButton.Visible = false;
            this.tvUsername.Icon = global::UI.Properties.Resources.user__1_;
            this.tvUsername.Lines = new string[0];
            this.tvUsername.Location = new System.Drawing.Point(23, 336);
            this.tvUsername.MaxLength = 32767;
            this.tvUsername.Name = "tvUsername";
            this.tvUsername.PasswordChar = '\0';
            this.tvUsername.PromptText = "Username";
            this.tvUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tvUsername.SelectedText = "";
            this.tvUsername.SelectionLength = 0;
            this.tvUsername.SelectionStart = 0;
            this.tvUsername.ShortcutsEnabled = true;
            this.tvUsername.Size = new System.Drawing.Size(294, 23);
            this.tvUsername.TabIndex = 14;
            this.tvUsername.UseSelectable = true;
            this.tvUsername.WaterMark = "Username";
            this.tvUsername.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tvUsername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnCancelOpp
            // 
            this.btnCancelOpp.Location = new System.Drawing.Point(714, 411);
            this.btnCancelOpp.Name = "btnCancelOpp";
            this.btnCancelOpp.Size = new System.Drawing.Size(153, 35);
            this.btnCancelOpp.TabIndex = 13;
            this.btnCancelOpp.Text = "Cancel";
            this.btnCancelOpp.UseSelectable = true;
            // 
            // btnApplyOpp
            // 
            this.btnApplyOpp.Location = new System.Drawing.Point(714, 358);
            this.btnApplyOpp.Name = "btnApplyOpp";
            this.btnApplyOpp.Size = new System.Drawing.Size(153, 35);
            this.btnApplyOpp.TabIndex = 12;
            this.btnApplyOpp.Text = "Apply";
            this.btnApplyOpp.UseSelectable = true;
            // 
            // btnUnblockUser
            // 
            this.btnUnblockUser.Location = new System.Drawing.Point(714, 195);
            this.btnUnblockUser.Name = "btnUnblockUser";
            this.btnUnblockUser.Size = new System.Drawing.Size(153, 35);
            this.btnUnblockUser.TabIndex = 11;
            this.btnUnblockUser.Text = "Unblock";
            this.btnUnblockUser.UseSelectable = true;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(714, 154);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(153, 35);
            this.btnDeleteUser.TabIndex = 10;
            this.btnDeleteUser.Text = "Delete";
            this.btnDeleteUser.UseSelectable = true;
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.toggleActivos);
            this.metroPanel1.Controls.Add(this.lblActiveUSers);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 21);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(249, 45);
            this.metroPanel1.TabIndex = 9;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // toggleActivos
            // 
            this.toggleActivos.AutoSize = true;
            this.toggleActivos.Location = new System.Drawing.Point(149, 14);
            this.toggleActivos.Name = "toggleActivos";
            this.toggleActivos.Size = new System.Drawing.Size(80, 17);
            this.toggleActivos.TabIndex = 7;
            this.toggleActivos.Text = "Off";
            this.toggleActivos.UseSelectable = true;
            this.toggleActivos.CheckedChanged += new System.EventHandler(this.toggleActivos_CheckedChanged);
            // 
            // lblActiveUSers
            // 
            this.lblActiveUSers.AutoSize = true;
            this.lblActiveUSers.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblActiveUSers.Location = new System.Drawing.Point(3, 8);
            this.lblActiveUSers.Name = "lblActiveUSers";
            this.lblActiveUSers.Size = new System.Drawing.Size(140, 25);
            this.lblActiveUSers.TabIndex = 0;
            this.lblActiveUSers.Text = "Only active users";
            this.lblActiveUSers.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Location = new System.Drawing.Point(714, 113);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(153, 35);
            this.btnUpdateUser.TabIndex = 8;
            this.btnUpdateUser.Text = "Update";
            this.btnUpdateUser.UseSelectable = true;
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
            this.metroGrid1.Location = new System.Drawing.Point(23, 72);
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
            this.metroGrid1.Size = new System.Drawing.Size(664, 246);
            this.metroGrid1.TabIndex = 6;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(714, 72);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(153, 35);
            this.btnAddUser.TabIndex = 5;
            this.btnAddUser.Text = "Add";
            this.btnAddUser.UseSelectable = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnConfigureUser_Click);
            // 
            // tbName
            // 
            // 
            // 
            // 
            this.tbName.CustomButton.Image = null;
            this.tbName.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.tbName.CustomButton.Name = "";
            this.tbName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbName.CustomButton.TabIndex = 1;
            this.tbName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbName.CustomButton.UseSelectable = true;
            this.tbName.CustomButton.Visible = false;
            this.tbName.Icon = global::UI.Properties.Resources.user__1_;
            this.tbName.Lines = new string[0];
            this.tbName.Location = new System.Drawing.Point(23, 365);
            this.tbName.MaxLength = 32767;
            this.tbName.Name = "tbName";
            this.tbName.PasswordChar = '\0';
            this.tbName.PromptText = "Name";
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbName.SelectedText = "";
            this.tbName.SelectionLength = 0;
            this.tbName.SelectionStart = 0;
            this.tbName.ShortcutsEnabled = true;
            this.tbName.Size = new System.Drawing.Size(294, 23);
            this.tbName.TabIndex = 15;
            this.tbName.UseSelectable = true;
            this.tbName.WaterMark = "Name";
            this.tbName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbSurname
            // 
            // 
            // 
            // 
            this.tbSurname.CustomButton.Image = null;
            this.tbSurname.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.tbSurname.CustomButton.Name = "";
            this.tbSurname.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbSurname.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbSurname.CustomButton.TabIndex = 1;
            this.tbSurname.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbSurname.CustomButton.UseSelectable = true;
            this.tbSurname.CustomButton.Visible = false;
            this.tbSurname.Icon = global::UI.Properties.Resources.user__1_;
            this.tbSurname.Lines = new string[0];
            this.tbSurname.Location = new System.Drawing.Point(23, 394);
            this.tbSurname.MaxLength = 32767;
            this.tbSurname.Name = "tbSurname";
            this.tbSurname.PasswordChar = '\0';
            this.tbSurname.PromptText = "Surname";
            this.tbSurname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbSurname.SelectedText = "";
            this.tbSurname.SelectionLength = 0;
            this.tbSurname.SelectionStart = 0;
            this.tbSurname.ShortcutsEnabled = true;
            this.tbSurname.Size = new System.Drawing.Size(294, 23);
            this.tbSurname.TabIndex = 16;
            this.tbSurname.UseSelectable = true;
            this.tbSurname.WaterMark = "Surname";
            this.tbSurname.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbSurname.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbRole
            // 
            // 
            // 
            // 
            this.tbRole.CustomButton.Image = null;
            this.tbRole.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.tbRole.CustomButton.Name = "";
            this.tbRole.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tbRole.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbRole.CustomButton.TabIndex = 1;
            this.tbRole.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbRole.CustomButton.UseSelectable = true;
            this.tbRole.CustomButton.Visible = false;
            this.tbRole.Icon = global::UI.Properties.Resources.user__1_;
            this.tbRole.Lines = new string[0];
            this.tbRole.Location = new System.Drawing.Point(23, 423);
            this.tbRole.MaxLength = 32767;
            this.tbRole.Name = "tbRole";
            this.tbRole.PasswordChar = '\0';
            this.tbRole.PromptText = "Role";
            this.tbRole.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbRole.SelectedText = "";
            this.tbRole.SelectionLength = 0;
            this.tbRole.SelectionStart = 0;
            this.tbRole.ShortcutsEnabled = true;
            this.tbRole.Size = new System.Drawing.Size(294, 23);
            this.tbRole.TabIndex = 17;
            this.tbRole.UseSelectable = true;
            this.tbRole.WaterMark = "Role";
            this.tbRole.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbRole.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // chkBlocked
            // 
            this.chkBlocked.AutoSize = true;
            this.chkBlocked.Location = new System.Drawing.Point(361, 402);
            this.chkBlocked.Name = "chkBlocked";
            this.chkBlocked.Size = new System.Drawing.Size(65, 15);
            this.chkBlocked.TabIndex = 18;
            this.chkBlocked.Text = "Blocked";
            this.chkBlocked.UseSelectable = true;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(361, 431);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 15);
            this.chkActive.TabIndex = 19;
            this.chkActive.Text = "Active";
            this.chkActive.UseSelectable = true;
            // 
            // UserManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 562);
            this.Controls.Add(this.Users);
            this.MaximizeBox = false;
            this.Name = "UserManagementForm";
            this.Resizable = false;
            this.Text = "User Management";
            this.Users.ResumeLayout(false);
            this.Users.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel Users;
        private MetroFramework.Controls.MetroLabel lblActiveUSers;
        private MetroFramework.Controls.MetroButton btnAddUser;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroToggle toggleActivos;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton btnUpdateUser;
        private MetroFramework.Controls.MetroButton btnUnblockUser;
        private MetroFramework.Controls.MetroButton btnDeleteUser;
        private MetroFramework.Controls.MetroButton btnApplyOpp;
        private MetroFramework.Controls.MetroButton btnCancelOpp;
        private MetroFramework.Controls.MetroTextBox tvUsername;
        private MetroFramework.Controls.MetroCheckBox chkActive;
        private MetroFramework.Controls.MetroCheckBox chkBlocked;
        private MetroFramework.Controls.MetroTextBox tbRole;
        private MetroFramework.Controls.MetroTextBox tbSurname;
        private MetroFramework.Controls.MetroTextBox tbName;
    }
}

