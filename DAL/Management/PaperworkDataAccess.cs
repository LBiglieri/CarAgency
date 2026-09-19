using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Management
{
    // Tramites de gestoria y sus archivos adjuntos. PaperworkFile no esta protegida con digitos verificadores.
    public sealed class PaperworkDataAccess : DataAccessBase
    {
        public DataTable GetById(Guid id) { return Read("Paperwork_GetById", new SqlParameter("@Id", id)); }
        public DataTable GetAllActiveByClient(Guid clientId) { return Read("Paperwork_GetAllActiveByClient", new SqlParameter("@Client_Id", clientId)); }

        public DataTable Add(Guid id, Guid vehicleId, Guid clientId, Guid invoiceId, string prechargeCode, DateTime transferDate,
            string observations, bool isFinished, DvhCalculator dvh)
        {
            return Read("Paperwork_Add", dvh, PaperworkParameters(id, vehicleId, clientId, invoiceId, prechargeCode, transferDate,
                observations, isFinished));
        }

        public DataTable Update(Guid id, Guid vehicleId, Guid clientId, Guid invoiceId, string prechargeCode, DateTime transferDate,
            string observations, bool isFinished, DvhCalculator dvh)
        {
            return Read("Paperwork_Update", dvh, PaperworkParameters(id, vehicleId, clientId, invoiceId, prechargeCode, transferDate,
                observations, isFinished));
        }

        public DataTable Delete(Guid id) { return Read("Paperwork_Delete", new SqlParameter("@Id", id)); }

        public DataTable GetFilesByPaperwork(Guid paperworkId)
        {
            return Read("PaperworkFile_GetByPaperWork", new SqlParameter("@Paperwork_Id", paperworkId));
        }

        public DataTable AddFile(Guid id, Guid paperworkId, string fileName, byte[] fileContent, DateTime uploadedDate)
        {
            return Read("PaperworkFile_Add",
                new SqlParameter("@Id", id),
                new SqlParameter("@Paperwork_Id", paperworkId),
                new SqlParameter("@FileName", fileName),
                new SqlParameter("@FileContent", fileContent),
                new SqlParameter("@UploadedDate", uploadedDate));
        }

        public DataTable DeleteFile(Guid id) { return Read("PaperworkFile_Delete", new SqlParameter("@Id", id)); }

        private static SqlParameter[] PaperworkParameters(Guid id, Guid vehicleId, Guid clientId, Guid invoiceId, string prechargeCode,
            DateTime transferDate, string observations, bool isFinished)
        {
            return new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Vehicle_Id", vehicleId),
                new SqlParameter("@Client_Id", clientId),
                new SqlParameter("@Invoice_Id", invoiceId),
                new SqlParameter("@Paperwork_Precharge_Code", prechargeCode),
                new SqlParameter("@Transfer_Date", transferDate),
                new SqlParameter("@Observations", observations),
                new SqlParameter("@IsFinished", isFinished)
            };
        }
    }
}
