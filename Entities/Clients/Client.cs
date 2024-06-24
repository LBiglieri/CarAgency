using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Client : Entity
    {
        [TableColumnAttribute]
        public int Dni { get; set; }

        [TableColumnAttribute]
        public string Name { get; set; }

        [TableColumnAttribute]
        public string Surname { get; set; }

        [TableColumnAttribute]
        public string Address { get; set; }

        [TableColumnAttribute]
        public string Phone_Number_Personal { get; set; }

        [TableColumnAttribute]
        public string Phone_Number_House { get; set; }

        [TableColumnAttribute]
        public string Email { get; set; }

        [TableColumnAttribute]
        public DateTime Date_Of_Birth { get; set; }
    }
}
