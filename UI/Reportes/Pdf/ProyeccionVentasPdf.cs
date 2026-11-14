using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using CarAgency.BE;
using CarAgency.Security.Session;
using Security.Session;

namespace UI.Reportes.Pdf
{
    // PDF del reporte inteligente de PN1. Solo dibuja: todos los numeros vienen calculados en ProyeccionVentas.
    public static class ProyeccionVentasPdf
    {
        public static string Generar(ProyeccionVentas p, string carpeta)
        {
            DateTime datosHasta = p.FechaCorte.AddDays(-1);
            DateTime horizonteFin = p.HorizonteHasta.AddDays(-1);
            string usuario = SessionHandler.Instance.User == null ? "" : SessionHandler.Instance.User.NombreUsuario;

            var pdf = new ReportePdfBuilder(
                T("SalesForecastTitle"),
                string.Format(T("SalesForecastSubtitle"), Dia(datosHasta), Dia(p.FechaCorte), Dia(horizonteFin)),
                string.Format(T("ReportFooter"), DateTime.Now.Year, usuario),
                T("ReportPageNumber"));

            if (!p.HistoriaSuficiente)
                pdf.Aviso(string.Format(T("SalesForecastNoHistory"), ProyeccionVentasCalculator.VentanaTasasDias));

            // 1. Resumen
            pdf.Seccion(T("SalesForecastSummary"));
            pdf.Indicadores(new List<Indicador>
            {
                new Indicador { Titulo = T("SalesForecastUnits"), Valor = Unidades(p.Unidades), Destacado = true,
                    Detalle = string.Format(T("SalesForecastRange"), Unidades(p.UnidadesMinimo), Unidades(p.UnidadesMaximo)) },
                new Indicador { Titulo = T("SalesForecastAmount"), Valor = PlataCorta(p.Monto), Destacado = true,
                    Detalle = string.Format(T("SalesForecastRange"), PlataCorta(p.MontoMinimo), PlataCorta(p.MontoMaximo)) },
                new Indicador { Titulo = T("SalesForecastReservationRate"), Valor = Porcentaje(p.TasaReservaFactura),
                    Detalle = string.Format(TN("SalesForecastReservationRateDetail", p.ReservasResueltas), p.ReservasResueltas) },
                new Indicador { Titulo = T("SalesForecastQuotationRate"), Valor = Porcentaje(p.TasaCotizacionReserva),
                    Detalle = string.Format(TN("SalesForecastQuotationRateDetail", p.CotizacionesResueltas), p.CotizacionesResueltas) }
            });

            // 2. Historial con la barra proyectada al final
            pdf.Seccion(T("SalesForecastHistory"));
            var barras = p.Historial.Select(m => new BarraGrafico
            {
                Etiqueta = m.Mes.ToString("00") + "/" + (m.Anio % 100).ToString("00"),
                Valor = m.Unidades,
                Texto = m.Unidades.ToString()
            }).ToList();
            barras.Add(new BarraGrafico
            {
                Etiqueta = T("SalesForecastChartProjectedLabel"),
                Valor = p.Unidades,
                Texto = Unidades(p.Unidades),
                Proyectada = true,
                Minimo = p.UnidadesMinimo,
                Maximo = p.UnidadesMaximo
            });
            pdf.GraficoBarras(barras, T("SalesForecastChartReal"), T("SalesForecastChartProjected"));
            pdf.Nota(string.Format(T("SalesForecastHistoryNote"), Dia(datosHasta)));

            // 3. Composicion del numero
            pdf.Seccion(T("SalesForecastComposition"));
            pdf.Tabla(
                new[] { T("SalesForecastPart"), T("SalesForecastPartUnits"), T("SalesForecastPartAmount") },
                new double[] { 5, 2, 2.5 },
                new[] { false, true, true },
                new List<string[]>
                {
                    new[] { string.Format(T("SalesForecastPartReservations"), p.ReservasVigentes.Count), Unidades(p.UnidadesReservasVigentes), Plata(p.MontoReservasVigentes) },
                    new[] { string.Format(T("SalesForecastPartFlow"), Unidades(p.ReservasNuevasEsperadas)), Unidades(p.UnidadesFlujoNuevo), Plata(p.MontoFlujoNuevo) },
                    new[] { T("SalesForecastTotal"), Unidades(p.Unidades), Plata(p.Monto) }
                });

            // 4. Reservas vigentes
            pdf.Seccion(T("SalesForecastReservations"));
            if (p.ReservasVigentes.Count == 0)
                pdf.Parrafo(T("SalesForecastNoReservations"));
            else
                pdf.Tabla(
                    new[] { T("SalesForecastColVehicle"), T("SalesForecastColPerson"), T("SalesForecastColExpiration"),
                        T("SalesForecastColPrice"), T("SalesForecastColProbability"), T("SalesForecastColExpected") },
                    new double[] { 3.8, 3.2, 2.1, 2.3, 2.4, 2.3 },
                    new[] { false, false, false, true, true, true },
                    p.ReservasVigentes.Select(o => new[] { o.Vehiculo_Descripcion, o.Persona_Descripcion, Dia(o.FechaVencimiento),
                        Plata(o.Precio), Porcentaje(o.Probabilidad), Plata(o.MontoEsperado) }));

            // 5. Por marca
            if (p.Marcas.Count > 0)
            {
                pdf.Seccion(T("SalesForecastBrands"));
                pdf.Tabla(
                    new[] { T("SalesForecastColBrand"), string.Format(T("SalesForecastColSold"), ProyeccionVentasCalculator.VentanaTasasDias),
                        T("SalesForecastColReserved"), T("SalesForecastColExpectedUnits") },
                    new double[] { 4, 2.5, 2.5, 2.5 },
                    new[] { false, true, true, true },
                    p.Marcas.Select(m => new[] { m.Marca, m.UnidadesVendidas.ToString(), m.ReservasVigentes.ToString(), Unidades(m.UnidadesEsperadas) }));
                pdf.Nota(string.Format(T("SalesForecastBrandsNote"), ProyeccionVentasCalculator.HorizonteDias * ProyeccionVentasCalculator.PeriodosFlujo));
            }

            // 6. Validacion hacia atras
            pdf.Seccion(T("SalesForecastValidation"));
            pdf.Parrafo(T("SalesForecastValidationText"));
            if (p.Validaciones.Count > 0)
                pdf.Tabla(
                    new[] { T("SalesForecastColPeriod"), T("SalesForecastColProjected"), T("SalesForecastColActual"), T("SalesForecastColDifference") },
                    new double[] { 4, 2.5, 2.5, 2.5 },
                    new[] { false, true, true, true },
                    p.Validaciones.Select(v => new[] { Dia(v.Desde) + " - " + Dia(v.Hasta.AddDays(-1)), Unidades(v.UnidadesProyectadas),
                        v.UnidadesReales.ToString(), Unidades(v.UnidadesProyectadas - v.UnidadesReales) }));
            pdf.Parrafo(p.ErrorValidacion.HasValue
                ? string.Format(T("SalesForecastValidationError"), Porcentaje(p.ErrorValidacion.Value))
                : T("SalesForecastValidationNone"));

            // 7. Metodologia: la formula con los valores que se usaron
            pdf.Seccion(T("SalesForecastMethod"));
            pdf.Parrafo(string.Format(T("SalesForecastMethod1"), ProyeccionVentasCalculator.VentanaTasasDias, Porcentaje(p.TasaReservaFactura)));
            pdf.Parrafo(string.Format(T("SalesForecastMethod2"), Porcentaje(p.TasaCotizacionReserva)));
            pdf.Parrafo(T("SalesForecastMethod3"));
            pdf.Parrafo(string.Format(T("SalesForecastMethod4"),
                ProyeccionVentasCalculator.HorizonteDias * ProyeccionVentasCalculator.PeriodosFlujo,
                Unidades(p.CotizacionesPorPeriodo), Unidades(p.ReservasDirectasPorPeriodo), ProyeccionVentasCalculator.HorizonteDias,
                Porcentaje(p.TasaCotizacionReserva), Unidades(p.ReservasNuevasEsperadas), Plata(p.TicketPromedio)));
            pdf.Parrafo(T("SalesForecastMethod5"));

            string ruta = System.IO.Path.Combine(carpeta, "CarAgency " + T("SalesForecastTitle") + " " + datosHasta.ToString("yyyy-MM-dd") + ".pdf");
            pdf.Guardar(ruta);
            return ruta;
        }

        private static string T(string tag)
        {
            return LanguageService.GetTagText(tag);
        }

        // Textos con una cantidad entera: con 1 se usa la variante en singular (<tag>_One).
        private static string TN(string tag, int cantidad)
        {
            return T(cantidad == 1 ? tag + "_One" : tag);
        }

        private static string Dia(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy");
        }

        private static string Unidades(double valor)
        {
            return valor.ToString("N1");
        }

        private static string Plata(double valor)
        {
            return "$ " + valor.ToString("N0");
        }

        // Para los recuadros del resumen, donde el monto completo no entra: "$ 400.0 M". Los espacios no cortan
        // la linea (U+00A0), asi un monto nunca queda partido entre dos renglones.
        private static string PlataCorta(double valor)
        {
            return Math.Abs(valor) >= 1000000 ? "$ " + (valor / 1000000).ToString("N1") + " M" : Plata(valor).Replace(' ', ' ');
        }

        private static string Porcentaje(double valor)
        {
            return valor.ToString("P0");
        }
    }
}
