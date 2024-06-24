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

		public static List<T> MapReaderToEntities<T>(IDataReader reader) where T : Entity, new()
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
        public static T MapEntityToEntity<T>(Entity f) where T : Entity, new()
        {
            var key = (from: f.GetType(), to: typeof(T));

            if (!_cache.ContainsKey(key))
            {
                PopulateCacheKey(key);
            }

            var result = new T();
            var entry = _cache[key];
            foreach (var e in entry)
            {
                var val = e.Get.Invoke(f, null);
                e.Set.Invoke(result, new[] { val });
            }

            return result;
        }

        public static void PopulateCacheKey((Type from, Type to) key)
		{
			var fromProps = key.from.GetProperties();
			var toProps = key.to.GetProperties();

			List<(MethodInfo, MethodInfo)> entry = new List<(MethodInfo, MethodInfo)>();
			foreach (var from in fromProps)
			{
				var to = toProps.FirstOrDefault(x => x.Name == from.Name);
				if (to == null)
				{
					continue;
				}
				entry.Add((from.GetMethod, to.SetMethod));
			}
			_cache[key] = entry;
		}
	}
}
