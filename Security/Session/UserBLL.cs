using BE;
using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarAgency.BE;

using CarAgency.Security.Security;
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
            return RecordResult(_userDataMapper.AddUser(user), AuditEventType.UserCreated, user);
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

            return RecordResult(_userDataMapper.UpdateUser(user), AuditEventType.UserUpdated, user);
        }

        public SQLUpdateResult AlterBlockedState(User user, Boolean state)
        {
            user.Blocked = state;
            if (!state)
            {
                user.Available_Login_Attempts = 3;
                user.Password = CryptographyHandler.HashPassword(user.Dni.ToString());
            }

            return RecordResult(_userDataMapper.UpdateUser(user), state ? AuditEventType.UserBlocked : AuditEventType.UserUnblocked, user, SessionHandler.Instance.User ?? user);
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
                    SessionHandler.Instance.RefreshUser(user);
                    AuditBLL.Record(AuditEventType.PasswordChanged, user.Id);
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
                SessionHandler.Instance.RefreshUser(user);
                AuditBLL.Record(AuditEventType.LanguageChanged, user.Id);
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

            return RecordResult(_userDataMapper.DeleteUser(user), AuditEventType.UserDeleted, user);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, AuditEventType type, User target, User actor = null)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(type, target.Id, actor);
            return result;
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
                {
                    AuditBLL.Record(AuditEventType.LoginFailed, attemptedLogin: username.Length > 256 ? username.Substring(0, 256) : username);
                    throw new Exception("User doesnt exist.");
                }
                if (!user.Active)
                {
                    AuditBLL.Record(AuditEventType.LoginFailed, user.Id, user);
                    throw new Exception("User is inactive.");
                }
                if (user.Blocked)
                {
                    AuditBLL.Record(AuditEventType.LoginFailed, user.Id, user);
                    throw new Exception("User is blocked. Please contact Tech Support to get it unblocked.");
                }
                if (!CryptographyHandler.VerifyPassword(password, user.Password))
                {
                    var failedResult = AddFailedLoginAttempt(user);
                    if (failedResult == null || failedResult.sqlResult != SQLResultType.success)
                        throw new TranslatableException("AuditLoginUpdateFailed", "No se pudo actualizar el estado de acceso del usuario.");
                    AuditBLL.Record(AuditEventType.LoginFailed, user.Id, user);
                    if (user.Available_Login_Attempts == 0)
                        throw new Exception("Incorrect password. You have no more available login attempts, your user was blocked. Please contact Tech Support to get it unblocked.");
                    else
                        throw new Exception("Incorrect password. You have " + user.Available_Login_Attempts.ToString() + " more available login attempts.");
                }

                user.Available_Login_Attempts = 3;

                var resetResult = _userDataMapper.UpdateUser(user);
                if (resetResult == null || resetResult.sqlResult != SQLResultType.success)
                    throw new TranslatableException("AuditLoginUpdateFailed", "No se pudo actualizar el estado de acceso del usuario.");

                user = _userDataMapper.GetFullUserById(user.Id);

                SessionHandler.Instance.Login(user);
                return null;

            }
            catch (TranslatableException) { throw; }
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
