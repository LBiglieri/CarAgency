
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Compra
    {
        [TableColumnAttribute]
        public Guid Compra_Id { get; set; }

        [TableColumnAttribute]
        public int Numero { get; set; }

        [TableColumnAttribute]
        public Guid Persona_Id { get; set; }

        [TableColumnAttribute]
        public Guid VehiculoProspecto_Id { get; set; }

        [TableColumnAttribute]
        public Guid Vendedor_Id { get; set; }

        [TableColumnAttribute]
        public DateTime Fecha { get; set; }

        // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
        public CompraStatus Estado { get; set; }

        // Se completan en las etapas siguientes (CUF08 a CUF11).
        public RejectionReason? MotivoRechazo { get; set; }
        public Guid? TasacionAceptada_Id { get; set; }
        public double? PrecioAcordado { get; set; }
        public string CuilCuitTitular { get; set; }
        public DateTime? FechaFirmaContrato { get; set; }
        public string CodigoPrecarga08 { get; set; }
        public DateTime? FechaCitaTransferencia { get; set; }
        public Guid? Vehiculo_Id { get; set; }
        public DateTime? FechaCierre { get; set; }

        // No son columnas: el alta de CUF07 guarda los tres registros juntos.
        public VehiculoProspecto VehiculoProspecto { get; set; }
        public Inspeccion Inspeccion { get; set; }

    }
}
