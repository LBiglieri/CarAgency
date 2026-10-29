
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Inspection
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Acquisition_Id { get; set; }

        [TableColumnAttribute]
        public Guid Mechanic_Id { get; set; }

        [TableColumnAttribute]
        public DateTime Scheduled_Date { get; set; }

        // Se completan en CUF08. Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
        public DateTime? Performed_Date { get; set; }
        public int? Verified_Kilometers { get; set; }
        public InspectionResult? Result { get; set; }
        public string Observations { get; set; }

        // No es columna: CUF08 graba las calificaciones junto con la inspeccion.
        public List<InspectionScore> Scores { get; set; }

    }
}
