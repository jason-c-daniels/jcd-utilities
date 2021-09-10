using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Jcd.Utilities.Reflection
{
   public static class DictionaryFactoryExtensions
   {
      public static dynamic DfsD<T>(this object o, Dictionary<object,object> visited=null)
         where T: class, IDictionary<string,object>, new()
      {
         if (visited==null) visited=new Dictionary<object, object>();
         if (visited.ContainsKey(o)) return visited[o];

         if (o.IsScalar()) return o;

         visited.Add(o,null);
         switch (o)
         {
            case IDictionary d:
            {
               var result = new T();
               foreach (var key in d.Keys) { if (!visited.ContainsKey(d[key])) result.Add(key.ToString(),DfsD<T>(d[key], visited)); }
               visited[o] = result;
               return result;
            }
            case IEnumerable e:
            {
               var result = new List<dynamic>();
               foreach (var item in e) { if (!visited.ContainsKey(item)) result.Add(DfsD<T>(item,visited)); }
               visited[o] = result;
               return result;
            }
            case object obj:
            {
               var result = new T();
               var dict = (IDictionary<string, object>) result;
               foreach (var fpi in new FieldOrPropertyEnumerator(obj)) { dict.Add(fpi.Name, DfsD<T>(fpi.GetValue(obj), visited)); }
               visited[o] = result;
               return result;
            }
         }

         return null;
      }
   }
}
