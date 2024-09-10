
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Quotation
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Vehicle_Id { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

        [TableColumnAttribute]
        public Guid Client_Id { get; set; }

        [TableColumnAttribute]
        public string Client_Description { get; set; }

        [TableColumnAttribute]
        public double Price { get; set; }

        [TableColumnAttribute]
        public DateTime Creation_Date { get; set; }

    }
}
