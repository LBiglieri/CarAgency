
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Vehiculo de la inspeccion seleccionada, con lo declarado en CUF07 (CUF08 paso 3).
    public class InspectionDetail
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Acquisition_Id { get; set; }

        [TableColumnAttribute]
        public int Number { get; set; }

        [TableColumnAttribute]
        public string License_Plate { get; set; }

        [TableColumnAttribute]
        public string Chassis_Number { get; set; }

        [TableColumnAttribute]
        public string Engine_Number { get; set; }

        [TableColumnAttribute]
        public string Vehicle_Description { get; set; }

        [TableColumnAttribute]
        public int Doors { get; set; }

        [TableColumnAttribute]
        public int Declared_Kilometers { get; set; }

    }
}
