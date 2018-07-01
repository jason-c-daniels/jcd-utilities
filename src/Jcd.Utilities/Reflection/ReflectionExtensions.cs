using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jcd.Utilities.Reflection
{
   public static class ReflectionExtensions
   {
      public static IEnumerable<PropertyInfo> EnumerateProperties(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, Func<PropertyInfo, bool> skip = null)
      {
         foreach (var pi in type.GetProperties(flags))
         {
            var skipped = skip?.Invoke(pi);
            if (skipped.HasValue && skipped.Value) continue;
            yield return pi;
         }
      }

      public static IEnumerable<FieldInfo> EnumerateFields(this Type type, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> skip = null)
      {
         foreach (var fi in type.GetFields(flags))
         {
            var skipped = skip?.Invoke(fi);
            if (skipped.HasValue && skipped.Value) continue;
            yield return fi;
         }
      }

      public static IEnumerable<KeyValuePair<PropertyInfo,object>> EnumeratePropertiesAndValues(this object item, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, Func<PropertyInfo, bool> skip = null, Func<PropertyInfo, object, object> valueMutator = null, Func<PropertyInfo, PropertyInfo> propInfoMutator = null)
      {
         Argument.IsNotNull(item, nameof(item));
         foreach (var pi in item.GetType().EnumerateProperties(flags, skip))
         {
            var value = pi.GetValue(item);
            var propInfo = propInfoMutator?.Invoke(pi) ?? pi;
            value = valueMutator?.Invoke(pi, value) ?? value;
            yield return new KeyValuePair<PropertyInfo, object>(propInfo, value);
         }
      }

      public static IEnumerable<KeyValuePair<FieldInfo, object>> EnumerateFieldsAndValues(this object item, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> skip = null, Func<FieldInfo, object, object> valueMutator = null, Func<FieldInfo, FieldInfo> fieldInfoMutator = null)
      {
         Argument.IsNotNull(item, nameof(item));
         foreach (var fi in item.GetType().EnumerateFields(flags, skip))
         {
            var value = fi.GetValue(item);
            var fieldInfo = fieldInfoMutator?.Invoke(fi) ?? fi;
            value = valueMutator?.Invoke(fi, value) ?? value;
            yield return new KeyValuePair<FieldInfo, object>(fieldInfo, value);
         }
      }
   }
}