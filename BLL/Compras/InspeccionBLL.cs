using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;

namespace CarAgency.BLL
{
    // Turnos de los mecanicos (CUF07) e inspeccion del vehiculo ofrecido (CUF08).
    public class InspeccionBLL
    {
        // Turnos de inspeccion: de lunes a viernes, en punto, dentro del horario del taller.
        public const int InspectionSlotMinutes = 60;
        public const int WorkdayStartHour = 9;
        public const int WorkdayEndHour = 18;
        private const int NextFreeSlotSearchDays = 30;

        // Fotografias de la inspeccion (CUF08 paso 5).
        public const int MaxPhotoMegabytes = 5;
        private static readonly string[] PhotoExtensions = { ".jpg", ".jpeg", ".png" };

        private InspeccionMapper _inspectionmapper;
        private CompraMapper _acquisitionmapper;
        public InspeccionBLL()
        {
            _inspectionmapper = new InspeccionMapper();
            _acquisitionmapper = new CompraMapper();
        }
        public List<Usuario> GetMechanics()
        {
            return _inspectionmapper.GetMechanics(PermisoType.RegisterInspeccionForm);
        }
        public List<TimeSpan> GetInspectionHours()
        {
            List<TimeSpan> hours = new List<TimeSpan>();
            TimeSpan slot = TimeSpan.FromMinutes(InspectionSlotMinutes);
            for (TimeSpan hour = TimeSpan.FromHours(WorkdayStartHour); hour + slot <= TimeSpan.FromHours(WorkdayEndHour); hour += slot)
                hours.Add(hour);
            return hours;
        }
        // Primer turno de lunes a viernes que todavia no paso: es el que viene elegido al programar la inspeccion.
        public DateTime GetFirstInspectionSlot(DateTime from)
        {
            List<TimeSpan> hours = GetInspectionHours();
            for (DateTime day = from.Date; ; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                    continue;
                foreach (TimeSpan hour in hours)
                {
                    if (day + hour > from)
                        return day + hour;
                }
            }
        }
        public bool IsMechanicAvailable(Guid Mechanic_Id, DateTime Scheduled_Date)
        {
            ValidateInspectionSlot(Scheduled_Date);
            return !_inspectionmapper.IsMechanicBusy(Mechanic_Id, Scheduled_Date, InspectionSlotMinutes);
        }
        public DateTime GetNextFreeSlot(Guid Mechanic_Id, DateTime from)
        {
            DateTime? slot = _inspectionmapper.GetNextFreeSlot(Mechanic_Id, from, InspectionSlotMinutes, WorkdayStartHour, WorkdayEndHour,
                NextFreeSlotSearchDays);
            if (slot == null)
                throw new TranslatableException("NoFreeInspectionSlot", "El mecanico no tiene turnos libres en los proximos {0} dias.", NextFreeSlotSearchDays);
            return slot.Value;
        }

        // La inspeccion se programa con el alta de la adquisicion (CUF07): CompraBLL valida y arma los datos.
        internal SQLUpdateResult AddInspection(Inspeccion inspection)
        {
            return _inspectionmapper.AddInspection(inspection);
        }

        // La fecha programada es orientativa: el mecanico ve todas sus inspecciones pendientes.
        public List<ScheduledInspeccion> GetScheduledInspections()
        {
            return _inspectionmapper.GetScheduledInspections(SessionHandler.Instance.GetId());
        }
        public InspeccionDetail GetInspectionDetail(Guid Inspection_Id)
        {
            return _inspectionmapper.GetInspectionDetail(Inspection_Id);
        }
        public List<ItemInspeccion> GetInspectionItems()
        {
            return _inspectionmapper.GetInspectionItems();
        }
        public List<FotografiaInspeccion> GetInspectionPhotos(Guid Inspection_Id)
        {
            return _inspectionmapper.GetInspectionPhotos(Inspection_Id);
        }
        // Las fotos se guardan a medida que se adjuntan: quedan con la inspeccion aunque no se confirme.
        public SQLUpdateResult AddInspectionPhoto(FotografiaInspeccion photo)
        {
            ValidatePhoto(photo);
            photo.FotografiaInspeccion_Id = Guid.NewGuid();
            photo.FechaCarga = DateTime.Now;
            return _inspectionmapper.AddInspectionPhoto(photo);
        }
        public SQLUpdateResult DeleteInspectionPhoto(FotografiaInspeccion photo)
        {
            return _inspectionmapper.DeleteInspectionPhoto(photo);
        }

