using Jcd.Utilities.Test.TestHelpers;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Jcd.Utilities.Test.Extensions
{
   //NOTE: don't yield a 0, it will confuse XUnit into thinking tests have already been run, when we need type info, not value info.

   public class NumericMemberDataProvider
   {

      public static IEnumerable<object[]> BigIntegers()
      {
         var biMax = new BigInteger(ulong.MaxValue) * 2;
         var biMin = new BigInteger(ulong.MinValue) * 2;
         var bi2 = new BigInteger(2);
         var bi1 = new BigInteger(1);
         yield return new[] { (object)biMax };
         yield return new[] { (object)biMin };
         yield return new[] { (object)bi2 };
         yield return new[] { (object)bi1 };
      }

      public static IEnumerable<object[]> Bytes()
      {
         byte two = 2;
         byte one = 1;
         yield return new[] { (object)byte.MaxValue };
         yield return new[] { (object)byte.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> Decimals()
      {
         decimal two = 2;
         decimal one = 1;
         yield return new[] { (object)decimal.MaxValue };
         yield return new[] { (object)decimal.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> Doubles()
      {
         double two = 2;
         double one = 1;
         yield return new[] { (object)double.MaxValue };
         yield return new[] { (object)double.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> Int16s()
      {
         short two = 2;
         short one = 1;
         yield return new[] { (object)short.MaxValue };
         yield return new[] { (object)short.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> Int32s()
      {
         var two = 2;
         var one = 1;
         yield return new[] { (object)int.MaxValue };
         yield return new[] { (object)int.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> Int64s()
      {
         long two = 2;
         long one = 1;
         yield return new[] { (object)long.MaxValue };
         yield return new[] { (object)long.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> NonNumbersCollection()
      {
         yield return new[] { new object() };
         yield return new[] { (object)new[] { 1, 2, 3 } };
         yield return new[] { new Exception() };
      }

      public static IEnumerable<object[]> SBytes()
      {
         sbyte two = 2;
         sbyte one = 1;
         yield return new[] { (object)sbyte.MaxValue };
         yield return new[] { (object)sbyte.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> Singles()
      {
         float two = 2;
         float one = 1;
         yield return new[] { (object)float.MaxValue };
         yield return new[] { (object)float.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> UInt16s()
      {
         ushort two = 2;
         ushort one = 1;
         yield return new[] { (object)ushort.MaxValue };
         yield return new[] { (object)ushort.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> UInt32s()
      {
         uint two = 2;
         uint one = 1;
         yield return new[] { (object)uint.MaxValue };
         yield return new[] { (object)uint.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      public static IEnumerable<object[]> UInt64s()
      {
         ulong two = 2;
         ulong one = 1;
         yield return new[] { (object)ulong.MaxValue };
         yield return new[] { (object)ulong.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }


      public static IEnumerable<object[]> FibonacciiBigIntegers()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(long.MaxValue*(BigInteger)15))
         {
            yield return new[] { (object)bi };
         }
      }


      public static IEnumerable<object[]> FibonacciiUInt64s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(ulong.MaxValue))
         {
            var v = (ulong)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> FibonacciiInt64s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(long.MaxValue))
         {
            var v = (long)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> FibonacciiUInt32s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(uint.MaxValue))
         {
            var v = (uint)bi;
            yield return new [] { (object) v };
         }
      }

      public static IEnumerable<object[]> FibonacciiInt32s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(int.MaxValue))
         {
            var v = (int)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> FibonacciiUInt16s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(ushort.MaxValue))
         {
            var v = (ushort)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> FibonacciiInt16s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(short.MaxValue))
         {
            var v = (short)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> FibonacciiBytes()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(byte.MaxValue))
         {
            var v = (byte)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> FibonacciiSBytes()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(sbyte.MaxValue))
         {
            var v = (sbyte)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> NegativeFibonacciiSBytes()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(sbyte.MinValue))
         {
            var v = (sbyte)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> NegativeFibonacciiInt16s()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(short.MinValue))
         {
            var v = (short)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> NegativeFibonacciiInt32s()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(int.MinValue))
         {
            var v = (int)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> NegativeFibonacciiInt64s()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(long.MinValue))
         {
            var v = (long)bi;
            yield return new[] { (object)v };
         }
      }

      public static IEnumerable<object[]> NegativeFibonacciiBigIntegers()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(long.MinValue*(BigInteger)15))
         {
            yield return new[] { (object)bi };
         }
      }
   }
}