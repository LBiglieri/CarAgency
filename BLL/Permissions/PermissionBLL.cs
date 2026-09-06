using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;

namespace CarAgency.BLL
{
    public class PermissionBLL
    {
        private PermissionMapper _permissionmapper;
        public PermissionBLL()
        {
            _permissionmapper = new PermissionMapper();
        }
        public Array GetAllPermissionTypes()
        {
            return _permissionmapper.GetAllPermissionTypes();
        }

        public bool Exists(ComposedPermission c, Guid id)
        {
            bool exists = false;

            if (c.Id.Equals(id))
                exists = true;
            else

                foreach (var item in c.Children)
                {

                    exists = Exists(item, id);
                    if (exists) return true;
                }

            return exists;
        }
        public bool InsertComposedPermission(ComposedPermission p, bool isfamily)
        {
            return _permissionmapper.InsertComponent(p, isfamily);
        }

        public void SaveFamily(Family c)
        {
            _permissionmapper.SaveFamily(c);
        }

        public IList<Patent> GetAllPatents()
        {
            return _permissionmapper.GetPatents();
        }

        public IList<Family> GetAllFamilies()
        {
            return _permissionmapper.GetFamilies();
        }

        public IList<ComposedPermission> GetAll(Guid family)
        {
            return _permissionmapper.GetAll(family);
        }

        public void FillFamilyComponents(Family family)
        {
            _permissionmapper.FillFamilyComponents(family);
        }

        public bool DeletePatent(Patent selectedItem)
        {
            return _permissionmapper.DeletePatent(selectedItem);
        }

        public bool DeleteFamily(ComposedPermission selectedItem)
        {
            return _permissionmapper.DeleteCompleteFamily(selectedItem);
        }
    }
}
