
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Calificacion de un item con su descripcion, para mostrar la inspeccion registrada (CUF09 paso 2).
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class InspectionScoreDetail
    {
        public Guid Inspection_Id { get; set; }
        public Guid InspectionItem_Id { get; set; }
        public string Description { get; set; }
        public int Display_Order { get; set; }
        public InspectionRating Score { get; set; }
        public string Observations { get; set; }

    }
}
