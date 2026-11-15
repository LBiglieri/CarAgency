using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.BE;

namespace CarAgency.Security.Session
{
    public class PermisoBLL
    {
        private PermisoMapper _permissionmapper;
        public PermisoBLL()
        {
            _permissionmapper = new PermisoMapper();
        }
        public Array GetAllPermissionTypes()
        {
            return _permissionmapper.GetAllPermissionTypes();
        }

        public bool Exists(ComposedPermiso c, Guid id)
        {
            bool exists = false;

            if (c.Permiso_Id.Equals(id))
                exists = true;
            else

                foreach (var item in c.Children)
                {

                    exists = Exists(item, id);
                    if (exists) return true;
                }

            return exists;
        }
        public bool InsertComposedPermission(ComposedPermiso p, bool isfamily)
        {
            bool result = _permissionmapper.InsertComponent(p, isfamily);
            if (result) BitacoraEventosBLL.Record(isfamily ? TipoEvento.FamilyCreated : TipoEvento.PermissionCreated, p.Permiso_Id);
            return result;
        }

        public void SaveFamily(Familia c)
        {
            _permissionmapper.SaveFamily(c);
            BitacoraEventosBLL.Record(TipoEvento.FamilyUpdated, c.Permiso_Id);
        }

        public IList<Patente> GetAllPatents()
        {
            return _permissionmapper.GetPatents();
        }

        public IList<Familia> GetAllFamilies()
        {
            return _permissionmapper.GetFamilies();
        }

        public IList<ComposedPermiso> GetAll(Guid family)
        {
            return _permissionmapper.GetAll(family);
        }

        public void FillFamilyComponents(Familia family)
        {
            _permissionmapper.FillFamilyComponents(family);
        }

        public bool DeletePatent(Patente selectedItem)
        {
            bool result = _permissionmapper.DeletePatent(selectedItem);
            if (result) BitacoraEventosBLL.Record(TipoEvento.PermissionDeleted, selectedItem.Permiso_Id);
            return result;
        }

        public bool DeleteFamily(ComposedPermiso selectedItem)
        {
            bool result = _permissionmapper.DeleteCompleteFamily(selectedItem);
            if (result) BitacoraEventosBLL.Record(TipoEvento.FamilyDeleted, selectedItem.Permiso_Id);
            return result;
        }
    }
}
