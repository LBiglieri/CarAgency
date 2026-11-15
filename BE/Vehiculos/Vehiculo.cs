using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Vehiculo
    {
        [TableColumnAttribute]
        public Guid Vehiculo_Id { get; set; }

        [TableColumnAttribute]
        public string Patente { get; set; }

        [TableColumnAttribute]
        public Guid Marca_Id { get; set; }

        [TableColumnAttribute]
        public string Marca_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Modelo_Id { get; set; }

        [TableColumnAttribute]
        public string Modelo_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Version_Id { get; set; }

        [TableColumnAttribute]
        public string Version_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Color_Id { get; set; }

        [TableColumnAttribute]
        public string Color_Descripcion { get; set; }

        [TableColumnAttribute]
        public double Precio { get; set; }

        [TableColumnAttribute]
        public string Opcionales { get; set; }

        [TableColumnAttribute]
        public string Observaciones { get; set; }

        [TableColumnAttribute]
        public int Puertas { get; set; }

        [TableColumnAttribute]
        public int Anio { get; set; }

        [TableColumnAttribute]
        public int Kilometraje { get; set; }

        [TableColumnAttribute]
        public string LinkImagen { get; set; }

    }
}
