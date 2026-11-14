using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Familia : ComposedPermiso
    {
        private IList<ComposedPermiso> _children;
        public Familia()
        {
            _children = new List<ComposedPermiso>();
        }
        public override void AddPermission(ComposedPermiso p)
        {
            if (!_children.Contains(p))
                _children.Add(p);
        }

        public override void DeletePermission(ComposedPermiso p)
        {
            if (_children.Contains(p))
                _children.Remove(p);
        }
        public void DeleteChildren ()
        {
            _children = new List<ComposedPermiso>();
        }

        public override IList<ComposedPermiso> Children
        {
            get
            {
                return _children.ToArray();
            }
        }
    }
}
