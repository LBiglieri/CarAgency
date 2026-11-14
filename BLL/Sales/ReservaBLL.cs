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
    public class ReservaBLL
    {
        private ReservaMapper _reservationmapper;
        public ReservaBLL()
        {
            _reservationmapper = new ReservaMapper();
        }
        public Reserva GetById(Guid id)
        {
            return _reservationmapper.GetById(id);
        }
        public List<Reserva> GetAllActiveByPerson(Guid id)
        {
            return _reservationmapper.GetAllActiveByPerson(id);
        }
        public List<Reserva> GetAll()
        {
            return _reservationmapper.GetAll();
        }

        public SQLUpdateResult AddReservation(Reserva reservation)
        {
            SQLUpdateResult result = _reservationmapper.AddReservation(reservation);
            if (result != null && result.sqlResult == SQLResultType.success)
                BitacoraEventosBLL.Record(TipoEvento.ReservationCreated, reservation.Reserva_Id);
            return result;
        }
    }
}
