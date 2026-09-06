using CarAgency.DAL.Persistence;

namespace CarAgency.Security.Session
{
    public abstract class SecurityMapperBase
    {
        protected string GetConnectionString()
        {
            return new DatabaseConnectionProvider().GetConnectionString();
        }
    }
}
