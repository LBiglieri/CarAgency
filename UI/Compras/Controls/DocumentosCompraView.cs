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

namespace UI.Compras.Controls
{
    // Documentos obligatorios de una etapa de CUF10 (Documentar o Transferir): estado de cada uno, carga del
    // escaneo con su fecha de emision, apertura, observacion y baja. Los archivos se manejan como en los tramites.
    public partial class DocumentosCompraView : UserControl, ILanguageObserver
    {
        public event EventHandler DocumentsChanged;
        public DocumentoCompraStage Stage { get; set; }
        List<TipoDocumentoCompra> document_types;
        List<DocumentoCompra> documents;
        Guid acquisition_id;
        CompraBLL _AcquisitionBLL;

        public DocumentosCompraView()
        {
            InitializeComponent();
            _AcquisitionBLL = new CompraBLL();
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
        public static string GetStatusDescription(DocumentoCompra document)
        {
            return LanguageService.GetTagText("AcquisitionDocumentStatus_" + (document == null ? "Pending" : document.Estado.ToString()));
        }

        private void PerformUpdateDocumentsView()
        {
            int selected = gridDocuments.CurrentRow == null ? 0 : gridDocuments.CurrentRow.Index;
            gridDocuments.Rows.Clear();
            if (document_types == null)
                return;
            foreach (TipoDocumentoCompra type in document_types)
            {
                DocumentoCompra document = GetDocument(type);
                int index = gridDocuments.Rows.Add(GetDocumentTypeDescription(type.Descripcion), GetStatusDescription(document),
                    document == null ? "" : document.FechaEmision.ToString("dd/MM/yyyy"), document == null ? "" : document.Observacion);
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
        private DocumentoCompra GetDocument(TipoDocumentoCompra type)
        {
            return documents == null ? null : documents.FirstOrDefault(d => d.TipoDocumentoCompra_Id == type.TipoDocumentoCompra_Id);
        }
        private TipoDocumentoCompra GetSelectedType()
        {
            return gridDocuments.CurrentRow == null ? null : (TipoDocumentoCompra)gridDocuments.CurrentRow.Tag;
        }
        // Abrir, observar y eliminar necesitan un documento ya cargado.
        private DocumentoCompra GetSelectedDocument()
        {
            TipoDocumentoCompra type = GetSelectedType();
            DocumentoCompra document = type == null ? null : GetDocument(type);
            if (document == null)
                MessageBox.Show(LanguageService.GetTagText("PleaseSelectUploadedDocument"), LanguageService.GetTagText("AttentionTitle"));
            return document;
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                TipoDocumentoCompra type = GetSelectedType();
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
                        DocumentoCompra document = new DocumentoCompra();
                        document.Compra_Id = acquisition_id;
                        document.TipoDocumentoCompra_Id = type.TipoDocumentoCompra_Id;
                        document.Etapa = Stage;
                        document.FechaEmision = dtIssue_Date.Value;
                        document.File = new ArchivoDocumentoCompra();
                        string filePath = openFileDialog.FileName;
                        document.File.NombreArchivo = Path.GetFileName(filePath);
                        document.File.Contenido = File.ReadAllBytes(filePath);
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
                DocumentoCompra document = GetSelectedDocument();
                if (document == null)
                    return;

                ArchivoDocumentoCompra file = _AcquisitionBLL.GetDocumentFile(document.DocumentoCompra_Id);
                string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(file.NombreArchivo));
                File.WriteAllBytes(tempFilePath, file.Contenido);
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
                DocumentoCompra document = GetSelectedDocument();
                if (document == null)
                    return;

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureDeleteDocument"), GetDocumentTypeDescription(document.Descripcion)), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
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
                DocumentoCompra document = GetSelectedDocument();
                if (document == null)
                    return;
                if (tbDocumentObservation.Text.Trim() == "")
                {
                    MessageBox.Show(LanguageService.GetTagText("PleaseWriteDocumentObservation"), LanguageService.GetTagText("AttentionTitle"));
                    return;
                }

                if (MessageBox.Show(string.Format(LanguageService.GetTagText("AreYouSureObserveDocument"), GetDocumentTypeDescription(document.Descripcion)), LanguageService.GetTagText("AttentionTitle"), MessageBoxButtons.YesNo) == DialogResult.No)
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
