using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Repository;
using CarAgency.Entities;
using CarAgency.Utilities.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;

namespace CarAgency.BLL
{
    public class QuotationBLL
    {
        private QuotationRepository _quotationrepository;
        public QuotationBLL()
        {
            _quotationrepository = new QuotationRepository();
        }
        public Quotation GetById(Guid id)
        {
            return _quotationrepository.GetById(id);
        }
        public List<Quotation> GetAll()
        {
            return _quotationrepository.GetAll();
        }
        public List<Quotation> GetAllActiveByClient(Guid client_Id)
        {
            return _quotationrepository.GetAllActiveByClient(client_Id);
        }

        public SQLUpdateResult AddQuotation(Quotation quotation)
        {
            return _quotationrepository.AddQuotation(quotation);
        }
    }
}
