namespace CarAgency.UI
{
    partial class IntegrityRepairForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntegrityRepairForm));
            this.IntegrityPanel = new MetroFramework.Controls.MetroPanel();
            this.lblIntegrityDetected = new MetroFramework.Controls.MetroLabel();
            this.tbIssues = new MetroFramework.Controls.MetroTextBox();
            this.btnRecalculate = new MetroFramework.Controls.MetroButton();
            this.btnRestore = new MetroFramework.Controls.MetroButton();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.btnVerify = new MetroFramework.Controls.MetroButton();
            this.IntegrityPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // IntegrityPanel
            //
            this.IntegrityPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IntegrityPanel.Controls.Add(this.lblIntegrityDetected);
            this.IntegrityPanel.Controls.Add(this.tbIssues);
            this.IntegrityPanel.Controls.Add(this.btnRecalculate);
            this.IntegrityPanel.Controls.Add(this.btnRestore);
            this.IntegrityPanel.Controls.Add(this.btnExit);
            this.IntegrityPanel.Controls.Add(this.btnVerify);
            this.IntegrityPanel.HorizontalScrollbarBarColor = true;
            this.IntegrityPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.IntegrityPanel.HorizontalScrollbarSize = 10;
            this.IntegrityPanel.Location = new System.Drawing.Point(23, 63);
            this.IntegrityPanel.Name = "IntegrityPanel";
            this.IntegrityPanel.Size = new System.Drawing.Size(734, 384);
            this.IntegrityPanel.TabIndex = 0;
            this.IntegrityPanel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.IntegrityPanel.VerticalScrollbarBarColor = true;
            this.IntegrityPanel.VerticalScrollbarHighlightOnWheel = false;
            this.IntegrityPanel.VerticalScrollbarSize = 10;
            //
            // lblIntegrityDetected
            //
            this.lblIntegrityDetected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIntegrityDetected.AutoSize = false;
            this.lblIntegrityDetected.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblIntegrityDetected.ForeColor = System.Drawing.Color.DarkRed;
            this.lblIntegrityDetected.Location = new System.Drawing.Point(12, 10);
            this.lblIntegrityDetected.Name = "lblIntegrityDetected";
            this.lblIntegrityDetected.Size = new System.Drawing.Size(708, 48);
            this.lblIntegrityDetected.TabIndex = 0;
            this.lblIntegrityDetected.Tag = "IntegrityDetected";
            this.lblIntegrityDetected.Text = "Se detectaron inconsistencias. El acceso normal está bloqueado.";
            this.lblIntegrityDetected.UseCustomForeColor = true;
            this.lblIntegrityDetected.WrapToLine = true;
            //
            // tbIssues
            //
            this.tbIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            //
            //
            //
            this.tbIssues.CustomButton.Image = null;
            this.tbIssues.CustomButton.Location = new System.Drawing.Point(678, 2);
            this.tbIssues.CustomButton.Name = "";
            this.tbIssues.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.tbIssues.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbIssues.CustomButton.TabIndex = 1;
            this.tbIssues.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbIssues.CustomButton.UseSelectable = true;
            this.tbIssues.CustomButton.Visible = false;
            this.tbIssues.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbIssues.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbIssues.Lines = new string[0];
            this.tbIssues.Location = new System.Drawing.Point(12, 66);
            this.tbIssues.MaxLength = 32767;
            this.tbIssues.Multiline = true;
            this.tbIssues.Name = "tbIssues";
            this.tbIssues.PasswordChar = '\0';
            this.tbIssues.ReadOnly = true;
            this.tbIssues.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbIssues.SelectedText = "";
            this.tbIssues.SelectionLength = 0;
            this.tbIssues.SelectionStart = 0;
            this.tbIssues.ShortcutsEnabled = true;
            this.tbIssues.Size = new System.Drawing.Size(708, 240);
            this.tbIssues.TabIndex = 1;
            this.tbIssues.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbIssues.UseSelectable = true;
            this.tbIssues.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbIssues.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            //
            // btnRecalculate
            //
            this.btnRecalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecalculate.Location = new System.Drawing.Point(12, 322);
            this.btnRecalculate.Name = "btnRecalculate";
            this.btnRecalculate.Size = new System.Drawing.Size(170, 36);
            this.btnRecalculate.TabIndex = 2;
            this.btnRecalculate.Tag = "IntegrityRecalculate";
            this.btnRecalculate.Text = "Recalcular DV";
            this.btnRecalculate.UseSelectable = true;
            this.btnRecalculate.Click += new System.EventHandler(this.btnRecalculate_Click);
            //
            // btnRestore
            //
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestore.Location = new System.Drawing.Point(194, 322);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(170, 36);
            this.btnRestore.TabIndex = 3;
            this.btnRestore.Tag = "IntegrityRestore";
            this.btnRestore.Text = "Restaurar BD";
            this.btnRestore.UseSelectable = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // btnExit
            //
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(376, 322);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(130, 36);
            this.btnExit.TabIndex = 4;
            this.btnExit.Tag = "IntegrityExit";
            this.btnExit.Text = "Salir";
            this.btnExit.UseSelectable = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            //
            // btnVerify
            //
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(550, 322);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(170, 36);
            this.btnVerify.TabIndex = 5;
            this.btnVerify.Tag = "IntegrityVerify";
            this.btnVerify.Text = "Verificar integridad";
            this.btnVerify.UseSelectable = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            //
            // IntegrityRepairForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 470);
            this.Controls.Add(this.IntegrityPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "IntegrityRepairForm";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "IntegrityRepairTitle";
            this.Text = "Reparar integridad de la base de datos";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IntegrityRepairForm_FormClosing);
            this.IntegrityPanel.ResumeLayout(false);
            this.IntegrityPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel IntegrityPanel;
        private MetroFramework.Controls.MetroLabel lblIntegrityDetected;
        private MetroFramework.Controls.MetroTextBox tbIssues;
        private MetroFramework.Controls.MetroButton btnRecalculate;
        private MetroFramework.Controls.MetroButton btnRestore;
        private MetroFramework.Controls.MetroButton btnExit;
        private MetroFramework.Controls.MetroButton btnVerify;
    }
}
