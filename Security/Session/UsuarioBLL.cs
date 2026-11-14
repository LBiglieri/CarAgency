using BE;
using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
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
    public class UsuarioBLL
    {
        private UsuarioDataMapper _userDataMapper;
        public UsuarioBLL()
        {
            _userDataMapper = new UsuarioDataMapper();
        }
        public Usuario GetFullUserById(Guid id)
        {
            return _userDataMapper.GetFullUserById(id);
        }
        public List<Usuario> GetAllByState(Boolean Active)
        {
            return _userDataMapper.GetAllByState(Active);
        }

        public SQLUpdateResult AddUser(Usuario user)
        {
            Usuario validationUser;
            validationUser = _userDataMapper.GetUserByUsername(user.NombreUsuario);
            if (validationUser != null)
                throw new Exception("A User with this Username already exists.");
            validationUser = _userDataMapper.GetUserByDni(user.DNI);
            if (validationUser != null)
                throw new Exception("A User with this DNI already exists.");

            user.Usuario_Id = Guid.NewGuid();
            user.IntentosDisponibles = 3;
            user.Bloqueado = false;
            user.Activo = true;
            user.Clave = CryptographyHandler.HashPassword(user.DNI.ToString());
            user.CodigoIdioma = "es";
            return RecordResult(_userDataMapper.AddUser(user), TipoEvento.UserCreated, user);
        }

        public SQLUpdateResult UpdateUser(Usuario user)
        {
            Usuario validationUser;
            validationUser = _userDataMapper.GetUserByUsername(user.NombreUsuario);
            if (validationUser != null && validationUser.Usuario_Id != user.Usuario_Id)
                throw new Exception("A User with this Username already exists.");
            validationUser = _userDataMapper.GetUserByDni(user.DNI);
            if (validationUser != null && validationUser.Usuario_Id != user.Usuario_Id)
                throw new Exception("A User with this DNI already exists.");

            return RecordResult(_userDataMapper.UpdateUser(user), TipoEvento.UserUpdated, user);
        }

        public SQLUpdateResult AlterBlockedState(Usuario user, Boolean state)
        {
            user.Bloqueado = state;
            if (!state)
            {
                user.IntentosDisponibles = 3;
                user.Clave = CryptographyHandler.HashPassword(user.DNI.ToString());
            }

            return RecordResult(_userDataMapper.UpdateUser(user), state ? TipoEvento.UserBlocked : TipoEvento.UserUnblocked, user, SessionHandler.Instance.User ?? user);
        }

        public SQLUpdateResult AddFailedLoginAttempt(Usuario user)
        {
            user.IntentosDisponibles -= 1;
            if (user.IntentosDisponibles == 0)
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
                Usuario user = GetFullUserById(Id);
                user.Clave = CryptographyHandler.HashPassword(NewPassword);
                result = _userDataMapper.UpdateUser(user);

                if (result != null && result.sqlResult == SQLResultType.success)
                {
                    SessionHandler.Instance.RefreshUser(user);
                    BitacoraEventosBLL.Record(TipoEvento.PasswordChanged, user.Usuario_Id);
                }
            }

            return result;
        }

        public SQLUpdateResult ChangeLanguage(Guid Id, string NewLanguage)
        {
            SQLUpdateResult result;

            Usuario user = GetFullUserById(Id);
            user.CodigoIdioma = NewLanguage;
            result = _userDataMapper.UpdateUser(user);

            if (result != null && result.sqlResult == SQLResultType.success)
            {
                SessionHandler.Instance.RefreshUser(user);
                BitacoraEventosBLL.Record(TipoEvento.LanguageChanged, user.Usuario_Id);
            }

            return result;
        }

        public bool IsUsingDefaultPassword(Guid Id)
        {
            Usuario user = GetFullUserById(Id);
            if(CryptographyHandler.VerifyPassword(user.DNI.ToString(), user.Clave))
                return true;
            else
                return false;

        }

        public SQLUpdateResult DeleteUser(Usuario user)
        {
            if (user.NombreUsuario == SessionHandler.Instance.GetUsername())
                throw new Exception("You cant delete the User you are using.");

            return RecordResult(_userDataMapper.DeleteUser(user), TipoEvento.UserDeleted, user);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Usuario target, Usuario actor = null)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, target.Usuario_Id, actor);
            return result;
        }

        public void Logout()
        {
            if (!SessionHandler.Instance.Logged())
                throw new TranslatableException("notLogged", "You are not logged in.");
            SessionHandler.Instance.Logout();
        }
        public RecoverySession Login(string username, string password)
        {
            if (SessionHandler.Instance.Logged())
                throw new TranslatableException("alreadyLogged", "You are already logged in.");

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) throw new TranslatableException("LoginFillAllFields", "Please complete all fields.");
            RecoverySession recovery = IntegrityService.Current.CheckLogin(username, password);
            if (recovery != null) return recovery;
            try
            {
                Usuario user = _userDataMapper.GetUserByUsername(username);

                if (user == null)
                {
                    BitacoraEventosBLL.Record(TipoEvento.LoginFailed, attemptedLogin: username.Length > 256 ? username.Substring(0, 256) : username);
                    throw new TranslatableException("LoginUserNotFound", "The user does not exist.");
                }
                if (!user.Activo)
                {
                    BitacoraEventosBLL.Record(TipoEvento.LoginFailed, user.Usuario_Id, user);
                    throw new TranslatableException("LoginUserInactive", "The user is inactive.");
                }
                if (user.Bloqueado)
                {
                    BitacoraEventosBLL.Record(TipoEvento.LoginFailed, user.Usuario_Id, user);
                    throw new TranslatableException("LoginUserBlocked", "The user is blocked. Contact the administrator to unblock it.");
                }
                if (!CryptographyHandler.VerifyPassword(password, user.Clave))
                {
                    var failedResult = AddFailedLoginAttempt(user);
                    if (failedResult == null || failedResult.sqlResult != SQLResultType.success)
                        throw new TranslatableException("AuditLoginUpdateFailed", "No se pudo actualizar el estado de acceso del usuario.");
                    BitacoraEventosBLL.Record(TipoEvento.LoginFailed, user.Usuario_Id, user);
                    if (user.IntentosDisponibles == 0)
                        throw new TranslatableException("LoginWrongPasswordBlocked", "Incorrect password. You have no attempts left and your user was blocked. Contact the administrator to unblock it.");
                    else
                        throw new TranslatableException("LoginWrongPassword", "Incorrect password. You have {0} attempts left.", user.IntentosDisponibles);
                }

                user.IntentosDisponibles = 3;

                var resetResult = _userDataMapper.UpdateUser(user);
                if (resetResult == null || resetResult.sqlResult != SQLResultType.success)
                    throw new TranslatableException("AuditLoginUpdateFailed", "No se pudo actualizar el estado de acceso del usuario.");

                user = _userDataMapper.GetFullUserById(user.Usuario_Id);

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
