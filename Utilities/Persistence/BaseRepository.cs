using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Utilities.Persistence
{
    public abstract class BaseRepository
    {
        public string GetConnectionString()
        {
            var cs = new SqlConnectionStringBuilder();
            cs.InitialCatalog = "CarAgency";
            cs.DataSource = "PersDev-01";
            cs.IntegratedSecurity = true;
            return cs.ConnectionString;
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
