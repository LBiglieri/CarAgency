
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.BE
{
    public class Tramite
    {
        [TableColumnAttribute]
        public Guid Tramite_Id { get; set; }

        [TableColumnAttribute]
        public Guid Vehiculo_Id { get; set; }

        [TableColumnAttribute]
        public string Vehiculo_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Persona_Id { get; set; }
        public string Persona_Descripcion { get; set; }

        [TableColumnAttribute]
        public Guid Factura_Id { get; set; }

        [TableColumnAttribute]
        public string CodigoPrecarga08 { get; set; }

        [TableColumnAttribute]
        public DateTime FechaTransferencia { get; set; }

        [TableColumnAttribute]
        public string Observaciones { get; set; }

        [TableColumnAttribute]
        public Boolean Finalizado { get; set; }

    }
}
