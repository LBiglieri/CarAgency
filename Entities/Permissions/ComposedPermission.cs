using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Entities
{
    public abstract class ComposedPermission
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public PermissionType Type { get; set; }

        public abstract void AddPermission(ComposedPermission p);
        public abstract void DeletePermission(ComposedPermission p);
        public abstract IList<ComposedPermission> Children { get; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
