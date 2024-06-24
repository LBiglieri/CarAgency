using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CarAgency.Repository
{
    public class ReservationRepository : BaseRepository
    {
        public Reservation GetById(Guid id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Reservation_GetById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntity<Reservation>(reader);
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
        public List<Reservation> GetAllActiveByClient(Guid Client_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Reservation_GetAllActiveByClient", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Client_Id", Client_Id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<Reservation>(reader);
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
        public List<Reservation> GetAll()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Reservation_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<Reservation>(reader);
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
        public SQLUpdateResult AddReservation(Reservation reservation)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Reservation_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("Id", reservation.Id));
                cmd.Parameters.Add(new SqlParameter("Vehicle_Id", reservation.Vehicle_Id));
                cmd.Parameters.Add(new SqlParameter("Client_Id", reservation.Client_Id));
                cmd.Parameters.Add(new SqlParameter("Price", reservation.Price));
                cmd.Parameters.Add(new SqlParameter("Creation_Date", reservation.Creation_Date));
                cmd.Parameters.Add(new SqlParameter("Expiration_Date", reservation.Expiration_Date));

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
