using CarAgency.Security.BitacoraEventos;
using CarAgency.BE.BitacoraEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAgency.Mappers;
using CarAgency.BE;
using CarAgency.Security.Security;
using CarAgency.Security.Session;

namespace CarAgency.BLL
{
    public class CotizacionBLL
    {
        private CotizacionMapper _quotationmapper;
        public CotizacionBLL()
        {
            _quotationmapper = new CotizacionMapper();
        }
        public Cotizacion GetById(Guid id)
        {
            return _quotationmapper.GetById(id);
        }
        public List<Cotizacion> GetAll()
        {
            return _quotationmapper.GetAll();
        }
        public List<Cotizacion> GetAllActiveByPerson(Guid person_Id)
        {
            return _quotationmapper.GetAllActiveByPerson(person_Id);
        }

        public SQLUpdateResult AddQuotation(Cotizacion quotation)
        {
            SQLUpdateResult result = _quotationmapper.AddQuotation(quotation);
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(TipoEvento.QuotationCreated, quotation.Cotizacion_Id);
            return result;
        }
    }
}
