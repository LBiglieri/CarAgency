using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Compras
{
    // Tasacion del vehiculo inspeccionado (PN2).
    public sealed class TasacionDataAccess : DataAccessBase
    {
        public DataTable GetPendingAppraisals(string filter, DateTime now)
        {
            return Read("Compra_GetPendingAppraisal",
                new SqlParameter("@Filter", (object)filter ?? DBNull.Value),
                new SqlParameter("@Now", now));
        }

        public DataTable GetLastAppraisal(Guid acquisitionId)
        {
            return Read("Tasacion_GetLastByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable AddAppraisal(Guid id, Guid acquisitionId, Guid inspectionId, Guid userId, double amount, string justification,
            DateTime creationDate, DateTime expirationDate, DvhCalculator dvh)
        {
            return Read("Tasacion_Add", dvh,
                new SqlParameter("@Tasacion_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@Inspeccion_Id", inspectionId),
                new SqlParameter("@Tasador_Id", userId),
                new SqlParameter("@Valor", amount),
                new SqlParameter("@Justificacion", justification),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@Vigencia", expirationDate));
        }
    }
}
