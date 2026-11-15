using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;

namespace BLL
{
    // Reporte inteligente de PN1: proyeccion de ventas por embudo.
    //
    // En el sistema toda factura sale de una reserva (Factura.Reserva_Id es obligatorio) y una cotizacion
    // vale solo el dia que se hizo, asi que el embudo es Cotizacion -> Reserva -> Factura. La proyeccion
    // para los proximos HorizonteDias suma dos partes:
    //
    //   1. Reservas vigentes: cada una se factura con la tasa historica reserva -> factura.
    //   2. Flujo nuevo: las reservas que van a entrar (cotizaciones por periodo x tasa cotizacion -> reserva,
    //      mas las reservas que se hacen sin cotizacion), por la misma tasa reserva -> factura.
    //
    // El rango minimo-maximo es un intervalo del 90%: cada reserva es un "si o no" con su probabilidad
    // (varianza p(1-p)) y el flujo nuevo se toma como Poisson (varianza = media).
    //
    // No accede a la base: recibe los hechos ya cargados y solo mira los anteriores a la fecha de corte.
    // Por eso la misma clase sirve para validar el modelo: proyecta desde fechas pasadas y compara con lo real.
    public sealed class ProyeccionVentasCalculator
    {
        public const int HorizonteDias = 30;
        public const int VentanaTasasDias = 180;
        public const int PeriodosFlujo = 3;
        public const int PeriodosValidacion = 3;
        public const int MesesHistorial = 12;
        // Dias de datos que hay que cargar antes del corte: cubre el historial de 12 meses
        // y la ventana de tasas de la validacion mas vieja (3 x 30 + 180).
        public const int DiasDeHistoria = 400;
        private const double Z90 = 1.645;

        public ProyeccionVentas Calcular(IEnumerable<HechoVenta> hechos, DateTime fechaCorte)
        {
            List<HechoVenta> anteriores = (hechos ?? Enumerable.Empty<HechoVenta>())
                .Where(h => h != null && h.Fecha < fechaCorte).ToList();

            ProyeccionVentas proyeccion = Proyectar(anteriores, fechaCorte);
            proyeccion.Historial = ArmarHistorial(anteriores, fechaCorte);
            proyeccion.Marcas = ArmarMarcas(anteriores, fechaCorte, proyeccion);
            proyeccion.Validaciones = ArmarValidaciones(anteriores, fechaCorte);

            int reales = proyeccion.Validaciones.Sum(v => v.UnidadesReales);
            if (reales > 0)
                proyeccion.ErrorValidacion = proyeccion.Validaciones.Sum(v => Math.Abs(v.UnidadesProyectadas - v.UnidadesReales)) / reales;
            return proyeccion;
        }

