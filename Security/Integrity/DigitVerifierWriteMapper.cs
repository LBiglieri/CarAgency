using System;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.BE.Integrity;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.Security.Integrity
{
    public sealed class DigitVerifierWriteMapper
    {
        private readonly DigitVerifierBLL bll;

        public DigitVerifierWriteMapper() : this(new DatabaseConnectionProvider().GetConnectionString()) { }
        public DigitVerifierWriteMapper(string connectionString) { bll = new DigitVerifierBLL(connectionString); }

        public void EnsureConfigured(params string[] tables) { bll.EnsureConfigured(tables); }
        public void UpdateDvv(params string[] tables) { bll.UpdateDvv(tables); }

        // La DAL entrega los valores de los parametros del comando y recibe los valores normalizados,
        // el tipo de cada columna protegida y el DVH resultante.
        public DvhCalculator Dvh(string table)
        {
            return values =>
            {
                DVPreparedRow prepared = bll.PrepareDvh(table, new DVRow(values));
                return new DvhResult(prepared.Row.DVH, prepared.Columns
                    .Select(c => new DvhColumn(c.Name, c.SqlType, c.MaxLength, prepared.Row.GetValue(c.Name)))
                    .ToList());
            };
        }

        public DVFamilyDeletion PrepareFamilyDeletion(Guid familyId) { return bll.PrepareFamilyDeletion(familyId); }

        // Alta o modificacion de una fila protegida: el SP recibe el DVH y, si sale bien, se rehace el DVV.
        public static SQLUpdateResult Save(string table, Func<DvhCalculator, DataTable> write)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            SQLUpdateResult result = MappingHandler.MapUpdateResult(write(digitVerifier.Dvh(table)));
            if (result.sqlResult == SQLResultType.success)
                digitVerifier.UpdateDvv(table);
            return result;
        }

        // Baja: no hay DVH que calcular, pero cambia el DVV de cada tabla que pierde filas (la del
        // registro y las que se borran en cascada).
        public static SQLUpdateResult Remove(Func<DataTable> delete, params string[] tables)
        {
            var digitVerifier = new DigitVerifierWriteMapper();
            digitVerifier.EnsureConfigured(tables);
            SQLUpdateResult result = MappingHandler.MapUpdateResult(delete());
            if (result.sqlResult == SQLResultType.success)
                digitVerifier.UpdateDvv(tables);
            return result;
        }
    }
}
