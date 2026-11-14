using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Reportes;

namespace CarAgency.Mappers
{
    public class ReporteVentasMapper
    {
        private readonly ReporteVentasDataAccess data = new ReporteVentasDataAccess();

        // Hechos del periodo [from, until). Nombre y apellido llegan encriptados por separado y se describen
        // igual que en cotizaciones y reservas.
        public List<HechoVenta> GetHechos(DateTime from, DateTime until)
        {
            using (var table = data.GetHechos(from, until))
            {
                return table.Rows.Cast<DataRow>().Select(row => new HechoVenta
                {
                    Tipo = (TipoHechoVenta)Convert.ToInt32(row["Tipo"]),
                    Id = (Guid)row["Id"],
                    Fecha = (DateTime)row["Fecha"],
                    FechaVencimiento = row["FechaVencimiento"] == DBNull.Value ? (DateTime?)null : (DateTime)row["FechaVencimiento"],
                    Persona_Id = row["Persona_Id"] == DBNull.Value ? (Guid?)null : (Guid)row["Persona_Id"],
                    Vehiculo_Id = (Guid)row["Vehiculo_Id"],
                    Reserva_Id = row["Reserva_Id"] == DBNull.Value ? (Guid?)null : (Guid)row["Reserva_Id"],
                    Precio = Convert.ToDouble(row["Precio"]),
                    Marca = (string)row["Marca"],
                    Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"],
                    Persona_Descripcion = row["Persona_DNI"] == DBNull.Value ? "" : PersonaMapper.Describe(
                        (string)row["Persona_Nombre"], (string)row["Persona_Apellido"], (int)row["Persona_DNI"])
                }).ToList();
            }
        }
    }
}
