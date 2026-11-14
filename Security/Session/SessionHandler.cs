using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using CarAgency.BE;
using CarAgency.Security.Security;
using CarAgency.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Session;

namespace CarAgency.Security.Session
{
    public sealed class SessionHandler
    {
        // volatile impide que se reordene la escritura y que otro hilo
        // llegue a ver la referencia antes de que el objeto este construido.
        private static volatile SessionHandler _instance;
        private static readonly object _lock = new object();

        // Constructor privado: nadie fuera de esta clase puede instanciarla.
        private SessionHandler() { }

        public static SessionHandler Instance
        {
            get
            {
                // Doble verificacion: el primer check evita tomar el cerrojo
                // en cada acceso; el segundo es el que realmente decide.
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionHandler();
                        }
                    }
                }
                return _instance;
            }
        }

        private Usuario _user;

        public Usuario User
        {
            get { return _user; }
        }

        public Boolean Logged()
        {
            return _user != null;
        }

        public void Login(Usuario user)
        {
            if (user != null)
            {
                try
                {
                    RefreshUser(user);
                    BitacoraEventosBLL.Record(TipoEvento.Login);
                }
                catch { SetUser(null); throw; }
            }

        }

        public void Logout()
        {
            if (_user == null) return;
            try { BitacoraEventosBLL.Record(TipoEvento.Logout); }
            finally { SetUser(null); }
        }

        internal void RefreshUser(Usuario user)
        {
            SetUser(user);
            LanguageService.LoadLanguage(user.CodigoIdioma);
        }

        // El usuario de la sesion tambien viaja en cada conexion: los triggers de la bitacora de
        // cambios lo leen de SESSION_CONTEXT, porque la base se usa siempre con el mismo login.
        private void SetUser(Usuario user)
        {
            _user = user;
            DatabaseSessionContext.UsuarioId = user == null ? (Guid?)null : user.Usuario_Id;
        }

        public string GetUsername()
        {
            return _user.NombreUsuario;
        }

        public Guid GetId()
        {
            return _user.Usuario_Id;
        }

        public bool ValidatePassword(string NewPassword)
        {
            if(_user == null)
                return false;
            if (CryptographyHandler.VerifyPassword(NewPassword, _user.Clave))
                return false;
            return true;
        }

        public bool IsAuthorized(PermisoType permission)
        {
            if (_user == null)
                return false;
            if (User.Rol == null) 
                return false;

            return HasPermission(_user.Rol, permission);
        }

        private bool HasPermission(ComposedPermiso c, PermisoType permission)
        {
            return HasPermission(c, permission, new HashSet<Guid>());
        }

        private bool HasPermission(ComposedPermiso component, PermisoType permission, HashSet<Guid> visited)
        {
            if (component == null || !visited.Add(component.Permiso_Id)) return false;
            if (component is Patente) return component.Tipo == permission;
            return component.Children.Any(child => HasPermission(child, permission, visited));
        }
    }
}
