using System;
using System.Collections.Generic;
using BE;
using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Security.Session;

namespace BLL
{
    // Reporte inteligente de PN2. Carga las compras con sus etapas y delega todo el calculo en ProyeccionComprasCalculator.
    public class ReporteComprasBLL
    {
        private readonly ReporteComprasMapper _reportmapper = new ReporteComprasMapper();
        private readonly ProyeccionComprasCalculator _calculator = new ProyeccionComprasCalculator();

        public static void RequireAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermisoType.ProyeccionComprasForm))
                throw new TranslatableException("PurchaseForecastAccessDenied", "No tiene permiso para generar la proyección de compras.");
        }

        // Usa los datos hasta el final del dia indicado y proyecta los ProyeccionComprasCalculator.HorizonteDias siguientes.
        public ProyeccionCompras GenerarProyeccion(DateTime datosHasta)
        {
            RequireAccess();
            Validate(datosHasta);
            DateTime corte = datosHasta.Date.AddDays(1);
            List<HechoCompra> hechos;
            try { hechos = _reportmapper.GetHechos(corte.AddDays(-ProyeccionComprasCalculator.DiasDeHistoria), corte); }
            catch (Exception error) { throw new TranslatableException("PurchaseForecastReadFailed", "No se pudieron leer las compras para la proyección.", error); }
            return _calculator.Calcular(hechos, corte);
        }

        public static void Validate(DateTime datosHasta)
        {
            if (datosHasta.Date > DateTime.Today || datosHasta.Date == DateTime.MaxValue.Date || datosHasta.Year < 2000)
                throw new TranslatableException("SalesForecastInvalidDate", "La fecha de los datos no puede ser posterior a hoy.");
        }
    }
}
