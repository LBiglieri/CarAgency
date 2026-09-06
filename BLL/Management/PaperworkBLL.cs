using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Mappers.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using BLL;

namespace CarAgency.BLL
{
    public class PaperworkBLL
    {
        private PaperworkMapper _paperworkmapper;
        public PaperworkBLL()
        {
            _paperworkmapper = new PaperworkMapper();
        }
        public Paperwork GetById(Guid id)
        {
            return _paperworkmapper.GetById(id);
        }
        public List<Paperwork> GetAllActiveByClient(Guid Client_Id)
        {
            List<Paperwork> paperworks = _paperworkmapper.GetAllActiveByClient(Client_Id);
            if (paperworks == null)
                return null;

            ClientsBLL clientsBLL = new ClientsBLL();
            foreach (Paperwork paperwork in paperworks)
            {
                Client client = clientsBLL.GetById(paperwork.Client_Id);
                paperwork.Client_Description = "DNI: " + client.Dni.ToString() + " Full Name: " + client.Name + client.Surname + " Email: " + client.Email;
            }

            return paperworks;
        }
        public SQLUpdateResult AddPaperwork(Paperwork paperwork)
        {
            return _paperworkmapper.AddPaperwork(paperwork);
        }
        public SQLUpdateResult UpdatePaperwork(Paperwork paperwork)
        {
            return _paperworkmapper.UpdatePaperwork(paperwork);
        }
        public SQLUpdateResult DeletePaperwork(Paperwork paperwork)
        {
            return _paperworkmapper.DeletePaperwork(paperwork);
        }
        public List<PaperworkFile> GetFilesByPaperWork(Guid Paperwork_Id)
        {
            return _paperworkmapper.GetFilesByPaperWork(Paperwork_Id);
        }
        public SQLUpdateResult AddPaperworkFile(PaperworkFile paperwork)
        {
            return _paperworkmapper.AddPaperworkFile(paperwork);
        }
        public SQLUpdateResult DeletePaperworkFile(PaperworkFile paperwork)
        {
            return _paperworkmapper.DeletePaperworkFile(paperwork);
        }
    }
}
