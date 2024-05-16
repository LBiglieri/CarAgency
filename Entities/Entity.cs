using CarAgency.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Entities
{
    public abstract class Entity
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }
    }
    
}
