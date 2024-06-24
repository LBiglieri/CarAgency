using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Entities
{
    public class Patent : ComposedPermission
    {

        public override void AddPermission(ComposedPermission p)
        {
        }
        public override void DeletePermission(ComposedPermission p)
        {
        }

        public override IList<ComposedPermission> Children
        {
            get
            {
                return new List<ComposedPermission>();
            }
        }
    }
}
