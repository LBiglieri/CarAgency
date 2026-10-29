using BLL;
using CarAgency.BLL;
using CarAgency.BE;
using CarAgency.UI;
using BE;
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
using Security.Session;

namespace UI.Acquisitions.Controls
{
    // Documentos obligatorios de una etapa de CUF10 (Documentar o Transferir): estado de cada uno, carga del
    // escaneo con su fecha de emision, apertura, observacion y baja. Los archivos se manejan como en los tramites.
    public partial class AcquisitionDocumentsView : UserControl, ILanguageObserver
    {
        public event EventHandler DocumentsChanged;
        public AcquisitionDocumentStage Stage { get; set; }
        List<AcquisitionDocumentType> document_types;
        List<AcquisitionDocument> documents;
        Guid acquisition_id;
        AcquisitionBLL _AcquisitionBLL;

        public AcquisitionDocumentsView()
        {
            InitializeComponent();
            _AcquisitionBLL = new AcquisitionBLL();
            UpdateLanguage("");
        }

        public void InitWithAcquisition(Guid Acquisition_Id, bool editable)
        {
            acquisition_id = Acquisition_Id;
            document_types = _AcquisitionBLL.GetDocumentTypes(Stage);
            documents = _AcquisitionBLL.GetDocuments(Acquisition_Id);
            dtIssue_Date.Value = DateTime.Today;
            tbDocumentObservation.Text = "";
            EnableControls(editable);
            PerformUpdateDocumentsView();
        }

        public void Clear()
        {
            acquisition_id = Guid.Empty;
            document_types = null;
            documents = null;
            tbDocumentObservation.Text = "";
            EnableControls(false);
            gridDocuments.Rows.Clear();
        }

        public void UpdateLanguage(string language)
        {
            colDocumentType.HeaderText = LanguageService.GetTagText(colDocumentType.Tag.ToString());
            colDocumentStatus.HeaderText = LanguageService.GetTagText(colDocumentStatus.Tag.ToString());
            colIssue_Date.HeaderText = LanguageService.GetTagText(colIssue_Date.Tag.ToString());
            colDocumentObservation.HeaderText = LanguageService.GetTagText(colDocumentObservation.Tag.ToString());
            lblIssueDate.Text = LanguageService.GetTagText(lblIssueDate.Tag.ToString());
            btnAddFile.Text = LanguageService.GetTagText(btnAddFile.Tag.ToString());
            btnOpenFile.Text = LanguageService.GetTagText(btnOpenFile.Tag.ToString());
            btnDeleteFile.Text = LanguageService.GetTagText(btnDeleteFile.Tag.ToString());
            tbDocumentObservation.WaterMark = LanguageService.GetTagText(tbDocumentObservation.Tag.ToString());
            btnObserveDocument.Text = LanguageService.GetTagText(btnObserveDocument.Tag.ToString());
            PerformUpdateDocumentsView();
        }

        // El catalogo esta en ingles: se muestra con su traduccion, como los items de la inspeccion.
        public static string GetDocumentTypeDescription(string description)
        {
            return LanguageService.GetTagText("AcquisitionDocumentType_" + description.Replace(" ", ""), description);
        }
        // Un documento sin fila esta Pendiente.
        public static string GetStatusDescription(AcquisitionDocument document)
        {
            return LanguageService.GetTagText("AcquisitionDocumentStatus_" + (document == null ? "Pending" : document.Status.ToString()));
        }

