using CarAgency.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.Utilities.Persistence
{
    public class MappingHandler
    {
		static Dictionary<(Type from, Type to), List<(MethodInfo Get, MethodInfo Set)>> _cache = new Dictionary<(Type from, Type to), List<(MethodInfo Get, MethodInfo Set)>>();

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
	}
}
