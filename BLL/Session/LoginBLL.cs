using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;

namespace CarAgency.BLL
{
    public class LoginBLL
    {
        private UserRepository _userrepository;
        public LoginBLL()
        {
            _userrepository = new UserRepository();
        }
        public void Logout()
        {
            if (!SessionHandler.Logged())
                throw new Exception("You are not logged in.");
            SessionHandler.Logout();
        }
        public void Login(string username, string password)
        {
            if (SessionHandler.Logged())
                throw new Exception("You are already logged in.");

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) throw new Exception("Please complete all fields.");
            try
            {
                User user = _userrepository.GetUserByUsername(username);

                if (user == null) 
                    throw new Exception("User doesnt exist.");
                if (!CryptographyHandler.GenerateSHA512Hash(password).Equals(user.Password))
                    throw new Exception("Incorrect password.");

                user = _userrepository.GetFullUserById(user.Id);

                SessionHandler.Login(user);

            }
            catch (Exception e)
            {
                if (e.Message != "")
                    throw new Exception(e.Message);
                else
                    throw new Exception("Error logging in to the application.");
            }

        }
    }
}
