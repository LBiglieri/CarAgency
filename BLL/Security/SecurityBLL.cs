using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Mappers.Persistence;
using CarAgency.Security.Security;
using CarAgency.Security.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SecurityBLL
    {
        private SecurityMapper _securitymapper;
        public SecurityBLL()
        {
            _securitymapper = new SecurityMapper();
        }

        public void RealizarBackup(string path)
        {
            _securitymapper.RealizarBackup(path);
        }

        public void RealizarRestore(string path)
        {
            _securitymapper.RealizarRestore(path);
        }
    }
}
