using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Session;
using BE;

namespace CarAgency.BLL
{
    // Raiz de la compra de vehiculos a personas (PN2): alta (CUF07), acuerdo, contrato, transferencia y cierre (CUF10).
    // La inspeccion, la tasacion, los documentos y los pagos viven en su propia BLL; esta las usa y ellas no la usan a ella.
    public class CompraBLL
    {
        private CompraMapper _acquisitionmapper;
        private InspeccionBLL _inspectionbll;
        private TasacionBLL _appraisalbll;
        private DocumentoCompraBLL _documentbll;
        private PagoCompraBLL _purchasepaymentbll;
        public CompraBLL()
        {
            _acquisitionmapper = new CompraMapper();
            _inspectionbll = new InspeccionBLL();
            _appraisalbll = new TasacionBLL();
            _documentbll = new DocumentoCompraBLL();
            _purchasepaymentbll = new PagoCompraBLL();
        }
        public List<DuplicateVehiculo> GetDuplicates(VehiculoProspecto vehicle)
        {
            NormalizeIdentification(vehicle);
            return _acquisitionmapper.GetDuplicates(vehicle);
        }

        // Vehiculo, adquisicion e inspeccion se graban uno detras del otro, como el resto de las altas.
        public SQLUpdateResult AddAcquisition(Compra acquisition)
        {
            VehiculoProspecto vehicle = acquisition.VehiculoProspecto;
            Inspeccion inspection = acquisition.Inspeccion;

            if (GetDuplicates(vehicle) != null)
                throw new TranslatableException("VehicleAlreadyRegistered", "Ya existe un vehiculo en inventario o una adquisicion abierta con la misma patente, chasis o motor.");
            if (!_inspectionbll.IsMechanicAvailable(inspection.Mecanico_Id, inspection.FechaProgramada))
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
                result = _inspectionbll.AddInspection(inspection);
            return RecordResult(result, TipoEvento.AcquisitionCreated, acquisition.Compra_Id);
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
        // El acuerdo es valido solo si se hizo sobre la ultima tasacion: si se re-taso, hay que volver a pactarlo.
        public bool HasAgreement(Compra acquisition)
        {
            Tasacion last = _appraisalbll.GetLastAppraisal(acquisition.Compra_Id);
            return acquisition.PrecioAcordado != null && last != null && acquisition.TasacionAceptada_Id == last.Tasacion_Id;
        }

        // CUF10 pasos 5 a 7: el acuerdo se registra al generar el contrato y la adquisicion sigue Tasada hasta la firma.
        // Mientras no se firme se puede volver a generar: la forma de pago se reemplaza completa.
        public SQLUpdateResult RegisterAgreement(Compra agreement, List<FormaPagoCompra> terms)
        {
            if (agreement.PrecioAcordado == null || agreement.PrecioAcordado <= 0)
                throw new TranslatableException("PleaseWriteValidAgreedPrice", "Escriba un precio acordado mayor a cero para continuar.");
            if (string.IsNullOrWhiteSpace(agreement.CuilCuitTitular))
                throw new TranslatableException("PleaseWriteOwnerCuilCuit", "Escriba el CUIL/CUIT del titular del vehiculo para continuar.");
            _purchasepaymentbll.ValidatePaymentTerms(agreement.PrecioAcordado.Value, terms);

            Compra acquisition = GetAppraisedAcquisition(agreement.Compra_Id);
            acquisition.TasacionAceptada_Id = _appraisalbll.GetLastAppraisal(acquisition.Compra_Id).Tasacion_Id;
            acquisition.PrecioAcordado = agreement.PrecioAcordado;
            acquisition.CuilCuitTitular = agreement.CuilCuitTitular.Trim();

            SQLUpdateResult result = _acquisitionmapper.UpdateAcquisition(acquisition);
            // Sin evento: la formalizacion se registra con la firma del contrato.
            return _purchasepaymentbll.ReplacePaymentTerms(acquisition.Compra_Id, terms, result);
        }

        // CUF10 pasos 8 y 9: con el contrato firmado el precio queda bloqueado.
        public SQLUpdateResult RegisterContractSigned(Guid Acquisition_Id)
        {
            Compra acquisition = GetAppraisedAcquisition(Acquisition_Id);
            if (!HasAgreement(acquisition))
                throw new TranslatableException("ContractNotGenerated", "Genere el contrato de compraventa antes de registrar la firma.");
            _purchasepaymentbll.ValidatePaymentTerms(acquisition.PrecioAcordado.Value, _purchasepaymentbll.GetPaymentTerms(Acquisition_Id));

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

        // CUF10 paso 17 y 18.1: la cita queda agendada y la adquisicion sigue Pagada.
        public SQLUpdateResult ScheduleTransfer(Compra transfer)
        {
            if (string.IsNullOrWhiteSpace(transfer.CodigoPrecarga08))
                throw new TranslatableException("PleaseWrite08", "Escriba el codigo de precarga del formulario 08 para continuar.");
            if (transfer.FechaCitaTransferencia == null)
                throw new TranslatableException("PleaseSelectTransferAppointment", "Seleccione la fecha de la cita en el registro automotor para continuar.");

            Compra acquisition = CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, transfer.Compra_Id, CompraStatus.Paid);
            acquisition.CodigoPrecarga08 = transfer.CodigoPrecarga08.Trim();
            acquisition.FechaCitaTransferencia = transfer.FechaCitaTransferencia.Value.Date;
            return RecordResult(_acquisitionmapper.UpdateAcquisition(acquisition), TipoEvento.AcquisitionTransferScheduled, acquisition.Compra_Id);
        }

        // CUF10 paso 19: despues de la cita, con el 08 firmado y la baja municipal cargados.
        public SQLUpdateResult RegisterTransfer(Guid Acquisition_Id)
        {
            Compra acquisition = CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, Acquisition_Id, CompraStatus.Paid);
            if (acquisition.CodigoPrecarga08 == null || acquisition.FechaCitaTransferencia == null)
                throw new TranslatableException("TransferNotScheduled", "Guarde el codigo de precarga y la fecha de la cita para continuar.");
            if (acquisition.FechaCitaTransferencia.Value.Date > DateTime.Today)
                throw new TranslatableException("TransferAppointmentPending", "La cita en el registro automotor todavia no ocurrio.");
            if (!_documentbll.IsStageComplete(Acquisition_Id, DocumentoCompraStage.Transfer))
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

            Compra acquisition = CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, Acquisition_Id, CompraStatus.Transferred);
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

        // Etapa A: Tasada y con la tasacion vigente (3.1).
        private Compra GetAppraisedAcquisition(Guid Acquisition_Id)
        {
            Compra acquisition = CompraStatusGuard.GetAcquisitionInStatus(_acquisitionmapper, Acquisition_Id, CompraStatus.Appraised);
            if (_appraisalbll.IsAppraisalExpired(_appraisalbll.GetLastAppraisal(Acquisition_Id)))
                throw new TranslatableException("AppraisalExpired", "La tasacion vencio. Vuelva a tasar el vehiculo desde Compras > Tasar vehiculo.");
            return acquisition;
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
