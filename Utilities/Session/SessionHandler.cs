using CarAgency.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Session;

namespace CarAgency.Utilities.Session
{
    public static class SessionHandler
    {
        static User _user;

        public static User User
        {
            get { return _user; }
        }

        public static Boolean Logged()
        {
            return _user != null;
        }

        public static void Login(User user)
        {
            if (user != null)
            {
                _user = user;
                LanguageService.LoadLanguage(_user.Language_Code);
            }

        }

        public static void Logout()
        {
            _user = null;
        }

        public static string GetUsername()
        {
            return _user.Username;
        }

        public static Guid GetId()
        {
            return _user.Id;
        }

        public static bool ValidatePassword(string NewPassword)
        {
            if(_user == null)
                return false;
            if (_user.Password == NewPassword)
                return false;
            return true;
        }

        public static bool IsAuthorized(PermissionType permission)
        {
            if (_user == null)
                return false;
            if (User.Role == null) 
                return false;

            return HasPermission(_user.Role, permission);
        }

        private static bool HasPermission(ComposedPermission c, PermissionType permission)
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
