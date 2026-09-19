using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;

namespace CarAgency.DAL.Persistence
{
    // Ejecuta stored procedures y devuelve DataTable o filas afectadas. No conoce entidades de BE:
    // convertir el resultado en objetos es trabajo de los mappers.
    public abstract class DataAccessBase
    {
        private readonly string connectionString = new DatabaseConnectionProvider().GetConnectionString();

        protected DataTable Read(string procedure, params SqlParameter[] parameters)
        {
            return Read(procedure, (DvhCalculator)null, parameters);
        }

        // dvh, si viene, agrega el digito verificador horizontal al comando antes de ejecutarlo.
        protected DataTable Read(string procedure, DvhCalculator dvh, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = Command(procedure, connection, dvh, parameters))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
        }

        protected int Execute(string procedure, DvhCalculator dvh, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = Command(procedure, connection, dvh, parameters))
            {
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        private static SqlCommand Command(string procedure, SqlConnection connection, DvhCalculator dvh, SqlParameter[] parameters)
        {
            var command = new SqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddRange(parameters);
            if (dvh != null) DvhCommand.Apply(command, dvh);
            return command;
        }
    }
}