        private ProyeccionVentas Proyectar(List<HechoVenta> todos, DateTime corte)
        {
            List<HechoVenta> hechos = todos.Where(h => h.Fecha < corte).ToList();
            List<HechoVenta> cotizaciones = hechos.Where(h => h.Tipo == TipoHechoVenta.Cotizacion).ToList();
            List<HechoVenta> reservas = hechos.Where(h => h.Tipo == TipoHechoVenta.Reserva).ToList();
            List<HechoVenta> ventas = Ventas(hechos);
            Dictionary<Guid, DateTime> facturadas = ventas.ToDictionary(v => ClaveReserva(v), v => v.Fecha);

            var proyeccion = new ProyeccionVentas { FechaCorte = corte, HorizonteHasta = corte.AddDays(HorizonteDias) };
            DateTime desdeTasas = corte.AddDays(-VentanaTasasDias);

            // Tasa reserva -> factura, con las reservas cuyo resultado ya se conoce al corte.
            int reservasResueltas = 0, reservasFacturadas = 0;
            foreach (HechoVenta reserva in reservas.Where(r => r.Fecha >= desdeTasas))
            {
                if (facturadas.ContainsKey(reserva.Id)) { reservasResueltas++; reservasFacturadas++; }
                else if (Vencida(reserva, corte) || VehiculoVendidoAOtro(reserva, ventas, corte)) reservasResueltas++;
            }
            proyeccion.ReservasResueltas = reservasResueltas;
            proyeccion.TasaReservaFactura = Tasa(reservasFacturadas, reservasResueltas);
            proyeccion.HistoriaSuficiente = reservasResueltas > 0;

            // Tasa cotizacion -> reserva. La cotizacion vale solo su dia: si ese dia no hubo reserva
            // de la misma persona por el mismo vehiculo, se perdio.
            int cotizacionesResueltas = 0, cotizacionesReservadas = 0;
            foreach (HechoVenta cotizacion in cotizaciones.Where(c => c.Fecha >= desdeTasas && c.Fecha.Date < corte.Date))
            {
                cotizacionesResueltas++;
                if (reservas.Any(r => VieneDe(r, cotizacion))) cotizacionesReservadas++;
            }
            proyeccion.CotizacionesResueltas = cotizacionesResueltas;
            proyeccion.TasaCotizacionReserva = Tasa(cotizacionesReservadas, cotizacionesResueltas);

            // Flujo nuevo: ritmo de entrada al embudo en los ultimos PeriodosFlujo periodos.
            DateTime desdeFlujo = corte.AddDays(-HorizonteDias * PeriodosFlujo);
            proyeccion.CotizacionesPorPeriodo = (double)cotizaciones.Count(c => c.Fecha >= desdeFlujo) / PeriodosFlujo;
            proyeccion.ReservasDirectasPorPeriodo = (double)reservas.Count(r => r.Fecha >= desdeFlujo && EsDirecta(r, cotizaciones)) / PeriodosFlujo;
            proyeccion.ReservasNuevasEsperadas = proyeccion.CotizacionesPorPeriodo * proyeccion.TasaCotizacionReserva
                + proyeccion.ReservasDirectasPorPeriodo;
            proyeccion.UnidadesFlujoNuevo = proyeccion.ReservasNuevasEsperadas * proyeccion.TasaReservaFactura;

            // Ticket: lo que se facturo en promedio; si todavia no hubo ventas, el precio promedio reservado.
            List<HechoVenta> ventasRecientes = ventas.Where(v => v.Fecha >= desdeTasas).ToList();
            List<HechoVenta> reservasRecientes = reservas.Where(r => r.Fecha >= desdeTasas).ToList();
            proyeccion.TicketPromedio = ventasRecientes.Count > 0 ? ventasRecientes.Average(v => v.Precio)
                : reservasRecientes.Count > 0 ? reservasRecientes.Average(r => r.Precio) : 0;
            proyeccion.MontoFlujoNuevo = proyeccion.UnidadesFlujoNuevo * proyeccion.TicketPromedio;

            // Reservas vigentes. Un vehiculo se vende una sola vez: si tiene varias reservas, la probabilidad
            // de venderlo es 1 - (1 - p)^n y se reparte entre ellas.
            double varianzaUnidades = proyeccion.UnidadesFlujoNuevo;
            double varianzaMonto = proyeccion.UnidadesFlujoNuevo * proyeccion.TicketPromedio * proyeccion.TicketPromedio;
            var vigentes = reservas.Where(r => !Vencida(r, corte) && !facturadas.ContainsKey(r.Id) && !VehiculoVendidoAOtro(r, ventas, corte));
            foreach (var vehiculo in vigentes.GroupBy(r => r.Vehiculo_Id))
            {
                List<HechoVenta> delVehiculo = vehiculo.ToList();
                double probabilidadVehiculo = 1 - Math.Pow(1 - proyeccion.TasaReservaFactura, delVehiculo.Count);
                double precioVehiculo = delVehiculo.Average(r => r.Precio);
                foreach (HechoVenta reserva in delVehiculo)
                {
                    double probabilidad = probabilidadVehiculo / delVehiculo.Count;
                    proyeccion.ReservasVigentes.Add(new OportunidadVenta
                    {
                        Reserva_Id = reserva.Id,
                        Vehiculo_Id = reserva.Vehiculo_Id,
                        Vehiculo_Descripcion = reserva.Vehiculo_Descripcion,
                        Persona_Descripcion = reserva.Persona_Descripcion,
                        Marca = reserva.Marca,
                        Fecha = reserva.Fecha,
                        FechaVencimiento = reserva.FechaVencimiento ?? reserva.Fecha,
                        Precio = reserva.Precio,
                        Probabilidad = probabilidad,
                        MontoEsperado = probabilidad * reserva.Precio
                    });
                }
                varianzaUnidades += probabilidadVehiculo * (1 - probabilidadVehiculo);
                varianzaMonto += probabilidadVehiculo * (1 - probabilidadVehiculo) * precioVehiculo * precioVehiculo;
            }
            proyeccion.ReservasVigentes = proyeccion.ReservasVigentes.OrderByDescending(o => o.MontoEsperado).ToList();
            proyeccion.UnidadesReservasVigentes = proyeccion.ReservasVigentes.Sum(o => o.Probabilidad);
            proyeccion.MontoReservasVigentes = proyeccion.ReservasVigentes.Sum(o => o.MontoEsperado);

            proyeccion.Unidades = proyeccion.UnidadesReservasVigentes + proyeccion.UnidadesFlujoNuevo;
            proyeccion.Monto = proyeccion.MontoReservasVigentes + proyeccion.MontoFlujoNuevo;
            double margenUnidades = Z90 * Math.Sqrt(varianzaUnidades);
            double margenMonto = Z90 * Math.Sqrt(varianzaMonto);
            proyeccion.UnidadesMinimo = Math.Max(0, proyeccion.Unidades - margenUnidades);
            proyeccion.UnidadesMaximo = proyeccion.Unidades + margenUnidades;
            proyeccion.MontoMinimo = Math.Max(0, proyeccion.Monto - margenMonto);
            proyeccion.MontoMaximo = proyeccion.Monto + margenMonto;
            return proyeccion;
        }

