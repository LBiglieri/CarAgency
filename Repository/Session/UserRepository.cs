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
using System.Collections;

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
        public User GetUserByDni(int Dni)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_GetUserByDni", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Dni", Dni);
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

        public List<User> GetAllByState(Boolean Active)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_GetAllByState", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Active", Active));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;


                return MappingHandler.MapReaderToEntities<User>(reader);
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
        

        public SQLUpdateResult AddUser(User user)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", user.Id));
                cmd.Parameters.Add(new SqlParameter("Dni", user.Dni));
                cmd.Parameters.Add(new SqlParameter("Username", user.Username));
                cmd.Parameters.Add(new SqlParameter("Password", user.Password));
                cmd.Parameters.Add(new SqlParameter("Name", user.Name));
                cmd.Parameters.Add(new SqlParameter("Surname", user.Surname));
                cmd.Parameters.Add(new SqlParameter("Role", user.Role));
                cmd.Parameters.Add(new SqlParameter("Blocked", user.Blocked));
                cmd.Parameters.Add(new SqlParameter("Active", user.Active));
                cmd.Parameters.Add(new SqlParameter("Available_Login_Attempts", user.Available_Login_Attempts));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                string message = "";
                SQLResultType sqlResultType = SQLResultType.database_error;
                while (reader.Read())
                {
                    message = reader.GetString(reader.GetOrdinal("message"));
                    Enum.TryParse(reader.GetString(reader.GetOrdinal("SQLResultType")), out SQLResultType _SQLResultType);
                    sqlResultType = _SQLResultType;
                }
                return new SQLUpdateResult(sqlResultType, message);
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

        public SQLUpdateResult UpdateUser(User user)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_Update", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", user.Id));
                cmd.Parameters.Add(new SqlParameter("Dni", user.Dni));
                cmd.Parameters.Add(new SqlParameter("Username", user.Username));
                cmd.Parameters.Add(new SqlParameter("Password", user.Password));
                cmd.Parameters.Add(new SqlParameter("Name", user.Name));
                cmd.Parameters.Add(new SqlParameter("Surname", user.Surname));
                cmd.Parameters.Add(new SqlParameter("Role", user.Role));
                cmd.Parameters.Add(new SqlParameter("Blocked", user.Blocked));
                cmd.Parameters.Add(new SqlParameter("Active", user.Active));
                cmd.Parameters.Add(new SqlParameter("Available_Login_Attempts", user.Available_Login_Attempts));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                string message = "";
                SQLResultType sqlResultType = SQLResultType.database_error;
                while (reader.Read())
                {
                    message = reader.GetString(reader.GetOrdinal("message"));
                    Enum.TryParse(reader.GetString(reader.GetOrdinal("SQLResultType")), out SQLResultType _SQLResultType);
                    sqlResultType = _SQLResultType;
                }
                return new SQLUpdateResult(sqlResultType, message);
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

        public SQLUpdateResult DeleteUser(User user)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("User_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", user.Id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                string message = "";
                SQLResultType sqlResultType = SQLResultType.database_error;
                while (reader.Read())
                {
                    message = reader.GetString(reader.GetOrdinal("message"));
                    Enum.TryParse(reader.GetString(reader.GetOrdinal("SQLResultType")), out SQLResultType _SQLResultType);
                    sqlResultType = _SQLResultType;
                }
                return new SQLUpdateResult(sqlResultType, message);
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
