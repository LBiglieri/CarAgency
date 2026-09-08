using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
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
            bool result = _permissionmapper.InsertComponent(p, isfamily);
            if (result) AuditBLL.Record(isfamily ? AuditEventType.FamilyCreated : AuditEventType.PermissionCreated, p.Id);
            return result;
        }

        public void SaveFamily(Family c)
        {
            _permissionmapper.SaveFamily(c);
            AuditBLL.Record(AuditEventType.FamilyUpdated, c.Id);
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
            bool result = _permissionmapper.DeletePatent(selectedItem);
            if (result) AuditBLL.Record(AuditEventType.PermissionDeleted, selectedItem.Id);
            return result;
        }

        public bool DeleteFamily(ComposedPermission selectedItem)
        {
            bool result = _permissionmapper.DeleteCompleteFamily(selectedItem);
            if (result) AuditBLL.Record(AuditEventType.FamilyDeleted, selectedItem.Id);
            return result;
        }
    }
}
