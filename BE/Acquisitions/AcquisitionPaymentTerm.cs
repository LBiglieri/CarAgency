
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Monto pactado por medio de pago en CUF10 etapa A. La suma tiene que igualar Agreed_Price.
    public class AcquisitionPaymentTerm
    {
        [TableColumnAttribute]
        public Guid Acquisition_Id { get; set; }

        [TableColumnAttribute]
        public Guid PaymentType_Id { get; set; }

        [TableColumnAttribute]
        public double Amount { get; set; }

        // No son columnas de AcquisitionPaymentTerms: descripcion del medio y lo pagado en CUF11.
        [TableColumnAttribute]
        public string PaymentType_Description { get; set; }

        [TableColumnAttribute]
        public double Paid_Amount { get; set; }

    }
}
