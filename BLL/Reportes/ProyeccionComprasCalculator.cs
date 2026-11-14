using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;

namespace BLL
{
    // Reporte inteligente de PN2: proyeccion de ingresos al inventario por compras.
    //
    // Una compra recorre siete etapas hasta Finalizada (el vehiculo entra al inventario) y se puede perder en la
    // inspeccion (no se presento o no apto) o en la tasada (rechazo la tasacion o no firmo el contrato).
    // La proyeccion para los proximos HorizonteDias suma dos partes:
    //
    //   1. Compras en curso: cada una se finaliza con P(finalizar | etapa) historica, por la probabilidad de que
    //      se cierre dentro del horizonte segun cuanto tardaron las compras finalizadas desde esa etapa y cuanto
    //      lleva ya esta en la suya.
    //   2. Flujo nuevo: las compras que se van a iniciar (compras por periodo), por P(finalizar | inicio), por la
    //      parte de ellas que llega a cerrarse en el horizonte (si una compra tarda D dias y se inicia en un dia
    //      al azar del horizonte, se cierra a tiempo con probabilidad max(0, H - D) / H).
    //
    // El rango minimo-maximo es un intervalo del 90%: cada compra en curso es un "si o no" con su probabilidad
    // (varianza p(1-p)) y el flujo nuevo se toma como Poisson (varianza = media).
    //
    // La compra no guarda el historial de estados: la etapa a una fecha de corte se reconstruye con la fecha de
    // cada etapa (ver Reporte_HechosCompras). Con eso la misma clase proyecta desde fechas pasadas y valida el modelo.
    public sealed class ProyeccionComprasCalculator
    {
        public const int HorizonteDias = 30;
        public const int VentanaTasasDias = 180;
        public const int PeriodosFlujo = 3;
        public const int PeriodosValidacion = 3;
        public const int MesesHistorial = 12;
        public const int DiasDeHistoria = 400;
        // Con menos compras resueltas en la ventana las tasas no son confiables y el reporte lo advierte.
        public const int MinimoComprasResueltas = 10;
        private const double Z90 = 1.645;

        private const int EtapaInicial = (int)CompraStatus.InspectionScheduled;
        private const int UltimaEtapaAbierta = (int)CompraStatus.Transferred;
        private const int EtapaFinal = (int)CompraStatus.Finished;

        public ProyeccionCompras Calcular(IEnumerable<HechoCompra> hechos, DateTime fechaCorte)
        {
            List<Trayectoria> compras = (hechos ?? Enumerable.Empty<HechoCompra>())
                .Where(h => h != null && h.Fecha < fechaCorte).Select(h => new Trayectoria(h)).ToList();

            ProyeccionCompras proyeccion = Proyectar(compras, fechaCorte);
            proyeccion.Historial = ArmarHistorial(compras, fechaCorte);
            proyeccion.Etapas = ArmarEtapas(compras, fechaCorte);
            proyeccion.Perdidas = ArmarPerdidas(compras, fechaCorte);
            proyeccion.Validaciones = ArmarValidaciones(compras, fechaCorte);

            List<EtapaEmbudoCompra> conDatos = proyeccion.Etapas.Where(e => e.Llegaron > 0).ToList();
            EtapaEmbudoCompra lenta = conDatos.Where(e => e.DiasPromedio.HasValue).OrderByDescending(e => e.DiasPromedio.Value).FirstOrDefault();
            EtapaEmbudoCompra perdida = conDatos.Where(e => e.Avanzaron < e.Llegaron).OrderBy(e => e.TasaPaso).FirstOrDefault();
            proyeccion.EtapaMasLenta = lenta == null ? (int?)null : (int)lenta.Etapa;
            proyeccion.EtapaConMasPerdidas = perdida == null ? (int?)null : (int)perdida.Etapa;

            int reales = proyeccion.Validaciones.Sum(v => v.UnidadesReales);
            if (reales > 0)
                proyeccion.ErrorValidacion = proyeccion.Validaciones.Sum(v => Math.Abs(v.UnidadesProyectadas - v.UnidadesReales)) / reales;
            return proyeccion;
        }

