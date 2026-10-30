using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;
using BLL;

namespace CarAgency.BLL
{
    public class AcquisitionBLL
    {
        // Turnos de inspeccion: de lunes a viernes, en punto, dentro del horario del taller.
        public const int InspectionSlotMinutes = 60;
        public const int WorkdayStartHour = 9;
        public const int WorkdayEndHour = 18;
        private const int NextFreeSlotSearchDays = 30;

        // Fotografias de la inspeccion (CUF08 paso 5).
        public const int MaxPhotoMegabytes = 5;
        private static readonly string[] PhotoExtensions = { ".jpg", ".jpeg", ".png" };

        // Vigencia de la tasacion en dias corridos (CUF09 paso 6). Vencida, se puede volver a tasar.
        public const int AppraisalValidityDays = 7;

        // Medios con los que la agencia le paga al cliente (CUF10 paso 5). Se filtran por Description,
        // como las tarjetas en AddPaymentForm.
        public const string BankTransferPaymentType = "Bank Transfer";
        public const string CheckPaymentType = "Check";
        public const string CashPaymentType = "Cash";
        public static readonly string[] AcquisitionPaymentTypes = { BankTransferPaymentType, CheckPaymentType, CashPaymentType };

        // Emisor del comprobante de compra (CUF11 paso 7). PurchaseReceipts no lo guarda porque es siempre la agencia.
        public const string AgencyName = "CarAgency";
        public const string AgencyCuit = "30-71654328-1";

        // CBU de 22 digitos o alias de 6 a 20 caracteres (letras, numeros, punto o guion).
        private static readonly Regex CbuFormat = new Regex(@"^\d{22}$");
        private static readonly Regex AliasFormat = new Regex(@"^[A-Za-z0-9.\-]{6,20}$");

        // Documentos escaneados (CUF10 paso 12): solo pdf, como los archivos de los tramites.
        private static readonly string[] DocumentExtensions = { ".pdf" };

