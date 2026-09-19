using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Permissions
{
    public sealed class PermissionDataAccess : DataAccessBase
    {
        public DataTable GetPatents() { return Read("Permissions_GetPatents"); }
        public DataTable GetFamilies() { return Read("Permissions_GetFamilies"); }
        public DataTable GetAll(Guid family) { return Read("Permissions_GetAll", new SqlParameter("@Family", family)); }

        // type null = familia.
        public int InsertComponent(Guid id, string name, string type, DvhCalculator dvh)
        {
            return Execute("Permissions_InsertComponent", dvh, new SqlParameter("@Id", id),
                new SqlParameter("@Name", name), new SqlParameter("@Type", (object)type ?? DBNull.Value));
        }

        public int InsertChild(Guid fatherId, Guid childId, DvhCalculator dvh)
        {
            return Execute("Permissions_Insert_Permission_Permission", dvh,
                new SqlParameter("@Father_Id", fatherId), new SqlParameter("@Child_Id", childId));
        }

        public int DeleteFamily(Guid id) { return Execute("Permissions_DeleteFamily", null, new SqlParameter("@Id", id)); }
        public int DeletePatent(Guid id) { return Execute("Permissions_DeletePatent", null, new SqlParameter("@Id", id)); }

        // Los usuarios de la familia pasan a baseRoleId; userDvh trae el DVH ya recalculado de cada uno.
        public int DeleteCompleteFamily(Guid id, Guid? baseRoleId, IDictionary<Guid, string> userDvh)
        {
            using (var digests = new DataTable())
            {
                digests.Columns.Add("Id", typeof(Guid));
                digests.Columns.Add("DVH", typeof(string));
                foreach (KeyValuePair<Guid, string> user in userDvh) digests.Rows.Add(user.Key, user.Value);

                return Execute("Permissions_DeleteCompleteFamily", null, new SqlParameter("@Id", id),
                    new SqlParameter("@BaseRoleId", SqlDbType.UniqueIdentifier) { Value = (object)baseRoleId ?? DBNull.Value },
                    new SqlParameter("@UserDigests", SqlDbType.Structured) { TypeName = "dbo.UserDvhUpdates", Value = digests });
            }
        }
    }
}
