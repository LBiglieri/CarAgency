
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Pago del cajero a la persona (CUF11 paso 5) con uno de los medios pactados en CUF10.
    // Sin [TableColumnAttribute]: los datos de cada medio son NULL en los otros y el MappingHandler no convierte NULL.
    public class PagoCompra
    {
        public Guid PagoCompra_Id { get; set; }
        public Guid Compra_Id { get; set; }
        public Guid TipoPago_Id { get; set; }
        public Guid Cajero_Id { get; set; }
        public double Monto { get; set; }
        public string Banco { get; set; }
        public string CbuAlias { get; set; }
        public string NumeroOperacion { get; set; }
        public string NumeroCheque { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime FechaCreacion { get; set; }

        // No son columnas de AcquisitionPayments: descripcion del medio y nombre del cajero.
        public string TipoPago_Descripcion { get; set; }
        public string Cajero { get; set; }

    }
}
