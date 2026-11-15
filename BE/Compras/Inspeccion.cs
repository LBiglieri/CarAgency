
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Inspeccion
    {
        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public Guid Mecanico_Id { get; set; }

        [TableColumnAttribute]
        public DateTime FechaProgramada { get; set; }

        // Se completan en CUF08. Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
        public DateTime? FechaRealizada { get; set; }
        public int? KilometrajeVerificado { get; set; }
        public InspeccionResult? Resultado { get; set; }
        public string Observaciones { get; set; }

        // No es columna: CUF08 graba las calificaciones junto con la inspeccion.
        public List<PuntajeInspeccion> Puntajes { get; set; }

    }
}
