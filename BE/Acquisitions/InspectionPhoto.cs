
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class InspectionPhoto
    {
        [TableColumnAttribute]
        public Guid Id { get; set; }

        [TableColumnAttribute]
        public Guid Inspection_Id { get; set; }

        [TableColumnAttribute]
        public string FileName { get; set; }

        [TableColumnAttribute]
        public byte[] FileContent { get; set; }

        [TableColumnAttribute]
        public DateTime UploadedDate { get; set; }

    }
}
