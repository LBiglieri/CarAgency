using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CarAgency.BE.BitacoraEventos;

namespace CarAgency.Security.BitacoraEventos
{
    public static class BitacoraEventosXmlSerializer
    {
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(BitacoraEventosXmlFile));

        public static void Write(IEnumerable<BitacoraEvento> events, string path)
        {
            var file = new BitacoraEventosXmlFile();
            file.Eventos.AddRange(events.Select(e => new BitacoraEventoXmlEntry {
                Login = e.Login, Nombre = e.Nombre, Apellido = e.Apellido, Fecha = e.Fecha,
                Modulo = e.Modulo, TipoEvento = e.TipoEvento, Criticidad = e.Criticidad
            }));
            var settings = new XmlWriterSettings { Indent = true, Encoding = new UTF8Encoding(false) };
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                var noNamespaces = new XmlSerializerNamespaces();
                noNamespaces.Add("", "");
                serializer.Serialize(writer, file, noNamespaces);
            }
        }

        public static List<BitacoraEvento> Read(string path)
        {
            // Sin DTD ni resolución de entidades externas: el archivo viene de afuera del sistema.
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null };
            BitacoraEventosXmlFile file;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var reader = XmlReader.Create(stream, settings))
                file = (BitacoraEventosXmlFile)serializer.Deserialize(reader);

            if (file == null || file.Eventos == null)
                throw new InvalidDataException("El archivo no contiene una bitácora.");
            return file.Eventos.Select(ToEvent).ToList();
        }

        private static BitacoraEvento ToEvent(BitacoraEventoXmlEntry entry)
        {
            if (entry == null || entry.Fecha == default(DateTime)
                || !Enum.IsDefined(typeof(BitacoraEventosModule), entry.Modulo)
                || !Enum.IsDefined(typeof(TipoEvento), entry.TipoEvento)
                || entry.Criticidad < 1 || entry.Criticidad > 5)
                throw new InvalidDataException("El archivo tiene un evento incompleto o inválido.");
            return new BitacoraEvento {
                Login = entry.Login ?? "", Nombre = entry.Nombre ?? "", Apellido = entry.Apellido ?? "",
                Fecha = entry.Fecha, Modulo = entry.Modulo, TipoEvento = entry.TipoEvento, Criticidad = entry.Criticidad
            };
        }
    }
}
