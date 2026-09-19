using System;
using System.Collections.Generic;
using System.Data;
using CarAgency.BE;
using CarAgency.DAL.Sales;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class ReservationMapper
    {
        private const string Table = "Reservation";
        private readonly ReservationDataAccess data = new ReservationDataAccess();

        public Reservation GetById(Guid id)
        {
            List<Reservation> reservations = Load(data.GetById(id));
            return reservations == null ? null : reservations[0];
        }

        public List<Reservation> GetAllActiveByClient(Guid Client_Id)
        {
            return Load(data.GetAllActiveByClient(Client_Id));
        }

        public List<Reservation> GetAll()
        {
            return Load(data.GetAll());
        }

        public SQLUpdateResult AddReservation(Reservation reservation)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(reservation.Id, reservation.Vehicle_Id,
                reservation.Client_Id, reservation.Price, reservation.Creation_Date, reservation.Expiration_Date, dvh));
        }

        // Sin filas devuelve null. El SP trae Client_Name/Client_Surname encriptados y Client_Dni para
        // armar Client_Description; las entidades salen en el mismo orden que las filas.
        private static List<Reservation> Load(DataTable table)
        {
            using (table)
            using (DataTableReader reader = table.CreateDataReader())
            {
                List<Reservation> reservations = MappingHandler.MapReaderToEntities<Reservation>(reader);
                if (reservations.Count == 0) return null;
                for (int i = 0; i < reservations.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    reservations[i].Client_Description = ClientMapper.Describe(
                        (string)row["Client_Name"], (string)row["Client_Surname"], (int)row["Client_Dni"]);
                }
                return reservations;
            }
        }
    }
}
