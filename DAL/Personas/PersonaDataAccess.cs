using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Personas
{
    public sealed class PersonaDataAccess : DataAccessBase
    {
        public DataTable GetByDni(int dni) { return Read("Persona_GetByDni", new SqlParameter("@DNI", dni)); }
        public DataTable GetById(Guid id) { return Read("Persona_GetById", new SqlParameter("@Persona_Id", id)); }

        public DataTable Add(Guid id, int dni, string name, string surname, string address, string phoneNumberHouse,
            string phoneNumberPersonal, string email, DateTime dateOfBirth, DvhCalculator dvh)
        {
            return Read("Persona_Add", dvh,
                new SqlParameter("@Persona_Id", id),
                new SqlParameter("@DNI", dni),
                new SqlParameter("@Nombre", name),
                new SqlParameter("@Apellido", surname),
                new SqlParameter("@Direccion", address),
                new SqlParameter("@TelefonoCasa", phoneNumberHouse),
                new SqlParameter("@TelefonoPersonal", phoneNumberPersonal),
                new SqlParameter("@Email", email),
                new SqlParameter("@FechaNacimiento", dateOfBirth));
        }
    }
}
