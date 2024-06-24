
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class PaymentType : Entity
    {

        [TableColumnAttribute]
        public string Description { get; set; }
        public override string ToString()
        {
            return Description;
        }

    }
}
