namespace CarAgency.UI
{
    partial class PermissionManagementForm
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
            this.Patents = new MetroFramework.Controls.MetroPanel();
            this.NewPatent = new MetroFramework.Controls.MetroPanel();
            this.comboNewPatent = new MetroFramework.Controls.MetroComboBox();
            this.tbNewPatent = new MetroFramework.Controls.MetroTextBox();
            this.btnAddNewPatent = new MetroFramework.Controls.MetroButton();
            this.lblNewPatent = new MetroFramework.Controls.MetroLabel();
            this.btnAddPatentToFamily = new MetroFramework.Controls.MetroButton();
            this.comboPatente = new MetroFramework.Controls.MetroComboBox();
            this.lblPatents = new MetroFramework.Controls.MetroLabel();
            this.Families = new MetroFramework.Controls.MetroPanel();
            this.btnAddToFamilyToFamily = new MetroFramework.Controls.MetroButton();
            this.NewFamily = new MetroFramework.Controls.MetroPanel();
            this.tbNewFamily = new MetroFramework.Controls.MetroTextBox();
            this.btnAddNewFamily = new MetroFramework.Controls.MetroButton();
            this.lblNewFamily = new MetroFramework.Controls.MetroLabel();
            this.btnConfigureFamily = new MetroFramework.Controls.MetroButton();
            this.comboFamily = new MetroFramework.Controls.MetroComboBox();
            this.lblFamilies = new MetroFramework.Controls.MetroLabel();
            this.ConfigureFamilies = new MetroFramework.Controls.MetroPanel();
            this.treeFamily = new System.Windows.Forms.TreeView();
            this.btnSaveFamily = new MetroFramework.Controls.MetroButton();
            this.lblConfigureFamilies = new MetroFramework.Controls.MetroLabel();
            this.Patents.SuspendLayout();
            this.NewPatent.SuspendLayout();
            this.Families.SuspendLayout();
            this.NewFamily.SuspendLayout();
            this.ConfigureFamilies.SuspendLayout();
            this.SuspendLayout();
            // 
            // Patents
            // 
            this.Patents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Patents.Controls.Add(this.NewPatent);
            this.Patents.Controls.Add(this.btnAddPatentToFamily);
            this.Patents.Controls.Add(this.comboPatente);
            this.Patents.Controls.Add(this.lblPatents);
            this.Patents.HorizontalScrollbarBarColor = true;
            this.Patents.HorizontalScrollbarHighlightOnWheel = false;
            this.Patents.HorizontalScrollbarSize = 10;
            this.Patents.Location = new System.Drawing.Point(23, 63);
            this.Patents.Name = "Patents";
            this.Patents.Size = new System.Drawing.Size(357, 397);
            this.Patents.TabIndex = 0;
            this.Patents.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Patents.VerticalScrollbarBarColor = true;
            this.Patents.VerticalScrollbarHighlightOnWheel = false;
            this.Patents.VerticalScrollbarSize = 10;
            // 
            // NewPatent
            // 
            this.NewPatent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewPatent.Controls.Add(this.comboNewPatent);
            this.NewPatent.Controls.Add(this.tbNewPatent);
            this.NewPatent.Controls.Add(this.btnAddNewPatent);
            this.NewPatent.Controls.Add(this.lblNewPatent);
            this.NewPatent.HorizontalScrollbarBarColor = true;
            this.NewPatent.HorizontalScrollbarHighlightOnWheel = false;
            this.NewPatent.HorizontalScrollbarSize = 10;
            this.NewPatent.Location = new System.Drawing.Point(23, 141);
            this.NewPatent.Name = "NewPatent";
            this.NewPatent.Size = new System.Drawing.Size(307, 225);
            this.NewPatent.TabIndex = 6;
            this.NewPatent.Theme = MetroFramework.MetroThemeStyle.Light;
            this.NewPatent.VerticalScrollbarBarColor = true;
            this.NewPatent.VerticalScrollbarHighlightOnWheel = false;
            this.NewPatent.VerticalScrollbarSize = 10;
            // 
            // comboNewPatent
            // 
            this.comboNewPatent.FormattingEnabled = true;
            this.comboNewPatent.ItemHeight = 23;
            this.comboNewPatent.Location = new System.Drawing.Point(23, 63);
            this.comboNewPatent.Name = "comboNewPatent";
            this.comboNewPatent.Size = new System.Drawing.Size(259, 29);
            this.comboNewPatent.TabIndex = 5;
            this.comboNewPatent.UseSelectable = true;
            // 
            // tbNewPatent
            // 
            // 
            // 
            // 
            this.tbNewPatent.CustomButton.Image = null;
            this.tbNewPatent.CustomButton.Location = new System.Drawing.Point(229, 2);
            this.tbNewPatent.CustomButton.Name = "";
            this.tbNewPatent.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbNewPatent.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbNewPatent.CustomButton.TabIndex = 1;
            this.tbNewPatent.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbNewPatent.CustomButton.UseSelectable = true;
            this.tbNewPatent.CustomButton.Visible = false;
            this.tbNewPatent.DisplayIcon = true;
            this.tbNewPatent.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbNewPatent.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbNewPatent.Lines = new string[0];
            this.tbNewPatent.Location = new System.Drawing.Point(23, 118);
            this.tbNewPatent.MaxLength = 32767;
            this.tbNewPatent.Name = "tbNewPatent";
            this.tbNewPatent.PasswordChar = '\0';
            this.tbNewPatent.PromptText = "Enter the new Pantent Name";
            this.tbNewPatent.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbNewPatent.SelectedText = "";
            this.tbNewPatent.SelectionLength = 0;
            this.tbNewPatent.SelectionStart = 0;
            this.tbNewPatent.ShortcutsEnabled = true;
            this.tbNewPatent.Size = new System.Drawing.Size(259, 32);
            this.tbNewPatent.TabIndex = 1;
            this.tbNewPatent.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbNewPatent.UseSelectable = true;
            this.tbNewPatent.WaterMark = "Enter the new Pantent Name";
            this.tbNewPatent.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbNewPatent.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnAddNewPatent
            // 
            this.btnAddNewPatent.Location = new System.Drawing.Point(75, 175);
            this.btnAddNewPatent.Name = "btnAddNewPatent";
            this.btnAddNewPatent.Size = new System.Drawing.Size(153, 23);
            this.btnAddNewPatent.TabIndex = 3;
            this.btnAddNewPatent.Text = "Save";
            this.btnAddNewPatent.UseSelectable = true;
            this.btnAddNewPatent.Click += new System.EventHandler(this.btnAddNewPatent_Click);
            // 
            // lblNewPatent
            // 
            this.lblNewPatent.AutoSize = true;
            this.lblNewPatent.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblNewPatent.Location = new System.Drawing.Point(23, 12);
            this.lblNewPatent.Name = "lblNewPatent";
            this.lblNewPatent.Size = new System.Drawing.Size(149, 25);
            this.lblNewPatent.TabIndex = 0;
            this.lblNewPatent.Text = "Create new Patent";
            this.lblNewPatent.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnAddPatentToFamily
            // 
            this.btnAddPatentToFamily.Location = new System.Drawing.Point(99, 100);
            this.btnAddPatentToFamily.Name = "btnAddPatentToFamily";
            this.btnAddPatentToFamily.Size = new System.Drawing.Size(153, 23);
            this.btnAddPatentToFamily.TabIndex = 5;
            this.btnAddPatentToFamily.Text = "Add to Family";
            this.btnAddPatentToFamily.UseSelectable = true;
            this.btnAddPatentToFamily.Click += new System.EventHandler(this.btnAddPatentToFamily_Click);
            // 
            // comboPatente
            // 
            this.comboPatente.FormattingEnabled = true;
            this.comboPatente.ItemHeight = 23;
            this.comboPatente.Location = new System.Drawing.Point(23, 53);
            this.comboPatente.Name = "comboPatente";
            this.comboPatente.Size = new System.Drawing.Size(307, 29);
            this.comboPatente.TabIndex = 4;
            this.comboPatente.UseSelectable = true;
            // 
            // lblPatents
            // 
            this.lblPatents.AutoSize = true;
            this.lblPatents.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblPatents.Location = new System.Drawing.Point(23, 12);
            this.lblPatents.Name = "lblPatents";
            this.lblPatents.Size = new System.Drawing.Size(66, 25);
            this.lblPatents.TabIndex = 0;
            this.lblPatents.Text = "Patents";
            this.lblPatents.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // Families
            // 
            this.Families.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Families.Controls.Add(this.btnAddToFamilyToFamily);
            this.Families.Controls.Add(this.NewFamily);
            this.Families.Controls.Add(this.btnConfigureFamily);
            this.Families.Controls.Add(this.comboFamily);
            this.Families.Controls.Add(this.lblFamilies);
            this.Families.HorizontalScrollbarBarColor = true;
            this.Families.HorizontalScrollbarHighlightOnWheel = false;
            this.Families.HorizontalScrollbarSize = 10;
            this.Families.Location = new System.Drawing.Point(395, 63);
            this.Families.Name = "Families";
            this.Families.Size = new System.Drawing.Size(357, 397);
            this.Families.TabIndex = 7;
            this.Families.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Families.VerticalScrollbarBarColor = true;
            this.Families.VerticalScrollbarHighlightOnWheel = false;
            this.Families.VerticalScrollbarSize = 10;
            // 
            // btnAddToFamilyToFamily
            // 
            this.btnAddToFamilyToFamily.Location = new System.Drawing.Point(180, 100);
            this.btnAddToFamilyToFamily.Name = "btnAddToFamilyToFamily";
            this.btnAddToFamilyToFamily.Size = new System.Drawing.Size(150, 23);
            this.btnAddToFamilyToFamily.TabIndex = 7;
            this.btnAddToFamilyToFamily.Text = "Add to Family";
            this.btnAddToFamilyToFamily.UseSelectable = true;
            this.btnAddToFamilyToFamily.Click += new System.EventHandler(this.btnAddToFamilyToFamily_Click);
            // 
            // NewFamily
            // 
            this.NewFamily.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewFamily.Controls.Add(this.tbNewFamily);
            this.NewFamily.Controls.Add(this.btnAddNewFamily);
            this.NewFamily.Controls.Add(this.lblNewFamily);
            this.NewFamily.HorizontalScrollbarBarColor = true;
            this.NewFamily.HorizontalScrollbarHighlightOnWheel = false;
            this.NewFamily.HorizontalScrollbarSize = 10;
            this.NewFamily.Location = new System.Drawing.Point(23, 141);
            this.NewFamily.Name = "NewFamily";
            this.NewFamily.Size = new System.Drawing.Size(307, 166);
            this.NewFamily.TabIndex = 6;
            this.NewFamily.Theme = MetroFramework.MetroThemeStyle.Light;
            this.NewFamily.VerticalScrollbarBarColor = true;
            this.NewFamily.VerticalScrollbarHighlightOnWheel = false;
            this.NewFamily.VerticalScrollbarSize = 10;
            // 
            // tbNewFamily
            // 
            // 
            // 
            // 
            this.tbNewFamily.CustomButton.Image = null;
            this.tbNewFamily.CustomButton.Location = new System.Drawing.Point(229, 2);
            this.tbNewFamily.CustomButton.Name = "";
            this.tbNewFamily.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbNewFamily.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbNewFamily.CustomButton.TabIndex = 1;
            this.tbNewFamily.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbNewFamily.CustomButton.UseSelectable = true;
            this.tbNewFamily.CustomButton.Visible = false;
            this.tbNewFamily.DisplayIcon = true;
            this.tbNewFamily.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbNewFamily.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbNewFamily.Lines = new string[0];
            this.tbNewFamily.Location = new System.Drawing.Point(23, 63);
            this.tbNewFamily.MaxLength = 32767;
            this.tbNewFamily.Name = "tbNewFamily";
            this.tbNewFamily.PasswordChar = '\0';
            this.tbNewFamily.PromptText = "Enter the new Family Name";
            this.tbNewFamily.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbNewFamily.SelectedText = "";
            this.tbNewFamily.SelectionLength = 0;
            this.tbNewFamily.SelectionStart = 0;
            this.tbNewFamily.ShortcutsEnabled = true;
            this.tbNewFamily.Size = new System.Drawing.Size(259, 32);
            this.tbNewFamily.TabIndex = 1;
            this.tbNewFamily.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbNewFamily.UseSelectable = true;
            this.tbNewFamily.WaterMark = "Enter the new Family Name";
            this.tbNewFamily.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbNewFamily.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnAddNewFamily
            // 
            this.btnAddNewFamily.Location = new System.Drawing.Point(72, 118);
            this.btnAddNewFamily.Name = "btnAddNewFamily";
            this.btnAddNewFamily.Size = new System.Drawing.Size(153, 23);
            this.btnAddNewFamily.TabIndex = 3;
            this.btnAddNewFamily.Text = "Save";
            this.btnAddNewFamily.UseSelectable = true;
            this.btnAddNewFamily.Click += new System.EventHandler(this.btnAddNewFamily_Click);
            // 
            // lblNewFamily
            // 
            this.lblNewFamily.AutoSize = true;
            this.lblNewFamily.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblNewFamily.Location = new System.Drawing.Point(23, 12);
            this.lblNewFamily.Name = "lblNewFamily";
            this.lblNewFamily.Size = new System.Drawing.Size(149, 25);
            this.lblNewFamily.TabIndex = 0;
            this.lblNewFamily.Text = "Create new Family";
            this.lblNewFamily.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnConfigureFamily
            // 
            this.btnConfigureFamily.Location = new System.Drawing.Point(23, 100);
            this.btnConfigureFamily.Name = "btnConfigureFamily";
            this.btnConfigureFamily.Size = new System.Drawing.Size(150, 23);
            this.btnConfigureFamily.TabIndex = 5;
            this.btnConfigureFamily.Text = "Configure";
            this.btnConfigureFamily.UseSelectable = true;
            this.btnConfigureFamily.Click += new System.EventHandler(this.btnConfigureFamily_Click);
            // 
            // comboFamily
            // 
            this.comboFamily.FormattingEnabled = true;
            this.comboFamily.ItemHeight = 23;
            this.comboFamily.Location = new System.Drawing.Point(23, 53);
            this.comboFamily.Name = "comboFamily";
            this.comboFamily.Size = new System.Drawing.Size(307, 29);
            this.comboFamily.TabIndex = 4;
            this.comboFamily.UseSelectable = true;
            // 
            // lblFamilies
            // 
            this.lblFamilies.AutoSize = true;
            this.lblFamilies.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblFamilies.Location = new System.Drawing.Point(23, 12);
            this.lblFamilies.Name = "lblFamilies";
            this.lblFamilies.Size = new System.Drawing.Size(71, 25);
            this.lblFamilies.TabIndex = 0;
            this.lblFamilies.Text = "Families";
            this.lblFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ConfigureFamilies
            // 
            this.ConfigureFamilies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConfigureFamilies.Controls.Add(this.treeFamily);
            this.ConfigureFamilies.Controls.Add(this.btnSaveFamily);
            this.ConfigureFamilies.Controls.Add(this.lblConfigureFamilies);
            this.ConfigureFamilies.HorizontalScrollbarBarColor = true;
            this.ConfigureFamilies.HorizontalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.HorizontalScrollbarSize = 10;
            this.ConfigureFamilies.Location = new System.Drawing.Point(767, 63);
            this.ConfigureFamilies.Name = "ConfigureFamilies";
            this.ConfigureFamilies.Size = new System.Drawing.Size(357, 397);
            this.ConfigureFamilies.TabIndex = 8;
            this.ConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ConfigureFamilies.VerticalScrollbarBarColor = true;
            this.ConfigureFamilies.VerticalScrollbarHighlightOnWheel = false;
            this.ConfigureFamilies.VerticalScrollbarSize = 10;
            // 
            // treeFamily
            // 
            this.treeFamily.Location = new System.Drawing.Point(23, 53);
            this.treeFamily.Name = "treeFamily";
            this.treeFamily.Size = new System.Drawing.Size(313, 293);
            this.treeFamily.TabIndex = 8;
            // 
            // btnSaveFamily
            // 
            this.btnSaveFamily.Location = new System.Drawing.Point(103, 359);
            this.btnSaveFamily.Name = "btnSaveFamily";
            this.btnSaveFamily.Size = new System.Drawing.Size(150, 23);
            this.btnSaveFamily.TabIndex = 7;
            this.btnSaveFamily.Text = "Save Family";
            this.btnSaveFamily.UseSelectable = true;
            this.btnSaveFamily.Click += new System.EventHandler(this.btnSaveFamily_Click);
            // 
            // lblConfigureFamilies
            // 
            this.lblConfigureFamilies.AutoSize = true;
            this.lblConfigureFamilies.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblConfigureFamilies.Location = new System.Drawing.Point(23, 12);
            this.lblConfigureFamilies.Name = "lblConfigureFamilies";
            this.lblConfigureFamilies.Size = new System.Drawing.Size(139, 25);
            this.lblConfigureFamilies.TabIndex = 0;
            this.lblConfigureFamilies.Text = "Configure Family";
            this.lblConfigureFamilies.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // PermissionManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 485);
            this.Controls.Add(this.ConfigureFamilies);
            this.Controls.Add(this.Families);
            this.Controls.Add(this.Patents);
            this.MaximizeBox = false;
            this.Name = "PermissionManagementForm";
            this.Resizable = false;
            this.Text = "Permission Configuration";
            this.Load += new System.EventHandler(this.PermissionManagementForm_Load);
            this.Patents.ResumeLayout(false);
            this.Patents.PerformLayout();
            this.NewPatent.ResumeLayout(false);
            this.NewPatent.PerformLayout();
            this.Families.ResumeLayout(false);
            this.Families.PerformLayout();
            this.NewFamily.ResumeLayout(false);
            this.NewFamily.PerformLayout();
            this.ConfigureFamilies.ResumeLayout(false);
            this.ConfigureFamilies.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel Patents;
        private MetroFramework.Controls.MetroLabel lblPatents;
        private MetroFramework.Controls.MetroComboBox comboPatente;
        private MetroFramework.Controls.MetroPanel NewPatent;
        private MetroFramework.Controls.MetroComboBox comboNewPatent;
        private MetroFramework.Controls.MetroTextBox tbNewPatent;
        private MetroFramework.Controls.MetroButton btnAddNewPatent;
        private MetroFramework.Controls.MetroLabel lblNewPatent;
        private MetroFramework.Controls.MetroButton btnAddPatentToFamily;
        private MetroFramework.Controls.MetroPanel Families;
        private MetroFramework.Controls.MetroButton btnAddToFamilyToFamily;
        private MetroFramework.Controls.MetroPanel NewFamily;
        private MetroFramework.Controls.MetroTextBox tbNewFamily;
        private MetroFramework.Controls.MetroButton btnAddNewFamily;
        private MetroFramework.Controls.MetroLabel lblNewFamily;
        private MetroFramework.Controls.MetroButton btnConfigureFamily;
        private MetroFramework.Controls.MetroComboBox comboFamily;
        private MetroFramework.Controls.MetroLabel lblFamilies;
        private MetroFramework.Controls.MetroPanel ConfigureFamilies;
        private System.Windows.Forms.TreeView treeFamily;
        private MetroFramework.Controls.MetroButton btnSaveFamily;
        private MetroFramework.Controls.MetroLabel lblConfigureFamilies;
    }
}

