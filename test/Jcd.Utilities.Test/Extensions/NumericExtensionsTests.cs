using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;
using Jcd.Utilities.Extensions;

namespace Jcd.Utilities.Test.Extensions
{
    public class NumericExtensionsTests
    {

        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.NonNumbersCollection), MemberType = typeof(NumericMemberDataProvider))]
        public void IsNumericType_NonnumericType_ExpectFalse(object self)
        {
            Assert.False(self.IsNumericType());
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
        [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
        public void IsNumericType_NumericType_ExpectTrue(object self)
        {
            Assert.True(self.IsNumericType());
        }

        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
        public void IsFloatType_FloatType_ExpectTrue( object self)
        {
            Assert.True(self.IsFloatType());
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
        [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
        public void IsFloatType_NonFloatType_ExpectFalse(object self)
        {
            Assert.False(self.IsFloatType());
        }

        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
        public void IsDecimalType_DecimalType_ExpectTrue( object self)
        {
            Assert.True(self.IsDecimalType());
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
        [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
        public void IsDecimalType_NonDecimalType_ExpectFalse(object self)
        {
            Assert.False(self.IsDecimalType());
        }


        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
        public static void IsIntegerType_IntegerType_ExpectTrue( object self)
        {
            Assert.True(self.IsIntegerType());
        }


        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
        public static void IsIntegerType_NonIntegerType_ExpectFalse(object self)
        {
            Assert.False(self.IsIntegerType());
        }

        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
        public void IsSignedType_SignedType_ExpectTrue(object self)
        {
            Assert.True(self.IsSignedType());
        }

        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
        public void IsSignedType_UnsignedType_ExpectFalse(object self)
        {
            Assert.False(self.IsSignedType());
        }


        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
        public void IsUnsignedType_SignedType_ExpectFalse(object self)
        {
            Assert.False(self.IsUnsignedType());
        }

        [Theory]
        [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
        [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
        public void IsUnsignedType_UnsignedType_ExpectTrue(object self)
        {
            Assert.True(self.IsUnsignedType());
        }
    }
}
