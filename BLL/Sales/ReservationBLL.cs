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
    public class ReservationBLL
    {
        private ReservationMapper _reservationmapper;
        public ReservationBLL()
        {
            _reservationmapper = new ReservationMapper();
        }
        public Reservation GetById(Guid id)
        {
            return _reservationmapper.GetById(id);
        }
        public List<Reservation> GetAllActiveByClient(Guid id)
        {
            return _reservationmapper.GetAllActiveByClient(id);
        }
        public List<Reservation> GetAll()
        {
            return _reservationmapper.GetAll();
        }

        public SQLUpdateResult AddReservation(Reservation reservation)
        {
            SQLUpdateResult result = _reservationmapper.AddReservation(reservation);
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(AuditEventType.ReservationCreated, reservation.Id);
            return result;
        }
    }
}
