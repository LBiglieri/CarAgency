
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class ArchivoDocumentoCompra
    {
        [TableColumnAttribute]
        public Guid DocumentoCompra_Id { get; set; }

        [TableColumnAttribute]
        public string NombreArchivo { get; set; }

        [TableColumnAttribute]
        public byte[] Contenido { get; set; }

    }
}
