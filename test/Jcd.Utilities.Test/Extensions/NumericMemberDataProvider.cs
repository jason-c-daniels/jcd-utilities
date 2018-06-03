using System;
using System.Collections.Generic;
using System.Numerics;

namespace Jcd.Utilities.Test.Extensions
{
   //NOTE: don't yield a 0, it will confuse XUnit into thinking tests have already been run, when we need type info, not value info.

   public class NumericMemberDataProvider
   {
      #region Public Methods

      public static IEnumerable<object[]> BigIntegers()
      {
         var biMax = new BigInteger(ulong.MaxValue) * 2;
         var biMin = new BigInteger(ulong.MinValue) * 2;
         var bi2 = new BigInteger(2);
         var bi1 = new BigInteger(1);
         yield return new[] {(object) biMax};
         yield return new[] {(object) biMin};
         yield return new[] {(object) bi2};
         yield return new[] {(object) bi1};
      }

      public static IEnumerable<object[]> Bytes()
      {
         byte two = 2;
         byte one = 1;
         yield return new[] {(object) byte.MaxValue};
         yield return new[] {(object) byte.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> Decimals()
      {
         decimal two = 2;
         decimal one = 1;
         yield return new[] {(object) decimal.MaxValue};
         yield return new[] {(object) decimal.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> Doubles()
      {
         double two = 2;
         double one = 1;
         yield return new[] {(object) double.MaxValue};
         yield return new[] {(object) double.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> Int16s()
      {
         short two = 2;
         short one = 1;
         yield return new[] {(object) short.MaxValue};
         yield return new[] {(object) short.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> Int32s()
      {
         var two = 2;
         var one = 1;
         yield return new[] {(object) int.MaxValue};
         yield return new[] {(object) int.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> Int64s()
      {
         long two = 2;
         long one = 1;
         yield return new[] {(object) long.MaxValue};
         yield return new[] {(object) long.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> NonNumbersCollection()
      {
         yield return new[] {new object()};
         yield return new[] {(object) new[] {1, 2, 3}};
         yield return new[] {new Exception()};
      }

      public static IEnumerable<object[]> SBytes()
      {
         sbyte two = 2;
         sbyte one = 1;
         yield return new[] {(object) sbyte.MaxValue};
         yield return new[] {(object) sbyte.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> Singles()
      {
         float two = 2;
         float one = 1;
         yield return new[] {(object) float.MaxValue};
         yield return new[] {(object) float.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> UInt16s()
      {
         ushort two = 2;
         ushort one = 1;
         yield return new[] {(object) ushort.MaxValue};
         yield return new[] {(object) ushort.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> UInt32s()
      {
         uint two = 2;
         uint one = 1;
         yield return new[] {(object) uint.MaxValue};
         yield return new[] {(object) uint.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      public static IEnumerable<object[]> UInt64s()
      {
         ulong two = 2;
         ulong one = 1;
         yield return new[] {(object) ulong.MaxValue};
         yield return new[] {(object) ulong.MinValue};
         yield return new[] {(object) two};
         yield return new[] {(object) one};
      }

      #endregion Public Methods
   }
}