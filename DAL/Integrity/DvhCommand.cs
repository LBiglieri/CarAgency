using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CarAgency.DAL.Integrity
{
    // Calcula el DVH de una fila a partir de los valores de los parametros del comando (sin '@').
    // Lo implementa la capa de seguridad; la DAL no sabe como se calcula.
    public delegate DvhResult DvhCalculator(IDictionary<string, object> values);

    public sealed class DvhResult
    {
        public string Dvh { get; private set; }
        public IList<DvhColumn> Columns { get; private set; }

        public DvhResult(string dvh, IList<DvhColumn> columns)
        {
            Dvh = dvh;
            Columns = columns;
        }
    }

    // Valor normalizado con el que se calculo el DVH y el tipo SQL de la columna: la base tiene que
    // guardar exactamente ese valor para que el digito siga verificando.
    public sealed class DvhColumn
    {
        public string Name { get; private set; }
        public string SqlType { get; private set; }
        public int MaxLength { get; private set; }
        public object Value { get; private set; }

        public DvhColumn(string name, string sqlType, int maxLength, object value)
        {
            Name = name;
            SqlType = sqlType;
            MaxLength = maxLength;
            Value = value;
        }
    }

    public static class DvhCommand
    {
        public static void Apply(SqlCommand command, DvhCalculator calculator)
        {
            SqlParameter[] parameters = command.Parameters.Cast<SqlParameter>().ToArray();
            var values = parameters.ToDictionary(p => p.ParameterName.TrimStart('@'),
                p => p.Value == DBNull.Value ? null : p.Value, StringComparer.OrdinalIgnoreCase);
            DvhResult result = calculator(values);
            foreach (DvhColumn column in result.Columns)
            {
                SqlParameter parameter = parameters.Single(p =>
                    string.Equals(p.ParameterName.TrimStart('@'), column.Name, StringComparison.OrdinalIgnoreCase));
                parameter.Value = column.Value ?? DBNull.Value;
                switch (column.SqlType)
                {
                    case "varchar": parameter.SqlDbType = SqlDbType.VarChar; parameter.Size = column.MaxLength; break;
                    case "nvarchar": parameter.SqlDbType = SqlDbType.NVarChar; parameter.Size = column.MaxLength < 0 ? -1 : column.MaxLength / 2; break;
                    case "uniqueidentifier": parameter.SqlDbType = SqlDbType.UniqueIdentifier; break;
                    case "int": parameter.SqlDbType = SqlDbType.Int; break;
                    case "float": parameter.SqlDbType = SqlDbType.Float; break;
                    case "bit": parameter.SqlDbType = SqlDbType.Bit; break;
                    case "datetime": parameter.SqlDbType = SqlDbType.DateTime; break;
                }
            }
            command.Parameters.Add("@DVH", SqlDbType.Char, 64).Value = result.Dvh;
        }
    }
}
