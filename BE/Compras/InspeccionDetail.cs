
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Vehiculo de la inspeccion seleccionada, con lo declarado en CUF07 (CUF08 paso 3).
    public class InspeccionDetail
    {
        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

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

        [TableColumnAttribute]
        public int Puertas { get; set; }

        [TableColumnAttribute]
        public int Kilometraje { get; set; }

    }
}