        private ProyeccionCompras Proyectar(List<Trayectoria> todas, DateTime corte)
        {
            List<Trayectoria> compras = todas.Where(c => c.Hecho.Fecha < corte).ToList();
            List<Trayectoria> resueltas = Resueltas(compras, corte);
            List<Trayectoria> finalizadas = resueltas.Where(c => c.Finalizada).ToList();

            var proyeccion = new ProyeccionCompras { FechaCorte = corte, HorizonteHasta = corte.AddDays(HorizonteDias) };
            proyeccion.ComprasResueltas = resueltas.Count;
            proyeccion.HistoriaSuficiente = resueltas.Count >= MinimoComprasResueltas;
            proyeccion.TasaFinalizacion = TasaFinalizacion(resueltas, EtapaInicial);
            proyeccion.DuracionPromedio = finalizadas.Count == 0 ? 0 : finalizadas.Average(c => Dias(c.Fechas[EtapaInicial], c.Fechas[EtapaFinal]));

            // Ticket: el precio acordado promedio de lo que se compro; si todavia no hubo cierres, la tasacion promedio.
            List<double> acordados = finalizadas.Where(c => c.Hecho.PrecioAcordado.HasValue).Select(c => c.Hecho.PrecioAcordado.Value).ToList();
            List<double> tasaciones = compras.Where(c => c.Hecho.ValorTasacion.HasValue).Select(c => c.Hecho.ValorTasacion.Value).ToList();
            proyeccion.TicketPromedio = acordados.Count > 0 ? acordados.Average() : tasaciones.Count > 0 ? tasaciones.Average() : 0;

            // Flujo nuevo
            DateTime desdeFlujo = corte.AddDays(-HorizonteDias * PeriodosFlujo);
            proyeccion.ComprasPorPeriodo = (double)compras.Count(c => c.Hecho.Fecha >= desdeFlujo) / PeriodosFlujo;
            List<double> duracionesDesdeInicio = Duraciones(finalizadas, EtapaInicial);
            proyeccion.FactorTiempoNuevas = duracionesDesdeInicio.Count == 0 ? 0
                : duracionesDesdeInicio.Average(d => Math.Max(0, HorizonteDias - d) / HorizonteDias);
            proyeccion.UnidadesFlujoNuevo = proyeccion.ComprasPorPeriodo * proyeccion.TasaFinalizacion * proyeccion.FactorTiempoNuevas;
            proyeccion.MontoFlujoNuevo = proyeccion.UnidadesFlujoNuevo * proyeccion.TicketPromedio;

            // Compras en curso
            double varianzaUnidades = proyeccion.UnidadesFlujoNuevo;
            double varianzaMonto = proyeccion.UnidadesFlujoNuevo * proyeccion.TicketPromedio * proyeccion.TicketPromedio;
            double cajaPendiente = proyeccion.MontoFlujoNuevo;
            foreach (Trayectoria compra in compras.Where(c => !c.ResueltaAntesDe(corte)))
            {
                int etapa = compra.EtapaAl(corte);
                double diasEnEtapa = Dias(compra.Fechas[etapa].Value, corte);
                List<double> duraciones = Duraciones(finalizadas, etapa);
                bool demorada = duraciones.Count > 0 && !duraciones.Any(d => d > diasEnEtapa);
                double enHorizonte = ProbabilidadEnHorizonte(duraciones, diasEnEtapa);
                double finalizar = TasaFinalizacion(resueltas, etapa);
                double probabilidad = finalizar * enHorizonte;

                bool estimado = !compra.Hecho.PrecioAcordado.HasValue;
                double precio = compra.Hecho.PrecioAcordado ?? compra.Hecho.ValorTasacion ?? proyeccion.TicketPromedio;
                proyeccion.ComprasEnCurso.Add(new CompraEnCurso
                {
                    Compra_Id = compra.Hecho.Compra_Id,
                    Numero = compra.Hecho.Numero,
                    Vehiculo_Descripcion = compra.Hecho.Vehiculo_Descripcion,
                    Persona_Descripcion = compra.Hecho.Persona_Descripcion,
                    Marca = compra.Hecho.Marca,
                    Etapa = (CompraStatus)etapa,
                    DiasEnEtapa = (int)Math.Floor(diasEnEtapa),
                    Demorada = demorada,
                    Precio = precio,
                    PrecioEstimado = estimado,
                    MontoPagado = compra.Hecho.MontoPagado,
                    ProbabilidadFinalizar = finalizar,
                    ProbabilidadEnHorizonte = enHorizonte,
                    Probabilidad = probabilidad,
                    MontoEsperado = probabilidad * precio
                });
                varianzaUnidades += probabilidad * (1 - probabilidad);
                varianzaMonto += probabilidad * (1 - probabilidad) * precio * precio;
                cajaPendiente += probabilidad * Math.Max(0, precio - compra.Hecho.MontoPagado);
            }
            proyeccion.ComprasEnCurso = proyeccion.ComprasEnCurso
                .OrderByDescending(c => (int)c.Etapa).ThenByDescending(c => c.Probabilidad).ThenBy(c => c.Numero).ToList();
            proyeccion.UnidadesEnCurso = proyeccion.ComprasEnCurso.Sum(c => c.Probabilidad);
            proyeccion.MontoEnCurso = proyeccion.ComprasEnCurso.Sum(c => c.MontoEsperado);

            proyeccion.Unidades = proyeccion.UnidadesEnCurso + proyeccion.UnidadesFlujoNuevo;
            proyeccion.Monto = proyeccion.MontoEnCurso + proyeccion.MontoFlujoNuevo;
            proyeccion.CajaPendiente = cajaPendiente;
            double margenUnidades = Z90 * Math.Sqrt(varianzaUnidades);
            double margenMonto = Z90 * Math.Sqrt(varianzaMonto);
            proyeccion.UnidadesMinimo = Math.Max(0, proyeccion.Unidades - margenUnidades);
            proyeccion.UnidadesMaximo = proyeccion.Unidades + margenUnidades;
            proyeccion.MontoMinimo = Math.Max(0, proyeccion.Monto - margenMonto);
            proyeccion.MontoMaximo = proyeccion.Monto + margenMonto;
            return proyeccion;
        }

