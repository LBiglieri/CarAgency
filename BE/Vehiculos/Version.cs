using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Version
    {
        [TableColumnAttribute]
        public Guid Version_Id { get; set; }

        [TableColumnAttribute]
        public Guid Marca_Id { get; set; }

        [TableColumnAttribute]
        public string Marca_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Modelo_Id { get; set; }

        [TableColumnAttribute]
        public string Modelo_Descripcion { get; set; }

        [TableColumnAttribute]
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
