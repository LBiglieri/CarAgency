
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Reserva
    {
        [TableColumnAttribute]
        public Guid Reserva_Id { get; set; }

        [TableColumnAttribute]
        public Guid Vehiculo_Id { get; set; }

        [TableColumnAttribute]
        public string Vehiculo_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Persona_Id { get; set; }

        // No es columna: la arma el mapper con los datos desencriptados de la persona.
        public string Persona_Descripcion { get; set; }

        [TableColumnAttribute]
        public double Precio { get; set; }

        [TableColumnAttribute]
        public DateTime Fecha { get; set; }

        [TableColumnAttribute]
        public DateTime FechaVencimiento { get; set; }

    }
}