        // Una venta por reserva: si una reserva tiene varias facturas, cuenta la primera.
        private static List<HechoVenta> Ventas(IEnumerable<HechoVenta> hechos)
        {
            return hechos.Where(h => h.Tipo == TipoHechoVenta.Factura)
                .GroupBy(ClaveReserva)
                .Select(g => g.OrderBy(f => f.Fecha).First())
                .ToList();
        }

        private static Guid ClaveReserva(HechoVenta factura)
        {
            return factura.Reserva_Id ?? factura.Id;
        }

        // La reserva vale hasta el final del dia de vencimiento.
        private static bool Vencida(HechoVenta reserva, DateTime corte)
        {
            return (reserva.FechaVencimiento ?? reserva.Fecha).Date < corte.Date;
        }

        // El vehiculo se facturo con otra reserva despues de esta: esta ya no se puede concretar.
        private static bool VehiculoVendidoAOtro(HechoVenta reserva, List<HechoVenta> ventas, DateTime corte)
        {
            return ventas.Any(v => v.Vehiculo_Id == reserva.Vehiculo_Id && ClaveReserva(v) != reserva.Id
                && v.Fecha >= reserva.Fecha && v.Fecha < corte);
        }

        // La reserva se hizo el mismo dia de la cotizacion, para la misma persona y el mismo vehiculo.
        private static bool VieneDe(HechoVenta reserva, HechoVenta cotizacion)
        {
            return cotizacion.Persona_Id.HasValue && reserva.Persona_Id == cotizacion.Persona_Id
                && reserva.Vehiculo_Id == cotizacion.Vehiculo_Id
                && reserva.Fecha.Date == cotizacion.Fecha.Date && reserva.Fecha >= cotizacion.Fecha;
        }

        private static bool EsDirecta(HechoVenta reserva, List<HechoVenta> cotizaciones)
        {
            return !cotizaciones.Any(c => VieneDe(reserva, c));
        }

        private static double Tasa(int casos, int total)
        {
            return total == 0 ? 0 : (double)casos / total;
        }

