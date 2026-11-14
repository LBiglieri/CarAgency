
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class ItemInspeccion
    {
        [TableColumnAttribute]
        public Guid ItemInspeccion_Id { get; set; }

        [TableColumnAttribute]
        public string Descripcion { get; set; }

        [TableColumnAttribute]
        public int Orden { get; set; }

    }
}
