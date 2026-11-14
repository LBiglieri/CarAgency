using System;
using System.Collections.Generic;
using System.Data;
using CarAgency.BE;
using CarAgency.DAL.Sales;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class ReservaMapper
    {
        private const string Table = "Reserva";
        private readonly ReservaDataAccess data = new ReservaDataAccess();

        public Reserva GetById(Guid id)
        {
            List<Reserva> reservations = Load(data.GetById(id));
            return reservations == null ? null : reservations[0];
        }

        public List<Reserva> GetAllActiveByPerson(Guid Person_Id)
        {
            return Load(data.GetAllActiveByPerson(Person_Id));
        }

        public List<Reserva> GetAll()
        {
            return Load(data.GetAll());
        }

        public SQLUpdateResult AddReservation(Reserva reservation)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(reservation.Reserva_Id, reservation.Vehiculo_Id,
                reservation.Persona_Id, reservation.Precio, reservation.Fecha, reservation.FechaVencimiento, dvh));
        }

        // Sin filas devuelve null. El SP trae Person_Name/Person_Surname encriptados y Person_Dni para
        // armar Person_Description; las entidades salen en el mismo orden que las filas.
        private static List<Reserva> Load(DataTable table)
        {
            using (table)
            using (DataTableReader reader = table.CreateDataReader())
            {
                List<Reserva> reservations = MappingHandler.MapReaderToEntities<Reserva>(reader);
                if (reservations.Count == 0) return null;
                for (int i = 0; i < reservations.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    reservations[i].Persona_Descripcion = PersonaMapper.Describe(
                        (string)row["Persona_Nombre"], (string)row["Persona_Apellido"], (int)row["Persona_DNI"]);
                }
                return reservations;
            }
        }
    }
}
