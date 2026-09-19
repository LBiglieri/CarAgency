using CarAgency.Security.Audit;
using CarAgency.BE.Audit;
using CarAgency.BE;
using CarAgency.Mappers;
using System;

namespace BLL
{
    public class ClientsBLL
    {
        private ClientMapper _clientmapper;
        public ClientsBLL()
        {
            _clientmapper = new ClientMapper();
        }
        public Client GetByDni(int dni)
        {
            return _clientmapper.GetByDni(dni);
        }
        public Client GetById(Guid id)
        {
            return _clientmapper.GetById(id);
        }

        public SQLUpdateResult AddClient(Client client)
        {
            Client validationClient;
            validationClient = _clientmapper.GetByDni(client.Dni);
            if (validationClient != null)
                throw new Exception("A Client with this Dni already exists.");

            client.Id = Guid.NewGuid();
            SQLUpdateResult result = _clientmapper.AddClient(client);
            if (result != null && result.sqlResult == SQLResultType.success)
                AuditBLL.Record(AuditEventType.ClientCreated, client.Id);
            return result;
        }
    }
}
