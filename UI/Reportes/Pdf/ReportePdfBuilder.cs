using System;
using System.Collections.Generic;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace UI.Reportes.Pdf
{
    // Arma los PDF de los reportes de a bloques (secciones, parrafos, indicadores, tablas y graficos).
    // Lleva la posicion vertical y, si un bloque no entra, agrega una pagina y repite el encabezado:
    // quien lo usa no calcula coordenadas. Las tablas largas siguen en la pagina siguiente con sus titulos.
    public sealed class ReportePdfBuilder
    {
        private const double Margen = 40;
        private const double AltoPie = 24;
        private const double AltoFila = 16;

        private static readonly XColor ColorPrimario = XColor.FromArgb(43, 108, 176);
        private static readonly XColor ColorSuave = XColor.FromArgb(226, 236, 247);
        private static readonly XColor ColorZebra = XColor.FromArgb(246, 248, 251);
        private static readonly XColor ColorLinea = XColor.FromArgb(211, 219, 228);
        private static readonly XColor ColorTextoSecundario = XColor.FromArgb(102, 114, 127);
        private static readonly XColor ColorProyectado = XColor.FromArgb(143, 97, 52);
        private static readonly XColor ColorProyectadoSuave = XColor.FromArgb(243, 237, 228);

        private readonly XFont fuenteMarca = new XFont("Verdana", 16, XFontStyleEx.Bold);
        private readonly XFont fuenteTitulo = new XFont("Verdana", 13, XFontStyleEx.Bold);
        private readonly XFont fuenteSeccion = new XFont("Verdana", 11, XFontStyleEx.Bold);
        private readonly XFont fuenteTexto = new XFont("Verdana", 8.5);
        private readonly XFont fuenteNegrita = new XFont("Verdana", 8.5, XFontStyleEx.Bold);
        private readonly XFont fuenteChica = new XFont("Verdana", 7);
        private readonly XFont fuenteIndicador = new XFont("Verdana", 15, XFontStyleEx.Bold);

        private readonly PdfDocument document = new PdfDocument();
        private readonly string titulo;
        private readonly string subtitulo;
        private readonly string pie;
        private readonly string formatoPagina;
        private PdfPage page;
        private XGraphics gfx;
        private double y;

        // formatoPagina recibe el numero de pagina y el total, por ejemplo "Página {0} de {1}".
        public ReportePdfBuilder(string titulo, string subtitulo, string pie, string formatoPagina)
        {
            this.titulo = titulo;
            this.subtitulo = subtitulo;
            this.pie = pie;
            this.formatoPagina = formatoPagina;
            document.Info.Title = "CarAgency® " + titulo;
            NuevaPagina();
        }

        private double Izquierda { get { return Margen; } }
        private double Ancho { get { return page.Width.Point - 2 * Margen; } }
        private double Inferior { get { return page.Height.Point - Margen - AltoPie; } }

        #region Bloques
        public void Seccion(string texto)
        {
            // Un titulo de seccion solo al pie de la pagina no sirve: se pide lugar para el titulo (32) y para lo
            // primero que va debajo, que entra en lo que ocupa una tabla con su encabezado y dos filas.
            Asegurar(90);
            y += 6;
            gfx.DrawString(texto, fuenteSeccion, new XSolidBrush(ColorPrimario), new XRect(Izquierda, y, Ancho, 16), XStringFormats.TopLeft);
            y += 18;
            gfx.DrawLine(new XPen(ColorLinea, 0.75), Izquierda, y, Izquierda + Ancho, y);
            y += 8;
        }

        public void Parrafo(string texto)
        {
            Parrafo(texto, fuenteTexto, XColors.Black);
        }

        public void Nota(string texto)
        {
            Parrafo(texto, fuenteChica, ColorTextoSecundario);
        }

        public void Aviso(string texto)
        {
            List<string> lineas = Partir(texto, fuenteNegrita, Ancho - 16);
            double alto = lineas.Count * 12 + 12;
            Asegurar(alto);
            gfx.DrawRectangle(new XPen(ColorProyectado, 0.75), new XSolidBrush(ColorProyectadoSuave), Izquierda, y, Ancho, alto);
            double linea = y + 6;
            foreach (string texto2 in lineas)
            {
                gfx.DrawString(texto2, fuenteNegrita, new XSolidBrush(ColorProyectado), new XRect(Izquierda + 8, linea, Ancho - 16, 12), XStringFormats.TopLeft);
                linea += 12;
            }
            y += alto + 8;
        }

        // Fila de recuadros con un numero grande: titulo arriba, valor al medio y detalle abajo.
        public void Indicadores(IList<Indicador> indicadores)
        {
            if (indicadores == null || indicadores.Count == 0)
                return;
            const double separacion = 8, altoLineaDetalle = 9;
            double ancho = (Ancho - separacion * (indicadores.Count - 1)) / indicadores.Count;
            // El detalle puede ocupar hasta dos lineas; todos los recuadros toman el alto del mas alto.
            List<List<string>> detalles = indicadores.Select(i => Partir(i.Detalle ?? "", fuenteChica, ancho - 12).Take(2).ToList()).ToList();
            double alto = 53 + altoLineaDetalle * detalles.Max(d => d.Count);
            Asegurar(alto + 8);
            for (int i = 0; i < indicadores.Count; i++)
            {
                Indicador indicador = indicadores[i];
                double x = Izquierda + i * (ancho + separacion);
                XColor fondo = indicador.Destacado ? ColorSuave : ColorZebra;
                gfx.DrawRectangle(new XPen(ColorLinea, 0.75), new XSolidBrush(fondo), x, y, ancho, alto);
                gfx.DrawString(Recortar(indicador.Titulo, fuenteChica, ancho - 12), fuenteChica, new XSolidBrush(ColorTextoSecundario),
                    new XRect(x + 6, y + 6, ancho - 12, 10), XStringFormats.TopLeft);
                gfx.DrawString(Recortar(indicador.Valor, fuenteIndicador, ancho - 12), fuenteIndicador, new XSolidBrush(ColorPrimario),
                    new XRect(x + 6, y + 19, ancho - 12, 20), XStringFormats.TopLeft);
                for (int l = 0; l < detalles[i].Count; l++)
                    gfx.DrawString(Recortar(detalles[i][l], fuenteChica, ancho - 12), fuenteChica, new XSolidBrush(ColorTextoSecundario),
                        new XRect(x + 6, y + 45 + l * altoLineaDetalle, ancho - 12, 10), XStringFormats.TopLeft);
            }
            y += alto + 10;
        }

        // anchos son proporciones (se normalizan al ancho util); derecha marca las columnas numericas.
        public void Tabla(string[] encabezados, double[] anchos, bool[] derecha, IEnumerable<string[]> filas)
        {
            double total = anchos.Sum();
            double[] columnas = anchos.Select(a => a / total * Ancho).ToArray();
            List<string[]> lista = filas.ToList();

            Asegurar(AltoFila * Math.Min(3, lista.Count + 1) + 4);
            DibujarEncabezadoTabla(encabezados, columnas, derecha);
            for (int i = 0; i < lista.Count; i++)
            {
                if (y + AltoFila > Inferior)
                {
                    NuevaPagina();
                    DibujarEncabezadoTabla(encabezados, columnas, derecha);
                }
                if (i % 2 == 1)
                    gfx.DrawRectangle(new XSolidBrush(ColorZebra), Izquierda, y, Ancho, AltoFila);
                DibujarFila(lista[i], columnas, derecha, fuenteTexto, XColors.Black);
                y += AltoFila;
            }
            gfx.DrawLine(new XPen(ColorLinea, 0.75), Izquierda, y, Izquierda + Ancho, y);
            y += 10;
        }

        // Barras verticales: las reales en el color principal y las proyectadas en el secundario,
        // con su rango minimo-maximo como una linea vertical.
        public void GraficoBarras(IList<BarraGrafico> barras, string leyendaReal, string leyendaProyectada)
        {
            if (barras == null || barras.Count == 0)
                return;
            const double alto = 190, ejeIzquierdo = 34, alturaEtiquetas = 14, alturaLeyenda = 18;
            Asegurar(alto + 10);

            double tope = barras.Max(b => Math.Max(b.Valor, b.Maximo ?? 0));
            double escala = EscalaRedonda(tope);
            double xPlot = Izquierda + ejeIzquierdo;
            double anchoPlot = Ancho - ejeIzquierdo;
            double yPlot = y + 12;
            double altoPlot = alto - 12 - alturaEtiquetas - alturaLeyenda;
            double baseY = yPlot + altoPlot;

            var pluma = new XPen(ColorLinea, 0.5);
            var textoEje = new XSolidBrush(ColorTextoSecundario);
            for (int i = 0; i <= 4; i++)
            {
                double valor = escala * i / 4;
                double lineaY = baseY - altoPlot * i / 4;
                gfx.DrawLine(pluma, xPlot, lineaY, xPlot + anchoPlot, lineaY);
                gfx.DrawString(FormatoEje(valor), fuenteChica, textoEje, new XRect(Izquierda, lineaY - 5, ejeIzquierdo - 4, 10), XStringFormats.CenterRight);
            }

            double slot = anchoPlot / barras.Count;
            double anchoBarra = Math.Min(28, slot * 0.62);
            for (int i = 0; i < barras.Count; i++)
            {
                BarraGrafico barra = barras[i];
                double centro = xPlot + slot * i + slot / 2;
                double altoBarra = escala == 0 ? 0 : altoPlot * barra.Valor / escala;
                XColor color = barra.Proyectada ? ColorProyectado : ColorPrimario;
                if (barra.Proyectada)
                    gfx.DrawRectangle(new XPen(color, 1), new XSolidBrush(ColorProyectadoSuave), centro - anchoBarra / 2, baseY - altoBarra, anchoBarra, altoBarra);
                else
                    gfx.DrawRectangle(new XSolidBrush(color), centro - anchoBarra / 2, baseY - altoBarra, anchoBarra, altoBarra);

                if (barra.Minimo.HasValue && barra.Maximo.HasValue && escala > 0)
                {
                    double yMin = baseY - altoPlot * barra.Minimo.Value / escala;
                    double yMax = baseY - altoPlot * barra.Maximo.Value / escala;
                    var rango = new XPen(color, 1);
                    gfx.DrawLine(rango, centro, yMin, centro, yMax);
                    gfx.DrawLine(rango, centro - 4, yMin, centro + 4, yMin);
                    gfx.DrawLine(rango, centro - 4, yMax, centro + 4, yMax);
                }

                double yTexto = baseY - Math.Max(altoBarra, barra.Maximo.HasValue && escala > 0 ? altoPlot * barra.Maximo.Value / escala : 0) - 11;
                gfx.DrawString(barra.Texto ?? "", fuenteChica, new XSolidBrush(color), new XRect(centro - slot / 2, yTexto, slot, 10), XStringFormats.Center);
                gfx.DrawString(Recortar(barra.Etiqueta, fuenteChica, slot), fuenteChica, textoEje, new XRect(centro - slot / 2, baseY + 3, slot, 10), XStringFormats.TopCenter);
            }
            gfx.DrawLine(new XPen(ColorTextoSecundario, 0.75), xPlot, baseY, xPlot + anchoPlot, baseY);

            double yLeyenda = baseY + alturaEtiquetas + 4;
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario), xPlot, yLeyenda + 1, 8, 8);
            gfx.DrawString(leyendaReal, fuenteChica, textoEje, new XRect(xPlot + 12, yLeyenda, 200, 10), XStringFormats.TopLeft);
            gfx.DrawRectangle(new XPen(ColorProyectado, 1), new XSolidBrush(ColorProyectadoSuave), xPlot + 212, yLeyenda + 1, 8, 8);
            gfx.DrawString(leyendaProyectada, fuenteChica, textoEje, new XRect(xPlot + 224, yLeyenda, 260, 10), XStringFormats.TopLeft);
            y += alto + 6;
        }

        public void Espacio(double alto)
        {
            y += alto;
        }

        // Escribe el pie con el numero de pagina en todas las hojas y guarda el archivo.
        public void Guardar(string ruta)
        {
            gfx.Dispose();
            gfx = null;
            int total = document.PageCount;
            for (int i = 0; i < total; i++)
            {
                PdfPage hoja = document.Pages[i];
                using (XGraphics g = XGraphics.FromPdfPage(hoja))
                {
                    double yPie = hoja.Height.Point - Margen - 10;
                    g.DrawLine(new XPen(ColorLinea, 0.75), Margen, yPie - 4, hoja.Width.Point - Margen, yPie - 4);
                    var brush = new XSolidBrush(ColorTextoSecundario);
                    g.DrawString(pie, fuenteChica, brush, new XRect(Margen, yPie, hoja.Width.Point - 2 * Margen, 10), XStringFormats.TopLeft);
                    g.DrawString(string.Format(formatoPagina, i + 1, total), fuenteChica, brush,
                        new XRect(Margen, yPie, hoja.Width.Point - 2 * Margen, 10), XStringFormats.TopRight);
                }
            }
            document.Save(ruta);
            document.Close();
        }
        #endregion

        #region Dibujo
        private void NuevaPagina()
        {
            if (gfx != null)
                gfx.Dispose();
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            y = Margen;

            gfx.DrawString("CarAgency®", fuenteMarca, new XSolidBrush(ColorPrimario), new XRect(Izquierda, y, Ancho, 20), XStringFormats.TopLeft);
            gfx.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fuenteChica, new XSolidBrush(ColorTextoSecundario),
                new XRect(Izquierda, y + 4, Ancho, 10), XStringFormats.TopRight);
            y += 24;
            gfx.DrawString(titulo, fuenteTitulo, XBrushes.Black, new XRect(Izquierda, y, Ancho, 16), XStringFormats.TopLeft);
            y += 18;
            if (!string.IsNullOrEmpty(subtitulo))
            {
                gfx.DrawString(subtitulo, fuenteTexto, new XSolidBrush(ColorTextoSecundario), new XRect(Izquierda, y, Ancho, 12), XStringFormats.TopLeft);
                y += 14;
            }
            gfx.DrawRectangle(new XSolidBrush(ColorPrimario), Izquierda, y + 2, Ancho, 2);
            y += 14;
        }

        private void Asegurar(double alto)
        {
            if (y + alto > Inferior)
                NuevaPagina();
        }

        private void Parrafo(string texto, XFont fuente, XColor color)
        {
            double altoLinea = fuente.Size * 1.45;
            var brush = new XSolidBrush(color);
            foreach (string linea in Partir(texto ?? "", fuente, Ancho))
            {
                Asegurar(altoLinea);
                gfx.DrawString(linea, fuente, brush, new XRect(Izquierda, y, Ancho, altoLinea), XStringFormats.TopLeft);
                y += altoLinea;
            }
            y += 4;
        }

        private void DibujarEncabezadoTabla(string[] encabezados, double[] columnas, bool[] derecha)
        {
            gfx.DrawRectangle(new XSolidBrush(ColorSuave), Izquierda, y, Ancho, AltoFila + 2);
            DibujarFila(encabezados, columnas, derecha, fuenteNegrita, ColorPrimario);
            y += AltoFila + 2;
        }

        private void DibujarFila(string[] celdas, double[] columnas, bool[] derecha, XFont fuente, XColor color)
        {
            var brush = new XSolidBrush(color);
            double x = Izquierda;
            for (int c = 0; c < columnas.Length; c++)
            {
                string texto = c < celdas.Length ? celdas[c] ?? "" : "";
                var rect = new XRect(x + 4, y, columnas[c] - 8, AltoFila);
                bool alDerecha = derecha != null && c < derecha.Length && derecha[c];
                gfx.DrawString(Recortar(texto, fuente, columnas[c] - 8), fuente, brush, rect, alDerecha ? XStringFormats.CenterRight : XStringFormats.CenterLeft);
                x += columnas[c];
            }
        }

        // Corta el texto con "…" si no entra en el ancho.
        private string Recortar(string texto, XFont fuente, double ancho)
        {
            if (string.IsNullOrEmpty(texto) || gfx.MeasureString(texto, fuente).Width <= ancho)
                return texto ?? "";
            string recortado = texto;
            while (recortado.Length > 1 && gfx.MeasureString(recortado + "…", fuente).Width > ancho)
                recortado = recortado.Substring(0, recortado.Length - 1);
            return recortado.TrimEnd() + "…";
        }

        // Separa en lineas por palabras para que el texto entre en el ancho; respeta los saltos de linea.
        private List<string> Partir(string texto, XFont fuente, double ancho)
        {
            var lineas = new List<string>();
            foreach (string renglon in texto.Replace("\r", "").Split('\n'))
            {
                string actual = "";
                foreach (string palabra in renglon.Split(' '))
                {
                    string prueba = actual.Length == 0 ? palabra : actual + " " + palabra;
                    if (actual.Length > 0 && gfx.MeasureString(prueba, fuente).Width > ancho)
                    {
                        lineas.Add(actual);
                        actual = palabra;
                    }
                    else
                        actual = prueba;
                }
                lineas.Add(actual);
            }
            return lineas;
        }

        // Tope del eje redondeado a 1, 2, 4, 6 u 8 por una potencia de 10: con 4 lineas, cada una cae en un numero limpio.
        private static double EscalaRedonda(double tope)
        {
            if (tope <= 0)
                return 4;
            double potencia = Math.Pow(10, Math.Floor(Math.Log10(tope * 1.1)));
            foreach (double paso in new[] { 1, 2, 4, 6, 8, 10 })
                if (paso * potencia >= tope * 1.1)
                    return paso * potencia;
            return 10 * potencia;
        }

        // Etiquetas del eje en formato corto: 1,5k ; 12M.
        private static string FormatoEje(double valor)
        {
            if (Math.Abs(valor) >= 1000000)
                return (valor / 1000000).ToString("0.#") + "M";
            if (Math.Abs(valor) >= 1000)
                return (valor / 1000).ToString("0.#") + "k";
            return valor.ToString("0.#");
        }
        #endregion
    }

    public sealed class Indicador
    {
        public string Titulo { get; set; }
        public string Valor { get; set; }
        public string Detalle { get; set; }
        public bool Destacado { get; set; }
    }

    public sealed class BarraGrafico
    {
        public string Etiqueta { get; set; }
        public double Valor { get; set; }
        // Texto que se escribe arriba de la barra.
        public string Texto { get; set; }
        public bool Proyectada { get; set; }
        public double? Minimo { get; set; }
        public double? Maximo { get; set; }
    }
}
