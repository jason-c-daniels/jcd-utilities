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

      public override PropertyAttributes Attributes => Original.Attributes;

      public override bool CanRead => Original.CanRead;

      public override bool CanWrite => Original.CanWrite;

      public override Type PropertyType => Original.PropertyType;

      public override Type DeclaringType => Original.DeclaringType;

      public override string Name
      {
         get
         {
            if (Mutations.ContainsKey("Name")) return (string)Mutations["Name"];
            return Original.Name;
         }
      }

      public override Type ReflectedType => Original.ReflectedType;

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
   }
}
