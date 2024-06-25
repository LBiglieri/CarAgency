using BLL;
using CarAgency.BLL;
using CarAgency.Entities;
using CarAgency.UI;
using CarAgency.Utilities.Persistence;
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
using UI.Clients.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace UI.Vehicles
{

    public partial class GenerateQuotationForm : MetroFramework.Forms.MetroForm
    {
        VehicleBLL _VehicleBLL;
        QuotationBLL _QuotationBLL;
        List<Quotation> quotations = new List<Quotation>();
        List<Vehicle> vehicles = new List<Vehicle>();
        Guid FilterMake = Guid.Empty;
        Guid FilterModel = Guid.Empty;
        Guid FilterVersion = Guid.Empty;
        Guid FilterColour = Guid.Empty;
        int FilterYear_From;
        int FilterYear_To;
        int FilterKilometers_From;
        int FilterKilometers_To;
        double FilterPrice_From;
        double FilterPrice_To;
        int FilterDoors_From;
        int FilterDoors_To;

        public GenerateQuotationForm()
        {
            InitializeComponent();
            _VehicleBLL = new VehicleBLL();
            _QuotationBLL = new QuotationBLL();

            PerformUpdateMakeCombos();
            PerformUpdateColourCombos();
            FilterMake = Guid.Empty;
            FilterColour = Guid.Empty;
            PerformUpdateVehiclesView();

            clientView1.clientViewMode = ClientViewMode.WithRegistration;
            this.clientView1.ClientFound += new EventHandler(clientView1_ClientFound);
        }
        #region "Performs"
        void PerformUpdateVehiclesView()
        {
            try
            {
                vehicles = _VehicleBLL.GetActiveVehiclesByFilters(FilterMake, FilterModel, FilterVersion, FilterColour, FilterPrice_From, FilterPrice_To, FilterYear_From, FilterYear_To, FilterDoors_From, FilterDoors_To, FilterKilometers_From, FilterKilometers_To);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = vehicles;
                if (vehicles != null && vehicles.Count > 0 ) 
                {
                    metroGrid1.Columns["Make_Id"].Visible = false;
                    metroGrid1.Columns["Model_Id"].Visible = false;
                    metroGrid1.Columns["Version_Id"].Visible = false;
                    metroGrid1.Columns["Colour_Id"].Visible = false;
                    metroGrid1.Columns["ImageLink"].Visible = false;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        void PerformGenerateQuotationPDF(Client q_client, List<Quotation> q_quotations)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency®️ Vehicles Quotation for " + q_client.Name + " " + q_client.Surname;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            XFont fontTitle = new XFont("Verdana", 32);
            XFont fontSubTitle = new XFont("Verdana", 20);
            XFont fontBody = new XFont("Verdana", 10);

            XRect rect = new XRect(25, 0, page.Width.Point-20, page.Height.Point-20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString("CarAgency®️", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(25, 65, page.Width.Point-20, page.Height.Point-20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString("Vehicles Quotation for " + q_client.Name + " " + q_client.Surname, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            int rectplace = 120;
            foreach (Quotation quotation in q_quotations)
            {
                Vehicle vehicle = _VehicleBLL.GetById(quotation.Vehicle_Id);

                rect = new XRect(25, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("Vehicle " + vehicle.Year + " " + vehicle.Make_Description + " " + vehicle.Model_Description + " " + vehicle.Version_Description + " . Colour:" + vehicle.Colour_Description, fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                
                rectplace += 25;

                rect = new XRect(25, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("                  License plate: " + vehicle.License_Plate + ". KM's:" + vehicle.Kilometers.ToString() + ". PRICE: $" + vehicle.Price.ToString(), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                rectplace += 25;
            }


            rect = new XRect(0, rectplace+50, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("Quotation prices are only valid for purchases/reservations on " +DateTime.Now.ToString("dd/MM/yyyy"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            
            rectplace += 75;

            rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®️", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Quotation" + q_client.Surname+ " " + q_client.Name + ".pdf";
            document.Close();
            document.Save(filename);


            Process.Start(filename);
        }
        private void PerformUpdateColourCombos()
        {
            try
            {
                List<Colour> colour = _VehicleBLL.GetAllColours();
                comboColour.DataSource = colour;
                comboColour.DisplayMember = "Description";
                comboColour.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PerformUpdateMakeCombos()
        {
            try
            {
                List<Make> makes = _VehicleBLL.GetAllMakes();
                comboMake.DataSource = makes;
                comboMake.DisplayMember = "Description";
                comboMake.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PerformUpdateModelCombos()
        {
            try
            {
                if (comboMake.SelectedIndex != -1)
                {
                    List<Model> models = _VehicleBLL.GetAllModelsByMake((Make)comboMake.SelectedValue);
                    comboModel.DataSource = models;
                    comboModel.DisplayMember = "Description";
                    comboModel.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedValue) != null) 
            {
            }
        }
        private void PerformUpdateVersionCombo()
        {
            try
            {
                if (comboModel.SelectedIndex != -1 && comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedValue) != null && ((Model)comboModel.SelectedValue) != null) 
                {
                    List<CarAgency.Entities.Version> versions = _VehicleBLL.GetAllVersionsByMakeModel((Make)comboMake.SelectedValue,(Model)comboModel.SelectedValue);
                    comboVersions.DataSource = versions;
                    comboVersions.DisplayMember = "Description";
                    comboVersions.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion


        private void comboDeleteVersionMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateModelCombos();
            if (comboMake.SelectedIndex != -1 && ((Make)comboMake.SelectedItem).Description  != "")
            {
                FilterMake = ((Make)comboMake.SelectedItem).Id;
                FilterModel = Guid.Empty;
                PerformUpdateVehiclesView();
            }
        }

        private void comboDeleteVersionModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformUpdateVersionCombo();
            if (comboModel.SelectedIndex != -1 && ((Model)comboModel.SelectedItem).Description != "")
            {
                FilterModel = ((Model)comboModel.SelectedItem).Id;
                FilterVersion = Guid.Empty;
                PerformUpdateVehiclesView();
            }
        }

        private void comboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboVersions.SelectedIndex != -1 && ((CarAgency.Entities.Version)comboVersions.SelectedItem).Description != "")
            {
                FilterVersion = ((CarAgency.Entities.Version)comboVersions.SelectedItem).Id;
                PerformUpdateVehiclesView();
            }
        }

        private void comboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboColour.SelectedIndex != -1 && ((Colour)comboColour.SelectedItem).Description != "")
            {
                FilterColour = ((Colour)comboColour.SelectedItem).Id;
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
                if (clientView1.client != null && metroGrid1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = metroGrid1.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        Quotation quotation = new Quotation();
                        quotation.Id = Guid.NewGuid();
                        quotation.Client_Id = clientView1.client.Id;
                        quotation.Vehicle_Id = ((Vehicle)row.DataBoundItem).Id;
                        quotation.Price = ((Vehicle)row.DataBoundItem).Price;
                        quotation.Creation_Date = DateTime.Now;

                        SQLUpdateResult result = _QuotationBLL.AddQuotation(quotation);

                        if (result.sqlResult == SQLResultType.success)
                        {
                            quotations.Add(quotation);
                        }
                    }
                    metroPanel1.Enabled = false;
                    clientView1.Enabled = false;
                    btnGenerateQuotation.Enabled = false;
                    btnGoToReservations.Enabled = true;

                    if (quotations.Count > 0)
                    {
                        PerformGenerateQuotationPDF(clientView1.client, quotations);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGoToReservations_Click(object sender, EventArgs e)
        {
            GenerateReservationForm frm = new GenerateReservationForm(clientView1.client);
            frm.Show();
        }
        public void clientView1_ClientFound(object sender, EventArgs e)
        {
        }
    }
}
