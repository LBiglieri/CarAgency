using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Abstractions
{
    public interface IPermission : IEntity
    {
        string Name { get; set; }
        string Type { get; set; }
    }
}
