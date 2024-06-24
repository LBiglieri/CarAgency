
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Invoice : Entity
    {

        [TableColumnAttribute]
        public Guid Vehicle_Id { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

        [TableColumnAttribute]
        public Guid Client_Id { get; set; }

        [TableColumnAttribute]
        public Guid Reservation_Id { get; set; }

        [TableColumnAttribute]
        public string Detail { get; set; }

        [TableColumnAttribute]
        public string CUIL_CUIT_Client { get; set; }

        [TableColumnAttribute]
        public string Razon_Social { get; set; }

        [TableColumnAttribute]
        public double Amount { get; set; }

        [TableColumnAttribute]
        public Boolean Payment_Status { get; set; }

        [TableColumnAttribute]
        public DateTime Creation_Date { get; set; }

    }
}
