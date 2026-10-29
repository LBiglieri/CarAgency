using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;

namespace CarAgency.BLL
{
    public class AcquisitionBLL
    {
        // Turnos de inspeccion: de lunes a viernes, en punto, dentro del horario del taller.
        public const int InspectionSlotMinutes = 60;
        public const int WorkdayStartHour = 9;
        public const int WorkdayEndHour = 18;
        private const int NextFreeSlotSearchDays = 30;

        private AcquisitionMapper _acquisitionmapper;
        public AcquisitionBLL()
        {
            _acquisitionmapper = new AcquisitionMapper();
        }
        public List<User> GetMechanics()
        {
            return _acquisitionmapper.GetMechanics(PermissionType.RegisterInspectionForm);
        }
        public List<TimeSpan> GetInspectionHours()
        {
            List<TimeSpan> hours = new List<TimeSpan>();
            TimeSpan slot = TimeSpan.FromMinutes(InspectionSlotMinutes);
            for (TimeSpan hour = TimeSpan.FromHours(WorkdayStartHour); hour + slot <= TimeSpan.FromHours(WorkdayEndHour); hour += slot)
                hours.Add(hour);
            return hours;
        }
        public List<DuplicateVehicle> GetDuplicates(ProspectVehicle vehicle)
        {
            NormalizeIdentification(vehicle);
            return _acquisitionmapper.GetDuplicates(vehicle);
        }
        public bool IsMechanicAvailable(Guid Mechanic_Id, DateTime Scheduled_Date)
        {
            ValidateInspectionSlot(Scheduled_Date);
            return !_acquisitionmapper.IsMechanicBusy(Mechanic_Id, Scheduled_Date, InspectionSlotMinutes);
        }
        public DateTime GetNextFreeSlot(Guid Mechanic_Id, DateTime from)
        {
            DateTime? slot = _acquisitionmapper.GetNextFreeSlot(Mechanic_Id, from, InspectionSlotMinutes, WorkdayStartHour, WorkdayEndHour,
                NextFreeSlotSearchDays);
            if (slot == null)
                throw new TranslatableException("NoFreeInspectionSlot", "El mecanico no tiene turnos libres en los proximos {0} dias.", NextFreeSlotSearchDays);
            return slot.Value;
        }

        // Vehiculo, adquisicion e inspeccion se graban uno detras del otro, como el resto de las altas.
        public SQLUpdateResult AddAcquisition(Acquisition acquisition)
        {
            ProspectVehicle vehicle = acquisition.ProspectVehicle;
            Inspection inspection = acquisition.Inspection;

            if (GetDuplicates(vehicle) != null)
                throw new TranslatableException("VehicleAlreadyRegistered", "Ya existe un vehiculo en inventario o una adquisicion abierta con la misma patente, chasis o motor.");
            if (!IsMechanicAvailable(inspection.Mechanic_Id, inspection.Scheduled_Date))
                throw new TranslatableException("MechanicNotAvailable", "El mecanico ya tiene una inspeccion en esa franja.");

            vehicle.Id = Guid.NewGuid();
            acquisition.Id = Guid.NewGuid();
            acquisition.Number = _acquisitionmapper.GetNextNumber();
            acquisition.ProspectVehicle_Id = vehicle.Id;
            acquisition.Salesman_Id = SessionHandler.Instance.GetId();
            acquisition.Status = AcquisitionStatus.InspectionScheduled;
            acquisition.Creation_Date = DateTime.Now;
            inspection.Id = Guid.NewGuid();
            inspection.Acquisition_Id = acquisition.Id;

            SQLUpdateResult result = _acquisitionmapper.AddProspectVehicle(vehicle);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.AddAcquisition(acquisition);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.AddInspection(inspection);
            return RecordResult(result, AuditEventType.AcquisitionCreated, acquisition.Id);
        }

        private void ValidateInspectionSlot(DateTime Scheduled_Date)
        {
            if (Scheduled_Date <= DateTime.Now)
                throw new TranslatableException("InspectionDateInPast", "La inspeccion no puede programarse en una fecha u hora pasada.");
            if (Scheduled_Date.DayOfWeek == DayOfWeek.Saturday || Scheduled_Date.DayOfWeek == DayOfWeek.Sunday
                || !GetInspectionHours().Contains(Scheduled_Date.TimeOfDay))
                throw new TranslatableException("InspectionOutsideWorkingHours", "Las inspecciones se programan de lunes a viernes, de {0} a {1} hs, en turnos de {2} minutos.",
                    WorkdayStartHour, WorkdayEndHour, InspectionSlotMinutes);
        }

        // Patente, chasis y motor se comparan contra el inventario: se guardan sin espacios y en mayusculas.
        private static void NormalizeIdentification(ProspectVehicle vehicle)
        {
            vehicle.License_Plate = vehicle.License_Plate.Trim().ToUpperInvariant();
            vehicle.Chassis_Number = vehicle.Chassis_Number.Trim().ToUpperInvariant();
            vehicle.Engine_Number = vehicle.Engine_Number.Trim().ToUpperInvariant();
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, AuditEventType type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(type, targetId);
            return result;
        }
    }
}
