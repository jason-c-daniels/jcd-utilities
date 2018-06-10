using System;
using System.Numerics;
using Jcd.Utilities.Extensions;
using Jcd.Utilities.Test.Extensions;
using Jcd.Utilities.Test.TestHelpers;
using Xunit;

namespace Jcd.Utilities.Test.Formatting
{
   public class IntegerEncoderTests
   {
      #region CrockFordEncoder and Hex Tests

      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
      public void CrockFordEncoder_BigIntegerRoundTrip_ExpectValuesMatch(object number)
      {
         var encoder = IntegerEncoders.Base32_Crockford;

         if (number.IsBigIntegerType())
         {
            var encoded = encoder.Format((BigInteger) number);
            var decoded = encoder.ParseBigInteger(encoded);
            Assert.Equal(number, decoded);
         }
         else if (number.IsSignedType())
         {
            var num = Convert.ToInt64(number);
            var encoded = encoder.Format(num);
            var decoded = encoder.ParseInt64(encoded);
            Assert.Equal(num, decoded);
         }
         else
         {
            var num = Convert.ToUInt64(number);
            var encoded = encoder.Format(Convert.ToUInt64(number));
            var decoded = encoder.ParseUInt64(encoded);
            Assert.Equal(num, decoded);
         }
      }

      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
      public void HexadecimalEncoder_BigIntegerRoundTrip_ExpectValuesMatch(object number)
      {
         try
         {
            var encoder = IntegerEncoders.Hexadecimal;

            if (number.IsBigIntegerType())
            {
               var encoded = encoder.Format((BigInteger) number);
               var decoded = encoder.ParseBigInteger(encoded);
               Assert.Equal(number, decoded);
            }
            else if (number.IsSignedType())
            {
               var num = Convert.ToInt64(number);
               var encoded = encoder.Format(num);
               var decoded = encoder.ParseInt64(encoded);
               Assert.Equal(num, decoded);
            }
            else
            {
               var num = Convert.ToUInt64(number);
               var encoded = encoder.Format(Convert.ToUInt64(number));
               var decoded = encoder.ParseUInt64(encoded);
               Assert.Equal(num, decoded);
            }
         }
         catch
         {
            Console.WriteLine($"Error on {number}");
            throw;
         }
      }

      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
      public void CrockFordEncoder_Int64RoundTrip_ExpectValuesMatch(object number)
      {
         var encoder = IntegerEncoders.Base32_Crockford;

         if (number.IsSignedType())
         {
            var num = Convert.ToInt64(number);
            var encoded = encoder.Format(num);
            var decoded = encoder.ParseInt64(encoded);
            Assert.Equal(num, decoded);
         }
         else
         {
            var num = Convert.ToUInt64(number);
            var encoded = encoder.Format(Convert.ToUInt64(number));
            var decoded = encoder.ParseUInt64(encoded);
            Assert.Equal(num, decoded);
         }
      }

      #endregion CrockFordEncoder and Hex Tests

      #region Format positive number tests
      /// <summary>
      /// Validate that Format Returns correct hex string when given UInt64.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiBigIntegers), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenBigInteger_ReturnsCorrectHexString(BigInteger data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         // for some odd reason BigInteger zero pads its hex representation. Ensure we've stripped the leading zeros from both. We're looking for hex encoding equivalence, not string equivalence.
         Assert.Equal(expected.TrimLeadingZeros(), actual.TrimLeadingZeros());
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseBigInteger(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given UInt64.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiUInt64s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenUInt64_ReturnsCorrectHexString(UInt64 data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseUInt64(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given Int64.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiInt64s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenInt64_ReturnsCorrectHexString(Int64 data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseInt64(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given UInt32.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiUInt32s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenUInt32_ReturnsCorrectHexString(UInt32 data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseUInt32(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given Int32.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiInt32s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenInt32_ReturnsCorrectHexString(Int32 data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseInt32(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given UInt16.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiUInt16s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenUInt16_ReturnsCorrectHexString(UInt16 data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseUInt16(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given Int16.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiInt16s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenInt16_ReturnsCorrectHexString(Int16 data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseInt16(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given UInt16.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiBytes), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenByte_ReturnsCorrectHexString(Byte data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseByte(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given Int16. And validate round trip. (Parse)
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.FibonacciiSBytes), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenSByte_ReturnsCorrectHexString(SByte data)
      {
         var expected = data.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.Equal(expected, actual);
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseSByte(actual));
      }

      #endregion

      #region negative number formatting test
      /// <summary>
      /// Validate that Format Returns correct hex string when given negative sbyte.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.NegativeFibonacciiSBytes), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenNegativeSByte_ReturnsCorrectHexString(SByte data)
      {
         var abs = (byte)Math.Abs((short)data);
         var expected = abs.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.EndsWith(expected, actual);

         if (data < 0) {
            Assert.StartsWith("-", actual);
         }

         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseSByte(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given negative short.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.NegativeFibonacciiInt16s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenNegativeInt16_ReturnsCorrectHexString(short data)
      {
         var abs = (ushort)Math.Abs((int)data);
         var expected = abs.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.EndsWith(expected, actual);

         if (data < 0)
         {
            Assert.StartsWith("-", actual);
         }

         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseInt16(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given negative int.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.NegativeFibonacciiInt32s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenNegativeInt32_ReturnsCorrectHexString(int data)
      {
         var abs = (uint)Math.Abs((long)data);
         var expected = abs.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.EndsWith(expected, actual);

         if (data < 0)
         {
            Assert.StartsWith("-", actual);
         }

         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseInt32(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given negative int.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.NegativeFibonacciiInt64s), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenNegativeInt64_ReturnsCorrectHexString(long data)
      {
         var abs = (ulong)((BigInteger)data * -1);
         var expected = abs.ToString("X").ToLowerInvariant();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();
         Assert.EndsWith(expected, actual);

         if (data < 0)
         {
            Assert.StartsWith("-", actual);
         }

         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseInt64(actual));
      }

      /// <summary>
      /// Validate that Format Returns correct hex string when given negative int.
      /// </summary>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.NegativeFibonacciiBigIntegers), MemberType = typeof(NumericMemberDataProvider))]
      public void Format_WhenGivenNegativebigInteger_ReturnsCorrectHexString(BigInteger data)
      {
         var abs = data * -1;
         var expected = abs.ToString("X").ToLowerInvariant().TrimLeadingZeros();
         var actual = IntegerEncoders.Hexadecimal.Format(data).ToLowerInvariant();

         if (data < 0)
         {
            Assert.StartsWith("-", actual);
         }

         Assert.EndsWith(expected, actual.Replace("-", ""));
         Assert.Equal(data, IntegerEncoders.Hexadecimal.ParseBigInteger(actual));
      }
      #endregion

      //TODO: Add tests for Parse: Byte, Int16, Int32, SByte, UInt16, UInt32, result is negative - UInt64
      //TODO: Add tests for TryParse: BigInteger, SByte, Int16, Int32, Int64, Byte, UInt16, UInt32, UInt64
      //TODO: Add tests for FormatObject: all data types.
   }
}