using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Entities
{
    public interface IPermission
    {
        string Name { get; set; }
        string Type { get; set; }
    }
}
