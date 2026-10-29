using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Acquisitions;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class AcquisitionMapper
    {
        private readonly AcquisitionDataAccess data = new AcquisitionDataAccess();

        public int GetNextNumber()
        {
            using (DataTable table = data.GetNextNumber())
                return (int)table.Rows[0]["Number"];
        }

        // Sin coincidencias devuelve null.
        public List<DuplicateVehicle> GetDuplicates(ProspectVehicle vehicle)
        {
            List<DuplicateVehicle> duplicates = MappingHandler.MapTableToEntities<DuplicateVehicle>(data.GetDuplicates(vehicle.License_Plate,
                vehicle.Chassis_Number, vehicle.Engine_Number));
            return duplicates.Count == 0 ? null : duplicates;
        }

        public List<User> GetMechanics(PermissionType patent)
        {
            return MappingHandler.MapTableToEntities<User>(data.GetMechanics(patent.ToString()));
        }

        public bool IsMechanicBusy(Guid Mechanic_Id, DateTime Scheduled_Date, int slotMinutes)
        {
            using (DataTable table = data.IsMechanicBusy(Mechanic_Id, Scheduled_Date, slotMinutes))
                return (int)table.Rows[0]["Inspections"] > 0;
        }

        // Sin turnos libres en el periodo devuelve null.
        public DateTime? GetNextFreeSlot(Guid Mechanic_Id, DateTime from, int slotMinutes, int dayStartHour, int dayEndHour, int maxDays)
        {
            using (DataTable table = data.GetNextFreeSlot(Mechanic_Id, from, slotMinutes, dayStartHour, dayEndHour, maxDays))
            {
                if (table.Rows.Count == 0) return null;
                return (DateTime)table.Rows[0]["Scheduled_Date"];
            }
        }

        public SQLUpdateResult AddProspectVehicle(ProspectVehicle vehicle)
        {
            return DigitVerifierWriteMapper.Save("ProspectVehicles", dvh => data.AddProspectVehicle(vehicle.Id, vehicle.License_Plate,
                vehicle.Chassis_Number, vehicle.Engine_Number, vehicle.Make_Id, vehicle.Model_Id, vehicle.Version_Id, vehicle.Colour_Id,
                vehicle.Year, vehicle.Doors, vehicle.Declared_Kilometers, dvh));
        }

        public SQLUpdateResult AddAcquisition(Acquisition acquisition)
        {
            return DigitVerifierWriteMapper.Save("Acquisitions", dvh => data.Add(acquisition.Id, acquisition.Number, acquisition.Client_Id,
                acquisition.ProspectVehicle_Id, acquisition.Salesman_Id, (int)acquisition.Status, acquisition.Rejection_Reason,
                acquisition.Creation_Date, acquisition.Accepted_Appraisal_Id, acquisition.Agreed_Price, acquisition.Owner_CUIL_CUIT,
                acquisition.Contract_Signed_Date, acquisition.Precharge_Code_08, acquisition.Transfer_Appointment_Date,
                acquisition.Vehicle_Id, acquisition.Closing_Date, dvh));
        }

        public SQLUpdateResult AddInspection(Inspection inspection)
        {
            return DigitVerifierWriteMapper.Save("Inspections", dvh => data.AddInspection(inspection.Id, inspection.Acquisition_Id,
                inspection.Mechanic_Id, inspection.Scheduled_Date, inspection.Performed_Date, inspection.Verified_Kilometers,
                inspection.Result, inspection.Observations, dvh));
        }
    }
}
