using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Session;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.Session
{
    public class UserDataMapper
    {
        private const string Table = "Users";
        private readonly UserDataAccess data = new UserDataAccess();

        public User GetFullUserById(Guid Id)
        {
            User user = MappingHandler.MapTableToEntities<User>(data.GetById(Id)).FirstOrDefault();
            if (user == null) return null;
            new PermissionMapper().FillUserRole(user);
            return user;
        }

        public User GetUserByUsername(string Username)
        {
            return MappingHandler.MapTableToEntities<User>(data.GetByUsername(Username)).FirstOrDefault();
        }

        public User GetUserByDni(int Dni)
        {
            return MappingHandler.MapTableToEntities<User>(data.GetByDni(Dni)).FirstOrDefault();
        }

        public List<User> GetAllByState(Boolean Active)
        {
            return MappingHandler.MapTableToEntities<User>(data.GetAllByState(Active));
        }

        public SQLUpdateResult AddUser(User user)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(user.Id, user.Dni, user.Username, user.Password, user.Name,
                user.Surname, user.Role_Id, user.Blocked, user.Active, user.Available_Login_Attempts, user.Language_Code, dvh));
        }

        public SQLUpdateResult UpdateUser(User user)
        {
            // Contrato: Password vacio significa "conservar el hash actual". Las
            // proyecciones parciales (User_GetAllByState) lo enmascaran con '', y ese
            // mismo objeto vuelve desde la UI para el update; sin esto, el UPDATE de
            // fila completa pisaria la contrasena real.
            string password = user.Password;
            if (string.IsNullOrEmpty(password))
            {
                password = GetStoredPassword(user.Id);
                if (password == null)
                    throw new Exception("Cannot update the User: the User no longer exists.");
            }

            return DigitVerifierWriteMapper.Save(Table, dvh => data.Update(user.Id, user.Dni, user.Username, password, user.Name,
                user.Surname, user.Role_Id, user.Blocked, user.Active, user.Available_Login_Attempts, user.Language_Code, dvh));
        }

        public SQLUpdateResult DeleteUser(User user)
        {
            // El borrado arrastra en cascada los eventos del usuario (FK Events -> Users ON DELETE
            // CASCADE): cambia el conjunto de DVH de Events y hay que rehacer su DVV junto con el de Users.
            return DigitVerifierWriteMapper.Remove(() => data.Delete(user.Id), Table, "Events");
        }

        private string GetStoredPassword(Guid Id)
        {
            using (DataTable table = data.GetById(Id))
            {
                if (table.Rows.Count == 0 || table.Rows[0].IsNull("Password")) return null;
                return (string)table.Rows[0]["Password"];
            }
        }
    }
}
