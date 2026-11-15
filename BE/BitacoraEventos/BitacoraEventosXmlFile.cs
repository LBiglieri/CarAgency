using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CarAgency.BE.BitacoraEventos
{
    // Forma del archivo XML de la bitácora exportada. Es una lista blanca: solo lleva lo que
    // el usuario ve en el formulario. Los Id, el login intentado y el DVH no se exportan.
    [XmlRoot("BitacoraEventos")]
    public sealed class BitacoraEventosXmlFile
    {
        [XmlElement("Evento")]
        public List<BitacoraEventoXmlEntry> Eventos { get; set; } = new List<BitacoraEventoXmlEntry>();
    }

    public sealed class BitacoraEventoXmlEntry
    {
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha { get; set; }
        public BitacoraEventosModule Modulo { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public int Criticidad { get; set; }
    }
}
