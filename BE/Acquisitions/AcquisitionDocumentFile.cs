
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class AcquisitionDocumentFile
    {
        [TableColumnAttribute]
        public Guid Document_Id { get; set; }

        [TableColumnAttribute]
        public string FileName { get; set; }

        [TableColumnAttribute]
        public byte[] FileContent { get; set; }

    }
}
