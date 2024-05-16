using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Abstractions;
using CarAgency.Utilities.Permissions;

namespace CarAgency.Entities
{
    public class User : Entity, IUser
    {
        private IList<ComposedPermission> _permissions;
        public User()
        {
            _permissions = new List<ComposedPermission>();
        }
        [TableColumnAttribute]
        public string Username { get; set; }
        [TableColumnAttribute]
        public string Password { get; set; }
        public IList<ComposedPermission> Permissions
        {
            get
            {
                return _permissions;
            }
        }
    }
}
