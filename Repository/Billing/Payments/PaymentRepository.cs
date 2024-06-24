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
    public class PaymentRepository : BaseRepository
    {
        public List<PaymentType> GetAllPaymentTypes()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTypes_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                return MappingHandler.MapReaderToEntities<PaymentType>(reader);
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

        public List<Payment> GetAllByInvoice(Guid Invoice_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Payments_GetAllByInvoice", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Invoice_Id", Invoice_Id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (!reader.HasRows) return null;

                var lista = new List<User>();

                return MappingHandler.MapReaderToEntities<Payment>(reader); 
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

        public SQLUpdateResult AddPayment(Payment payment)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Payments_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", payment.Id));
                cmd.Parameters.Add(new SqlParameter("Invoice_Id", payment.Invoice_Id));
                cmd.Parameters.Add(new SqlParameter("PaymentType_Id", payment.PaymentType_Id));
                cmd.Parameters.Add(new SqlParameter("Amount", payment.Amount));
                cmd.Parameters.Add(new SqlParameter("Detail", payment.Detail));

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

        public SQLUpdateResult DeletePayment(Payment payment)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Payments_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", payment.Id));

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
