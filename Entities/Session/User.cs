using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Entities
{
    public class User : Entity
    {
        private IList<ComposedPermission> _permissions;
        public User()
        {
            _permissions = new List<ComposedPermission>();
        }
        [TableColumnAttribute]
        public int Dni { get; set; }

        [TableColumnAttribute]
        public string Username { get; set; }

        [TableColumnAttribute]
        public string Password { get; set; }

        [TableColumnAttribute]
        public string Name { get; set; }

        [TableColumnAttribute]
        public string Surname { get; set; }

        [TableColumnAttribute]
        public Guid Role_Id { get; set; }

        [TableColumnAttribute]
        public Boolean Blocked { get; set; }

        [TableColumnAttribute]
        public Boolean Active { get; set; }

        [TableColumnAttribute]
        public int Available_Login_Attempts { get; set; }

        public ComposedPermission Role { get; set; }
    }
}
