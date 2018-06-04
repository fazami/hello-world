using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Farzin.Infrastructure.CrossCutting.Helpers
{
    public static class SqlHelper
    {
        //public static List<T> MapToList<T>(this IDataReader dr) where T : new()
        //{
        //    Type businessEntityType = typeof(T);
        //    List<T> entitys = new List<T>();
        //    Hashtable hashtable = new Hashtable();
        //    PropertyInfo[] properties = businessEntityType.GetProperties();
        //    foreach (PropertyInfo info in properties)
        //    {
        //        hashtable[info.Name.ToUpper()] = info;
        //    }
        //    while (dr.Read())
        //    {
        //        T newObject = new T();
        //        for (int index = 0; index < dr.FieldCount; index++)
        //        {
        //            PropertyInfo info = (PropertyInfo)
        //                                hashtable[dr.GetName(index).ToUpper()];
        //            if ((info != null) && info.CanWrite)
        //            {
        //                var val = dr.GetValue(index);
        //                info.SetValue(newObject, val is DBNull ? null : val, null);
        //            }
        //        }
        //        entitys.Add(newObject);
        //    }
        //    dr.Close();
        //    return entitys;
        //}
        //private List<T> getList<T>(SqlDataReader reader)
        //{
        //    var results = new List<T>();
        //    var properties = typeof(T).GetProperties();
        //    bool isTValueType = properties.Length == 0 && typeof(T).IsValueType;
        //    while (reader.Read())
        //    {
        //        var item = Activator.CreateInstance<T>();
        //        if (isTValueType && reader.FieldCount == 1)
        //        {
        //            if (!reader.IsDBNull(0))
        //            {
        //                item = (T)reader[0];
        //            }
        //        }
        //        else
        //        {
        //            foreach (var property in typeof(T).GetProperties())
        //            {
        //                if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
        //                {
        //                    Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
        //                    property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
        //                }
        //            }
        //        }
        //        results.Add(item);
        //    }
        //    return results;
        //}

    }
}
