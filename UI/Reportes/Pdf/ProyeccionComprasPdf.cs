using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using CarAgency.BE;
using CarAgency.Security.Session;
using Security.Session;

namespace UI.Reportes.Pdf
{
    // PDF del reporte inteligente de PN2. Solo dibuja: todos los numeros vienen calculados en ProyeccionCompras.
    public static class ProyeccionComprasPdf
    {
        public static string Generar(ProyeccionCompras p, string carpeta)
        {
            DateTime datosHasta = p.FechaCorte.AddDays(-1);
            DateTime horizonteFin = p.HorizonteHasta.AddDays(-1);
            string usuario = SessionHandler.Instance.User == null ? "" : SessionHandler.Instance.User.NombreUsuario;

            var pdf = new ReportePdfBuilder(
                T("PurchaseForecastTitle"),
                string.Format(T("SalesForecastSubtitle"), Dia(datosHasta), Dia(p.FechaCorte), Dia(horizonteFin)),
                string.Format(T("ReportFooter"), DateTime.Now.Year, usuario),
                T("ReportPageNumber"));

            if (!p.HistoriaSuficiente)
                pdf.Aviso(string.Format(TN("PurchaseForecastNoHistory", p.ComprasResueltas), p.ComprasResueltas, ProyeccionComprasCalculator.VentanaTasasDias,
                    ProyeccionComprasCalculator.MinimoComprasResueltas));

            // 1. Resumen
            pdf.Seccion(T("SalesForecastSummary"));
            pdf.Indicadores(new List<Indicador>
            {
                new Indicador { Titulo = T("PurchaseForecastUnits"), Valor = Unidades(p.Unidades), Destacado = true,
                    Detalle = string.Format(T("SalesForecastRange"), Unidades(p.UnidadesMinimo), Unidades(p.UnidadesMaximo)) },
                new Indicador { Titulo = T("PurchaseForecastAmount"), Valor = PlataCorta(p.Monto), Destacado = true,
                    Detalle = string.Format(T("SalesForecastRange"), PlataCorta(p.MontoMinimo), PlataCorta(p.MontoMaximo)) },
                new Indicador { Titulo = T("PurchaseForecastCash"), Valor = PlataCorta(p.CajaPendiente),
                    Detalle = T("PurchaseForecastCashDetail") },
                new Indicador { Titulo = T("PurchaseForecastFinishRate"), Valor = Porcentaje(p.TasaFinalizacion),
                    Detalle = string.Format(TN("PurchaseForecastFinishRateDetail", p.ComprasResueltas), p.ComprasResueltas) }
            });

            // 2. Hallazgos: lo que el embudo dice que hay que mirar
            List<string> hallazgos = Hallazgos(p);
            if (hallazgos.Count > 0)
            {
                pdf.Seccion(T("PurchaseForecastFindings"));
                foreach (string hallazgo in hallazgos)
                    pdf.Parrafo(hallazgo);
            }

            // 3. Historial con la barra proyectada al final
            pdf.Seccion(T("PurchaseForecastHistory"));
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
            pdf.GraficoBarras(barras, T("PurchaseForecastChartReal"), T("SalesForecastChartProjected"));
            pdf.Nota(string.Format(T("PurchaseForecastHistoryNote"), Dia(datosHasta)));

            // 4. Composicion del numero
            pdf.Seccion(T("SalesForecastComposition"));
            pdf.Tabla(
                new[] { T("SalesForecastPart"), T("SalesForecastPartUnits"), T("SalesForecastPartAmount") },
                new double[] { 5, 2, 2.5 },
                new[] { false, true, true },
                new List<string[]>
                {
                    new[] { string.Format(T("PurchaseForecastPartOpen"), p.ComprasEnCurso.Count), Unidades(p.UnidadesEnCurso), Plata(p.MontoEnCurso) },
                    new[] { string.Format(T("PurchaseForecastPartFlow"), Unidades(p.ComprasPorPeriodo)), Unidades(p.UnidadesFlujoNuevo), Plata(p.MontoFlujoNuevo) },
                    new[] { T("SalesForecastTotal"), Unidades(p.Unidades), Plata(p.Monto) }
                });

            // 5. Compras en curso
            pdf.Seccion(T("PurchaseForecastOpen"));
            if (p.ComprasEnCurso.Count == 0)
                pdf.Parrafo(T("PurchaseForecastNoOpen"));
            else
            {
                // Siete columnas en el ancho de la hoja: los montos van en millones para que entren el vehiculo y la etapa.
                pdf.Tabla(
                    new[] { T("PurchaseForecastColNumber"), T("SalesForecastColVehicle"), T("PurchaseForecastColStage"), T("PurchaseForecastColDays"),
                        T("SalesForecastColPrice"), T("PurchaseForecastColProbability"), T("SalesForecastColExpected") },
                    new double[] { 30, 165, 125, 42, 60, 40, 53 },
                    new[] { true, false, false, true, true, true, true },
                    p.ComprasEnCurso.Select(c => new[] { c.Numero.ToString(), c.Vehiculo_Descripcion, Etapa(c.Etapa),
                        c.DiasEnEtapa + (c.Demorada ? " (!)" : ""), PlataCorta(c.Precio) + (c.PrecioEstimado ? " *" : ""),
                        Porcentaje(c.Probabilidad), PlataCorta(c.MontoEsperado) }));
                if (p.ComprasEnCurso.Any(c => c.PrecioEstimado))
                    pdf.Nota(T("PurchaseForecastEstimatedPriceNote"));
                if (p.ComprasEnCurso.Any(c => c.Demorada))
                    pdf.Nota(T("PurchaseForecastDelayedNote"));
            }

            // 6. Embudo por etapa
            pdf.Seccion(T("PurchaseForecastFunnel"));
            pdf.Tabla(
                new[] { T("PurchaseForecastColStage"), T("PurchaseForecastColReached"), T("PurchaseForecastColAdvanced"),
                    T("PurchaseForecastColPassRate"), T("PurchaseForecastColAverageDays") },
                new double[] { 4, 2.2, 2.2, 2.2, 2.6 },
                new[] { false, true, true, true, true },
                p.Etapas.Select(e => new[] { Etapa(e.Etapa), e.Llegaron.ToString(), e.Avanzaron.ToString(),
                    e.Llegaron == 0 ? "-" : Porcentaje(e.TasaPaso), e.DiasPromedio.HasValue ? e.DiasPromedio.Value.ToString("N1") : "-" }));
            pdf.Nota(string.Format(T("PurchaseForecastFunnelNote"), ProyeccionComprasCalculator.VentanaTasasDias));

            // 7. Motivos de perdida
            if (p.Perdidas.Count > 0)
            {
                pdf.Seccion(T("PurchaseForecastLosses"));
                pdf.Tabla(
                    new[] { T("PurchaseForecastColReason"), T("PurchaseForecastColPurchases"), T("PurchaseForecastColShare") },
                    new double[] { 6, 2.5, 3 },
                    new[] { false, true, true },
                    p.Perdidas.Select(x => new[] { Motivo(x.Motivo), x.Cantidad.ToString(), Porcentaje(x.Porcentaje) }));
            }

            // 8. Validacion hacia atras
            pdf.Seccion(T("SalesForecastValidation"));
            pdf.Parrafo(T("PurchaseForecastValidationText"));
            if (p.Validaciones.Count > 0)
                pdf.Tabla(
                    new[] { T("SalesForecastColPeriod"), T("SalesForecastColProjected"), T("SalesForecastColActual"), T("SalesForecastColDifference") },
                    new double[] { 4, 2.5, 2.5, 2.5 },
                    new[] { false, true, true, true },
                    p.Validaciones.Select(v => new[] { Dia(v.Desde) + " - " + Dia(v.Hasta.AddDays(-1)), Unidades(v.UnidadesProyectadas),
                        v.UnidadesReales.ToString(), Unidades(v.UnidadesProyectadas - v.UnidadesReales) }));
            pdf.Parrafo(p.ErrorValidacion.HasValue
                ? string.Format(T("PurchaseForecastValidationError"), Porcentaje(p.ErrorValidacion.Value))
                : T("PurchaseForecastValidationNone"));

            // 9. Metodologia: la formula con los valores que se usaron
            pdf.Seccion(T("SalesForecastMethod"));
            pdf.Parrafo(string.Format(T("PurchaseForecastMethod1"), ProyeccionComprasCalculator.VentanaTasasDias, Porcentaje(p.TasaFinalizacion)));
            pdf.Parrafo(T("PurchaseForecastMethod2"));
            pdf.Parrafo(string.Format(T("PurchaseForecastMethod3"), ProyeccionComprasCalculator.HorizonteDias));
            pdf.Parrafo(string.Format(T("PurchaseForecastMethod4"),
                ProyeccionComprasCalculator.HorizonteDias * ProyeccionComprasCalculator.PeriodosFlujo, Unidades(p.ComprasPorPeriodo),
                ProyeccionComprasCalculator.HorizonteDias, Porcentaje(p.TasaFinalizacion), Porcentaje(p.FactorTiempoNuevas),
                Unidades(p.UnidadesFlujoNuevo), Plata(p.TicketPromedio)));
            pdf.Parrafo(T("PurchaseForecastMethod5"));
            pdf.Parrafo(T("PurchaseForecastMethod6"));

            string ruta = System.IO.Path.Combine(carpeta, "CarAgency " + T("PurchaseForecastTitle") + " " + datosHasta.ToString("yyyy-MM-dd") + ".pdf");
            pdf.Guardar(ruta);
            return ruta;
        }

