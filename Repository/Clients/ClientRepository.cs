using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using System.Data;
using System.Data.SqlClient;
using CarAgency.Utilities.Security;
using System.Collections;
using System.Net;

namespace CarAgency.Repository
{
    public class ClientRepository : BaseRepository
    {
        public Client GetByDni(int dni)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Clients_GetByDni", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Dni", dni));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                Client client = new Client();

                while (reader.Read())
                {
                    var Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    var Dni = reader.GetInt32(reader.GetOrdinal("Dni"));
                    var Name = reader.GetString(reader.GetOrdinal("Name"));
                    var Surname = reader.GetString(reader.GetOrdinal("Surname"));
                    var Address = reader.GetString(reader.GetOrdinal("Address"));
                    var Phone_Number_Personal = reader.GetString(reader.GetOrdinal("Phone_Number_Personal"));
                    var Phone_Number_House = reader.GetString(reader.GetOrdinal("Phone_Number_House"));
                    var Email = reader.GetString(reader.GetOrdinal("Email"));
                    var Date_Of_Birth = reader.GetDateTime(reader.GetOrdinal("Date_Of_Birth"));


                    client.Id = Id;
                    client.Dni = Dni;
                    client.Name = Name;
                    client.Surname = Surname;
                    client.Address = Address;
                    client.Phone_Number_Personal = Phone_Number_Personal;
                    client.Phone_Number_House = Phone_Number_House;
                    client.Email = Email;
                    client.Date_Of_Birth = Date_Of_Birth;
                }

                client.Name = CryptographyHandler.Decrypt(client.Name);
                client.Surname = CryptographyHandler.Decrypt(client.Surname);
                client.Address = CryptographyHandler.Decrypt(client.Address);
                client.Phone_Number_Personal = CryptographyHandler.Decrypt(client.Phone_Number_Personal);
                client.Phone_Number_House = CryptographyHandler.Decrypt(client.Phone_Number_House);
                client.Email = CryptographyHandler.Decrypt(client.Email);

                return client;
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
        public Client GetById(Guid id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Clients_GetById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                Client client = MappingHandler.MapReaderToEntity<Client>(reader);
                client.Name = CryptographyHandler.Decrypt(client.Name);
                client.Surname = CryptographyHandler.Decrypt(client.Surname);
                client.Address = CryptographyHandler.Decrypt(client.Address);
                client.Phone_Number_Personal = CryptographyHandler.Decrypt(client.Phone_Number_Personal);
                client.Phone_Number_House = CryptographyHandler.Decrypt(client.Phone_Number_House);
                client.Email = CryptographyHandler.Decrypt(client.Email);

                return client;
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
        public SQLUpdateResult AddClient(Client client)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Clients_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", client.Id));
                cmd.Parameters.Add(new SqlParameter("Dni", client.Dni));
                cmd.Parameters.Add(new SqlParameter("Name", client.Name));
                cmd.Parameters.Add(new SqlParameter("Surname", client.Surname));
                cmd.Parameters.Add(new SqlParameter("Address", client.Address));
                cmd.Parameters.Add(new SqlParameter("Phone_Number_House", client.Phone_Number_House));
                cmd.Parameters.Add(new SqlParameter("Phone_Number_Personal", client.Phone_Number_Personal));
                cmd.Parameters.Add(new SqlParameter("Email", client.Email));
                cmd.Parameters.Add(new SqlParameter("Date_Of_Birth", client.Date_Of_Birth));

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
