﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jcd.Utilities.Reflection
{
   public class MemberInfoEnumerator : IEnumerable<MemberInfo>
   {
      public struct Settings
      {
         public BindingFlags? Flags;
         public Func<MemberInfo, bool> Skip;
      }

      public static Func<MemberInfo, bool> SkipSystemMembers = (mi) => mi.DeclaringType.Namespace != null && mi.DeclaringType.Namespace.StartsWith("System");
      public static Func<MemberInfo, bool> SkipSystemAndNonDataMembers = (mi) => SkipSystemMembers(mi) || (mi.MemberType != MemberTypes.Field && mi.MemberType != MemberTypes.Property);

      public Settings EnumerationSettings { get; set; }

      public Type Type { get; }

      public MemberInfoEnumerator(Type type, Settings settings = default(Settings)) { Type = type; EnumerationSettings = settings; }
      public MemberInfoEnumerator(object item, Settings settings = default(Settings)) : this((Type)(item is Type || item is null ? item : item.GetType()), settings)
      {

      }

      public IEnumerator<MemberInfo> GetEnumerator()
      {
         if (Type == null) yield break;
         IEnumerable<MemberInfo> member = null;
         if (EnumerationSettings.Flags.HasValue) member = Type.GetMembers(EnumerationSettings.Flags.Value);
         else member = Type.GetMembers();
         foreach (var mi in member)
         {
            var skipped = EnumerationSettings.Skip?.Invoke(mi);
            if (skipped.HasValue && skipped.Value) continue;
            yield return mi;
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}
