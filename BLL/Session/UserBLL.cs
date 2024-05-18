using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Abstractions;

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
        public List<User> GetAll()
        {
            return _userrepository.GetAll();
        }
        public List<User> GetAllByState(Boolean Active)
        {
            return _userrepository.GetAllByState(Active);
        }
        public void SavePermissions(User u)
        {
            _userrepository.SavePermissions(u);
        }

        public SQLUpdateResult AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.Available_Login_Attempts = 3;
            user.Blocked = false;
            user.Active = true;
            user.Password = CryptographyHandler.GenerateSHA512Hash(user.Dni.ToString());
            return _userrepository.AddUser(user);
        }

        public SQLUpdateResult UpdateUser(User user)
        {
            return _userrepository.UpdateUser(user);
        }

        public SQLUpdateResult AlterBlockedState(User user, Boolean state)
        {
            user.Blocked = state;
            return _userrepository.UpdateUser(user);
        }

        public SQLUpdateResult DeleteUser(User user)
        {
            return _userrepository.DeleteUser(user);
        }
    }
}
