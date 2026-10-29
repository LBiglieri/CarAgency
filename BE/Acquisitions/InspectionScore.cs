
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class InspectionScore
    {
        [TableColumnAttribute]
        public Guid Inspection_Id { get; set; }

        [TableColumnAttribute]
        public Guid InspectionItem_Id { get; set; }

        // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
        public InspectionRating Score { get; set; }
        public string Observations { get; set; }

    }
}
