using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarAgency.BE;

using CarAgency.Security.Security;
using CarAgency.Security.Persistence;
using CarAgency.Security.Session;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.Session
{
    public class UserBLL
    {
        private UserDataMapper _userDataMapper;
        public UserBLL()
        {
            _userDataMapper = new UserDataMapper();
        }
        public User GetFullUserById(Guid id)
        {
            return _userDataMapper.GetFullUserById(id);
        }
        public List<User> GetAllByState(Boolean Active)
        {
            return _userDataMapper.GetAllByState(Active);
        }

        public SQLUpdateResult AddUser(User user)
        {
            User validationUser;
            validationUser = _userDataMapper.GetUserByUsername(user.Username);
            if (validationUser != null)
                throw new Exception("A User with this Username already exists.");
            validationUser = _userDataMapper.GetUserByDni(user.Dni);
            if (validationUser != null)
                throw new Exception("A User with this DNI already exists.");

            user.Id = Guid.NewGuid();
            user.Available_Login_Attempts = 3;
            user.Blocked = false;
            user.Active = true;
            user.Password = CryptographyHandler.HashPassword(user.Dni.ToString());
            user.Language_Code = "es";
            return _userDataMapper.AddUser(user);
        }

        public SQLUpdateResult UpdateUser(User user)
        {
            User validationUser;
            validationUser = _userDataMapper.GetUserByUsername(user.Username);
            if (validationUser != null && validationUser.Id != user.Id)
                throw new Exception("A User with this Username already exists.");
            validationUser = _userDataMapper.GetUserByDni(user.Dni);
            if (validationUser != null && validationUser.Id != user.Id)
                throw new Exception("A User with this DNI already exists.");

            return _userDataMapper.UpdateUser(user);
        }

        public SQLUpdateResult AlterBlockedState(User user, Boolean state)
        {
            user.Blocked = state;
            if (!state)
            {
                user.Available_Login_Attempts = 3;
                user.Password = CryptographyHandler.HashPassword(user.Dni.ToString());
            }

            return _userDataMapper.UpdateUser(user);
        }

        public SQLUpdateResult AddFailedLoginAttempt(User user)
        {
            user.Available_Login_Attempts -= 1;
            if (user.Available_Login_Attempts == 0)
                return AlterBlockedState(user, true);
            else
                return _userDataMapper.UpdateUser(user);
        }

        public SQLUpdateResult ChangePassword(Guid Id, string NewPassword)
        {
            SQLUpdateResult result;

            if (!SessionHandler.Instance.ValidatePassword(NewPassword))
            {
                result = new SQLUpdateResult(SQLResultType.validation_error, "The new password you entered is the same as the one you had before.");
            }
            else
            {
                User user = GetFullUserById(Id);
                user.Password = CryptographyHandler.HashPassword(NewPassword);
                result = _userDataMapper.UpdateUser(user);

                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    SessionHandler.Instance.Logout();
                    SessionHandler.Instance.Login(user);
                }
            }

            return result;
        }

        public SQLUpdateResult ChangeLanguage(Guid Id, string NewLanguage)
        {
            SQLUpdateResult result;

            User user = GetFullUserById(Id);
            user.Language_Code = NewLanguage;
            result = _userDataMapper.UpdateUser(user);

            if (result != null && result.sqlResult == SQLResultType.success)
            {
                SessionHandler.Instance.Logout();
                SessionHandler.Instance.Login(user);
            }

            return result;
        }

        public bool IsUsingDefaultPassword(Guid Id)
        {
            User user = GetFullUserById(Id);
            if(CryptographyHandler.VerifyPassword(user.Dni.ToString(), user.Password))
                return true;
            else
                return false;

        }

        public SQLUpdateResult DeleteUser(User user)
        {
            if (user.Username == SessionHandler.Instance.GetUsername())
                throw new Exception("You cant delete the User you are using.");

            return _userDataMapper.DeleteUser(user);
        }

        public void Logout()
        {
            if (!SessionHandler.Instance.Logged())
                throw new Exception("You are not logged in.");
            SessionHandler.Instance.Logout();
        }
        public RecoverySession Login(string username, string password)
        {
            if (SessionHandler.Instance.Logged())
                throw new Exception("You are already logged in.");

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) throw new Exception("Please complete all fields.");
            RecoverySession recovery = IntegrityService.Current.CheckLogin(username, password);
            if (recovery != null) return recovery;
            try
            {
                User user = _userDataMapper.GetUserByUsername(username);

                if (user == null)
                    throw new Exception("User doesnt exist.");
                if (!user.Active)
                    throw new Exception("User is inactive.");
                if (user.Blocked)
                    throw new Exception("User is blocked. Please contact Tech Support to get it unblocked.");
                if (!CryptographyHandler.VerifyPassword(password, user.Password))
                {
                    AddFailedLoginAttempt(user);
                    if (user.Available_Login_Attempts == 0)
                        throw new Exception("Incorrect password. You have no more available login attempts, your user was blocked. Please contact Tech Support to get it unblocked.");
                    else
                        throw new Exception("Incorrect password. You have " + user.Available_Login_Attempts.ToString() + " more available login attempts.");
                }

                user.Available_Login_Attempts = 3;

                UpdateUser(user);

                user = _userDataMapper.GetFullUserById(user.Id);

                SessionHandler.Instance.Login(user);
                return null;

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
