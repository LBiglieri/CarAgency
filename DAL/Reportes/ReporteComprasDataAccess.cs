using System;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Reportes
{
    // Solo lectura: trae las compras con la fecha de cada etapa para el reporte de proyeccion de compras.
    public sealed class ReporteComprasDataAccess : DataAccessBase
    {
        public DataTable GetHechos(DateTime from, DateTime until)
        {
            return Read("Reporte_HechosCompras",
                new SqlParameter("@Desde", SqlDbType.DateTime) { Value = from },
                new SqlParameter("@Hasta", SqlDbType.DateTime) { Value = until });
        }
    }
}
