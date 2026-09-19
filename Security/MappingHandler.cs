using CarAgency.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Security
{
    public class MappingHandler
    {
		public static T MapReaderToEntity<T>(IDataReader reader) where T : class, new()
		{
			var type = typeof(T);
			T obj = (T)Activator.CreateInstance(type);
			foreach (var prop in type.GetProperties())
			{
				var attributes = prop.GetCustomAttributes(false);
				foreach (var attribute in attributes)
				{
					if (attribute.GetType() == typeof(TableColumnAttribute))
					{
						var propType = prop.PropertyType;
						prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], propType));
					}
				}
			}
			return obj;
		}

		public static List<T> MapReaderToEntities<T>(IDataReader reader) where T : class, new()
		{
			List <T> list = new List<T>();

            while (reader.Read())
            {
				var type = typeof(T);
				T obj =(T)Activator.CreateInstance(type);
				foreach (var prop in type.GetProperties())
                {
					var attributes = prop.GetCustomAttributes(false);
					foreach (var attribute in attributes)
					{
						if (attribute.GetType() == typeof(TableColumnAttribute))
						{
							var propType = prop.PropertyType;
							prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], propType));
						}
					}
				}
				list.Add(obj);
            }
			return list;
        }

		// Entrada desde la DAL: los data access devuelven DataTable.
		public static List<T> MapTableToEntities<T>(DataTable table) where T : class, new()
		{
			using (table)
			using (IDataReader reader = table.CreateDataReader())
				return MapReaderToEntities<T>(reader);
		}

		// Los SP de escritura devuelven una fila con SQLResultType y message.
		public static SQLUpdateResult MapUpdateResult(DataTable table)
		{
			using (table)
			{
				string message = "";
				SQLResultType sqlResultType = SQLResultType.database_error;
				foreach (DataRow row in table.Rows)
				{
					message = (string)row["message"];
					Enum.TryParse((string)row["SQLResultType"], out sqlResultType);
				}
				return new SQLUpdateResult(sqlResultType, message);
			}
		}
	}
}
