﻿using System;
using System.Numerics;
using Jcd.Utilities.Extensions;
using Jcd.Utilities.Test.Extensions;
using Xunit;

namespace Jcd.Utilities.Test.Formatting
{
    public class IntegerFormatterTests
    {
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
                var encoder = IntegerEncoders.Hexdecimal;
                if (number.IsBigIntegerType())
                {
                    var encoded = encoder.Format((BigInteger)number);
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

        //[Theory]
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
                var encoded = encoder.Format((BigInteger)number);
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


        //[Theory]
        [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
        private void CrockFordEncoder_Int64RoundTrip_ExpectValuesMatch(object number)
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
    }
}