        private AcquisitionMapper _acquisitionmapper;
        private PaymentMapper _paymentmapper;
        public AcquisitionBLL()
        {
            _acquisitionmapper = new AcquisitionMapper();
            _paymentmapper = new PaymentMapper();
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

        // La fecha programada es orientativa: el mecanico ve todas sus inspecciones pendientes.
        public List<ScheduledInspection> GetScheduledInspections()
        {
            return _acquisitionmapper.GetScheduledInspections(SessionHandler.Instance.GetId());
        }
        public InspectionDetail GetInspectionDetail(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspectionDetail(Inspection_Id);
        }
        public List<InspectionItem> GetInspectionItems()
        {
            return _acquisitionmapper.GetInspectionItems();
        }
        public List<InspectionPhoto> GetInspectionPhotos(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspectionPhotos(Inspection_Id);
        }
        // Las fotos se guardan a medida que se adjuntan: quedan con la inspeccion aunque no se confirme.
        public SQLUpdateResult AddInspectionPhoto(InspectionPhoto photo)
        {
            ValidatePhoto(photo);
            photo.Id = Guid.NewGuid();
            photo.UploadedDate = DateTime.Now;
            return _acquisitionmapper.AddInspectionPhoto(photo);
        }
        public SQLUpdateResult DeleteInspectionPhoto(InspectionPhoto photo)
        {
            return _acquisitionmapper.DeleteInspectionPhoto(photo);
        }

        // CUF08: Apto pasa la adquisicion a Inspeccionado y No apto a Rechazada (6.2).
        public SQLUpdateResult RecordInspection(Inspection inspection)
        {
            if (inspection.Result != InspectionResult.Fit && inspection.Result != InspectionResult.Unfit)
                throw new TranslatableException("PleaseSelectInspectionResult", "Seleccione el resultado de la inspeccion para continuar.");
            if (inspection.Verified_Kilometers == null || inspection.Verified_Kilometers < 0)
                throw new TranslatableException("PleaseWriteValidKilometers", "Escriba un kilometraje valido para continuar.");
            if (GetInspectionItems().Any(item => inspection.Scores == null || !inspection.Scores.Any(s => s.InspectionItem_Id == item.Id)))
                throw new TranslatableException("InspectionItemsNotScored", "Califique todos los items de la inspeccion para continuar.");
            if (GetInspectionPhotos(inspection.Id) == null)
                throw new TranslatableException("InspectionPhotoRequired", "Adjunte al menos una fotografia del vehiculo para continuar.");

            Acquisition acquisition = GetScheduledAcquisition(inspection);
            inspection.Performed_Date = DateTime.Now;
            foreach (InspectionScore score in inspection.Scores)
                score.Inspection_Id = inspection.Id;
            if (inspection.Result == InspectionResult.Fit)
                acquisition.Status = AcquisitionStatus.Inspected;
            else
            {
                acquisition.Status = AcquisitionStatus.Rejected;
                acquisition.Rejection_Reason = RejectionReason.InspectionUnfit;
            }

            SQLUpdateResult result = RecordResult(_acquisitionmapper.RecordInspection(inspection, acquisition), AuditEventType.InspectionRecorded, acquisition.Id);
            if (acquisition.Status == AcquisitionStatus.Rejected)
                result = RecordResult(result, AuditEventType.AcquisitionRejected, acquisition.Id);
            return result;
        }

        // CUF08 2.1: el vehiculo no se presento. La inspeccion queda como ausencia, sin calificaciones,
        // y la adquisicion se cancela: el vendedor tiene que iniciar otra con CUF07.
        public SQLUpdateResult RegisterNoShow(Inspection inspection)
        {
            Acquisition acquisition = GetScheduledAcquisition(inspection);
            inspection.Performed_Date = DateTime.Now;
            inspection.Verified_Kilometers = null;
            inspection.Result = InspectionResult.NoShow;
            inspection.Scores = new List<InspectionScore>();
            acquisition.Status = AcquisitionStatus.Cancelled;
            return RecordResult(_acquisitionmapper.RecordInspection(inspection, acquisition), AuditEventType.AcquisitionCancelled, acquisition.Id);
        }

        // Solo el mecanico asignado registra la inspeccion, y una sola vez.
        private Acquisition GetScheduledAcquisition(Inspection inspection)
        {
            Acquisition acquisition = _acquisitionmapper.GetById(inspection.Acquisition_Id);
            if (acquisition == null || acquisition.Status != AcquisitionStatus.InspectionScheduled || inspection.Mechanic_Id != SessionHandler.Instance.GetId())
                throw new TranslatableException("InspectionNotPending", "La inspeccion ya fue registrada o no esta asignada a este mecanico.");
            return acquisition;
        }

        // La matricula se guarda en mayusculas; el DNI se compara tal cual.
        public List<PendingAppraisal> GetPendingAppraisals(string filter)
        {
            return _acquisitionmapper.GetPendingAppraisals(filter == null ? null : filter.Trim().ToUpperInvariant(), DateTime.Now);
        }
        public Inspection GetInspection(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspection(Inspection_Id);
        }
        public List<InspectionScoreDetail> GetInspectionScores(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspectionScores(Inspection_Id);
        }
        public Appraisal GetLastAppraisal(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetLastAppraisal(Acquisition_Id);
        }

        // CUF09: la tasacion y el pase a Tasada se graban uno detras del otro, como el resto de las altas.
        public SQLUpdateResult AddAppraisal(Appraisal appraisal)
        {
            if (appraisal.Amount <= 0)
                throw new TranslatableException("PleaseWriteValidAppraisalAmount", "Escriba un valor de tasacion mayor a cero para continuar.");
            if (string.IsNullOrWhiteSpace(appraisal.Justification))
                throw new TranslatableException("PleaseWriteAppraisalJustification", "Escriba la justificacion de la tasacion para continuar.");

            Acquisition acquisition = GetAppraisableAcquisition(appraisal.Acquisition_Id);
            appraisal.Id = Guid.NewGuid();
            appraisal.User_Id = SessionHandler.Instance.GetId();
            appraisal.Justification = appraisal.Justification.Trim();
            appraisal.Creation_Date = DateTime.Now;
            appraisal.Expiration_Date = appraisal.Creation_Date.AddDays(AppraisalValidityDays);
            acquisition.Status = AcquisitionStatus.Appraised;

            SQLUpdateResult result = _acquisitionmapper.AddAppraisal(appraisal);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.UpdateAcquisition(acquisition);
            return RecordResult(result, AuditEventType.AppraisalCreated, acquisition.Id);
        }

        // Se tasa una adquisicion Inspeccionada, o una Tasada cuya ultima tasacion ya vencio (2.1).
        // Mientras la tasacion este vigente no se puede volver a tasar.
        private Acquisition GetAppraisableAcquisition(Guid Acquisition_Id)
        {
            Acquisition acquisition = _acquisitionmapper.GetById(Acquisition_Id);
            if (acquisition != null && acquisition.Status == AcquisitionStatus.Inspected)
                return acquisition;
            if (acquisition != null && acquisition.Status == AcquisitionStatus.Appraised)
            {
                Appraisal last = GetLastAppraisal(Acquisition_Id);
                if (last == null || last.Expiration_Date < DateTime.Now)
                    return acquisition;
            }
            throw new TranslatableException("AppraisalNotPending", "La adquisicion tiene una tasacion vigente o no esta en estado Inspeccionado.");
        }

        // CUF10: de Tasada a Transferida. La matricula se guarda en mayusculas; el DNI se compara tal cual.
        public List<PendingAcquisition> GetPendingAcquisitions(string filter)
        {
            return _acquisitionmapper.GetPendingManagement(filter == null ? null : filter.Trim().ToUpperInvariant());
        }
        public Acquisition GetAcquisition(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetById(Acquisition_Id);
        }
        // CUF10 3.1: vencida la tasacion no se puede seguir, hay que volver a tasar con CUF09.
        public bool IsAppraisalExpired(Appraisal appraisal)
        {
            return appraisal == null || appraisal.Expiration_Date < DateTime.Now;
        }
        // El acuerdo es valido solo si se hizo sobre la ultima tasacion: si se re-taso, hay que volver a pactarlo.
        public bool HasAgreement(Acquisition acquisition)
        {
            Appraisal last = GetLastAppraisal(acquisition.Id);
            return acquisition.Agreed_Price != null && last != null && acquisition.Accepted_Appraisal_Id == last.Id;
        }
        public List<PaymentType> GetAcquisitionPaymentTypes()
        {
            List<PaymentType> types = _paymentmapper.GetAllPaymentTypes();
            return types == null ? new List<PaymentType>() : types.Where(t => AcquisitionPaymentTypes.Contains(t.Description)).ToList();
        }
        public List<AcquisitionPaymentTerm> GetPaymentTerms(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetPaymentTerms(Acquisition_Id);
        }
        // CUF10 paso 6: la suma se compara redondeada a centavos. Positiva, falta asignar; negativa, sobra.
        public decimal GetPaymentTermsDifference(double price, List<AcquisitionPaymentTerm> terms)
        {
            double total = terms == null ? 0 : terms.Sum(t => t.Amount);
            return (decimal)Math.Round(price, 2) - (decimal)Math.Round(total, 2);
        }

        // CUF10 pasos 5 a 7: el acuerdo se registra al generar el contrato y la adquisicion sigue Tasada hasta la firma.
        // Mientras no se firme se puede volver a generar: la forma de pago se reemplaza completa.
        public SQLUpdateResult RegisterAgreement(Acquisition agreement, List<AcquisitionPaymentTerm> terms)
        {
            if (agreement.Agreed_Price == null || agreement.Agreed_Price <= 0)
                throw new TranslatableException("PleaseWriteValidAgreedPrice", "Escriba un precio acordado mayor a cero para continuar.");
            if (string.IsNullOrWhiteSpace(agreement.Owner_CUIL_CUIT))
                throw new TranslatableException("PleaseWriteOwnerCuilCuit", "Escriba el CUIL/CUIT del titular del vehiculo para continuar.");
            ValidatePaymentTerms(agreement.Agreed_Price.Value, terms);

            Acquisition acquisition = GetAppraisedAcquisition(agreement.Id);
            acquisition.Accepted_Appraisal_Id = GetLastAppraisal(acquisition.Id).Id;
            acquisition.Agreed_Price = agreement.Agreed_Price;
            acquisition.Owner_CUIL_CUIT = agreement.Owner_CUIL_CUIT.Trim();

            SQLUpdateResult result = _acquisitionmapper.UpdateAcquisition(acquisition);
            if (result != null && result.sqlResult == SQLResultType.success && GetPaymentTerms(acquisition.Id) != null)
                result = _acquisitionmapper.DeletePaymentTerms(acquisition.Id);
            foreach (AcquisitionPaymentTerm term in terms)
            {
                term.Acquisition_Id = acquisition.Id;
                if (result != null && result.sqlResult == SQLResultType.success)
                    result = _acquisitionmapper.AddPaymentTerm(term);
            }
            // Sin evento: la formalizacion se registra con la firma del contrato.
            return result;
        }

        // CUF10 pasos 8 y 9: con el contrato firmado el precio queda bloqueado.
        public SQLUpdateResult RegisterContractSigned(Guid Acquisition_Id)
        {
            Acquisition acquisition = GetAppraisedAcquisition(Acquisition_Id);
            if (!HasAgreement(acquisition))
                throw new TranslatableException("ContractNotGenerated", "Genere el contrato de compraventa antes de registrar la firma.");
            ValidatePaymentTerms(acquisition.Agreed_Price.Value, GetPaymentTerms(Acquisition_Id));

            acquisition.Contract_Signed_Date = DateTime.Now;
            acquisition.Status = AcquisitionStatus.PurchaseFormalized;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), AuditEventType.AcquisitionFormalized, acquisition.Id);
        }

