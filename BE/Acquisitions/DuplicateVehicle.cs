
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Registro existente con la misma patente, chasis o motor.
    public class DuplicateVehicle
    {
        // 0 = vehiculo en inventario; si no, numero de la adquisicion abierta.
        [TableColumnAttribute]
        public int Number { get; set; }

        [TableColumnAttribute]
        public string License_Plate { get; set; }

        [TableColumnAttribute]
        public string Chassis_Number { get; set; }

        [TableColumnAttribute]
        public string Engine_Number { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

    }
}
