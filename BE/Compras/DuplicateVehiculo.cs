
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Registro existente con la misma patente, chasis o motor.
    public class DuplicateVehiculo
    {
        // 0 = vehiculo en inventario; si no, numero de la adquisicion abierta.
        [TableColumnAttribute]
        public int Numero { get; set; }

        [TableColumnAttribute]
        public string Patente { get; set; }

        [TableColumnAttribute]
        public string NumeroChasis { get; set; }

        [TableColumnAttribute]
        public string NumeroMotor { get; set; }

        [TableColumnAttribute]
        public string Vehiculo_Descripcion { get; set; }

    }
}
