using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Compras
{
    // Documentos de la compra (PN2) y su escaneo. AcquisitionDocumentFiles no esta protegida con digitos verificadores.
    public sealed class DocumentoCompraDataAccess : DataAccessBase
    {
        public DataTable GetDocumentTypes(int stage) { return Read("TipoDocumentoCompra_GetByStage", new SqlParameter("@Etapa", stage)); }

        public DataTable GetDocuments(Guid acquisitionId)
        {
            return Read("DocumentoCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable AddDocument(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate, string observation,
            Guid userId, DateTime uploadedDate, DvhCalculator dvh)
        {
            return Read("DocumentoCompra_Add", dvh, DocumentParameters(id, acquisitionId, documentTypeId, status, issueDate, observation,
                userId, uploadedDate));
        }

        public DataTable ObserveDocument(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate, string observation,
            Guid userId, DateTime uploadedDate, DvhCalculator dvh)
        {
            return Read("DocumentoCompra_Observe", dvh, DocumentParameters(id, acquisitionId, documentTypeId, status, issueDate, observation,
                userId, uploadedDate));
        }

        public DataTable DeleteDocument(Guid id) { return Read("DocumentoCompra_Delete", new SqlParameter("@DocumentoCompra_Id", id)); }

        public DataTable GetDocumentFile(Guid documentId)
        {
            return Read("ArchivoDocumentoCompra_GetByDocument", new SqlParameter("@DocumentoCompra_Id", documentId));
        }

        public DataTable AddDocumentFile(Guid documentId, string fileName, byte[] fileContent)
        {
            return Read("ArchivoDocumentoCompra_Add",
                new SqlParameter("@DocumentoCompra_Id", documentId),
                new SqlParameter("@NombreArchivo", fileName),
                new SqlParameter("@Contenido", fileContent));
        }

        private static SqlParameter[] DocumentParameters(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate,
            string observation, Guid userId, DateTime uploadedDate)
        {
            return new[]
            {
                new SqlParameter("@DocumentoCompra_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@TipoDocumentoCompra_Id", documentTypeId),
                new SqlParameter("@Estado", status),
                new SqlParameter("@FechaEmision", issueDate),
                new SqlParameter("@Observacion", (object)observation ?? DBNull.Value),
                new SqlParameter("@UsuarioCarga_Id", userId),
                new SqlParameter("@FechaCarga", uploadedDate)
            };
        }
    }
}