        // Compras cuyo resultado (finalizada o perdida) ya se conocia al corte, dentro de la ventana de tasas.
        private static List<Trayectoria> Resueltas(List<Trayectoria> compras, DateTime corte)
        {
            DateTime desde = corte.AddDays(-VentanaTasasDias);
            return compras.Where(c => c.ResueltaAntesDe(corte) && c.FechaResolucion.Value >= desde).ToList();
        }

        // De las resueltas que llegaron a la etapa, cuantas se finalizaron. Si ninguna llego a esa etapa se usa
        // la etapa anterior mas cercana que tenga datos.
        private static double TasaFinalizacion(List<Trayectoria> resueltas, int etapa)
        {
            for (int e = etapa; e >= EtapaInicial; e--)
            {
                List<Trayectoria> llegaron = resueltas.Where(c => c.EtapaMaxima >= e).ToList();
                if (llegaron.Count > 0)
                    return (double)llegaron.Count(c => c.Finalizada) / llegaron.Count;
            }
            return 0;
        }

        // Dias que tardaron las compras finalizadas desde que entraron a la etapa hasta el cierre.
        private static List<double> Duraciones(List<Trayectoria> finalizadas, int etapa)
        {
            return finalizadas.Select(c => Dias(c.Fechas[etapa].Value, c.Fechas[EtapaFinal].Value)).ToList();
        }

        // P(cerrarse en el horizonte | ya lleva "dias" en la etapa): de las finalizadas que tardaron mas que eso,
        // cuantas cerraron dentro de los HorizonteDias siguientes. Si ya lleva mas que todas, no hay con que
        // comparar y se usa la proporcion que cerro dentro del horizonte desde que entro a la etapa.
        private static double ProbabilidadEnHorizonte(List<double> duraciones, double dias)
        {
            if (duraciones.Count == 0)
                return 0;
            List<double> mayores = duraciones.Where(d => d > dias).ToList();
            if (mayores.Count == 0)
                return (double)duraciones.Count(d => d <= HorizonteDias) / duraciones.Count;
            return (double)mayores.Count(d => d <= dias + HorizonteDias) / mayores.Count;
        }

        private static double Dias(DateTime? desde, DateTime? hasta)
        {
            return Math.Max(0, (hasta.Value - desde.Value).TotalDays);
        }

