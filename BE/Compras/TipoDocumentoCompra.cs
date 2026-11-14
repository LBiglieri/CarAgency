
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Documento obligatorio de la compra (CUF10 pasos 10 y 18).
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class TipoDocumentoCompra
    {
        public Guid TipoDocumentoCompra_Id { get; set; }
        public string Descripcion { get; set; }
        public DocumentoCompraStage Etapa { get; set; }

        // NULL: el documento no vence.
        public int? DiasVigencia { get; set; }
        public int Orden { get; set; }

    }
}
