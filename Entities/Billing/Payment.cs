
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Payment
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Invoice_Id { get; set; }

        [TableColumnAttribute]
        public Guid PaymentType_Id { get; set; }

        [TableColumnAttribute]
        public string PaymentType_Description { get; set; }

        [TableColumnAttribute]
        public double Amount { get; set; }

        [TableColumnAttribute]
        public string Detail { get; set; }

    }
}
