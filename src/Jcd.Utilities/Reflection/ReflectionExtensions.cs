using Jcd.Utilities.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jcd.Utilities.Reflection
{
   public static class ReflectionExtensions
   {
      public static readonly HashSet<Type> BuiltInNonPrimitiveScalars = new HashSet<Type>(new[] { typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Uri), typeof(Guid), typeof(Type), typeof(string) });

      public static IEnumerable<PropertyInfo> EnumerateProperties(this Type type, BindingFlags? flags=null, Func<PropertyInfo, bool> skip = null)
      {
         IEnumerable<PropertyInfo> props = null;
         if (flags.HasValue) props = type.GetProperties(flags.Value);
         else props = type.GetProperties();
         foreach (var pi in props)
         {
            if (!pi.CanRead) continue;
            if (pi.DeclaringType.Namespace !=null && pi.DeclaringType.Namespace.StartsWith("System")) continue;
            var skipped = skip?.Invoke(pi);
            if (skipped.HasValue && skipped.Value) continue;
            yield return pi;
         }
      }

      public static IEnumerable<KeyValuePair<PropertyInfo,object>> ToPropertyInfoValuePairs(this IEnumerable<PropertyInfo> items, object item, BindingFlags? flags =null, Func<PropertyInfo, bool> skip = null, Func<PropertyInfo, object, object> valueMutator = null, Func<PropertyInfo, PropertyInfo> propInfoMutator = null)
      {
         Argument.IsNotNull(item, nameof(item));
         Argument.IsNotNull(items, nameof(items));
         foreach (var pi in items)
         {
            var value = (object)null;
            value = pi.GetValue(item);
            var propInfo = propInfoMutator?.Invoke(pi) ?? pi;
            value = valueMutator?.Invoke(pi, value) ?? value;
            yield return new KeyValuePair<PropertyInfo, object>(propInfo, value);
         }
      }

      public static IEnumerable<KeyValuePair<string, object>> ToNameValuePairs(this IEnumerable<KeyValuePair<PropertyInfo, object>> items)
      {
         Argument.IsNotNull(items, nameof(items));
         foreach (var kvp in items)
         {
            yield return new KeyValuePair<string, object>(kvp.Key.Name, kvp.Value);
         }
      }

      public static IEnumerable<FieldInfo> EnumerateFields(this Type type, BindingFlags? flags = null, Func<FieldInfo, bool> skip = null)
      {
         IEnumerable<FieldInfo> fields = null;
         if (flags.HasValue) fields = type.GetFields(flags.Value);
         else fields = type.GetFields();

         foreach (var fi in fields)
         {
            if (fi.DeclaringType.Namespace.StartsWith("System")) continue;
            var skipped = skip?.Invoke(fi);
            if (skipped.HasValue && skipped.Value) continue;
            yield return fi;
         }
      }
      public static IEnumerable<KeyValuePair<FieldInfo, object>> ToFieldInfoValuePairs(this IEnumerable<FieldInfo> items, object item, BindingFlags? flags = null, Func<FieldInfo, bool> skip = null, Func<FieldInfo, object, object> valueMutator = null, Func<FieldInfo, FieldInfo> propInfoMutator = null)
      {
         Argument.IsNotNull(item, nameof(item));
         Argument.IsNotNull(items, nameof(items));
         foreach (var fi in items)
         {
            var value = (object)null;
            value = fi.GetValue(item);
            var propInfo = propInfoMutator?.Invoke(fi) ?? fi;
            value = valueMutator?.Invoke(fi, value) ?? value;
            yield return new KeyValuePair<FieldInfo, object>(propInfo, value);
         }
      }

      public static IEnumerable<KeyValuePair<string, object>> ToNameValuePairs(this IEnumerable<KeyValuePair<FieldInfo, object>> items)
      {
         Argument.IsNotNull(items, nameof(items));
         foreach (var kvp in items)
         {
            yield return new KeyValuePair<string, object>(kvp.Key.Name, kvp.Value);
         }
      }


      /*
      public static IEnumerable<KeyValuePair<FieldInfo, object>> EnumerateFieldInfosAndValues(this object item, BindingFlags? flags = null, Func<FieldInfo, bool> skip = null, Func<FieldInfo, object, object> valueMutator = null, Func<FieldInfo, FieldInfo> fieldInfoMutator = null)
      {
         Argument.IsNotNull(item, nameof(item));
         foreach (var fi in item.GetType().EnumerateFields(flags, skip))
         {
            var value = (object)null;
            value = fi.GetValue(item);
            var fieldInfo = fieldInfoMutator?.Invoke(fi) ?? fi;
            value = valueMutator?.Invoke(fi, value) ?? value;
            yield return new KeyValuePair<FieldInfo, object>(fieldInfo, value);
         }
      }
      public static IEnumerable<KeyValuePair<string, object>> EnumerateFieldNamesAndValues(this object item, BindingFlags? flags = null, Func<FieldInfo, bool> skip = null, Func<FieldInfo, object, object> valueMutator = null, Func<FieldInfo, FieldInfo> fieldInfoMutator = null)
      {
         Argument.IsNotNull(item, nameof(item));
         foreach (var kvp in item.GetType().EnumerateFieldInfosAndValues(flags, skip, valueMutator, fieldInfoMutator))
         {
            yield return new KeyValuePair<string, object>(kvp.Key.Name, kvp.Value);
         }
      }
      */
      public static bool IsScalar(this object self, HashSet<Type> nonPrimitiveScalars=null)
      {
         if (self == null) return true;
         if (nonPrimitiveScalars == null) nonPrimitiveScalars = BuiltInNonPrimitiveScalars;
         var t = self.GetType();
         var ti = t.GetTypeInfo();
         return ti.IsEnum || ti.IsPrimitive || nonPrimitiveScalars.Contains(t);
      }

      public static bool IsKeyValuePair(this Type type)
      {
         return type.Name.StartsWith("KeyValuePair");
      }

      public static object Get(this object self, string name)
      {
         var t = self.GetType();
         var value = t.GetProperty(name)?.GetValue(self);
         value = value ?? t.GetField(name)?.GetValue(self);
         return value;
      }
      
      public static object ToDictionaryTree(this object self, HashSet<object> visited=null)
      {
         Dictionary<string, object> expando = null;
         if (visited == null) visited = new HashSet<object>();
         if (!visited.Contains(self))
         {
            visited.Add(self);
            expando = new Dictionary<string, object>();
            if (self.IsScalar()) return self;
            try
            {
               if (self is IDictionary dictionary)
               {
                  foreach (var key in dictionary.Keys)
                  {
                     expando.Append(key.ToString(), dictionary[key], visited);
                  }
               }
               else if (self is IEnumerable coll)
               {
                  var index = 0;
                  bool? isKeyValuePair = null;
                  var list = new List<object>();
                  foreach (var item in coll)
                  {
                     if (!isKeyValuePair.HasValue) isKeyValuePair = item?.GetType().IsKeyValuePair();
                     var key = index.ToString();
                     var value = (object)null;
                     if (isKeyValuePair.HasValue && isKeyValuePair.Value)
                     {
                        key = item.Get("Key").ToString();
                        value = item.Get("Value");
                        expando.Append(key, value, visited);
                     }
                     else
                     {
                        //value = item.ToDictionaryTree(visited);
                        if (item.IsScalar())
                           list.Add(item);
                        else
                           list.Add(item.ToDictionaryTree(visited));
                     }
                     index++;
                  }
                  if (isKeyValuePair.HasValue && !isKeyValuePair.Value) return list.ToArray();
                  if (!isKeyValuePair.HasValue) return list.ToArray();
               }
               else
               {
                  var type = self.GetType();
                  foreach (var kvp in type.EnumerateProperties().ToPropertyInfoValuePairs(self).ToNameValuePairs())
                  {
                     expando.Append(kvp.Key, kvp.Value, visited);
                  }
                  foreach (var kvp in type.EnumerateFields().ToFieldInfoValuePairs(self).ToNameValuePairs())
                  {
                     expando.Append(kvp.Key, kvp.Value, visited);
                  }
               }
            }
            finally
            {
               visited.Remove(self);
            }
         }
         return expando;
      }

      private static void Append(this Dictionary<string, object> expando, string key, object val, HashSet<object> visited)
      {
         if (!visited.Contains(val))
         {
            var value = val.IsScalar() ? val : val.ToDictionaryTree(visited);
            if (!expando.ContainsKey(key))
            {
               expando.Add(key, value);
            }
         }
      }
   }
}