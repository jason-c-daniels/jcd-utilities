using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Jcd.Utilities.Test.Extensions
{
    //NOTE: don't yield a 0, it will confuse XUnit into thinking tests have already been run, when we need type info, not value info.

    public class NumericMemberDataProvider
    {
        public static IEnumerable<object[]> Bytes()
        {
            byte two = 2;
            byte one = 1;
            yield return new[] { (object)Byte.MaxValue };
            yield return new[] { (object)Byte.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> SBytes()
        {
            sbyte two = 2;
            sbyte one = 1;
            yield return new[] { (object)SByte.MaxValue };
            yield return new[] { (object)SByte.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> UInt16s()
        {
            ushort two = 2;
            ushort one = 1;
            yield return new[] { (object)UInt16.MaxValue };
            yield return new[] { (object)UInt16.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> Int16s()
        {
            short two = 2;
            short one = 1;
            yield return new[] { (object)Int16.MaxValue };
            yield return new[] { (object)Int16.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> UInt32s()
        {
            uint two = 2;
            uint one = 1;
            yield return new[] { (object)UInt32.MaxValue };
            yield return new[] { (object)UInt32.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> Int32s()
        {
            int two = 2;
            int  one = 1;
            yield return new[] { (object)Int32.MaxValue };
            yield return new[] { (object)Int32.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> UInt64s()
        {
            ulong two = 2;
            ulong one = 1;
            yield return new[] { (object)UInt64.MaxValue };
            yield return new[] { (object)UInt64.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> Int64s()
        {
            long two = 2;
            long one = 1;
            yield return new[] { (object)Int64.MaxValue };
            yield return new[] { (object)Int64.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }


        public static IEnumerable<object[]> Decimals()
        {
            decimal two = 2;
            decimal one = 1;
            yield return new[] { (object)Decimal.MaxValue };
            yield return new[] { (object)Decimal.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> Singles()
        {
            Single two = 2;
            Single one = 1;
            yield return new[] { (object)Single.MaxValue };
            yield return new[] { (object)Single.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> Doubles()
        {
            double two = 2;
            double one = 1;
            yield return new[] { (object)Double.MaxValue };
            yield return new[] { (object)Double.MinValue };
            yield return new[] { (object)two };
            yield return new[] { (object)one };
        }

        public static IEnumerable<object[]> BigIntegers()
        {
            var biMax = new BigInteger(UInt64.MaxValue) * 2;
            var biMin = new BigInteger(UInt64.MinValue) * 2;
            var bi2 = new BigInteger(2);
            var bi1 = new BigInteger(1);
            yield return new[] { (object)biMax };
            yield return new[] { (object)biMin };
            yield return new[] { (object)bi2 };
            yield return new[] { (object)bi1 };
        }

        public static IEnumerable<object[]> NonNumbersCollection()
        {
            yield return new[] { new object() };
            yield return new[] { (object)new [] { 1,2,3 } };
            yield return new[] { new Exception() };
        }

    }
}