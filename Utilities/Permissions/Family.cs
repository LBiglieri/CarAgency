using CarAgency.Abstractions;
using CarAgency.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Utilities.Permissions
{
    public class Family : ComposedPermission
    {
        private IList<ComposedPermission> _children;
        public Family()
        {
            _children = new List<ComposedPermission>();
        }
        public override void AddPermission(ComposedPermission p)
        {
            if (!_children.Contains(p))
                _children.Add(p);
        }

        public override void DeletePermission(ComposedPermission p)
        {
            if (_children.Contains(p))
                _children.Remove(p);
        }
        public void DeleteChildren ()
        {
            _children = new List<ComposedPermission>();
        }

        public override IList<ComposedPermission> Children
        {
            get
            {
                return _children.ToArray();
            }
        }
    }
}
