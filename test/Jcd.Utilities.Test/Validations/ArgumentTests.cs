using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
   public class ArgumentTests
   {
      #region Private Fields

      private static readonly string defaultArgumentExceptionMessage = "contains an invalid value";
      private static readonly string defaultArgumentNullExceptionMessage = "expected non-null";
      private static readonly string defaultArgumentOutOfRangeMessage = "Expected value within range";
      private static readonly string defaultNotFoundInCollectionMessage = "not found in";
      private static readonly string[] defaultExpectationViolationMessage = { "Expect", "to be", "it was" };
      private static readonly object nullObject = null;
      private static readonly object nonNullObject = new object();
      private static readonly object nonNullObject2 = new object();
      private static readonly object[] emptyObjectCollection = new object[] { };
      private static readonly object[] nullObjectCollection = null;
      private static readonly List<int> populatedIntCollection = new List<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
      private const int valueNotInList = 400;
      private const int valueInList1 = 1;
      private const int valueInList2 = 4;
      private const int valueInList3 = 9;

      private const string nullString = null;
      private const string emptyString = "";
      private const string allWhitespaceString = "    \r\n\t";
      private const string someWhitespaceString = "    abc \r d \n e \t";
      private const string nonWhitespaceString = "abcdefghijklmnop";

      #endregion Private Fields

      #region exception helpers

      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void RaiseArgumentException_WhenCalled_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.RaiseArgumentException(paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentExceptionMessage);
      }

      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void RaiseArgumentNullException_WhenCalled_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.RaiseArgumentNullException(paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      [Theory]
      [InlineData(1, 2, 5, "param", null)]
      [InlineData(1, 2, 5, "param", "message")]
      [InlineData(1, 2, 5, null, "message")]
      [InlineData(1, 2, 5, "", "message")]
      [InlineData(1, 2, 5, " ", "message")]
      public void RaiseArgumentOutOfRangeException_WhenCalled_ThrowsArgumentOutOfRangeException(int actual, int min, int max,
            string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.RaiseArgumentOutOfRangeException<int>(actual, min, max,
                  paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentOutOfRangeMessage);
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
      public void RaiseExpectationViolation_WhenCalled_ThrowsArgumentExceptionWithSpecialMessage(int expected, int actual,
            string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.RaiseExpectationViolation<int>(expected, actual, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultExpectationViolationMessage);
      }

      #endregion exception helpers

      #region Boolean and Null checks

      [Fact]
      public void IsFalse_WhenGivenFalse_NoExceptionIsThrown()
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
      public void IsFalse_WhenGivenTrue_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsFalse(true, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultExpectationViolationMessage);
      }

      [Fact]
      public void IsNotNull_WhenGivenNonNull_NoExceptionIsThrown()
      {
         Argument.IsNotNull(nonNullObject, "none", "this error shouldn't have happened.");
      }

      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNull_WhenGivenNull_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNull(nullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      [Fact]
      public void IsNull_WhenGivenNull_NoExceptionIsThrown()
      {
         Argument.IsNull(nullObject, "none", "this error shouldn't have happened.");
      }

      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNull_WhenGivenNonNull_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsNull(nonNullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultExpectationViolationMessage);
      }

      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsTrue_WhenGivenFalse_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsTrue(false, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultExpectationViolationMessage);
      }

      [Fact]
      public void IsTrue_WhenGivenTrue_NoExceptionIsThrown()
      {
         Argument.IsTrue(true);
         Argument.IsTrue(true, "param");
         Argument.IsTrue(true, "param", "message");
      }

      #endregion Boolean and Null checks

      #region collection operations

      /// <summary>
      /// Validate that no exceptions are thrown when the target item exists in the collectionwhen
      /// Contains is called.
      /// </summary>
      [Fact]
      public void Contains_WhenGivenPopulatedCollectionAndTargetIsFound_NoExceptionIsThrown()
      {
         Argument.Contains(populatedIntCollection, valueInList2, "none", "this shouldn't be an error!");
      }

      /// <summary>
      /// Validate the an argument exception is thrown when a populated collection doesn't contain
      /// the specified item. And validate that we get the expected messaging.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      private void Contains_WhenGivenPopulatedCollectionAndTargetIsNotFound_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.Contains(populatedIntCollection, valueNotInList, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultNotFoundInCollectionMessage);
      }

      /// <summary>
      /// Validate the behavior of Contains with a null collection, and that the error messaging is correct.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      private void Contains_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.Contains(nullObjectCollection, nonNullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      /// <summary>
      /// Validate that an argument exception is thrown when
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      private void Contains_WhenGivenEmptyCollection_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.Contains(emptyObjectCollection, nonNullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "not found in");
      }

      /// <summary>
      /// Validate no exceptions are thrown when the item does not exist in the collection.
      /// </summary>
      [Fact]
      public void DoesNotContain_WhenGivenPopulatedCollectionAndTargetIsNotFound_NoExceptionIsThrown()
      {
         Argument.DoesNotContain(populatedIntCollection, valueNotInList, "none", "this shouldn't be an error!");
      }

      /// <summary>
      /// Validate that DoesNotContain throws an ArgumentNullException when passed a null exception.
      /// Also validate the expected param name and message are correct.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void DoesNotContain_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.DoesNotContain(nullObjectCollection, nonNullObject, paramName,
                  message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      /// <summary>
      /// Validate that DoesNotContain throws an no exceptions when given an empty collection. Also
      /// validate the expected wording and parameter name in the exception.
      /// </summary>
      [Fact]
      public void DoesNotContain_WhenGivenEmptyCollection_ThrowsNoException()
      {
         Argument.DoesNotContain(emptyObjectCollection, nonNullObject, "none", "this should never error.");
      }

      /// <summary>
      /// Validate that DoesNotContain throws an ArgumentException when the item is found in the list.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void DoesNotContain_WhenGivenItemIsFound_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.DoesNotContain(populatedIntCollection, valueInList3, paramName,
                  message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "expected to not be in");
      }

      /// <summary>
      /// Validate that an exception is not thrown when there are items in the list and HasItems is called.
      /// </summary>
      [Fact]
      public void HasItems_WhenGivenPopulatedCollection_NoExceptionIsThrown()
      {
         Argument.HasItems(populatedIntCollection, "none", "this shouldn't be an error!");
      }

      /// <summary>
      /// Validate that HasItems throws an ArgumentException when given a null collection. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void HasItems_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.HasItems(nullObjectCollection, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      /// <summary>
      /// Validate that HasItems throws an ArgumentException when given an empty collection. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void HasItems_WhenGivenEmptyCollection_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.HasItems(emptyObjectCollection, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it was empty");
      }

      /// <summary>
      /// Validate that no exceptions are thrown when IsEmpty is called on an empty collection.
      /// </summary>
      [Fact]
      public void IsEmpty_WhenGivenEmptyCollection_NoExceptionIsThrown()
      {
         Argument.IsEmpty(emptyObjectCollection, "none", "this shouldn't be an error!");
      }

      /// <summary>
      /// Validate that IsEmpty throws an ArgumentNullException when given a null. And validate that
      /// the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsEmpty(nullObjectCollection, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      /// <summary>
      /// Validate that IsEmpty throws an ArgumentException when given a populated collection. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenPopulatedCollection_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsEmpty(populatedIntCollection, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it contained values");
      }

      #endregion collection operations

      #region string operations

      /// <summary>
      /// Validate that IsNotEmpty does not throw any exception when given a string of length 1 or more.
      /// </summary>
      [Theory]
      [InlineData(nonWhitespaceString)]
      [InlineData(allWhitespaceString)]
      [InlineData(someWhitespaceString)]
      public void IsNotEmpty_WhenPopulatedString_NoExceptionIsThrown(string data)
      {
         Argument.IsNotEmpty(data, nameof(data), $"{nameof(Argument.IsNotEmpty)} should not throw an error for \"{data}\"");
      }

      /// <summary>
      /// Validate that IsNotEmpty throws an ArgumentNullException when given a null string. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Fact]
      public void IsNotEmpty_WhenGivenNullString_ThrowsNoException()
      {
         Argument.IsNotEmpty(nullString, "none", "This shouldn't fail.");
      }

      /// <summary>
      /// Validate that IsNotEmpty throws an ArgumentException when given an empty string. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotEmpty_WhenGivenEmptyString_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsNotEmpty(string.Empty, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it was empty");
      }

      /// <summary>
      /// Validate that IsEmpty does not throw any exception when given an empty string.
      /// </summary>
      [Fact]
      public void IsEmpty_WhenGivenEmptyString_NoExceptionIsThrown()
      {
         Argument.IsEmpty(string.Empty);
      }

      /// <summary>
      /// Validate that IsEmpty throws an ArgumentNullException when given null. And validate that the
      /// paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenNullString_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsEmpty(nullString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
      }

      /// <summary>
      /// Validate that IsEmpty throws an ArgumentException when given a non-empty string. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenPopulatedString_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsEmpty(nonWhitespaceString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it contains text");
      }

      /// <summary>
      /// Validate that IsNotNullOrEmpty throws no exception when given a non-null and non-empty string.
      /// </summary>
      [Theory]
      [InlineData(nonWhitespaceString)]
      [InlineData(someWhitespaceString)]
      [InlineData(allWhitespaceString)]
      public void IsNotNullOrEmpty_WhenGivenNonNullAndNonEmptyString_ThrowsNoException(string data)
      {
         Argument.IsNotNullOrEmpty(data, nameof(data), "this should never fail!");
      }

      /// <summary>
      /// Validate that IsNotNullOrEmpty throws an ArgumentException when given null. And validate
      /// that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullOrEmpty_WhenGivenNull_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrEmpty(nullString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "expected non-null");
      }

      /// <summary>
      /// Validate that IsNotNullOrEmpty throws an ArgumentException when given an empty string. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullOrEmpty_WhenGivenEmptyString_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrEmpty(emptyString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "to be non-null and non-empty");
      }

      /// <summary>
      /// Validate that IsNotNullOrWhitespace throws no exception when given nonwhitespace.
      /// </summary>
      [Theory]
      [InlineData(nonWhitespaceString)]
      [InlineData(someWhitespaceString)]
      [InlineData(emptyString)]
      public void IsNotNullOrWhitespace_WhenGivenNonWhitespace_ThrowsNoException(string data)
      {
         Argument.IsNotNullOrWhitespace(data, "none", "this should never fail.");
      }

      /// <summary>
      /// Validate that IsNotNullOrWhitespace throws an ArgumentException when given null or whitespace.
      /// And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullOrWhitespace_WhenGivenNullOrWhitespace_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullOrWhitespace(nullString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
         var ex2 = Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrWhitespace(allWhitespaceString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, new[] {"null", "whitespace" });
      }

      /// <summary>
      /// Validate that IsNotNullWhitespaceOrEmpty throws no exception when given nonwhitespace, non-empty, non-null strings.
      /// </summary>
      [Theory]
      [InlineData(nonWhitespaceString)]
      [InlineData(someWhitespaceString)]
      public void IsNotNullWhitespaceOrEmpty_WhenGivenNonWhitespace_ThrowsNoException(string data)
      {
         Argument.IsNotNullWhitespaceOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      /// Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      /// And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullWhitespaceOrEmpty_WhenGivenEmptyNullOrWhitespace_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNullWhitespaceOrEmpty(nullString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, defaultArgumentNullExceptionMessage);
         var ex2 = Assert.Throws<ArgumentException>(() => Argument.IsNotNullWhitespaceOrEmpty(allWhitespaceString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, new[] { "null", "whitespace", "empty" });
         ex2 = Assert.Throws<ArgumentException>(() => Argument.IsNotNullWhitespaceOrEmpty(string.Empty, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, new[] { "null", "whitespace", "empty" });
      }

      /// <summary>
      /// Validate that IsNotWhitespace throws no exception when given nonwhitespace, non-empty, non-null strings.
      /// </summary>
      [Theory]
      [InlineData(nonWhitespaceString)]
      [InlineData(someWhitespaceString)]
      [InlineData(nullString)]
      [InlineData(emptyString)]
      public void IsNotWhitespace_WhenGivenNonWhitespaceNullOrEmpty_ThrowsNoException(string data)
      {
         Argument.IsNotWhitespace(data, "none", "this should never fail.");
      }


      /// <summary>
      /// Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      /// And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotWhitespace_WhenGivenWhitespace_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsNotWhitespace(allWhitespaceString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "all whitespace");
      }

      /// <summary>
      /// Validate that IsNotWhitespaceOrEmpty throws no exception when given nonwhitespace, but either empty or null strings.
      /// </summary>
      [Theory]
      [InlineData(nonWhitespaceString)]
      [InlineData(someWhitespaceString)]
      [InlineData(nullString)]
      public void IsNotWhitespaceOrEmpty_WhenGivenNonWhitespaceOrNull_ThrowsNoException(string data)
      {
         Argument.IsNotWhitespaceOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      /// Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      /// And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotWhitespaceOrEmpty_WhenGivenEmptyWhitespace_ThrowsArgumentException(string paramName, string message)
      {
         var ex2 = Assert.Throws<ArgumentException>(() => Argument.IsNotWhitespaceOrEmpty(allWhitespaceString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, "be non-empty and non-whitespace");
         ex2 = Assert.Throws<ArgumentException>(() => Argument.IsNotWhitespaceOrEmpty(string.Empty, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, "be non-empty and non-whitespace");
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

      /// <summary>
      /// Validate that AreEqual throws no exceptions when the values are equal.
      /// </summary>
      [Fact]
      public void AreEqual_WhenTheValuesAreEqual_ThrowsNoExceptions()
      {
         var ih1 = new IntHolder(1);
         var ih2 = new IntHolder(1);
         Argument.AreEqual(1, 1, "none", "this shouldn't ever fail");
         Argument.AreEqual(ih1, ih2, "none", "this shouldn't ever fail");
         Argument.AreEqual(ih1, ih1, "none", "this shouldn't ever fail");
         Argument.AreEqual<IntHolder>(null, null, "none", "this shouldn't ever fail");
      }

      /// <summary>
      /// Validate that AreEqual throws an argument exception when the values are not equal. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreEqual_ValueTypes_WhenTheValuesAreNotEqual_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.AreEqual(1, 2, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "is not equal to");
      }

      /// <summary>
      /// Validate that AreEqual throws an argument exception when the values are not equal. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreEqual_ReferenceTypes_WhenTheValuesAreNotEqual_ThrowsArgumentException(string paramName, string message)
      {
         var ih1 = new IntHolder(1);
         var ih2 = new IntHolder(2);
         var ex = Assert.Throws<ArgumentException>(() => Argument.AreEqual(ih1, ih2, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "is not equal to");
      }

      /// <summary>
      /// Validate that AreEqual throws an argument exception when the values are not equal. And
      /// validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreEqual_ReferenceTypes_WhenOneValuesIsNull_ThrowsArgumentException(string paramName, string message)
      {
         var ih1 = new IntHolder(1);
         var ih2 = new IntHolder(2);
         var ex = Assert.Throws<ArgumentException>(() => Argument.AreEqual(ih1, null, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "is not equal to");
         // now test the other side.
         ex = Assert.Throws<ArgumentException>(() => Argument.AreEqual(null, ih2, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "is not equal to");
      }

      /// <summary>
      /// Validate that AreSameObject throws no exception when both values are null.
      /// </summary>
      [Fact]
      public void AreSameObject_WhenBothValuesAreNull_ThrowsNoException()
      {
         Argument.AreSameObject(null, null, "none", "this should not fail.");
      }

      /// <summary>
      /// Validate that AreSameObject throws no exception when both values are the same object.
      /// </summary>
      [Fact]
      public void AreSameObject_WhenBothValuesAreTheSameObject_ThrowsNoException()
      {
         Argument.AreSameObject(nonNullObject, nonNullObject, "none", "this should not fail.");
      }

      /// <summary>
      /// Validate that AreSameObject throws an ArgumentException when objects are different.
      /// And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreSameObject_WhenObjectsAreDifferent_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.AreSameObject(nullObject, nonNullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "was not the expected instance");
         ex = Assert.Throws<ArgumentException>(() => Argument.AreSameObject(nonNullObject, nullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "was not the expected instance");
         ex = Assert.Throws<ArgumentException>(() => Argument.AreSameObject(nonNullObject, nonNullObject2, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "was not the expected instance");
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

      private static void ValidateArgumentExceptionMessageAndParam(ArgumentException ex, string paramName, string message,
            string expectedDefaultMessage)
      {
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, new[] { expectedDefaultMessage });
      }

      private static void ValidateArgumentExceptionMessageAndParam(ArgumentException ex, string paramName, string message,
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
            Assert.Contains(message, ex.Message);
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