
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Adquisicion inspeccionada, o con la tasacion vencida, que espera ser tasada (CUF09 paso 1).
    public class PendingAppraisal
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public int Number { get; set; }

        [TableColumnAttribute]
        public Guid Inspection_Id { get; set; }

        [TableColumnAttribute]
        public Guid Client_Id { get; set; }

        [TableColumnAttribute]
        public int Dni { get; set; }

        [TableColumnAttribute]
        public string License_Plate { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

        [TableColumnAttribute]
        public DateTime Performed_Date { get; set; }

        [TableColumnAttribute]
        public int Previous_Appraisals { get; set; }

    }
}
