using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Acquisitions
{
    // Compra de vehiculos a clientes (PN2): vehiculo ofrecido, adquisicion, inspeccion mecanica, tasacion,
    // forma de pago pactada, documentos y cierre. AcquisitionDocumentFiles no esta protegida con digitos verificadores.
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

        public DataTable GetById(Guid id) { return Read("Acquisitions_GetById", new SqlParameter("@Id", id)); }
        public DataTable GetScheduledInspections(Guid mechanicId) { return Read("Inspections_GetScheduledByMechanic", new SqlParameter("@Mechanic_Id", mechanicId)); }
        public DataTable GetInspectionDetail(Guid id) { return Read("Inspections_GetDetail", new SqlParameter("@Id", id)); }
        public DataTable GetInspectionItems() { return Read("InspectionItems_GetAll"); }

        public DataTable GetInspectionPhotos(Guid inspectionId)
        {
            return Read("InspectionPhotos_GetByInspection", new SqlParameter("@Inspection_Id", inspectionId));
        }

        public DataTable AddInspectionPhoto(Guid id, Guid inspectionId, string fileName, byte[] fileContent, DateTime uploadedDate)
        {
            return Read("InspectionPhotos_Add",
                new SqlParameter("@Id", id),
                new SqlParameter("@Inspection_Id", inspectionId),
                new SqlParameter("@FileName", fileName),
                new SqlParameter("@FileContent", fileContent),
                new SqlParameter("@UploadedDate", uploadedDate));
        }

        public DataTable DeleteInspectionPhoto(Guid id) { return Read("InspectionPhotos_Delete", new SqlParameter("@Id", id)); }

        // Inspeccion, calificaciones y adquisicion van en un solo SP, dentro de una transaccion. Cada fila llega
        // completa (columna -> valor) porque el DVH se calcula con todas; al SP solo viaja lo que cambia.
        public DataTable RecordInspection(IDictionary<string, object> inspection, IList<IDictionary<string, object>> scores,
            IDictionary<string, object> acquisition, DvhCalculator inspectionDvh, DvhCalculator scoreDvh, DvhCalculator acquisitionDvh)
        {
            DvhResult inspectionRow = inspectionDvh(inspection);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            using (var scoreRows = new DataTable())
            {
                scoreRows.Columns.Add("InspectionItem_Id", typeof(Guid));
                scoreRows.Columns.Add("Score", typeof(int));
                scoreRows.Columns.Add("Observations", typeof(string));
                scoreRows.Columns.Add("DVH", typeof(string));
                foreach (IDictionary<string, object> score in scores)
                {
                    DvhResult scoreRow = scoreDvh(score);
                    scoreRows.Rows.Add(Value(scoreRow, "InspectionItem_Id"), Value(scoreRow, "Score"), Value(scoreRow, "Observations"), scoreRow.Dvh);
                }

                return Read("Inspections_Record",
                    new SqlParameter("@Id", Value(inspectionRow, "Id")),
                    new SqlParameter("@Acquisition_Id", Value(inspectionRow, "Acquisition_Id")),
                    new SqlParameter("@Performed_Date", SqlDbType.DateTime) { Value = Value(inspectionRow, "Performed_Date") },
                    new SqlParameter("@Verified_Kilometers", SqlDbType.Int) { Value = Value(inspectionRow, "Verified_Kilometers") },
                    new SqlParameter("@Result", SqlDbType.Int) { Value = Value(inspectionRow, "Result") },
                    new SqlParameter("@Observations", SqlDbType.VarChar, -1) { Value = Value(inspectionRow, "Observations") },
                    new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = inspectionRow.Dvh },
                    new SqlParameter("@Scores", SqlDbType.Structured) { TypeName = "dbo.InspectionScoreRows", Value = scoreRows },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = Value(acquisitionRow, "Status") },
                    new SqlParameter("@Rejection_Reason", SqlDbType.Int) { Value = Value(acquisitionRow, "Rejection_Reason") },
                    new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
            }
        }

        public DataTable GetPendingAppraisals(string filter, DateTime now)
        {
            return Read("Acquisitions_GetPendingAppraisal",
                new SqlParameter("@Filter", (object)filter ?? DBNull.Value),
                new SqlParameter("@Now", now));
        }

        public DataTable GetInspection(Guid id) { return Read("Inspections_GetById", new SqlParameter("@Id", id)); }

        public DataTable GetInspectionScores(Guid inspectionId)
        {
            return Read("InspectionScores_GetByInspection", new SqlParameter("@Inspection_Id", inspectionId));
        }

        public DataTable GetLastAppraisal(Guid acquisitionId)
        {
            return Read("Appraisals_GetLastByAcquisition", new SqlParameter("@Acquisition_Id", acquisitionId));
        }

        public DataTable AddAppraisal(Guid id, Guid acquisitionId, Guid inspectionId, Guid userId, double amount, string justification,
            DateTime creationDate, DateTime expirationDate, DvhCalculator dvh)
        {
            return Read("Appraisals_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Acquisition_Id", acquisitionId),
                new SqlParameter("@Inspection_Id", inspectionId),
                new SqlParameter("@User_Id", userId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Justification", justification),
                new SqlParameter("@Creation_Date", creationDate),
                new SqlParameter("@Expiration_Date", expirationDate));
        }

        // Igual que el alta: todas las columnas viajan para que el DVH se calcule con la fila completa.
        public DataTable Update(Guid id, int number, Guid clientId, Guid prospectVehicleId, Guid salesmanId, int status, int? rejectionReason,
            DateTime creationDate, Guid? acceptedAppraisalId, double? agreedPrice, string ownerCuilCuit, DateTime? contractSignedDate,
            string prechargeCode08, DateTime? transferAppointmentDate, Guid? vehicleId, DateTime? closingDate, DvhCalculator dvh)
        {
            return Read("Acquisitions_Update", dvh,
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

        public DataTable GetPendingManagement(string filter)
        {
            return Read("Acquisitions_GetPendingManagement", new SqlParameter("@Filter", (object)filter ?? DBNull.Value));
        }

        public DataTable GetPaymentTerms(Guid acquisitionId)
        {
            return Read("AcquisitionPaymentTerms_GetByAcquisition", new SqlParameter("@Acquisition_Id", acquisitionId));
        }

        public DataTable AddPaymentTerm(Guid acquisitionId, Guid paymentTypeId, double amount, DvhCalculator dvh)
        {
            return Read("AcquisitionPaymentTerms_Add", dvh,
                new SqlParameter("@Acquisition_Id", acquisitionId),
                new SqlParameter("@PaymentType_Id", paymentTypeId),
                new SqlParameter("@Amount", amount));
        }

        public DataTable DeletePaymentTerms(Guid acquisitionId)
        {
            return Read("AcquisitionPaymentTerms_DeleteByAcquisition", new SqlParameter("@Acquisition_Id", acquisitionId));
        }

        public DataTable GetDocumentTypes(int stage) { return Read("AcquisitionDocumentTypes_GetByStage", new SqlParameter("@Stage", stage)); }

        public DataTable GetDocuments(Guid acquisitionId)
        {
            return Read("AcquisitionDocuments_GetByAcquisition", new SqlParameter("@Acquisition_Id", acquisitionId));
        }

        public DataTable AddDocument(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate, string observation,
            Guid userId, DateTime uploadedDate, DvhCalculator dvh)
        {
            return Read("AcquisitionDocuments_Add", dvh, DocumentParameters(id, acquisitionId, documentTypeId, status, issueDate, observation,
                userId, uploadedDate));
        }

        public DataTable ObserveDocument(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate, string observation,
            Guid userId, DateTime uploadedDate, DvhCalculator dvh)
        {
            return Read("AcquisitionDocuments_Observe", dvh, DocumentParameters(id, acquisitionId, documentTypeId, status, issueDate, observation,
                userId, uploadedDate));
        }

        public DataTable DeleteDocument(Guid id) { return Read("AcquisitionDocuments_Delete", new SqlParameter("@Id", id)); }

        public DataTable GetDocumentFile(Guid documentId)
        {
            return Read("AcquisitionDocumentFiles_GetByDocument", new SqlParameter("@Document_Id", documentId));
        }

        public DataTable AddDocumentFile(Guid documentId, string fileName, byte[] fileContent)
        {
            return Read("AcquisitionDocumentFiles_Add",
                new SqlParameter("@Document_Id", documentId),
                new SqlParameter("@FileName", fileName),
                new SqlParameter("@FileContent", fileContent));
        }

        public DataTable GetSummary(Guid id) { return Read("Acquisitions_GetSummary", new SqlParameter("@Id", id)); }

        // Alta en inventario y cierre de la compra en un solo SP, dentro de una transaccion. Igual que en la
        // inspeccion, las filas llegan completas para el DVH y al SP solo viaja lo que cambia de la adquisicion.
        public DataTable Finish(IDictionary<string, object> vehicle, IDictionary<string, object> acquisition, DvhCalculator vehicleDvh,
            DvhCalculator acquisitionDvh)
        {
            DvhResult vehicleRow = vehicleDvh(vehicle);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            return Read("Acquisitions_Finish",
                new SqlParameter("@Vehicle_Id", Value(vehicleRow, "Id")),
                new SqlParameter("@License_Plate", SqlDbType.VarChar, 255) { Value = Value(vehicleRow, "License_Plate") },
                new SqlParameter("@Make_Id", Value(vehicleRow, "Make_Id")),
                new SqlParameter("@Model_Id", Value(vehicleRow, "Model_Id")),
                new SqlParameter("@Version_Id", Value(vehicleRow, "Version_Id")),
                new SqlParameter("@Colour_Id", Value(vehicleRow, "Colour_Id")),
                new SqlParameter("@Price", SqlDbType.Float) { Value = Value(vehicleRow, "Price") },
                new SqlParameter("@Opcionals", SqlDbType.VarChar, 255) { Value = Value(vehicleRow, "Opcionals") },
                new SqlParameter("@Observations", SqlDbType.VarChar, -1) { Value = Value(vehicleRow, "Observations") },
                new SqlParameter("@Doors", SqlDbType.Int) { Value = Value(vehicleRow, "Doors") },
                new SqlParameter("@Year", SqlDbType.Int) { Value = Value(vehicleRow, "Year") },
                new SqlParameter("@Kilometers", SqlDbType.Int) { Value = Value(vehicleRow, "Kilometers") },
                new SqlParameter("@ImageLink", SqlDbType.NVarChar, -1) { Value = Value(vehicleRow, "ImageLink") },
                new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = vehicleRow.Dvh },
                new SqlParameter("@Acquisition_Id", Value(acquisitionRow, "Id")),
                new SqlParameter("@Status", SqlDbType.Int) { Value = Value(acquisitionRow, "Status") },
                new SqlParameter("@Closing_Date", SqlDbType.DateTime) { Value = Value(acquisitionRow, "Closing_Date") },
                new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
        }

        private static SqlParameter[] DocumentParameters(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate,
            string observation, Guid userId, DateTime uploadedDate)
        {
            return new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Acquisition_Id", acquisitionId),
                new SqlParameter("@DocumentType_Id", documentTypeId),
                new SqlParameter("@Status", status),
                new SqlParameter("@Issue_Date", issueDate),
                new SqlParameter("@Observation", (object)observation ?? DBNull.Value),
                new SqlParameter("@User_Id", userId),
                new SqlParameter("@Uploaded_Date", uploadedDate)
            };
        }

        // Valor normalizado con el que se calculo el DVH: es el que tiene que quedar guardado.
        private static object Value(DvhResult row, string column)
        {
            return row.Columns.Single(c => string.Equals(c.Name, column, StringComparison.OrdinalIgnoreCase)).Value ?? DBNull.Value;
        }
    }
}
