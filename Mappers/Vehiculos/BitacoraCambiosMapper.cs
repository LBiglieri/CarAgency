using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Vehiculos;

namespace CarAgency.Mappers
{
    public class BitacoraCambiosMapper
    {
        private readonly BitacoraCambiosDataAccess data = new BitacoraCambiosDataAccess();

        // El periodo incluye el dia final completo: se consulta hasta el comienzo del dia siguiente.
        public List<BitacoraCambio> Query(BitacoraCambiosFilter filter)
        {
            using (var table = data.Query(filter.Vehiculo_Id, filter.From.Date, filter.To.Date.AddDays(1),
                filter.Operacion.HasValue ? (int)filter.Operacion.Value : (int?)null))
            {
                return table.Rows.Cast<DataRow>().Select(row => new BitacoraCambio {
                    BitacoraCambios_Id = (Guid)row["BitacoraCambios_Id"],
                    Vehiculo_Id = (Guid)row["Vehiculo_Id"],
                    Patente = (string)row["Patente"],
                    Fecha = (DateTime)row["Fecha"],
                    Operacion = (BitacoraCambiosOperation)Convert.ToInt32(row["Operacion"]),
                    Campo = row["Campo"] == DBNull.Value ? null : (string)row["Campo"],
                    ValorAnterior = row["ValorAnterior"] == DBNull.Value ? null : (string)row["ValorAnterior"],
                    ValorNuevo = row["ValorNuevo"] == DBNull.Value ? null : (string)row["ValorNuevo"],
                    Usuario_Id = row["Usuario_Id"] == DBNull.Value ? (Guid?)null : (Guid)row["Usuario_Id"],
                    Login = (string)row["Login"]
                }).ToList();
            }
        }
    }
}
