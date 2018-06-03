using Jcd.Utilities.Validations;
using System;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
    public class ArgumentTests
    {
        #region Private Fields

        private static string defaultArgumentExceptionMessage = "contains an invalid value";
        private static string defaultArgumentNullExceptionMessage = "expected non-null";
        private static string defaultArgumentOutOfRangeMessage = "Expected value within range";
        private static string[] defaultExpectationViolationMessage = { "Expect", "to be", "it was" };

        #endregion Private Fields

        #region exception helpers

        [Theory]
        [InlineData("param", null)]
        [InlineData("param", "message")]
        [InlineData(null, "message")]
        [InlineData("", "message")]
        [InlineData(" ", "message")]
        public void RaiseArgumentException(string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentException>(() => Argument.RaiseArgumentException(paramName, message));
            ValidateMessageAndParamName(ex, paramName, message, defaultArgumentExceptionMessage);
        }

        [Theory]
        [InlineData("param", null)]
        [InlineData("param", "message")]
        [InlineData(null, "message")]
        [InlineData("", "message")]
        [InlineData(" ", "message")]
        public void RaiseArgumentNullException(string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Argument.RaiseArgumentNullException(paramName, message));
            ValidateMessageAndParamName(ex, paramName, message, defaultArgumentNullExceptionMessage);
        }

        [Theory]
        [InlineData(1, 2, 5, "param", null)]
        [InlineData(1, 2, 5, "param", "message")]
        [InlineData(1, 2, 5, null, "message")]
        [InlineData(1, 2, 5, "", "message")]
        [InlineData(1, 2, 5, " ", "message")]
        public void RaiseArgumentOutOfRangeException_WithNullMessage_ExpectExceptionWithDefaultMessage(int actual, int min, int max,
            string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.RaiseArgumentOutOfRangeException<int>(actual, min, max,
                     paramName, message));
            ValidateMessageAndParamName(ex, paramName, message, defaultArgumentOutOfRangeMessage);
        }

        [Theory]
        [InlineData(0, 1, null, null)]
        [InlineData(0, 1, null, "message")]
        [InlineData(0, 1, "param", "message")]
        [InlineData(0, 1, "param", null)]
        [InlineData(0, 1, "", null)]
        [InlineData(0, 1, "", "message")]
        [InlineData(0, 1, " ", null)]
        [InlineData(0, 1, " ", "message")]
        public void RaiseExpectationViolation(int expected, int actual, string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentException>(() => Argument.RaiseExpectationViolation<int>(expected, actual, paramName, message));

            ValidateMessageAndParamName(ex, paramName, message, defaultExpectationViolationMessage);
        }

        #endregion exception helpers

        #region Boolean and Null checks

        [Fact]
        public void IsFalse_PassingFalse_ExpectNoExceptions()
        {
            Argument.IsFalse(false);
            Argument.IsFalse(false, "param");
            Argument.IsFalse(false, "param", "message");
        }

        [Theory]
        [InlineData("param", null)]
        [InlineData("param", "message")]
        [InlineData(null, "message")]
        [InlineData("", "message")]
        [InlineData(" ", "message")]
        public void IsFalse_PassingTrue_ExpectArgumentException(string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentException>(() => Argument.IsFalse(true, paramName, message));
            ValidateMessageAndParamName(ex, paramName, message, defaultExpectationViolationMessage);
        }

        [Fact]
        public void IsNotNull()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNull()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [InlineData("param", null)]
        [InlineData("param", "message")]
        [InlineData(null, "message")]
        [InlineData("", "message")]
        [InlineData(" ", "message")]
        public void IsTrue_PassingFalse_ExpectArgumentException(string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentException>(() => Argument.IsTrue(false, paramName, message));
            ValidateMessageAndParamName(ex, paramName, message, defaultExpectationViolationMessage);
        }

        [Fact]
        public void IsTrue_PassingTrue_ExpectNoExceptions()
        {
            Argument.IsTrue(true);
            Argument.IsTrue(true, "param");
            Argument.IsTrue(true, "param", "message");
        }

        #endregion Boolean and Null checks

        #region collection operations

        [Fact]
        public void Contains()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DoesNotContain()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void HasItems()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsEmptyCollection()
        {
            throw new NotImplementedException();
        }

        #endregion collection operations

        #region string operations

        [Fact]
        public void HasData()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsEmptyString()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNotNullOrEmpty()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNotNullOrWhitespace()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNotNullWhitespaceOrEmpty()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNotWhitespace()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNotWhitespaceOrEmpty()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNullOrEmpty()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNullOrWhitespace()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsNullWhitespaceOrEmpty()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsWhitespace()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsWhitespaceOrEmpty()
        {
            throw new NotImplementedException();
        }

        #endregion string operations

        #region range and relational operations

        [Fact]
        public void AreEqual()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void AreSameObject()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void InRange()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsGreaterThan()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsGreaterThanOrEqual()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsLessThan()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsLessThanOrEqual()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void NotInRange()
        {
            throw new NotImplementedException();
        }

        #endregion range and relational operations

        #region custom and multi-condition operations

        [Fact]
        public void Fails()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void FailsAll()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void FailsAny()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Passes()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PassesAll()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PassesAny()
        {
            throw new NotImplementedException();
        }

        #endregion custom and multi-condition operations

        #region Private Methods

        private static void ValidateMessageAndParamName(ArgumentException ex, string paramName, string message,
            string expectedDefaultMessage)
        {
            ValidateMessageAndParamName(ex, paramName, message, new[] { expectedDefaultMessage });
        }

        private static void ValidateMessageAndParamName(ArgumentException ex, string paramName, string message,
              string[] expectedDefaultMessage)
        {
            if (message == null)
            {
                foreach (var text in expectedDefaultMessage)
                {
                    Assert.Contains(text, ex.Message);
                }
            }
            else
            {
                Assert.StartsWith(message, ex.Message);
            }

            if (string.IsNullOrWhiteSpace(paramName))
            {
                Assert.StartsWith(Argument.UnspecifiedParamName, ex.ParamName);
            }
            else
            {
                Assert.StartsWith(paramName, ex.ParamName);
            }
        }

        #endregion Private Methods
    }
}