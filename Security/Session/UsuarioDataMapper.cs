using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Session;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.Session
{
    public class UsuarioDataMapper
    {
        private const string Table = "Usuario";
        private readonly UsuarioDataAccess data = new UsuarioDataAccess();

        public Usuario GetFullUserById(Guid Id)
        {
            Usuario user = MappingHandler.MapTableToEntities<Usuario>(data.GetById(Id)).FirstOrDefault();
            if (user == null) return null;
            new PermisoMapper().FillUserRole(user);
            return user;
        }

        public Usuario GetUserByUsername(string Username)
        {
            return MappingHandler.MapTableToEntities<Usuario>(data.GetByUsername(Username)).FirstOrDefault();
        }

        public Usuario GetUserByDni(int Dni)
        {
            return MappingHandler.MapTableToEntities<Usuario>(data.GetByDni(Dni)).FirstOrDefault();
        }

        public List<Usuario> GetAllByState(Boolean Active)
        {
            return MappingHandler.MapTableToEntities<Usuario>(data.GetAllByState(Active));
        }

        public SQLUpdateResult AddUser(Usuario user)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(user.Usuario_Id, user.DNI, user.NombreUsuario, user.Clave, user.Nombre,
                user.Apellido, user.Rol_Id, user.Bloqueado, user.Activo, user.IntentosDisponibles, user.CodigoIdioma, dvh));
        }

        public SQLUpdateResult UpdateUser(Usuario user)
        {
            // Contrato: Password vacio significa "conservar el hash actual". Las
            // proyecciones parciales (User_GetAllByState) lo enmascaran con '', y ese
            // mismo objeto vuelve desde la UI para el update; sin esto, el UPDATE de
            // fila completa pisaria la contrasena real.
            string password = user.Clave;
            if (string.IsNullOrEmpty(password))
            {
                password = GetStoredPassword(user.Usuario_Id);
                if (password == null)
                    throw new Exception("Cannot update the User: the User no longer exists.");
            }

            return DigitVerifierWriteMapper.Save(Table, dvh => data.Update(user.Usuario_Id, user.DNI, user.NombreUsuario, password, user.Nombre,
                user.Apellido, user.Rol_Id, user.Bloqueado, user.Activo, user.IntentosDisponibles, user.CodigoIdioma, dvh));
        }

        public SQLUpdateResult DeleteUser(Usuario user)
        {
            // El borrado arrastra en cascada los eventos del usuario (FK Events -> Users ON DELETE
            // CASCADE): cambia el conjunto de DVH de Events y hay que rehacer su DVV junto con el de Users.
            return DigitVerifierWriteMapper.Remove(() => data.Delete(user.Usuario_Id), Table, "BitacoraEventos");
        }

        private string GetStoredPassword(Guid Id)
        {
            using (DataTable table = data.GetById(Id))
            {
                if (table.Rows.Count == 0 || table.Rows[0].IsNull("Clave")) return null;
                return (string)table.Rows[0]["Clave"];
            }
        }
    }
}
