using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Reportes
{
    // Solo lectura: trae los hechos del embudo comercial para el reporte de proyeccion de ventas.
    public sealed class ReporteVentasDataAccess : DataAccessBase
    {
        public DataTable GetHechos(DateTime from, DateTime until)
        {
            return Read("Reporte_HechosVentas",
                new SqlParameter("@Desde", SqlDbType.DateTime) { Value = from },
                new SqlParameter("@Hasta", SqlDbType.DateTime) { Value = until });
        }
    }
}
