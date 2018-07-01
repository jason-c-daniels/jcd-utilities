using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Jcd.Utilities.Reflection
{
   /// <summary>
   /// A helper class to effectively allow changing a structure while retaining access to the original definition.
   /// </summary>
   public class MutatedPropertyInfo : PropertyInfo
   {
      public MutatedPropertyInfo(PropertyInfo original)
      {
         Argument.IsNotNull(original);
         Original = original;
      }
      public PropertyInfo Original { get; }

      public Dictionary<string, object> Mutations { get; } = new Dictionary<string, object>();

      public override PropertyAttributes Attributes => Mutation(nameof(Attributes), () => Original.Attributes);

      public override bool CanRead => Original.CanRead;

      public override bool CanWrite => Original.CanWrite;

      public override Type PropertyType => Mutation(nameof(PropertyType), () => Original.PropertyType);

      public override Type DeclaringType => Mutation(nameof(DeclaringType), () => Original.DeclaringType);

      public override string Name => Mutation(nameof(Name), () => Original.Name);

      public override Type ReflectedType => Mutation(nameof(ReflectedType), () => Original.ReflectedType);

      public override MethodInfo[] GetAccessors(bool nonPublic)
      {
         return Original.GetAccessors(nonPublic);
      }

      public override object[] GetCustomAttributes(bool inherit)
      {
         return Original.GetCustomAttributes(inherit);
      }

      public override object[] GetCustomAttributes(Type attributeType, bool inherit)
      {
         return Original.GetCustomAttributes(attributeType, inherit);
      }

      public override MethodInfo GetGetMethod(bool nonPublic)
      {
         return Original.GetGetMethod(nonPublic);
      }

      public override ParameterInfo[] GetIndexParameters()
      {
         return Original.GetIndexParameters();
      }

      public override MethodInfo GetSetMethod(bool nonPublic)
      {
         return Original.GetSetMethod(nonPublic);
      }

      public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
      {
         return Original.GetValue(obj, invokeAttr, binder, index, culture);
      }

      public override bool IsDefined(Type attributeType, bool inherit)
      {
         return Original.IsDefined(attributeType, inherit);
      }

      public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
      {
         Original.SetValue(obj, value, invokeAttr, binder, index, culture);
      }

      public void Mutate<T>(string propName, T val)
      {
         Argument.IsTrue(GetMutablePropertyType(propName) == typeof(T), nameof(propName), $"{propName} is not of type {typeof(T)}");
         if (!Mutations.ContainsKey(propName))
            Mutations.Add(propName, val);
         else
            Mutations[propName] = val;
      }

      private T Mutation<T>(string propName, Func<T> original = null)
      {
         if (Mutations.ContainsKey(propName)) return (T)Mutations[propName];
         if (original != null) return original();
         return default(T);
      }

      Type GetMutablePropertyType(string propName)
      {
         return typeof(MutatedPropertyInfo).GetProperty(propName).PropertyType;
      }
   }
}
