using System;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Clients;
using CarAgency.Security.Integrity;
using CarAgency.Security.Security;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    // Los datos personales se guardan encriptados: el mapper encripta al escribir y desencripta
    // al leer, asi BLL y UI trabajan siempre con texto plano. El DVH se calcula sobre lo encriptado,
    // que es lo que queda guardado en la tabla.
    public class ClientMapper
    {
        private const string Table = "Clients";
        private readonly ClientDataAccess data = new ClientDataAccess();

        public Client GetByDni(int dni)
        {
            return Decrypt(MappingHandler.MapTableToEntities<Client>(data.GetByDni(dni)).FirstOrDefault());
        }

        public Client GetById(Guid id)
        {
            return Decrypt(MappingHandler.MapTableToEntities<Client>(data.GetById(id)).FirstOrDefault());
        }

        // Descripcion del cliente para otras entidades: "Nombre Apellido DNI: 123".
        public string GetDescription(Guid id)
        {
            Client client = GetById(id);
            return client == null ? null : Format(client.Name, client.Surname, client.Dni);
        }

        // Para SP que traen nombre y apellido encriptados por separado: concatenados en SQL ya no se
        // podrian desencriptar.
        public static string Describe(string encryptedName, string encryptedSurname, int dni)
        {
            return Format(CryptographyHandler.Decrypt(encryptedName), CryptographyHandler.Decrypt(encryptedSurname), dni);
        }

        private static string Format(string name, string surname, int dni)
        {
            return name + " " + surname + " DNI: " + dni;
        }

        public SQLUpdateResult AddClient(Client client)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(client.Id, client.Dni,
                CryptographyHandler.Encrypt(client.Name),
                CryptographyHandler.Encrypt(client.Surname),
                CryptographyHandler.Encrypt(client.Address),
                CryptographyHandler.Encrypt(client.Phone_Number_House),
                CryptographyHandler.Encrypt(client.Phone_Number_Personal),
                CryptographyHandler.Encrypt(client.Email),
                client.Date_Of_Birth,
                dvh));
        }

        private static Client Decrypt(Client client)
        {
            if (client == null) return null;
            client.Name = CryptographyHandler.Decrypt(client.Name);
            client.Surname = CryptographyHandler.Decrypt(client.Surname);
            client.Address = CryptographyHandler.Decrypt(client.Address);
            client.Phone_Number_Personal = CryptographyHandler.Decrypt(client.Phone_Number_Personal);
            client.Phone_Number_House = CryptographyHandler.Decrypt(client.Phone_Number_House);
            client.Email = CryptographyHandler.Decrypt(client.Email);
            return client;
        }
    }
}
