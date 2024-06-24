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
    public class ReservationBLL
    {
        private ReservationRepository _reservationrepository;
        public ReservationBLL()
        {
            _reservationrepository = new ReservationRepository();
        }
        public Reservation GetById(Guid id)
        {
            return _reservationrepository.GetById(id);
        }
        public List<Reservation> GetAllActiveByClient(Guid id)
        {
            return _reservationrepository.GetAllActiveByClient(id);
        }
        public List<Reservation> GetAll()
        {
            return _reservationrepository.GetAll();
        }

        public SQLUpdateResult AddReservation(Reservation reservation)
        {
            return _reservationrepository.AddReservation(reservation);
        }
    }
}
