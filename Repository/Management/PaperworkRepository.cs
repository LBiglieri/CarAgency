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
using System.Collections;

namespace CarAgency.Repository
{
    public class PaperworkRepository : BaseRepository
    {
        public List<PaperworkFile> GetFilesByPaperWork(Guid Paperwork_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("PaperworkFile_GetByPaperWork", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Paperwork_Id", Paperwork_Id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<PaperworkFile>(reader);
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

        public SQLUpdateResult AddPaperworkFile(PaperworkFile paperwork)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("PaperworkFile_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", paperwork.Id));
                cmd.Parameters.Add(new SqlParameter("Paperwork_Id", paperwork.Paperwork_Id));
                cmd.Parameters.Add(new SqlParameter("FileName", paperwork.FileName));
                cmd.Parameters.Add(new SqlParameter("FileContent", paperwork.FileContent));
                cmd.Parameters.Add(new SqlParameter("UploadedDate", paperwork.UploadedDate));

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

        public SQLUpdateResult DeletePaperworkFile(PaperworkFile paperwork)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("PaperworkFile_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", paperwork.Id));

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

        public Paperwork GetById(Guid id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Paperwork_GetById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                
                return MappingHandler.MapReaderToEntity<Paperwork>(reader); 
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
        public List<Paperwork> GetAllActiveByClient(Guid Client_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Paperwork_GetAllActiveByClient", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Client_Id", Client_Id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                List<Paperwork> paperworks = MappingHandler.MapReaderToEntities<Paperwork>(reader);
                List<Paperwork> paperworks_complete = new List<Paperwork>();

                ClientRepository clientRepository = new ClientRepository();

                foreach (Paperwork paperwork in paperworks)
                {
                    Client client = clientRepository.GetById(paperwork.Client_Id);
                    paperwork.Client_Description = "DNI: " + client.Dni.ToString() + " Full Name: " + client.Name + client.Surname + " Email: " + client.Email;
                    paperworks_complete.Add(paperwork);
                }

                return paperworks_complete;
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

        public SQLUpdateResult AddPaperwork(Paperwork paperwork)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Paperwork_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", paperwork.Id));
                cmd.Parameters.Add(new SqlParameter("Vehicle_Id", paperwork.Vehicle_Id));
                cmd.Parameters.Add(new SqlParameter("Client_Id", paperwork.Client_Id));
                cmd.Parameters.Add(new SqlParameter("Invoice_Id", paperwork.Invoice_Id));
                cmd.Parameters.Add(new SqlParameter("Paperwork_Precharge_Code", paperwork.Paperwork_Precharge_Code));
                cmd.Parameters.Add(new SqlParameter("Transfer_Date", paperwork.Transfer_Date));
                cmd.Parameters.Add(new SqlParameter("Observations", paperwork.Observations));
                cmd.Parameters.Add(new SqlParameter("IsFinished", paperwork.IsFinished));

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

        public SQLUpdateResult UpdatePaperwork(Paperwork paperwork)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Paperwork_Update", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", paperwork.Id));
                cmd.Parameters.Add(new SqlParameter("Vehicle_Id", paperwork.Vehicle_Id));
                cmd.Parameters.Add(new SqlParameter("Client_Id", paperwork.Client_Id));
                cmd.Parameters.Add(new SqlParameter("Invoice_Id", paperwork.Invoice_Id));
                cmd.Parameters.Add(new SqlParameter("Paperwork_Precharge_Code", paperwork.Paperwork_Precharge_Code));
                cmd.Parameters.Add(new SqlParameter("Transfer_Date", paperwork.Transfer_Date));
                cmd.Parameters.Add(new SqlParameter("Observations", paperwork.Observations));
                cmd.Parameters.Add(new SqlParameter("IsFinished", paperwork.IsFinished));

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

        public SQLUpdateResult DeletePaperwork(Paperwork paperwork)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Paperwork_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", paperwork.Id));

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
