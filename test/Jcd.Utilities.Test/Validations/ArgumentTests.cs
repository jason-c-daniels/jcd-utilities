using System;
using System.Collections.Generic;
using Jcd.Utilities.Test.TestHelpers;
using Jcd.Utilities.Validations;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
   public class ArgumentTests
   {
      #region Private Fields
      private const string DefaultArgumentExceptionMessage = "contains an invalid value";
      private const string DefaultArgumentNullExceptionMessage = "expected non-null";
      private const string DefaultArgumentOutOfRangeMessage = "Expected value within range";
      private const string DefaultNotFoundInCollectionMessage = "not found in";
      private static readonly string[] DefaultExpectationViolationMessage = {"Expect", "to be", "it was"};
      private static readonly object NullObject = null;
      private static readonly object NonNullObject = new object();
      private static readonly object NonNullObject2 = new object();
      private static readonly object[] EmptyObjectCollection = { };
      private static readonly object[] NullObjectCollection = null;
      private static readonly List<int> PopulatedIntCollection = new List<int>(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
      private const int ValueNotInList = 400;
      private const int ValueInList2 = 4;
      private const int ValueInList3 = 9;

      private static readonly string defaultArgumentExceptionMessage = "contains an invalid value";
      private static readonly string defaultArgumentNullExceptionMessage = "expected non-null";
      private static readonly string defaultArgumentOutOfRangeMessage = "Expected value within range";
      private static readonly string defaultNotFoundInCollectionMessage = "not found in";
      private static readonly string[] defaultExpectationViolationMessage = { "Expect", "to be", "it was" };
      private static readonly object nullObject = null;
      private static readonly object nonNullObject = new object();
      private static readonly object nonNullObject2 = new object();
      private static readonly object[] emptyObjectCollection = { };
      private static readonly object[] nullObjectCollection = null;
      private static readonly List<int> populatedIntCollection = new List<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
      private const int valueNotInList = 400;
      private const int valueInList1 = 1;
      private const int valueInList2 = 4;

      private const string NullString = null;
      private const string EmptyString = "";
      private const string AllWhitespaceString = "    \r\n\t";
      private const string SomeWhitespaceString = "    abc \r d \n e \t";
      private const string NonWhitespaceString = "abcdefghijklmnop";

      private static readonly IntHolder Intholder1 = new IntHolder(1);
      private static readonly IntHolder Intholder5 = new IntHolder(5);
      private static readonly IntHolder Intholder9 = new IntHolder(9);
      #endregion Private Fields

      #region exception helpers

      /// <summary>
      ///    Validate that RaiseArgumentException raises an ArgumentException with the provided param and message, or defaults
      ///    if none provided.
      /// </summary>
      /// <param name="paramName">the name of the parameter at fault.</param>
      /// <param name="message">the message for the exception.</param>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void RaiseArgumentException_WhenCalled_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.RaiseArgumentException(paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentExceptionMessage);
      }

      /// <summary>
      ///    Validate that RaiseArgumentNullException raises an ArgumentNullException with the provided param and message, or
      ///    defaults if none provided.
      /// </summary>
      /// <param name="paramName">the name of the parameter at fault.</param>
      /// <param name="message">the message for the exception.</param>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void RaiseArgumentNullException_WhenCalled_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.RaiseArgumentNullException(paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that RaiseArgumentOutOfRangeException raises an ArgumentOutOfRangeException with the provided param and
      ///    message, or defaults if none provided.
      /// </summary>
      /// <param name="min">the minimum value for the range</param>
      /// <param name="max">the maximum value for the range</param>
      /// <param name="actual">The actual value encountered</param>
      /// <param name="paramName">the name of the parameter at fault.</param>
      /// <param name="message">the message for the exception.</param>
      [Theory]
      [InlineData(1, 2, 5, "param", null)]
      [InlineData(1, 2, 5, "param", "message")]
      [InlineData(1, 2, 5, null, "message")]
      [InlineData(1, 2, 5, "", "message")]
      [InlineData(1, 2, 5, " ", "message")]
      public void RaiseArgumentOutOfRangeException_WhenCalled_ThrowsArgumentOutOfRangeException(int actual,
                                                                                                int min,
                                                                                                int max,
                                                                                                string paramName,
                                                                                                string message)
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.RaiseArgumentOutOfRangeException(actual,
                                                                                                             min,
                                                                                                             max,
                                                                                                             paramName,
                                                                                                             message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentOutOfRangeMessage);
      }

      /// <summary>
      ///    Validates that when RaiseExpectationViolation is called it raises an ArgumentException with specific verbiage, or a
      ///    custom message (if provided)
      /// </summary>
      /// <param name="expected">The expected value</param>
      /// <param name="actual">The actual value</param>
      /// <param name="paramName">The parameter name</param>
      /// <param name="message">the message</param>
      [Theory]
      [InlineData(0, 1, null, null)]
      [InlineData(0, 1, null, "message")]
      [InlineData(0, 1, "param", "message")]
      [InlineData(0, 1, "param", null)]
      [InlineData(0, 1, "", null)]
      [InlineData(0, 1, "", "message")]
      [InlineData(0, 1, " ", null)]
      [InlineData(0, 1, " ", "message")]
      public void RaiseExpectationViolation_WhenCalled_ThrowsArgumentExceptionWithSpecialMessage(int expected,
                                                                                                 int actual,
                                                                                                 string paramName,
                                                                                                 string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.RaiseExpectationViolation(expected,
                                                                                         actual,
                                                                                         paramName,
                                                                                         message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultExpectationViolationMessage);
      }

      /// <summary>
      ///    Validate that IsFalse does throw an exception when it receives a value of true. Validate the verbiage and custom
      ///    messaging as well.
      /// </summary>
      /// <param name="message">The custom message</param>
      /// <param name="paramName">The parameter</param>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsFalse_WhenGivenTrue_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsFalse(true, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultExpectationViolationMessage);
      }

      /// <summary>
      ///    Validate that IsNotNull throws an exception when it receives a value of null. Validate the verbiage and custom
      ///    messaging as well.
      /// </summary>
      /// <param name="message">The custom message</param>
      /// <param name="paramName">The parameter</param>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNull_WhenGivenNull_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsNotNull(NullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that IsNull throws an exception when it recieves a value of non-null. Validate the verbiage and custom
      ///    messaging as well.
      /// </summary>
      /// <param name="message">The custom message</param>
      /// <param name="paramName">The parameter</param>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNull_WhenGivenNonNull_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsNull(NonNullObject, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultExpectationViolationMessage);
      }

      /// <summary>
      ///    Validate that IsTrue throws an exception when it receives a value of false. Validate the verbiage and custom
      ///    messaging as well.
      /// </summary>
      /// <param name="message">The custom message</param>
      /// <param name="paramName">The parameter</param>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsTrue_WhenGivenFalse_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsTrue(false, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultExpectationViolationMessage);
      }

      /// <summary>
      ///    Validate the an argument exception is thrown when a populated collection doesn't contain
      ///    the specified item. And validate that we get the expected messaging.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      private void Contains_WhenGivenPopulatedCollectionAndTargetIsNotFound_ThrowsArgumentException(string paramName,
                                                                                                    string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.Contains(PopulatedIntCollection,
                                                                        ValueNotInList,
                                                                        paramName,
                                                                        message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultNotFoundInCollectionMessage);
      }

      /// <summary>
      ///    Validate the behavior of Contains with a null collection, and that the error messaging is correct.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      private void Contains_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.Contains(NullObjectCollection,
                                                                            NonNullObject,
                                                                            paramName,
                                                                            message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that an argument exception is thrown when
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      private void Contains_WhenGivenEmptyCollection_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.Contains(EmptyObjectCollection,
                                                                        NonNullObject,
                                                                        paramName,
                                                                        message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "not found in");
      }

      /// <summary>
      ///    Validate that DoesNotContain throws an ArgumentNullException when passed a null exception.
      ///    Also validate the expected param name and message are correct.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void DoesNotContain_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.DoesNotContain(NullObjectCollection,
                                                                                     NonNullObject,
                                                                                     paramName,
                                                                                     message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that DoesNotContain throws an ArgumentException when the item is found in the list.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void DoesNotContain_WhenGivenItemIsFound_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.DoesNotContain(PopulatedIntCollection,
                                                                                 ValueInList3,
                                                                                 paramName,
                                                                                 message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "expected to not be in");
      }

      /// <summary>
      ///    Validate that HasItems throws an ArgumentException when given a null collection. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void HasItems_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.HasItems(NullObjectCollection, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that HasItems throws an ArgumentException when given an empty collection. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void HasItems_WhenGivenEmptyCollection_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(
                                                   () => Argument.HasItems(EmptyObjectCollection, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it was empty");
      }

      /// <summary>
      ///    Validate that IsEmpty throws an ArgumentNullException when given a null. And validate that
      ///    the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenNullCollection_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.IsEmpty(NullObjectCollection, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that IsEmpty throws an ArgumentException when given a populated collection. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenPopulatedCollection_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(
                                                   () => Argument.IsEmpty(PopulatedIntCollection, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it contained values");
      }

      /// <summary>
      ///    Validate that IsNotEmpty does not throw any exception when given a string of length 1 or more.
      /// </summary>
      [Theory]
      [InlineData(NonWhitespaceString)]
      [InlineData(AllWhitespaceString)]
      [InlineData(SomeWhitespaceString)]
      public void IsNotEmpty_WhenPopulatedString_NoExceptionIsThrown(string data)
      {
         Argument.IsNotEmpty(data,
                             nameof(data),
                             $"{nameof(Argument.IsNotEmpty)} should not throw an error for \"{data}\"");
      }

      /// <summary>
      ///    Validate that IsNotEmpty throws an ArgumentException when given an empty string. And
      ///    validate that the paramName and message are set correctly on the exception.
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
      ///    Validate that IsEmpty throws an ArgumentNullException when given null. And validate that the
      ///    paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenNullString_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsEmpty(NullString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);
      }

      /// <summary>
      ///    Validate that IsEmpty throws an ArgumentException when given a non-empty string. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsEmpty_WhenGivenPopulatedString_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsEmpty(NonWhitespaceString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "but it contains text");
      }

      /// <summary>
      ///    Validate that IsNotNullOrEmpty throws no exception when given a non-null and non-empty string.
      /// </summary>
      [Theory]
      [InlineData(NonWhitespaceString)]
      [InlineData(SomeWhitespaceString)]
      [InlineData(AllWhitespaceString)]
      public void IsNotNullOrEmpty_WhenGivenNonNullAndNonEmptyString_ThrowsNoException(string data)
      {
         Argument.IsNotNullOrEmpty(data, nameof(data), "this should never fail!");
      }

      /// <summary>
      ///    Validate that IsNotNullOrEmpty throws an ArgumentException when given null. And validate
      ///    that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullOrEmpty_WhenGivenNull_ThrowsArgumentNullException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.IsNotNullOrEmpty(NullString, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "expected non-null");
      }

      /// <summary>
      ///    Validate that IsNotNullOrEmpty throws an ArgumentException when given an empty string. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullOrEmpty_WhenGivenEmptyString_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsNotNullOrEmpty(EmptyString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "to be non-null and non-empty");
      }

      /// <summary>
      ///    Validate that IsNotNullOrWhitespace throws no exception when given non-whitespace.
      /// </summary>
      [Theory]
      [InlineData(NonWhitespaceString)]
      [InlineData(SomeWhitespaceString)]
      [InlineData(EmptyString)]
      public void IsNotNullOrWhitespace_WhenGivenNonWhitespace_ThrowsNoException(string data)
      {
         Argument.IsNotNullOrWhitespace(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNotNullOrWhitespace throws an ArgumentException when given null or whitespace.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullOrWhitespace_WhenGivenNullOrWhitespace_ThrowsArgumentException(string paramName,
                                                                                          string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.IsNotNullOrWhitespace(NullString,
                                                                                         paramName,
                                                                                         message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);

         var ex2 = Assert.Throws<ArgumentException>(() =>
                                                       Argument.IsNotNullOrWhitespace(AllWhitespaceString,
                                                                                      paramName,
                                                                                      message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, new[] {"null", "whitespace"});
      }

      /// <summary>
      ///    Validate that IsNotNullWhitespaceOrEmpty throws no exception when given non-whitespace, non-empty, non-null strings.
      /// </summary>
      [Theory]
      [InlineData(NonWhitespaceString)]
      [InlineData(SomeWhitespaceString)]
      public void IsNotNullWhitespaceOrEmpty_WhenGivenNonWhitespace_ThrowsNoException(string data)
      {
         Argument.IsNotNullWhitespaceOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotNullWhitespaceOrEmpty_WhenGivenEmptyNullOrWhitespace_ThrowsArgumentException(string paramName,
                                                                                                    string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.IsNotNullWhitespaceOrEmpty(NullString,
                                                                                              paramName,
                                                                                              message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);

         var ex2 = Assert.Throws<ArgumentException>(() =>
                                                       Argument.IsNotNullWhitespaceOrEmpty(AllWhitespaceString,
                                                                                           paramName,
                                                                                           message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, new[] {"null", "whitespace", "empty"});

         ex2 = Assert.Throws<ArgumentException>(() =>
                                                   Argument.IsNotNullWhitespaceOrEmpty(string.Empty,
                                                                                       paramName,
                                                                                       message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, new[] {"null", "whitespace", "empty"});
      }

      /// <summary>
      ///    Validate that IsNotWhitespace throws no exception when given non-whitespace, non-empty, non-null strings.
      /// </summary>
      [Theory]
      [InlineData(NonWhitespaceString)]
      [InlineData(SomeWhitespaceString)]
      [InlineData(NullString)]
      [InlineData(EmptyString)]
      public void IsNotWhitespace_WhenGivenNonWhitespaceNullOrEmpty_ThrowsNoException(string data)
      {
         Argument.IsNotWhitespace(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotWhitespace_WhenGivenWhitespace_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.IsNotWhitespace(AllWhitespaceString,
                                                                               paramName,
                                                                               message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "all whitespace");
      }

      /// <summary>
      ///    Validate that IsNotWhitespaceOrEmpty throws no exception when given non-whitespace, but either empty or null
      ///    strings.
      /// </summary>
      [Theory]
      [InlineData(NonWhitespaceString)]
      [InlineData(SomeWhitespaceString)]
      [InlineData(NullString)]
      public void IsNotWhitespaceOrEmpty_WhenGivenNonWhitespaceOrNull_ThrowsNoException(string data)
      {
         Argument.IsNotWhitespaceOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNotWhitespaceOrEmpty_WhenGivenEmptyWhitespace_ThrowsArgumentException(string paramName,
                                                                                          string message)
      {
         var ex2 = Assert.Throws<ArgumentException>(() =>
                                                       Argument.IsNotWhitespaceOrEmpty(AllWhitespaceString,
                                                                                       paramName,
                                                                                       message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, "be non-empty and non-whitespace");

         ex2 = Assert.Throws<ArgumentException>(() =>
                                                   Argument.IsNotWhitespaceOrEmpty(string.Empty, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, "be non-empty and non-whitespace");
      }

      /// <summary>
      ///    Validate that IsNotNullOrWhitespace throws an ArgumentException when given empty, null or whitespace.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsWhitespace_WhenGivenNonWhitespace_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Argument.IsWhitespace(NullString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);

         var ex2 = Assert.Throws<ArgumentException>(() =>
                                                       Argument.IsWhitespace(SomeWhitespaceString, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, "whitespace");
      }

      /// <summary>
      ///    Validate that IsNullOrEmpty throws no exception when given empty, or null strings.
      /// </summary>
      [Theory]
      [InlineData(EmptyString)]
      [InlineData(NullString)]
      public void IsNullOrEmpty_WhenGivenNullOrEmpty_ThrowsNoException(string data)
      {
         Argument.IsNullOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNullOrEmpty throws an ArgumentException when given whitespace and non-whitespace strings.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNullOrEmpty_WhenGivenNonWhitespaceOrWhitespace_ThrowsArgumentException(string paramName,
                                                                                           string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.IsNullOrEmpty(SomeWhitespaceString, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "null or empty");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.IsNullOrEmpty(AllWhitespaceString, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "null or empty");
      }

      /// <summary>
      ///    Validate that IsNullOrWhitespace throws no exception when given whitespace, empty, or null strings.
      /// </summary>
      [Theory]
      [InlineData(AllWhitespaceString)]
      [InlineData(NullString)]
      public void IsNullOrWhitespace_WhenGivenWhitespaceNullOrEmpty_ThrowsNoException(string data)
      {
         Argument.IsNullOrWhitespace(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNullOrWhitespace throws an ArgumentException when given non-null, non-whitespace strings.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNullOrWhitespace_WhenGivenNonEmptyNonWhitespace_ThrowsArgumentException(string paramName,
                                                                                            string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.IsNullOrWhitespace(SomeWhitespaceString,
                                                                                  paramName,
                                                                                  message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "null or whitespace");
         ex = Assert.Throws<ArgumentException>(() => Argument.IsNullOrWhitespace(EmptyString, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "null or whitespace");
      }

      /// <summary>
      ///    Validate that IsNullWhitespaceOrEmpty throws no exception when given whitespace, empty, or null strings.
      /// </summary>
      [Theory]
      [InlineData(AllWhitespaceString)]
      [InlineData(NullString)]
      [InlineData(EmptyString)]
      public void IsNullWhitespaceOrEmpty_WhenGivenWhitespaceNullOrEmpty_ThrowsNoException(string data)
      {
         Argument.IsNullWhitespaceOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsNullOrWhitespace throws an ArgumentException when given non-empty, non-null, non-whitespace
      ///    strings.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsNullWhitespaceOrEmpty_WhenGivenEmptyNullOrWhitespace_ThrowsArgumentException(string paramName,
                                                                                                 string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.IsNullWhitespaceOrEmpty(SomeWhitespaceString,
                                                                                       paramName,
                                                                                       message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, new[] {"null", "whitespace", "empty"});
      }

      /// <summary>
      ///    Validate that IsWhitespaceOrEmpty throws no exception when given whitespace or an empty string.
      /// </summary>
      [Theory]
      [InlineData(AllWhitespaceString)]
      [InlineData(EmptyString)]
      public void IsWhitespaceOrEmpty_WhenGivenWhitespaceOrEmptyString_ThrowsNoException(string data)
      {
         Argument.IsWhitespaceOrEmpty(data, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that IsWhitespaceOrEmpty throws an ArgumentException when given non-empty, non-null, non-whitespace input.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsWhitespaceOrEmpty_WhenGivenEmptyWhitespace_ThrowsArgumentException(string paramName,
                                                                                       string message)
      {
         var ex = Assert.Throws<ArgumentNullException>(() =>
                                                          Argument.IsWhitespaceOrEmpty(NullString, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultArgumentNullExceptionMessage);

         var ex2 = Assert.Throws<ArgumentException>(() =>
                                                       Argument.IsWhitespaceOrEmpty(SomeWhitespaceString,
                                                                                    paramName,
                                                                                    message));

         ValidateArgumentExceptionMessageAndParam(ex2, paramName, message, "be whitespace or empty");
      }

      /// <summary>
      ///    Validate that Contains (string) Throws ArgumentException When a character not found in the search string.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void Contains_StringChar_WhenCharacterNotFound_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.Contains("string", 'c', paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultNotFoundInCollectionMessage);
      }

      /// <summary>
      ///    Validate that Contains (string) Throws ArgumentException when a substring is not found in the search string.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void Contains_Substring_WhenCharacterNotFound_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.Contains("string", "c", paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, DefaultNotFoundInCollectionMessage);
      }

      /// <summary>
      ///    Validate that AreEqual throws an argument exception when the values are not equal. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreEqual_ValueTypes_WhenTheValuesAreNotEqual_ThrowsArgumentException(string paramName,
                                                                                       string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.AreEqual(1, 2, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "is not equal to");
      }

      /// <summary>
      ///    Validate that AreEqual throws an argument exception when the values are not equal. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreEqual_ReferenceTypes_WhenTheValuesAreNotEqual_ThrowsArgumentException(string paramName,
                                                                                           string message)
      {
         var ih1 = new IntHolder(1);
         var ih2 = new IntHolder(2);
         var ex = Assert.Throws<ArgumentException>(() => Argument.AreEqual(ih1, ih2, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "is not equal to");
      }

      /// <summary>
      ///    Validate that AreEqual throws an argument exception when the values are not equal. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreEqual_ReferenceTypes_WhenOneValuesIsNull_ThrowsArgumentException(string paramName,
                                                                                      string message)
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
      ///    Validate that AreSameObject throws an ArgumentException when objects are different.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void AreSameObject_WhenObjectsAreDifferent_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.AreSameObject(NullObject,
                                                                             NonNullObject,
                                                                             paramName,
                                                                             message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "was not the expected instance");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.AreSameObject(NonNullObject,
                                                                         NullObject,
                                                                         paramName,
                                                                         message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "was not the expected instance");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.AreSameObject(NonNullObject,
                                                                         NonNullObject2,
                                                                         paramName,
                                                                         message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "was not the expected instance");
      }

      /// <summary>
      ///    Validate that InRange throws no exception when value is between min and max.
      /// </summary>
      [Theory]
      [InlineData(1, 1, 5)]
      [InlineData(3, 1, 5)]
      [InlineData(5, 1, 5)]
      public void InRange_WhenValueIsBetweenMinAndMax_ThrowsNoException(int value, int min, int max)
      {
         Argument.InRange(value, min, max, "none", "this should never fail!");
      }

      /// <summary>
      ///    Validate that InRange throws an ArgumentOutOfRangeException when value is outside of range.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void InRangeReferenceType_WhenValueIsOutsideOfRange_ThrowsArgumentOutOfRangeException(string paramName,
                                                                                                   string message)
      {
         var value1 = new IntHolder(0);
         var value2 = new IntHolder(6);
         var min = new IntHolder(1);
         var max = new IntHolder(5);

         var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                                Argument.InRange(value1, min, max, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value within range");

         ex = Assert.Throws<ArgumentOutOfRangeException>(
                                                         () => Argument.InRange(value2, min, max, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value within range");
      }

      /// <summary>
      ///    Validate that InRange throws an ArgumentOutOfRangeException When ValueIsOutsideOfRange.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void InRange_WhenValueIsOutsideOfRange_ThrowsArgumentOutOfRangeException(string paramName,
                                                                                      string message)
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.InRange(1, 2, 5, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value within range");
         ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.InRange(6, 2, 5, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value within range");
      }

      /// <summary>
      ///    Validate that NotInRange throws no exception when value is not between min and max.
      /// </summary>
      [Theory]
      [InlineData(0, 1, 5)]
      [InlineData(8, 1, 5)]
      public void NotInRange_WhenValueIsNotInRange_ThrowsNoException(int value, int min, int max)
      {
         Argument.NotInRange(value, min, max, "none", "this should never fail!");
      }

      /// <summary>
      ///    Validate that NotInRange throws an ArgumentOutOfRangeException when value is inside of range.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void NotInRangeReferenceType_WhenValueIsInsideOfRange_ThrowsArgumentOutOfRangeException(string paramName,
                                                                                                     string message)
      {
         var value1 = new IntHolder(1);
         var value2 = new IntHolder(5);
         var min = new IntHolder(1);
         var max = new IntHolder(5);

         var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                                Argument.NotInRange(value1,
                                                                                    min,
                                                                                    max,
                                                                                    paramName,
                                                                                    message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value outside of the range ");

         ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                                                            Argument.NotInRange(value2, min, max, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value outside of the range ");
      }

      /// <summary>
      ///    Validate that NotInRange throws an ArgumentOutOfRangeException when value is inside of range.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void NotInRange_WhenValueIsInsideOfRange_ThrowsArgumentOutOfRangeException(string paramName,
                                                                                        string message)
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.NotInRange(1, 1, 5, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value outside of the range ");
         ex = Assert.Throws<ArgumentOutOfRangeException>(() => Argument.NotInRange(5, 2, 5, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "Expected value outside of the range ");
      }

      /// <summary>
      ///    Validate that IsGreaterThan throws an ArgumentException when left is less than or equal to right.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsGreaterThan_WhenLeftIsLessThanOrEqualToRight_ThrowsArgumentException(string paramName,
                                                                                         string message)
      {
         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.IsGreaterThan(Intholder5,
                                                                             Intholder9,
                                                                             paramName,
                                                                             message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "greater than");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.IsGreaterThan(Intholder9, Intholder9, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "greater than");
      }

      /// <summary>
      ///    Validate that IsGreaterThanOrEqual throws an ArgumentException When ValueIsLessThanComparison.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsGreaterThanOrEqual_WhenValueIsLessThanComparison_ThrowsArgumentException(string paramName,
                                                                                             string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsGreaterThanOrEqual(1, 2, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex,
                                                  paramName,
                                                  message,
                                                  "was expected to be greater than or equal to");
      }

      /// <summary>
      ///    Validate that IsGreaterThanOrEqual throws an ArgumentException When ValueIsLessThanComparison.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsGreaterThanOrEqualReferenceType_WhenValueIsLessThanComparison_ThrowsArgumentException(
         string paramName,
         string message)
      {
         var value1 = new IntHolder(1);
         var value2 = new IntHolder(5);

         var ex = Assert.Throws<ArgumentException>(() =>
                                                      Argument.IsGreaterThanOrEqual(value1,
                                                                                    value2,
                                                                                    paramName,
                                                                                    message));

         ValidateArgumentExceptionMessageAndParam(ex,
                                                  paramName,
                                                  message,
                                                  "was expected to be greater than or equal to");
      }

      /// <summary>
      ///    Validate that IsLessThan throws an ArgumentException When ValueIsGreaterOrEqualToComparison.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsLessThan_WhenValueIsGreaterOrEqualToComparison_ThrowsArgumentException(string paramName,
                                                                                           string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsLessThan(9, 1, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "less than");
         ex = Assert.Throws<ArgumentException>(() => Argument.IsLessThan(1, 1, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "less than");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.IsLessThan(Intholder9, Intholder1, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "less than");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.IsLessThan(Intholder9, Intholder9, paramName, message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "less than");
      }

      /// <summary>
      ///    Validate that IsLessThanOrEqual throws an ArgumentException When ValueIsGreaterOrEqualToComparison.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void IsLessThanOrEqual_WhenValueIsGreaterThanToComparison_ThrowsArgumentException(string paramName,
                                                                                               string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.IsLessThanOrEqual(9, 1, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "less than or equal to");

         ex = Assert.Throws<ArgumentException>(() =>
                                                  Argument.IsLessThanOrEqual(Intholder9,
                                                                             Intholder1,
                                                                             paramName,
                                                                             message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "less than or equal to");
      }

      /// <summary>
      ///    Validate that Fails throws an ArgumentException when custom condition succeeds.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void Fails_WhenCustomConditionFails_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.Fails(Check.IsTrue, true, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "contains an invalid value");
      }

      /// <summary>
      ///    Validate that FailsAny throws an ArgumentException when all conditions succeed.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void FailsAny_WhenNoConditionsFail_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.FailsAny(
                                                                           new Check.Signature<bool>[]
                                                                           {
                                                                              Check.IsTrue,
                                                                              Check.IsTrue
                                                                           },
                                                                           true,
                                                                           paramName,
                                                                           message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "contains an invalid value");
      }

      /// <summary>
      ///    Validate that FailsAll throws an ArgumentException when one conditions succeeds.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void FailsAll_WhenOneConditionsSucceeds_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.FailsAll(
                                                                           new Check.Signature<bool>[]
                                                                           {
                                                                              Check.IsTrue,
                                                                              Check.IsFalse
                                                                           },
                                                                           true,
                                                                           paramName,
                                                                           message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "contains an invalid value");
      }

      /// <summary>
      ///    Validate that PassesAny throws an ArgumentException when no condition succeeds.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void PassesAny_WhenAllConditionsFail_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.PassesAny(
                                                                            new Check.Signature<bool>[]
                                                                            {
                                                                               Check
                                                                                 .IsFalse,
                                                                               Check.IsFalse
                                                                            },
                                                                            true,
                                                                            paramName,
                                                                            message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "contains an invalid value");
      }

      /// <summary>
      ///    Validate that PassesAll throws an ArgumentException when one condition fails.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void PassesAll_WhenOneConditionsFail_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.PassesAll(
                                                                            new Check.Signature<bool>[]
                                                                            {
                                                                               Check.IsTrue,
                                                                               Check.IsFalse
                                                                            },
                                                                            true,
                                                                            paramName,
                                                                            message));

         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "contains an invalid value");
      }

      /// <summary>
      ///    Validate that Passes throws an ArgumentException when custom condition fails.
      ///    And validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Theory]
      [InlineData("param", null)]
      [InlineData("param", "message")]
      [InlineData(null, "message")]
      [InlineData("", "message")]
      [InlineData(" ", "message")]
      public void Passes_WhenCustomConditionFails_ThrowsArgumentException(string paramName, string message)
      {
         var ex = Assert.Throws<ArgumentException>(() => Argument.Passes(Check.IsTrue, false, paramName, message));
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, "contains an invalid value");
      }

      /// <summary>
      ///    Validation helper that ensures a message contains the necessary text.
      /// </summary>
      /// <param name="ex">teh exception to validate</param>
      /// <param name="paramName">the parameter name</param>
      /// <param name="message">the custom message, if any.</param>
      /// <param name="expectedDefaultMessage">the default message</param>
      private static void ValidateArgumentExceptionMessageAndParam(ArgumentException ex,
                                                                   string paramName,
                                                                   string message,
                                                                   string expectedDefaultMessage)
      {
         ValidateArgumentExceptionMessageAndParam(ex, paramName, message, new[] {expectedDefaultMessage});
      }

      /// <summary>
      ///    Validation helper that ensures a message contains the necessary text.
      /// </summary>
      /// <param name="ex">teh exception to validate</param>
      /// <param name="paramName">the parameter name</param>
      /// <param name="message">the custom message, if any.</param>
      /// <param name="expectedDefaultMessageFragments">the default message fragments to look for</param>
      private static void ValidateArgumentExceptionMessageAndParam(ArgumentException ex,

                                                                   // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
                                                                   string paramName,
                                                                   string message,
                                                                   IEnumerable<string> expectedDefaultMessageFragments)
      {
         if (ex == null) throw new ArgumentNullException(nameof(ex));

         if (message == null)
         {
            foreach (var text in expectedDefaultMessageFragments)
            {
               Assert.Contains(text, ex.Message);
            }
         }
         else
         {
            Assert.Contains(message, ex.Message);
         }

         Assert.StartsWith(string.IsNullOrWhiteSpace(paramName) ? Argument.UnspecifiedParamName : paramName,
                           ex.ParamName);
      }

      /// <summary>
      ///    Validate that AreEqual throws no exceptions when the values are equal.
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
      ///    Validate that AreSameObject throws no exception when both values are null.
      /// </summary>
      [Fact]
      public void AreSameObject_WhenBothValuesAreNull_ThrowsNoException()
      {
         Argument.AreSameObject(null, null, "none", "this should not fail.");
      }

      /// <summary>
      ///    Validate that AreSameObject throws no exception when both values are the same object.
      /// </summary>
      [Fact]
      public void AreSameObject_WhenBothValuesAreTheSameObject_ThrowsNoException()
      {
         Argument.AreSameObject(NonNullObject, NonNullObject, "none", "this should not fail.");
      }

      /// <summary>
      ///    Validate that Contains does not throw any exception when the search string contains the target character.
      /// </summary>
      [Fact]
      public void Contains_StringChar_WhenSearchStringContainsTarget_DoesNotThrowAnySception()
      {
         Argument.Contains("abc", 'c');
      }

      /// <summary>
      ///    Validate that Contains does not throw any exception when the search string contains the target substring.
      /// </summary>
      [Fact]
      public void Contains_Substring_WhenSearchStringContainsTarget_DoesNotThrowAnyException()
      {
         Argument.Contains("abc", "c");
      }

      /// <summary>
      ///    Validate that no exceptions are thrown when the target item exists in the collectionwhen
      ///    Contains is called.
      /// </summary>
      [Fact]
      public void Contains_WhenGivenPopulatedCollectionAndTargetIsFound_NoExceptionIsThrown()
      {
         Argument.Contains(PopulatedIntCollection, ValueInList2, "none", "this shouldn't be an error!");
      }

      /// <summary>
      ///    Validate that DoesNotContain throws an no exceptions when given an empty collection. Also
      ///    validate the expected wording and parameter name in the exception.
      /// </summary>
      [Fact]
      public void DoesNotContain_WhenGivenEmptyCollection_ThrowsNoException()
      {
         Argument.DoesNotContain(EmptyObjectCollection, NonNullObject, "none", "this should never error.");
      }

      /// <summary>
      ///    Validate no exceptions are thrown when the item does not exist in the collection.
      /// </summary>
      [Fact]
      public void DoesNotContain_WhenGivenPopulatedCollectionAndTargetIsNotFound_NoExceptionIsThrown()
      {
         Argument.DoesNotContain(PopulatedIntCollection, ValueNotInList, "none", "this shouldn't be an error!");
      }

      /// <summary>
      ///    Validate that Fails throws no exception when custom condition fails.
      /// </summary>
      [Fact]
      public void Fails_WhenCustomConditionFails_ThrowsNoException()
      {
         Argument.Fails(Check.IsTrue, false, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that FailsAll does not throw an exception when all conditions fail.
      /// </summary>
      [Fact]
      public void FailsAll_WhenAllConditionsFail_ThrowsNoException()
      {
         Argument.FailsAll(new Check.Signature<bool>[] {Check.IsTrue, Check.IsTrue}, false);
      }

      /// <summary>
      ///    Validate that FailsAll does not throw an exception when all conditions fail.
      /// </summary>
      [Fact]
      public void FailsAny_WhenAnyConditionsFail_ThrowsNoException()
      {
         Argument.FailsAny(new Check.Signature<bool>[] {Check.IsTrue, Check.IsTrue}, false);
      }

      /// <summary>
      ///    Validate that an exception is not thrown when there are items in the list and HasItems is called.
      /// </summary>
      [Fact]
      public void HasItems_WhenGivenPopulatedCollection_NoExceptionIsThrown()
      {
         Argument.HasItems(PopulatedIntCollection, "none", "this shouldn't be an error!");
      }

      /// <summary>
      ///    Validate that InRange for reference type throws noexception when value is between min and max.
      /// </summary>
      [Fact]
      public void InRangeReferenceType_WhenValueIsBetweenMinAndMax_ThrowsNoException()
      {
         var value = new IntHolder(2);
         var min = new IntHolder(1);
         var max = new IntHolder(5);
         Argument.InRange(value, min, max, "none", "this should never fail!");
      }

      /// <summary>
      ///    Validate that no exceptions are thrown when IsEmpty is called on an empty collection.
      /// </summary>
      [Fact]
      public void IsEmpty_WhenGivenEmptyCollection_NoExceptionIsThrown()
      {
         Argument.IsEmpty(EmptyObjectCollection, "none", "this shouldn't be an error!");
      }

      /// <summary>
      ///    Validate that IsEmpty does not throw any exception when given an empty string.
      /// </summary>
      [Fact]
      public void IsEmpty_WhenGivenEmptyString_NoExceptionIsThrown() { Argument.IsEmpty(string.Empty); }

      /// <summary>
      ///    Validate that IsFalse does not throw an exception when it receives a value of false.
      /// </summary>
      [Fact]
      public void IsFalse_WhenGivenFalse_NoExceptionIsThrown()
      {
         Argument.IsFalse(false);
         Argument.IsFalse(false, "param");
         Argument.IsFalse(false, "param", "message");
      }

      /// <summary>
      ///    Validate that IsGreaterThan Throws NoException When LeftIsGreaterThanRight.
      /// </summary>
      [Fact]
      public void IsGreaterThan_WhenLeftIsGreaterThanRight_ThrowsNoException()
      {
         Argument.IsGreaterThan(Intholder9, Intholder5, "none", "This should never fail!");
      }

      /// <summary>
      ///    Validate that IsGreaterThanOrEqual throws no exception when value is greater than or equal to comparison.
      /// </summary>
      [Fact]
      public void IsGreaterThanOrEqual_WhenValueIsGreaterThanOrEqualToComparison_ThrowsNoException()
      {
         Argument.IsGreaterThanOrEqual(1, 1, "none", "this should never fail");
         Argument.IsGreaterThanOrEqual(2, 1, "none", "this should never fail");
      }

      /// <summary>
      ///    Validate that IsLessThan throws no exception when value is less than comparison.
      /// </summary>
      [Fact]
      public void IsLessThan_WhenValueIsLessThanComparison_ThrowsNoException()
      {
         Argument.IsLessThan(Intholder5, Intholder9, "none", "This should never fail.");
         Argument.IsLessThan(5, 9, "none", "This should never fail.");
      }

      /// <summary>
      ///    Validate that IsLessThan throws no exception when value is less than comparison.
      /// </summary>
      [Fact]
      public void IsLessThanOrEqual_WhenValueIsLessThanOrEqualToComparison_ThrowsNoException()
      {
         Argument.IsLessThanOrEqual(Intholder5, Intholder9, "none", "This should never fail.");
         Argument.IsLessThanOrEqual(5, 9, "none", "This should never fail.");
         Argument.IsLessThanOrEqual(Intholder5, Intholder5, "none", "This should never fail.");
         Argument.IsLessThanOrEqual(9, 9, "none", "This should never fail.");
      }

      /// <summary>
      ///    Validate that IsNotEmpty throws an ArgumentNullException when given a null string. And
      ///    validate that the paramName and message are set correctly on the exception.
      /// </summary>
      [Fact]
      public void IsNotEmpty_WhenGivenNullString_ThrowsNoException()
      {
         Argument.IsNotEmpty(NullString, "none", "This shouldn't fail.");
      }

      /// <summary>
      ///    Validate that IsNotNull does not throw an exception when it receives a non-null value.
      /// </summary>
      [Fact]
      public void IsNotNull_WhenGivenNonNull_NoExceptionIsThrown()
      {
         Argument.IsNotNull(NonNullObject, "none", "this error shouldn't have happened.");
      }

      /// <summary>
      ///    Validate that IsNull does not throw an exception when it receives a null value.
      /// </summary>
      [Fact]
      public void IsNull_WhenGivenNull_NoExceptionIsThrown()
      {
         Argument.IsNull(NullObject, "none", "this error shouldn't have happened.");
      }

      /// <summary>
      ///    Validate that IsTrue does not throw an exception when it receives true.
      /// </summary>
      [Fact]
      public void IsTrue_WhenGivenTrue_NoExceptionIsThrown()
      {
         Argument.IsTrue(true);
         Argument.IsTrue(true, "param");
         Argument.IsTrue(true, "param", "message");
      }

      /// <summary>
      ///    Validate that IsWhitespace throws no exception when given only whitespace.
      /// </summary>
      [Fact]
      public void IsWhitespace_WhenGivenWhitespace_ThrowsNoException()
      {
         Argument.IsWhitespace(AllWhitespaceString, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that Passes throws no exception when custom condition succeeds.
      /// </summary>
      [Fact]
      public void Passes_WhenCustomConditionSucceeds_ThrowsNoException()
      {
         Argument.Passes(Check.IsTrue, true, "none", "this should never fail.");
      }

      /// <summary>
      ///    Validate that PassesAll does not throw an exception when all conditions fail.
      /// </summary>
      [Fact]
      public void PassesAll_WhenAllConditionsSucceed_ThrowsNoException()
      {
         Argument.PassesAny(new Check.Signature<bool>[] {Check.IsTrue, Check.IsTrue}, true);
      }

      /// <summary>
      ///    Validate that PassesAll does not throw an exception when all conditions fail.
      /// </summary>
      [Fact]
      public void PassesAny_WhenAnyConditionsSucceeds_ThrowsNoException()
      {
         Argument.PassesAny(new Check.Signature<bool>[] {Check.IsTrue, Check.IsFalse}, true);
      }
      #endregion
   }
}
