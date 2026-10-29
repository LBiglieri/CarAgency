
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Vehiculo que ofrece el cliente. Pasa a Vehicles recien cuando se completa la adquisicion.
    public class ProspectVehicle
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public string License_Plate { get; set; }

        [TableColumnAttribute]
        public string Chassis_Number { get; set; }

        [TableColumnAttribute]
        public string Engine_Number { get; set; }

        [TableColumnAttribute]
        public Guid Make_Id { get; set; }

        [TableColumnAttribute]
        public Guid Model_Id { get; set; }

        [TableColumnAttribute]
        public Guid Version_Id { get; set; }

        [TableColumnAttribute]
        public Guid Colour_Id { get; set; }

        [TableColumnAttribute]
        public int Year { get; set; }

        [TableColumnAttribute]
        public int Doors { get; set; }

        [TableColumnAttribute]
        public int Declared_Kilometers { get; set; }

    }
}
