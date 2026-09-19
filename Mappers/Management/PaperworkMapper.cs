using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Management;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class PaperworkMapper
    {
        private const string Table = "Paperwork";
        private readonly PaperworkDataAccess data = new PaperworkDataAccess();
        private readonly ClientMapper clients = new ClientMapper();

        // Sin archivos devuelve null.
        public List<PaperworkFile> GetFilesByPaperWork(Guid Paperwork_Id)
        {
            List<PaperworkFile> files = MappingHandler.MapTableToEntities<PaperworkFile>(data.GetFilesByPaperwork(Paperwork_Id));
            return files.Count == 0 ? null : files;
        }

        // PaperworkFile no esta protegida con digitos verificadores.
        public SQLUpdateResult AddPaperworkFile(PaperworkFile paperwork)
        {
            return MappingHandler.MapUpdateResult(data.AddFile(paperwork.Id, paperwork.Paperwork_Id, paperwork.FileName,
                paperwork.FileContent, paperwork.UploadedDate));
        }

        public SQLUpdateResult DeletePaperworkFile(PaperworkFile paperwork)
        {
            return MappingHandler.MapUpdateResult(data.DeleteFile(paperwork.Id));
        }

        public Paperwork GetById(Guid id)
        {
            return MappingHandler.MapTableToEntities<Paperwork>(data.GetById(id)).FirstOrDefault();
        }

        // Sin tramites devuelve null. Todos son del mismo cliente: su descripcion se pide una sola vez.
        public List<Paperwork> GetAllActiveByClient(Guid Client_Id)
        {
            List<Paperwork> paperworks = MappingHandler.MapTableToEntities<Paperwork>(data.GetAllActiveByClient(Client_Id));
            if (paperworks.Count == 0) return null;
            string clientDescription = clients.GetDescription(Client_Id);
            foreach (Paperwork paperwork in paperworks)
                paperwork.Client_Description = clientDescription;
            return paperworks;
        }

        public SQLUpdateResult AddPaperwork(Paperwork paperwork)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(paperwork.Id, paperwork.Vehicle_Id, paperwork.Client_Id, paperwork.Invoice_Id,
                paperwork.Paperwork_Precharge_Code, paperwork.Transfer_Date, paperwork.Observations, paperwork.IsFinished, dvh));
        }

        public SQLUpdateResult UpdatePaperwork(Paperwork paperwork)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Update(paperwork.Id, paperwork.Vehicle_Id, paperwork.Client_Id, paperwork.Invoice_Id,
                paperwork.Paperwork_Precharge_Code, paperwork.Transfer_Date, paperwork.Observations, paperwork.IsFinished, dvh));
        }

        public SQLUpdateResult DeletePaperwork(Paperwork paperwork)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(paperwork.Id), Table);
        }
    }
}
