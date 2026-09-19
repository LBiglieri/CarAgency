using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.DAL.Permissions;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.Session
{
    public class PermissionMapper
    {
        private readonly PermissionDataAccess data = new PermissionDataAccess();

        public Array GetAllPermissionTypes()
        {
            return Enum.GetValues(typeof(PermissionType));
        }

        public bool InsertComponent(ComposedPermission p, bool isfamily)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            int affected = data.InsertComponent(p.Id, p.Name, isfamily ? null : p.Type.ToString(),
                digitVerifier.Dvh("Permissions"));
            digitVerifier.UpdateDvv("Permissions");
            return affected > 0;
        }

        public bool DeleteFamily(ComposedPermission p)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured("Permission_Permission");
            int affected = data.DeleteFamily(p.Id);
            digitVerifier.UpdateDvv("Permission_Permission");
            return affected > 0;
        }

        // Deuda conocida: borra y reinserta los hijos sin transaccion; si falla un insert
        // la familia queda a medias.
        public void SaveFamily(Family c)
        {
            DeleteFamily(c);
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured("Permission_Permission");
            foreach (var item in c.Children)
            {
                data.InsertChild(c.Id, item.Id, digitVerifier.Dvh("Permission_Permission"));
                digitVerifier.UpdateDvv("Permission_Permission");
            }
        }

        public IList<Patent> GetPatents()
        {
            using (DataTable table = data.GetPatents())
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows.Cast<DataRow>().Select(row => new Patent
                {
                    Id = (Guid)row["Id"],
                    Name = (string)row["Name"],
                    Type = (PermissionType)Enum.Parse(typeof(PermissionType), (string)row["Type"])
                }).ToList();
            }
        }

        public IList<Family> GetFamilies()
        {
            using (DataTable table = data.GetFamilies())
            {
                if (table.Rows.Count == 0) return null;
                return table.Rows.Cast<DataRow>().Select(row => new Family
                {
                    Id = (Guid)row["Id"],
                    Name = (string)row["Name"]
                }).ToList();
            }
        }

        // Las filas llegan ordenadas padre antes que hijo: cada una se cuelga del componente
        // ya leido cuyo Id coincide con su Father_Id, o va a la raiz si no lo tiene.
        public IList<ComposedPermission> GetAll(Guid family)
        {
            using (DataTable table = data.GetAll(family))
            {
                if (table.Rows.Count == 0) return null;

                var list = new List<ComposedPermission>();
                foreach (DataRow row in table.Rows)
                {
                    Guid fatherId = row.IsNull("Father_Id") ? Guid.Empty : (Guid)row["Father_Id"];
                    string type = row.IsNull("Type") ? string.Empty : (string)row["Type"];

                    ComposedPermission c = string.IsNullOrEmpty(type) ? (ComposedPermission)new Family() : new Patent();
                    c.Id = (Guid)row["Id"];
                    c.Name = (string)row["Name"];
                    if (!string.IsNullOrEmpty(type))
                        c.Type = (PermissionType)Enum.Parse(typeof(PermissionType), type);

                    var father = GetComponent(fatherId, list);
                    if (father == null) list.Add(c);
                    else father.AddPermission(c);
                }
                return list;
            }
        }

        private ComposedPermission GetComponent(Guid Id, IList<ComposedPermission> list)
        {

            ComposedPermission composedPermission = list != null ? list.Where(i => i.Id.Equals(Id)).FirstOrDefault() : null;

            if (composedPermission == null && list != null)
            {
                foreach (var c in list)
                {

                    var l = GetComponent(Id, c.Children);
                    if (l != null && l.Id == Id) return l;
                    else
                    if (l != null)
                        return GetComponent(Id, l.Children);

                }
            }

            return composedPermission;
        }

        public void FillFamilyComponents(Family family)
        {
            family.DeleteChildren();
            foreach (var item in GetAll(family.Id))
            {
                family.AddPermission(item);
            }
        }

        public void FillUserRole(User u)
        {
            u.Role = new Family();
            u.Role.Id = u.Role_Id;

            IList<ComposedPermission> family = GetAll(u.Role.Id);
            if (family != null)
            {
                foreach (var i in family)
                    u.Role.AddPermission(i);
            }
        }

        public bool DeletePatent(Patent selectedItem)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured("Permission_Permission", "Permissions");
            int affected = data.DeletePatent(selectedItem.Id);
            digitVerifier.UpdateDvv("Permission_Permission", "Permissions");
            return affected > 0;
        }

        public bool DeleteCompleteFamily(ComposedPermission selectedItem)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            DVFamilyDeletion deletion = digitVerifier.PrepareFamilyDeletion(selectedItem.Id);
            int affected = data.DeleteCompleteFamily(selectedItem.Id, deletion.BaseRoleId,
                deletion.UserDigests.ToDictionary(d => d.Id, d => d.DVH));
            digitVerifier.UpdateDvv("Users", "Permission_Permission", "Permissions");
            return affected > 0;
        }
    }
}
