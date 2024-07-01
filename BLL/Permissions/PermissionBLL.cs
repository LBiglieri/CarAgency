using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;

namespace CarAgency.BLL
{
    public class PermissionBLL
    {
        private PermissionRepository _permissionrepository;
        public PermissionBLL()
        {
            _permissionrepository = new PermissionRepository();
        }
        public Array GetAllPermissionTypes()
        {
            return _permissionrepository.GetAllPermissionTypes();
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
            return _permissionrepository.InsertComponent(p, isfamily);
        }

        public void SaveFamily(Family c)
        {
            _permissionrepository.SaveFamily(c);
        }

        public IList<Patent> GetAllPatents()
        {
            return _permissionrepository.GetPatents();
        }

        public IList<Family> GetAllFamilies()
        {
            return _permissionrepository.GetFamilies();
        }

        public IList<ComposedPermission> GetAll(Guid family)
        {
            return _permissionrepository.GetAll(family);
        }

        public void FillFamilyComponents(Family family)
        {
            _permissionrepository.FillFamilyComponents(family);
        }

    }
}