        // Compras finalizadas en los ultimos MesesHistorial meses calendario; el ultimo es el mes del dia anterior al corte.
        private static List<CompraMensual> ArmarHistorial(List<Trayectoria> compras, DateTime corte)
        {
            List<Trayectoria> finalizadas = compras.Where(c => c.Finalizada && c.FechaResolucion.Value < corte).ToList();
            DateTime ultimoDia = corte.AddDays(-1);
            DateTime ultimoMes = new DateTime(ultimoDia.Year, ultimoDia.Month, 1);
            var historial = new List<CompraMensual>();
            for (int i = MesesHistorial - 1; i >= 0; i--)
            {
                DateTime desde = ultimoMes.AddMonths(-i);
                DateTime hasta = desde.AddMonths(1);
                List<Trayectoria> delMes = finalizadas.Where(c => c.FechaResolucion.Value >= desde && c.FechaResolucion.Value < hasta).ToList();
                historial.Add(new CompraMensual { Anio = desde.Year, Mes = desde.Month, Unidades = delMes.Count, Monto = delMes.Sum(c => c.Hecho.PrecioAcordado ?? 0) });
            }
            return historial;
        }

        // Embudo de las compras resueltas en la ventana: por cada etapa, cuantas llegaron, cuantas pasaron a la
        // siguiente y los dias promedio que tardaron en pasar.
        private static List<EtapaEmbudoCompra> ArmarEtapas(List<Trayectoria> compras, DateTime corte)
        {
            List<Trayectoria> resueltas = Resueltas(compras, corte);
            var etapas = new List<EtapaEmbudoCompra>();
            for (int e = EtapaInicial; e <= UltimaEtapaAbierta; e++)
            {
                List<Trayectoria> llegaron = resueltas.Where(c => c.EtapaMaxima >= e).ToList();
                List<Trayectoria> avanzaron = llegaron.Where(c => c.EtapaMaxima >= e + 1).ToList();
                etapas.Add(new EtapaEmbudoCompra
                {
                    Etapa = (CompraStatus)e,
                    Llegaron = llegaron.Count,
                    Avanzaron = avanzaron.Count,
                    TasaPaso = llegaron.Count == 0 ? 0 : (double)avanzaron.Count / llegaron.Count,
                    DiasPromedio = avanzaron.Count == 0 ? (double?)null : avanzaron.Average(c => Dias(c.Fechas[e], c.Fechas[e + 1]))
                });
            }
            return etapas;
        }

        private static List<PerdidaCompra> ArmarPerdidas(List<Trayectoria> compras, DateTime corte)
        {
            List<Trayectoria> resueltas = Resueltas(compras, corte);
            return resueltas.Where(c => c.Motivo.HasValue)
                .GroupBy(c => c.Motivo.Value)
                .Select(g => new PerdidaCompra { Motivo = g.Key, Etapa = (CompraStatus)g.First().EtapaMaxima, Cantidad = g.Count(),
                    Porcentaje = (double)g.Count() / resueltas.Count })
                .OrderByDescending(p => p.Cantidad).ThenBy(p => p.Motivo)
                .ToList();
        }

        // Prueba hacia atras: que habria proyectado el modelo en cada uno de los periodos anteriores y cuantas
        // compras se finalizaron de verdad. Se omiten los periodos sin ningun dato previo.
        private List<ValidacionProyeccion> ArmarValidaciones(List<Trayectoria> compras, DateTime corte)
        {
            var validaciones = new List<ValidacionProyeccion>();
            for (int k = PeriodosValidacion; k >= 1; k--)
            {
                DateTime desde = corte.AddDays(-HorizonteDias * k);
                DateTime hasta = desde.AddDays(HorizonteDias);
                if (!compras.Any(c => c.Hecho.Fecha < desde))
                    continue;
                validaciones.Add(new ValidacionProyeccion
                {
                    Desde = desde,
                    Hasta = hasta,
                    UnidadesProyectadas = Proyectar(compras, desde).Unidades,
                    UnidadesReales = compras.Count(c => c.Finalizada && c.FechaResolucion.Value >= desde && c.FechaResolucion.Value < hasta)
                });
            }
            return validaciones;
        }

