using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using System;
using System.Collections.Generic;
using System.IO;
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

        // Fotografias de la inspeccion (CUF08 paso 5).
        public const int MaxPhotoMegabytes = 5;
        private static readonly string[] PhotoExtensions = { ".jpg", ".jpeg", ".png" };

        // Vigencia de la tasacion en dias corridos (CUF09 paso 6). Vencida, se puede volver a tasar.
        public const int AppraisalValidityDays = 7;

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
