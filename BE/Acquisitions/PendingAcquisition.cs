
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Compra en gestion, de Tasada a Transferida (CUF10 paso 1).
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum.
    public class PendingAcquisition
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid Inspection_Id { get; set; }
        public Guid Client_Id { get; set; }
        public int Dni { get; set; }
        public string License_Plate { get; set; }
        public string Vehicle_Description { get; set; }
        public AcquisitionStatus Status { get; set; }

    }
}
