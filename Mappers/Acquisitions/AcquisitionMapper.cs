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

        // Sin adquisiciones para tasar devuelve null.
        public List<PendingAppraisal> GetPendingAppraisals(string filter, DateTime now)
        {
            List<PendingAppraisal> appraisals = MappingHandler.MapTableToEntities<PendingAppraisal>(data.GetPendingAppraisals(filter, now));
            return appraisals.Count == 0 ? null : appraisals;
        }

        // Como la adquisicion, la inspeccion registrada tiene NULL y enums: se arma a mano.
        public Inspection GetInspection(Guid id)
        {
            using (DataTable table = data.GetInspection(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                Inspection inspection = new Inspection();
                inspection.Id = (Guid)row["Id"];
                inspection.Acquisition_Id = (Guid)row["Acquisition_Id"];
                inspection.Mechanic_Id = (Guid)row["Mechanic_Id"];
                inspection.Scheduled_Date = (DateTime)row["Scheduled_Date"];
                inspection.Performed_Date = row["Performed_Date"] as DateTime?;
                inspection.Verified_Kilometers = row["Verified_Kilometers"] as int?;
                inspection.Result = (InspectionResult?)(row["Result"] as int?);
                inspection.Observations = row["Observations"] as string;
                return inspection;
            }
        }

        public List<InspectionScoreDetail> GetInspectionScores(Guid Inspection_Id)
        {
            List<InspectionScoreDetail> scores = new List<InspectionScoreDetail>();
            using (DataTable table = data.GetInspectionScores(Inspection_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    InspectionScoreDetail score = new InspectionScoreDetail();
                    score.Inspection_Id = (Guid)row["Inspection_Id"];
                    score.InspectionItem_Id = (Guid)row["InspectionItem_Id"];
                    score.Description = (string)row["Description"];
                    score.Display_Order = (int)row["Display_Order"];
                    score.Score = (InspectionRating)(int)row["Score"];
                    score.Observations = row["Observations"] as string;
                    scores.Add(score);
                }
            }
            return scores;
        }

        // Sin tasaciones previas devuelve null.
        public Appraisal GetLastAppraisal(Guid Acquisition_Id)
        {
            return MappingHandler.MapTableToEntities<Appraisal>(data.GetLastAppraisal(Acquisition_Id)).FirstOrDefault();
        }

        public SQLUpdateResult AddAppraisal(Appraisal appraisal)
        {
            return DigitVerifierWriteMapper.Save("Appraisals", dvh => data.AddAppraisal(appraisal.Id, appraisal.Acquisition_Id,
                appraisal.Inspection_Id, appraisal.User_Id, appraisal.Amount, appraisal.Justification, appraisal.Creation_Date,
                appraisal.Expiration_Date, dvh));
        }

        public SQLUpdateResult UpdateAcquisition(Acquisition acquisition)
        {
            return DigitVerifierWriteMapper.Save("Acquisitions", dvh => data.Update(acquisition.Id, acquisition.Number, acquisition.Client_Id,
                acquisition.ProspectVehicle_Id, acquisition.Salesman_Id, (int)acquisition.Status, (int?)acquisition.Rejection_Reason,
                acquisition.Creation_Date, acquisition.Accepted_Appraisal_Id, acquisition.Agreed_Price, acquisition.Owner_CUIL_CUIT,
                acquisition.Contract_Signed_Date, acquisition.Precharge_Code_08, acquisition.Transfer_Appointment_Date,
                acquisition.Vehicle_Id, acquisition.Closing_Date, dvh));
        }

        // Sin compras en gestion devuelve null.
        public List<PendingAcquisition> GetPendingManagement(string filter)
        {
            List<PendingAcquisition> acquisitions = new List<PendingAcquisition>();
            using (DataTable table = data.GetPendingManagement(filter))
            {
                foreach (DataRow row in table.Rows)
                {
                    PendingAcquisition acquisition = new PendingAcquisition();
                    acquisition.Id = (Guid)row["Id"];
                    acquisition.Number = (int)row["Number"];
                    acquisition.Inspection_Id = (Guid)row["Inspection_Id"];
                    acquisition.Client_Id = (Guid)row["Client_Id"];
                    acquisition.Dni = (int)row["Dni"];
                    acquisition.License_Plate = (string)row["License_Plate"];
                    acquisition.Vehicle_Description = (string)row["Vehicle_Description"];
                    acquisition.Status = (AcquisitionStatus)(int)row["Status"];
                    acquisitions.Add(acquisition);
                }
            }
            return acquisitions.Count == 0 ? null : acquisitions;
        }

        // Sin forma de pago pactada devuelve null.
        public List<AcquisitionPaymentTerm> GetPaymentTerms(Guid Acquisition_Id)
        {
            List<AcquisitionPaymentTerm> terms = MappingHandler.MapTableToEntities<AcquisitionPaymentTerm>(data.GetPaymentTerms(Acquisition_Id));
            return terms.Count == 0 ? null : terms;
        }

        public SQLUpdateResult AddPaymentTerm(AcquisitionPaymentTerm term)
        {
            return DigitVerifierWriteMapper.Save("AcquisitionPaymentTerms", dvh => data.AddPaymentTerm(term.Acquisition_Id, term.PaymentType_Id,
                term.Amount, dvh));
        }

        public SQLUpdateResult DeletePaymentTerms(Guid Acquisition_Id)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeletePaymentTerms(Acquisition_Id), "AcquisitionPaymentTerms");
        }

        // Validity_Days puede ser NULL: los tipos se arman a mano.
        public List<AcquisitionDocumentType> GetDocumentTypes(AcquisitionDocumentStage stage)
        {
            List<AcquisitionDocumentType> types = new List<AcquisitionDocumentType>();
            using (DataTable table = data.GetDocumentTypes((int)stage))
            {
                foreach (DataRow row in table.Rows)
                {
                    AcquisitionDocumentType type = new AcquisitionDocumentType();
                    type.Id = (Guid)row["Id"];
                    type.Description = (string)row["Description"];
                    type.Stage = (AcquisitionDocumentStage)(int)row["Stage"];
                    type.Validity_Days = row["Validity_Days"] as int?;
                    type.Display_Order = (int)row["Display_Order"];
                    types.Add(type);
                }
            }
            return types;
        }

        public List<AcquisitionDocument> GetDocuments(Guid Acquisition_Id)
        {
            List<AcquisitionDocument> documents = new List<AcquisitionDocument>();
            using (DataTable table = data.GetDocuments(Acquisition_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    AcquisitionDocument document = new AcquisitionDocument();
                    document.Id = (Guid)row["Id"];
                    document.Acquisition_Id = (Guid)row["Acquisition_Id"];
                    document.DocumentType_Id = (Guid)row["DocumentType_Id"];
                    document.Status = (AcquisitionDocumentStatus)(int)row["Status"];
                    document.Issue_Date = (DateTime)row["Issue_Date"];
                    document.Observation = row["Observation"] as string;
                    document.User_Id = (Guid)row["User_Id"];
                    document.Uploaded_Date = (DateTime)row["Uploaded_Date"];
                    document.Description = (string)row["Description"];
                    document.Stage = (AcquisitionDocumentStage)(int)row["Stage"];
                    document.FileName = row["FileName"] as string;
                    documents.Add(document);
                }
            }
            return documents;
        }

        public SQLUpdateResult AddDocument(AcquisitionDocument document)
        {
            return DigitVerifierWriteMapper.Save("AcquisitionDocuments", dvh => data.AddDocument(document.Id, document.Acquisition_Id,
                document.DocumentType_Id, (int)document.Status, document.Issue_Date, document.Observation, document.User_Id,
                document.Uploaded_Date, dvh));
        }

        public SQLUpdateResult ObserveDocument(AcquisitionDocument document)
        {
            return DigitVerifierWriteMapper.Save("AcquisitionDocuments", dvh => data.ObserveDocument(document.Id, document.Acquisition_Id,
                document.DocumentType_Id, (int)document.Status, document.Issue_Date, document.Observation, document.User_Id,
                document.Uploaded_Date, dvh));
        }

        // El archivo se borra en cascada y no esta protegido: solo cambia el DVV de AcquisitionDocuments.
        public SQLUpdateResult DeleteDocument(AcquisitionDocument document)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteDocument(document.Id), "AcquisitionDocuments");
        }

        // AcquisitionDocumentFiles no esta protegida con digitos verificadores.
        public SQLUpdateResult AddDocumentFile(AcquisitionDocumentFile file)
        {
            return MappingHandler.MapUpdateResult(data.AddDocumentFile(file.Document_Id, file.FileName, file.FileContent));
        }

        public AcquisitionDocumentFile GetDocumentFile(Guid Document_Id)
        {
            return MappingHandler.MapTableToEntities<AcquisitionDocumentFile>(data.GetDocumentFile(Document_Id)).FirstOrDefault();
        }

        // Como la adquisicion, el resumen tiene NULL segun la etapa: se arma a mano.
        public AcquisitionSummary GetSummary(Guid id)
        {
            using (DataTable table = data.GetSummary(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                AcquisitionSummary summary = new AcquisitionSummary();
                summary.Id = (Guid)row["Id"];
                summary.Number = (int)row["Number"];
                summary.Client_Id = (Guid)row["Client_Id"];
                summary.Dni = (int)row["Dni"];
                summary.License_Plate = (string)row["License_Plate"];
                summary.Chassis_Number = (string)row["Chassis_Number"];
                summary.Engine_Number = (string)row["Engine_Number"];
                summary.Vehicle_Description = (string)row["Vehicle_Description"];
                summary.Make_Id = (Guid)row["Make_Id"];
                summary.Model_Id = (Guid)row["Model_Id"];
                summary.Version_Id = (Guid)row["Version_Id"];
                summary.Colour_Id = (Guid)row["Colour_Id"];
                summary.Year = (int)row["Year"];
                summary.Doors = (int)row["Doors"];
                summary.Declared_Kilometers = (int)row["Declared_Kilometers"];
                summary.Inspection_Id = (Guid)row["Inspection_Id"];
                summary.Performed_Date = row["Performed_Date"] as DateTime?;
                summary.Verified_Kilometers = row["Verified_Kilometers"] as int?;
                summary.Appraisal_Amount = row["Appraisal_Amount"] as double?;
                summary.Appraisal_Date = row["Appraisal_Date"] as DateTime?;
                summary.Agreed_Price = row["Agreed_Price"] as double?;
                summary.Owner_CUIL_CUIT = row["Owner_CUIL_CUIT"] as string;
                summary.Contract_Signed_Date = row["Contract_Signed_Date"] as DateTime?;
                summary.Paid_Amount = (double)row["Paid_Amount"];
                summary.Precharge_Code_08 = row["Precharge_Code_08"] as string;
                summary.Transfer_Appointment_Date = row["Transfer_Appointment_Date"] as DateTime?;
                summary.Closing_Date = row["Closing_Date"] as DateTime?;
                summary.Receipt_Number = row["Receipt_Number"] as int?;
                summary.Receipt_Issue_Date = row["Receipt_Issue_Date"] as DateTime?;
                return summary;
            }
        }

        // Vehiculo y adquisicion se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult FinishAcquisition(Vehicle vehicle, Acquisition acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.Finish(VehicleRow(vehicle), AcquisitionRow(acquisition),
                digitVerifier.Dvh("Vehicles"), digitVerifier.Dvh("Acquisitions")), "Vehicles", "Acquisitions");
        }

        // Sin compras para pagar ni comprobantes emitidos devuelve null.
        public List<PayableAcquisition> GetPendingPayment(Guid Client_Id)
        {
            List<PayableAcquisition> acquisitions = new List<PayableAcquisition>();
            using (DataTable table = data.GetPendingPayment(Client_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    PayableAcquisition acquisition = new PayableAcquisition();
                    acquisition.Id = (Guid)row["Id"];
                    acquisition.Number = (int)row["Number"];
                    acquisition.License_Plate = (string)row["License_Plate"];
                    acquisition.Vehicle_Description = (string)row["Vehicle_Description"];
                    acquisition.Agreed_Price = (double)row["Agreed_Price"];
                    acquisition.Paid_Amount = (double)row["Paid_Amount"];
                    acquisition.Receipt_Number = row["Receipt_Number"] as int?;
                    acquisition.Status = (AcquisitionStatus)(int)row["Status"];
                    acquisitions.Add(acquisition);
                }
            }
            return acquisitions.Count == 0 ? null : acquisitions;
        }

        // Los datos de cada medio son NULL en los otros: los pagos se arman a mano.
        public List<AcquisitionPayment> GetPayments(Guid Acquisition_Id)
        {
            List<AcquisitionPayment> payments = new List<AcquisitionPayment>();
            using (DataTable table = data.GetPayments(Acquisition_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    AcquisitionPayment payment = new AcquisitionPayment();
                    payment.Id = (Guid)row["Id"];
                    payment.Acquisition_Id = (Guid)row["Acquisition_Id"];
                    payment.PaymentType_Id = (Guid)row["PaymentType_Id"];
                    payment.PaymentType_Description = (string)row["PaymentType_Description"];
                    payment.Cashier_Id = (Guid)row["Cashier_Id"];
                    payment.Cashier = (string)row["Cashier"];
                    payment.Amount = (double)row["Amount"];
                    payment.Bank = row["Bank"] as string;
                    payment.Cbu_Alias = row["Cbu_Alias"] as string;
                    payment.Operation_Number = row["Operation_Number"] as string;
                    payment.Check_Number = row["Check_Number"] as string;
                    payment.Payment_Date = row["Payment_Date"] as DateTime?;
                    payment.Creation_Date = (DateTime)row["Creation_Date"];
                    payments.Add(payment);
                }
            }
            return payments;
        }

        public SQLUpdateResult AddPayment(AcquisitionPayment payment)
        {
            return DigitVerifierWriteMapper.Save("AcquisitionPayments", dvh => data.AddPayment(payment.Id, payment.Acquisition_Id,
                payment.PaymentType_Id, payment.Cashier_Id, payment.Amount, payment.Bank, payment.Cbu_Alias, payment.Operation_Number,
                payment.Check_Number, payment.Payment_Date, payment.Creation_Date, dvh));
        }

        public int GetNextReceiptNumber()
        {
            using (DataTable table = data.GetNextReceiptNumber())
                return (int)table.Rows[0]["Number"];
        }

        // Comprobante y adquisicion se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult PayAcquisition(PurchaseReceipt receipt, Acquisition acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.Pay(ReceiptRow(receipt), AcquisitionRow(acquisition),
                digitVerifier.Dvh("PurchaseReceipts"), digitVerifier.Dvh("Acquisitions")), "PurchaseReceipts", "Acquisitions");
        }

        // Sin comprobante emitido devuelve null.
        public PurchaseReceipt GetReceipt(Guid Acquisition_Id)
        {
            return MappingHandler.MapTableToEntities<PurchaseReceipt>(data.GetReceipt(Acquisition_Id)).FirstOrDefault();
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

        private static IDictionary<string, object> ReceiptRow(PurchaseReceipt receipt)
        {
            return new Dictionary<string, object>
            {
                { "Id", receipt.Id },
                { "Number", receipt.Number },
                { "Acquisition_Id", receipt.Acquisition_Id },
                { "Issue_Date", receipt.Issue_Date },
                { "Client_Name", receipt.Client_Name },
                { "Client_CUIL_CUIT", receipt.Client_CUIL_CUIT },
                { "Detail", receipt.Detail },
                { "Net_Amount", receipt.Net_Amount },
                { "Tax_Amount", receipt.Tax_Amount },
                { "Total_Amount", receipt.Total_Amount }
            };
        }

        private static IDictionary<string, object> VehicleRow(Vehicle vehicle)
        {
            return new Dictionary<string, object>
            {
                { "Id", vehicle.Id },
                { "License_Plate", vehicle.License_Plate },
                { "Make_Id", vehicle.Make_Id },
                { "Model_Id", vehicle.Model_Id },
                { "Version_Id", vehicle.Version_Id },
                { "Colour_Id", vehicle.Colour_Id },
                { "Price", vehicle.Price },
                { "Opcionals", vehicle.Opcionals },
                { "Observations", vehicle.Observations },
                { "Doors", vehicle.Doors },
                { "Year", vehicle.Year },
                { "Kilometers", vehicle.Kilometers },
                { "ImageLink", vehicle.ImageLink }
            };
        }
    }
}
