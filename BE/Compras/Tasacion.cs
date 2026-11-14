
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Tasacion
    {
        [TableColumnAttribute]
        public Guid Tasacion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public Guid Inspeccion_Id { get; set; }

        [TableColumnAttribute]
        public Guid Tasador_Id { get; set; }

        [TableColumnAttribute]
        public double Valor { get; set; }

        [TableColumnAttribute]
        public string Justificacion { get; set; }

        [TableColumnAttribute]
        public DateTime Fecha { get; set; }

        [TableColumnAttribute]
        public DateTime Vigencia { get; set; }

        // No es columna de Appraisals: nombre y apellido del usuario que taso.
        [TableColumnAttribute]
        public string Tasador { get; set; }

    }
}
