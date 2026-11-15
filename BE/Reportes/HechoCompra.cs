using System;

namespace CarAgency.BE
{
    // Una compra de PN2 tal como sale de Reporte_HechosCompras: su estado actual y la fecha en que llego a
    // cada etapa (null si no llego o si no quedo registro). ProyeccionComprasCalculator reconstruye con estas
    // fechas en que etapa estaba la compra en cualquier fecha de corte.
    public sealed class HechoCompra
    {
        public Guid Compra_Id { get; set; }
        public int Numero { get; set; }
        public CompraStatus Estado { get; set; }
        public RejectionReason? MotivoRechazo { get; set; }

        public DateTime Fecha { get; set; }
        public DateTime? FechaInspeccion { get; set; }
        public DateTime? FechaTasacion { get; set; }
        public DateTime? FechaUltimaTasacion { get; set; }
        public DateTime? FechaFirmaContrato { get; set; }
        public DateTime? FechaDocumentacion { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaTransferencia { get; set; }
        public DateTime? FechaCierre { get; set; }

        public double? ValorTasacion { get; set; }
        public double? PrecioAcordado { get; set; }
        public double MontoPagado { get; set; }

        public string Marca { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public string Persona_Descripcion { get; set; }
    }
}