        // CUF10 3.3: el cliente rechaza la tasacion.
        public SQLUpdateResult RejectAppraisal(Guid Acquisition_Id)
        {
            Acquisition acquisition = GetAppraisedAcquisition(Acquisition_Id);
            acquisition.Status = AcquisitionStatus.Rejected;
            acquisition.Rejection_Reason = RejectionReason.AppraisalRejected;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), AuditEventType.AcquisitionRejected, acquisition.Id);
        }

        // CUF10 8.1: el cliente no firma el contrato generado.
        public SQLUpdateResult RegisterContractNotSigned(Guid Acquisition_Id)
        {
            Acquisition acquisition = GetAppraisedAcquisition(Acquisition_Id);
            if (!HasAgreement(acquisition))
                throw new TranslatableException("ContractNotGenerated", "Genere el contrato de compraventa antes de registrar la firma.");
            acquisition.Status = AcquisitionStatus.Rejected;
            acquisition.Rejection_Reason = RejectionReason.ContractNotSigned;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), AuditEventType.AcquisitionRejected, acquisition.Id);
        }

        public List<AcquisitionDocumentType> GetDocumentTypes(AcquisitionDocumentStage stage)
        {
            return _acquisitionmapper.GetDocumentTypes(stage);
        }
        public List<AcquisitionDocument> GetDocuments(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetDocuments(Acquisition_Id);
        }
        public AcquisitionDocumentFile GetDocumentFile(Guid Document_Id)
        {
            return _acquisitionmapper.GetDocumentFile(Document_Id);
        }

        // CUF10 pasos 11 a 13 y 18: el documento y su escaneo se graban uno detras del otro, como los archivos de los tramites.
        // Para reemplazar uno cargado u observado (11.2) primero se elimina.
        public SQLUpdateResult AddDocument(AcquisitionDocument document)
        {
            AcquisitionDocumentType type = GetDocumentTypes(document.Stage).FirstOrDefault(t => t.Id == document.DocumentType_Id);
            if (type == null)
                throw new TranslatableException("PleaseSelectDocument", "Seleccione un documento de la lista para continuar.");
            ValidateDocumentFile(document.File);
            ValidateDocumentValidity(type, document.Issue_Date);

            Acquisition acquisition = GetAcquisitionInStage(document.Acquisition_Id, type.Stage);
            if (GetDocuments(acquisition.Id).Any(d => d.DocumentType_Id == type.Id))
                throw new TranslatableException("DocumentAlreadyUploaded", "El documento ya esta cargado. Eliminelo para reemplazarlo.");

            document.Id = Guid.NewGuid();
            document.Status = AcquisitionDocumentStatus.Uploaded;
            document.Issue_Date = document.Issue_Date.Date;
            document.Observation = null;
            document.User_Id = SessionHandler.Instance.GetId();
            document.Uploaded_Date = DateTime.Now;
            document.File.Document_Id = document.Id;

            SQLUpdateResult result = _acquisitionmapper.AddDocument(document);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.AddDocumentFile(document.File);
            result = RecordResult(result, AuditEventType.AcquisitionDocumentCreated, acquisition.Id);

            // Paso 15: con el ultimo documento cargado la compra queda lista para el pago (CUF11).
            if (result != null && result.sqlResult == SQLResultType.success && type.Stage == AcquisitionDocumentStage.Document
                && IsStageComplete(acquisition.Id, AcquisitionDocumentStage.Document))
            {
                acquisition.Status = AcquisitionStatus.DocumentationComplete;
                result = _acquisitionmapper.UpdateAcquisition(acquisition);
            }
            return result;
        }

        // CUF10 11.1: el titular no es el cliente o el dominio informa un gravamen. La compra no avanza hasta reemplazarlo.
        public SQLUpdateResult ObserveDocument(AcquisitionDocument document, string observation)
        {
            if (string.IsNullOrWhiteSpace(observation))
                throw new TranslatableException("PleaseWriteDocumentObservation", "Escriba el motivo de la observacion del documento para continuar.");
            Acquisition acquisition = GetAcquisitionInStage(document.Acquisition_Id, document.Stage);
            document.Status = AcquisitionDocumentStatus.Observed;
            document.Observation = observation.Trim();
            return RecordResult(_acquisitionmapper.ObserveDocument(document), AuditEventType.AcquisitionDocumentObserved, acquisition.Id);
        }

        // CUF10 11.2: el escaneo se borra con el documento.
        public SQLUpdateResult DeleteDocument(AcquisitionDocument document)
        {
            Acquisition acquisition = GetAcquisitionInStage(document.Acquisition_Id, document.Stage);
            return RecordResult(_acquisitionmapper.DeleteDocument(document), AuditEventType.AcquisitionDocumentDeleted, acquisition.Id);
        }

        // Todos los documentos de la etapa cargados y sin observaciones.
        public bool IsStageComplete(Guid Acquisition_Id, AcquisitionDocumentStage stage)
        {
            List<AcquisitionDocument> documents = GetDocuments(Acquisition_Id);
            return GetDocumentTypes(stage).All(t => documents.Any(d => d.DocumentType_Id == t.Id && d.Status == AcquisitionDocumentStatus.Uploaded));
        }

        // CUF10 paso 17 y 18.1: la cita queda agendada y la adquisicion sigue Pagada.
        public SQLUpdateResult ScheduleTransfer(Acquisition transfer)
        {
            if (string.IsNullOrWhiteSpace(transfer.Precharge_Code_08))
                throw new TranslatableException("PleaseWrite08", "Escriba el codigo de precarga del formulario 08 para continuar.");
            if (transfer.Transfer_Appointment_Date == null)
                throw new TranslatableException("PleaseSelectTransferAppointment", "Seleccione la fecha de la cita en el registro automotor para continuar.");

            Acquisition acquisition = GetAcquisitionInStatus(transfer.Id, AcquisitionStatus.Paid);
            acquisition.Precharge_Code_08 = transfer.Precharge_Code_08.Trim();
            acquisition.Transfer_Appointment_Date = transfer.Transfer_Appointment_Date.Value.Date;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), AuditEventType.AcquisitionTransferScheduled, acquisition.Id);
        }

        // CUF10 paso 19: despues de la cita, con el 08 firmado y la baja municipal cargados.
        public SQLUpdateResult RegisterTransfer(Guid Acquisition_Id)
        {
            Acquisition acquisition = GetAcquisitionInStatus(Acquisition_Id, AcquisitionStatus.Paid);
            if (acquisition.Precharge_Code_08 == null || acquisition.Transfer_Appointment_Date == null)
                throw new TranslatableException("TransferNotScheduled", "Guarde el codigo de precarga y la fecha de la cita para continuar.");
            if (acquisition.Transfer_Appointment_Date.Value.Date > DateTime.Today)
                throw new TranslatableException("TransferAppointmentPending", "La cita en el registro automotor todavia no ocurrio.");
            if (!IsStageComplete(Acquisition_Id, AcquisitionDocumentStage.Transfer))
                throw new TranslatableException("TransferDocumentsMissing", "Adjunte el formulario 08 firmado y el comprobante de baja municipal para continuar.");

            acquisition.Status = AcquisitionStatus.Transferred;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), AuditEventType.AcquisitionTransferred, acquisition.Id);
        }

        public AcquisitionSummary GetSummary(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetSummary(Acquisition_Id);
        }

        // CUF10 pasos 21 a 24: el vehiculo entra al inventario disponible para CUF01 con lo verificado en la
        // inspeccion. Es el unico paso en una transaccion: si quedara solo el vehiculo, se venderia con la compra abierta.
        public SQLUpdateResult FinishAcquisition(Guid Acquisition_Id, Vehicle sale)
        {
            if (sale.Price <= 0)
                throw new TranslatableException("PleaseWriteValidSalePrice", "Escriba un precio de venta mayor a cero para continuar.");

            Acquisition acquisition = GetAcquisitionInStatus(Acquisition_Id, AcquisitionStatus.Transferred);
            AcquisitionSummary summary = GetSummary(Acquisition_Id);
            Vehicle vehicle = new Vehicle();
            vehicle.Id = Guid.NewGuid();
            vehicle.License_Plate = summary.License_Plate;
            vehicle.Make_Id = summary.Make_Id;
            vehicle.Model_Id = summary.Model_Id;
            vehicle.Version_Id = summary.Version_Id;
            vehicle.Colour_Id = summary.Colour_Id;
            vehicle.Price = sale.Price;
            vehicle.Opcionals = sale.Opcionals == null ? null : sale.Opcionals.Trim();
            vehicle.Observations = sale.Observations == null ? null : sale.Observations.Trim();
            vehicle.Doors = summary.Doors;
            vehicle.Year = summary.Year;
            vehicle.Kilometers = summary.Verified_Kilometers ?? summary.Declared_Kilometers;
            vehicle.ImageLink = null;
            acquisition.Status = AcquisitionStatus.Finished;
            acquisition.Vehicle_Id = vehicle.Id;
            acquisition.Closing_Date = DateTime.Now;

            SQLUpdateResult result = RecordResult(_acquisitionmapper.FinishAcquisition(vehicle, acquisition), AuditEventType.VehicleCreated, vehicle.Id);
            return RecordResult(result, AuditEventType.AcquisitionFinished, acquisition.Id);
        }

        // CUF11 paso 2: compras del cliente con la documentacion completa y las que ya tienen comprobante.
        public List<PayableAcquisition> GetPayableAcquisitions(Guid Client_Id)
        {
            return _acquisitionmapper.GetPendingPayment(Client_Id);
        }
        public List<AcquisitionPayment> GetPayments(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetPayments(Acquisition_Id);
        }
        public PurchaseReceipt GetPurchaseReceipt(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetReceipt(Acquisition_Id);
        }
        // Saldo de un medio: lo pactado menos lo pagado, redondeado a centavos como la forma de pago.
        public decimal GetPaymentTermBalance(AcquisitionPaymentTerm term)
        {
            return (decimal)Math.Round(term.Amount, 2) - (decimal)Math.Round(term.Paid_Amount, 2);
        }
        public decimal GetBalance(List<AcquisitionPaymentTerm> terms)
        {
            return terms == null ? 0 : terms.Sum(t => GetPaymentTermBalance(t));
        }

        // CUF11 pasos 3 a 5: el pago es con un medio pactado y no puede superar su saldo (4.1). Cada medio
        // guarda solo sus datos: la transferencia banco, CBU o alias y operacion; el cheque banco, numero y fecha.
        public SQLUpdateResult AddPayment(AcquisitionPayment payment)
        {
            GetAcquisitionInStatus(payment.Acquisition_Id, AcquisitionStatus.DocumentationComplete);
            List<AcquisitionPaymentTerm> terms = GetPaymentTerms(payment.Acquisition_Id);
            AcquisitionPaymentTerm term = terms == null ? null : terms.FirstOrDefault(t => t.PaymentType_Id == payment.PaymentType_Id);
            if (term == null)
                throw new TranslatableException("PaymentTypeNotAgreed", "El medio de pago no esta en la forma de pago pactada con el cliente.");
            if (payment.Amount <= 0)
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            decimal balance = GetPaymentTermBalance(term);
            if ((decimal)Math.Round(payment.Amount, 2) > balance)
                throw new TranslatableException("PaymentExceedsBalance", "El monto supera el saldo del medio pactado: $ {0}.", balance.ToString("N2"));

            payment.Amount = Math.Round(payment.Amount, 2);
            NormalizePaymentDetails(payment, term.PaymentType_Description);
            payment.Cashier_Id = SessionHandler.Instance.GetId();
            payment.Creation_Date = DateTime.Now;
            return RecordResult(_acquisitionmapper.AddPayment(payment), AuditEventType.AcquisitionPaymentCreated, payment.Id);
        }

        // CUF11 pasos 7 y 9: con el saldo en cero se emite el comprobante y la compra pasa a Pagada, en una sola
        // transaccion. Si fallara despues del ultimo pago, la compra sigue en Documentacion completa y se reintenta.
        public SQLUpdateResult IssuePurchaseReceipt(Guid Acquisition_Id)
        {
            Acquisition acquisition = GetAcquisitionInStatus(Acquisition_Id, AcquisitionStatus.DocumentationComplete);
            decimal balance = GetBalance(GetPaymentTerms(Acquisition_Id));
            if (balance != 0)
                throw new TranslatableException("PurchaseBalancePending", "La compra todavia tiene saldo pendiente: $ {0}.", balance.ToString("N2"));

            AcquisitionSummary summary = GetSummary(Acquisition_Id);
            Client client = new ClientsBLL().GetById(acquisition.Client_Id);
            PurchaseReceipt receipt = new PurchaseReceipt();
            receipt.Id = Guid.NewGuid();
            receipt.Number = _acquisitionmapper.GetNextReceiptNumber();
            receipt.Acquisition_Id = acquisition.Id;
            receipt.Issue_Date = DateTime.Now;
            receipt.Client_Name = client.Name + " " + client.Surname;
            receipt.Client_CUIL_CUIT = acquisition.Owner_CUIL_CUIT;
            receipt.Detail = summary.Vehicle_Description + " - " + summary.License_Plate + " - " + summary.Chassis_Number + " - " + summary.Engine_Number;
            // La compra a un particular no lleva impuestos: el total es el precio acordado.
            receipt.Net_Amount = Math.Round(acquisition.Agreed_Price.Value, 2);
            receipt.Tax_Amount = 0;
            receipt.Total_Amount = receipt.Net_Amount + receipt.Tax_Amount;
            acquisition.Status = AcquisitionStatus.Paid;

            return RecordResult(_acquisitionmapper.PayAcquisition(receipt, acquisition), AuditEventType.PurchaseReceiptIssued, acquisition.Id);
        }

        // Etapa A: Tasada y con la tasacion vigente (3.1).
        private Acquisition GetAppraisedAcquisition(Guid Acquisition_Id)
        {
            Acquisition acquisition = GetAcquisitionInStatus(Acquisition_Id, AcquisitionStatus.Appraised);
            if (IsAppraisalExpired(GetLastAppraisal(Acquisition_Id)))
                throw new TranslatableException("AppraisalExpired", "La tasacion vencio. Vuelva a tasar el vehiculo desde Compras > Tasar vehiculo.");
            return acquisition;
        }

        // Los documentos de la etapa B se cargan con la compra formalizada y los de la etapa C con la compra pagada.
        private Acquisition GetAcquisitionInStage(Guid Acquisition_Id, AcquisitionDocumentStage stage)
        {
            return GetAcquisitionInStatus(Acquisition_Id,
                stage == AcquisitionDocumentStage.Document ? AcquisitionStatus.PurchaseFormalized : AcquisitionStatus.Paid);
        }

        private Acquisition GetAcquisitionInStatus(Guid Acquisition_Id, AcquisitionStatus status)
        {
            Acquisition acquisition = _acquisitionmapper.GetById(Acquisition_Id);
            if (acquisition == null || acquisition.Status != status)
                throw new TranslatableException("AcquisitionNotInStage", "La adquisicion ya no esta en esta etapa. Vuelva a buscarla.");
            return acquisition;
        }

        // CUF10 pasos 5 y 6: medios permitidos, una vez cada uno, con monto, y la suma igual al precio (6.1).
        private void ValidatePaymentTerms(double price, List<AcquisitionPaymentTerm> terms)
        {
            if (terms == null || terms.Count == 0)
                throw new TranslatableException("PleaseAddPaymentTerm", "Agregue al menos un medio de pago para continuar.");
            List<Guid> allowed = GetAcquisitionPaymentTypes().Select(t => t.Id).ToList();
            if (terms.Any(t => !allowed.Contains(t.PaymentType_Id)))
                throw new TranslatableException("InvalidAcquisitionPaymentType", "La compra solo se paga con transferencia, cheque o efectivo.");
            if (terms.GroupBy(t => t.PaymentType_Id).Any(g => g.Count() > 1))
                throw new TranslatableException("PaymentTypeAlreadyAdded", "El medio de pago ya esta en la forma de pago. Quitelo para cambiar el monto.");
            if (terms.Any(t => t.Amount <= 0))
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            decimal difference = GetPaymentTermsDifference(price, terms);
            if (difference != 0)
                throw new TranslatableException("PaymentTermsDifference", "La forma de pago no coincide con el precio acordado. Diferencia: $ {0}.", difference.ToString("N2"));
        }

        // CUF11 paso 4: datos obligatorios de cada medio. Los del resto quedan en NULL.
        private static void NormalizePaymentDetails(AcquisitionPayment payment, string paymentType)
        {
            string bank = payment.Bank == null ? "" : payment.Bank.Trim();
            string cbu_alias = payment.Cbu_Alias == null ? "" : payment.Cbu_Alias.Trim();
            string operation_number = payment.Operation_Number == null ? "" : payment.Operation_Number.Trim();
            string check_number = payment.Check_Number == null ? "" : payment.Check_Number.Trim();
            DateTime? payment_date = payment.Payment_Date;
            payment.Bank = null;
            payment.Cbu_Alias = null;
            payment.Operation_Number = null;
            payment.Check_Number = null;
            payment.Payment_Date = null;

            if (paymentType == BankTransferPaymentType)
            {
                if (bank == "")
                    throw new TranslatableException("PleaseWriteBank", "Escriba el banco para continuar.");
                if (!CbuFormat.IsMatch(cbu_alias) && !(AliasFormat.IsMatch(cbu_alias) && !cbu_alias.All(char.IsDigit)))
                    throw new TranslatableException("PleaseWriteValidCbuAlias", "Escriba un CBU de 22 digitos o un alias de 6 a 20 caracteres (letras, numeros, punto o guion).");
                if (operation_number == "")
                    throw new TranslatableException("PleaseWriteOperationNumber", "Escriba el numero de operacion de la transferencia para continuar.");
                payment.Bank = bank;
                payment.Cbu_Alias = cbu_alias;
                payment.Operation_Number = operation_number;
            }
            else if (paymentType == CheckPaymentType)
            {
                if (bank == "")
                    throw new TranslatableException("PleaseWriteBank", "Escriba el banco para continuar.");
                if (check_number == "")
                    throw new TranslatableException("PleaseWriteCheckNumber", "Escriba el numero de cheque para continuar.");
                if (payment_date == null)
                    throw new TranslatableException("PleaseSelectCheckPaymentDate", "Seleccione la fecha de pago del cheque para continuar.");
                if (payment_date.Value.Date < DateTime.Today)
                    throw new TranslatableException("CheckPaymentDateInPast", "La fecha de pago del cheque no puede ser anterior a hoy.");
                payment.Bank = bank;
                payment.Check_Number = check_number;
                payment.Payment_Date = payment_date.Value.Date;
            }
        }

        private static void ValidateDocumentFile(AcquisitionDocumentFile file)
        {
            if (file == null || file.FileContent == null || file.FileContent.Length == 0)
                throw new TranslatableException("PleaseSelectDocumentFile", "Adjunte el archivo escaneado del documento para continuar.");
            if (!DocumentExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
                throw new TranslatableException("InvalidDocumentFormat", "Los documentos tienen que ser archivos pdf.");
        }

        // CUF10 paso 12: emision no futura y, si el tipo vence, dentro de su vigencia.
        private static void ValidateDocumentValidity(AcquisitionDocumentType type, DateTime Issue_Date)
        {
            if (Issue_Date.Date > DateTime.Today)
                throw new TranslatableException("DocumentIssueDateInFuture", "La fecha de emision del documento no puede ser futura.");
            if (type.Validity_Days != null && Issue_Date.Date.AddDays(type.Validity_Days.Value) < DateTime.Today)
                throw new TranslatableException("DocumentExpired", "El documento esta vencido: tiene una vigencia de {0} dias desde su emision.", type.Validity_Days.Value);
        }

        private static void ValidatePhoto(InspectionPhoto photo)
        {
            if (!PhotoExtensions.Contains(Path.GetExtension(photo.FileName).ToLowerInvariant()))
                throw new TranslatableException("InvalidPhotoFormat", "Las fotografias tienen que ser archivos jpg o png.");
            if (photo.FileContent.Length > MaxPhotoMegabytes * 1024 * 1024)
                throw new TranslatableException("PhotoTooLarge", "Las fotografias no pueden superar los {0} MB.", MaxPhotoMegabytes);
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
