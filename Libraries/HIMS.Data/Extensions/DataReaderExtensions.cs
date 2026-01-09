using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Extensions
{
    public static class DataReaderExtensions
    {
        public static List<T> MapToList<T>(this DbDataReader reader) where T : new()
        {
            var list = new List<T>();
            var props = typeof(T).GetProperties();

            while (reader.Read())
            {
                var obj = new T();
                foreach (var prop in props)
                {
                    if (!reader.HasColumn(prop.Name) || reader[prop.Name] == DBNull.Value)
                        continue;

                    prop.SetValue(obj, reader[prop.Name]);
                }
                list.Add(obj);
            }
            return list;
        }

        public static bool HasColumn(this DbDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }
    }

}
