using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Compras;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class TasacionMapper
    {
        private readonly TasacionDataAccess data = new TasacionDataAccess();

        // Sin adquisiciones para tasar devuelve null.
        public List<PendingTasacion> GetPendingAppraisals(string filter, DateTime now)
        {
            List<PendingTasacion> appraisals = MappingHandler.MapTableToEntities<PendingTasacion>(data.GetPendingAppraisals(filter, now));
            return appraisals.Count == 0 ? null : appraisals;
        }

        // Sin tasaciones previas devuelve null.
        public Tasacion GetLastAppraisal(Guid Acquisition_Id)
        {
            return MappingHandler.MapTableToEntities<Tasacion>(data.GetLastAppraisal(Acquisition_Id)).FirstOrDefault();
        }

        public SQLUpdateResult AddAppraisal(Tasacion appraisal)
        {
            return DigitVerifierWriteMapper.Save("Tasacion", dvh => data.AddAppraisal(appraisal.Tasacion_Id, appraisal.Compra_Id,
                appraisal.Inspeccion_Id, appraisal.Tasador_Id, appraisal.Valor, appraisal.Justificacion, appraisal.Fecha,
                appraisal.Vigencia, dvh));
        }
    }
}
