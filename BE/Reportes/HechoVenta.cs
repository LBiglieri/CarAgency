using System;

namespace CarAgency.BE
{
    // Valores de la columna Tipo de dbo.Reporte_HechosVentas.
    public enum TipoHechoVenta { Cotizacion = 1, Reserva = 2, Factura = 3 }

    // Un paso del embudo comercial de PN1: una cotizacion, una reserva o una factura, tal como quedo guardada.
    // El reporte de proyeccion de ventas recibe la lista completa del periodo y hace todos los calculos en memoria.
    public sealed class HechoVenta
    {
        public TipoHechoVenta Tipo { get; set; }
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        // Solo en las reservas.
        public DateTime? FechaVencimiento { get; set; }
        // Una cotizacion vieja puede no tener persona: queda en null y no se empareja con ninguna reserva.
        public Guid? Persona_Id { get; set; }
        public Guid Vehiculo_Id { get; set; }
        // En la reserva es su propio Id; en la factura, la reserva que la origino. Null en la cotizacion.
        public Guid? Reserva_Id { get; set; }
        // Precio cotizado, precio de la reserva o valor facturado, segun el tipo.
        public double Precio { get; set; }
        public string Marca { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public string Persona_Descripcion { get; set; }
    }
}
