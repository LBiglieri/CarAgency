
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Vehiculo que ofrece la persona. Pasa a Vehicles recien cuando se completa la adquisicion.
    public class VehiculoProspecto
    {
        [TableColumnAttribute]
        public Guid VehiculoProspecto_Id { get; set; }

        [TableColumnAttribute]
        public string Patente { get; set; }

        [TableColumnAttribute]
        public string NumeroChasis { get; set; }

        [TableColumnAttribute]
        public string NumeroMotor { get; set; }

        [TableColumnAttribute]
        public Guid Marca_Id { get; set; }

        [TableColumnAttribute]
        public Guid Modelo_Id { get; set; }

        [TableColumnAttribute]
        public Guid Version_Id { get; set; }

        [TableColumnAttribute]
        public Guid Color_Id { get; set; }

        [TableColumnAttribute]
        public int Anio { get; set; }

        [TableColumnAttribute]
        public int Puertas { get; set; }

        [TableColumnAttribute]
        public int Kilometraje { get; set; }

    }
}
