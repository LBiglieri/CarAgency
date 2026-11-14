using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Reportes;

namespace CarAgency.Mappers
{
    public class ReporteComprasMapper
    {
        private readonly ReporteComprasDataAccess data = new ReporteComprasDataAccess();

        // Compras iniciadas en [from, until) y las abiertas anteriores. Nombre y apellido llegan encriptados por separado.
        public List<HechoCompra> GetHechos(DateTime from, DateTime until)
        {
            using (var table = data.GetHechos(from, until))
            {
                return table.Rows.Cast<DataRow>().Select(row => new HechoCompra
                {
                    Compra_Id = (Guid)row["Compra_Id"],
                    Numero = Convert.ToInt32(row["Numero"]),
                    Estado = (CompraStatus)Convert.ToInt32(row["Estado"]),
                    MotivoRechazo = row["MotivoRechazo"] == DBNull.Value ? (RejectionReason?)null : (RejectionReason)Convert.ToInt32(row["MotivoRechazo"]),
                    Fecha = (DateTime)row["Fecha"],
                    FechaInspeccion = LeerFecha(row, "FechaInspeccion"),
                    FechaTasacion = LeerFecha(row, "FechaTasacion"),
                    FechaUltimaTasacion = LeerFecha(row, "FechaUltimaTasacion"),
                    FechaFirmaContrato = LeerFecha(row, "FechaFirmaContrato"),
                    FechaDocumentacion = LeerFecha(row, "FechaDocumentacion"),
                    FechaPago = LeerFecha(row, "FechaPago"),
                    FechaTransferencia = LeerFecha(row, "FechaTransferencia"),
                    FechaCierre = LeerFecha(row, "FechaCierre"),
                    ValorTasacion = row["ValorTasacion"] == DBNull.Value ? (double?)null : Convert.ToDouble(row["ValorTasacion"]),
                    PrecioAcordado = row["PrecioAcordado"] == DBNull.Value ? (double?)null : Convert.ToDouble(row["PrecioAcordado"]),
                    MontoPagado = Convert.ToDouble(row["MontoPagado"]),
                    Marca = (string)row["Marca"],
                    Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"],
                    Persona_Descripcion = PersonaMapper.Describe((string)row["Persona_Nombre"], (string)row["Persona_Apellido"], (int)row["Persona_DNI"])
                }).ToList();
            }
        }

        private static DateTime? LeerFecha(DataRow row, string column)
        {
            return row[column] == DBNull.Value ? (DateTime?)null : (DateTime)row[column];
        }
    }
}
