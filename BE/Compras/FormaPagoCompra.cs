
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Monto pactado por medio de pago en CUF10 etapa A. La suma tiene que igualar Agreed_Price.
    public class FormaPagoCompra
    {
        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public Guid TipoPago_Id { get; set; }

        [TableColumnAttribute]
        public double Monto { get; set; }

        // No son columnas de AcquisitionPaymentTerms: descripcion del medio y lo pagado en CUF11.
        [TableColumnAttribute]
        public string TipoPago_Descripcion { get; set; }

        [TableColumnAttribute]
        public double MontoPagado { get; set; }

    }
}
