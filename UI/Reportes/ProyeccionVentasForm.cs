using System;
using System.Diagnostics;
using System.Windows.Forms;
using BE;
using BLL;
using CarAgency.BE;
using Security.Session;
using UI.Reportes.Pdf;
using UI.Ayuda;

namespace UI.Reportes
{
    // Reporte inteligente de PN1: se elige hasta que dia tomar los datos y se genera el PDF con la proyeccion
    // de los dias siguientes. La pantalla no muestra numeros: el reporte es el PDF.
    public partial class ProyeccionVentasForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        readonly ReporteVentasBLL _reportBLL = new ReporteVentasBLL();

        public ProyeccionVentasForm()
        {
            ReporteVentasBLL.RequireAccess();
            InitializeComponent();
            AyudaButton.Add(this);
            dtDataUntil.MaxDate = DateTime.Today;
            dtDataUntil.Value = DateTime.Today;
            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        #region Perform
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("ProyeccionVentasForm");
            lblDescription.Text = string.Format(LanguageService.GetTagText(lblDescription.Tag.ToString()), ProyeccionVentasCalculator.HorizonteDias);
            lblDataUntil.Text = LanguageService.GetTagText(lblDataUntil.Tag.ToString());
            btnGenerate.Text = LanguageService.GetTagText(btnGenerate.Tag.ToString());
            btnExit.Text = LanguageService.GetTagText(btnExit.Tag.ToString());
            PerformUpdateHorizon();
            this.Refresh();
        }

        void PerformUpdateHorizon()
        {
            DateTime from = dtDataUntil.Value.Date.AddDays(1);
            DateTime to = from.AddDays(ProyeccionVentasCalculator.HorizonteDias - 1);
            lblHorizon.Text = string.Format(LanguageService.GetTagText("SalesForecastHorizon"), from.ToString("dd/MM/yyyy"), to.ToString("dd/MM/yyyy"));
        }

        void PerformGenerateReport()
        {
            ProyeccionVentas forecast = _reportBLL.GenerarProyeccion(dtDataUntil.Value);
            string path = ProyeccionVentasPdf.Generar(forecast, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            Process.Start(path);
        }
        #endregion

        #region Form Events
        private void ProyeccionVentasForm_Disposed(object sender, EventArgs e)
        {
            LanguageService.Detach(this);
        }

        private void dtDataUntil_ValueChanged(object sender, EventArgs e)
        {
            PerformUpdateHorizon();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Cursor previous = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnGenerate.Enabled = false;
                PerformGenerateReport();
            }
            catch (TranslatableException ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageService.GetTagText("SalesForecastPdfFailed"));
            }
            finally
            {
                btnGenerate.Enabled = true;
                Cursor.Current = previous;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
