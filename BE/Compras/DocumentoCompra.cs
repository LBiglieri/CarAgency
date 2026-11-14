
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class DocumentoCompra
    {
        public Guid DocumentoCompra_Id { get; set; }
        public Guid Compra_Id { get; set; }
        public Guid TipoDocumentoCompra_Id { get; set; }
        public DocumentoCompraStatus Estado { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Observacion { get; set; }
        public Guid UsuarioCarga_Id { get; set; }
        public DateTime FechaCarga { get; set; }

        // No son columnas de AcquisitionDocuments: tipo y nombre del archivo, para mostrar.
        public string Descripcion { get; set; }
        public DocumentoCompraStage Etapa { get; set; }
        public string NombreArchivo { get; set; }

        // Escaneo que se guarda con el alta.
        public ArchivoDocumentoCompra File { get; set; }

    }
}
