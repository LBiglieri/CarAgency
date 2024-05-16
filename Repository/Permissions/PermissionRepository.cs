using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Entities;
using CarAgency.Utilities.Permissions;
using CarAgency.Utilities.Persistence;
using System.Data;
using System.Data.SqlClient;

namespace CarAgency.Repository
{
    public class PermissionRepository : BaseRepository
    {
        public Array GetAllPermissionTypes()
        {
            return Enum.GetValues(typeof(PermissionType));
        }

        //revisar si corresponde que el id sea generado en sql ( para poder identificar familias/patentes ya guardadas) entre otras cosas.
        public bool InsertComponent(ComposedPermission p, bool isfamily)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand("Permissions_InsertComponent", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Id", p.Id);
                cmd.Parameters.AddWithValue("Name", p.Name);
                if (isfamily)
                    cmd.Parameters.Add(new SqlParameter("Type", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("Type", p.Type.ToString()));
                sql.Open();

                if(cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sql != null)
                    sql.Close();
            }
        }

        public bool DeleteFamily(ComposedPermission p)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand("Permissions_DeleteFamily", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Id", p.Id);
                sql.Open();

                if(cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sql != null)
                    sql.Close();
            }
        }

        public void SaveFamily(Family c)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            try
            {
                DeleteFamily(c);
                sql.Open();
                foreach (var item in c.Children)
                {
                    SqlCommand cmd = new SqlCommand("Permissions_Insert_Permission_Permission", sql);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("Father_Id", c.Id);
                    cmd.Parameters.AddWithValue("Child_Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sql != null)
                    sql.Close();
            }
        }

        public IList<Patent> GetPatents()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Permissions_GetPatents", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();

                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                var list = new List<Patent>();

                while (reader.Read())
                {
                    var Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    var Name = reader.GetString(reader.GetOrdinal("Name"));
                    var Type = reader.GetString(reader.GetOrdinal("Type"));

                    Patent c = new Patent();

                    c.Id = Id;
                    c.Name = Name;
                    c.Type = (PermissionType)Enum.Parse(typeof(PermissionType), Type);
                    list.Add(c);
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }

        public IList<Family> GetFamilies()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Permissions_GetFamilies", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();

                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                var list = new List<Family>();

                while (reader.Read())
                {
                    var Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    var Name = reader.GetString(reader.GetOrdinal("Name"));

                    Family c = new Family();

                    c.Id = Id;
                    c.Name = Name;
                    list.Add(c);

                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }
        public IList<ComposedPermission> GetAll(Guid family)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Permissions_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                if (family != null)
                    cmd.Parameters.AddWithValue("Family", family);
                else
                    cmd.Parameters.AddWithValue("Family", DBNull.Value);

                sql.Open();

                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                var list = new List<ComposedPermission>();

                while (reader.Read())
                {
                    Guid Father_Id = Guid.Empty;
                    if (reader["Father_Id"] != DBNull.Value)
                    {
                        Father_Id = reader.GetGuid(reader.GetOrdinal("Father_Id"));
                    }

                    var Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    var Name = reader.GetString(reader.GetOrdinal("Name"));

                    var Type = string.Empty;
                    if (reader["Type"] != DBNull.Value)
                        Type = reader.GetString(reader.GetOrdinal("Type"));


                    ComposedPermission c;

                    if (string.IsNullOrEmpty(Type))
                        c = new Family();

                    else
                        c = new Patent();

                    c.Id = Id;
                    c.Name = Name;
                    if (!string.IsNullOrEmpty(Type))
                        c.Type = (PermissionType)Enum.Parse(typeof(PermissionType), Type);

                    var Father = GetComponent(Father_Id, list);

                    if (Father == null)
                    {
                        list.Add(c);
                    }
                    else
                    {
                        Father.AddPermission(c);
                    }

                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
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
        public void FillUserComponents(User u)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Permissions_GetAllByUser", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("User_Id", u.Id);

                sql.Open();

                reader = cmd.ExecuteReader();

                u.Permissions.Clear();

                while (reader.Read())
                {

                    var idp = reader.GetGuid(reader.GetOrdinal("Id"));
                    var namep = reader.GetString(reader.GetOrdinal("Name"));

                    var type = String.Empty;
                    if (reader["Type"] != DBNull.Value)
                        type = reader.GetString(reader.GetOrdinal("Type"));

                    ComposedPermission c1;
                    if (!String.IsNullOrEmpty(type))
                    {
                        c1 = new Patent();
                        c1.Id = idp;
                        c1.Name = namep;
                        c1.Type = (PermissionType)Enum.Parse(typeof(PermissionType), type);
                        u.Permissions.Add(c1);
                    }
                    else
                    {
                        c1 = new Family();
                        c1.Id = idp;
                        c1.Name = namep;

                        var f = GetAll(idp);

                        foreach (var family in f)
                        {
                            c1.AddPermission(family);
                        }
                        u.Permissions.Add(c1);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }
    }
}
