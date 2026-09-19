using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Session
{
    public sealed class UserDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("User_GetUserById", new SqlParameter("@Id", id)); }
        public DataTable GetByUsername(string username) { return Read("User_GetUserByUsername", new SqlParameter("@Username", username)); }
        public DataTable GetByDni(int dni) { return Read("User_GetUserByDni", new SqlParameter("@Dni", dni)); }
        public DataTable GetAllByState(bool active) { return Read("User_GetAllByState", new SqlParameter("@Active", active)); }

        public DataTable Add(Guid id, int dni, string username, string password, string name, string surname, Guid roleId,
            bool blocked, bool active, int availableLoginAttempts, string languageCode, DvhCalculator dvh)
        {
            return Read("User_Add", dvh, UserParameters(id, dni, username, password, name, surname, roleId,
                blocked, active, availableLoginAttempts, languageCode));
        }

        public DataTable Update(Guid id, int dni, string username, string password, string name, string surname, Guid roleId,
            bool blocked, bool active, int availableLoginAttempts, string languageCode, DvhCalculator dvh)
        {
            return Read("User_Update", dvh, UserParameters(id, dni, username, password, name, surname, roleId,
                blocked, active, availableLoginAttempts, languageCode));
        }

        public DataTable Delete(Guid id) { return Read("User_Delete", new SqlParameter("@Id", id)); }

        private static SqlParameter[] UserParameters(Guid id, int dni, string username, string password, string name,
            string surname, Guid roleId, bool blocked, bool active, int availableLoginAttempts, string languageCode)
        {
            return new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Dni", dni),
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@Name", name),
                new SqlParameter("@Surname", surname),
                new SqlParameter("@Role_Id", roleId),
                new SqlParameter("@Blocked", blocked),
                new SqlParameter("@Active", active),
                new SqlParameter("@Available_Login_Attempts", availableLoginAttempts),
                new SqlParameter("@Language_Code", languageCode)
            };
        }
    }
}
