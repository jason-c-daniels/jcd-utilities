﻿using Jcd.Utilities.Extensions;
using Xunit;

namespace Jcd.Utilities.Test.Extensions
{
   /// <summary>
   /// Tests the validitity of some numeric extensions used by the IntegerEncoder class.
   /// </summary>
   public class NumericExtensionsTests
   {
      #region Public Methods

      /// <summary>
      /// Validate that IsIntegerType returns true for data which is of any integer type.
      /// </summary>
      /// <param name="self">The data item to test.</param>
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
      public static void IsIntegerType_WhenGivenIntegerData_ReturnsTrue(object self)
      {
         Assert.True(self.IsIntegerType());
      }

      /// <summary>
      /// Validate that IsIntegerType returns false for data which is not of any integer type.
      /// </summary>
      /// <param name="self">The data item to test.</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
      public static void IsIntegerType_WhenGivenNonIntegerData_ReturnsFalse(object self)
      {
         Assert.False(self.IsIntegerType());
      }

      /// <summary>
      /// Validate that IsDecimalType returns true for decimal data.
      /// </summary>
      /// <param name="self">The data item to test.</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
      public void IsDecimalType_WhenGivenDecimalData_ReturnsTrue(object self)
      {
         Assert.True(self.IsDecimalType());
      }

      /// <summary>
      /// Validate that IsDecimalType returns false when given non-decimal data.
      /// </summary>
      /// <param name="self">that data to test</param>
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
      public void IsDecimalType_WhenGivenNonDecimalData_ReturnsFalse(object self)
      {
         Assert.False(self.IsDecimalType());
      }

      /// <summary>
      /// Validate that IsFloatType returns true when given floating point data.
      /// </summary>
      /// <param name="self">that data to test</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
      public void IsFloatType_WhenGivenFloatData_ReturnsTrue(object self)
      {
         Assert.True(self.IsFloatType());
      }

      /// <summary>
      /// Validate that IsFloatType returns false when given non-floating point data.
      /// </summary>
      /// <param name="self">that data to test</param>
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
      public void IsFloatType_WhenGivenNonFloatData_ReturnsFalse(object self)
      {
         Assert.False(self.IsFloatType());
      }

      /// <summary>
      /// Validate that IsNumericType returns false when given non-numeric data.
      /// </summary>
      /// <param name="self">that data to test</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.NonNumbersCollection), MemberType = typeof(NumericMemberDataProvider))]
      public void IsNumericType_WhenGivenNonnumericData_ReturnsFalse(object self)
      {
         Assert.False(self.IsNumericType());
      }

      /// <summary>
      /// Validate that IsNumericType returns true when given numeric data.
      /// </summary>
      /// <param name="self">that data to test</param>
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
      public void IsNumericType_WhenGivenNumericData_ReturnsTrue(object self)
      {
         Assert.True(self.IsNumericType());
      }

      /// <summary>
      /// Validate that IsSignedType returns true when given signed numeric data.
      /// </summary>
      /// <param name="self">that data to test</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
      public void IsSignedType_WhenGivenSignedData_ReturnsTrue(object self)
      {
         Assert.True(self.IsSignedType());
      }

      /// <summary>
      /// Validate that IsSignedType returns false when given unsigned numeric data.
      /// </summary>
      /// <param name="self">that data to test</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
      public void IsSignedType_WhenGivenUnsignedData_ReturnsFalse(object self)
      {
         Assert.False(self.IsSignedType());
      }

      /// <summary>
      /// Validate that IsUnsignedType returns false when given signed numeric data.
      /// </summary>
      /// <param name="self">that data to test</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.SBytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Int64s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.BigIntegers), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Decimals), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Doubles), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.Singles), MemberType = typeof(NumericMemberDataProvider))]
      public void IsUnsignedType_WhenGivenSignedData_ReturnsFalse(object self)
      {
         Assert.False(self.IsUnsignedType());
      }

      /// <summary>
      /// Validate that IsUnsignedType returns true when given unsigned numeric data.
      /// </summary>
      /// <param name="self">that data to test</param>
      [Theory]
      [MemberData(nameof(NumericMemberDataProvider.Bytes), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt16s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt32s), MemberType = typeof(NumericMemberDataProvider))]
      [MemberData(nameof(NumericMemberDataProvider.UInt64s), MemberType = typeof(NumericMemberDataProvider))]
      public void IsUnsignedType_WhenGivenUnsignedData_ReturnsTrue(object self)
      {
         Assert.True(self.IsUnsignedType());
      }

      #endregion Public Methods
   }
}