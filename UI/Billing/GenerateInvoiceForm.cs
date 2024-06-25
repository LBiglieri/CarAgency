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
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Clients.Controls;

namespace UI.Vehicles
{
    public partial class GenerateInvoiceForm : MetroFramework.Forms.MetroForm
    {
        public Client _client;
        Reservation _reservation;
        Invoice _invoice;
        ReservationBLL _ReservationBLL;
        PaymentBLL _PaymentBLL;
        InvoiceBLL _InvoiceBLL;
        VehicleBLL _VehicleBLL;
        List<Payment> payments;

        double _price_left;

        public GenerateInvoiceForm()
        {
            InitializeComponent();
            _ReservationBLL = new ReservationBLL();
            _PaymentBLL = new PaymentBLL();
            _InvoiceBLL = new InvoiceBLL();
            _VehicleBLL = new VehicleBLL();


            this.clientView1.ClientFound += new EventHandler(clientView1_ClientFound);
        }
        #region "Performs"
        void PerformUpdatePaymentsView()
        {
            try
            {
                payments = _PaymentBLL.GetAllByInvoice(_invoice.Id);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = payments;
                if (payments!=null && payments.Count > 0)
                {
                    metroGrid1.Columns["Invoice_Id"].Visible = false;
                    metroGrid1.Columns["PaymentType_Id"].Visible = false;
                    metroGrid1.Columns["Id"].Visible = false;
                }
                double price_left = 0 - ((Reservation)comboReservations.SelectedItem).Price;
                if (payments != null && payments.Count > 0)
                {
                    foreach (Payment payment in payments)
                    {
                        price_left += payment.Amount;
                    }
                }
                _price_left = price_left;
                metroLabel1.Text = "Price Left to Pay: " + price_left.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void PerformGenerateInvoicePDF(Client q_client)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "CarAgency®️ Invoice for " + q_client.Name + " " + q_client.Surname;
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
                tf.DrawString("Invoice for " + tbRazonSocial.Text, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                rect = new XRect(25, 95, page.Width.Point-20, page.Height.Point-20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("CUIL/CUIT " + tbCUIL_CUIT_Client.Text, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                int rectplace = 175;

                Vehicle vehicle = _VehicleBLL.GetById(_reservation.Vehicle_Id);

                rect = new XRect(25, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("Vehicle " + vehicle.Year + " " + vehicle.Make_Description + " " + vehicle.Model_Description + " " + vehicle.Version_Description + " . Colour:" + vehicle.Colour_Description, fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                
                rectplace += 25;

                rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("                       License plate: " + vehicle.License_Plate + ". KM's:" + vehicle.Kilometers.ToString() + ".", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("PRICE: $" + _invoice.Amount.ToString(), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                rectplace += 425;

                rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("TOTAL " +_invoice.Amount.ToString(), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            
                rectplace += 25;

                rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®️", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Invoice" + q_client.Surname+ " " + q_client.Name + ".pdf";
                document.Close();
                document.Save(filename);


                Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PerformUpdateReservationCombo()
        {
            try
            {
                List<Reservation> reservations = _ReservationBLL.GetAllActiveByClient(_client.Id);
                comboReservations.DataSource = reservations;
                comboReservations.DisplayMember = "Vehicle_Description";
                comboReservations.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Boolean ValidateInvoiceCreation()
        {
            if (comboReservations.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a reservation to continue.", "¡ATENTION!");
                return false;
            }
            if (tbCUIL_CUIT_Client.Text == "")
            {
                MessageBox.Show("Please write the client's CUIL/CUIT to continue.", "¡ATENTION!");
                return false;
            }
            if (tbRazonSocial.Text == "")
            {
                MessageBox.Show("Please write the client's Name/RazonSocial to continue.", "¡ATENTION!");
                return false;
            }
            return true;
        }
        #endregion

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (_invoice != null && _price_left >= 0)
                {
                    _invoice.Payment_Status = true;
                    SQLUpdateResult result = _InvoiceBLL.UpdateInvoice(_invoice);
                    if (result != null && result.sqlResult == SQLResultType.success)
                    {
                        PerformGenerateInvoicePDF(_client);
                        btnAddPayment.Enabled = false;
                        btnDeletePayment.Enabled = false;
                        InvoiceDetailsPanel.Enabled = false;
                        btnGenerateInvoice.Enabled = false;
                    }
                    else
                        return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clientView1_ClientFound(object sender, EventArgs e)
        {
            clientView1.Enabled=false;
            InvoiceDetailsPanel.Enabled = true; 
            btnAddPayment.Enabled = true; 
            btnDeletePayment.Enabled = true;
            _client = clientView1.client;

            PerformUpdateReservationCombo();

            if (comboReservations.Items.Count < 1)
            {
                MessageBox.Show("There are no valid pending reservations for this client.", "¡ATENTION!");
                this.Close();
            }
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (_invoice == null)
                {
                    if (!ValidateInvoiceCreation())
                        return;
                    Invoice invoice = new Invoice();
                    invoice.Id = Guid.NewGuid();
                    invoice.Vehicle_Id = ((Reservation)comboReservations.SelectedItem).Vehicle_Id;
                    invoice.Reservation_Id = ((Reservation)comboReservations.SelectedItem).Id;
                    invoice.Client_Id = ((Reservation)comboReservations.SelectedItem).Client_Id;
                    invoice.Detail = ((Reservation)comboReservations.SelectedItem).Vehicle_Description;
                    invoice.Amount = ((Reservation)comboReservations.SelectedItem).Price;
                    invoice.CUIL_CUIT_Client = tbCUIL_CUIT_Client.Text;
                    invoice.Razon_Social = tbRazonSocial.Text;
                    invoice.Payment_Status = false;
                    invoice.Creation_Date = DateTime.Now;
                    
                    SQLUpdateResult result = _InvoiceBLL.AddInvoice(invoice);
                    if (result != null && result.sqlResult == SQLResultType.success) 
                    {
                        _invoice = invoice;
                        comboReservations.Enabled = false;
                        btnGenerateInvoice.Enabled = true;
                        metroLabel1.Text = "Price Left to Pay: " + (0-invoice.Amount).ToString();
                    }
                    else
                        return;
                }

                AddPaymentForm frm = new AddPaymentForm(_invoice);
                frm.ShowDialog();
                if (frm._payment != null)
                {
                    PerformUpdatePaymentsView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (metroGrid1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = metroGrid1.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        Payment payment = (Payment)row.DataBoundItem;
                        SQLUpdateResult result = _PaymentBLL.DeletePayment(payment);

                        if (result != null && result.sqlResult == SQLResultType.success)
                        {
                            PerformUpdatePaymentsView();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void comboReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboReservations.Items.Count > 0)
            {
                if (comboReservations.SelectedIndex != -1)
                {
                    _reservation = (Reservation)comboReservations.SelectedItem;
                    lblPriceToPay.Text = "Price to Pay: " + ((Reservation)comboReservations.SelectedItem).Price.ToString();
                }
            }
        }
    }
}
