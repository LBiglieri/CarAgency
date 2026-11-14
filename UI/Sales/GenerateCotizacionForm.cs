using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Personas.Controls;
using Security.Session;
using static System.Net.Mime.MediaTypeNames;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{

    public partial class GenerateCotizacionForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        VehiculoBLL _VehicleBLL;
        CotizacionBLL _QuotationBLL;
        List<Cotizacion> quotations = new List<Cotizacion>();
        List<Vehiculo> vehicles = new List<Vehiculo>();
        Guid FilterMake = Guid.Empty;
        Guid FilterModel = Guid.Empty;
        Guid FilterVersion = Guid.Empty;
        Guid FilterColour = Guid.Empty;
        // Mientras se recarga el combo de colores (alta o cambio de idioma) no se toca el filtro.
        bool _fillingColours;
        int FilterYear_From;
        int FilterYear_To;
        int FilterKilometers_From;
        int FilterKilometers_To;
        double FilterPrice_From;
        double FilterPrice_To;
        int FilterDoors_From;
        int FilterDoors_To;

        public GenerateCotizacionForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _VehicleBLL = new VehiculoBLL();
            _QuotationBLL = new CotizacionBLL();

            PerformUpdateMakeCombos();
            PerformUpdateColourCombos();
            GridColumns.TranslateValues(metroGrid1, "Color_Descripcion", LookupText.Colour);
            FilterMake = Guid.Empty;
            FilterColour = Guid.Empty;
            PerformUpdateVehiclesView();

            personView1.personViewMode = PersonaViewMode.WithRegistration;
            this.personView1.PersonFound += new EventHandler(personView1_PersonFound);

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void GenerateQuotationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("GenerateCotizacionForm");
            this.Refresh();
            lblFilters.Text = LanguageService.GetTagText(lblFilters.Tag.ToString());
            lblMake.Text = LanguageService.GetTagText(lblMake.Tag.ToString());
            lblModel.Text = LanguageService.GetTagText(lblModel.Tag.ToString());
            lblVersion.Text = LanguageService.GetTagText(lblVersion.Tag.ToString());
            lblColour.Text = LanguageService.GetTagText(lblColour.Tag.ToString());
            tbYearFrom.WaterMark = LanguageService.GetTagText(tbYearFrom.Tag.ToString());
            tbYearTo.WaterMark = LanguageService.GetTagText(tbYearTo.Tag.ToString());
            tbKilometersFrom.WaterMark = LanguageService.GetTagText(tbKilometersFrom.Tag.ToString());
            tbKilometersTo.WaterMark = LanguageService.GetTagText(tbKilometersTo.Tag.ToString());
            tbPriceFrom.WaterMark = LanguageService.GetTagText(tbPriceFrom.Tag.ToString());
            tbPriceTo.WaterMark = LanguageService.GetTagText(tbPriceTo.Tag.ToString());
            tbDoorsFrom.WaterMark = LanguageService.GetTagText(tbDoorsFrom.Tag.ToString());
            tbDoorsTo.WaterMark = LanguageService.GetTagText(tbDoorsTo.Tag.ToString());
            btnGoToReservations.Text = LanguageService.GetTagText(btnGoToReservations.Tag.ToString());
            btnGenerateQuotation.Text = LanguageService.GetTagText(btnGenerateQuotation.Tag.ToString());
            GridColumns.Translate(metroGrid1);
            PerformUpdateColourCombos();
        }
        void PerformUpdateVehiclesView()
        {
            try
            {
                vehicles = _VehicleBLL.GetActiveVehiclesByFilters(FilterMake, FilterModel, FilterVersion, FilterColour, FilterPrice_From, FilterPrice_To, FilterYear_From, FilterYear_To, FilterDoors_From, FilterDoors_To, FilterKilometers_From, FilterKilometers_To);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = vehicles;
                // Con la lista vacia la grilla igual genera las columnas: se ordenan y traducen siempre.
                GridColumns.Show(metroGrid1, GridColumns.Vehiculo);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        void PerformGenerateQuotationPDF(Persona q_person, List<Cotizacion> q_quotations)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency® " + LanguageService.GetTagText("VehiclesQuotationfor").Trim() + " " + q_person.Nombre + " " + q_person.Apellido;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            XFont fontTitle = new XFont("Verdana", 32);
            XFont fontSubTitle = new XFont("Verdana", 20);
            XFont fontBody = new XFont("Verdana", 10);

            XRect rect = new XRect(25, 0, page.Width.Point-20, page.Height.Point-20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString("CarAgency®", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(25, 65, page.Width.Point-20, page.Height.Point-20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(LanguageService.GetTagText("VehiclesQuotationfor").Trim() + " " + q_person.Nombre + " " + q_person.Apellido, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            int rectplace = 120;
            foreach (Cotizacion quotation in q_quotations)
            {
                Vehiculo vehicle = _VehicleBLL.GetById(quotation.Vehiculo_Id);

                rect = new XRect(25, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(LanguageService.GetTagText("Vehicle").Trim() + " " + vehicle.Anio + " " + vehicle.Marca_Descripcion + " " + vehicle.Modelo_Descripcion + " " + vehicle.Version_Descripcion + " . " + LanguageService.GetTagText("lblColour") + " " + LookupText.Colour(vehicle.Color_Descripcion), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                
                rectplace += 25;

                rect = new XRect(25, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("                  " + LanguageService.GetTagText("LicensePlate").Trim() + " " + vehicle.Patente + ". KM's:" + vehicle.Kilometraje.ToString() + ". " + LanguageService.GetTagText("Price").Trim() + " " + vehicle.Precio.ToString("N2"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                rectplace += 25;
            }


            rect = new XRect(0, rectplace+50, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString(LanguageService.GetTagText("QuotationPricesValidMessage").Trim() + " " + DateTime.Now.ToString("dd/MM/yyyy"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            
            rectplace += 75;

            rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Quotation " + q_person.Apellido + " " + q_person.Nombre + ".pdf";
            document.Close();
            document.Save(filename);


            Process.Start(filename);
        }
        // Es un filtro: viene vacio. Al cambiar de idioma se conserva el color elegido.
        private void PerformUpdateColourCombos()
        {
            try
            {
                _fillingColours = true;
                Guid selected = FilterColour;
                LookupText.Fill(comboColour, _VehicleBLL.GetAllColours(), c => LookupText.Colour(c.Descripcion),
                    c => selected != Guid.Empty && c.Color_Id == selected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            finally
            {
                _fillingColours = false;
            }
        }
        private void PerformUpdateMakeCombos()
        {
            try
            {
                List<Marca> makes = _VehicleBLL.GetAllMakes();
                comboMake.DataSource = makes;
                comboMake.DisplayMember = "Descripcion";
                comboMake.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateModelCombos()
        {
            try
            {
                if (comboMake.SelectedIndex != -1)
                {
                    List<Modelo> models = _VehicleBLL.GetAllModelsByMake((Marca)comboMake.SelectedValue);
                    comboModel.DataSource = models;
                    comboModel.DisplayMember = "Descripcion";
                    comboModel.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            if (comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedValue) != null) 
            {
            }
        }
        private void PerformUpdateVersionCombo()
        {
            try
            {
                if (comboModel.SelectedIndex != -1 && comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedValue) != null && ((Modelo)comboModel.SelectedValue) != null) 
                {
                    List<CarAgency.BE.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Marca)comboMake.SelectedValue,(Modelo)comboModel.SelectedValue);
                    comboVersions.DataSource = versions;
                    comboVersions.DisplayMember = "Descripcion";
                    comboVersions.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        #endregion


        private void comboDeleteVersionMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
            if (comboMake.SelectedIndex != -1 && ((Marca)comboMake.SelectedItem).Descripcion  != "")
            {
                FilterMake = ((Marca)comboMake.SelectedItem).Marca_Id;
                FilterModel = Guid.Empty;
                PerformUpdateVehiclesView();
            }
        }

        private void comboDeleteVersionModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateVersionCombo();
            if (comboModel.SelectedIndex != -1 && ((Modelo)comboModel.SelectedItem).Descripcion != "")
            {
                FilterModel = ((Modelo)comboModel.SelectedItem).Modelo_Id;
                FilterVersion = Guid.Empty;
                PerformUpdateVehiclesView();
            }
        }

        private void comboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboVersions.SelectedIndex != -1 && ((CarAgency.BE.Version)comboVersions.SelectedItem).Descripcion != "")
            {
                FilterVersion = ((CarAgency.BE.Version)comboVersions.SelectedItem).Version_Id;
                PerformUpdateVehiclesView();
            }
        }

        private void comboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarAgency.BE.Color colour = LookupText.Selected<CarAgency.BE.Color>(comboColour);
            if (!_fillingColours && colour != null && colour.Descripcion != "")
            {
                FilterColour = colour.Color_Id;
                PerformUpdateVehiclesView();
            }
        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void tbYearFrom_Leave(object sender, EventArgs e)
        {
            int year;
            if (int.TryParse(tbYearFrom.Text,out year))
            {
                FilterYear_From = year;
                PerformUpdateVehiclesView();
            }
        }

        private void tbYearTo_Leave(object sender, EventArgs e)
        {
            int year;
            if (int.TryParse(tbYearTo.Text, out year))
            {
                FilterYear_To = year;
                PerformUpdateVehiclesView();
            }
        }

        private void tbKilometersFrom_Leave(object sender, EventArgs e)
        {
            int kilometers;
            if (int.TryParse(tbKilometersFrom.Text, out kilometers))
            {
                FilterKilometers_From = kilometers;
                PerformUpdateVehiclesView();
            }
        }

        private void tbKilometersTo_Leave(object sender, EventArgs e)
        {
            int kilometers;
            if (int.TryParse(tbKilometersTo.Text, out kilometers))
            {
                FilterKilometers_To = kilometers;
                PerformUpdateVehiclesView();
            }
        }

        private void tbPriceFrom_Leave(object sender, EventArgs e)
        {
            double price;
            if (double.TryParse(tbPriceFrom.Text, out price))
            {
                FilterPrice_From = price;
                PerformUpdateVehiclesView();
            }
        }

        private void tbPriceTo_Leave(object sender, EventArgs e)
        {
            double price;
            if (double.TryParse(tbPriceTo.Text, out price))
            {
                FilterPrice_To = price;
                PerformUpdateVehiclesView();
            }
        }

        private void tbDoorsFrom_Leave(object sender, EventArgs e)
        {
            int doors;
            if (int.TryParse(tbDoorsFrom.Text, out doors))
            {
                FilterDoors_From = doors;
                PerformUpdateVehiclesView();
            }
        }

        private void tbDoorsTo_Leave(object sender, EventArgs e)
        {
            int doors;
            if (int.TryParse(tbDoorsTo.Text, out doors))
            {
                FilterDoors_To = doors;
                PerformUpdateVehiclesView();
            }
        }

        private void btnGenerateQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                if (personView1.person != null && metroGrid1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = metroGrid1.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        Cotizacion quotation = new Cotizacion();
                        quotation.Cotizacion_Id = Guid.NewGuid();
                        quotation.Persona_Id = personView1.person.Persona_Id;
                        quotation.Vehiculo_Id = ((Vehiculo)row.DataBoundItem).Vehiculo_Id;
                        quotation.Precio = ((Vehiculo)row.DataBoundItem).Precio;
                        quotation.Fecha = DateTime.Now;

                        SQLUpdateResult result = _QuotationBLL.AddQuotation(quotation);

                        if (result.sqlResult == SQLResultType.success)
                        {
                            quotations.Add(quotation);
                        }
                    }
                    metroPanel1.Enabled = false;
                    personView1.Enabled = false;
                    btnGenerateQuotation.Enabled = false;
                    btnGoToReservations.Enabled = true;

                    if (quotations.Count > 0)
                    {
                        PerformGenerateQuotationPDF(personView1.person, quotations);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnGoToReservations_Click(object sender, EventArgs e)
        {
            GenerateReservaForm frm = new GenerateReservaForm(personView1.person);
            frm.Show();
        }
        public void personView1_PersonFound(object sender, EventArgs e)
        {
        }
    }
}
