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
using UI.Ayuda;
using UI.Comun;
//using static System.Net.Mime.MediaTypeNames;

namespace UI.Vehiculos
{
    public enum TramiteFormMode
    {
        Add,
        Update
    }
    public partial class TramiteForm : MetroFramework.Forms.MetroForm, ILanguageObserver
    {
        TramiteBLL _PaperworkBLL;
        FacturaBLL _InvoiceBLL;
        Persona person;
        Tramite paperwork;
        List<ArchivoTramite> paperwork_files;
        TramiteFormMode FormMode;

        public TramiteForm(Persona _person, Tramite _paperwork, TramiteFormMode mode)
        {
            InitializeComponent();
            AyudaButton.Add(this);
            _PaperworkBLL = new TramiteBLL();
            _InvoiceBLL = new FacturaBLL();

            if (_paperwork != null)
                paperwork = _paperwork;

            FormMode = mode;

            if (FormMode == TramiteFormMode.Add)
            {
                this.Text = LanguageService.GetTagText("btnAddPaperwork");
                comboInvoice.Enabled=true; 
            }
            if (FormMode == TramiteFormMode.Update)
                this.Text = LanguageService.GetTagText("btnEditPaperwork");

            personView1.personViewMode = PersonaViewMode.WithNoRegistration;
            this.personView1.PersonFound += new EventHandler(personView1_PersonFound);

            if (_person!= null)
                this.personView1.InitWithPerson(_person);

            if (paperwork != null)
            {
                MapPaperworkToControls();
                PerformUpdatePaperworkFilesView();
            }

            LanguageService.Attach(this);
            UpdateLanguage("");
        }
        private void PaperworkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LanguageService.Detach(this);
        }
        #region "Performs"
        public void UpdateLanguage(string language)
        {
            if (FormMode == TramiteFormMode.Add)
                this.Text = LanguageService.GetTagText("btnAddPaperwork");
            if (FormMode == TramiteFormMode.Update)
                this.Text = LanguageService.GetTagText("btnEditPaperwork");
            this.Refresh();
            tbPaperwork_Precharge_Code.WaterMark = LanguageService.GetTagText(tbPaperwork_Precharge_Code.Tag.ToString());
            tbObservations.WaterMark = LanguageService.GetTagText(tbObservations.Tag.ToString());
            lblReservation.Text = LanguageService.GetTagText(lblReservation.Tag.ToString());
            lblTransferDate.Text = LanguageService.GetTagText(lblTransferDate.Tag.ToString());
            btnAddFile.Text = LanguageService.GetTagText(btnAddFile.Tag.ToString());
            btnOpenFile.Text = LanguageService.GetTagText(btnOpenFile.Tag.ToString());
            btnDeleteFile.Text = LanguageService.GetTagText(btnDeleteFile.Tag.ToString());
            btnSavePaperwork.Text = LanguageService.GetTagText(btnSavePaperwork.Tag.ToString());
            btnFinishPaperwork.Text = LanguageService.GetTagText(btnFinishPaperwork.Tag.ToString());
            GridColumns.Translate(metroGrid1);
        }
        void PerformUpdatePaperworkFilesView()
        {
            try
            {
                if(paperwork != null)
                {
                    paperwork_files = _PaperworkBLL.GetFilesByPaperWork(paperwork.Tramite_Id);
                    metroGrid1.DataSource = null;
                    metroGrid1.DataSource = paperwork_files;
                    GridColumns.Show(metroGrid1, "NombreArchivo", "FechaCarga");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private void PerformUpdateInvoiceCombo()
        {
            try
            {
                List<Factura> invoices;
                if (FormMode==TramiteFormMode.Add)
                    invoices = _InvoiceBLL.GetAllPendingOfPaperworkByPerson(personView1.person.Persona_Id);
                else
                {
                    invoices = new List<Factura>();
                    invoices.Add(_InvoiceBLL.GetById(paperwork.Factura_Id));
                }
                comboInvoice.DataSource = invoices;
                comboInvoice.DisplayMember = "Vehiculo_Descripcion";

                // Con una sola factura pendiente no hay nada que elegir: viene seleccionada.
                if (FormMode == TramiteFormMode.Add && invoices.Count != 1)
                    comboInvoice.SelectedIndex = -1;
                else
                    comboInvoice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
        private bool ValidateControls()
        {
            if (comboInvoice.SelectedIndex == -1 && FormMode == TramiteFormMode.Add)
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectReservation"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (tbPaperwork_Precharge_Code.Text == "")
            {
                MessageBox.Show(LanguageService.GetTagText("PleaseWrite08"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }

        private bool ValidateFinishPaperwork()
        {
            if (!ValidateControls())
                return false;
            if (metroGrid1.Rows.Count == 0)
            {
                MessageBox.Show(LanguageService.GetTagText("AtLeast1FileToFinishPaperwork"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (!dtTransfer_Date.Checked)
            {
                MessageBox.Show(LanguageService.GetTagText("WriteTransferDate"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            if (dtTransfer_Date.Value == (new DateTime(1754, 1, 1, 12, 0, 0).Date))
            {
                MessageBox.Show(LanguageService.GetTagText("WriteTransferDate"), LanguageService.GetTagText("AttentionTitle"));
                return false;
            }
            return true;
        }

        private void MapPaperworkToControls()
        {
            if (paperwork != null)
            {
                if (paperwork.FechaTransferencia.Date == (new DateTime(1754, 1, 1, 12, 0, 0).Date))
                {
                    dtTransfer_Date.Value = new DateTime(1754, 1, 1, 12, 0, 0);
                    dtTransfer_Date.Checked = false;
                }
                else
                {
                    dtTransfer_Date.Checked = true;
                    dtTransfer_Date.Value = paperwork.FechaTransferencia;
                }
                tbPaperwork_Precharge_Code.Text = paperwork.CodigoPrecarga08;
                tbPaperwork_Precharge_Code.Text = paperwork.CodigoPrecarga08;
                tbObservations.Text = paperwork.Observaciones;
            }
        }
        private bool Save()
        {
            try
            {
                if (!ValidateControls())
                    return false;

                if (paperwork == null)
                {
                    Tramite _paperwork = new Tramite();
                    _paperwork.Tramite_Id = Guid.NewGuid();
                    _paperwork.Factura_Id = ((Factura)comboInvoice.SelectedItem).Factura_Id;
                    _paperwork.Vehiculo_Id = ((Factura)comboInvoice.SelectedItem).Vehiculo_Id;
                    _paperwork.Persona_Id = personView1.person.Persona_Id;
                    _paperwork.CodigoPrecarga08 = tbPaperwork_Precharge_Code.Text;
                    _paperwork.Observaciones = tbObservations.Text;
                    if (dtTransfer_Date.Checked)
                    {
                        _paperwork.FechaTransferencia = dtTransfer_Date.Value;
                    }
                    else
                    {
                        _paperwork.FechaTransferencia = new DateTime(1754, 1, 1, 12, 0, 0); 
                    }
                    _paperwork.Finalizado = false;

                    SQLUpdateResult result = _PaperworkBLL.AddPaperwork(_paperwork);
                    if (result != null && result.sqlResult == SQLResultType.success)
                    {
                        comboInvoice.Enabled =false;
                        paperwork = _paperwork;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (dtTransfer_Date.Checked)
                    {
                        paperwork.FechaTransferencia = dtTransfer_Date.Value;
                    }
                    else
                    {
                        paperwork.FechaTransferencia = new DateTime(1754, 1, 1, 12, 0, 0);
                    }
                    paperwork.CodigoPrecarga08 = tbPaperwork_Precharge_Code.Text;
                    paperwork.Observaciones = tbObservations.Text;

                    SQLUpdateResult result = _PaperworkBLL.UpdatePaperwork(paperwork);
                    if (result != null && result.sqlResult == SQLResultType.success)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
                return false;
            }
        }
        #endregion


        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnSavePaperwork_Click(object sender, EventArgs e)
        {
            try
            {
                if (Save())
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnFinishPaperwork_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFinishPaperwork())
                    return;

                if(MessageBox.Show(LanguageService.GetTagText("AreYouSureYouWantToFinishPaperwork"), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                paperwork.Finalizado = true;
                
                if (Save())
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        public void personView1_PersonFound(object sender, EventArgs e)
        {
            if (personView1.person != null)
            {
                person = personView1.person;
                panelPaperwokr.Enabled = true;
                PaperworkDetailsPanel.Enabled = true;
                metroPanel1.Enabled = true;
                btnSavePaperwork.Enabled = true;
                btnFinishPaperwork.Enabled = true;
                PerformUpdateInvoiceCombo();
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Save())
                    return;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = LanguageService.GetTagText("DocumentFileFilter");
                    openFileDialog.Title = LanguageService.GetTagText("SelectDocumentTitle");

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ArchivoTramite file = new ArchivoTramite();
                        file.ArchivoTramite_Id = Guid.NewGuid();
                        file.Tramite_Id = paperwork.Tramite_Id;
                        string filePath = openFileDialog.FileName;
                        file.NombreArchivo = Path.GetFileName(filePath);
                        file.Contenido = File.ReadAllBytes(filePath);
                        file.FechaCarga = DateTime.Now;
                        SQLUpdateResult result = _PaperworkBLL.AddPaperworkFile(file);

                        if (result != null && result.sqlResult == SQLResultType.success)
                        {
                            PerformUpdatePaperworkFilesView();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (metroGrid1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = metroGrid1.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        ArchivoTramite file = (ArchivoTramite)row.DataBoundItem;
                        SQLUpdateResult result = _PaperworkBLL.DeletePaperworkFile(file);

                        string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");
                        File.WriteAllBytes(tempFilePath, file.Contenido);
                        System.Diagnostics.Process.Start(tempFilePath);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (metroGrid1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection rows = metroGrid1.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        ArchivoTramite file = (ArchivoTramite)row.DataBoundItem;
                        SQLUpdateResult result = _PaperworkBLL.DeletePaperworkFile(file);

                        if (result != null && result.sqlResult == SQLResultType.success)
                        {
                            PerformUpdatePaperworkFilesView();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        private void PaperworkForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

    }
}
