using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Vehicles
{
    // Catalogo de vehiculos: colores, marcas, modelos, versiones y vehiculos.
    public sealed class VehicleDataAccess : DataAccessBase
    {
        public DataTable GetAllColours() { return Read("Colours_GetAll"); }

        public DataTable GetAllMakes() { return Read("Makes_GetAll"); }
        public DataTable AddMake(Guid id, string description, DvhCalculator dvh)
        {
            return Read("Makes_Insert", dvh, new SqlParameter("@Id", id), new SqlParameter("@Description", description));
        }
        public DataTable DeleteMake(Guid id) { return Read("Makes_Delete", new SqlParameter("@Id", id)); }

        public DataTable GetModelsByMake(Guid makeId) { return Read("Models_GetAllByMake", new SqlParameter("@Make_Id", makeId)); }
        public DataTable AddModel(Guid id, Guid makeId, string description, DvhCalculator dvh)
        {
            return Read("Models_Insert", dvh, new SqlParameter("@Id", id), new SqlParameter("@Make_Id", makeId),
                new SqlParameter("@Description", description));
        }
        public DataTable DeleteModel(Guid id) { return Read("Models_Delete", new SqlParameter("@Id", id)); }

        public DataTable GetVersionsByMakeModel(Guid makeId, Guid modelId)
        {
            return Read("Versions_GetAllByMakeModel", new SqlParameter("@Make_Id", makeId), new SqlParameter("@Model_Id", modelId));
        }
        public DataTable AddVersion(Guid id, Guid makeId, Guid modelId, string description, DvhCalculator dvh)
        {
            return Read("Versions_Insert", dvh, new SqlParameter("@Id", id), new SqlParameter("@Make_Id", makeId),
                new SqlParameter("@Model_Id", modelId), new SqlParameter("@Description", description));
        }
        public DataTable DeleteVersion(Guid id) { return Read("Versions_Delete", new SqlParameter("@Id", id)); }

        public DataTable GetAll() { return Read("Vehicles_GetAll"); }
        public DataTable GetById(Guid id) { return Read("Vehicles_GetById", new SqlParameter("@Id", id)); }

        public DataTable Add(Guid id, string licensePlate, Guid makeId, Guid modelId, Guid versionId, Guid colourId, double price,
            int kilometers, int doors, int year, string observations, string opcionals, string imageLink, DvhCalculator dvh)
        {
            return Read("Vehicles_Add", dvh, VehicleParameters(id, licensePlate, makeId, modelId, versionId, colourId, price,
                kilometers, doors, year, observations, opcionals, imageLink));
        }

        public DataTable Update(Guid id, string licensePlate, Guid makeId, Guid modelId, Guid versionId, Guid colourId, double price,
            int kilometers, int doors, int year, string observations, string opcionals, string imageLink, DvhCalculator dvh)
        {
            return Read("Vehicles_Update", dvh, VehicleParameters(id, licensePlate, makeId, modelId, versionId, colourId, price,
                kilometers, doors, year, observations, opcionals, imageLink));
        }

        public DataTable Delete(Guid id) { return Read("Vehicles_Delete", new SqlParameter("@Id", id)); }

        public DataTable GetActiveByFilters(Guid makeId, Guid modelId, Guid versionId, Guid colourId, double priceFrom, double priceTo,
            int yearFrom, int yearTo, int doorsFrom, int doorsTo, int kilometersFrom, int kilometersTo)
        {
            return Read("Vehicles_GetActiveVehiclesByFilters",
                new SqlParameter("@Make_Id", makeId),
                new SqlParameter("@Model_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Colour_Id", colourId),
                new SqlParameter("@Price_From", priceFrom),
                new SqlParameter("@Price_To", priceTo),
                new SqlParameter("@Year_From", yearFrom),
                new SqlParameter("@Year_To", yearTo),
                new SqlParameter("@Doors_From", doorsFrom),
                new SqlParameter("@Doors_To", doorsTo),
                new SqlParameter("@Kilometers_From", kilometersFrom),
                new SqlParameter("@Kilometers_To", kilometersTo));
        }

        private static SqlParameter[] VehicleParameters(Guid id, string licensePlate, Guid makeId, Guid modelId, Guid versionId,
            Guid colourId, double price, int kilometers, int doors, int year, string observations, string opcionals, string imageLink)
        {
            return new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@License_Plate", licensePlate),
                new SqlParameter("@Make_Id", makeId),
                new SqlParameter("@Model_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Colour_Id", colourId),
                new SqlParameter("@Price", price),
                new SqlParameter("@Kilometers", kilometers),
                new SqlParameter("@Doors", doors),
                new SqlParameter("@Year", year),
                new SqlParameter("@Observations", observations),
                new SqlParameter("@Opcionals", opcionals),
                new SqlParameter("@ImageLink", imageLink)
            };
        }
    }
}
