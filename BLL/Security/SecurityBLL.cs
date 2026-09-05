using CarAgency.BE;
using CarAgency.DAL;
using CarAgency.DAL.Persistence;
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
        private SecurityRepository _securityrepository;
        public SecurityBLL()
        {
            _securityrepository = new SecurityRepository();
        }

        public void RealizarBackup(string path)
        {
            _securityrepository.RealizarBackup(path);
        }

        public void RealizarRestore(string path)
        {
            _securityrepository.RealizarRestore(path);
        }
    }
}
