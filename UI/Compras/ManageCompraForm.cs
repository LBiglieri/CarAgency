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
using UI.Compras.Controls;
using Security.Session;
using UI.Ayuda;
using UI.Comun;

namespace UI.Vehiculos
{
    public partial class ManageCompraForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        CompraBLL _AcquisitionBLL;
        TasacionBLL _AppraisalBLL;
        DocumentoCompraBLL _DocumentBLL;
        PagoCompraBLL _PurchasePaymentBLL;
        PersonaBLL _PeopleBLL;
        List<PendingCompra> pending_acquisitions;
        PendingCompra pending;
        Compra acquisition;
        Tasacion appraisal;
        Persona person;
        List<FormaPagoCompra> payment_terms = new List<FormaPagoCompra>();
        // El acuerdo en pantalla es el que esta guardado: recien ahi se puede registrar la firma.
        bool agreement_registered;
        MetroFramework.Controls.MetroTabPage stage_tab;
        List<MetroFramework.Controls.MetroTabPage> previous_tabs = new List<MetroFramework.Controls.MetroTabPage>();

        public ManageCompraForm()
        {
            InitializeComponent();
            AyudaButton.Add(this);
            // El Designer no guarda AutoGenerateColumns: la grilla usa las columnas definidas ahi.
            gridPending.AutoGenerateColumns = false;
            _AcquisitionBLL = new CompraBLL();
            _AppraisalBLL = new TasacionBLL();
            _DocumentBLL = new DocumentoCompraBLL();
            _PurchasePaymentBLL = new PagoCompraBLL();
            _PeopleBLL = new PersonaBLL();
            documentsView1.Stage = DocumentoCompraStage.Document;
            transferDocumentsView1.Stage = DocumentoCompraStage.Transfer;

            PerformUpdatePaymentTypesCombo();
            PerformUpdatePendingView(null);

            LanguageService.Attach(this);
            UpdateLanguage("");
        }

