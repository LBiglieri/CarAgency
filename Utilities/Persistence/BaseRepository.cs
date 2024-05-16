using CarAgency.Abstractions;
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
}
