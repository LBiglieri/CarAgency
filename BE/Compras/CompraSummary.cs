
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    // Resumen completo de la compra para la etapa Finalizar (CUF10 paso 20) y la constancia.
    // Sin [TableColumnAttribute]: el MappingHandler no convierte NULL.
    public class CompraSummary
    {
        public Guid Compra_Id { get; set; }
        public int Numero { get; set; }
        public Guid Persona_Id { get; set; }
        public int DNI { get; set; }
        public string Patente { get; set; }
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public Guid Marca_Id { get; set; }
        public Guid Modelo_Id { get; set; }
        public Guid Version_Id { get; set; }
        public Guid Color_Id { get; set; }
        public int Anio { get; set; }
        public int Puertas { get; set; }
        public int Kilometraje { get; set; }
        public Guid Inspeccion_Id { get; set; }
        public DateTime? FechaRealizada { get; set; }
        public int? KilometrajeVerificado { get; set; }
        public double? Tasacion_Valor { get; set; }
        public DateTime? Tasacion_Fecha { get; set; }
        public double? PrecioAcordado { get; set; }
        public string CuilCuitTitular { get; set; }
        public DateTime? FechaFirmaContrato { get; set; }
        public double MontoPagado { get; set; }
        public string CodigoPrecarga08 { get; set; }
        public DateTime? FechaCitaTransferencia { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int? ComprobanteCompra_Numero { get; set; }
        public DateTime? ComprobanteCompra_Fecha { get; set; }

    }
}
