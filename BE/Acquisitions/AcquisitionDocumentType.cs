
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Documento obligatorio de la compra (CUF10 pasos 10 y 18).
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class AcquisitionDocumentType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public AcquisitionDocumentStage Stage { get; set; }

        // NULL: el documento no vence.
        public int? Validity_Days { get; set; }
        public int Display_Order { get; set; }

    }
}
