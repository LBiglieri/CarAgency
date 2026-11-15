using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehiculoBLL
    {
        // Color que viene elegido al cargar un vehiculo: es el mas comun. Se busca por Description, que esta en ingles.
        public const string DefaultColour = "Black";

        private VehiculoMapper _vehiclemapper;
        public VehiculoBLL()
        {
            _vehiclemapper = new VehiculoMapper();
        }
        public List<Color> GetAllColours()
        {
            return _vehiclemapper.GetAllColours();
        }
        public List<Marca> GetAllMakes()
        {
            return _vehiclemapper.GetAllMakes();
        }
        public SQLUpdateResult AddMake(Marca make)
        {
            make.Marca_Id = Guid.NewGuid();
            return RecordResult(_vehiclemapper.AddMake(make), TipoEvento.MakeCreated, make.Marca_Id);
        }
        public SQLUpdateResult DeleteMake(Marca make)
        {
            return RecordResult(_vehiclemapper.DeleteMake(make), TipoEvento.MakeDeleted, make.Marca_Id);
        }
        public List<Modelo> GetAllModelsByMake(Marca make)
        {
            return _vehiclemapper.GetAllModelsByMake(make.Marca_Id);
        }
        public SQLUpdateResult AddModel(Modelo model)
        {
            model.Modelo_Id = Guid.NewGuid();
            return RecordResult(_vehiclemapper.AddModel(model), TipoEvento.ModelCreated, model.Modelo_Id);
        }
        public SQLUpdateResult DeleteModel(Modelo model)
        {
            return RecordResult(_vehiclemapper.DeleteModel(model), TipoEvento.ModelDeleted, model.Modelo_Id);
        }
        public List<CarAgency.BE.Version> GetAllVersionsByMakeModel(Marca make, Modelo model)
        {
            return _vehiclemapper.GetAllVersionsByMakeModel(make.Marca_Id, model.Modelo_Id);
        }
        public SQLUpdateResult AddVersion(CarAgency.BE.Version version)
        {
            version.Version_Id = Guid.NewGuid();
            return RecordResult(_vehiclemapper.AddVersion(version), TipoEvento.VersionCreated, version.Version_Id);
        }
        public SQLUpdateResult DeleteVersion(CarAgency.BE.Version version)
        {
            return RecordResult(_vehiclemapper.DeleteVersion(version), TipoEvento.VersionDeleted, version.Version_Id);
        }

        public List<Vehiculo> GetAll()
        {
            return _vehiclemapper.GetAll();
        }
        
        public Vehiculo GetById(Guid id)
        {
            return _vehiclemapper.GetById(id);
        }

        public SQLUpdateResult AddVehicle(Vehiculo vehicle)
        {
            vehicle.Vehiculo_Id = Guid.NewGuid();
            return RecordResult(_vehiclemapper.AddVehicle(vehicle), TipoEvento.VehicleCreated, vehicle.Vehiculo_Id);
        }

        public SQLUpdateResult UpdateVehicle(Vehiculo vehicle)
        {
            return RecordResult(_vehiclemapper.UpdateVehicle(vehicle), TipoEvento.VehicleUpdated, vehicle.Vehiculo_Id);
        }

        public SQLUpdateResult DeleteVehicle(Vehiculo vehicle)
        {
            return RecordResult(_vehiclemapper.DeleteVehicle(vehicle), TipoEvento.VehicleDeleted, vehicle.Vehiculo_Id);
        }

        public List<Vehiculo> GetActiveVehiclesByFilters(Guid Make_Id, Guid Model_Id, Guid Version_Id, Guid Colour_Id, double Price_From, double Price_To, int Year_From, int Year_To, int Doors_From, int Doors_To, int Kilometers_From, int Kilometers_To)
        {
            return _vehiclemapper.GetActiveVehiclesByFilters(Make_Id, Model_Id, Version_Id, Colour_Id, Price_From, Price_To, Year_From, Year_To, Doors_From, Doors_To, Kilometers_From, Kilometers_To);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
