using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Compras;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class DocumentoCompraMapper
    {
        private readonly DocumentoCompraDataAccess data = new DocumentoCompraDataAccess();

        // Validity_Days puede ser NULL: los tipos se arman a mano.
        public List<TipoDocumentoCompra> GetDocumentTypes(DocumentoCompraStage stage)
        {
            List<TipoDocumentoCompra> types = new List<TipoDocumentoCompra>();
            using (DataTable table = data.GetDocumentTypes((int)stage))
            {
                foreach (DataRow row in table.Rows)
                {
                    TipoDocumentoCompra type = new TipoDocumentoCompra();
                    type.TipoDocumentoCompra_Id = (Guid)row["TipoDocumentoCompra_Id"];
                    type.Descripcion = (string)row["Descripcion"];
                    type.Etapa = (DocumentoCompraStage)(int)row["Etapa"];
                    type.DiasVigencia = row["DiasVigencia"] as int?;
                    type.Orden = (int)row["Orden"];
                    types.Add(type);
                }
            }
            return types;
        }

        public List<DocumentoCompra> GetDocuments(Guid Acquisition_Id)
        {
            List<DocumentoCompra> documents = new List<DocumentoCompra>();
            using (DataTable table = data.GetDocuments(Acquisition_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    DocumentoCompra document = new DocumentoCompra();
                    document.DocumentoCompra_Id = (Guid)row["DocumentoCompra_Id"];
                    document.Compra_Id = (Guid)row["Compra_Id"];
                    document.TipoDocumentoCompra_Id = (Guid)row["TipoDocumentoCompra_Id"];
                    document.Estado = (DocumentoCompraStatus)(int)row["Estado"];
                    document.FechaEmision = (DateTime)row["FechaEmision"];
                    document.Observacion = row["Observacion"] as string;
                    document.UsuarioCarga_Id = (Guid)row["UsuarioCarga_Id"];
                    document.FechaCarga = (DateTime)row["FechaCarga"];
                    document.Descripcion = (string)row["Descripcion"];
                    document.Etapa = (DocumentoCompraStage)(int)row["Etapa"];
                    document.NombreArchivo = row["NombreArchivo"] as string;
                    documents.Add(document);
                }
            }
            return documents;
        }

        public SQLUpdateResult AddDocument(DocumentoCompra document)
        {
            return DigitVerifierWriteMapper.Save("DocumentoCompra", dvh => data.AddDocument(document.DocumentoCompra_Id, document.Compra_Id,
                document.TipoDocumentoCompra_Id, (int)document.Estado, document.FechaEmision, document.Observacion, document.UsuarioCarga_Id,
                document.FechaCarga, dvh));
        }

        public SQLUpdateResult ObserveDocument(DocumentoCompra document)
        {
            return DigitVerifierWriteMapper.Save("DocumentoCompra", dvh => data.ObserveDocument(document.DocumentoCompra_Id, document.Compra_Id,
                document.TipoDocumentoCompra_Id, (int)document.Estado, document.FechaEmision, document.Observacion, document.UsuarioCarga_Id,
                document.FechaCarga, dvh));
        }

        // El archivo se borra en cascada y no esta protegido: solo cambia el DVV de AcquisitionDocuments.
        public SQLUpdateResult DeleteDocument(DocumentoCompra document)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteDocument(document.DocumentoCompra_Id), "DocumentoCompra");
        }

        // AcquisitionDocumentFiles no esta protegida con digitos verificadores.
        public SQLUpdateResult AddDocumentFile(ArchivoDocumentoCompra file)
        {
            return MappingHandler.MapUpdateResult(data.AddDocumentFile(file.DocumentoCompra_Id, file.NombreArchivo, file.Contenido));
        }

        public ArchivoDocumentoCompra GetDocumentFile(Guid Document_Id)
        {
            return MappingHandler.MapTableToEntities<ArchivoDocumentoCompra>(data.GetDocumentFile(Document_Id)).FirstOrDefault();
        }
    }
}
