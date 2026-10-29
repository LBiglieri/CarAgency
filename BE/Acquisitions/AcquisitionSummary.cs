
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Resumen completo de la compra para la etapa Finalizar (CUF10 paso 20) y la constancia.
    // Sin [TableColumnAttribute]: el MappingHandler no convierte NULL.
    public class AcquisitionSummary
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid Client_Id { get; set; }
        public int Dni { get; set; }
        public string License_Plate { get; set; }
        public string Chassis_Number { get; set; }
        public string Engine_Number { get; set; }
        public string Vehicle_Description { get; set; }
        public Guid Make_Id { get; set; }
        public Guid Model_Id { get; set; }
        public Guid Version_Id { get; set; }
        public Guid Colour_Id { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public int Declared_Kilometers { get; set; }
        public Guid Inspection_Id { get; set; }
        public DateTime? Performed_Date { get; set; }
        public int? Verified_Kilometers { get; set; }
        public double? Appraisal_Amount { get; set; }
        public DateTime? Appraisal_Date { get; set; }
        public double? Agreed_Price { get; set; }
        public string Owner_CUIL_CUIT { get; set; }
        public DateTime? Contract_Signed_Date { get; set; }
        public double Paid_Amount { get; set; }
        public string Precharge_Code_08 { get; set; }
        public DateTime? Transfer_Appointment_Date { get; set; }
        public DateTime? Closing_Date { get; set; }

    }
}
