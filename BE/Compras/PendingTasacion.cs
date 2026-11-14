
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Adquisicion inspeccionada, o con la tasacion vencida, que espera ser tasada (CUF09 paso 1).
    public class PendingTasacion
    {
        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public int Numero { get; set; }

        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Persona_Id { get; set; }

        [TableColumnAttribute]
        public int DNI { get; set; }

        [TableColumnAttribute]
        public string Patente { get; set; }

        [TableColumnAttribute]
        public string Vehiculo_Descripcion { get; set; }

        [TableColumnAttribute]
        public DateTime FechaRealizada { get; set; }

        [TableColumnAttribute]
        public int TasacionesPrevias { get; set; }

    }
}
