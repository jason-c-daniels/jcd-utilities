using System;
using System.Collections.Generic;
using Jcd.Utilities.Formatting;
using Xunit;

namespace Jcd.Utilities.Test.Formatting
{
   public class CustomFormatterBaseTests
   {
      private static FakeCustomFormatter CreateSut()
      {
         return new FakeCustomFormatter("result", FakeCustomFormatter.handledTypes, FakeCustomFormatter.Format);
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

      //TODO: HandleOtherFormats: when arg is IFormattable
      //TODO: HandleOtherFormats: when arg is not IFormattable and non-null
      //TODO: HandleOtherFormats: when arg is not IFormattable and null
      //TODO: Format: non-self format provider
      //TODO: Format supported data type
      //TODO: Format unsupported data type
      //TODO: GetFormatType null type
      //TODO: GetFormatType ICustomFormatter type
      //TODO: GetFormatType built-in type.
   }
}