        private void PerformUpdateDocumentsView()
        {
            int selected = gridDocuments.CurrentRow == null ? 0 : gridDocuments.CurrentRow.Index;
            gridDocuments.Rows.Clear();
            if (document_types == null)
                return;
            foreach (AcquisitionDocumentType type in document_types)
            {
                AcquisitionDocument document = GetDocument(type);
                int index = gridDocuments.Rows.Add(GetDocumentTypeDescription(type.Description), GetStatusDescription(document),
                    document == null ? "" : document.Issue_Date.ToString("dd/MM/yyyy"), document == null ? "" : document.Observation);
                gridDocuments.Rows[index].Tag = type;
            }
            if (selected < gridDocuments.Rows.Count)
                gridDocuments.CurrentCell = gridDocuments.Rows[selected].Cells[0];
        }
        private void PerformDocumentsChanged()
        {
            documents = _AcquisitionBLL.GetDocuments(acquisition_id);
            tbDocumentObservation.Text = "";
            PerformUpdateDocumentsView();
            if (DocumentsChanged != null)
                this.DocumentsChanged(this, new EventArgs());
        }
        private void EnableControls(bool editable)
        {
            dtIssue_Date.Enabled = editable;
            btnAddFile.Enabled = editable;
            btnDeleteFile.Enabled = editable;
            tbDocumentObservation.Enabled = editable;
            btnObserveDocument.Enabled = editable;
        }
        private AcquisitionDocument GetDocument(AcquisitionDocumentType type)
        {
            return documents == null ? null : documents.FirstOrDefault(d => d.DocumentType_Id == type.Id);
        }
        private AcquisitionDocumentType GetSelectedType()
        {
            return gridDocuments.CurrentRow == null ? null : (AcquisitionDocumentType)gridDocuments.CurrentRow.Tag;
        }
        // Abrir, observar y eliminar necesitan un documento ya cargado.
        private AcquisitionDocument GetSelectedDocument()
        {
            AcquisitionDocumentType type = GetSelectedType();
            AcquisitionDocument document = type == null ? null : GetDocument(type);
            if (document == null)
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectUploadedDocument"), LanguageService.GetTagText("AttentionTitle"));
            return document;
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                AcquisitionDocumentType type = GetSelectedType();
                if (type == null)
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseSelectDocument"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }
                // CUF10 11.2: para reemplazarlo, primero se elimina el cargado.
                if (GetDocument(type) != null)
                {
                    MessageBox.Show(LanguageService.GetTagText("DocumentAlreadyUploaded"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = LanguageService.GetTagText("DocumentFileFilter");
                    openFileDialog.Title = LanguageService.GetTagText("SelectDocumentTitle");

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        AcquisitionDocument document = new AcquisitionDocument();
                        document.Acquisition_Id = acquisition_id;
                        document.DocumentType_Id = type.Id;
                        document.Stage = Stage;
                        document.Issue_Date = dtIssue_Date.Value;
                        document.File = new AcquisitionDocumentFile();
                        string filePath = openFileDialog.FileName;
                        document.File.FileName = Path.GetFileName(filePath);
                        document.File.FileContent = File.ReadAllBytes(filePath);
                        SQLUpdateResult result = _AcquisitionBLL.AddDocument(document);

                        if (result != null && result.sqlResult == SQLResultType.success)
                            PerformDocumentsChanged();
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
                AcquisitionDocument document = GetSelectedDocument();
                if (document == null)
                    return;

                AcquisitionDocumentFile file = _AcquisitionBLL.GetDocumentFile(document.Id);
                string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(file.FileName));
                File.WriteAllBytes(tempFilePath, file.FileContent);
                Process.Start(tempFilePath);
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
                AcquisitionDocument document = GetSelectedDocument();
                if (document == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureDeleteDocument"), GetDocumentTypeDescription(document.Description)), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.DeleteDocument(document);
                if (result != null && result.sqlResult == SQLResultType.success)
                    PerformDocumentsChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }

        // CUF10 11.1: el documento queda Observado con el comentario y la compra no avanza hasta reemplazarlo.
        private void btnObserveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                AcquisitionDocument document = GetSelectedDocument();
                if (document == null)
                    return;
                if (tbDocumentObservation.Text.Trim() == "")
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWriteDocumentObservation"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureObserveDocument"), GetDocumentTypeDescription(document.Description)), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SQLUpdateResult result = _AcquisitionBLL.ObserveDocument(document, tbDocumentObservation.Text);
                if (result != null && result.sqlResult == SQLResultType.success)
                    PerformDocumentsChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageService.GetErrorText(ex));
            }
        }
    }
}
