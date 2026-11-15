
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Calificacion de un item con su descripcion, para mostrar la inspeccion registrada (CUF09 paso 2).
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class PuntajeInspeccionDetail
    {
        public Guid Inspeccion_Id { get; set; }
        public Guid ItemInspeccion_Id { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public InspeccionRating Puntaje { get; set; }
        public string Observaciones { get; set; }

    }
}
