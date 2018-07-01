using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Jcd.Utilities.Reflection
{
   /// <summary>
   /// A helper class to effectively allow changing a structure while 
   /// (internally) retaining access to the original definition.
   /// </summary>
   public class MutatedFieldInfo : FieldInfo
   {
      public MutatedFieldInfo(FieldInfo original)
      {
         Argument.IsNotNull(original);
         Original = original;
      }

      public FieldInfo Original { get; }

      public Dictionary<string, object> Mutations { get; } = new Dictionary<string, object>();

      public override FieldAttributes Attributes => Mutation(nameof(Attributes), () => Original.Attributes);

      public override RuntimeFieldHandle FieldHandle => Original.FieldHandle;

      public override Type FieldType => Mutation(nameof(FieldType), () => Original.FieldType);

      public override Type DeclaringType => Mutation(nameof(DeclaringType), () => Original.DeclaringType);

      public override string Name => Mutation(nameof(Name), () => Original.Name);

      public override Type ReflectedType => Mutation(nameof(ReflectedType), () => Original.ReflectedType);

      public override object[] GetCustomAttributes(bool inherit)
      {
         return Original.GetCustomAttributes(inherit);
      }

      public override object[] GetCustomAttributes(Type attributeType, bool inherit)
      {
         return Original.GetCustomAttributes(attributeType, inherit);
      }

      public override object GetValue(object obj)
      {
         return Original.GetValue(obj);
      }

      public override bool IsDefined(Type attributeType, bool inherit)
      {
         return Original.IsDefined(attributeType, inherit);
      }

      public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
      {
         Original.SetValue(obj, value, invokeAttr, binder, culture);
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
