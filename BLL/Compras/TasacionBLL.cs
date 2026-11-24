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
    // Tasacion del vehiculo inspeccionado (CUF09) y su vigencia, que tambien controla la gestion de la compra (CUF10).
    public class TasacionBLL
    {
        // Vigencia de la tasacion en dias corridos (CUF09 paso 6). Vencida, se puede volver a tasar.
        public const int AppraisalValidityDays = 7;

        private TasacionMapper _appraisalmapper;
        private CompraMapper _acquisitionmapper;
        public TasacionBLL()
        {
            _appraisalmapper = new TasacionMapper();
            _acquisitionmapper = new CompraMapper();
        }

        // La matricula se guarda en mayusculas; el DNI se compara tal cual.
        public List<PendingTasacion> GetPendingAppraisals(string filter)
        {
            return _appraisalmapper.GetPendingAppraisals(filter == null ? null : filter.Trim().ToUpperInvariant(), DateTime.Now);
        }
        public Tasacion GetLastAppraisal(Guid Acquisition_Id)
        {
            return _appraisalmapper.GetLastAppraisal(Acquisition_Id);
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

            SQLUpdateResult result = _appraisalmapper.AddAppraisal(appraisal);
            if (result != null && result.sqlResult == SQLResultType.success)
                result = _acquisitionmapper.UpdateAcquisition(acquisition);
            return RecordResult(result, TipoEvento.AppraisalCreated, acquisition.Compra_Id);
        }

        // CUF10 3.1: vencida la tasacion no se puede seguir, hay que volver a tasar con CUF09.
        public bool IsAppraisalExpired(Tasacion appraisal)
        {
            return appraisal == null || appraisal.Vigencia < DateTime.Now;
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

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
