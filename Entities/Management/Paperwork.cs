
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Paperwork : Entity
    {

        [TableColumnAttribute]
        public Guid Vehicle_Id { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

        [TableColumnAttribute]
        public Guid Client_Id { get; set; }
        public string Client_Description { get; set; }

        [TableColumnAttribute]
        public Guid Invoice_Id { get; set; }

        [TableColumnAttribute]
        public string Paperwork_Precharge_Code { get; set; }

        [TableColumnAttribute]
        public DateTime Transfer_Date { get; set; }

        [TableColumnAttribute]
        public string Observations { get; set; }

        [TableColumnAttribute]
        public Boolean IsFinished { get; set; }

    }
}
