using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Entities;
using CarAgency.Utilities;
using System.Data;
using System.Data.SqlClient;
using CarAgency.Utilities.Persistence;

namespace CarAgency.Repository
{
    public class UserRepository : BaseRepository
    {
        public User GetFullUserById(Guid Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_GetUserById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Id", Id);
                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.Read()) return null;
                User user = MappingHandler.MapReaderToEntity<User>(reader);
                
                PermissionRepository permissionRepository = new PermissionRepository();
                permissionRepository.FillUserComponents(user);
                return user;
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
        public User GetUserByUsername(string Username)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_GetUserByUsername", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Username", Username);
                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.Read()) return null;
                return MappingHandler.MapReaderToEntity<User>(reader);
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
        public List<User> GetAll()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                var lista = new List<User>();

                while (reader.Read())
                {
                    User c = new User();
                    c.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    c.Username = reader.GetString(reader.GetOrdinal("Username"));
                    lista.Add(c);
                }
                return lista;
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
        public void SavePermissions(User u)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            try
            {
                SqlCommand cmd = new SqlCommand("User_DeleteAllPermissions", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", u.Id));

                sql.Open();

                cmd.ExecuteNonQuery();

                foreach (var item in u.Permissions)
                {
                    cmd = new SqlCommand("User_InsertPermission", sql);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("User_Id", u.Id));
                    cmd.Parameters.Add(new SqlParameter("Permission_Id", item.Id));

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
    }
}
