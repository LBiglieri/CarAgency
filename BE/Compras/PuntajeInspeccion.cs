
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class PuntajeInspeccion
    {
        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid ItemInspeccion_Id { get; set; }

        // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
        public InspeccionRating Puntaje { get; set; }
        public string Observaciones { get; set; }

    }
}
