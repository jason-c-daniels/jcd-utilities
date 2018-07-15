using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jcd.Utilities.Reflection
{
   public class FieldOrPropertyInfo : MemberInfo
   {
      MemberInfo _memberInfo;
      BindingFlags _flags;
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
         if (MemberType == MemberTypes.Property) return DeclaringType.GetProperty(Name, _flags).GetValue(obj);
         return DeclaringType.GetField(Name, _flags).GetValue(obj);
      }
   }
}
