using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Persona
    {
        [TableColumnAttribute]
        public Guid Persona_Id { get; set; }

        [TableColumnAttribute]
        public int DNI { get; set; }

        [TableColumnAttribute]
        public string Nombre { get; set; }

        [TableColumnAttribute]
        public string Apellido { get; set; }

        [TableColumnAttribute]
        public string Direccion { get; set; }

        [TableColumnAttribute]
        public string TelefonoPersonal { get; set; }

        [TableColumnAttribute]
        public string TelefonoCasa { get; set; }

        [TableColumnAttribute]
        public string Email { get; set; }

        [TableColumnAttribute]
        public DateTime FechaNacimiento { get; set; }
    }
}
