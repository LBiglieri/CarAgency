using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Vehicle : Entity
    {
        [TableColumnAttribute]
        public string License_Plate { get; set; }

        [TableColumnAttribute]
        public Guid Make_Id { get; set; }

        [TableColumnAttribute]
        public string Make_Description { get; set; }

        [TableColumnAttribute]
        public Guid Model_Id { get; set; }

        [TableColumnAttribute]
        public string Model_Description { get; set; }

        [TableColumnAttribute]
        public Guid Version_Id { get; set; }

        [TableColumnAttribute]
        public string Version_Description { get; set; }

        [TableColumnAttribute]
        public Guid Colour_Id { get; set; }

        [TableColumnAttribute]
        public string Colour_Description { get; set; }

        [TableColumnAttribute]
        public double Price { get; set; }

        [TableColumnAttribute]
        public string Opcionals { get; set; }

        [TableColumnAttribute]
        public string Observations { get; set; }

        [TableColumnAttribute]
        public int Doors { get; set; }

        [TableColumnAttribute]
        public int Year { get; set; }

        [TableColumnAttribute]
        public int Kilometers { get; set; }

        [TableColumnAttribute]
        public string ImageLink { get; set; }

    }
}
