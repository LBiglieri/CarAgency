using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;

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
        public void SavePermissions(User u)
        {
            _userrepository.SavePermissions(u);
        }
    }
}
