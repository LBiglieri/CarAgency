using System;
using System.Collections.Generic;
using BE;
using CarAgency.BE;
using CarAgency.Mappers;
using CarAgency.Security.Session;

namespace BLL
{
    // Reporte inteligente de PN1. Carga los hechos del embudo y delega todo el calculo en ProyeccionVentasCalculator.
    public class ReporteVentasBLL
    {
        private readonly ReporteVentasMapper _reportmapper = new ReporteVentasMapper();
        private readonly ProyeccionVentasCalculator _calculator = new ProyeccionVentasCalculator();

        public static void RequireAccess()
        {
            if (!SessionHandler.Instance.IsAuthorized(PermisoType.ProyeccionVentasForm))
                throw new TranslatableException("SalesForecastAccessDenied", "No tiene permiso para generar la proyección de ventas.");
        }

        // Usa los datos hasta el final del dia indicado y proyecta los ProyeccionVentasCalculator.HorizonteDias siguientes.
        public ProyeccionVentas GenerarProyeccion(DateTime datosHasta)
        {
            RequireAccess();
            Validate(datosHasta);
            DateTime corte = datosHasta.Date.AddDays(1);
            List<HechoVenta> hechos;
            try { hechos = _reportmapper.GetHechos(corte.AddDays(-ProyeccionVentasCalculator.DiasDeHistoria), corte); }
            catch (Exception error) { throw new TranslatableException("SalesForecastReadFailed", "No se pudieron leer las ventas para la proyección.", error); }
            return _calculator.Calcular(hechos, corte);
        }

        public static void Validate(DateTime datosHasta)
        {
            if (datosHasta.Date > DateTime.Today || datosHasta.Date == DateTime.MaxValue.Date || datosHasta.Year < 2000)
                throw new TranslatableException("SalesForecastInvalidDate", "La fecha de los datos no puede ser posterior a hoy.");
        }
    }
}
