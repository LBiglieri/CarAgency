using System;
using System.Collections.Generic;

namespace CarAgency.BE
{
    // Resultado del reporte inteligente de PN1: cuantas unidades y cuanta plata se van a facturar
    // en los proximos dias, con el detalle de como se llego al numero.
    // Lo arma ProyeccionVentasCalculator; la UI solo lo dibuja en el PDF.
    public sealed class ProyeccionVentas
    {
        // Se usaron los datos anteriores a FechaCorte. El horizonte es [FechaCorte, HorizonteHasta).
        public DateTime FechaCorte { get; set; }
        public DateTime HorizonteHasta { get; set; }

        public double Unidades { get; set; }
        public double UnidadesMinimo { get; set; }
        public double UnidadesMaximo { get; set; }
        public double Monto { get; set; }
        public double MontoMinimo { get; set; }
        public double MontoMaximo { get; set; }

        // Las dos partes de la proyeccion: lo que ya esta reservado y lo que todavia no entro al embudo.
        public double UnidadesReservasVigentes { get; set; }
        public double MontoReservasVigentes { get; set; }
        public double UnidadesFlujoNuevo { get; set; }
        public double MontoFlujoNuevo { get; set; }

        // Tasas historicas: de cada 100 reservas resueltas, cuantas se facturaron; idem cotizacion -> reserva.
        public double TasaReservaFactura { get; set; }
        public int ReservasResueltas { get; set; }
        public double TasaCotizacionReserva { get; set; }
        public int CotizacionesResueltas { get; set; }

        // Ritmo de entrada al embudo, promedio por periodo del largo del horizonte.
        public double CotizacionesPorPeriodo { get; set; }
        public double ReservasDirectasPorPeriodo { get; set; }
        public double ReservasNuevasEsperadas { get; set; }
        public double TicketPromedio { get; set; }

        // False si no hay reservas resueltas para calcular la tasa: el numero no es confiable.
        public bool HistoriaSuficiente { get; set; }
        // Error de las proyecciones pasadas: suma de |proyectado - real| sobre suma de lo real. Null sin ventas reales.
        public double? ErrorValidacion { get; set; }

        public List<OportunidadVenta> ReservasVigentes { get; set; } = new List<OportunidadVenta>();
        public List<VentaMensual> Historial { get; set; } = new List<VentaMensual>();
        public List<ProyeccionMarca> Marcas { get; set; } = new List<ProyeccionMarca>();
        public List<ValidacionProyeccion> Validaciones { get; set; } = new List<ValidacionProyeccion>();
    }

    // Reserva vigente a la fecha de corte, con la probabilidad de terminar en factura.
    public sealed class OportunidadVenta
    {
        public Guid Reserva_Id { get; set; }
        public Guid Vehiculo_Id { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public string Persona_Descripcion { get; set; }
        public string Marca { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public double Precio { get; set; }
        public double Probabilidad { get; set; }
        public double MontoEsperado { get; set; }
    }

    // Ventas facturadas en un mes calendario.
    public sealed class VentaMensual
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Unidades { get; set; }
        public double Monto { get; set; }
    }

    // Ventas recientes y esperadas por marca.
    public sealed class ProyeccionMarca
    {
        public string Marca { get; set; }
        public int UnidadesVendidas { get; set; }
        public int ReservasVigentes { get; set; }
        public double UnidadesEsperadas { get; set; }
    }

    // Proyeccion calculada como si se hubiera pedido en una fecha pasada, comparada con lo que se facturo despues.
    public sealed class ValidacionProyeccion
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public double UnidadesProyectadas { get; set; }
        public int UnidadesReales { get; set; }
    }
}
