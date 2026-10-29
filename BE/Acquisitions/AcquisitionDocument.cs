
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class AcquisitionDocument
    {
        public Guid Id { get; set; }
        public Guid Acquisition_Id { get; set; }
        public Guid DocumentType_Id { get; set; }
        public AcquisitionDocumentStatus Status { get; set; }
        public DateTime Issue_Date { get; set; }
        public string Observation { get; set; }
        public Guid User_Id { get; set; }
        public DateTime Uploaded_Date { get; set; }

        // No son columnas de AcquisitionDocuments: tipo y nombre del archivo, para mostrar.
        public string Description { get; set; }
        public AcquisitionDocumentStage Stage { get; set; }
        public string FileName { get; set; }

        // Escaneo que se guarda con el alta.
        public AcquisitionDocumentFile File { get; set; }

    }
}
