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
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI.Personas.Controls;
using Security.Session;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{
    public partial class GenerateFacturaForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        public Persona _person;
        Reserva _reservation;
        Factura _invoice;
        ReservaBLL _ReservationBLL;
        PagoBLL _PaymentBLL;
        FacturaBLL _InvoiceBLL;
        VehiculoBLL _VehicleBLL;
        List<Pago> payments;

        // Lo que falta pagar de la reserva: precio menos los pagos cargados. Cero o menos, ya se puede facturar.
        double _balance;

        public GenerateFacturaForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _ReservationBLL = new ReservaBLL();
            _PaymentBLL = new PagoBLL();
            _InvoiceBLL = new FacturaBLL();
            _VehicleBLL = new VehiculoBLL();


            this.personView1.PersonFound += new EventHandler(personView1_PersonFound);
            GridColumns.TranslateValues(metroGrid1, "TipoPago_Descripcion", LookupText.PaymentType);

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void GenerateInvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("GenerateFacturaForm");
            this.Refresh();
            tbCUIL_CUIT_Person.WaterMark = LanguageService.GetTagText(tbCUIL_CUIT_Person.Tag.ToString());
            tbRazonSocial.WaterMark = LanguageService.GetTagText(tbRazonSocial.Tag.ToString());
            btnAddPayment.Text = LanguageService.GetTagText(btnAddPayment.Tag.ToString());
            btnDeletePayment.Text = LanguageService.GetTagText(btnDeletePayment.Tag.ToString());
            btnGenerateInvoice.Text = LanguageService.GetTagText(btnGenerateInvoice.Tag.ToString());
            if (_reservation != null)
            {
                ShowBalance();
                lblPriceToPay.Text = LanguageService.GetTagText("PriceToPay").Trim() + " $ " + _reservation.Precio.ToString("N2");
            }
            else 
            {
                metroLabel1.Text = LanguageService.GetTagText("PriceLeftToPay");
                lblPriceToPay.Text = LanguageService.GetTagText("PriceToPay");
            }
            GridColumns.Translate(metroGrid1);
        }
        void ShowBalance()
        {
            metroLabel1.Text = LanguageService.GetTagText("PriceLeftToPay").Trim() + " $ " + Math.Max(_balance, 0).ToString("N2");
        }
        void PerformUpdatePaymentsView()
        {
            try
            {
                payments = _PaymentBLL.GetAllByInvoice(_invoice.Factura_Id);
                metroGrid1.DataSource = null;
                metroGrid1.DataSource = payments;
                GridColumns.Show(metroGrid1, "TipoPago_Descripcion", "Monto", "Detalle");
                _balance = PagoBLL.Balance(_reservation.Precio, payments);
                ShowBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        void PerformGenerateInvoicePDF(Persona q_person)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "CarAgency® " + LanguageService.GetTagText("InvoiceFor").Trim() + " " + q_person.Nombre + " " + q_person.Apellido;
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
                tf.DrawString(LanguageService.GetTagText("InvoiceFor").Trim() + " " + tbRazonSocial.Text, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                rect = new XRect(25, 95, page.Width.Point-20, page.Height.Point-20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(LanguageService.GetTagText("CUILCUIT").Trim() + ": " + tbCUIL_CUIT_Person.Text, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                int rectplace = 175;

                Vehiculo vehicle = _VehicleBLL.GetById(_reservation.Vehiculo_Id);

                rect = new XRect(25, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(LanguageService.GetTagText("Vehicle").Trim() + " " + vehicle.Anio + " " + vehicle.Marca_Descripcion + " " + vehicle.Modelo_Descripcion + " " + vehicle.Version_Descripcion + " . " + LanguageService.GetTagText("lblColour") + " " + LookupText.Colour(vehicle.Color_Descripcion), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                rectplace += 25;

                rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("                  " + LanguageService.GetTagText("LicensePlate").Trim() + " " + vehicle.Patente + ". KM's:" + vehicle.Kilometraje.ToString() + ". " , fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString(LanguageService.GetTagText("Price").Trim() + " " + _invoice.Valor.ToString("N2"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                rectplace += 425;

                rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("TOTAL $ " + _invoice.Valor.ToString("N2"), fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            
                rectplace += 25;

                rect = new XRect(0, rectplace, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

                string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Invoice " + q_person.Apellido + " " + q_person.Nombre + ".pdf";
                document.Close();
                document.Save(filename);
                CarAgency.Security.BitacoraEventos.BitacoraEventosBLL.Record(CarAgency.BE.BitacoraEventos.TipoEvento.InvoiceExported, _invoice.Factura_Id);


                Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateReservationCombo()
        {
            try
            {
                List<Reserva> reservations = _ReservationBLL.GetAllActiveByPerson(_person.Persona_Id);
                // Con una sola reserva activa no hay nada que elegir: viene seleccionada.
                comboReservations.DataSource = reservations;
                comboReservations.DisplayMember = "Vehiculo_Descripcion";
                comboReservations.SelectedIndex = reservations.Count == 1 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private Boolean ValidateInvoiceCreation()
        {
            if (comboReservations.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectReservation"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbCUIL_CUIT_Person.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteCuitCuil"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbRazonSocial.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteRazonSocial"), LanguageService.GetTagText("AttentionTitle"));
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
                // Sin pagos todavia no hay factura cargada; con saldo pendiente no se puede emitir.
                if (_invoice == null)
                {
                    MessageBox.Show(LanguageService.GetTagText("InvoiceNeedsPayment"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                if (_balance > PagoBLL.AmountTolerance)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("InvoiceBalancePending"), _balance.ToString("N2")), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                _invoice.EstadoPago = true;
                SQLUpdateResult result = _InvoiceBLL.UpdateInvoice(_invoice);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    PerformGenerateInvoicePDF(_person);
                    btnAddPayment.Enabled = false;
                    btnDeletePayment.Enabled = false;
                    InvoiceDetailsPanel.Enabled = false;
                    btnGenerateInvoice.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        public void personView1_PersonFound(object sender, EventArgs e)
        {
            personView1.Enabled=false;
            InvoiceDetailsPanel.Enabled = true; 
            btnAddPayment.Enabled = true; 
            btnDeletePayment.Enabled = true;
            _person = personView1.person;

            PerformUpdateReservationCombo();

            if (comboReservations.Items.Count < 1)
            {
                MessageBox.Show(LanguageService.GetTagText("NoPendingReservations"), LanguageService.GetTagText("AttentionTitle"));
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
                    Factura invoice = new Factura();
                    invoice.Factura_Id = Guid.NewGuid();
                    invoice.Vehiculo_Id = ((Reserva)comboReservations.SelectedItem).Vehiculo_Id;
                    invoice.Reserva_Id = ((Reserva)comboReservations.SelectedItem).Reserva_Id;
                    invoice.Persona_Id = ((Reserva)comboReservations.SelectedItem).Persona_Id;
                    invoice.Detalle = ((Reserva)comboReservations.SelectedItem).Vehiculo_Descripcion;
                    invoice.Valor = ((Reserva)comboReservations.SelectedItem).Precio;
                    invoice.CuilCuit = tbCUIL_CUIT_Person.Text;
                    invoice.RazonSocial = tbRazonSocial.Text;
                    invoice.EstadoPago = false;
                    invoice.Fecha = DateTime.Now;
                    
                    SQLUpdateResult result = _InvoiceBLL.AddInvoice(invoice);
                    if (result != null && result.sqlResult == SQLResultType.success) 
                    {
                        _invoice = invoice;
                        comboReservations.Enabled = false;
                        btnGenerateInvoice.Enabled = true;
                        _balance = invoice.Valor;
                        ShowBalance();
                    }
                    else
                        return;
                }

                if (_balance <= PagoBLL.AmountTolerance)
                {
                    MessageBox.Show(LanguageService.GetTagText("InvoiceAlreadyPaid"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                AddPagoForm frm = new AddPagoForm(_invoice);
                frm.ShowDialog();
                if (frm._payment != null)
                {
                    PerformUpdatePaymentsView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
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
                        Pago payment = (Pago)row.DataBoundItem;
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
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
            
        }

        private void comboReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboReservations.Items.Count > 0)
            {
                if (comboReservations.SelectedIndex != -1)
                {
                    _reservation = (Reserva)comboReservations.SelectedItem;
                    lblPriceToPay.Text = LanguageService.GetTagText("PriceToPay").Trim() + " $ " + _reservation.Precio.ToString("N2");
                    // La factura se crea con el primer pago: hasta entonces falta pagar todo.
                    if (_invoice == null)
                    {
                        _balance = _reservation.Precio;
                        ShowBalance();
                    }
                }
            }
        }
    }
}
