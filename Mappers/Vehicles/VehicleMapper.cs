using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Vehicles;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class VehicleMapper
    {
        private readonly VehicleDataAccess data = new VehicleDataAccess();

        public List<Colour> GetAllColours()
        {
            return MappingHandler.MapTableToEntities<Colour>(data.GetAllColours());
        }

        public List<Make> GetAllMakes()
        {
            return MappingHandler.MapTableToEntities<Make>(data.GetAllMakes());
        }

        public SQLUpdateResult AddMake(Make make)
        {
            return DigitVerifierWriteMapper.Save("Makes", dvh => data.AddMake(make.Id, make.Description, dvh));
        }

        public SQLUpdateResult DeleteMake(Make make)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteMake(make.Id), "Makes");
        }

        public List<Model> GetAllModelsByMake(Guid Make_Id)
        {
            return MappingHandler.MapTableToEntities<Model>(data.GetModelsByMake(Make_Id));
        }

        public SQLUpdateResult AddModel(Model model)
        {
            return DigitVerifierWriteMapper.Save("Models", dvh => data.AddModel(model.Id, model.Make_Id, model.Description, dvh));
        }

        public SQLUpdateResult DeleteModel(Model model)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteModel(model.Id), "Models");
        }

        public List<CarAgency.BE.Version> GetAllVersionsByMakeModel(Guid Make_Id, Guid Model_Id)
        {
            return MappingHandler.MapTableToEntities<CarAgency.BE.Version>(data.GetVersionsByMakeModel(Make_Id, Model_Id));
        }

        public SQLUpdateResult AddVersion(CarAgency.BE.Version version)
        {
            return DigitVerifierWriteMapper.Save("Versions", dvh => data.AddVersion(version.Id, version.Make_Id, version.Model_Id, version.Description, dvh));
        }

        public SQLUpdateResult DeleteVersion(CarAgency.BE.Version version)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteVersion(version.Id), "Versions");
        }

        public List<Vehicle> GetAll()
        {
            return MappingHandler.MapTableToEntities<Vehicle>(data.GetAll());
        }

        // Si no existe devuelve un Vehicle vacio y no null: los formularios de venta y facturacion
        // lo usan sin chequear.
        public Vehicle GetById(Guid id)
        {
            return MappingHandler.MapTableToEntities<Vehicle>(data.GetById(id)).FirstOrDefault() ?? new Vehicle();
        }

        public SQLUpdateResult AddVehicle(Vehicle vehicle)
        {
            return DigitVerifierWriteMapper.Save("Vehicles", dvh => data.Add(vehicle.Id, vehicle.License_Plate, vehicle.Make_Id, vehicle.Model_Id,
                vehicle.Version_Id, vehicle.Colour_Id, vehicle.Price, vehicle.Kilometers, vehicle.Doors, vehicle.Year,
                vehicle.Observations, vehicle.Opcionals, vehicle.ImageLink, dvh));
        }

        public SQLUpdateResult UpdateVehicle(Vehicle vehicle)
        {
            return DigitVerifierWriteMapper.Save("Vehicles", dvh => data.Update(vehicle.Id, vehicle.License_Plate, vehicle.Make_Id, vehicle.Model_Id,
                vehicle.Version_Id, vehicle.Colour_Id, vehicle.Price, vehicle.Kilometers, vehicle.Doors, vehicle.Year,
                vehicle.Observations, vehicle.Opcionals, vehicle.ImageLink, dvh));
        }

        public SQLUpdateResult DeleteVehicle(Vehicle vehicle)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(vehicle.Id), "Vehicles");
        }

        public List<Vehicle> GetActiveVehiclesByFilters(Guid Make_Id, Guid Model_Id, Guid Version_Id, Guid Colour_Id, double Price_From,
            double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            return MappingHandler.MapTableToEntities<Vehicle>(data.GetActiveByFilters(Make_Id, Model_Id, Version_Id, Colour_Id,
                Price_From, Price_To, Year_From, Year_To, Doors_From, Doors_To, Kilometers_From, Kilometers_To));
        }
    }
}
