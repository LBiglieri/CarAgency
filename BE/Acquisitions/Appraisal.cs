
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Appraisal
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Acquisition_Id { get; set; }

        [TableColumnAttribute]
        public Guid Inspection_Id { get; set; }

        [TableColumnAttribute]
        public Guid User_Id { get; set; }

        [TableColumnAttribute]
        public double Amount { get; set; }

        [TableColumnAttribute]
        public string Justification { get; set; }

        [TableColumnAttribute]
        public DateTime Creation_Date { get; set; }

        [TableColumnAttribute]
        public DateTime Expiration_Date { get; set; }

        // No es columna de Appraisals: nombre y apellido del usuario que taso.
        [TableColumnAttribute]
        public string Appraiser { get; set; }

    }
}
