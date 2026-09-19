using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.DAL.Persistence
{
    public class DatabaseConnectionProvider
    {
        public string GetConnectionString()
        {
            var setting = ConfigurationManager.ConnectionStrings["CarAgency"];

            if (setting == null)
                throw new ConfigurationErrorsException(
                    "No se encontro la cadena de conexion 'CarAgency' en el archivo de configuracion.");

            return setting.ConnectionString;
        }
    }
}