        private static List<string> Hallazgos(ProyeccionCompras p)
        {
            var hallazgos = new List<string>();
            if (p.DuracionPromedio > 0)
                hallazgos.Add(string.Format(T("PurchaseForecastFindingDuration"), p.DuracionPromedio.ToString("N1")));
            EtapaEmbudoCompra lenta = p.Etapas.FirstOrDefault(e => p.EtapaMasLenta.HasValue && (int)e.Etapa == p.EtapaMasLenta.Value);
            if (lenta != null && lenta.DiasPromedio.HasValue)
                hallazgos.Add(string.Format(T("PurchaseForecastFindingSlowest"), Etapa(lenta.Etapa), lenta.DiasPromedio.Value.ToString("N1")));
            EtapaEmbudoCompra perdida = p.Etapas.FirstOrDefault(e => p.EtapaConMasPerdidas.HasValue && (int)e.Etapa == p.EtapaConMasPerdidas.Value);
            if (perdida != null)
            {
                string texto = string.Format(T("PurchaseForecastFindingLosses"), Etapa(perdida.Etapa), Porcentaje(1 - perdida.TasaPaso));
                PerdidaCompra principal = p.Perdidas.FirstOrDefault(x => x.Etapa == perdida.Etapa);
                if (principal != null)
                    texto += " " + string.Format(T("PurchaseForecastFindingMainReason"), Motivo(principal.Motivo), principal.Cantidad);
                hallazgos.Add(texto);
            }
            int demoradas = p.ComprasEnCurso.Count(c => c.Demorada);
            if (demoradas > 0)
                hallazgos.Add(string.Format(TN("PurchaseForecastFindingDelayed", demoradas), demoradas));
            return hallazgos;
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

        private static string Etapa(CompraStatus etapa)
        {
            return T("AcquisitionStatus_" + etapa);
        }

        private static string Motivo(MotivoPerdidaCompra motivo)
        {
            return T("PurchaseForecastReason_" + motivo);
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
