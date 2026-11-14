using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Patente : ComposedPermiso
    {

        public override void AddPermission(ComposedPermiso p)
        {
        }
        public override void DeletePermission(ComposedPermiso p)
        {
        }

        public override IList<ComposedPermiso> Children
        {
            get
            {
                return new List<ComposedPermiso>();
            }
        }
    }
}
