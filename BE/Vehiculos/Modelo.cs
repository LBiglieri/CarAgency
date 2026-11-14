using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Modelo
    {
        [TableColumnAttribute]
        public Guid Modelo_Id { get; set; }

        [TableColumnAttribute]
        public Guid Marca_Id { get; set; }

        [TableColumnAttribute]
        public string Marca_Descripcion { get; set; }

        [TableColumnAttribute]
        public string Descripcion { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
