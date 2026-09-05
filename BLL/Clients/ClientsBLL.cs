using CarAgency.Entities;
using CarAgency.Repository;
using CarAgency.Repository.Persistence;
using CarAgency.Utilities.Security;
using CarAgency.Utilities.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClientsBLL
    {
        private ClientRepository _clientrepository;
        public ClientsBLL()
        {
            _clientrepository = new ClientRepository();
        }
        public Client GetByDni(int dni)
        {
            return Decrypt(_clientrepository.GetByDni(dni));
        }
        public Client GetById(Guid id)
        {
            return Decrypt(_clientrepository.GetById(id));
        }

        private Client Decrypt(Client client)
        {
            if (client == null)
                return null;

            client.Name = CryptographyHandler.Decrypt(client.Name);
            client.Surname = CryptographyHandler.Decrypt(client.Surname);
            client.Address = CryptographyHandler.Decrypt(client.Address);
            client.Phone_Number_Personal = CryptographyHandler.Decrypt(client.Phone_Number_Personal);
            client.Phone_Number_House = CryptographyHandler.Decrypt(client.Phone_Number_House);
            client.Email = CryptographyHandler.Decrypt(client.Email);

            return client;
        }
        public SQLUpdateResult AddClient(Client client)
        {
            Client validationClient;
            validationClient = _clientrepository.GetByDni(client.Dni);
            if (validationClient != null)
                throw new Exception("A Client with this Dni already exists.");

            client.Id = Guid.NewGuid();
            client.Name = CryptographyHandler.Encrypt(client.Name);
            client.Surname = CryptographyHandler.Encrypt(client.Surname);
            client.Address = CryptographyHandler.Encrypt(client.Address);
            client.Phone_Number_Personal = CryptographyHandler.Encrypt(client.Phone_Number_Personal);
            client.Phone_Number_House = CryptographyHandler.Encrypt(client.Phone_Number_House);
            client.Email = CryptographyHandler.Encrypt(client.Email);
            return _clientrepository.AddClient(client);
        }
    }
}
