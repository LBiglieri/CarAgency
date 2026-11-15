using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Permisos
{
    public sealed class PermisoDataAccess : DataAccessBase
    {
        public DataTable GetPatents() { return Read("Permiso_GetPatents"); }
        public DataTable GetFamilies() { return Read("Permiso_GetFamilies"); }
        public DataTable GetAll(Guid family) { return Read("Permiso_GetAll", new SqlParameter("@Family", family)); }

        // type null = familia.
        public int InsertComponent(Guid id, string name, string type, DvhCalculator dvh)
        {
            return Execute("Permiso_InsertComponent", dvh, new SqlParameter("@Permiso_Id", id),
                new SqlParameter("@Nombre", name), new SqlParameter("@Tipo", (object)type ?? DBNull.Value));
        }

        public int InsertChild(Guid fatherId, Guid childId, DvhCalculator dvh)
        {
            return Execute("Permiso_Insert_Permission_Permission", dvh,
                new SqlParameter("@Padre_Id", fatherId), new SqlParameter("@Hijo_Id", childId));
        }

        public int DeleteFamily(Guid id) { return Execute("Permiso_DeleteFamily", null, new SqlParameter("@Permiso_Id", id)); }
        public int DeletePatent(Guid id) { return Execute("Permiso_DeletePatent", null, new SqlParameter("@Permiso_Id", id)); }

        // Los usuarios de la familia pasan a baseRoleId; userDvh trae el DVH ya recalculado de cada uno.
        public int DeleteCompleteFamily(Guid id, Guid? baseRoleId, IDictionary<Guid, string> userDvh)
        {
            using (var digests = new DataTable())
            {
                digests.Columns.Add("Usuario_Id", typeof(Guid));
                digests.Columns.Add("DVH", typeof(string));
                foreach (KeyValuePair<Guid, string> user in userDvh) digests.Rows.Add(user.Key, user.Value);

                return Execute("Permiso_DeleteCompleteFamily", null, new SqlParameter("@Permiso_Id", id),
                    new SqlParameter("@BaseRoleId", SqlDbType.UniqueIdentifier) { Value = (object)baseRoleId ?? DBNull.Value },
                    new SqlParameter("@UserDigests", SqlDbType.Structured) { TypeName = "dbo.UsuarioDvhUpdates", Value = digests });
            }
        }
    }
}
