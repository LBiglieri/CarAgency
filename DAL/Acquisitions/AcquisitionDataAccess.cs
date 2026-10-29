using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Acquisitions
{
    // Compra de vehiculos a clientes (PN2): vehiculo ofrecido, adquisicion e inspeccion mecanica.
    public sealed class AcquisitionDataAccess : DataAccessBase
    {
        public DataTable GetNextNumber() { return Read("Acquisitions_GetNextNumber"); }

        public DataTable GetDuplicates(string licensePlate, string chassisNumber, string engineNumber)
        {
            return Read("Acquisitions_GetDuplicates",
                new SqlParameter("@License_Plate", licensePlate),
                new SqlParameter("@Chassis_Number", chassisNumber),
                new SqlParameter("@Engine_Number", engineNumber));
        }

        public DataTable GetMechanics(string patentType) { return Read("Inspections_GetMechanics", new SqlParameter("@Type", patentType)); }

        public DataTable IsMechanicBusy(Guid mechanicId, DateTime scheduledDate, int slotMinutes)
        {
            return Read("Inspections_IsMechanicBusy",
                new SqlParameter("@Mechanic_Id", mechanicId),
                new SqlParameter("@Scheduled_Date", scheduledDate),
                new SqlParameter("@Slot_Minutes", slotMinutes));
        }

        public DataTable GetNextFreeSlot(Guid mechanicId, DateTime from, int slotMinutes, int dayStartHour, int dayEndHour, int maxDays)
        {
            return Read("Inspections_GetNextFreeSlot",
                new SqlParameter("@Mechanic_Id", mechanicId),
                new SqlParameter("@From", from),
                new SqlParameter("@Slot_Minutes", slotMinutes),
                new SqlParameter("@Day_Start_Hour", dayStartHour),
                new SqlParameter("@Day_End_Hour", dayEndHour),
                new SqlParameter("@Max_Days", maxDays));
        }

        public DataTable AddProspectVehicle(Guid id, string licensePlate, string chassisNumber, string engineNumber, Guid makeId, Guid modelId,
            Guid versionId, Guid colourId, int year, int doors, int declaredKilometers, DvhCalculator dvh)
        {
            return Read("ProspectVehicles_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@License_Plate", licensePlate),
                new SqlParameter("@Chassis_Number", chassisNumber),
                new SqlParameter("@Engine_Number", engineNumber),
                new SqlParameter("@Make_Id", makeId),
                new SqlParameter("@Model_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Colour_Id", colourId),
                new SqlParameter("@Year", year),
                new SqlParameter("@Doors", doors),
                new SqlParameter("@Declared_Kilometers", declaredKilometers));
        }

        // Todas las columnas viajan aunque esten vacias: el DVH se calcula con la fila completa.
        public DataTable Add(Guid id, int number, Guid clientId, Guid prospectVehicleId, Guid salesmanId, int status, int? rejectionReason,
            DateTime creationDate, Guid? acceptedAppraisalId, double? agreedPrice, string ownerCuilCuit, DateTime? contractSignedDate,
            string prechargeCode08, DateTime? transferAppointmentDate, Guid? vehicleId, DateTime? closingDate, DvhCalculator dvh)
        {
            return Read("Acquisitions_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Number", number),
                new SqlParameter("@Client_Id", clientId),
                new SqlParameter("@ProspectVehicle_Id", prospectVehicleId),
                new SqlParameter("@Salesman_Id", salesmanId),
                new SqlParameter("@Status", status),
                new SqlParameter("@Rejection_Reason", (object)rejectionReason ?? DBNull.Value),
                new SqlParameter("@Creation_Date", creationDate),
                new SqlParameter("@Accepted_Appraisal_Id", (object)acceptedAppraisalId ?? DBNull.Value),
                new SqlParameter("@Agreed_Price", (object)agreedPrice ?? DBNull.Value),
                new SqlParameter("@Owner_CUIL_CUIT", (object)ownerCuilCuit ?? DBNull.Value),
                new SqlParameter("@Contract_Signed_Date", (object)contractSignedDate ?? DBNull.Value),
                new SqlParameter("@Precharge_Code_08", (object)prechargeCode08 ?? DBNull.Value),
                new SqlParameter("@Transfer_Appointment_Date", (object)transferAppointmentDate ?? DBNull.Value),
                new SqlParameter("@Vehicle_Id", (object)vehicleId ?? DBNull.Value),
                new SqlParameter("@Closing_Date", (object)closingDate ?? DBNull.Value));
        }

        public DataTable AddInspection(Guid id, Guid acquisitionId, Guid mechanicId, DateTime scheduledDate, DateTime? performedDate,
            int? verifiedKilometers, int? result, string observations, DvhCalculator dvh)
        {
            return Read("Inspections_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Acquisition_Id", acquisitionId),
                new SqlParameter("@Mechanic_Id", mechanicId),
                new SqlParameter("@Scheduled_Date", scheduledDate),
                new SqlParameter("@Performed_Date", (object)performedDate ?? DBNull.Value),
                new SqlParameter("@Verified_Kilometers", (object)verifiedKilometers ?? DBNull.Value),
                new SqlParameter("@Result", (object)result ?? DBNull.Value),
                new SqlParameter("@Observations", (object)observations ?? DBNull.Value));
        }
    }
}
