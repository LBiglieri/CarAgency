using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;

namespace CarAgency.BLL
{
    // Documentos de la compra (CUF10): los de la etapa B con la compra formalizada y los de la etapa C con la compra pagada.
    public class DocumentoCompraBLL
    {
        // Documentos escaneados (CUF10 paso 12): solo pdf, como los archivos de los tramites.
        private static readonly string[] DocumentExtensions = { ".pdf" };

        private DocumentoCompraMapper _documentmapper;
        private CompraMapper _acquisitionmapper;
        public DocumentoCompraBLL()
        {
            _documentmapper = new DocumentoCompraMapper();
            _acquisitionmapper = new CompraMapper();
        }

        public List<TipoDocumentoCompra> GetDocumentTypes(DocumentoCompraStage stage)
        {
            return _documentmapper.GetDocumentTypes(stage);
        }
        public List<DocumentoCompra> GetDocuments(Guid Acquisition_Id)
        {
            return _documentmapper.GetDocuments(Acquisition_Id);
        }
        public ArchivoDocumentoCompra GetDocumentFile(Guid Document_Id)
        {
            return _documentmapper.GetDocumentFile(Document_Id);
        }

        // CUF10 pasos 11 a 13 y 18: el documento y su escaneo se graban uno detras del otro, como los archivos de los tramites.
        // Para reemplazar uno cargado u observado (11.2) primero se elimina.
        public SQLUpdateResult AddDocument(DocumentoCompra document)
        {
            TipoDocumentoCompra type = GetDocumentTypes(document.Etapa).FirstOrDefault(t => t.TipoDocumentoCompra_Id == document.TipoDocumentoCompra_Id);
            if (type == null)
                throw new TranslatableException("PleaseSelectDocument", "Seleccione un documento de la lista para continuar.");
            ValidateDocumentFile(document.File);
            ValidateDocumentValidity(type, document.FechaEmision);

            Compra acquisition = GetAcquisitionInStage(document.Compra_Id, type.Etapa);
            if (GetDocuments(acquisition.Compra_Id).Any(d => d.TipoDocumentoCompra_Id == type.TipoDocumentoCompra_Id))
                throw new TranslatableException("DocumentAlreadyUploaded", "El documento ya esta cargado. Eliminelo para reemplazarlo.");

            document.DocumentoCompra_Id = Guid.NewGuid();
            document.Estado = DocumentoCompraStatus.Uploaded;
            document.FechaEmision = document.FechaEmision.Date;
            document.Observacion = null;
            document.UsuarioCarga_Id = SessionHandler.Instance.GetId();
            document.FechaCarga = DateTime.Now;
            document.File.DocumentoCompra_Id = document.DocumentoCompra_Id;

            SQLUpdateResult result = _documentmapper.AddDocument(document);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _documentmapper.AddDocumentFile(document.File);
            result = RecordResult(result, TipoEvento.AcquisitionDocumentCreated, acquisition.Compra_Id);

            // Paso 15: con el ultimo documento cargado la compra queda lista para el pago (CUF11).
            if (result != null && result.sqlResult == SQLResultType.success && type.Etapa == DocumentoCompraStage.Document
                && IsStageComplete(acquisition.Compra_Id, DocumentoCompraStage.Document))
            {
                acquisition.Estado = CompraStatus.DocumentationComplete;
                result = _acquisitionmapper.UpdateAcquisition(acquisition);
            }
            return result;
        }

        // CUF10 11.1: el titular no es la persona o el dominio informa un gravamen. La compra no avanza hasta reemplazarlo.
        public SQLUpdateResult ObserveDocument(DocumentoCompra document, string observation)
        {
            if (string.IsNullOrWhiteSpace(observation))
                throw new TranslatableException("PleaseWriteDocumentObservation", "Escriba el motivo de la observacion del documento para continuar.");
            Compra acquisition = GetAcquisitionInStage(document.Compra_Id, document.Etapa);
            document.Estado = DocumentoCompraStatus.Observed;
            document.Observacion = observation.Trim();
            return RecordResult(_documentmapper.ObserveDocument(document), TipoEvento.AcquisitionDocumentObserved, acquisition.Compra_Id);
        }

        // CUF10 11.2: el escaneo se borra con el documento.
        public SQLUpdateResult DeleteDocument(DocumentoCompra document)
        {
            Compra acquisition = GetAcquisitionInStage(document.Compra_Id, document.Etapa);
            return RecordResult(_documentmapper.DeleteDocument(document), TipoEvento.AcquisitionDocumentDeleted, acquisition.Compra_Id);
        }

        // Todos los documentos de la etapa cargados y sin observaciones.
        public bool IsStageComplete(Guid Acquisition_Id, DocumentoCompraStage stage)
        {
            List<DocumentoCompra> documents = GetDocuments(Acquisition_Id);
            return GetDocumentTypes(stage).All(t => documents.Any(d => d.TipoDocumentoCompra_Id == t.TipoDocumentoCompra_Id && d.Estado == DocumentoCompraStatus.Uploaded));
        }

        // Los documentos de la etapa B se cargan con la compra formalizada y los de la etapa C con la compra pagada.
        private Compra GetAcquisitionInStage(Guid Acquisition_Id, DocumentoCompraStage stage)
        {
            return CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, Acquisition_Id,
                stage == DocumentoCompraStage.Document ? CompraStatus.PurchaseFormalized : CompraStatus.Paid);
        }

        private static void ValidateDocumentFile(ArchivoDocumentoCompra file)
        {
            if (file == null || file.Contenido == null || file.Contenido.Length == 0)
                throw new TranslatableException("PleaseSelectDocumentFile", "Adjunte el archivo escaneado del documento para continuar.");
            if (!DocumentExtensions.Contains(Path.GetExtension(file.NombreArchivo).ToLowerInvariant()))
                throw new TranslatableException("InvalidDocumentFormat", "Los documentos tienen que ser archivos pdf.");
        }

        // CUF10 paso 12: emision no futura y, si el tipo vence, dentro de su vigencia.
        private static void ValidateDocumentValidity(TipoDocumentoCompra type, DateTime Issue_Date)
        {
            if (Issue_Date.Date > DateTime.Today)
                throw new TranslatableException("DocumentIssueDateInFuture", "La fecha de emision del documento no puede ser futura.");
            if (type.DiasVigencia != null && Issue_Date.Date.AddDays(type.DiasVigencia.Value) < DateTime.Today)
                throw new TranslatableException("DocumentExpired", "El documento esta vencido: tiene una vigencia de {0} dias desde su emision.", type.DiasVigencia.Value);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
