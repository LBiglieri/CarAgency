using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
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
    public class CompraBLL
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

        // Medios con los que la agencia le paga a la persona (CUF10 paso 5). Se filtran por Description,
        // como las tarjetas en AddPaymentForm.
        public const string BankTransferPaymentType = "Bank Transfer";
        public const string CheckPaymentType = "Check";
        public const string CashPaymentType = "Cash";
        public static readonly string[] AcquisitionPaymentTypes = { BankTransferPaymentType, CheckPaymentType, CashPaymentType };
        // El que viene elegido al armar la forma de pago: es el mas comun.
        public const string DefaultPaymentType = BankTransferPaymentType;

        // Emisor del comprobante de compra (CUF11 paso 7). PurchaseReceipts no lo guarda porque es siempre la agencia.
        public const string AgencyName = "CarAgency";
        public const string AgencyCuit = "30-71654328-1";

        // CBU de 22 digitos o alias de 6 a 20 caracteres (letras, numeros, punto o guion).
        private static readonly Regex CbuFormat = new Regex(@"^\d{22}$");
        private static readonly Regex AliasFormat = new Regex(@"^[A-Za-z0-9.\-]{6,20}$");

        // Documentos escaneados (CUF10 paso 12): solo pdf, como los archivos de los tramites.
        private static readonly string[] DocumentExtensions = { ".pdf" };

        private CompraMapper _acquisitionmapper;
        private PagoMapper _paymentmapper;
        public CompraBLL()
        {
            _acquisitionmapper = new CompraMapper();
            _paymentmapper = new PagoMapper();
        }
        public List<Usuario> GetMechanics()
        {
            return _acquisitionmapper.GetMechanics(PermisoType.RegisterInspeccionForm);
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
        public List<DuplicateVehiculo> GetDuplicates(VehiculoProspecto vehicle)
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
        public SQLUpdateResult AddAcquisition(Compra acquisition)
        {
            VehiculoProspecto vehicle = acquisition.VehiculoProspecto;
            Inspeccion inspection = acquisition.Inspeccion;

            if (GetDuplicates(vehicle) != null)
                throw new TranslatableException("VehicleAlreadyRegistered", "Ya existe un vehiculo en inventario o una adquisicion abierta con la misma patente, chasis o motor.");
            if (!IsMechanicAvailable(inspection.Mecanico_Id, inspection.FechaProgramada))
                throw new TranslatableException("MechanicNotAvailable", "El mecanico ya tiene una inspeccion en esa franja.");

            vehicle.VehiculoProspecto_Id = Guid.NewGuid();
            acquisition.Compra_Id = Guid.NewGuid();
            acquisition.Numero = _acquisitionmapper.GetNextNumber();
            acquisition.VehiculoProspecto_Id = vehicle.VehiculoProspecto_Id;
            acquisition.Vendedor_Id = SessionHandler.Instance.GetId();
            acquisition.Estado = CompraStatus.InspectionScheduled;
            acquisition.Fecha = DateTime.Now;
            inspection.Inspeccion_Id = Guid.NewGuid();
            inspection.Compra_Id = acquisition.Compra_Id;

            SQLUpdateResult result = _acquisitionmapper.AddProspectVehicle(vehicle);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.AddAcquisition(acquisition);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.AddInspection(inspection);
            return RecordResult(result, TipoEvento.AcquisitionCreated, acquisition.Compra_Id);
        }

        // La fecha programada es orientativa: el mecanico ve todas sus inspecciones pendientes.
        public List<ScheduledInspeccion> GetScheduledInspections()
        {
            return _acquisitionmapper.GetScheduledInspections(SessionHandler.Instance.GetId());
        }
        public InspeccionDetail GetInspectionDetail(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspectionDetail(Inspection_Id);
        }
        public List<ItemInspeccion> GetInspectionItems()
        {
            return _acquisitionmapper.GetInspectionItems();
        }
        public List<FotografiaInspeccion> GetInspectionPhotos(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspectionPhotos(Inspection_Id);
        }
        // Las fotos se guardan a medida que se adjuntan: quedan con la inspeccion aunque no se confirme.
        public SQLUpdateResult AddInspectionPhoto(FotografiaInspeccion photo)
        {
            ValidatePhoto(photo);
            photo.FotografiaInspeccion_Id = Guid.NewGuid();
            photo.FechaCarga = DateTime.Now;
            return _acquisitionmapper.AddInspectionPhoto(photo);
        }
        public SQLUpdateResult DeleteInspectionPhoto(FotografiaInspeccion photo)
        {
            return _acquisitionmapper.DeleteInspectionPhoto(photo);
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

            SQLUpdateResult result = RecordResult(_acquisitionmapper.RecordInspection(inspection, acquisition), TipoEvento.InspectionRecorded, acquisition.Compra_Id);
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
            return RecordResult(_acquisitionmapper.RecordInspection(inspection, acquisition), TipoEvento.AcquisitionCancelled, acquisition.Compra_Id);
        }

        // Solo el mecanico asignado registra la inspeccion, y una sola vez.
        private Compra GetScheduledAcquisition(Inspeccion inspection)
        {
            Compra acquisition = _acquisitionmapper.GetById(inspection.Compra_Id);
            if (acquisition == null || acquisition.Estado != CompraStatus.InspectionScheduled || inspection.Mecanico_Id != SessionHandler.Instance.GetId())
                throw new TranslatableException("InspectionNotPending", "La inspeccion ya fue registrada o no esta asignada a este mecanico.");
            return acquisition;
        }

        // La matricula se guarda en mayusculas; el DNI se compara tal cual.
        public List<PendingTasacion> GetPendingAppraisals(string filter)
        {
            return _acquisitionmapper.GetPendingAppraisals(filter == null ? null : filter.Trim().ToUpperInvariant(), DateTime.Now);
        }
        public Inspeccion GetInspection(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspection(Inspection_Id);
        }
        public List<PuntajeInspeccionDetail> GetInspectionScores(Guid Inspection_Id)
        {
            return _acquisitionmapper.GetInspectionScores(Inspection_Id);
        }
        public Tasacion GetLastAppraisal(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetLastAppraisal(Acquisition_Id);
        }

        // CUF09: la tasacion y el pase a Tasada se graban uno detras del otro, como el resto de las altas.
        public SQLUpdateResult AddAppraisal(Tasacion appraisal)
        {
            if (appraisal.Valor <= 0)
                throw new TranslatableException("PleaseWriteValidAppraisalAmount", "Escriba un valor de tasacion mayor a cero para continuar.");
            if (string.IsNullOrWhiteSpace(appraisal.Justificacion))
                throw new TranslatableException("PleaseWriteAppraisalJustification", "Escriba la justificacion de la tasacion para continuar.");

            Compra acquisition = GetAppraisableAcquisition(appraisal.Compra_Id);
            appraisal.Tasacion_Id = Guid.NewGuid();
            appraisal.Tasador_Id = SessionHandler.Instance.GetId();
            appraisal.Justificacion = appraisal.Justificacion.Trim();
            appraisal.Fecha = DateTime.Now;
            appraisal.Vigencia = appraisal.Fecha.AddDays(AppraisalValidityDays);
            acquisition.Estado = CompraStatus.Appraised;

            SQLUpdateResult result = _acquisitionmapper.AddAppraisal(appraisal);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.UpdateAcquisition(acquisition);
            return RecordResult(result, TipoEvento.AppraisalCreated, acquisition.Compra_Id);
        }

        // Se tasa una adquisicion Inspeccionada, o una Tasada cuya ultima tasacion ya vencio (2.1).
        // Mientras la tasacion este vigente no se puede volver a tasar.
        private Compra GetAppraisableAcquisition(Guid Acquisition_Id)
        {
            Compra acquisition = _acquisitionmapper.GetById(Acquisition_Id);
            if (acquisition != null && acquisition.Estado == CompraStatus.Inspected)
                return acquisition;
            if (acquisition != null && acquisition.Estado == CompraStatus.Appraised)
            {
                Tasacion last = GetLastAppraisal(Acquisition_Id);
                if (last == null || last.Vigencia < DateTime.Now)
                    return acquisition;
            }
            throw new TranslatableException("AppraisalNotPending", "La adquisicion tiene una tasacion vigente o no esta en estado Inspeccionado.");
        }

        // CUF10: de Tasada a Transferida. La matricula se guarda en mayusculas; el DNI se compara tal cual.
        public List<PendingCompra> GetPendingAcquisitions(string filter)
        {
            return _acquisitionmapper.GetPendingManagement(filter == null ? null : filter.Trim().ToUpperInvariant());
        }
        public Compra GetAcquisition(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetById(Acquisition_Id);
        }
        // CUF10 3.1: vencida la tasacion no se puede seguir, hay que volver a tasar con CUF09.
        public bool IsAppraisalExpired(Tasacion appraisal)
        {
            return appraisal == null || appraisal.Vigencia < DateTime.Now;
        }
        // El acuerdo es valido solo si se hizo sobre la ultima tasacion: si se re-taso, hay que volver a pactarlo.
        public bool HasAgreement(Compra acquisition)
        {
            Tasacion last = GetLastAppraisal(acquisition.Compra_Id);
            return acquisition.PrecioAcordado != null && last != null && acquisition.TasacionAceptada_Id == last.Tasacion_Id;
        }
        public List<TipoPago> GetAcquisitionPaymentTypes()
        {
            List<TipoPago> types = _paymentmapper.GetAllPaymentTypes();
            return types == null ? new List<TipoPago>() : types.Where(t => AcquisitionPaymentTypes.Contains(t.Descripcion)).ToList();
        }
        public List<FormaPagoCompra> GetPaymentTerms(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetPaymentTerms(Acquisition_Id);
        }
        // CUF10 paso 6: la suma se compara redondeada a centavos. Positiva, falta asignar; negativa, sobra.
        public decimal GetPaymentTermsDifference(double price, List<FormaPagoCompra> terms)
        {
            double total = terms == null ? 0 : terms.Sum(t => t.Monto);
            return (decimal)Math.Round(price, 2) - (decimal)Math.Round(total, 2);
        }

        // CUF10 pasos 5 a 7: el acuerdo se registra al generar el contrato y la adquisicion sigue Tasada hasta la firma.
        // Mientras no se firme se puede volver a generar: la forma de pago se reemplaza completa.
        public SQLUpdateResult RegisterAgreement(Compra agreement, List<FormaPagoCompra> terms)
        {
            if (agreement.PrecioAcordado == null || agreement.PrecioAcordado <= 0)
                throw new TranslatableException("PleaseWriteValidAgreedPrice", "Escriba un precio acordado mayor a cero para continuar.");
            if (string.IsNullOrWhiteSpace(agreement.CuilCuitTitular))
                throw new TranslatableException("PleaseWriteOwnerCuilCuit", "Escriba el CUIL/CUIT del titular del vehiculo para continuar.");
            ValidatePaymentTerms(agreement.PrecioAcordado.Value, terms);

            Compra acquisition = GetAppraisedAcquisition(agreement.Compra_Id);
            acquisition.TasacionAceptada_Id = GetLastAppraisal(acquisition.Compra_Id).Tasacion_Id;
            acquisition.PrecioAcordado = agreement.PrecioAcordado;
            acquisition.CuilCuitTitular = agreement.CuilCuitTitular.Trim();

            SQLUpdateResult result = _acquisitionmapper.UpdateAcquisition(acquisition);
            if (result != null && result.sqlResult == SQLResultType.success && GetPaymentTerms(acquisition.Compra_Id) != null)
                result = _acquisitionmapper.DeletePaymentTerms(acquisition.Compra_Id);
            foreach (FormaPagoCompra term in terms)
            {
                term.Compra_Id = acquisition.Compra_Id;
                if (result != null && result.sqlResult == SQLResultType.success)
                    result = _acquisitionmapper.AddPaymentTerm(term);
            }
            // Sin evento: la formalizacion se registra con la firma del contrato.
            return result;
        }

        // CUF10 pasos 8 y 9: con el contrato firmado el precio queda bloqueado.
        public SQLUpdateResult RegisterContractSigned(Guid Acquisition_Id)
        {
            Compra acquisition = GetAppraisedAcquisition(Acquisition_Id);
            if (!HasAgreement(acquisition))
                throw new TranslatableException("ContractNotGenerated", "Genere el contrato de compraventa antes de registrar la firma.");
            ValidatePaymentTerms(acquisition.PrecioAcordado.Value, GetPaymentTerms(Acquisition_Id));

            acquisition.FechaFirmaContrato = DateTime.Now;
            acquisition.Estado = CompraStatus.PurchaseFormalized;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), TipoEvento.AcquisitionFormalized, acquisition.Compra_Id);
        }

        // CUF10 3.3: la persona rechaza la tasacion.
        public SQLUpdateResult RejectAppraisal(Guid Acquisition_Id)
        {
            Compra acquisition = GetAppraisedAcquisition(Acquisition_Id);
            acquisition.Estado = CompraStatus.Rejected;
            acquisition.MotivoRechazo = RejectionReason.AppraisalRejected;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), TipoEvento.AcquisitionRejected, acquisition.Compra_Id);
        }

        // CUF10 8.1: la persona no firma el contrato generado.
        public SQLUpdateResult RegisterContractNotSigned(Guid Acquisition_Id)
        {
            Compra acquisition = GetAppraisedAcquisition(Acquisition_Id);
            if (!HasAgreement(acquisition))
                throw new TranslatableException("ContractNotGenerated", "Genere el contrato de compraventa antes de registrar la firma.");
            acquisition.Estado = CompraStatus.Rejected;
            acquisition.MotivoRechazo = RejectionReason.ContractNotSigned;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), TipoEvento.AcquisitionRejected, acquisition.Compra_Id);
        }

        public List<TipoDocumentoCompra> GetDocumentTypes(DocumentoCompraStage stage)
        {
            return _acquisitionmapper.GetDocumentTypes(stage);
        }
        public List<DocumentoCompra> GetDocuments(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetDocuments(Acquisition_Id);
        }
        public ArchivoDocumentoCompra GetDocumentFile(Guid Document_Id)
        {
            return _acquisitionmapper.GetDocumentFile(Document_Id);
        }

        // CUF10 pasos 11 a 13 y 18: el documento y su escaneo se graban uno detras del otro, como los archivos de los tramites.
        // Para reemplazar uno cargado u observado (11.2) primero se elimina.
        public SQLUpdateResult AddDocument(DocumentoCompra document)
        {
            TipoDocumentoCompra type = GetDocumentTypes(document.Etapa).FirstOrDefault(t => t.TipoDocumentoCompra_Id == document.TipoDocumentoCompra_Id);
            if (type == null)
                throw new TranslatableException("PleaseSelectDocument", "Seleccione un documento de la lista para continuar.");
            ValidateDocumentFile(document.File);
            ValidateDocumentValidity(type, document.FechaEmision);

            Compra acquisition = GetAcquisitionInStage(document.Compra_Id, type.Etapa);
            if (GetDocuments(acquisition.Compra_Id).Any(d => d.TipoDocumentoCompra_Id == type.TipoDocumentoCompra_Id))
                throw new TranslatableException("DocumentAlreadyUploaded", "El documento ya esta cargado. Eliminelo para reemplazarlo.");

            document.DocumentoCompra_Id = Guid.NewGuid();
            document.Estado = DocumentoCompraStatus.Uploaded;
            document.FechaEmision = document.FechaEmision.Date;
            document.Observacion = null;
            document.UsuarioCarga_Id = SessionHandler.Instance.GetId();
            document.FechaCarga = DateTime.Now;
            document.File.DocumentoCompra_Id = document.DocumentoCompra_Id;

            SQLUpdateResult result = _acquisitionmapper.AddDocument(document);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.AddDocumentFile(document.File);
            result = RecordResult(result, TipoEvento.AcquisitionDocumentCreated, acquisition.Compra_Id);

            // Paso 15: con el ultimo documento cargado la compra queda lista para el pago (CUF11).
            if (result != null && result.sqlResult == SQLResultType.success && type.Etapa == DocumentoCompraStage.Document
                && IsStageComplete(acquisition.Compra_Id, DocumentoCompraStage.Document))
            {
                acquisition.Estado = CompraStatus.DocumentationComplete;
                result = _acquisitionmapper.UpdateAcquisition(acquisition);
            }
            return result;
        }

        // CUF10 11.1: el titular no es la persona o el dominio informa un gravamen. La compra no avanza hasta reemplazarlo.
        public SQLUpdateResult ObserveDocument(DocumentoCompra document, string observation)
        {
            if (string.IsNullOrWhiteSpace(observation))
                throw new TranslatableException("PleaseWriteDocumentObservation", "Escriba el motivo de la observacion del documento para continuar.");
            Compra acquisition = GetAcquisitionInStage(document.Compra_Id, document.Etapa);
            document.Estado = DocumentoCompraStatus.Observed;
            document.Observacion = observation.Trim();
            return RecordResult(_acquisitionmapper.ObserveDocument(document), TipoEvento.AcquisitionDocumentObserved, acquisition.Compra_Id);
        }

        // CUF10 11.2: el escaneo se borra con el documento.
        public SQLUpdateResult DeleteDocument(DocumentoCompra document)
        {
            Compra acquisition = GetAcquisitionInStage(document.Compra_Id, document.Etapa);
            return RecordResult(_acquisitionmapper.DeleteDocument(document), TipoEvento.AcquisitionDocumentDeleted, acquisition.Compra_Id);
        }

        // Todos los documentos de la etapa cargados y sin observaciones.
        public bool IsStageComplete(Guid Acquisition_Id, DocumentoCompraStage stage)
        {
            List<DocumentoCompra> documents = GetDocuments(Acquisition_Id);
            return GetDocumentTypes(stage).All(t => documents.Any(d => d.TipoDocumentoCompra_Id == t.TipoDocumentoCompra_Id && d.Estado == DocumentoCompraStatus.Uploaded));
        }

        // CUF10 paso 17 y 18.1: la cita queda agendada y la adquisicion sigue Pagada.
        public SQLUpdateResult ScheduleTransfer(Compra transfer)
        {
            if (string.IsNullOrWhiteSpace(transfer.CodigoPrecarga08))
                throw new TranslatableException("PleaseWrite08", "Escriba el codigo de precarga del formulario 08 para continuar.");
            if (transfer.FechaCitaTransferencia == null)
                throw new TranslatableException("PleaseSelectTransferAppointment", "Seleccione la fecha de la cita en el registro automotor para continuar.");

            Compra acquisition = GetAcquisitionInStatus(transfer.Compra_Id, CompraStatus.Paid);
            acquisition.CodigoPrecarga08 = transfer.CodigoPrecarga08.Trim();
            acquisition.FechaCitaTransferencia = transfer.FechaCitaTransferencia.Value.Date;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), TipoEvento.AcquisitionTransferScheduled, acquisition.Compra_Id);
        }

        // CUF10 paso 19: despues de la cita, con el 08 firmado y la baja municipal cargados.
        public SQLUpdateResult RegisterTransfer(Guid Acquisition_Id)
        {
            Compra acquisition = GetAcquisitionInStatus(Acquisition_Id, CompraStatus.Paid);
            if (acquisition.CodigoPrecarga08 == null || acquisition.FechaCitaTransferencia == null)
                throw new TranslatableException("TransferNotScheduled", "Guarde el codigo de precarga y la fecha de la cita para continuar.");
            if (acquisition.FechaCitaTransferencia.Value.Date > DateTime.Today)
                throw new TranslatableException("TransferAppointmentPending", "La cita en el registro automotor todavia no ocurrio.");
            if (!IsStageComplete(Acquisition_Id, DocumentoCompraStage.Transfer))
                throw new TranslatableException("TransferDocumentsMissing", "Adjunte el formulario 08 firmado y el comprobante de baja municipal para continuar.");

            acquisition.Estado = CompraStatus.Transferred;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), TipoEvento.AcquisitionTransferred, acquisition.Compra_Id);
        }

        public CompraSummary GetSummary(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetSummary(Acquisition_Id);
        }

        // CUF10 pasos 21 a 24: el vehiculo entra al inventario disponible para CUF01 con lo verificado en la
        // inspeccion. Es el unico paso en una transaccion: si quedara solo el vehiculo, se venderia con la compra abierta.
        public SQLUpdateResult FinishAcquisition(Guid Acquisition_Id, Vehiculo sale)
        {
            if (sale.Precio <= 0)
                throw new TranslatableException("PleaseWriteValidSalePrice", "Escriba un precio de venta mayor a cero para continuar.");

            Compra acquisition = GetAcquisitionInStatus(Acquisition_Id, CompraStatus.Transferred);
            CompraSummary summary = GetSummary(Acquisition_Id);
            Vehiculo vehicle = new Vehiculo();
            vehicle.Vehiculo_Id = Guid.NewGuid();
            vehicle.Patente = summary.Patente;
            vehicle.Marca_Id = summary.Marca_Id;
            vehicle.Modelo_Id = summary.Modelo_Id;
            vehicle.Version_Id = summary.Version_Id;
            vehicle.Color_Id = summary.Color_Id;
            vehicle.Precio = sale.Precio;
            vehicle.Opcionales = sale.Opcionales == null ? null : sale.Opcionales.Trim();
            vehicle.Observaciones = sale.Observaciones == null ? null : sale.Observaciones.Trim();
            vehicle.Puertas = summary.Puertas;
            vehicle.Anio = summary.Anio;
            vehicle.Kilometraje = summary.KilometrajeVerificado ?? summary.Kilometraje;
            vehicle.LinkImagen = null;
            acquisition.Estado = CompraStatus.Finished;
            acquisition.Vehiculo_Id = vehicle.Vehiculo_Id;
            acquisition.FechaCierre = DateTime.Now;

            SQLUpdateResult result = RecordResult(_acquisitionmapper.FinishAcquisition(vehicle, acquisition), TipoEvento.VehicleCreated, vehicle.Vehiculo_Id);
            return RecordResult(result, TipoEvento.AcquisitionFinished, acquisition.Compra_Id);
        }

        // CUF11 paso 2: compras de la persona con la documentacion completa y las que ya tienen comprobante.
        public List<PayableCompra> GetPayableAcquisitions(Guid Person_Id)
        {
            return _acquisitionmapper.GetPendingPayment(Person_Id);
        }
        public List<PagoCompra> GetPayments(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetPayments(Acquisition_Id);
        }
        public ComprobanteCompra GetPurchaseReceipt(Guid Acquisition_Id)
        {
            return _acquisitionmapper.GetReceipt(Acquisition_Id);
        }
        // Saldo de un medio: lo pactado menos lo pagado, redondeado a centavos como la forma de pago.
        public decimal GetPaymentTermBalance(FormaPagoCompra term)
        {
            return (decimal)Math.Round(term.Monto, 2) - (decimal)Math.Round(term.MontoPagado, 2);
        }
        public decimal GetBalance(List<FormaPagoCompra> terms)
        {
            return terms == null ? 0 : terms.Sum(t => GetPaymentTermBalance(t));
        }

        // CUF11 pasos 3 a 5: el pago es con un medio pactado y no puede superar su saldo (4.1). Cada medio
        // guarda solo sus datos: la transferencia banco, CBU o alias y operacion; el cheque banco, numero y fecha.
        public SQLUpdateResult AddPayment(PagoCompra payment)
        {
            GetAcquisitionInStatus(payment.Compra_Id, CompraStatus.DocumentationComplete);
            List<FormaPagoCompra> terms = GetPaymentTerms(payment.Compra_Id);
            FormaPagoCompra term = terms == null ? null : terms.FirstOrDefault(t => t.TipoPago_Id == payment.TipoPago_Id);
            if (term == null)
                throw new TranslatableException("PaymentTypeNotAgreed", "El medio de pago no esta en la forma de pago pactada con la persona.");
            if (payment.Monto <= 0)
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            decimal balance = GetPaymentTermBalance(term);
            if ((decimal)Math.Round(payment.Monto, 2) > balance)
                throw new TranslatableException("PaymentExceedsBalance", "El monto supera el saldo del medio pactado: $ {0}.", balance.ToString("N2"));

            payment.Monto = Math.Round(payment.Monto, 2);
            NormalizePaymentDetails(payment, term.TipoPago_Descripcion);
            payment.Cajero_Id = SessionHandler.Instance.GetId();
            payment.FechaCreacion = DateTime.Now;
            return RecordResult(_acquisitionmapper.AddPayment(payment), TipoEvento.AcquisitionPaymentCreated, payment.PagoCompra_Id);
        }

        // CUF11 pasos 7 y 9: con el saldo en cero se emite el comprobante y la compra pasa a Pagada, en una sola
        // transaccion. Si fallara despues del ultimo pago, la compra sigue en Documentacion completa y se reintenta.
        public SQLUpdateResult IssuePurchaseReceipt(Guid Acquisition_Id)
        {
            Compra acquisition = GetAcquisitionInStatus(Acquisition_Id, CompraStatus.DocumentationComplete);
            decimal balance = GetBalance(GetPaymentTerms(Acquisition_Id));
            if (balance != 0)
                throw new TranslatableException("PurchaseBalancePending", "La compra todavia tiene saldo pendiente: $ {0}.", balance.ToString("N2"));

            CompraSummary summary = GetSummary(Acquisition_Id);
            Persona person = new PersonaBLL().GetById(acquisition.Persona_Id);
            ComprobanteCompra receipt = new ComprobanteCompra();
            receipt.ComprobanteCompra_Id = Guid.NewGuid();
            receipt.Numero = _acquisitionmapper.GetNextReceiptNumber();
            receipt.Compra_Id = acquisition.Compra_Id;
            receipt.Fecha = DateTime.Now;
            receipt.NombrePersona = person.Nombre + " " + person.Apellido;
            receipt.CuilCuit = acquisition.CuilCuitTitular;
            receipt.Detalle = summary.Vehiculo_Descripcion + " - " + summary.Patente + " - " + summary.NumeroChasis + " - " + summary.NumeroMotor;
            // La compra a un particular no lleva impuestos: el total es el precio acordado.
            receipt.MontoNeto = Math.Round(acquisition.PrecioAcordado.Value, 2);
            receipt.MontoImpuesto = 0;
            receipt.MontoTotal = receipt.MontoNeto + receipt.MontoImpuesto;
            acquisition.Estado = CompraStatus.Paid;

            return RecordResult(_acquisitionmapper.PayAcquisition(receipt, acquisition), TipoEvento.PurchaseReceiptIssued, acquisition.Compra_Id);
        }

        // Etapa A: Tasada y con la tasacion vigente (3.1).
        private Compra GetAppraisedAcquisition(Guid Acquisition_Id)
        {
            Compra acquisition = GetAcquisitionInStatus(Acquisition_Id, CompraStatus.Appraised);
            if (IsAppraisalExpired(GetLastAppraisal(Acquisition_Id)))
                throw new TranslatableException("AppraisalExpired", "La tasacion vencio. Vuelva a tasar el vehiculo desde Compras > Tasar vehiculo.");
            return acquisition;
        }

        // Los documentos de la etapa B se cargan con la compra formalizada y los de la etapa C con la compra pagada.
        private Compra GetAcquisitionInStage(Guid Acquisition_Id, DocumentoCompraStage stage)
        {
            return GetAcquisitionInStatus(Acquisition_Id,
                stage == DocumentoCompraStage.Document ? CompraStatus.PurchaseFormalized : CompraStatus.Paid);
        }

        private Compra GetAcquisitionInStatus(Guid Acquisition_Id, CompraStatus status)
        {
            Compra acquisition = _acquisitionmapper.GetById(Acquisition_Id);
            if (acquisition == null || acquisition.Estado != status)
                throw new TranslatableException("AcquisitionNotInStage", "La adquisicion ya no esta en esta etapa. Vuelva a buscarla.");
            return acquisition;
        }

        // CUF10 pasos 5 y 6: medios permitidos, una vez cada uno, con monto, y la suma igual al precio (6.1).
        private void ValidatePaymentTerms(double price, List<FormaPagoCompra> terms)
        {
            if (terms == null || terms.Count == 0)
                throw new TranslatableException("PleaseAddPaymentTerm", "Agregue al menos un medio de pago para continuar.");
            List<Guid> allowed = GetAcquisitionPaymentTypes().Select(t => t.TipoPago_Id).ToList();
            if (terms.Any(t => !allowed.Contains(t.TipoPago_Id)))
                throw new TranslatableException("InvalidAcquisitionPaymentType", "La compra solo se paga con transferencia, cheque o efectivo.");
            if (terms.GroupBy(t => t.TipoPago_Id).Any(g => g.Count() > 1))
                throw new TranslatableException("PaymentTypeAlreadyAdded", "El medio de pago ya esta en la forma de pago. Quitelo para cambiar el monto.");
            if (terms.Any(t => t.Monto <= 0))
                throw new TranslatableException("PleaseWriteValidPaymentAmount", "Escriba un importe de pago valido para continuar.");
            decimal difference = GetPaymentTermsDifference(price, terms);
            if (difference != 0)
                throw new TranslatableException("PaymentTermsDifference", "La forma de pago no coincide con el precio acordado. Diferencia: $ {0}.", difference.ToString("N2"));
        }

        // CUF11 paso 4: datos obligatorios de cada medio. Los del resto quedan en NULL.
        private static void NormalizePaymentDetails(PagoCompra payment, string paymentType)
        {
            string bank = payment.Banco == null ? "" : payment.Banco.Trim();
            string cbu_alias = payment.CbuAlias == null ? "" : payment.CbuAlias.Trim();
            string operation_number = payment.NumeroOperacion == null ? "" : payment.NumeroOperacion.Trim();
            string check_number = payment.NumeroCheque == null ? "" : payment.NumeroCheque.Trim();
            DateTime? payment_date = payment.FechaPago;
            payment.Banco = null;
            payment.CbuAlias = null;
            payment.NumeroOperacion = null;
            payment.NumeroCheque = null;
            payment.FechaPago = null;

            if (paymentType == BankTransferPaymentType)
            {
                if (bank == "")
                    throw new TranslatableException("PleaseWriteBank", "Escriba el banco para continuar.");
                if (!CbuFormat.IsMatch(cbu_alias) && !(AliasFormat.IsMatch(cbu_alias) && !cbu_alias.All(char.IsDigit)))
                    throw new TranslatableException("PleaseWriteValidCbuAlias", "Escriba un CBU de 22 digitos o un alias de 6 a 20 caracteres (letras, numeros, punto o guion).");
                if (operation_number == "")
                    throw new TranslatableException("PleaseWriteOperationNumber", "Escriba el numero de operacion de la transferencia para continuar.");
                payment.Banco = bank;
                payment.CbuAlias = cbu_alias;
                payment.NumeroOperacion = operation_number;
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
                payment.Banco = bank;
                payment.NumeroCheque = check_number;
                payment.FechaPago = payment_date.Value.Date;
            }
        }

        private static void ValidateDocumentFile(ArchivoDocumentoCompra file)
        {
            if (file == null || file.Contenido == null || file.Contenido.Length == 0)
                throw new TranslatableException("PleaseSelectDocumentFile", "Adjunte el archivo escaneado del documento para continuar.");
            if (!DocumentExtensions.Contains(Path.GetExtension(file.NombreArchivo).ToLowerInvariant()))
                throw new TranslatableException("InvalidDocumentFormat", "Los documentos tienen que ser archivos pdf.");
        }

        // CUF10 paso 12: emision no futura y, si el tipo vence, dentro de su vigencia.
        private static void ValidateDocumentValidity(TipoDocumentoCompra type, DateTime Issue_Date)
        {
            if (Issue_Date.Date > DateTime.Today)
                throw new TranslatableException("DocumentIssueDateInFuture", "La fecha de emision del documento no puede ser futura.");
            if (type.DiasVigencia != null && Issue_Date.Date.AddDays(type.DiasVigencia.Value) < DateTime.Today)
                throw new TranslatableException("DocumentExpired", "El documento esta vencido: tiene una vigencia de {0} dias desde su emision.", type.DiasVigencia.Value);
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

        // Patente, chasis y motor se comparan contra el inventario: se guardan sin espacios y en mayusculas.
        private static void NormalizeIdentification(VehiculoProspecto vehicle)
        {
            vehicle.Patente = vehicle.Patente.Trim().ToUpperInvariant();
            vehicle.NumeroChasis = vehicle.NumeroChasis.Trim().ToUpperInvariant();
            vehicle.NumeroMotor = vehicle.NumeroMotor.Trim().ToUpperInvariant();
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
