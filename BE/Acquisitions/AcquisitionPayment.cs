
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Pago del cajero al cliente (CUF11 paso 5) con uno de los medios pactados en CUF10.
    // Sin [TableColumnAttribute]: los datos de cada medio son NULL en los otros y el MappingHandler no convierte NULL.
    public class AcquisitionPayment
    {
        public Guid Id { get; set; }
        public Guid Acquisition_Id { get; set; }
        public Guid PaymentType_Id { get; set; }
        public Guid Cashier_Id { get; set; }
        public double Amount { get; set; }
        public string Bank { get; set; }
        public string Cbu_Alias { get; set; }
        public string Operation_Number { get; set; }
        public string Check_Number { get; set; }
        public DateTime? Payment_Date { get; set; }
        public DateTime Creation_Date { get; set; }

        // No son columnas de AcquisitionPayments: descripcion del medio y nombre del cajero.
        public string PaymentType_Description { get; set; }
        public string Cashier { get; set; }

    }
}
