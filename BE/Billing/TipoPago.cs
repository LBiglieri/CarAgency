
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class TipoPago
    {
        [TableColumnAttribute]
        public Guid TipoPago_Id { get; set; }

        [TableColumnAttribute]
        public string Descripcion { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }

    }
}
