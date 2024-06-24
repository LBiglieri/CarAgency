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
    public class QuotationRepository : BaseRepository
    {
        public Quotation GetById(Guid id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Quotations_GetById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntity<Quotation>(reader);
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
        public List<Quotation> GetAll()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Quotations_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<Quotation>(reader);
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
        public List<Quotation> GetAllActiveByClient(Guid client_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Quotations_GetAllActiveByClient", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Client_Id", client_Id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<Quotation>(reader);
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
        public SQLUpdateResult AddQuotation(Quotation quotation)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Quotations_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("Id", quotation.Id));
                cmd.Parameters.Add(new SqlParameter("Vehicle_Id", quotation.Vehicle_Id));
                cmd.Parameters.Add(new SqlParameter("Client_Id", quotation.Client_Id));
                cmd.Parameters.Add(new SqlParameter("Price", quotation.Price));
                cmd.Parameters.Add(new SqlParameter("Creation_Date", quotation.Creation_Date));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

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
