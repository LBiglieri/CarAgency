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
//using static System.Net.Mime.MediaTypeNames;

namespace UI.Vehicles
{
    public enum PaperworkFormMode
    {
        Add,
        Update
    }
    public partial class PaperworkForm : MetroFramework.Forms.MetroForm
    {
        PaperworkBLL _PaperworkBLL;
        InvoiceBLL _InvoiceBLL;
        Client client;
        Paperwork paperwork;
        List<PaperworkFile> paperwork_files;
        PaperworkFormMode FormMode;

        public PaperworkForm(Client _client, Paperwork _paperwork, PaperworkFormMode mode)
        {
            InitializeComponent();
            _PaperworkBLL = new PaperworkBLL();
            _InvoiceBLL = new InvoiceBLL();

            if (_paperwork != null)
                paperwork = _paperwork;

            FormMode = mode;

            if (FormMode == PaperworkFormMode.Add)
            {
                this.Text = "Add Paperwork";
                comboInvoice.Enabled=true; 
            }
            if (FormMode == PaperworkFormMode.Update)
                this.Text = "Edit Paperwork";

            clientView1.clientViewMode = ClientViewMode.WithNoRegistration;
            this.clientView1.ClientFound += new EventHandler(clientView1_ClientFound);

            if (_client!= null)
                this.clientView1.InitWithClient(_client);

            if (paperwork != null)
            {
                MapPaperworkToControls();
                PerformUpdatePaperworkFilesView();
            }
        }
        #region "Performs"
        void PerformUpdatePaperworkFilesView()
        {
            try
            {
                if(paperwork != null)
                {
                    paperwork_files = _PaperworkBLL.GetFilesByPaperWork(paperwork.Id);
                    metroGrid1.DataSource = null;
                    metroGrid1.DataSource = paperwork_files;
                    if (paperwork_files != null && paperwork_files.Count > 0)
                    {
                        metroGrid1.Columns["Id"].Visible = false;
                        metroGrid1.Columns["Paperwork_Id"].Visible = false;
                        metroGrid1.Columns["FileContent"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PerformUpdateInvoiceCombo()
        {
            try
            {
                List<Invoice> invoices;
                if (FormMode==PaperworkFormMode.Add)
                    invoices = _InvoiceBLL.GetAllPendingOfPaperworkByClient(clientView1.client.Id);
                else
                {
                    invoices = new List<Invoice>();
                    invoices.Add(_InvoiceBLL.GetById(paperwork.Invoice_Id));
                }
                comboInvoice.DataSource = invoices;
                comboInvoice.DisplayMember = "Vehicle_Description";

                if (FormMode == PaperworkFormMode.Add)
                    comboInvoice.SelectedIndex = -1;
                else
                    comboInvoice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateControls()
        {
            if (comboInvoice.SelectedIndex == -1 && FormMode == PaperworkFormMode.Add)
            {
                MessageBox.Show("Please select a reservation to continue.", "¡ATENTION!");
                return false;
            }
            if (tbPaperwork_Precharge_Code.Text == "")
            {
                MessageBox.Show("Please write the Paperwork's 08 Precharge Code to continue.", "¡ATENTION!");
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
                MessageBox.Show("You have to at least upload 1 file to finish the paperwork.", "¡ATENTION!");
                return false;
            }
            if (!dtTransfer_Date.Checked)
            {
                MessageBox.Show("You have to put the actual transfer date to finish the paperwork.", "¡ATENTION!");
                return false;
            }
            if (dtTransfer_Date.Value == (new DateTime(1754, 1, 1, 12, 0, 0).Date))
            {
                MessageBox.Show("You have to put the actual transfer date to finish the paperwork.", "¡ATENTION!");
                return false;
            }
            return true;
        }

        private void MapPaperworkToControls()
        {
            if (paperwork != null)
            {
                if (paperwork.Transfer_Date.Date == (new DateTime(1754, 1, 1, 12, 0, 0).Date))
                {
                    dtTransfer_Date.Value = new DateTime(1754, 1, 1, 12, 0, 0);
                    dtTransfer_Date.Checked = false;
                }
                else
                {
                    dtTransfer_Date.Checked = true;
                    dtTransfer_Date.Value = paperwork.Transfer_Date;
                }
                tbPaperwork_Precharge_Code.Text = paperwork.Paperwork_Precharge_Code;
                tbPaperwork_Precharge_Code.Text = paperwork.Paperwork_Precharge_Code;
                tbObservations.Text = paperwork.Observations;
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
                    Paperwork _paperwork = new Paperwork();
                    _paperwork.Id = Guid.NewGuid();
                    _paperwork.Invoice_Id = ((Invoice)comboInvoice.SelectedItem).Id;
                    _paperwork.Vehicle_Id = ((Invoice)comboInvoice.SelectedItem).Vehicle_Id;
                    _paperwork.Client_Id = clientView1.client.Id;
                    _paperwork.Paperwork_Precharge_Code = tbPaperwork_Precharge_Code.Text;
                    _paperwork.Observations = tbObservations.Text;
                    if (dtTransfer_Date.Checked)
                    {
                        _paperwork.Transfer_Date = dtTransfer_Date.Value;
                    }
                    else
                    {
                        _paperwork.Transfer_Date = new DateTime(1754, 1, 1, 12, 0, 0); 
                    }
                    _paperwork.IsFinished = false;

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
                        paperwork.Transfer_Date = dtTransfer_Date.Value;
                    }
                    else
                    {
                        paperwork.Transfer_Date = new DateTime(1754, 1, 1, 12, 0, 0);
                    }
                    paperwork.Paperwork_Precharge_Code = tbPaperwork_Precharge_Code.Text;
                    paperwork.Observations = tbObservations.Text;

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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFinishPaperwork_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFinishPaperwork())
                    return;

                if(MessageBox.Show("Are you sure you want to finish the Paperwork? ", "¡ATENTION!", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                paperwork.IsFinished = true;
                
                if (Save())
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clientView1_ClientFound(object sender, EventArgs e)
        {
            if (clientView1.client != null)
            {
                client = clientView1.client;
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
                    openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    openFileDialog.Title = "Select a PDF file";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        PaperworkFile file = new PaperworkFile();
                        file.Id = Guid.NewGuid();
                        file.Paperwork_Id = paperwork.Id;
                        string filePath = openFileDialog.FileName;
                        file.FileName = Path.GetFileName(filePath);
                        file.FileContent = File.ReadAllBytes(filePath);
                        file.UploadedDate = DateTime.Now;
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
                MessageBox.Show(ex.Message);
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
                        PaperworkFile file = (PaperworkFile)row.DataBoundItem;
                        SQLUpdateResult result = _PaperworkBLL.DeletePaperworkFile(file);

                        string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");
                        File.WriteAllBytes(tempFilePath, file.FileContent);
                        System.Diagnostics.Process.Start(tempFilePath);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        PaperworkFile file = (PaperworkFile)row.DataBoundItem;
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
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
