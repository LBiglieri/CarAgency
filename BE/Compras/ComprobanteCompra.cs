
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Comprobante de compra (CUF11 paso 7). Como Invoice, guarda los datos tal como se imprimieron.
    public class ComprobanteCompra
    {
        [TableColumnAttribute]
        public Guid ComprobanteCompra_Id { get; set; }

        [TableColumnAttribute]
        public int Numero { get; set; }

        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public DateTime Fecha { get; set; }

        [TableColumnAttribute]
        public string NombrePersona { get; set; }

        [TableColumnAttribute]
        public string CuilCuit { get; set; }

        [TableColumnAttribute]
        public string Detalle { get; set; }

        [TableColumnAttribute]
        public double MontoNeto { get; set; }

        [TableColumnAttribute]
        public double MontoImpuesto { get; set; }

        [TableColumnAttribute]
        public double MontoTotal { get; set; }

    }
}
