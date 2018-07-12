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

      public static IEnumerable<KeyValuePair<PropertyInfo,object>> ToPropertyInfoValuePairs(this IEnumerable<PropertyInfo> items, object item, BindingFlags? flags =null, Func<PropertyInfo, bool> skip = null)
      {
         Argument.IsNotNull(item, nameof(item));
         Argument.IsNotNull(items, nameof(items));
         foreach (var pi in items)
         {
            var value = (object)null;
            try { value = pi.GetValue(item); } catch { /* ignore for now.*/ }
            yield return new KeyValuePair<PropertyInfo, object>(pi, value);
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
      public static IEnumerable<KeyValuePair<FieldInfo, object>> ToFieldInfoValuePairs(this IEnumerable<FieldInfo> items, object item, BindingFlags? flags = null, Func<FieldInfo, bool> skip = null)
      {
         Argument.IsNotNull(item, nameof(item));
         Argument.IsNotNull(items, nameof(items));
         foreach (var fi in items)
         {
            var value = (object)null;
            value = fi.GetValue(item);
            yield return new KeyValuePair<FieldInfo, object>(fi, value);
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

      public static object GetPropertyOrFieldValue(this object self, string name)
      {
         var t = self.GetType();
         var value = t.GetProperty(name)?.GetValue(self);
         value = value ?? t.GetField(name)?.GetValue(self);
         return value;
      }

      public static IDictionary<string, object> ToDictionaryTree(this object self, HashSet<object> visited = null, Func<string, string> keyRenamingStrategy = null, Func<string, object, bool> valueRetentionStrategy = null)
      {
         return (IDictionary<string, object>)self.ToDictionaryTree<Dictionary<string, object>>(keyRenamingStrategy:keyRenamingStrategy, valueRetentionStrategy:valueRetentionStrategy);
      }

      public static ExpandoObject ToExpandoObject(this object self, HashSet<object> visited = null, Func<string,string> keyRenamingStrategy=null, Func<string, object, bool> valueRetentionStrategy = null)
      {
         Func<string, string> myKeyRenamingStrategy = (key) =>
         {
            return DefaultExpandoKeyRenamingStrategy(keyRenamingStrategy!=null ? keyRenamingStrategy(key) :key);
         };
         Func<string, object, bool> myValueRetentionStrategy = (key, value) =>
         {
            return (valueRetentionStrategy == null || valueRetentionStrategy(key, value)) && DefaultExpandoValueRetentionStrategy(value);
         };
         return (ExpandoObject)self.ToDictionaryTree<ExpandoObject>(keyRenamingStrategy: myKeyRenamingStrategy, valueRetentionStrategy: myValueRetentionStrategy);
      }

      public static string DefaultExpandoKeyRenamingStrategy(string k)
      {
         var sb = new StringBuilder();
         char pc = '_';
         pc = BuildName(k, sb, pc);
         if (sb.Length == 0)
         {
            BuildName($"__MungedField{k}", sb, pc);
         }
         return sb.ToString();
      }

      public static bool DefaultExpandoValueRetentionStrategy(object value)
      {
         var retain = false;
         if (value != null)
         {
            retain = true;
            if (value is IDictionary dictionary)
            {
               retain = dictionary.Count > 0;
            }
            else if (value is IEnumerable collection)
            {
               var enumerator = collection.GetEnumerator();
               retain = enumerator.MoveNext();
               if (enumerator is IDisposable disp) disp.Dispose();
            }
         }

         return retain;
      }

      private static char BuildName(string k, StringBuilder sb, char pc)
      {
         foreach (var c in k)
         {

            if ((sb.Length > 0 && (Char.IsLetterOrDigit(c) || c == '_')) || // valid member name char.
                (sb.Length == 0 && (Char.IsLetter(c) || c == '_' || c == '@'))) // valid member name starting char
            {
               if ((Char.IsLetterOrDigit(pc) && sb.Length > 0) || c == '_' || c == '@')
                  sb.Append(c);
               else if (sb.Length > 0 || Char.IsLetter(c))
                  sb.Append(Char.ToUpperInvariant(c));
            }
            pc = c;
         }

         return pc;
      }

      private static dynamic ToDictionaryTree<TNode>(this object self, HashSet<object> visited=null, Func<string, string> keyRenamingStrategy = null, Func<string, object, bool> valueRetentionStrategy=null)
         where TNode : IDictionary<string, object>, new()
      {
         TNode root = default(TNode);
         if (visited == null) visited = new HashSet<object>();
         if (keyRenamingStrategy == null) keyRenamingStrategy = (k) => k;
         if (valueRetentionStrategy == null) valueRetentionStrategy = (name,value) => true;
         if (!visited.Contains(self))
         {
            visited.Add(self);
            root = new TNode();
            if (self.IsScalar()) return self;
            try
            {
               if (self is IDictionary dictionary)
               {
                  foreach (var key in dictionary.Keys)
                  {
                     root.Append<TNode>(key.ToString(), dictionary[key], visited, keyRenamingStrategy, valueRetentionStrategy);
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
                        key = item.GetPropertyOrFieldValue("Key").ToString();
                        value = item.GetPropertyOrFieldValue("Value");
                        root.Append<TNode>(key, value, visited, keyRenamingStrategy, valueRetentionStrategy);
                     }
                     else
                     {
                        var val = item.IsScalar() ? item : item.ToDictionaryTree<TNode>(visited, keyRenamingStrategy, valueRetentionStrategy);
                        if (valueRetentionStrategy($"{key}:{index}",val))
                           list.Add(val);
                     }
                     index++;
                  }
                  if (isKeyValuePair.HasValue && !isKeyValuePair.Value) return list.ToArray();
                  else if (!isKeyValuePair.HasValue) return list.ToArray();
               }
               else
               {
                  var type = self.GetType();
                  foreach (var kvp in type.EnumerateProperties().ToPropertyInfoValuePairs(self).ToNameValuePairs())
                  {
                     root.Append<TNode>(kvp.Key, kvp.Value, visited, keyRenamingStrategy, valueRetentionStrategy);
                  }
                  foreach (var kvp in type.EnumerateFields().ToFieldInfoValuePairs(self).ToNameValuePairs())
                  {
                     root.Append<TNode>(kvp.Key, kvp.Value, visited, keyRenamingStrategy, valueRetentionStrategy);
                  }
               }
            }
            finally
            {
               visited.Remove(self);
            }
         }
         return root;
      }

      private static void Append<TNode>(this IDictionary<string, object> dictionary, string key, object val, HashSet<object> visited, Func<string, string> keyRenamingStrategy, Func<string,object,bool> valueRetentionStrategy)
         where TNode: IDictionary<string, object>, new()
      {
         Argument.IsNotNull(keyRenamingStrategy,nameof(keyRenamingStrategy));
         Argument.IsNotNull(valueRetentionStrategy, nameof(valueRetentionStrategy));
         if (!visited.Contains(val))
         {
            key = keyRenamingStrategy(key);
            var value = val.IsScalar() ? val : val.ToDictionaryTree<TNode>(visited, keyRenamingStrategy,valueRetentionStrategy);
            if (!dictionary.ContainsKey(key) && valueRetentionStrategy(key,value))
            {
               dictionary.Add(key, value);
            }
         }
      }

   }
}