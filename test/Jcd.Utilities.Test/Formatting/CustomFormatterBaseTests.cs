using Jcd.Utilities.Formatting;
using System;
using System.Collections.Generic;
using Xunit;

namespace Jcd.Utilities.Test.Formatting
{
    public class CustomFormatterBaseTests
    {
        public class FakeCustomFormatter : CustomFormatterBase
        {
            public static Type[] handledTypes = new[] { typeof(int), typeof(Single) };
            protected string formatResult;

            public FakeCustomFormatter(string formatResult, IEnumerable<Type> handledTypes = null,
                                       Func<ICustomFormatter, string, object, IFormatProvider, string> formatFunction = null) : base(handledTypes, formatFunction)
            {
                this.formatResult = formatResult;
            }

            public static string Format(ICustomFormatter formatter, string fmt, object arg, IFormatProvider fmtProvider)
            {
                if (formatter is FakeCustomFormatter self)
                {
                    return self.formatResult;
                }

                return null;
            }
        }

        [Fact]
        public void Constructor_NullParams_ExpectArgumentNullExceptions()
        {
            Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter("", null, null));
            Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter("", new Type[] { typeof(int) }, null));
            Assert.Throws<ArgumentNullException>(() => new FakeCustomFormatter("", null, FakeCustomFormatter.Format));
        }

        [Fact]
        public void Constructor_EmptyHandledTypes_ExpectArgumentExceptions()
        {
            Assert.Throws<ArgumentException>(() => new FakeCustomFormatter(null, new Type[] { }, null));
        }

        [Fact]
        public void Format_NullParameters_ExpectArgumentNullException()
        {
            var sut = CreateSut();
            Assert.Throws<ArgumentNullException>(() => sut.Format("", null, sut));
            Assert.Throws<ArgumentNullException>(() => sut.Format("", new object(), null));
            Assert.Throws<ArgumentNullException>(() => sut.Format(null, new object(), sut));
        }

        private static FakeCustomFormatter CreateSut()
        {
            return new FakeCustomFormatter("result", FakeCustomFormatter.handledTypes, FakeCustomFormatter.Format);
        }
    }
}