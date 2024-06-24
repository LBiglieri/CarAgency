using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Entities
{
    public class Make : Entity
    {

        [TableColumnAttribute]
        public string Description { get; set; }
        public override string ToString()
        {
            return Description;
        }
    }
}
