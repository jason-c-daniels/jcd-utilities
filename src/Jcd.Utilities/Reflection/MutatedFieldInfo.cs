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

      public override FieldAttributes Attributes => Original.Attributes;

      public override RuntimeFieldHandle FieldHandle => Original.FieldHandle;

      public override Type FieldType => Original.FieldType;

      public override Type DeclaringType => Original.DeclaringType;

      public override string Name
      {
         get {
            if (Mutations.ContainsKey("Name")) return (string)Mutations["Name"];
            return Original.Name;
         }
      }

      public override Type ReflectedType => Original.ReflectedType;

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
   }
}
