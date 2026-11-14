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
using BLL;

namespace CarAgency.BLL
{
    public class TramiteBLL
    {
        private TramiteMapper _paperworkmapper;
        public TramiteBLL()
        {
            _paperworkmapper = new TramiteMapper();
        }
        public Tramite GetById(Guid id)
        {
            return _paperworkmapper.GetById(id);
        }
        public List<Tramite> GetAllActiveByPerson(Guid Person_Id)
        {
            return _paperworkmapper.GetAllActiveByPerson(Person_Id);
        }
        public SQLUpdateResult AddPaperwork(Tramite paperwork)
        {
            return RecordResult(_paperworkmapper.AddPaperwork(paperwork), TipoEvento.PaperworkCreated, paperwork.Tramite_Id);
        }
        public SQLUpdateResult UpdatePaperwork(Tramite paperwork)
        {
            return RecordResult(_paperworkmapper.UpdatePaperwork(paperwork), TipoEvento.PaperworkUpdated, paperwork.Tramite_Id);
        }
        public SQLUpdateResult DeletePaperwork(Tramite paperwork)
        {
            return RecordResult(_paperworkmapper.DeletePaperwork(paperwork), TipoEvento.PaperworkDeleted, paperwork.Tramite_Id);
        }
        public List<ArchivoTramite> GetFilesByPaperWork(Guid Paperwork_Id)
        {
            return _paperworkmapper.GetFilesByPaperWork(Paperwork_Id);
        }
        public SQLUpdateResult AddPaperworkFile(ArchivoTramite paperwork)
        {
            return RecordResult(_paperworkmapper.AddPaperworkFile(paperwork), TipoEvento.PaperworkFileCreated, paperwork.ArchivoTramite_Id);
        }
        public SQLUpdateResult DeletePaperworkFile(ArchivoTramite paperwork)
        {
            return RecordResult(_paperworkmapper.DeletePaperworkFile(paperwork), TipoEvento.PaperworkFileDeleted, paperwork.ArchivoTramite_Id);
        }

        private static SQLUpdateResult RecordResult(SQLUpdateResult result, TipoEvento type, Guid targetId)
        {
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(type, targetId);
            return result;
        }
    }
}
