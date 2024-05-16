using CarAgency.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Utilities
{
    public abstract class ServiceEntity
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }
    }
    
}
