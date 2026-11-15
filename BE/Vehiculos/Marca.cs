using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Marca
    {
        [TableColumnAttribute]
        public Guid Marca_Id { get; set; }

        [TableColumnAttribute]
        public string Descripcion { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
