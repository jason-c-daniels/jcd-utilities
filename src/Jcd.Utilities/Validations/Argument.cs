using System;
using System.Collections.Generic;

namespace Jcd.Utilities.Validations
{
   public static class Argument
   {
      #region exception helpers

      public const string UnspecifiedParamName = "[unspecified]";

      public static void RaiseArgumentException(string name = null, string message = null)
      {
         if (Check.IsNull(name) || Check.IsWhitespace(name) || Check.IsEmpty(name))
         {
            name = UnspecifiedParamName;
         }

         throw new ArgumentException(message ?? $"{name} contains an invalid value.", name);
      }

      public static void RaiseArgumentNullException(string name = null, string message = null)
      {
         if (Check.IsNull(name) || Check.IsWhitespace(name) || Check.IsEmpty(name))
         {
            name = UnspecifiedParamName;
         }

         throw new ArgumentNullException(name, message ?? $"{name} was null, expected non-null");
      }

      public static void RaiseArgumentOutOfRangeException<T>(T value, T min, T max, string name = null, string message = null)
      {
         if (Check.IsNull(name) || Check.IsWhitespace(name) || Check.IsEmpty(name))
         {
            name = UnspecifiedParamName;
         }

         throw new ArgumentOutOfRangeException(name, value,
                                               message ?? $"Argument {name} ({value}) had an invalid value. Expected value within range of {min} to {max}.");
      }

      public static void RaiseExpectationViolation<T>(T expected, T actual, string name = null, string message = null)
      {
         RaiseArgumentException(name, message ?? $"Expected {name} to be {expected}, but it was {actual}.");
      }

      #endregion exception helpers

      #region Boolean and Null checks

      public static void IsFalse(bool value, string name = null, string message = null)
      {
         Check.IsFalse(value, onFailure: () => RaiseExpectationViolation(false, true, name, message));
      }

      public static void IsNotNull<T>(T value, string name = null, string message = null)
      where T : class
      {
         Check.IsNotNull(value, onFailure: () => RaiseArgumentNullException(name, message));
      }

      public static void IsNull<T>(T value, string name = null, string message = null)
      where T : class
      {
         Check.IsNull(value, onFailure: () => RaiseExpectationViolation("null", "non-null", name, message));
      }

      public static void IsTrue(bool value, string name = null, string message = null)
      {
         Check.IsTrue(value, onFailure: () => RaiseExpectationViolation(true, false, name, message));
      }

      #endregion Boolean and Null checks

      #region collection operations

      public static void Contains<T>(IEnumerable<T> list, T target, string name = null, string message = null)
      {
         IsNotNull(list, name, message);
         Check.Contains(list, target, onFailure: () => RaiseArgumentException(name, message ?? $"{target} was not found in {name}."));
      }

      public static void DoesNotContain<T>(IEnumerable<T> list, T target, string name = null, string message = null)
      {
         IsNotNull(list, name, message);
         Check.DoesNotContain(list, target, onFailure: () => RaiseArgumentException(name,
                              message ?? $"{target} was expected to not be in {name}, but was found."));
      }

      public static void HasItems<T>(IEnumerable<T> list, string name = null, string message = null)
      {
         IsNotNull(list, name, message);
         Check.HasItems(list, onFailure: () => RaiseArgumentException(name,
                        message ?? $"Expected {name} to contain at least one item, but it was empty."));
      }

      public static void IsEmpty<T>(IEnumerable<T> list, string name = null, string message = null)
      {
         IsNotNull(list, name, message);
         Check.IsEmpty(list, onFailure: () => RaiseArgumentException(name,
                       message ?? $"Expected {name} to be an empty collection, but it contained values."));
      }

      #endregion collection operations

      #region string operations

      public static void IsNotEmpty(string value, string name = null, string message = null)
      {
         IsNotNull(value, name, message);
         Check.IsNotEmpty(value, onFailure: () => RaiseArgumentException(name,
                          message ?? $"Expected {name} to be a non-empty string, but it was empty."));
      }

      public static void IsEmpty(string value, string name = null, string message = null)
      {
         IsNotNull(value, name, message);
         Check.IsEmpty(value, onFailure: () => RaiseArgumentException(name,
                       message ?? $"Expected {name} to be an empty string, but it contains text."));
      }

