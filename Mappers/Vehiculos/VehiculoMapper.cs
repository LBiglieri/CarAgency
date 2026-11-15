using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Vehiculos;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class VehiculoMapper
    {
        private readonly VehiculoDataAccess data = new VehiculoDataAccess();

        public List<Color> GetAllColours()
        {
            return MappingHandler.MapTableToEntities<Color>(data.GetAllColours());
        }

        public List<Marca> GetAllMakes()
        {
            return MappingHandler.MapTableToEntities<Marca>(data.GetAllMakes());
        }

        public SQLUpdateResult AddMake(Marca make)
        {
            return DigitVerifierWriteMapper.Save("Marca", dvh => data.AddMake(make.Marca_Id, make.Descripcion, dvh));
        }

        public SQLUpdateResult DeleteMake(Marca make)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteMake(make.Marca_Id), "Marca");
        }

        public List<Modelo> GetAllModelsByMake(Guid Make_Id)
        {
            return MappingHandler.MapTableToEntities<Modelo>(data.GetModelsByMake(Make_Id));
        }

        public SQLUpdateResult AddModel(Modelo model)
        {
            return DigitVerifierWriteMapper.Save("Modelo", dvh => data.AddModel(model.Modelo_Id, model.Marca_Id, model.Descripcion, dvh));
        }

        public SQLUpdateResult DeleteModel(Modelo model)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteModel(model.Modelo_Id), "Modelo");
        }

        public List<CarAgency.BE.Version> GetAllVersionsByMakeModel(Guid Make_Id, Guid Model_Id)
        {
            return MappingHandler.MapTableToEntities<CarAgency.BE.Version>(data.GetVersionsByMakeModel(Make_Id, Model_Id));
        }

        public SQLUpdateResult AddVersion(CarAgency.BE.Version version)
        {
            return DigitVerifierWriteMapper.Save("Version", dvh => data.AddVersion(version.Version_Id, version.Marca_Id, version.Modelo_Id, version.Descripcion, dvh));
        }

        public SQLUpdateResult DeleteVersion(CarAgency.BE.Version version)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteVersion(version.Version_Id), "Version");
        }

        public List<Vehiculo> GetAll()
        {
            return MappingHandler.MapTableToEntities<Vehiculo>(data.GetAll());
        }

        // Si no existe devuelve un Vehicle vacio y no null: los formularios de venta y facturacion
        // lo usan sin chequear.
        public Vehiculo GetById(Guid id)
        {
            return MappingHandler.MapTableToEntities<Vehiculo>(data.GetById(id)).FirstOrDefault() ?? new Vehiculo();
        }

        public SQLUpdateResult AddVehicle(Vehiculo vehicle)
        {
            return DigitVerifierWriteMapper.Save("Vehiculo", dvh => data.Add(vehicle.Vehiculo_Id, vehicle.Patente, vehicle.Marca_Id, vehicle.Modelo_Id,
                vehicle.Version_Id, vehicle.Color_Id, vehicle.Precio, vehicle.Kilometraje, vehicle.Puertas, vehicle.Anio,
                vehicle.Observaciones, vehicle.Opcionales, vehicle.LinkImagen, dvh));
        }

        public SQLUpdateResult UpdateVehicle(Vehiculo vehicle)
        {
            return DigitVerifierWriteMapper.Save("Vehiculo", dvh => data.Update(vehicle.Vehiculo_Id, vehicle.Patente, vehicle.Marca_Id, vehicle.Modelo_Id,
                vehicle.Version_Id, vehicle.Color_Id, vehicle.Precio, vehicle.Kilometraje, vehicle.Puertas, vehicle.Anio,
                vehicle.Observaciones, vehicle.Opcionales, vehicle.LinkImagen, dvh));
        }

        public SQLUpdateResult DeleteVehicle(Vehiculo vehicle)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(vehicle.Vehiculo_Id), "Vehiculo");
        }

        public List<Vehiculo> GetActiveVehiclesByFilters(Guid Make_Id, Guid Model_Id, Guid Version_Id, Guid Colour_Id, double Price_From,
            double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            return MappingHandler.MapTableToEntities<Vehiculo>(data.GetActiveByFilters(Make_Id, Model_Id, Version_Id, Colour_Id,
                Price_From, Price_To, Year_From, Year_To, Doors_From, Doors_To, Kilometers_From, Kilometers_To));
        }
    }
}
