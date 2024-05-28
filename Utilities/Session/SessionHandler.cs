using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Abstractions;

namespace CarAgency.Utilities.Session
{
    public static class SessionHandler
    {
        static IUser _user;

        public static IUser User
        {
            get { return _user; }
        }

        public static Boolean Logged()
        {
            return _user != null;
        }

        public static void Login(IUser user)
        {
            if (user != null)
                _user = user;
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
    }
}
