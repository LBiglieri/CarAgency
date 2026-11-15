using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE.BitacoraEventos;
using CarAgency.DAL.BitacoraEventos;
using CarAgency.DAL.Persistence;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.BitacoraEventos
{
    public sealed class BitacoraEventosMapper
    {
        private const string Table = "BitacoraEventos";
        private readonly BitacoraEventosDataAccess data;
        private readonly string connectionString;
        public BitacoraEventosMapper() : this(new DatabaseConnectionProvider().GetConnectionString()) { }
        public BitacoraEventosMapper(string connectionString)
        {
            this.connectionString = connectionString;
            data = new BitacoraEventosDataAccess(connectionString);
        }

        public bool ActorExists(Guid userId) { return data.ActorExists(userId); }

        public void Insert(BitacoraEvento entry)
        {
            var digitVerifier = new DigitVerifierWriteMapper(connectionString);
            data.Insert(entry.BitacoraEventos_Id, entry.Usuario_Id, entry.LoginIntentado, entry.Fecha, entry.Modulo.ToString(),
                entry.TipoEvento.ToString(), entry.Criticidad, entry.Objetivo_Id,
                digitVerifier.Dvh(Table));
            digitVerifier.UpdateDvv(Table);
        }

        public List<BitacoraEvento> Query(BitacoraEventosFilter filter)
        {
            using (var table = data.Query(filter.Login, filter.From.Date, filter.To.Date.AddDays(1),
                filter.Modulo.HasValue ? filter.Modulo.ToString() : null,
                filter.TipoEvento.HasValue ? filter.TipoEvento.ToString() : null, filter.Criticidad))
            {
                return table.Rows.Cast<DataRow>().Select(row => new BitacoraEvento {
                    BitacoraEventos_Id = (Guid)row["BitacoraEventos_Id"], Usuario_Id = row["Usuario_Id"] == DBNull.Value ? (Guid?)null : (Guid)row["Usuario_Id"],
                    Login = (string)row["Login"], Nombre = (string)row["Nombre"], Apellido = (string)row["Apellido"],
                    Fecha = (DateTime)row["Fecha"], Modulo = (BitacoraEventosModule)Enum.Parse(typeof(BitacoraEventosModule), (string)row["Modulo"]),
                    TipoEvento = (TipoEvento)Enum.Parse(typeof(TipoEvento), (string)row["TipoEvento"]),
                    Criticidad = Convert.ToInt32(row["Criticidad"]),
                    Objetivo_Id = row["Objetivo_Id"] == DBNull.Value ? (Guid?)null : (Guid)row["Objetivo_Id"]
                }).ToList();
            }
        }
        public List<string> GetLogins()
        { using (var table = data.GetLogins()) return table.Rows.Cast<DataRow>().Select(r => (string)r["Login"]).ToList(); }
    }
}
