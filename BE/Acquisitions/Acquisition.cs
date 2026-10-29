
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Acquisition
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public int Number { get; set; }

        [TableColumnAttribute]
        public Guid Client_Id { get; set; }

        [TableColumnAttribute]
        public Guid ProspectVehicle_Id { get; set; }

        [TableColumnAttribute]
        public Guid Salesman_Id { get; set; }

        [TableColumnAttribute]
        public DateTime Creation_Date { get; set; }

        // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
        public AcquisitionStatus Status { get; set; }

        // Se completan en las etapas siguientes (CUF08 a CUF11).
        public RejectionReason? Rejection_Reason { get; set; }
        public Guid? Accepted_Appraisal_Id { get; set; }
        public double? Agreed_Price { get; set; }
        public string Owner_CUIL_CUIT { get; set; }
        public DateTime? Contract_Signed_Date { get; set; }
        public string Precharge_Code_08 { get; set; }
        public DateTime? Transfer_Appointment_Date { get; set; }
        public Guid? Vehicle_Id { get; set; }
        public DateTime? Closing_Date { get; set; }

        // No son columnas: el alta de CUF07 guarda los tres registros juntos.
        public ProspectVehicle ProspectVehicle { get; set; }
        public Inspection Inspection { get; set; }

    }
}
