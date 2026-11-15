
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Compra de la persona para el cajero (CUF11 paso 2): pendiente de pago o con el comprobante ya emitido.
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class PayableCompra
    {
        public Guid Compra_Id { get; set; }
        public int Numero { get; set; }
        public string Patente { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public double PrecioAcordado { get; set; }
        public double MontoPagado { get; set; }
        public int? ComprobanteCompra_Numero { get; set; }
        public CompraStatus Estado { get; set; }

        // Lo que falta pagar, para la grilla. Las validaciones de saldo las hace la BLL.
        public double Balance { get { return PrecioAcordado - MontoPagado; } }

    }
}
