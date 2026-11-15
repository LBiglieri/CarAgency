namespace UI.Reportes
{
    partial class ProyeccionVentasForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblDescription = new MetroFramework.Controls.MetroLabel();
            this.lblDataUntil = new MetroFramework.Controls.MetroLabel();
            this.dtDataUntil = new MetroFramework.Controls.MetroDateTime();
            this.lblHorizon = new MetroFramework.Controls.MetroLabel();
            this.btnGenerate = new MetroFramework.Controls.MetroButton();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            //
            // lblDescription
            //
            this.lblDescription.Location = new System.Drawing.Point(23, 70);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(574, 60);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Tag = "SalesForecastDescription";
            this.lblDescription.Text = "Proyecta las ventas de los próximos 30 días a partir de las reservas vigentes y del ritmo de cotizaciones y reservas.";
            this.lblDescription.WrapToLine = true;
            //
            // lblDataUntil
            //
            this.lblDataUntil.Location = new System.Drawing.Point(23, 146);
            this.lblDataUntil.Name = "lblDataUntil";
            this.lblDataUntil.Size = new System.Drawing.Size(150, 29);
            this.lblDataUntil.TabIndex = 1;
            this.lblDataUntil.Tag = "SalesForecastDataUntil";
            this.lblDataUntil.Text = "Datos hasta";
            this.lblDataUntil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dtDataUntil
            //
            this.dtDataUntil.CustomFormat = "dd/MM/yyyy";
            this.dtDataUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDataUntil.Location = new System.Drawing.Point(180, 146);
            this.dtDataUntil.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtDataUntil.Name = "dtDataUntil";
            this.dtDataUntil.Size = new System.Drawing.Size(200, 29);
            this.dtDataUntil.TabIndex = 2;
            this.dtDataUntil.ValueChanged += new System.EventHandler(this.dtDataUntil_ValueChanged);
            //
            // lblHorizon
            //
            this.lblHorizon.Location = new System.Drawing.Point(23, 186);
            this.lblHorizon.Name = "lblHorizon";
            this.lblHorizon.Size = new System.Drawing.Size(574, 29);
            this.lblHorizon.TabIndex = 3;
            this.lblHorizon.Text = "Proyección del 01/01/2026 al 30/01/2026";
            this.lblHorizon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnGenerate
            //
            this.btnGenerate.Location = new System.Drawing.Point(331, 236);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 35);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Tag = "SalesForecastGenerate";
            this.btnGenerate.Text = "Generar PDF";
            this.btnGenerate.UseSelectable = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            //
            // btnExit
            //
            this.btnExit.Location = new System.Drawing.Point(467, 236);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(130, 35);
            this.btnExit.TabIndex = 5;
            this.btnExit.Tag = "AuditClose";
            this.btnExit.Text = "Salir";
            this.btnExit.UseSelectable = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            //
            // ProyeccionVentasForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 294);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblDataUntil);
            this.Controls.Add(this.dtDataUntil);
            this.Controls.Add(this.lblHorizon);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnExit);
            this.MaximizeBox = false;
            this.Name = "ProyeccionVentasForm";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "ProyeccionVentasForm";
            this.Text = "Proyección de ventas";
            this.Disposed += new System.EventHandler(this.ProyeccionVentasForm_Disposed);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblDescription;
        private MetroFramework.Controls.MetroLabel lblDataUntil;
        private MetroFramework.Controls.MetroDateTime dtDataUntil;
        private MetroFramework.Controls.MetroLabel lblHorizon;
        private MetroFramework.Controls.MetroButton btnGenerate;
        private MetroFramework.Controls.MetroButton btnExit;
    }
}
