
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Factura
    {
        [TableColumnAttribute]
        public Guid Factura_Id { get; set; }

        [TableColumnAttribute]
        public Guid Vehiculo_Id { get; set; }

        [TableColumnAttribute]
        public string Vehiculo_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Persona_Id { get; set; }

        [TableColumnAttribute]
        public Guid Reserva_Id { get; set; }

        [TableColumnAttribute]
        public string Detalle { get; set; }

        [TableColumnAttribute]
        public string CuilCuit { get; set; }

        [TableColumnAttribute]
        public string RazonSocial { get; set; }

        [TableColumnAttribute]
        public double Valor { get; set; }

        [TableColumnAttribute]
        public Boolean EstadoPago { get; set; }

        [TableColumnAttribute]
        public DateTime Fecha { get; set; }

    }
}
