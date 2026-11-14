using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Session
{
    public sealed class UsuarioDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Usuario_GetUserById", new SqlParameter("@Usuario_Id", id)); }
        public DataTable GetByUsername(string username) { return Read("Usuario_GetUserByUsername", new SqlParameter("@NombreUsuario", username)); }
        public DataTable GetByDni(int dni) { return Read("Usuario_GetUserByDni", new SqlParameter("@DNI", dni)); }
        public DataTable GetAllByState(bool active) { return Read("Usuario_GetAllByState", new SqlParameter("@Activo", active)); }

        public DataTable Add(Guid id, int dni, string username, string password, string name, string surname, Guid roleId,
            bool blocked, bool active, int availableLoginAttempts, string languageCode, DvhCalculator dvh)
        {
            return Read("Usuario_Add", dvh, UserParameters(id, dni, username, password, name, surname, roleId,
                blocked, active, availableLoginAttempts, languageCode));
        }

        public DataTable Update(Guid id, int dni, string username, string password, string name, string surname, Guid roleId,
            bool blocked, bool active, int availableLoginAttempts, string languageCode, DvhCalculator dvh)
        {
            return Read("Usuario_Update", dvh, UserParameters(id, dni, username, password, name, surname, roleId,
                blocked, active, availableLoginAttempts, languageCode));
        }

        public DataTable Delete(Guid id) { return Read("Usuario_Delete", new SqlParameter("@Usuario_Id", id)); }

        private static SqlParameter[] UserParameters(Guid id, int dni, string username, string password, string name,
            string surname, Guid roleId, bool blocked, bool active, int availableLoginAttempts, string languageCode)
        {
            return new[]
            {
                new SqlParameter("@Usuario_Id", id),
                new SqlParameter("@DNI", dni),
                new SqlParameter("@NombreUsuario", username),
                new SqlParameter("@Clave", password),
                new SqlParameter("@Nombre", name),
                new SqlParameter("@Apellido", surname),
                new SqlParameter("@Rol_Id", roleId),
                new SqlParameter("@Bloqueado", blocked),
                new SqlParameter("@Activo", active),
                new SqlParameter("@IntentosDisponibles", availableLoginAttempts),
                new SqlParameter("@CodigoIdioma", languageCode)
            };
        }
    }
}
