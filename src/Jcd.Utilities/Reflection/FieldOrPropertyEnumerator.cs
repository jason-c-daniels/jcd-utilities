using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jcd.Utilities.Reflection
{
   public class FieldOrPropertyEnumerator : IEnumerable<FieldOrPropertyInfo>
   {
      private readonly MemberInfoEnumerator _innerEnumerator;
      public struct Settings
      {
         public BindingFlags? Flags;
         public Func<FieldOrPropertyInfo, bool> Skip;
      }

      public Settings EnumerationSettings { get; set; }

      public Type Type { get; }

      public FieldOrPropertyEnumerator(Type type, Settings settings = default(Settings))
      {
         Type = type;
         EnumerationSettings = settings;
         _innerEnumerator = new MemberInfoEnumerator(Type, new MemberInfoEnumerator.Settings { Flags = settings.Flags, Skip = MemberInfoEnumerator.SkipSystemAndNonDataMembers });
      }

      public FieldOrPropertyEnumerator(object item, Settings settings = default(Settings)) 
         : this((Type)(item is Type || item is null ? item : item.GetType()), settings)
      {

      }

      public IEnumerator<FieldOrPropertyInfo> GetEnumerator()
      {
         foreach (var mi in _innerEnumerator)
         {
            var fpi=new FieldOrPropertyInfo(mi, EnumerationSettings.Flags ?? BindingFlags.Public | BindingFlags.Instance);
            var skipped = EnumerationSettings.Skip?.Invoke(fpi);
            if (skipped.HasValue && skipped.Value) continue;
            yield return fpi;
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}
