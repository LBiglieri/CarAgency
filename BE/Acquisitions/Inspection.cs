
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

        public DateTime? Performed_Date { get; set; }
        public int? Verified_Kilometers { get; set; }
        public int? Result { get; set; }
        public string Observations { get; set; }

    }
}
