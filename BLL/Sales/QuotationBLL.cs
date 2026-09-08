using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
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

namespace CarAgency.BLL
{
    public class QuotationBLL
    {
        private QuotationMapper _quotationmapper;
        public QuotationBLL()
        {
            _quotationmapper = new QuotationMapper();
        }
        public Quotation GetById(Guid id)
        {
            return _quotationmapper.GetById(id);
        }
        public List<Quotation> GetAll()
        {
            return _quotationmapper.GetAll();
        }
        public List<Quotation> GetAllActiveByClient(Guid client_Id)
        {
            return _quotationmapper.GetAllActiveByClient(client_Id);
        }

        public SQLUpdateResult AddQuotation(Quotation quotation)
        {
            SQLUpdateResult result = _quotationmapper.AddQuotation(quotation);
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(AuditEventType.QuotationCreated, quotation.Id);
            return result;
        }
    }
}