        // Ventas de los ultimos MesesHistorial meses calendario; el ultimo es el mes del dia anterior al corte.
        private static List<VentaMensual> ArmarHistorial(List<HechoVenta> hechos, DateTime corte)
        {
            List<HechoVenta> ventas = Ventas(hechos);
            DateTime ultimoDia = corte.AddDays(-1);
            DateTime ultimoMes = new DateTime(ultimoDia.Year, ultimoDia.Month, 1);
            var historial = new List<VentaMensual>();
            for (int i = MesesHistorial - 1; i >= 0; i--)
            {
                DateTime desde = ultimoMes.AddMonths(-i);
                DateTime hasta = desde.AddMonths(1);
                List<HechoVenta> delMes = ventas.Where(v => v.Fecha >= desde && v.Fecha < hasta).ToList();
                historial.Add(new VentaMensual { Anio = desde.Year, Mes = desde.Month, Unidades = delMes.Count, Monto = delMes.Sum(v => v.Precio) });
            }
            return historial;
        }

        // Por marca: lo vendido en la ventana de tasas, lo reservado y lo esperado. El flujo nuevo se reparte
        // segun lo que la gente esta cotizando y reservando ahora (la demanda de los ultimos periodos).
        private static List<ProyeccionMarca> ArmarMarcas(List<HechoVenta> hechos, DateTime corte, ProyeccionVentas proyeccion)
        {
            DateTime desdeTasas = corte.AddDays(-VentanaTasasDias);
            DateTime desdeFlujo = corte.AddDays(-HorizonteDias * PeriodosFlujo);
            List<HechoVenta> cotizaciones = hechos.Where(h => h.Tipo == TipoHechoVenta.Cotizacion).ToList();
            List<HechoVenta> ventas = Ventas(hechos).Where(v => v.Fecha >= desdeTasas).ToList();
            List<HechoVenta> demanda = hechos.Where(h => h.Fecha >= desdeFlujo && (h.Tipo == TipoHechoVenta.Cotizacion
                || (h.Tipo == TipoHechoVenta.Reserva && EsDirecta(h, cotizaciones)))).ToList();

            IEnumerable<string> marcas = ventas.Select(v => v.Marca)
                .Concat(proyeccion.ReservasVigentes.Select(o => o.Marca))
                .Concat(demanda.Select(d => d.Marca))
                .Where(m => !string.IsNullOrEmpty(m)).Distinct();

            return marcas.Select(marca => new ProyeccionMarca
            {
                Marca = marca,
                UnidadesVendidas = ventas.Count(v => v.Marca == marca),
                ReservasVigentes = proyeccion.ReservasVigentes.Count(o => o.Marca == marca),
                UnidadesEsperadas = proyeccion.ReservasVigentes.Where(o => o.Marca == marca).Sum(o => o.Probabilidad)
                    + (demanda.Count == 0 ? 0 : proyeccion.UnidadesFlujoNuevo * demanda.Count(d => d.Marca == marca) / demanda.Count)
            })
            .OrderByDescending(m => m.UnidadesEsperadas).ThenByDescending(m => m.UnidadesVendidas).ThenBy(m => m.Marca)
            .ToList();
        }

        // Prueba hacia atras: que habria proyectado el modelo en cada uno de los periodos anteriores
        // y cuanto se facturo de verdad. Se omiten los periodos sin ningun dato previo.
        private List<ValidacionProyeccion> ArmarValidaciones(List<HechoVenta> hechos, DateTime corte)
        {
            List<HechoVenta> ventas = Ventas(hechos);
            var validaciones = new List<ValidacionProyeccion>();
            for (int k = PeriodosValidacion; k >= 1; k--)
            {
                DateTime desde = corte.AddDays(-HorizonteDias * k);
                DateTime hasta = desde.AddDays(HorizonteDias);
                if (!hechos.Any(h => h.Fecha < desde))
                    continue;
                validaciones.Add(new ValidacionProyeccion
                {
                    Desde = desde,
                    Hasta = hasta,
                    UnidadesProyectadas = Proyectar(hechos, desde).Unidades,
                    UnidadesReales = ventas.Count(v => v.Fecha >= desde && v.Fecha < hasta)
                });
            }
            return validaciones;
        }
    }
}
