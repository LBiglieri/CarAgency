namespace UI.Ayuda
{
    partial class AyudaForm
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
            this.lblBody = new System.Windows.Forms.Label();
            this.lblNext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblBody
            //
            this.lblBody.AutoSize = true;
            this.lblBody.BackColor = System.Drawing.Color.Transparent;
            this.lblBody.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBody.ForeColor = System.Drawing.Color.Black;
            this.lblBody.Location = new System.Drawing.Point(20, 60);
            this.lblBody.MaximumSize = new System.Drawing.Size(380, 0);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(0, 19);
            this.lblBody.TabIndex = 0;
            //
            // lblNext
            //
            this.lblNext.AutoSize = true;
            this.lblNext.BackColor = System.Drawing.Color.Transparent;
            this.lblNext.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNext.ForeColor = System.Drawing.Color.Black;
            this.lblNext.Location = new System.Drawing.Point(20, 91);
            this.lblNext.MaximumSize = new System.Drawing.Size(380, 0);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(0, 19);
            this.lblNext.TabIndex = 1;
            //
            // AyudaForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(420, 130);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.lblNext);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AyudaForm";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ayuda";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AyudaForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.Label lblNext;
    }
}
