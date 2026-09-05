using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.DAL;
using CarAgency.BE;
using CarAgency.DAL.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using BLL;

namespace CarAgency.BLL
{
    public class PaperworkBLL
    {
        private PaperworkRepository _paperworkrepository;
        public PaperworkBLL()
        {
            _paperworkrepository = new PaperworkRepository();
        }
        public Paperwork GetById(Guid id)
        {
            return _paperworkrepository.GetById(id);
        }
        public List<Paperwork> GetAllActiveByClient(Guid Client_Id)
        {
            List<Paperwork> paperworks = _paperworkrepository.GetAllActiveByClient(Client_Id);
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
            return _paperworkrepository.AddPaperwork(paperwork);
        }
        public SQLUpdateResult UpdatePaperwork(Paperwork paperwork)
        {
            return _paperworkrepository.UpdatePaperwork(paperwork);
        }
        public SQLUpdateResult DeletePaperwork(Paperwork paperwork)
        {
            return _paperworkrepository.DeletePaperwork(paperwork);
        }
        public List<PaperworkFile> GetFilesByPaperWork(Guid Paperwork_Id)
        {
            return _paperworkrepository.GetFilesByPaperWork(Paperwork_Id);
        }
        public SQLUpdateResult AddPaperworkFile(PaperworkFile paperwork)
        {
            return _paperworkrepository.AddPaperworkFile(paperwork);
        }
        public SQLUpdateResult DeletePaperworkFile(PaperworkFile paperwork)
        {
            return _paperworkrepository.DeletePaperworkFile(paperwork);
        }
    }
}
