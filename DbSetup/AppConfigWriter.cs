using System.Linq;
using System.Xml.Linq;

namespace CarAgency.DbSetup
{
    // Apunta el UI.exe.config instalado a la instancia elegida en el instalador.
    // Solo toca la cadena de conexion: la clave CARAGENCY_DV_KEY tiene que seguir siendo la original.
    public static class AppConfigWriter
    {
        public static void SetConnectionString(string configPath, string name, string connectionString)
        {
            XDocument document = XDocument.Load(configPath, LoadOptions.PreserveWhitespace);
            XElement configuration = document.Root;
            XElement section = configuration.Element("connectionStrings");
            if (section == null)
            {
                section = new XElement("connectionStrings");
                configuration.AddFirst(section);
            }

            XElement entry = section.Elements("add").FirstOrDefault(e => (string)e.Attribute("name") == name);
            if (entry == null)
            {
                entry = new XElement("add", new XAttribute("name", name));
                section.Add(entry);
            }
            entry.SetAttributeValue("connectionString", connectionString);
            entry.SetAttributeValue("providerName", "System.Data.SqlClient");
            document.Save(configPath, SaveOptions.DisableFormatting);
        }
    }
}
