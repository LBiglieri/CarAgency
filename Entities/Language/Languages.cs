
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class Languages
    {
        public Languages(string code, string description)
        {
            Language_Code = code;
            Language_Description = description;   
        }
        public string Language_Code { get; set; }
        public string Language_Description { get; set; }
    }
}
