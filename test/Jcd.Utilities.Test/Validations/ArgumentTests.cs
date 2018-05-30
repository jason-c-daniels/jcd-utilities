using Jcd.Utilities.Validations;
using System;
using System.Linq;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
    public class ArgumentTests
    {
        #region exception helpers		

        [Theory]
        [InlineData(0,1,null,null)]
        [InlineData(0, 1, null, "message")]
        [InlineData(0, 1, "param", "message")]
        [InlineData(0, 1, "param", null)]
        [InlineData(0, 1, "", null)]
        [InlineData(0, 1, "", "message")]
        [InlineData(0, 1, " ", null)]
        [InlineData(0, 1, " ", "message")]
        public void RaiseExpectationViolation(int expected, int actual, string paramName, string message)
        {
            var ex=Assert.Throws<ArgumentException>(() => Argument.RaiseExpectationViolation<int>(expected, actual, paramName, message));
            if (message == null)
            {
                Assert.Contains("Expect", ex.Message);
                Assert.Contains("to be", ex.Message);
                Assert.Contains("it was", ex.Message);
            }
            else
            {
                Assert.StartsWith(message,ex.Message);
            }
            if (string.IsNullOrWhiteSpace(paramName))
            {
                Assert.StartsWith(Argument.UnspecifiedParamName, ex.ParamName );
            }
            else
            {
                Assert.StartsWith(paramName, ex.ParamName);
            }
        }

        [Theory]
        [InlineData("param",null)]
        [InlineData("param","message")]
        [InlineData(null, "message")]
        [InlineData("", "message")]
        [InlineData(" ", "message")]
        public void RaiseArgumentException(string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentException>(() => Argument.RaiseArgumentException(paramName, message));
            if (message == null)
            {
                Assert.Contains("contains an invalid value", ex.Message);
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

        [Theory]
        [InlineData("param", null)]
        [InlineData("param", "message")]
        [InlineData(null, "message")]
        [InlineData("", "message")]
        [InlineData(" ", "message")]
        public void RaiseArgumentNullException(string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Argument.RaiseArgumentNullException(paramName, message));
            if (message == null)
            {
                Assert.Contains("expected non-null", ex.Message);
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

        [Theory]
        [InlineData(1, 2, 5, "param", null)]
        [InlineData(1, 2, 5, "param", "message")]
        [InlineData(1, 2, 5, null, "message")]
        [InlineData(1, 2, 5, "", "message")]
        [InlineData(1, 2, 5, " ", "message")]
        public void RaiseArgumentOutOfRangeException_WithNullMessage_ExpectExceptionWithDefaultMessage(int actual, int min, int max, string paramName, string message)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.RaiseArgumentOutOfRangeException<int>(actual,min,max,paramName, message));
            if (message == null)
            {
                Assert.Contains("Expected value within range", ex.Message);
                Assert.Contains(actual.ToString(), ex.Message);
                Assert.Contains(min.ToString(), ex.Message);
                Assert.Contains(max.ToString(), ex.Message);
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

        #endregion

        #region Boolean and Null checks
        [Fact]
        public void IsTrue()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void IsFalse()
        {
            throw new NotImplementedException();
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
        #endregion

        #region collection operations
        [Fact]
        public void IsEmptyCollection()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void HasItems()
        {
            throw new NotImplementedException();
        }

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
        #endregion

        #region string operations
        [Fact]
        public void IsEmptyString()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void HasData()
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
        public void IsNullOrEmpty()
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
        public void IsNotNullOrEmpty()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region range and relational operations

        [Fact]
        public void AreSameObject()
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
        public void AreEqual()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void InRange()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void NotInRange()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region custom and multi-condition operations
        [Fact]
        public void Passes()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Fails()
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
        #endregion
    }
}
