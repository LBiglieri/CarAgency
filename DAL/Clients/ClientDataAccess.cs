using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Clients
{
    public sealed class ClientDataAccess : DataAccessBase
    {
        public DataTable GetByDni(int dni) { return Read("Clients_GetByDni", new SqlParameter("@Dni", dni)); }
        public DataTable GetById(Guid id) { return Read("Clients_GetById", new SqlParameter("@Id", id)); }

        public DataTable Add(Guid id, int dni, string name, string surname, string address, string phoneNumberHouse,
            string phoneNumberPersonal, string email, DateTime dateOfBirth, DvhCalculator dvh)
        {
            return Read("Clients_Add", dvh,
                new SqlParameter("@Id", id),
                new SqlParameter("@Dni", dni),
                new SqlParameter("@Name", name),
                new SqlParameter("@Surname", surname),
                new SqlParameter("@Address", address),
                new SqlParameter("@Phone_Number_House", phoneNumberHouse),
                new SqlParameter("@Phone_Number_Personal", phoneNumberPersonal),
                new SqlParameter("@Email", email),
                new SqlParameter("@Date_Of_Birth", dateOfBirth));
        }
    }
}
