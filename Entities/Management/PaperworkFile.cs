
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarAgency.Entities
{
    public class PaperworkFile
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Paperwork_Id { get; set; }

        [TableColumnAttribute]
        public string FileName { get; set; }

        [TableColumnAttribute]
        public byte[] FileContent { get; set; }

        [TableColumnAttribute]
        public DateTime UploadedDate { get; set; }

    }
}
