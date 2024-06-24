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
    public class VehicleRepository : BaseRepository
    {
        public List<Colour> GetAllColours()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Colours_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                return MappingHandler.MapReaderToEntities<Colour>(reader);
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
        public List<Make> GetAllMakes()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Makes_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                return MappingHandler.MapReaderToEntities<Make>(reader);
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
        public SQLUpdateResult AddMake(Make make)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Makes_Insert", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", make.Id));
                cmd.Parameters.Add(new SqlParameter("Description", make.Description));

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
        public SQLUpdateResult DeleteMake(Make make)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Makes_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", make.Id));

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
        public List<Model> GetAllModelsByMake(Guid Make_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Models_GetAllByMake", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Make_Id", Make_Id);

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                return MappingHandler.MapReaderToEntities<Model>(reader);
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
        public SQLUpdateResult AddModel(Model model)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Models_Insert", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", model.Id));
                cmd.Parameters.Add(new SqlParameter("Make_Id", model.Make_Id));
                cmd.Parameters.Add(new SqlParameter("Description", model.Description));

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
        public SQLUpdateResult DeleteModel(Model model)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Models_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", model.Id));

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
        public List<CarAgency.Entities.Version> GetAllVersionsByMakeModel(Guid Make_Id, Guid Model_Id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Versions_GetAllByMakeModel", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Make_Id", Make_Id);
                cmd.Parameters.AddWithValue("Model_Id", Model_Id);

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                return MappingHandler.MapReaderToEntities<CarAgency.Entities.Version>(reader);
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
        public SQLUpdateResult AddVersion(CarAgency.Entities.Version version)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Versions_Insert", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", version.Id));
                cmd.Parameters.Add(new SqlParameter("Make_Id", version.Make_Id));
                cmd.Parameters.Add(new SqlParameter("Model_Id", version.Model_Id));
                cmd.Parameters.Add(new SqlParameter("Description", version.Description));

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
        public SQLUpdateResult DeleteVersion(CarAgency.Entities.Version version)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Versions_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", version.Id));

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

        public List<Vehicle> GetAll()
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Vehicles_GetAll", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                var lista = new List<User>();

                return MappingHandler.MapReaderToEntities<Vehicle>(reader); 
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

        public Vehicle GetById(Guid id)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Vehicles_GetById", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", id));

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;
                
                Vehicle vehicle = new Vehicle();

                while (reader.Read())
                {
                    var Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    var License_Plate = reader.GetString(reader.GetOrdinal("License_Plate"));
                    var Make_Id = reader.GetGuid(reader.GetOrdinal("Make_Id"));
                    var Make_Description = reader.GetString(reader.GetOrdinal("Make_Description"));
                    var Model_Id = reader.GetGuid(reader.GetOrdinal("Model_Id"));
                    var Model_Description = reader.GetString(reader.GetOrdinal("Model_Description"));
                    var Version_Id = reader.GetGuid(reader.GetOrdinal("Version_Id"));
                    var Version_Description = reader.GetString(reader.GetOrdinal("Version_Description"));
                    var Colour_Id = reader.GetGuid(reader.GetOrdinal("Colour_Id"));
                    var Colour_Description = reader.GetString(reader.GetOrdinal("Colour_Description"));
                    var Price = reader.GetDouble(reader.GetOrdinal("Price"));
                    var Opcionals = reader.GetString(reader.GetOrdinal("Opcionals"));
                    var Observations = reader.GetString(reader.GetOrdinal("Observations"));
                    var Doors = reader.GetInt32(reader.GetOrdinal("Doors"));
                    var Year = reader.GetInt32(reader.GetOrdinal("Year"));
                    var Kilometers = reader.GetInt32(reader.GetOrdinal("Kilometers"));
                    var ImageLink = reader.GetString(reader.GetOrdinal("ImageLink"));


                    vehicle.Id = Id;
                    vehicle.License_Plate = License_Plate;
                    vehicle.Make_Id = Make_Id;
                    vehicle.Make_Description = Make_Description;
                    vehicle.Model_Id = Model_Id;
                    vehicle.Model_Description = Model_Description;
                    vehicle.Version_Id = Version_Id;
                    vehicle.Version_Description = Version_Description;
                    vehicle.Colour_Id = Colour_Id;
                    vehicle.Colour_Description = Colour_Description;
                    vehicle.Price = Price;

                    vehicle.Opcionals = Opcionals;
                    vehicle.Observations = Observations;
                    vehicle.Doors = Doors;
                    vehicle.Year = Year;
                    vehicle.Kilometers = Kilometers;
                    vehicle.ImageLink = ImageLink;
                }

                return vehicle;
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

        public SQLUpdateResult AddVehicle(Vehicle vehicle)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Vehicles_Add", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", vehicle.Id));
                cmd.Parameters.Add(new SqlParameter("License_Plate", vehicle.License_Plate));
                cmd.Parameters.Add(new SqlParameter("Make_Id", vehicle.Make_Id));
                cmd.Parameters.Add(new SqlParameter("Model_Id", vehicle.Model_Id));
                cmd.Parameters.Add(new SqlParameter("Version_Id", vehicle.Version_Id));
                cmd.Parameters.Add(new SqlParameter("Colour_Id", vehicle.Colour_Id));
                cmd.Parameters.Add(new SqlParameter("Price", vehicle.Price));
                cmd.Parameters.Add(new SqlParameter("Kilometers", vehicle.Kilometers));
                cmd.Parameters.Add(new SqlParameter("Doors", vehicle.Doors));
                cmd.Parameters.Add(new SqlParameter("Year", vehicle.Year));
                cmd.Parameters.Add(new SqlParameter("Observations", vehicle.Observations));
                cmd.Parameters.Add(new SqlParameter("Opcionals", vehicle.Opcionals));
                cmd.Parameters.Add(new SqlParameter("ImageLink", vehicle.ImageLink));

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

        public SQLUpdateResult UpdateVehicle(Vehicle vehicle)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Vehicles_Update", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", vehicle.Id));
                cmd.Parameters.Add(new SqlParameter("License_Plate", vehicle.License_Plate));
                cmd.Parameters.Add(new SqlParameter("Make_Id", vehicle.Make_Id));
                cmd.Parameters.Add(new SqlParameter("Model_Id", vehicle.Model_Id));
                cmd.Parameters.Add(new SqlParameter("Version_Id", vehicle.Version_Id));
                cmd.Parameters.Add(new SqlParameter("Colour_Id", vehicle.Colour_Id));
                cmd.Parameters.Add(new SqlParameter("Price", vehicle.Price));
                cmd.Parameters.Add(new SqlParameter("Kilometers", vehicle.Kilometers));
                cmd.Parameters.Add(new SqlParameter("Doors", vehicle.Doors));
                cmd.Parameters.Add(new SqlParameter("Year", vehicle.Year));
                cmd.Parameters.Add(new SqlParameter("Observations", vehicle.Observations));
                cmd.Parameters.Add(new SqlParameter("Opcionals", vehicle.Opcionals));
                cmd.Parameters.Add(new SqlParameter("ImageLink", vehicle.ImageLink));

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

        public SQLUpdateResult DeleteVehicle(Vehicle vehicle)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Vehicles_Delete", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("Id", vehicle.Id));

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
        public List<Vehicle> GetActiveVehiclesByFilters(Guid Make_Id,Guid Model_Id,Guid Version_Id, Guid Colour_Id, double Price_From, double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            SqlConnection sql = new SqlConnection(base.GetConnectionString());
            IDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Vehicles_GetActiveVehiclesByFilters", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Make_Id", Make_Id);
                cmd.Parameters.AddWithValue("Model_Id", Model_Id);
                cmd.Parameters.AddWithValue("Version_Id", Version_Id);
                cmd.Parameters.AddWithValue("Colour_Id", Colour_Id);
                cmd.Parameters.AddWithValue("Price_From", Price_From);
                cmd.Parameters.AddWithValue("Price_To", Price_To);
                cmd.Parameters.AddWithValue("Year_From", Year_From);
                cmd.Parameters.AddWithValue("Year_To", Year_To);
                cmd.Parameters.AddWithValue("Doors_From", Doors_From);
                cmd.Parameters.AddWithValue("Doors_To", Doors_To);
                cmd.Parameters.AddWithValue("Kilometers_From", Kilometers_From);
                cmd.Parameters.AddWithValue("Kilometers_To", Kilometers_To);

                sql.Open();
                reader = cmd.ExecuteReader();

                if (reader == null) return null;

                return MappingHandler.MapReaderToEntities<Vehicle>(reader);
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
