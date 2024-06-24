using CarAgency.Entities;
using CarAgency.Repository;
using CarAgency.Utilities.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehicleBLL
    {
        private VehicleRepository _vehiclerepository;
        public VehicleBLL()
        {
            _vehiclerepository = new VehicleRepository();
        }
        public List<Colour> GetAllColours()
        {
            return _vehiclerepository.GetAllColours();
        }
        public List<Make> GetAllMakes()
        {
            return _vehiclerepository.GetAllMakes();
        }
        public SQLUpdateResult AddMake(Make make)
        {
            make.Id = Guid.NewGuid();
            return _vehiclerepository.AddMake(make);
        }
        public SQLUpdateResult DeleteMake(Make make)
        {
            return _vehiclerepository.DeleteMake(make);
        }
        public List<Model> GetAllModelsByMake(Make make)
        {
            return _vehiclerepository.GetAllModelsByMake(make.Id);
        }
        public SQLUpdateResult AddModel(Model model)
        {
            model.Id = Guid.NewGuid();
            return _vehiclerepository.AddModel(model);
        }
        public SQLUpdateResult DeleteModel(Model model)
        {
            return _vehiclerepository.DeleteModel(model);
        }
        public List<CarAgency.Entities.Version> GetAllVersionsByMakeModel(Make make, Model model)
        {
            return _vehiclerepository.GetAllVersionsByMakeModel(make.Id, model.Id);
        }
        public SQLUpdateResult AddVersion(CarAgency.Entities.Version version)
        {
            version.Id = Guid.NewGuid();
            return _vehiclerepository.AddVersion(version);
        }
        public SQLUpdateResult DeleteVersion(CarAgency.Entities.Version version)
        {
            return _vehiclerepository.DeleteVersion(version);
        }

        public List<Vehicle> GetAll()
        {
            return _vehiclerepository.GetAll();
        }
        
        public Vehicle GetById(Guid id)
        {
            return _vehiclerepository.GetById(id);
        }

        public SQLUpdateResult AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            return _vehiclerepository.AddVehicle(vehicle);
        }

        public SQLUpdateResult UpdateVehicle(Vehicle vehicle)
        {
            return _vehiclerepository.UpdateVehicle(vehicle);
        }

        public SQLUpdateResult DeleteVehicle(Vehicle vehicle)
        {
            return _vehiclerepository.DeleteVehicle(vehicle);
        }

        public List<Vehicle> GetActiveVehiclesByFilters(Guid Make_Id, Guid Model_Id, Guid Version_Id, Guid Colour_Id, double Price_From, double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            return _vehiclerepository.GetActiveVehiclesByFilters(Make_Id, Model_Id, Version_Id, Colour_Id, Price_From, Price_To, Year_From, Year_To, Doors_From, Doors_To, Kilometers_From, Kilometers_To);
        }
    }
}
