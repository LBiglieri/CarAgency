namespace UI.Vehicles
{
    partial class ManagePaperworkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePaperworkForm));
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.panelPaperwokr = new MetroFramework.Controls.MetroPanel();
            this.clientView1 = new UI.Clients.Controls.ClientView();
            this.btnAddPaperwork = new MetroFramework.Controls.MetroButton();
            this.btnEditPaperwork = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.panelPaperwokr.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
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
            this.metroGrid1.Location = new System.Drawing.Point(18, 199);
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
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(1153, 528);
            this.metroGrid1.TabIndex = 2;
            this.metroGrid1.SelectionChanged += new System.EventHandler(this.metroGrid1_SelectionChanged);
            // 
            // panelPaperwokr
            // 
            this.panelPaperwokr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPaperwokr.Controls.Add(this.clientView1);
            this.panelPaperwokr.Controls.Add(this.btnAddPaperwork);
            this.panelPaperwokr.Controls.Add(this.btnEditPaperwork);
            this.panelPaperwokr.Controls.Add(this.metroGrid1);
            this.panelPaperwokr.HorizontalScrollbarBarColor = true;
            this.panelPaperwokr.HorizontalScrollbarHighlightOnWheel = false;
            this.panelPaperwokr.HorizontalScrollbarSize = 10;
            this.panelPaperwokr.Location = new System.Drawing.Point(23, 63);
            this.panelPaperwokr.Name = "panelPaperwokr";
            this.panelPaperwokr.Size = new System.Drawing.Size(1192, 750);
            this.panelPaperwokr.TabIndex = 0;
            this.panelPaperwokr.Theme = MetroFramework.MetroThemeStyle.Light;
            this.panelPaperwokr.VerticalScrollbarBarColor = true;
            this.panelPaperwokr.VerticalScrollbarHighlightOnWheel = false;
            this.panelPaperwokr.VerticalScrollbarSize = 10;
            // 
            // clientView1
            // 
            this.clientView1.Location = new System.Drawing.Point(18, 20);
            this.clientView1.Name = "clientView1";
            this.clientView1.Size = new System.Drawing.Size(363, 132);
            this.clientView1.TabIndex = 0;
            // 
            // btnAddPaperwork
            // 
            this.btnAddPaperwork.Location = new System.Drawing.Point(18, 158);
            this.btnAddPaperwork.Name = "btnAddPaperwork";
            this.btnAddPaperwork.Size = new System.Drawing.Size(169, 35);
            this.btnAddPaperwork.TabIndex = 4;
            this.btnAddPaperwork.Text = "Add Paperwork";
            this.btnAddPaperwork.UseSelectable = true;
            this.btnAddPaperwork.Click += new System.EventHandler(this.btnAddPaperwork_Click);
            // 
            // btnEditPaperwork
            // 
            this.btnEditPaperwork.Location = new System.Drawing.Point(193, 158);
            this.btnEditPaperwork.Name = "btnEditPaperwork";
            this.btnEditPaperwork.Size = new System.Drawing.Size(169, 35);
            this.btnEditPaperwork.TabIndex = 3;
            this.btnEditPaperwork.Text = "Edit Paperwork";
            this.btnEditPaperwork.UseSelectable = true;
            this.btnEditPaperwork.Click += new System.EventHandler(this.btnEditPaperwork_Click);
            // 
            // ManagePaperworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 837);
            this.Controls.Add(this.panelPaperwokr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagePaperworkForm";
            this.Text = "Manage Paperwork";
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.panelPaperwokr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroPanel panelPaperwokr;
        private MetroFramework.Controls.MetroButton btnEditPaperwork;
        private MetroFramework.Controls.MetroButton btnAddPaperwork;
        private Clients.Controls.ClientView clientView1;
    }
}