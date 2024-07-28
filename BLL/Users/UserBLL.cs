using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;

namespace CarAgency.BLL
{
    public class UserBLL
    {
        private UserRepository _userrepository;
        public UserBLL()
        {
            _userrepository = new UserRepository();
        }
        public User GetFullUserById(Guid id)
        {
            return _userrepository.GetFullUserById(id);
        }
        public List<User> GetAllByState(Boolean Active)
        {
            return _userrepository.GetAllByState(Active);
        }

        public SQLUpdateResult AddUser(User user)
        {
            User validationUser;
            validationUser = _userrepository.GetUserByUsername(user.Username);
            if (validationUser != null)
                throw new Exception("A User with this Username already exists.");
            validationUser = _userrepository.GetUserByDni(user.Dni);
            if (validationUser != null)
                throw new Exception("A User with this DNI already exists.");

            user.Id = Guid.NewGuid();
            user.Available_Login_Attempts = 3;
            user.Blocked = false;
            user.Active = true;
            user.Password = CryptographyHandler.GenerateSHA512Hash(user.Dni.ToString());
            user.Language_Code = "es";
            return _userrepository.AddUser(user);
        }

        public SQLUpdateResult UpdateUser(User user)
        {
            User validationUser;
            validationUser = _userrepository.GetUserByUsername(user.Username);
            if (validationUser != null && validationUser.Id != user.Id)
                throw new Exception("A User with this Username already exists.");
            validationUser = _userrepository.GetUserByDni(user.Dni);
            if (validationUser != null && validationUser.Id != user.Id)
                throw new Exception("A User with this DNI already exists.");

            return _userrepository.UpdateUser(user);
        }

        public SQLUpdateResult AlterBlockedState(User user, Boolean state)
        {
            user.Blocked = state;
            if (!state)
            {
                user.Available_Login_Attempts = 3;
                user.Password = CryptographyHandler.GenerateSHA512Hash(user.Dni.ToString());
            }

            return _userrepository.UpdateUser(user);
        }

        public SQLUpdateResult AddFailedLoginAttempt(User user)
        {
            user.Available_Login_Attempts -= 1;
            if (user.Available_Login_Attempts == 0)
                return AlterBlockedState(user, true);
            else
                return _userrepository.UpdateUser(user);
        }

        public SQLUpdateResult ChangePassword(Guid Id, string NewPassword)
        {
            SQLUpdateResult result;

            NewPassword = CryptographyHandler.GenerateSHA512Hash(NewPassword);
            if (!SessionHandler.ValidatePassword(NewPassword))
            {
                result = new SQLUpdateResult(SQLResultType.validation_error, "The new password you entered is the same as the one you had before.");
            }
            else
            {
                User user = GetFullUserById(Id);
                user.Password = NewPassword;
                result = _userrepository.UpdateUser(user);

                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    SessionHandler.Logout();
                    SessionHandler.Login(user);
                }
            }

            return result;
        }

        public SQLUpdateResult ChangeLanguage(Guid Id, string NewLanguage)
        {
            SQLUpdateResult result;

            User user = GetFullUserById(Id);
            user.Language_Code = NewLanguage;
            result = _userrepository.UpdateUser(user);

            if (result != null && result.sqlResult == SQLResultType.success)
            {
                SessionHandler.Logout();
                SessionHandler.Login(user);
            }

            return result;
        }

        public bool IsUsingDefaultPassword(Guid Id)
        {
            User user = GetFullUserById(Id);
            if(user.Password == CryptographyHandler.GenerateSHA512Hash(user.Dni.ToString()))
                return true;
            else
                return false;

        }

        public SQLUpdateResult DeleteUser(User user)
        {
            if (user.Username == SessionHandler.GetUsername())
                throw new Exception("You cant delete the User you are using.");

            return _userrepository.DeleteUser(user);
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
                if (user.Blocked)
                    throw new Exception("User is blocked. Please contact Tech Support to get it unblocked.");
                if (!CryptographyHandler.GenerateSHA512Hash(password).Equals(user.Password))
                {
                    AddFailedLoginAttempt(user);
                    if (user.Available_Login_Attempts == 0)
                        throw new Exception("Incorrect password. You have no more available login attempts, your user was blocked. Please contact Tech Support to get it unblocked.");
                    else
                        throw new Exception("Incorrect password. You have " + user.Available_Login_Attempts.ToString() + " more available login attempts.");
                }

                user.Available_Login_Attempts = 3;

                UpdateUser(user);

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
