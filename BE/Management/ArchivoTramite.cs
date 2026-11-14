
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class ArchivoTramite
    {
        [TableColumnAttribute]
        public Guid ArchivoTramite_Id { get; set; }

        [TableColumnAttribute]
        public Guid Tramite_Id { get; set; }

        [TableColumnAttribute]
        public string NombreArchivo { get; set; }

        [TableColumnAttribute]
        public byte[] Contenido { get; set; }

        [TableColumnAttribute]
        public DateTime FechaCarga { get; set; }

    }
}
