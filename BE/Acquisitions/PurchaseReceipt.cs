
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Comprobante de compra (CUF11 paso 7). Como Invoice, guarda los datos tal como se imprimieron.
    public class PurchaseReceipt
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public int Number { get; set; }

        [TableColumnAttribute]
        public Guid Acquisition_Id { get; set; }

        [TableColumnAttribute]
        public DateTime Issue_Date { get; set; }

        [TableColumnAttribute]
        public string Client_Name { get; set; }

        [TableColumnAttribute]
        public string Client_CUIL_CUIT { get; set; }

        [TableColumnAttribute]
        public string Detail { get; set; }

        [TableColumnAttribute]
        public double Net_Amount { get; set; }

        [TableColumnAttribute]
        public double Tax_Amount { get; set; }

        [TableColumnAttribute]
        public double Total_Amount { get; set; }

    }
}
