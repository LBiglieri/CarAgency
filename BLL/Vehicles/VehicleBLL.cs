using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Mappers.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehicleBLL
    {
        private VehicleMapper _vehiclemapper;
        public VehicleBLL()
        {
            _vehiclemapper = new VehicleMapper();
        }
        public List<Colour> GetAllColours()
        {
            return _vehiclemapper.GetAllColours();
        }
        public List<Make> GetAllMakes()
        {
            return _vehiclemapper.GetAllMakes();
        }
        public SQLUpdateResult AddMake(Make make)
        {
            make.Id = Guid.NewGuid();
            return _vehiclemapper.AddMake(make);
        }
        public SQLUpdateResult DeleteMake(Make make)
        {
            return _vehiclemapper.DeleteMake(make);
        }
        public List<Model> GetAllModelsByMake(Make make)
        {
            return _vehiclemapper.GetAllModelsByMake(make.Id);
        }
        public SQLUpdateResult AddModel(Model model)
        {
            model.Id = Guid.NewGuid();
            return _vehiclemapper.AddModel(model);
        }
        public SQLUpdateResult DeleteModel(Model model)
        {
            return _vehiclemapper.DeleteModel(model);
        }
        public List<CarAgency.BE.Version> GetAllVersionsByMakeModel(Make make, Model model)
        {
            return _vehiclemapper.GetAllVersionsByMakeModel(make.Id, model.Id);
        }
        public SQLUpdateResult AddVersion(CarAgency.BE.Version version)
        {
            version.Id = Guid.NewGuid();
            return _vehiclemapper.AddVersion(version);
        }
        public SQLUpdateResult DeleteVersion(CarAgency.BE.Version version)
        {
            return _vehiclemapper.DeleteVersion(version);
        }

        public List<Vehicle> GetAll()
        {
            return _vehiclemapper.GetAll();
        }
        
        public Vehicle GetById(Guid id)
        {
            return _vehiclemapper.GetById(id);
        }

        public SQLUpdateResult AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            return _vehiclemapper.AddVehicle(vehicle);
        }

        public SQLUpdateResult UpdateVehicle(Vehicle vehicle)
        {
            return _vehiclemapper.UpdateVehicle(vehicle);
        }

        public SQLUpdateResult DeleteVehicle(Vehicle vehicle)
        {
            return _vehiclemapper.DeleteVehicle(vehicle);
        }

        public List<Vehicle> GetActiveVehiclesByFilters(Guid Make_Id, Guid Model_Id, Guid Version_Id, Guid Colour_Id, double Price_From, double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            return _vehiclemapper.GetActiveVehiclesByFilters(Make_Id, Model_Id, Version_Id, Colour_Id, Price_From, Price_To, Year_From, Year_To, Doors_From, Doors_To, Kilometers_From, Kilometers_To);
        }
    }
}