        // Recorrido de una compra: hasta que etapa llego, cuando entro a cada una y como y cuando termino.
        private sealed class Trayectoria
        {
            public readonly HechoCompra Hecho;
            public readonly int EtapaMaxima;
            // Fechas[e] = cuando entro a la etapa e (1 a 8); null para las etapas a las que no llego.
            public readonly DateTime?[] Fechas = new DateTime?[EtapaFinal + 1];
            public readonly bool Finalizada;
            public readonly MotivoPerdidaCompra? Motivo;
            public readonly DateTime? FechaResolucion;

            public Trayectoria(HechoCompra hecho)
            {
                Hecho = hecho;
                EtapaMaxima = EtapaMaximaDe(hecho);

                DateTime?[] registradas = { null, hecho.Fecha, hecho.FechaInspeccion, hecho.FechaTasacion, hecho.FechaFirmaContrato,
                    hecho.FechaDocumentacion, hecho.FechaPago, hecho.FechaTransferencia, hecho.FechaCierre };
                // Una etapa sin registro toma la fecha de la anterior; y ninguna puede ser anterior a la previa.
                Fechas[EtapaInicial] = hecho.Fecha;
                for (int e = EtapaInicial + 1; e <= EtapaMaxima; e++)
                    Fechas[e] = registradas[e].HasValue && registradas[e].Value > Fechas[e - 1].Value ? registradas[e] : Fechas[e - 1];

                if (hecho.Estado == CompraStatus.Finished)
                {
                    Finalizada = true;
                    FechaResolucion = Fechas[EtapaFinal];
                }
                else if (hecho.Estado == CompraStatus.Cancelled || hecho.Estado == CompraStatus.Rejected)
                {
                    Motivo = MotivoDe(hecho);
                    // La perdida en la inspeccion se fecha con la inspeccion; la de la tasada no deja registro propio
                    // y se fecha con la ultima tasacion, que es lo ultimo que paso antes de que se cayera.
                    DateTime? registro = Motivo == MotivoPerdidaCompra.NoSePresento || Motivo == MotivoPerdidaCompra.InspeccionNoApta
                        ? hecho.FechaInspeccion : hecho.FechaUltimaTasacion;
                    DateTime ultimaEtapa = Fechas[EtapaMaxima].Value;
                    FechaResolucion = registro.HasValue && registro.Value > ultimaEtapa ? registro.Value : ultimaEtapa;
                }
            }

            public bool ResueltaAntesDe(DateTime corte)
            {
                return FechaResolucion.HasValue && FechaResolucion.Value < corte;
            }

            // Ultima etapa a la que habia entrado antes del corte. Solo tiene sentido si al corte seguia abierta.
            public int EtapaAl(DateTime corte)
            {
                int etapa = EtapaInicial;
                for (int e = EtapaInicial + 1; e <= Math.Min(EtapaMaxima, UltimaEtapaAbierta); e++)
                    if (Fechas[e].Value < corte)
                        etapa = e;
                return etapa;
            }

            // Rechazada en la inspeccion o cancelada se quedaron en la primera etapa; rechazada en la tasada llego a Tasada.
            private static int EtapaMaximaDe(HechoCompra hecho)
            {
                if (hecho.Estado == CompraStatus.Cancelled)
                    return EtapaInicial;
                if (hecho.Estado == CompraStatus.Rejected)
                    return hecho.MotivoRechazo == RejectionReason.AppraisalRejected || hecho.MotivoRechazo == RejectionReason.ContractNotSigned
                        ? (int)CompraStatus.Appraised : EtapaInicial;
                int estado = (int)hecho.Estado;
                return estado < EtapaInicial || estado > EtapaFinal ? EtapaInicial : estado;
            }

            private static MotivoPerdidaCompra MotivoDe(HechoCompra hecho)
            {
                if (hecho.Estado == CompraStatus.Cancelled)
                    return MotivoPerdidaCompra.NoSePresento;
                if (hecho.MotivoRechazo == RejectionReason.AppraisalRejected)
                    return MotivoPerdidaCompra.TasacionRechazada;
                if (hecho.MotivoRechazo == RejectionReason.ContractNotSigned)
                    return MotivoPerdidaCompra.ContratoNoFirmado;
                return MotivoPerdidaCompra.InspeccionNoApta;
            }
        }
    }
}
