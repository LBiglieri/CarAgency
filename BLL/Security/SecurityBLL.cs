using CarAgency.Entities;
using CarAgency.Repository;
using CarAgency.Utilities.Persistence;
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
