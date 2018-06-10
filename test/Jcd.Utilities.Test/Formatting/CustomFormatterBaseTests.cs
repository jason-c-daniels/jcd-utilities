using System;
using System.Collections.Generic;
using Jcd.Utilities.Formatting;
using Xunit;

namespace Jcd.Utilities.Test.Formatting
{
   public class CustomFormatterBaseTests
   {
      const string fake_formatted_result = "result";
      private static FakeCustomFormatter CreateSut()
      {
         return new FakeCustomFormatter(fake_formatted_result, FakeCustomFormatter.handledTypes, FakeCustomFormatter.Format);
      }

      public class FakeCustomFormatter : CustomFormatterBase
      {
         #region Public Fields

         public static Type[] handledTypes = {typeof(int), typeof(float)};

         #endregion Public Fields

         #region Protected Fields

         protected string formatResult;

         #endregion Protected Fields

         #region Public Constructors

         public FakeCustomFormatter(string formatResult, IEnumerable<Type> handledTypes = null,
                                    Func<ICustomFormatter, string, object, IFormatProvider, string> formatFunction = null) : base(
                                       handledTypes, formatFunction)
         {
            this.formatResult = formatResult;
         }

         #endregion Public Constructors

         #region Public Methods

         public static string Format(ICustomFormatter formatter, string fmt, object arg, IFormatProvider fmtProvider)
         {
            if (formatter is FakeCustomFormatter self) {
               return self.formatResult;
            }

            return null;
         }

         #endregion Public Methods
      }

      [Fact]
      public void Constructor_EmptyHandledTypes_ExpectArgumentExceptions()
      {
         Assert.Throws<ArgumentException>(() => new FakeCustomFormatter(null, new Type[] { }, null));
      }

      [Fact]
      public void Constructor_NullParams_ExpectArgumentNullExceptions()
      {
         Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter(null, null, null));
         Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter("", null, null));
         Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter("", new[] {typeof(int)}, null));
         Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter("", null,
               (formatter, s, o, arg4) => FakeCustomFormatter.Format(formatter, s, o, arg4)));
      }

      [Fact]
      public void Format_NullParameters_ExpectArgumentNullException()
      {
         var sut = CreateSut();
         Assert.Throws<ArgumentNullException>(() => sut.Format("", null, sut));
         Assert.Throws<ArgumentNullException>(() => sut.Format("", new object(), null));
         Assert.Throws<ArgumentNullException>(() => sut.Format(null, new object(), sut));
      }


      /// <summary>
      /// Validate that Format Returns default format for an unhandled data type
      /// </summary>
      [Fact]
      public void Format_WhenGivenUnhandledType_ReturnsDefaultFormat()
      {
         // setup
         var sut = CreateSut();
         long arg = (long)9;
         // act
         var result=sut.Format("", arg, sut);
         // assert
         Assert.Equal("9", result);
      }

      /// <summary>
      /// Validate that Format Returns default format for an unhandled data type
      /// </summary>
      [Fact]
      public void Format_WhenGivenWrongFormatProvider_ReturnsNull()
      {
         // setup
         var sut = CreateSut();
         var otherCustomFormatter = IntegerEncoders.Decimal;
         long arg = (long)9;
         // act
         // assert
         Assert.Null(sut.Format("", arg, otherCustomFormatter));
      }

      /// <summary>
      /// Validate that Format Returns default format for an unhandled data type
      /// </summary>
      [Fact]
      public void Format_WhenGivenHandledType_ReturnsFormattedResult()
      {
         // setup
         var sut = CreateSut();
         int arg = 9;
         // act
         var result = sut.Format("", arg, sut);
         // assert
         Assert.Equal(fake_formatted_result, result);
      }



      /// <summary>
      /// Validate that GetFormat throws ArgumentNullException when given null format type.
      /// </summary>
      [Fact]
      public void GetFormat_WhenGivenNullFormatType_ThrowsArgumentNullException()
      {
         var sut = CreateSut();
         Assert.Throws<ArgumentNullException>(() => sut.GetFormat(null));
      }


      /// <summary>
      /// Validate that GetFormat returns null when given non-custom formatter type.
      /// </summary>
      [Fact]
      public void GetFormat_WhenGivenNonCustomFormatterType_ReturnsNull()
      {
         var sut = CreateSut();
         Assert.Null(sut.GetFormat(typeof(int)));
      }

      /// <summary>
      /// Validate that GetFormat returns itself when given custom formatter type.
      /// </summary>
      [Fact]
      public void GetFormat_WhenGivenCustomFormatterType_ReturnsSelf()
      {
         var sut = CreateSut();
         Assert.Same(sut, sut.GetFormat(typeof(ICustomFormatter)));
      }
   }
}