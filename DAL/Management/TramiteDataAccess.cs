using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Management
{
    // Tramites de gestoria y sus archivos adjuntos. PaperworkFile no esta protegida con digitos verificadores.
    public sealed class TramiteDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Tramite_GetById", new SqlParameter("@Tramite_Id", id)); }
        public DataTable GetAllActiveByPerson(Guid personId) { return Read("Tramite_GetAllActiveByPerson", new SqlParameter("@Persona_Id", personId)); }

        public DataTable Add(Guid id, Guid vehicleId, Guid personId, Guid invoiceId, string prechargeCode, DateTime transferDate,
            string observations, bool isFinished, DvhCalculator dvh)
        {
            return Read("Tramite_Add", dvh, PaperworkParameters(id, vehicleId, personId, invoiceId, prechargeCode, transferDate,
                observations, isFinished));
        }

        public DataTable Update(Guid id, Guid vehicleId, Guid personId, Guid invoiceId, string prechargeCode, DateTime transferDate,
            string observations, bool isFinished, DvhCalculator dvh)
        {
            return Read("Tramite_Update", dvh, PaperworkParameters(id, vehicleId, personId, invoiceId, prechargeCode, transferDate,
                observations, isFinished));
        }

        public DataTable Delete(Guid id) { return Read("Tramite_Delete", new SqlParameter("@Tramite_Id", id)); }

        public DataTable GetFilesByPaperwork(Guid paperworkId)
        {
            return Read("ArchivoTramite_GetByPaperWork", new SqlParameter("@Tramite_Id", paperworkId));
        }

        public DataTable AddFile(Guid id, Guid paperworkId, string fileName, byte[] fileContent, DateTime uploadedDate)
        {
            return Read("ArchivoTramite_Add",
                new SqlParameter("@ArchivoTramite_Id", id),
                new SqlParameter("@Tramite_Id", paperworkId),
                new SqlParameter("@NombreArchivo", fileName),
                new SqlParameter("@Contenido", fileContent),
                new SqlParameter("@FechaCarga", uploadedDate));
        }

        public DataTable DeleteFile(Guid id) { return Read("ArchivoTramite_Delete", new SqlParameter("@ArchivoTramite_Id", id)); }

        private static SqlParameter[] PaperworkParameters(Guid id, Guid vehicleId, Guid personId, Guid invoiceId, string prechargeCode,
            DateTime transferDate, string observations, bool isFinished)
        {
            return new[]
            {
                new SqlParameter("@Tramite_Id", id),
                new SqlParameter("@Vehiculo_Id", vehicleId),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@Factura_Id", invoiceId),
                new SqlParameter("@CodigoPrecarga08", prechargeCode),
                new SqlParameter("@FechaTransferencia", transferDate),
                new SqlParameter("@Observaciones", observations),
                new SqlParameter("@Finalizado", isFinished)
            };
        }
    }
}
