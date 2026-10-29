
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Inspeccion pendiente en la agenda del mecanico (CUF08 paso 1).
    public class ScheduledInspection
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Acquisition_Id { get; set; }

        [TableColumnAttribute]
        public Guid Mechanic_Id { get; set; }

        [TableColumnAttribute]
        public DateTime Scheduled_Date { get; set; }

        [TableColumnAttribute]
        public int Number { get; set; }

        [TableColumnAttribute]
        public string License_Plate { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

    }
}
