using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.DAL.Permisos;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.Session
{
    public class PermisoMapper
    {
        private readonly PermisoDataAccess data = new PermisoDataAccess();

        public Array GetAllPermissionTypes()
        {
            return Enum.GetValues(typeof(PermisoType));
        }

        public bool InsertComponent(ComposedPermiso p, bool isfamily)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            int affected = data.InsertComponent(p.Permiso_Id, p.Nombre, isfamily ? null : p.Tipo.ToString(),
                digitVerifier.Dvh("Permiso"));
            digitVerifier.UpdateDvv("Permiso");
            return affected > 0;
        }

        public bool DeleteFamily(ComposedPermiso p)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured("Permiso_Permiso");
            int affected = data.DeleteFamily(p.Permiso_Id);
            digitVerifier.UpdateDvv("Permiso_Permiso");
            return affected > 0;
        }

        // Deuda conocida: borra y reinserta los hijos sin transaccion; si falla un insert
        // la familia queda a medias.
        public void SaveFamily(Familia c)
        {
            DeleteFamily(c);
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured("Permiso_Permiso");
            foreach (var item in c.Children)
            {
                data.InsertChild(c.Permiso_Id, item.Permiso_Id, digitVerifier.Dvh("Permiso_Permiso"));
                digitVerifier.UpdateDvv("Permiso_Permiso");
            }
        }

        public IList<Patente> GetPatents()
        {
            using (DataTable table = data.GetPatents())
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows.Cast<DataRow>().Select(row => new Patente
                {
                    Permiso_Id = (Guid)row["Permiso_Id"],
                    Nombre = (string)row["Nombre"],
                    Tipo = (PermisoType)Enum.Parse(typeof(PermisoType), (string)row["Tipo"])
                }).ToList();
            }
        }

        public IList<Familia> GetFamilies()
        {
            using (DataTable table = data.GetFamilies())
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows.Cast<DataRow>().Select(row => new Familia
                {
                    Permiso_Id = (Guid)row["Permiso_Id"],
                    Nombre = (string)row["Nombre"]
                }).ToList();
            }
        }

        // Las filas llegan ordenadas padre antes que hijo: cada una se cuelga del componente
        // ya leido cuyo Id coincide con su Father_Id, o va a la raiz si no lo tiene.
        public IList<ComposedPermiso> GetAll(Guid family)
        {
            using (DataTable table = data.GetAll(family))
            {
                if (table.Rows.Count == 0) return null;

                var list = new List<ComposedPermiso>();
                foreach (DataRow row in table.Rows)
                {
                    Guid fatherId = row.IsNull("Padre_Id") ? Guid.Empty : (Guid)row["Padre_Id"];
                    string type = row.IsNull("Tipo") ? string.Empty : (string)row["Tipo"];

                    ComposedPermiso c = string.IsNullOrEmpty(type) ? (ComposedPermiso)new Familia() : new Patente();
                    c.Permiso_Id = (Guid)row["Permiso_Id"];
                    c.Nombre = (string)row["Nombre"];
                    if (!string.IsNullOrEmpty(type))
                        c.Tipo = (PermisoType)Enum.Parse(typeof(PermisoType), type);

                    var father = GetComponent(fatherId, list);
                    if (father == null) list.Add(c);
                    else father.AddPermission(c);
                }
                return list;
            }
        }

        private ComposedPermiso GetComponent(Guid Id, IList<ComposedPermiso> list)
        {

            ComposedPermiso composedPermission = list != null ? list.Where(i => i.Permiso_Id.Equals(Id)).FirstOrDefault() : null;

            if (composedPermission == null && list != null)
            {
                foreach (var c in list)
                {

                    var l = GetComponent(Id, c.Children);
                    if (l != null && l.Permiso_Id == Id) return l;
                    else
                    if (l != null)
                        return GetComponent(Id, l.Children);

                }
            }

            return composedPermission;
        }

        public void FillFamilyComponents(Familia family)
        {
            family.DeleteChildren();
            foreach (var item in GetAll(family.Permiso_Id))
            {
                family.AddPermission(item);
            }
        }

        public void FillUserRole(Usuario u)
        {
            u.Rol = new Familia();
            u.Rol.Permiso_Id = u.Rol_Id;

            IList<ComposedPermiso> family = GetAll(u.Rol.Permiso_Id);
            if (family != null)
            {
                foreach (var i in family)
                    u.Rol.AddPermission(i);
            }
        }

        public bool DeletePatent(Patente selectedItem)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured("Permiso_Permiso", "Permiso");
            int affected = data.DeletePatent(selectedItem.Permiso_Id);
            digitVerifier.UpdateDvv("Permiso_Permiso", "Permiso");
            return affected > 0;
        }

        public bool DeleteCompleteFamily(ComposedPermiso selectedItem)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            DVFamiliaDeletion deletion = digitVerifier.PrepareFamilyDeletion(selectedItem.Permiso_Id);
            int affected = data.DeleteCompleteFamily(selectedItem.Permiso_Id, deletion.BaseRoleId,
                deletion.UserDigests.ToDictionary(d => d.Usuario_Id, d => d.DVH));
            digitVerifier.UpdateDvv("Usuario", "Permiso_Permiso", "Permiso");
            return affected > 0;
        }
    }
}
