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
                acquisition.ProspectVehicle_Id, acquisition.Salesman_Id, (int)acquisition.Status, (int?)acquisition.Rejection_Reason,
                acquisition.Creation_Date, acquisition.Accepted_Appraisal_Id, acquisition.Agreed_Price, acquisition.Owner_CUIL_CUIT,
                acquisition.Contract_Signed_Date, acquisition.Precharge_Code_08, acquisition.Transfer_Appointment_Date,
                acquisition.Vehicle_Id, acquisition.Closing_Date, dvh));
        }

        public SQLUpdateResult AddInspection(Inspection inspection)
        {
            return DigitVerifierWriteMapper.Save("Inspections", dvh => data.AddInspection(inspection.Id, inspection.Acquisition_Id,
                inspection.Mechanic_Id, inspection.Scheduled_Date, inspection.Performed_Date, inspection.Verified_Kilometers,
                (int?)inspection.Result, inspection.Observations, dvh));
        }

        // El MappingHandler no convierte NULL ni enums: la adquisicion se arma a mano.
        public Acquisition GetById(Guid id)
        {
            using (DataTable table = data.GetById(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                Acquisition acquisition = new Acquisition();
                acquisition.Id = (Guid)row["Id"];
                acquisition.Number = (int)row["Number"];
                acquisition.Client_Id = (Guid)row["Client_Id"];
                acquisition.ProspectVehicle_Id = (Guid)row["ProspectVehicle_Id"];
                acquisition.Salesman_Id = (Guid)row["Salesman_Id"];
                acquisition.Status = (AcquisitionStatus)(int)row["Status"];
                acquisition.Rejection_Reason = (RejectionReason?)(row["Rejection_Reason"] as int?);
                acquisition.Creation_Date = (DateTime)row["Creation_Date"];
                acquisition.Accepted_Appraisal_Id = row["Accepted_Appraisal_Id"] as Guid?;
                acquisition.Agreed_Price = row["Agreed_Price"] as double?;
                acquisition.Owner_CUIL_CUIT = row["Owner_CUIL_CUIT"] as string;
                acquisition.Contract_Signed_Date = row["Contract_Signed_Date"] as DateTime?;
                acquisition.Precharge_Code_08 = row["Precharge_Code_08"] as string;
                acquisition.Transfer_Appointment_Date = row["Transfer_Appointment_Date"] as DateTime?;
                acquisition.Vehicle_Id = row["Vehicle_Id"] as Guid?;
                acquisition.Closing_Date = row["Closing_Date"] as DateTime?;
                return acquisition;
            }
        }

        // Sin inspecciones pendientes devuelve null.
        public List<ScheduledInspection> GetScheduledInspections(Guid Mechanic_Id)
        {
            List<ScheduledInspection> inspections = MappingHandler.MapTableToEntities<ScheduledInspection>(data.GetScheduledInspections(Mechanic_Id));
            return inspections.Count == 0 ? null : inspections;
        }

        public InspectionDetail GetInspectionDetail(Guid id)
        {
            return MappingHandler.MapTableToEntities<InspectionDetail>(data.GetInspectionDetail(id)).FirstOrDefault();
        }

        public List<InspectionItem> GetInspectionItems()
        {
            return MappingHandler.MapTableToEntities<InspectionItem>(data.GetInspectionItems());
        }

        // Sin fotos devuelve null.
        public List<InspectionPhoto> GetInspectionPhotos(Guid Inspection_Id)
        {
            List<InspectionPhoto> photos = MappingHandler.MapTableToEntities<InspectionPhoto>(data.GetInspectionPhotos(Inspection_Id));
            return photos.Count == 0 ? null : photos;
        }

        // InspectionPhotos no esta protegida con digitos verificadores.
        public SQLUpdateResult AddInspectionPhoto(InspectionPhoto photo)
        {
            return MappingHandler.MapUpdateResult(data.AddInspectionPhoto(photo.Id, photo.Inspection_Id, photo.FileName,
                photo.FileContent, photo.UploadedDate));
        }

        public SQLUpdateResult DeleteInspectionPhoto(InspectionPhoto photo)
        {
            return MappingHandler.MapUpdateResult(data.DeleteInspectionPhoto(photo.Id));
        }

        // Las tres tablas se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult RecordInspection(Inspection inspection, Acquisition acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.RecordInspection(InspectionRow(inspection),
                inspection.Scores.Select(ScoreRow).ToList(), AcquisitionRow(acquisition), digitVerifier.Dvh("Inspections"),
                digitVerifier.Dvh("InspectionScores"), digitVerifier.Dvh("Acquisitions")), "Inspections", "InspectionScores", "Acquisitions");
        }

        // Filas completas de cada tabla, con el nombre de columna como clave.
        private static IDictionary<string, object> InspectionRow(Inspection inspection)
        {
            return new Dictionary<string, object>
            {
                { "Id", inspection.Id },
                { "Acquisition_Id", inspection.Acquisition_Id },
                { "Mechanic_Id", inspection.Mechanic_Id },
                { "Scheduled_Date", inspection.Scheduled_Date },
                { "Performed_Date", inspection.Performed_Date },
                { "Verified_Kilometers", inspection.Verified_Kilometers },
                { "Result", (int?)inspection.Result },
                { "Observations", inspection.Observations }
            };
        }

        private static IDictionary<string, object> ScoreRow(InspectionScore score)
        {
            return new Dictionary<string, object>
            {
                { "Inspection_Id", score.Inspection_Id },
                { "InspectionItem_Id", score.InspectionItem_Id },
                { "Score", (int)score.Score },
                { "Observations", score.Observations }
            };
        }

        private static IDictionary<string, object> AcquisitionRow(Acquisition acquisition)
        {
            return new Dictionary<string, object>
            {
                { "Id", acquisition.Id },
                { "Number", acquisition.Number },
                { "Client_Id", acquisition.Client_Id },
                { "ProspectVehicle_Id", acquisition.ProspectVehicle_Id },
                { "Salesman_Id", acquisition.Salesman_Id },
                { "Status", (int)acquisition.Status },
                { "Rejection_Reason", (int?)acquisition.Rejection_Reason },
                { "Creation_Date", acquisition.Creation_Date },
                { "Accepted_Appraisal_Id", acquisition.Accepted_Appraisal_Id },
                { "Agreed_Price", acquisition.Agreed_Price },
                { "Owner_CUIL_CUIT", acquisition.Owner_CUIL_CUIT },
                { "Contract_Signed_Date", acquisition.Contract_Signed_Date },
                { "Precharge_Code_08", acquisition.Precharge_Code_08 },
                { "Transfer_Appointment_Date", acquisition.Transfer_Appointment_Date },
                { "Vehicle_Id", acquisition.Vehicle_Id },
                { "Closing_Date", acquisition.Closing_Date }
            };
        }
    }
}
