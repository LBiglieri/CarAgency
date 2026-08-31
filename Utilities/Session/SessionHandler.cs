using CarAgency.Entities;
using CarAgency.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Session;

namespace CarAgency.Utilities.Session
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

        private User _user;

        public User User
        {
            get { return _user; }
        }

        public Boolean Logged()
        {
            return _user != null;
        }

        public void Login(User user)
        {
            if (user != null)
            {
                _user = user;
                LanguageService.LoadLanguage(_user.Language_Code);
            }

        }

        public void Logout()
        {
            _user = null;
        }

        public string GetUsername()
        {
            return _user.Username;
        }

        public Guid GetId()
        {
            return _user.Id;
        }

        public bool ValidatePassword(string NewPassword)
        {
            if(_user == null)
                return false;
            if (CryptographyHandler.VerifyPassword(NewPassword, _user.Password))
                return false;
            return true;
        }

        public bool IsAuthorized(PermissionType permission)
        {
            if (_user == null)
                return false;
            if (User.Role == null) 
                return false;

            return HasPermission(_user.Role, permission);
        }

        private bool HasPermission(ComposedPermission c, PermissionType permission)
        {
            bool exists = false;

            if (c.Type.Equals(permission))
                exists = true;
            else
                foreach (var item in c.Children)
                {
                    exists = HasPermission(item, permission);
                    if (exists) return true;
                }

            return exists;
        }
    }
}
