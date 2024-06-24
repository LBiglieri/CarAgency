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

                
                return MappingHandler.MapReaderToEntity<Invoice>(reader); 
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
        public List<Invoice> GetAllPendingOfPaperwork()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Invoice_GetAllPendingOfPaperwork", sql);
                cmd.CommandType = CommandType.StoredProcedure;

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
