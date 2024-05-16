using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Abstractions
{
    public interface IUser : IEntity
    {

        string Username { get; set; }
        string Password { get; set; }

    }
}