        private void ManageAcquisitionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            this.Text = LanguageService.GetTagText("ManageCompraForm");
            this.Refresh();
            lblPendingAcquisitions.Text = LanguageService.GetTagText(lblPendingAcquisitions.Tag.ToString());
            tbFilter.WaterMark = LanguageService.GetTagText(tbFilter.Tag.ToString());
            btnSearch.Text = LanguageService.GetTagText(btnSearch.Tag.ToString());
            colNumber.HeaderText = LanguageService.GetTagText(colNumber.Tag.ToString());
            colDni.HeaderText = LanguageService.GetTagText(colDni.Tag.ToString());
            colLicense_Plate.HeaderText = LanguageService.GetTagText(colLicense_Plate.Tag.ToString());
            colStatus.HeaderText = LanguageService.GetTagText(colStatus.Tag.ToString());
            lblAcquisitionSummary.Text = LanguageService.GetTagText(lblAcquisitionSummary.Tag.ToString());
            tabInspection.Text = LanguageService.GetTagText(tabInspection.Tag.ToString());
            tabFormalize.Text = LanguageService.GetTagText(tabFormalize.Tag.ToString());
            tabDocuments.Text = LanguageService.GetTagText(tabDocuments.Tag.ToString());
            tabTransfer.Text = LanguageService.GetTagText(tabTransfer.Tag.ToString());
            tabFinish.Text = LanguageService.GetTagText(tabFinish.Tag.ToString());
            lblAgreement.Text = LanguageService.GetTagText(lblAgreement.Tag.ToString());
            tbAgreed_Price.WaterMark = LanguageService.GetTagText(tbAgreed_Price.Tag.ToString());
            tbOwner_CUIL_CUIT.WaterMark = LanguageService.GetTagText(tbOwner_CUIL_CUIT.Tag.ToString());
            lblPaymentTerms.Text = LanguageService.GetTagText(lblPaymentTerms.Tag.ToString());
            tbTermAmount.WaterMark = LanguageService.GetTagText(tbTermAmount.Tag.ToString());
            btnAddTerm.Text = LanguageService.GetTagText(btnAddTerm.Tag.ToString());
            btnRemoveTerm.Text = LanguageService.GetTagText(btnRemoveTerm.Tag.ToString());
            colPaymentType.HeaderText = LanguageService.GetTagText(colPaymentType.Tag.ToString());
            colTermAmount.HeaderText = LanguageService.GetTagText(colTermAmount.Tag.ToString());
            btnRejectAppraisal.Text = LanguageService.GetTagText(btnRejectAppraisal.Tag.ToString());
            btnGenerateContract.Text = LanguageService.GetTagText(btnGenerateContract.Tag.ToString());
            btnContractNotSigned.Text = LanguageService.GetTagText(btnContractNotSigned.Tag.ToString());
            btnContractSigned.Text = LanguageService.GetTagText(btnContractSigned.Tag.ToString());
            lblTransfer.Text = LanguageService.GetTagText(lblTransfer.Tag.ToString());
            tbPaperwork_Precharge_Code.WaterMark = LanguageService.GetTagText(tbPaperwork_Precharge_Code.Tag.ToString());
            lblTransferAppointment.Text = LanguageService.GetTagText(lblTransferAppointment.Tag.ToString());
            btnScheduleTransfer.Text = LanguageService.GetTagText(btnScheduleTransfer.Tag.ToString());
            btnRegisterTransfer.Text = LanguageService.GetTagText(btnRegisterTransfer.Tag.ToString());
            lblFinishSummary.Text = LanguageService.GetTagText(lblFinishSummary.Tag.ToString());
            tbSale_Price.WaterMark = LanguageService.GetTagText(tbSale_Price.Tag.ToString());
            tbOpcionals.WaterMark = LanguageService.GetTagText(tbOpcionals.Tag.ToString());
            tbVehicleObservations.WaterMark = LanguageService.GetTagText(tbVehicleObservations.Tag.ToString());
            btnFinishAcquisition.Text = LanguageService.GetTagText(btnFinishAcquisition.Tag.ToString());
            inspectionView1.UpdateLanguage(language);
            documentsView1.UpdateLanguage(language);
            transferDocumentsView1.UpdateLanguage(language);
            PerformUpdatePaymentTypesCombo();
            PerformUpdateTermsView();
            gridPending.Refresh();
            MapAcquisitionToControls();
            MapTransferToControls();
            MapSummaryToControls();
        }
        void PerformUpdatePendingView(Guid? Acquisition_Id)
        {
            try
            {
                pending_acquisitions = _AcquisitionBLL.GetPendingAcquisitions(tbFilter.Text);
                // La compra que se estaba gestionando pudo cambiar de estado: se vuelve a cargar.
                pending = null;
                gridPending.DataSource = null;
                gridPending.DataSource = pending_acquisitions;
                if (Acquisition_Id != null)
                {
                    foreach (DataGridViewRow row in gridPending.Rows)
                    {
                        if (((PendingCompra)row.DataBoundItem).Compra_Id == Acquisition_Id.Value)
                        {
                            gridPending.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
                PerformSelectAcquisition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdatePaymentTypesCombo()
        {
            try
            {
                // Bank Transfer, Check y Cash, con su traduccion. Al cambiar de idioma se conserva el elegido.
                Guid? selected = comboPaymentType.SelectedIndex == -1 ? (Guid?)null : ((KeyValuePair<TipoPago, string>)comboPaymentType.SelectedItem).Key.TipoPago_Id;
                List<KeyValuePair<TipoPago, string>> payment_types = _PurchasePaymentBLL.GetAcquisitionPaymentTypes()
                    .Select(t => new KeyValuePair<TipoPago, string>(t, LookupText.PaymentType(t.Descripcion))).ToList();
                comboPaymentType.DataSource = payment_types;
                comboPaymentType.DisplayMember = "Value";
                comboPaymentType.ValueMember = "Key";
                if (selected == null)
                    PerformSelectDefaultPaymentType();
                else
                    comboPaymentType.SelectedIndex = payment_types.FindIndex(t => t.Key.TipoPago_Id == selected.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // Viene elegido el medio mas comun, salvo que ya este en la forma de pago: no se puede repetir.
        private void PerformSelectDefaultPaymentType()
        {
            List<KeyValuePair<TipoPago, string>> payment_types = comboPaymentType.DataSource as List<KeyValuePair<TipoPago, string>>;
            if (payment_types == null)
                return;
            int index = payment_types.FindIndex(t => t.Key.Descripcion == PagoCompraBLL.DefaultPaymentType);
            if (index != -1 && payment_terms.Any(t => t.TipoPago_Id == payment_types[index].Key.TipoPago_Id))
                index = -1;
            comboPaymentType.SelectedIndex = index;
        }
        private void PerformSelectAcquisition()
        {
            try
            {
                PendingCompra selected = gridPending.CurrentRow == null ? null : (PendingCompra)gridPending.CurrentRow.DataBoundItem;
                if (selected == null)
                {
                    ClearAcquisition();
                    return;
                }
                if (pending != null && pending.Compra_Id == selected.Compra_Id)
                    return;

                ClearAcquisition();
                pending = selected;
                acquisition = _AcquisitionBLL.GetAcquisition(pending.Compra_Id);
                appraisal = _AppraisalBLL.GetLastAppraisal(pending.Compra_Id);
                person = _PeopleBLL.GetById(pending.Persona_Id);
                inspectionView1.InitWithInspection(pending.Inspeccion_Id);
                PerformInitStage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        // CUF10 paso 2: solo se gestiona la etapa que corresponde al estado de la compra;
        // las etapas ya cumplidas se pueden consultar sin modificarlas.
        private void PerformInitStage()
        {
            stage_tab = null;
            previous_tabs = new List<MetroFramework.Controls.MetroTabPage>();
            switch (acquisition.Estado)
            {
                case CompraStatus.Appraised:
                    // 3.1: con la tasacion vencida no se puede seguir.
                    if (!_AppraisalBLL.IsAppraisalExpired(appraisal))
                        stage_tab = tabFormalize;
                    break;
                case CompraStatus.PurchaseFormalized:
                    stage_tab = tabDocuments;
                    previous_tabs.Add(tabFormalize);
                    break;
                case CompraStatus.DocumentationComplete:
                    previous_tabs.Add(tabFormalize);
                    previous_tabs.Add(tabDocuments);
                    break;
                case CompraStatus.Paid:
                    stage_tab = tabTransfer;
                    previous_tabs.Add(tabFormalize);
                    previous_tabs.Add(tabDocuments);
                    break;
                case CompraStatus.Transferred:
                    stage_tab = tabFinish;
                    previous_tabs.Add(tabFormalize);
                    previous_tabs.Add(tabDocuments);
                    previous_tabs.Add(tabTransfer);
                    break;
            }
            if (stage_tab == tabFormalize || previous_tabs.Contains(tabFormalize))
                MapAgreementToControls();
            if (stage_tab == tabDocuments || previous_tabs.Contains(tabDocuments))
                documentsView1.InitWithAcquisition(acquisition.Compra_Id, stage_tab == tabDocuments);
            if (stage_tab == tabTransfer || previous_tabs.Contains(tabTransfer))
            {
                transferDocumentsView1.InitWithAcquisition(acquisition.Compra_Id, stage_tab == tabTransfer);
                MapAppointmentToControls();
            }
            EnableFormalizeControls(stage_tab == tabFormalize);
            EnableTransferControls(stage_tab == tabTransfer);
            MapAcquisitionToControls();
            MapTransferToControls();
            MapSummaryToControls();
            tabStages.SelectedTab = stage_tab == null ? tabInspection : stage_tab;
        }
        private void MapAppointmentToControls()
        {
            tbPaperwork_Precharge_Code.Text = acquisition.CodigoPrecarga08;
            if (acquisition.FechaCitaTransferencia == null)
                dtTransfer_Appointment_Date.Checked = false;
            else
            {
                dtTransfer_Appointment_Date.Checked = true;
                dtTransfer_Appointment_Date.Value = acquisition.FechaCitaTransferencia.Value;
            }
        }
        // Una etapa ya cumplida se muestra con sus datos pero sin acciones.
        private void EnableFormalizeControls(bool editable)
        {
            tbAgreed_Price.Enabled = editable;
            tbOwner_CUIL_CUIT.Enabled = editable;
            comboPaymentType.Enabled = editable;
            tbTermAmount.Enabled = editable;
            btnAddTerm.Enabled = editable;
            btnRemoveTerm.Enabled = editable;
            btnRejectAppraisal.Enabled = editable;
            btnGenerateContract.Enabled = editable;
            UpdateAgreementButtons();
        }
        private void EnableTransferControls(bool editable)
        {
            tbPaperwork_Precharge_Code.Enabled = editable;
            dtTransfer_Appointment_Date.Enabled = editable;
            btnScheduleTransfer.Enabled = editable;
            btnRegisterTransfer.Enabled = editable;
        }
        private void ClearAcquisition()
        {
            pending = null;
            acquisition = null;
            appraisal = null;
            person = null;
            stage_tab = null;
            previous_tabs = new List<MetroFramework.Controls.MetroTabPage>();
            payment_terms = new List<FormaPagoCompra>();
            inspectionView1.Clear();
            documentsView1.Clear();
            transferDocumentsView1.Clear();
            tbAgreed_Price.Text = "";
            tbOwner_CUIL_CUIT.Text = "";
            tbTermAmount.Text = "";
            PerformSelectDefaultPaymentType();
            tbPaperwork_Precharge_Code.Text = "";
            dtTransfer_Appointment_Date.Checked = false;
            tbSale_Price.Text = "";
            tbOpcionals.Text = "";
            tbVehicleObservations.Text = "";
            agreement_registered = false;
            PerformUpdateTermsView();
            UpdateAgreementButtons();
            MapAcquisitionToControls();
            MapTransferToControls();
            MapSummaryToControls();
            tabStages.SelectedTab = tabInspection;
        }
        private void MapAcquisitionToControls()
        {
            if (acquisition == null || pending == null || person == null)
            {
                lblSummaryPerson.Text = "";
                lblSummaryVehicle.Text = "";
                lblSummaryAppraisal.Text = "";
                lblSummaryStatus.Text = "";
                lblStageMessage.Text = "";
                return;
            }
            lblSummaryPerson.Text = LanguageService.GetTagText("lblNameSurname") + " " + person.Nombre + " " + person.Apellido
                + " - " + LanguageService.GetTagText("lblDNI") + " " + person.DNI;
            lblSummaryVehicle.Text = pending.Vehiculo_Descripcion + " - " + pending.Patente;
            lblSummaryAppraisal.Text = appraisal == null ? "" : string.Format(LanguageService.GetTagText("SummaryAppraisal"),
                appraisal.Valor.ToString("N2"), appraisal.Vigencia.ToString("dd/MM/yyyy"));
            lblSummaryStatus.Text = LanguageService.GetTagText("SummaryStatus") + GetStatusDescription(acquisition.Estado);
            // 3.1 y 2.1: la compra no tiene etapa para gestionar.
            if (acquisition.Estado == CompraStatus.Appraised && _AppraisalBLL.IsAppraisalExpired(appraisal))
                lblStageMessage.Text = LanguageService.GetTagText("AppraisalExpired");
            else if (acquisition.Estado == CompraStatus.DocumentationComplete)
                lblStageMessage.Text = LanguageService.GetTagText("AcquisitionWaitingPayment");
            else
                lblStageMessage.Text = "";
        }

        // Los estados y los medios de pago estan en ingles: se muestran con su traduccion.
        private string GetStatusDescription(CompraStatus status)
        {
            return LanguageService.GetTagText("AcquisitionStatus_" + status);
        }

        // Etapa A: si el acuerdo ya se registro se muestra el guardado; si no, el precio arranca en el valor tasado (paso 4).
        private void MapAgreementToControls()
        {
            bool registered = _AcquisitionBLL.HasAgreement(acquisition);
            if (registered)
            {
                tbAgreed_Price.Text = acquisition.PrecioAcordado.Value.ToString("0.00");
                tbOwner_CUIL_CUIT.Text = acquisition.CuilCuitTitular;
                List<FormaPagoCompra> terms = _PurchasePaymentBLL.GetPaymentTerms(acquisition.Compra_Id);
                payment_terms = terms == null ? new List<FormaPagoCompra>() : terms;
            }
            else
            {
                tbAgreed_Price.Text = appraisal.Valor.ToString("0.00");
                tbOwner_CUIL_CUIT.Text = "";
                payment_terms = new List<FormaPagoCompra>();
            }
            PerformUpdateTermsView();
            PerformSelectDefaultPaymentType();
            agreement_registered = registered;
            UpdateAgreementButtons();
        }
        private void PerformUpdateTermsView()
        {
            gridTerms.Rows.Clear();
            foreach (FormaPagoCompra term in payment_terms)
            {
                int index = gridTerms.Rows.Add(LookupText.PaymentType(term.TipoPago_Descripcion), term.Monto.ToString("N2"));
                gridTerms.Rows[index].Tag = term;
            }
            UpdateTermsDifference();
        }
        // CUF10 paso 6: la suma y la diferencia con el precio se ven mientras se carga la forma de pago.
        private void UpdateTermsDifference()
        {
            double price;
            if (!double.TryParse(tbAgreed_Price.Text, out price))
                price = 0;
            decimal difference = _PurchasePaymentBLL.GetPaymentTermsDifference(price, payment_terms);
            lblTermsDifference.Text = string.Format(LanguageService.GetTagText("PaymentTermsTotal"), payment_terms.Sum(t => t.Monto).ToString("N2"),
                difference.ToString("N2"));
        }
        private void PerformAgreementChanged()
        {
            agreement_registered = false;
            UpdateTermsDifference();
            UpdateAgreementButtons();
        }
        private void UpdateAgreementButtons()
        {
            btnContractSigned.Enabled = agreement_registered && stage_tab == tabFormalize;
            btnContractNotSigned.Enabled = agreement_registered && stage_tab == tabFormalize;
        }
        // CUF10 6.1: se indica la diferencia y se vuelve a la forma de pago.
        private bool ValidateAgreementControls()
        {
            double price;
            if (!double.TryParse(tbAgreed_Price.Text, out price) || price <= 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidAgreedPrice"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbOwner_CUIL_CUIT.Text.Trim() == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWriteOwnerCuilCuit"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (payment_terms.Count == 0)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseAddPaymentTerm"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            decimal difference = _PurchasePaymentBLL.GetPaymentTermsDifference(price, payment_terms);
            if (difference != 0)
            {
                MessageBox.Show(string.Format(LanguageService.GetTagText("PaymentTermsDifference"), difference.ToString("N2")), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }
        private Compra MapControlsToAgreement()
        {
            Compra agreement = new Compra();
            agreement.Compra_Id = acquisition.Compra_Id;
            agreement.PrecioAcordado = double.Parse(tbAgreed_Price.Text);
            agreement.CuilCuitTitular = tbOwner_CUIL_CUIT.Text.Trim();
            return agreement;
        }

        // Etapa C: titular y vehiculo de la transferencia.
        private void MapTransferToControls()
        {
            if (acquisition == null || pending == null || person == null)
            {
                lblTransferOwner.Text = "";
                lblTransferVehicle.Text = "";
                return;
            }
            lblTransferOwner.Text = LanguageService.GetTagText("TransferOwner") + person.Nombre + " " + person.Apellido
                + " - " + LanguageService.GetTagText("CUILCUIT") + " " + acquisition.CuilCuitTitular;
            lblTransferVehicle.Text = LanguageService.GetTagText("AppraisalVehicle") + pending.Vehiculo_Descripcion
                + " - " + LanguageService.GetTagText("LicensePlate") + " " + pending.Patente;
        }

        // Etapa D: resumen completo de la operacion (paso 20).
        private void MapSummaryToControls()
        {
            if (acquisition == null || person == null || acquisition.Estado != CompraStatus.Transferred)
            {
                tbSummary.Text = "";
                return;
            }
            tbSummary.Text = string.Join(Environment.NewLine, GetSummaryLines(_AcquisitionBLL.GetSummary(acquisition.Compra_Id)));
        }
        // Las mismas lineas se muestran en la etapa D y se imprimen en la constancia.
        private List<string> GetSummaryLines(CompraSummary summary)
        {
            List<string> lines = new List<string>();
            lines.Add(LanguageService.GetTagText("AppraisalAcquisitionNumber") + summary.Numero + ". " + LanguageService.GetTagText("SummaryStatus")
                + GetStatusDescription(acquisition.Estado));
            lines.Add(LanguageService.GetTagText("SummaryPerson") + person.Nombre + " " + person.Apellido + ". " + LanguageService.GetTagText("lblDNI") + " " + person.DNI);
            lines.Add(LanguageService.GetTagText("AppraisalVehicle") + summary.Vehiculo_Descripcion + ". " + LanguageService.GetTagText("LicensePlate") + " " + summary.Patente);
            lines.Add(LanguageService.GetTagText("Chassis") + summary.NumeroChasis + " - " + LanguageService.GetTagText("Engine") + summary.NumeroMotor);
            lines.Add(LanguageService.GetTagText("InspectionPerformedDate") + summary.FechaRealizada.Value.ToString("dd/MM/yyyy")
                + ". " + LanguageService.GetTagText("VerifiedKilometers") + summary.KilometrajeVerificado
                + ". " + LanguageService.GetTagText("lblResult") + " " + inspectionView1.GetResultDescription());
            lines.Add(string.Format(LanguageService.GetTagText("SummaryAcceptedAppraisal"), summary.Tasacion_Valor.Value.ToString("N2"),
                summary.Tasacion_Fecha.Value.ToString("dd/MM/yyyy")));
            lines.Add(string.Format(LanguageService.GetTagText("SummaryAgreement"), summary.PrecioAcordado.Value.ToString("N2"), summary.CuilCuitTitular,
                summary.FechaFirmaContrato.Value.ToString("dd/MM/yyyy")));
            lines.Add(LanguageService.GetTagText("SummaryPaymentTerms"));
            List<FormaPagoCompra> terms = _PurchasePaymentBLL.GetPaymentTerms(summary.Compra_Id);
            if (terms != null)
                foreach (FormaPagoCompra term in terms)
                    lines.Add("      " + string.Format(LanguageService.GetTagText("SummaryPaymentTerm"), LookupText.PaymentType(term.TipoPago_Descripcion),
                        term.Monto.ToString("N2"), term.MontoPagado.ToString("N2")));
            lines.Add(string.Format(LanguageService.GetTagText("SummaryPaid"), summary.MontoPagado.ToString("N2")));
            if (summary.ComprobanteCompra_Numero != null)
                lines.Add(string.Format(LanguageService.GetTagText("SummaryReceipt"), summary.ComprobanteCompra_Numero.Value,
                    summary.ComprobanteCompra_Fecha.Value.ToString("dd/MM/yyyy")));
            lines.Add(string.Format(LanguageService.GetTagText("SummaryTransfer"), summary.CodigoPrecarga08,
                summary.FechaCitaTransferencia.Value.ToString("dd/MM/yyyy")));
            lines.Add(LanguageService.GetTagText("SummaryDocuments"));
            foreach (DocumentoCompra document in _DocumentBLL.GetDocuments(summary.Compra_Id))
                lines.Add("      " + string.Format(LanguageService.GetTagText("SummaryDocument"), DocumentosCompraView.GetDocumentTypeDescription(document.Descripcion),
                    document.FechaEmision.ToString("dd/MM/yyyy")));
            return lines;
        }

        // Contrato de compraventa (CUF10 paso 7): dos paginas iguales, original y duplicado, para que firmen las dos partes.
        // Se arma con el acuerdo guardado cada vez que se genera.
        void PerformGenerateContractPDF()
        {
            InspeccionDetail detail = inspectionView1.inspection_detail;
            Inspeccion inspection = inspectionView1.inspection;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency® " + LanguageService.GetTagText("PurchaseContractFor") + person.Nombre + " " + person.Apellido;

            XFont fontTitle = new XFont("Verdana", 32);
            XFont fontSubTitle = new XFont("Verdana", 20);
            XFont fontSection = new XFont("Verdana", 14);
            XFont fontBody = new XFont("Verdana", 10);

            foreach (string copy in new[] { "ContractOriginal", "ContractDuplicate" })
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);

                XRect rect = new XRect(25, 0, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Center;
                tf.DrawString("CarAgency®", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                rect = new XRect(25, 65, page.Width.Point - 20, page.Height.Point - 20);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(LanguageService.GetTagText("PurchaseContractFor") + person.Nombre + " " + person.Apellido, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString(LanguageService.GetTagText(copy), fontSection, XBrushes.Black, new XRect(0, 100, page.Width.Point - 25, 20), XStringFormats.TopLeft);
                tf.Alignment = XParagraphAlignment.Left;

                int rectplace = 125;
                tf.DrawString(LanguageService.GetTagText("AppraisalAcquisitionNumber") + acquisition.Numero + ". " + LanguageService.GetTagText("ContractDate") + DateTime.Now.ToString("dd/MM/yyyy"),
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractSeller"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("lblNameSurname") + " " + person.Nombre + " " + person.Apellido + ". " + LanguageService.GetTagText("lblDNI") + " " + person.DNI
                    + ". " + LanguageService.GetTagText("CUILCUIT") + " " + acquisition.CuilCuitTitular,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractBuyer"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString("CarAgency®", fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractVehicle"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("AppraisalVehicle") + detail.Vehiculo_Descripcion + ". " + LanguageService.GetTagText("LicensePlate") + " " + detail.Patente,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("Chassis") + detail.NumeroChasis + " - " + LanguageService.GetTagText("Engine") + detail.NumeroMotor
                    + " - " + LanguageService.GetTagText("VerifiedKilometers") + inspection.KilometrajeVerificado,
                    fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);

                // Precio y forma de pago pactada.
                rectplace += 35;
                tf.DrawString(LanguageService.GetTagText("ContractPriceSection"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("ContractAgreedPrice") + acquisition.PrecioAcordado.Value.ToString("N2"), fontBody, XBrushes.Black,
                    new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                foreach (FormaPagoCompra term in payment_terms)
                {
                    tf.DrawString("                  " + LookupText.PaymentType(term.TipoPago_Descripcion) + ": $ " + term.Monto.ToString("N2"),
                        fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                    rectplace += 18;
                }

                rectplace += 25;
                tf.DrawString(LanguageService.GetTagText("ContractConditions"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 25;
                // Las condiciones ocupan varias lineas: se cortan antes de las firmas.
                tf.DrawString(LanguageService.GetTagText("ContractClauses"), fontBody, XBrushes.Black,
                    new XRect(25, rectplace, page.Width.Point - 70, page.Height.Point - rectplace - 140), XStringFormats.TopLeft);

                gfx.DrawLine(XPens.Black, 60, page.Height.Point - 110, 250, page.Height.Point - 110);
                gfx.DrawLine(XPens.Black, page.Width.Point - 250, page.Height.Point - 110, page.Width.Point - 60, page.Height.Point - 110);
                tf.DrawString(LanguageService.GetTagText("ContractSellerSignature"), fontBody, XBrushes.Black,
                    new XRect(60, page.Height.Point - 105, 190, 20), XStringFormats.TopLeft);
                tf.DrawString(LanguageService.GetTagText("ContractBuyerSignature"), fontBody, XBrushes.Black,
                    new XRect(page.Width.Point - 250, page.Height.Point - 105, 190, 20), XStringFormats.TopLeft);

                rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            }

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Purchase Contract " + acquisition.Numero + " " + person.Apellido + " " + person.Nombre + ".pdf";
            document.Close();
            document.Save(filename);

            Process.Start(filename);
        }

        // Constancia de operacion finalizada (CUF10 paso 25), con el resumen de la etapa D y la fecha de cierre.
        void PerformGenerateCertificatePDF()
        {
            CompraSummary summary = _AcquisitionBLL.GetSummary(acquisition.Compra_Id);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "CarAgency® " + LanguageService.GetTagText("PurchaseCertificateFor") + person.Nombre + " " + person.Apellido;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            XFont fontTitle = new XFont("Verdana", 32);
            XFont fontSubTitle = new XFont("Verdana", 20);
            XFont fontSection = new XFont("Verdana", 14);
            XFont fontBody = new XFont("Verdana", 10);

            XRect rect = new XRect(25, 0, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString("CarAgency®", fontTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(25, 65, page.Width.Point - 20, page.Height.Point - 20);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(LanguageService.GetTagText("PurchaseCertificateFor") + person.Nombre + " " + person.Apellido, fontSubTitle, XBrushes.Black, rect, XStringFormats.TopLeft);

            int rectplace = 120;
            tf.DrawString(LanguageService.GetTagText("CertificateSummary"), fontSection, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
            rectplace += 30;
            foreach (string line in GetSummaryLines(summary))
            {
                tf.DrawString(line, fontBody, XBrushes.Black, new XRect(25, rectplace, page.Width.Point - 20, 20), XStringFormats.TopLeft);
                rectplace += 20;
            }

            rectplace += 25;
            tf.DrawString(string.Format(LanguageService.GetTagText("CertificateClosing"), summary.FechaCierre.Value.ToString("dd/MM/yyyy HH:mm")), fontBody, XBrushes.Black,
                new XRect(25, rectplace, page.Width.Point - 70, 40), XStringFormats.TopLeft);

            rect = new XRect(0, page.Height.Point - 45, page.Width.Point - 20, 25);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString("CarAgency " + DateTime.Now.ToString("yyyy") + "®", fontBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency Purchase Certificate " + acquisition.Numero + " " + person.Apellido + " " + person.Nombre + ".pdf";
            document.Close();
            document.Save(filename);

            Process.Start(filename);
        }

        // CUF10 paso 25: copias del formulario 08 firmado y de la baja municipal para entregar a la persona.
        // Se guardan en el Escritorio sin abrirlas.
        void PerformExportTransferCopies()
        {
            foreach (DocumentoCompra document in _DocumentBLL.GetDocuments(acquisition.Compra_Id).Where(d => d.Etapa == DocumentoCompraStage.Transfer))
            {
                ArchivoDocumentoCompra file = _DocumentBLL.GetDocumentFile(document.DocumentoCompra_Id);
                string filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CarAgency " + document.Descripcion + " " + acquisition.Numero
                    + " " + person.Apellido + " " + person.Nombre + Path.GetExtension(file.NombreArchivo);
                File.WriteAllBytes(filename, file.Contenido);
            }
        }
        #endregion

        private void gridPending_SelectionChanged(object sender, EventArgs e)
        {
            PerformSelectAcquisition();
        }

        private void gridPending_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridPending.Columns[e.ColumnIndex] == colStatus && e.Value is CompraStatus)
            {
                e.Value = GetStatusDescription((CompraStatus)e.Value);
                e.FormattingApplied = true;
            }
        }

        // Se entra a la etapa del estado actual y a las ya cumplidas; la inspeccion se puede consultar siempre.
        private void tabStages_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != tabInspection && e.TabPage != stage_tab && !previous_tabs.Contains(e.TabPage))
                e.Cancel = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformUpdatePendingView(null);
            if (pending_acquisitions == null && tbFilter.Text.Trim() != "")
                MessageBox.Show(LanguageService.GetTagText("NoPendingAcquisitionsFound"), LanguageService.GetTagText("AttentionTitle"));
        }

        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
        }

        private void tbAgreement_TextChanged(object sender, EventArgs e)
        {
            PerformAgreementChanged();
        }

        private void btnAddTerm_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                if (comboPaymentType.SelectedIndex == -1)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseSelectPaymentType"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                double amount;
                if (!double.TryParse(tbTermAmount.Text, out amount) || amount <= 0)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidPaymentAmount"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                TipoPago type = ((KeyValuePair<TipoPago, string>)comboPaymentType.SelectedItem).Key;
                if (payment_terms.Any(t => t.TipoPago_Id == type.TipoPago_Id))
                {
                    MessageBox.Show(LanguageService.GetTagText("PaymentTypeAlreadyAdded"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                FormaPagoCompra term = new FormaPagoCompra();
                term.TipoPago_Id = type.TipoPago_Id;
                term.TipoPago_Descripcion = type.Descripcion;
                term.Monto = amount;
                payment_terms.Add(term);
                tbTermAmount.Text = "";
                PerformSelectDefaultPaymentType();
                PerformUpdateTermsView();
                PerformAgreementChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnRemoveTerm_Click(object sender, EventArgs e)
        {
            if (gridTerms.CurrentRow == null)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectPaymentTerm"), LanguageService.GetTagText("AttentionTitle"));
                return;
            }
            payment_terms.Remove((FormaPagoCompra)gridTerms.CurrentRow.Tag);
            if (comboPaymentType.SelectedIndex == -1)
                PerformSelectDefaultPaymentType();
            PerformUpdateTermsView();
            PerformAgreementChanged();
        }

        private void btnGenerateContract_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null || !ValidateAgreementControls())
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureGenerateContract"), double.Parse(tbAgreed_Price.Text).ToString("N2"), pending.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterAgreement(MapControlsToAgreement(), payment_terms);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    acquisition = _AcquisitionBLL.GetAcquisition(acquisition.Compra_Id);
                    MapAgreementToControls();
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AgreementRegistered"), acquisition.Numero));
                    PerformGenerateContractPDF();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnContractSigned_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureContractSigned"), pending.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterContractSigned(acquisition.Compra_Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("PurchaseFormalized"), acquisition.Numero));
                    PerformUpdatePendingView(acquisition.Compra_Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnContractNotSigned_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureContractNotSigned"), pending.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterContractNotSigned(acquisition.Compra_Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("ContractNotSignedRegistered"), acquisition.Numero));
                    PerformUpdatePendingView(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // CUF10 3.3. Si la persona lo quiere pensar (3.2) no se registra nada: la compra sigue Tasada.
        private void btnRejectAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRejectAppraisal"), pending.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RejectAppraisal(acquisition.Compra_Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AppraisalRejectedRegistered"), acquisition.Numero));
                    PerformUpdatePendingView(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // CUF10 paso 15: con el ultimo documento cargado la compra pasa a Documentacion completa.
        private void documentsView1_DocumentsChanged(object sender, EventArgs e)
        {
            try
            {
                Compra current = _AcquisitionBLL.GetAcquisition(acquisition.Compra_Id);
                if (current.Estado != acquisition.Estado)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("DocumentationCompleted"), current.Numero));
                    PerformUpdatePendingView(current.Compra_Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnScheduleTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                if (tbPaperwork_Precharge_Code.Text.Trim() == "")
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWrite08"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                if (!dtTransfer_Appointment_Date.Checked)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseSelectTransferAppointment"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                Compra transfer = new Compra();
                transfer.Compra_Id = acquisition.Compra_Id;
                transfer.CodigoPrecarga08 = tbPaperwork_Precharge_Code.Text.Trim();
                transfer.FechaCitaTransferencia = dtTransfer_Appointment_Date.Value;
                SQLUpdateResult result = _AcquisitionBLL.ScheduleTransfer(transfer);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    acquisition = _AcquisitionBLL.GetAcquisition(acquisition.Compra_Id);
                    MessageBox.Show(string.Format(LanguageService.GetTagText("TransferScheduled"), acquisition.FechaCitaTransferencia.Value.ToString("dd/MM/yyyy")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnRegisterTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureRegisterTransfer"), pending.Patente), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.RegisterTransfer(acquisition.Compra_Id);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AcquisitionTransferredMessage"), acquisition.Numero));
                    PerformUpdatePendingView(acquisition.Compra_Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnFinishAcquisition_Click(object sender, EventArgs e)
        {
            try
            {
                if (acquisition == null)
                    return;
                double price;
                if (!double.TryParse(tbSale_Price.Text, out price) || price <= 0)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWriteValidSalePrice"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                // CUF10 21.1: vender por debajo de lo que se le pago a la persona se advierte y se confirma.
                if (price < acquisition.PrecioAcordado.Value && MessageBox.Show(string.Format(LanguageService.GetTagText("SalePriceBelowPurchasePrice"), price.ToString("N2"), acquisition.PrecioAcordado.Value.ToString("N2")), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureFinishAcquisition"), pending.Patente, price.ToString("N2")), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                Vehiculo sale = new Vehiculo();
                sale.Precio = price;
                sale.Opcionales = tbOpcionals.Text;
                sale.Observaciones = tbVehicleObservations.Text;
                SQLUpdateResult result = _AcquisitionBLL.FinishAcquisition(acquisition.Compra_Id, sale);
                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    acquisition = _AcquisitionBLL.GetAcquisition(acquisition.Compra_Id);
                    MessageBox.Show(string.Format(LanguageService.GetTagText("AcquisitionFinishedMessage"), acquisition.Numero));
                    PerformGenerateCertificatePDF();
                    PerformExportTransferCopies();
                    PerformUpdatePendingView(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
