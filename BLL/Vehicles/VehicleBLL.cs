using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
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
            return RecordResult(_vehiclemapper.AddMake(make), AuditEventType.MakeCreated, make.Id);
        }
        public SQLUpdateResult DeleteMake(Make make)
        {
            return RecordResult(_vehiclemapper.DeleteMake(make), AuditEventType.MakeDeleted, make.Id);
        }
        public List<Model> GetAllModelsByMake(Make make)
        {
            return _vehiclemapper.GetAllModelsByMake(make.Id);
        }
        public SQLUpdateResult AddModel(Model model)
        {
            model.Id = Guid.NewGuid();
            return RecordResult(_vehiclemapper.AddModel(model), AuditEventType.ModelCreated, model.Id);
        }
        public SQLUpdateResult DeleteModel(Model model)
        {
            return RecordResult(_vehiclemapper.DeleteModel(model), AuditEventType.ModelDeleted, model.Id);
        }
        public List<CarAgency.BE.Version> GetAllVersionsByMakeModel(Make make, Model model)
        {
            return _vehiclemapper.GetAllVersionsByMakeModel(make.Id, model.Id);
        }
        public SQLUpdateResult AddVersion(CarAgency.BE.Version version)
        {
            version.Id = Guid.NewGuid();
            return RecordResult(_vehiclemapper.AddVersion(version), AuditEventType.VersionCreated, version.Id);
        }
        public SQLUpdateResult DeleteVersion(CarAgency.BE.Version version)
        {
            return RecordResult(_vehiclemapper.DeleteVersion(version), AuditEventType.VersionDeleted, version.Id);
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
            return RecordResult(_vehiclemapper.AddVehicle(vehicle), AuditEventType.VehicleCreated, vehicle.Id);
        }

        public SQLUpdateResult UpdateVehicle(Vehicle vehicle)
        {
            return RecordResult(_vehiclemapper.UpdateVehicle(vehicle), AuditEventType.VehicleUpdated, vehicle.Id);
        }

        public SQLUpdateResult DeleteVehicle(Vehicle vehicle)
        {
            return RecordResult(_vehiclemapper.DeleteVehicle(vehicle), AuditEventType.VehicleDeleted, vehicle.Id);
        }

        public List<Vehicle> GetActiveVehiclesByFilters(Guid Make_Id, Guid Model_Id, Guid Version_Id, Guid Colour_Id, double Price_From, double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            return _vehiclemapper.GetActiveVehiclesByFilters(Make_Id, Model_Id, Version_Id, Colour_Id, Price_From, Price_To, Year_From, Year_To, Doors_From, Doors_To, Kilometers_From, Kilometers_To);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, AuditEventType type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(type, targetId);
            return result;
        }
    }
}
