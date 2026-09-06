using CarAgency.DAL.Persistence;

namespace CarAgency.Mappers.Persistence
{
    public abstract class MapperBase
    {
        protected string GetConnectionString()
        {
            return new DatabaseConnectionProvider().GetConnectionString();
        }
    }
}