        // CUF08: Apto pasa la adquisicion a Inspeccionado y No apto a Rechazada (6.2).
        public SQLUpdateResult RecordInspection(Inspeccion inspection)
        {
            if (inspection.Resultado != InspeccionResult.Fit && inspection.Resultado != InspeccionResult.Unfit)
                throw new TranslatableException("PleaseSelectInspectionResult", "Seleccione el resultado de la inspeccion para continuar.");
            if (inspection.KilometrajeVerificado == null || inspection.KilometrajeVerificado < 0)
                throw new TranslatableException("PleaseWriteValidKilometers", "Escriba un kilometraje valido para continuar.");
            if (GetInspectionItems().Any(item => inspection.Puntajes == null || !inspection.Puntajes.Any(s => s.ItemInspeccion_Id == item.ItemInspeccion_Id)))
                throw new TranslatableException("InspectionItemsNotScored", "Califique todos los items de la inspeccion para continuar.");
            if (GetInspectionPhotos(inspection.Inspeccion_Id) == null)
                throw new TranslatableException("InspectionPhotoRequired", "Adjunte al menos una fotografia del vehiculo para continuar.");

            Compra acquisition = GetScheduledAcquisition(inspection);
            inspection.FechaRealizada = DateTime.Now;
            foreach (PuntajeInspeccion score in inspection.Puntajes)
                score.Inspeccion_Id = inspection.Inspeccion_Id;
            if (inspection.Resultado == InspeccionResult.Fit)
                acquisition.Estado = CompraStatus.Inspected;
            else
            {
                acquisition.Estado = CompraStatus.Rejected;
                acquisition.MotivoRechazo = RejectionReason.InspectionUnfit;
            }

            SQLUpdateResult result = RecordResult(_inspectionmapper.RecordInspection(inspection, acquisition), TipoEvento.InspectionRecorded, acquisition.Compra_Id);
            if (acquisition.Estado == CompraStatus.Rejected)
                result = RecordResult(result, TipoEvento.AcquisitionRejected, acquisition.Compra_Id);
            return result;
        }

        // CUF08 2.1: el vehiculo no se presento. La inspeccion queda como ausencia, sin calificaciones,
        // y la adquisicion se cancela: el vendedor tiene que iniciar otra con CUF07.
        public SQLUpdateResult RegisterNoShow(Inspeccion inspection)
        {
            Compra acquisition = GetScheduledAcquisition(inspection);
            inspection.FechaRealizada = DateTime.Now;
            inspection.KilometrajeVerificado = null;
            inspection.Resultado = InspeccionResult.NoShow;
            inspection.Puntajes = new List<PuntajeInspeccion>();
            acquisition.Estado = CompraStatus.Cancelled;
            return RecordResult(_inspectionmapper.RecordInspection(inspection, acquisition), TipoEvento.AcquisitionCancelled, acquisition.Compra_Id);
        }

        // Solo el mecanico asignado registra la inspeccion, y una sola vez.
        private Compra GetScheduledAcquisition(Inspeccion inspection)
        {
            Compra acquisition = _acquisitionmapper.GetById(inspection.Compra_Id);
            if (acquisition == null || acquisition.Estado != CompraStatus.InspectionScheduled || inspection.Mecanico_Id != SessionHandler.Instance.GetId())
                throw new TranslatableException("InspectionNotPending", "La inspeccion ya fue registrada o no esta asignada a este mecanico.");
            return acquisition;
        }

        public Inspeccion GetInspection(Guid Inspection_Id)
        {
            return _inspectionmapper.GetInspection(Inspection_Id);
        }
        public List<PuntajeInspeccionDetail> GetInspectionScores(Guid Inspection_Id)
        {
            return _inspectionmapper.GetInspectionScores(Inspection_Id);
        }

        private static void ValidatePhoto(FotografiaInspeccion photo)
        {
            if (!PhotoExtensions.Contains(Path.GetExtension(photo.NombreArchivo).ToLowerInvariant()))
                throw new TranslatableException("InvalidPhotoFormat", "Las fotografias tienen que ser archivos jpg o png.");
            if (photo.Contenido.Length > MaxPhotoMegabytes * 1024 * 1024)
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

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
