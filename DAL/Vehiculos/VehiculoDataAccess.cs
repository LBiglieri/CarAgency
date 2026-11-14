using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Vehiculos
{
    // Catalogo de vehiculos: colores, marcas, modelos, versiones y vehiculos.
    public sealed class VehiculoDataAccess : DataAccessBase
    {
        public DataTable GetAllColours() { return Read("Color_GetAll"); }

        public DataTable GetAllMakes() { return Read("Marca_GetAll"); }
        public DataTable AddMake(Guid id, string description, DvhCalculator dvh)
        {
            return Read("Marca_Insert", dvh, new SqlParameter("@Marca_Id", id), new SqlParameter("@Descripcion", description));
        }
        public DataTable DeleteMake(Guid id) { return Read("Marca_Delete", new SqlParameter("@Marca_Id", id)); }

        public DataTable GetModelsByMake(Guid makeId) { return Read("Modelo_GetAllByMake", new SqlParameter("@Marca_Id", makeId)); }
        public DataTable AddModel(Guid id, Guid makeId, string description, DvhCalculator dvh)
        {
            return Read("Modelo_Insert", dvh, new SqlParameter("@Modelo_Id", id), new SqlParameter("@Marca_Id", makeId),
                new SqlParameter("@Descripcion", description));
        }
        public DataTable DeleteModel(Guid id) { return Read("Modelo_Delete", new SqlParameter("@Modelo_Id", id)); }

        public DataTable GetVersionsByMakeModel(Guid makeId, Guid modelId)
        {
            return Read("Version_GetAllByMakeModel", new SqlParameter("@Marca_Id", makeId), new SqlParameter("@Modelo_Id", modelId));
        }
        public DataTable AddVersion(Guid id, Guid makeId, Guid modelId, string description, DvhCalculator dvh)
        {
            return Read("Version_Insert", dvh, new SqlParameter("@Version_Id", id), new SqlParameter("@Marca_Id", makeId),
                new SqlParameter("@Modelo_Id", modelId), new SqlParameter("@Descripcion", description));
        }
        public DataTable DeleteVersion(Guid id) { return Read("Version_Delete", new SqlParameter("@Version_Id", id)); }

        public DataTable GetAll() { return Read("Vehiculo_GetAll"); }
        public DataTable GetById(Guid id) { return Read("Vehiculo_GetById", new SqlParameter("@Vehiculo_Id", id)); }

        public DataTable Add(Guid id, string licensePlate, Guid makeId, Guid modelId, Guid versionId, Guid colourId, double price,
            int kilometers, int doors, int year, string observations, string opcionals, string imageLink, DvhCalculator dvh)
        {
            return Read("Vehiculo_Add", dvh, VehicleParameters(id, licensePlate, makeId, modelId, versionId, colourId, price,
                kilometers, doors, year, observations, opcionals, imageLink));
        }

        public DataTable Update(Guid id, string licensePlate, Guid makeId, Guid modelId, Guid versionId, Guid colourId, double price,
            int kilometers, int doors, int year, string observations, string opcionals, string imageLink, DvhCalculator dvh)
        {
            return Read("Vehiculo_Update", dvh, VehicleParameters(id, licensePlate, makeId, modelId, versionId, colourId, price,
                kilometers, doors, year, observations, opcionals, imageLink));
        }

        public DataTable Delete(Guid id) { return Read("Vehiculo_Delete", new SqlParameter("@Vehiculo_Id", id)); }

        public DataTable GetActiveByFilters(Guid makeId, Guid modelId, Guid versionId, Guid colourId, double priceFrom, double priceTo,
            int yearFrom, int yearTo, int doorsFrom, int doorsTo, int kilometersFrom, int kilometersTo)
        {
            return Read("Vehiculo_GetActiveVehiclesByFilters",
                new SqlParameter("@Marca_Id", makeId),
                new SqlParameter("@Modelo_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Color_Id", colourId),
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
                new SqlParameter("@Vehiculo_Id", id),
                new SqlParameter("@Patente", licensePlate),
                new SqlParameter("@Marca_Id", makeId),
                new SqlParameter("@Modelo_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Color_Id", colourId),
                new SqlParameter("@Precio", price),
                new SqlParameter("@Kilometraje", kilometers),
                new SqlParameter("@Puertas", doors),
                new SqlParameter("@Anio", year),
                new SqlParameter("@Observaciones", observations),
                new SqlParameter("@Opcionales", opcionals),
                new SqlParameter("@LinkImagen", imageLink)
            };
        }
    }
}
