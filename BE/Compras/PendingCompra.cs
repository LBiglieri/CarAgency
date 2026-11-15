
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Compra en gestion, de Tasada a Transferida (CUF10 paso 1).
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum.
    public class PendingCompra
    {
        public Guid Compra_Id { get; set; }
        public int Numero { get; set; }
        public Guid Inspeccion_Id { get; set; }
        public Guid Persona_Id { get; set; }
        public int DNI { get; set; }
        public string Patente { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public CompraStatus Estado { get; set; }

    }
}
