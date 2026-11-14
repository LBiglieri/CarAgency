
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Pago
    {
        [TableColumnAttribute]
        public Guid Pago_Id { get; set; }

        [TableColumnAttribute]
        public Guid Factura_Id { get; set; }

        [TableColumnAttribute]
        public Guid TipoPago_Id { get; set; }

        [TableColumnAttribute]
        public string TipoPago_Descripcion { get; set; }

        [TableColumnAttribute]
        public double Monto { get; set; }

        [TableColumnAttribute]
        public string Detalle { get; set; }

    }
}
