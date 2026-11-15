
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class FotografiaInspeccion
    {
        [TableColumnAttribute]
        public Guid FotografiaInspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public string NombreArchivo { get; set; }

        [TableColumnAttribute]
        public byte[] Contenido { get; set; }

        [TableColumnAttribute]
        public DateTime FechaCarga { get; set; }

    }
}
