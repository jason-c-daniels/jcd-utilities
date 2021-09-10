using System;
using System.Collections.Generic;
using System.Numerics;

namespace Jcd.Utilities.Test.TestHelpers
{
   /// <summary>
   /// An XUnit data provider. This one provides lists of numeric data of a various sorts.
   /// </summary>
   public class NumericMemberDataProvider
   {
      /// <summary>
      /// Provides a set of BigIntegers
      /// </summary>
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

      /// <summary>
      /// Provides a set of Bytes
      /// </summary>
      public static IEnumerable<object[]> Bytes()
      {
         byte two = 2;
         byte one = 1;
         yield return new[] { (object)byte.MaxValue };
         yield return new[] { (object)byte.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of Decimals
      /// </summary>
      public static IEnumerable<object[]> Decimals()
      {
         decimal two = 2;
         decimal one = 1;
         yield return new[] { (object)decimal.MaxValue };
         yield return new[] { (object)decimal.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of Doubles
      /// </summary>
      public static IEnumerable<object[]> Doubles()
      {
         double two = 2;
         double one = 1;
         yield return new[] { (object)double.MaxValue };
         yield return new[] { (object)double.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of Int16s
      /// </summary>
      public static IEnumerable<object[]> Int16s()
      {
         short two = 2;
         short one = 1;
         yield return new[] { (object)short.MaxValue };
         yield return new[] { (object)short.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of Int32s
      /// </summary>
      public static IEnumerable<object[]> Int32s()
      {
         var two = 2;
         var one = 1;
         yield return new[] { (object)int.MaxValue };
         yield return new[] { (object)int.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of Int64s
      /// </summary>
      public static IEnumerable<object[]> Int64s()
      {
         long two = 2;
         long one = 1;
         yield return new[] { (object)long.MaxValue };
         yield return new[] { (object)long.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of non-numeric data.
      /// </summary>
      public static IEnumerable<object[]> NonNumbersCollection()
      {
         yield return new[] { new object() };
         yield return new[] { (object)new[] { 1, 2, 3 } };
         yield return new[] { new Exception() };
      }

      /// <summary>
      /// Provides a set of signed bytes
      /// </summary>
      public static IEnumerable<object[]> SBytes()
      {
         sbyte two = 2;
         sbyte one = 1;
         yield return new[] { (object)sbyte.MaxValue };
         yield return new[] { (object)sbyte.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of Singles
      /// </summary>
      public static IEnumerable<object[]> Singles()
      {
         float two = 2;
         float one = 1;
         yield return new[] { (object)float.MaxValue };
         yield return new[] { (object)float.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of UInt16s
      /// </summary>
      public static IEnumerable<object[]> UInt16s()
      {
         ushort two = 2;
         ushort one = 1;
         yield return new[] { (object)ushort.MaxValue };
         yield return new[] { (object)ushort.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of UInt32s
      /// </summary>
      public static IEnumerable<object[]> UInt32s()
      {
         uint two = 2;
         uint one = 1;
         yield return new[] { (object)uint.MaxValue };
         yield return new[] { (object)uint.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of UInt64s
      /// </summary>
      public static IEnumerable<object[]> UInt64s()
      {
         ulong two = 2;
         ulong one = 1;
         yield return new[] { (object)ulong.MaxValue };
         yield return new[] { (object)ulong.MinValue };
         yield return new[] { (object)two };
         yield return new[] { (object)one };
      }

      /// <summary>
      /// Provides a set of BigIntegers from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciBigIntegers()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(long.MaxValue * (BigInteger)15))
         {
            yield return new[] { (object)bi };
         }
      }

      /// <summary>
      /// Provides a set of UInt64s from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciUInt64s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(ulong.MaxValue))
         {
            var v = (ulong)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Int64s from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciInt64s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(long.MaxValue))
         {
            var v = (long)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of UInt32s from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciUInt32s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(uint.MaxValue))
         {
            var v = (uint)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Int32s from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciInt32s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(int.MaxValue))
         {
            var v = (int)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of UInt16s from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciUInt16s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(ushort.MaxValue))
         {
            var v = (ushort)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Int16s from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciInt16s()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(short.MaxValue))
         {
            var v = (short)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Bytes from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciBytes()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(byte.MaxValue))
         {
            var v = (byte)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of SBytes from the fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> FibonacciSBytes()
      {
         foreach (var bi in new NaiiveFibonacciGenerator(sbyte.MaxValue))
         {
            var v = (sbyte)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of SBytes from the negative fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> NegativeFibonacciSBytes()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(sbyte.MinValue))
         {
            var v = (sbyte)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Int16s from the negative fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> NegativeFibonacciInt16s()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(short.MinValue))
         {
            var v = (short)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Int32s from the negative fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> NegativeFibonacciInt32s()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(int.MinValue))
         {
            var v = (int)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of Int64s from the negative fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> NegativeFibonacciInt64s()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(long.MinValue))
         {
            var v = (long)bi;
            yield return new[] { (object)v };
         }
      }

      /// <summary>
      /// Provides a set of BigIntegers from the negative fibonacci sequence,
      /// </summary>
      public static IEnumerable<object[]> NegativeFibonacciBigIntegers()
      {
         foreach (var bi in new NegativeNaiiveFibonacciGenerator(long.MinValue * (BigInteger)15))
         {
            yield return new[] { (object)bi };
         }
      }
   }
}