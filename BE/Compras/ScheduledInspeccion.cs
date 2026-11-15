
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Inspeccion pendiente en la agenda del mecanico (CUF08 paso 1).
    public class ScheduledInspeccion
    {
        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public Guid Mecanico_Id { get; set; }

        [TableColumnAttribute]
        public DateTime FechaProgramada { get; set; }

        [TableColumnAttribute]
        public int Numero { get; set; }

        [TableColumnAttribute]
        public string Patente { get; set; }

        [TableColumnAttribute]
        public string Vehiculo_Descripcion { get; set; }

    }
}
