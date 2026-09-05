using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.DAL.Persistence
{
    public abstract class BaseRepository
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
    

    public class SQLUpdateResult
    {
        public SQLResultType sqlResult;
        public string message;
        public SQLUpdateResult(SQLResultType _sqlResult, string _message)
        {
            sqlResult = _sqlResult;
            message = _message;
        }
    }

    public enum SQLResultType
    {
        success,
        database_error,
        validation_error,
        registry_already_exists
    }
}
