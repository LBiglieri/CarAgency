
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Compra del cliente para el cajero (CUF11 paso 2): pendiente de pago o con el comprobante ya emitido.
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public class PayableAcquisition
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string License_Plate { get; set; }
        public string Vehicle_Description { get; set; }
        public double Agreed_Price { get; set; }
        public double Paid_Amount { get; set; }
        public int? Receipt_Number { get; set; }
        public AcquisitionStatus Status { get; set; }

        // Lo que falta pagar, para la grilla. Las validaciones de saldo las hace la BLL.
        public double Balance { get { return Agreed_Price - Paid_Amount; } }

    }
}
