using System;
using System.Reflection;
using Jcd.Utilities.Validations;

namespace Jcd.Utilities.Reflection
{
   public class FieldOrPropertyInfo : MemberInfo
   {
      private readonly MemberInfo _memberInfo;
      private readonly BindingFlags _flags;
      public FieldOrPropertyInfo(MemberInfo memberInfo, BindingFlags flags)
      {
         Argument.IsNotNull(memberInfo, nameof(memberInfo));
         Argument.PassesAny(new Check.Signature<MemberInfo>[] { (mi, s, f) => mi.MemberType == MemberTypes.Field, (mi, s, f) => mi.MemberType == MemberTypes.Property }, memberInfo, nameof(memberInfo), $"memberInfo.MemberType must be a Property or Field but was {memberInfo.MemberType}");
         _flags = flags;
         _memberInfo = memberInfo;
      }

      public override Type DeclaringType => _memberInfo.DeclaringType;

      public override MemberTypes MemberType => _memberInfo.MemberType;

      public override string Name => _memberInfo.Name;

      public override Type ReflectedType => _memberInfo.ReflectedType;

      public override object[] GetCustomAttributes(bool inherit) => _memberInfo.GetCustomAttributes(inherit);

      public override object[] GetCustomAttributes(Type attributeType, bool inherit) => _memberInfo.GetCustomAttributes(attributeType, inherit);

      public override bool IsDefined(Type attributeType, bool inherit) => _memberInfo.IsDefined(attributeType, inherit);

      public object GetValue(object obj)
      {
         try
         {
            if (MemberType == MemberTypes.Property) return DeclaringType?.GetProperty(Name, _flags)?.GetValue(obj);
            return DeclaringType?.GetField(Name, _flags)?.GetValue(obj);
         }
         catch
         {
            return null; // throwing exceptions from a property is a bad practice. Perhaps I'll ad the ability to bypass the catch block. But i'm not feeling that generous right now.
         }
      }

      public object GetValue(object obj, out bool errored)
      {
         try
         {
            var result = MemberType == MemberTypes.Property ? DeclaringType?.GetProperty(Name, _flags)?.GetValue(obj) : DeclaringType?.GetField(Name, _flags)?.GetValue(obj);
            errored = false;
            return result;
         }
         catch
         {
            errored = true;
            return null; // throwing exceptions from a property is a bad practice. Perhaps I'll ad the ability to bypass the catch block. But i'm not feeling that generous right now.
         }
      }
   }
}
