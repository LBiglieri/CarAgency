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
    public class InvoiceRepository : BaseRepository
    {
        public Invoice GetById(Guid id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Invoice_GetById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                Invoice invoice = new Invoice();

                while (reader.Read())
                {
                    var Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    var Vehicle_Id = reader.GetGuid(reader.GetOrdinal("Vehicle_Id"));
                    var Vehicle_Description = reader.GetString(reader.GetOrdinal("Vehicle_Description"));
                    var Client_Id = reader.GetGuid(reader.GetOrdinal("Client_Id"));
                    var Reservation_Id = reader.GetGuid(reader.GetOrdinal("Reservation_Id"));
                    var Detail = reader.GetString(reader.GetOrdinal("Detail"));
                    var CUIL_CUIT_Client = reader.GetString(reader.GetOrdinal("CUIL_CUIT_Client"));
                    var Razon_Social = reader.GetString(reader.GetOrdinal("Razon_Social"));
                    var Amount = reader.GetDouble(reader.GetOrdinal("Amount"));
                    var Payment_Status = reader.GetBoolean(reader.GetOrdinal("Payment_Status"));
                    var Creation_Date = reader.GetDateTime(reader.GetOrdinal("Creation_Date"));


                    invoice.Id = Id;
                    invoice.Vehicle_Id = Vehicle_Id;
                    invoice.Vehicle_Description = Vehicle_Description;
                    invoice.Client_Id = Client_Id;
                    invoice.Reservation_Id = Reservation_Id;
                    invoice.Detail = Detail;
                    invoice.CUIL_CUIT_Client = CUIL_CUIT_Client;
                    invoice.Razon_Social = Razon_Social;
                    invoice.Amount = Amount;
                    invoice.Payment_Status = Payment_Status;
                    invoice.Creation_Date = Creation_Date;
                }
                return invoice; 
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
        public List<Invoice> GetAllPendingOfPaperworkByClient(Guid Client_id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Invoice_GetAllPendingOfPaperworkByClient", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Client_id", Client_id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<Invoice>(reader);
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

        public SQLUpdateResult AddInvoice(Invoice invoice)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Invoice_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", invoice.Id));
                cmd.Parameters.Add(new SqlParameter("Vehicle_Id", invoice.Vehicle_Id));
                cmd.Parameters.Add(new SqlParameter("Client_Id", invoice.Client_Id));
                cmd.Parameters.Add(new SqlParameter("Reservation_Id", invoice.Reservation_Id));
                cmd.Parameters.Add(new SqlParameter("Detail", invoice.Detail));
                cmd.Parameters.Add(new SqlParameter("CUIL_CUIT_Client", invoice.CUIL_CUIT_Client));
                cmd.Parameters.Add(new SqlParameter("Razon_Social", invoice.Razon_Social));
                cmd.Parameters.Add(new SqlParameter("Amount", invoice.Amount));
                cmd.Parameters.Add(new SqlParameter("Payment_Status", invoice.Payment_Status));
                cmd.Parameters.Add(new SqlParameter("Creation_Date", invoice.Creation_Date));

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

        public SQLUpdateResult UpdateInvoice(Invoice invoice)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Invoice_Update", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", invoice.Id));
                cmd.Parameters.Add(new SqlParameter("Vehicle_Id", invoice.Vehicle_Id));
                cmd.Parameters.Add(new SqlParameter("Client_Id", invoice.Client_Id));
                cmd.Parameters.Add(new SqlParameter("Reservation_Id", invoice.Reservation_Id));
                cmd.Parameters.Add(new SqlParameter("Detail", invoice.Detail));
                cmd.Parameters.Add(new SqlParameter("CUIL_CUIT_Client", invoice.CUIL_CUIT_Client));
                cmd.Parameters.Add(new SqlParameter("Razon_Social", invoice.Razon_Social));
                cmd.Parameters.Add(new SqlParameter("Amount", invoice.Amount));
                cmd.Parameters.Add(new SqlParameter("Payment_Status", invoice.Payment_Status));
                cmd.Parameters.Add(new SqlParameter("Creation_Date", invoice.Creation_Date));

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

        public SQLUpdateResult DeleteInvoice(Invoice invoice)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Invoice_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", invoice.Id));

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
