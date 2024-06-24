using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Version : Entity
    {

        [TableColumnAttribute]
        public Guid Make_Id { get; set; }

        [TableColumnAttribute]
        public string Make_Description { get; set; }

        [TableColumnAttribute]
        public Guid Model_Id { get; set; }

        [TableColumnAttribute]
        public string Model_Description { get; set; }

        [TableColumnAttribute]
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