      public static void IsNotNullOrEmpty(string value, string name = null, string message = null)
      {
         Check.FailsAll(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, value,
                        onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be non-null and non-empty."));
      }

      public static void IsNotNullOrWhitespace(string value, string name = null, string message = null)
      {
         Check.FailsAll(new Check.Signature<string>[] { Check.IsNull, Check.IsWhitespace }, value,
                        onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be null, empty, or whitespace."));
      }

      public static void IsNotNullWhitespaceOrEmpty(string value, string name = null, string message = null)
      {
         Check.FailsAll(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty, Check.IsWhitespace }, value,
                        onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be non-null, non-empty, and non-whitespace."));
      }

      public static void IsNotWhitespace(string value, string name = null, string message = null)
      {
         Check.IsNotWhitespace(value, onFailure: () => RaiseArgumentException(name,
                               message ?? $"Invalid argument: {name} is all whitespace."));
      }

      public static void IsNotWhitespaceOrEmpty(string value, string name = null, string message = null)
      {
         Check.FailsAll(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, value,
                        onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be non-empty and non-whitespace."));
      }

      public static void IsNullOrEmpty(string value, string name = null, string message = null)
      {
         Check.PassesAny(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, value,
                         onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be null or empty"));
      }

      public static void IsNullOrWhitespace(string value, string name = null, string message = null)
      {
         Check.PassesAny(new Check.Signature<string>[] { Check.IsNull, Check.IsWhitespace }, value,
                         onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be null or whitespace"));
      }

      public static void IsNullWhitespaceOrEmpty(string value, string name = null, string message = null)
      {
         Check.PassesAny(new Check.Signature<string>[] { Check.IsNull, Check.IsWhitespace, Check.IsEmpty }, value,
                         onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be null, whitespace, or empty"));
      }

      public static void IsWhitespace(string value, string name = null, string message = null)
      {
         IsNotNull(value, name, message);
         Check.IsWhitespace(value, onFailure: () => RaiseExpectationViolation("all whitespace", "non-whitespace", name, message));
      }

      public static void IsWhitespaceOrEmpty(string value, string name = null, string message = null)
      {
         IsNotNull(value, name, message);
         Check.PassesAny(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, value,
                         onFailure: () => RaiseArgumentException(name, message ?? $"Expected {name} to be whitespace or empty."));
      }

      #endregion string operations

      #region range and relational operations

      public static void AreEqual<T>(T value, T comparison, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.AreEqual(value, comparison, onFailure: () => RaiseArgumentException(name, message ?? $"Value for {name} ({value}) is not equal to {comparison}"));
      }

      public static void AreSameObject(object value, object comparison, string name = null, string message = null)
      {
         Check.AreSameObject(value, comparison, onFailure: () => RaiseArgumentException(name,
                             message ?? $"Object for {name} was not the expected instance."));
      }

      public static void InRange<T>(T value, T min, T max, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.InRange(value, min, max, onFailure: () => RaiseArgumentOutOfRangeException(value, min, max, name, message));
      }

      public static void IsGreaterThan<T>(T value, T comparison, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.IsGreaterThan(value, comparison, onFailure: () => RaiseArgumentException(name, message ?? $"Value for {name} ({value}) was expected to be greater than {comparison}"));
      }

      public static void IsGreaterThanOrEqual<T>(T value, T comparison, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.Passes(() => value.CompareTo(comparison) >= 0, onFailure: () => RaiseArgumentException(name, message ?? $"Value for {name} ({value}) was expected to be greater than or equal to {comparison}"));
      }

      public static void IsLessThan<T>(T value, T comparison, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.IsLessThan(value, comparison, onFailure: () => RaiseArgumentException(name, message ?? $"Value for {name} ({value}) was expected to be less than {comparison}"));
      }

      public static void IsLessThanOrEqual<T>(T value, T comparison, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.Passes(() => value.CompareTo(comparison) >= 0, onFailure: () => RaiseArgumentException(name, message ?? $"Value for {name} ({value}) was expected to be less than {comparison}"));
      }

      public static void NotInRange<T>(T value, T min, T max, string name = null, string message = null)
      where T : IComparable<T>
      {
         Check.NotInRange(value, min, max, onFailure: () => RaiseArgumentOutOfRangeException(value, min, max, name, message ?? $"Argument {name} ({value}) had an invalid value. Expected value outside of the range of {min} to {max}."));
      }

      #endregion range and relational operations

      #region custom and multi-condition operations

      public static void Fails<T>(Check.Signature<T> condition, T value, string name = null, string message = null)
      {
         Check.Passes(condition, value, onSuccess: () => RaiseArgumentException(name, message));
      }

      public static void FailsAll<T>(IEnumerable<Check.Signature<T>> conditions, T value, string name = null, string message = null)
      {
         Check.FailsAll(conditions, value, onFailure: () => RaiseArgumentException(name, message));
      }

      public static void FailsAny<T>(IEnumerable<Check.Signature<T>> conditions, T value, string name = null, string message = null)
      {
         Check.FailsAny(conditions, value, onFailure: () => RaiseArgumentException(name, message));
      }

      public static void Passes<T>(Check.Signature<T> condition, T value, string name = null, string message = null)
      {
         Check.Passes(condition, value, onFailure: () => RaiseArgumentException(name, message));
      }

      public static void PassesAll<T>(IEnumerable<Check.Signature<T>> conditions, T value, string name = null,
                                      string message = null)
      {
         Check.PassesAll(conditions, value, onFailure: () => RaiseArgumentException(name, message));
      }

      public static void PassesAny<T>(IEnumerable<Check.Signature<T>> conditions, T value, string name = null,
                                      string message = null)
      {
         Check.PassesAny(conditions, value, onFailure: () => RaiseArgumentException(name, message));
      }

      #endregion custom and multi-condition operations
   }
}