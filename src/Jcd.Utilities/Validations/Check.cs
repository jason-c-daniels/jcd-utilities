using System;
using System.Collections.Generic;
using System.Linq;

namespace Jcd.Utilities.Validations
{
    /// <summary>
    /// A helper class to assist in making certain classes of validations more human readable.
    /// </summary>
    /// <remarks>
    /// The mthods in this helper build from a basic set of rudimentary validations, to aggregates
    /// like PassesAll. These are in turn used within the Argument enforcement helper, Argument and
    /// its derived methods, Keeping those methods as readable as possible. Additionally, while
    /// Argument is used for enforcing a non-null for its consumers it could not be used within this
    /// class without injecting the potential for infinite recursion. To work around that a sole
    /// private helper, EnforceNonNull was implemented and used judiciously.
    /// </remarks>
    public static class Check
    {
        /// <summary>
        /// The signature for delegates used in Passes&lt; <typeparamref name="T"/>&gt;, Fails&lt;
        /// <typeparamref name="T"/>&gt;, and aggregates of these methods (e.g. PassesAll). Most
        /// helpers implement this signature.
        /// </summary>
        /// <typeparam name="T">The data type to perform a validation on.</typeparam>
        /// <param name="value">the value to validate</param>
        /// <param name="onSuccess">
        /// The action to take, if any, when the delegate detects a "success" condition.
        /// </param>
        /// <param name="onFailure">
        /// The action to take, if any, when the delegate detects a "failure" condition.
        /// </param>
        /// <returns>true if successful, false otherwise.</returns>
        public delegate bool Signature<T>(T value, Action onSuccess = null, Action onFailure = null);

        #region Boolean and Null checks

        /// <summary>
        /// Returns the value of value, and executes any success or failure conditions.
        /// </summary>
        /// <remarks>
        /// This is a rudimentary helper method. While public it has little value outside of this or
        /// a similar framework.
        /// </remarks>
        /// <param name="value">The value to evaluate and return</param>
        /// <param name="onSuccess">The action to take, if any, when value == true.</param>
        /// <param name="onFailure">The action to take, if any, when value == true.</param>
        /// <returns>the value of value</returns>
        public static bool IsTrue(bool value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value, onSuccess, onFailure);
        }

        /// <summary>
        /// Returns the logical complement of value, and executes any success or failure conditions.
        /// </summary>
        /// <remarks>
        /// This is a rudimentary helper method. While public it has little value outside of this or
        /// a similar framework.
        /// </remarks>
        /// <param name="value">The value to negate, evaluate, then return</param>
        /// <param name="onSuccess">The action to take, if any, when value == false.</param>
        /// <param name="onFailure">The action to take, if any, when value == false.</param>
        /// <returns>the logical negation of value(true if false, fals if true)</returns>
        public static bool IsFalse(bool value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => !value, onSuccess, onFailure);
        }

        /// <summary>
        /// Check if the provided value is null and, if applicable, take an indicated action.
        /// </summary>
        /// <typeparam name="T">The type of the data being evaluated</typeparam>
        /// <param name="value">The value being evaluated</param>
        /// <param name="onSuccess">The action to take, if any, when value == null.</param>
        /// <param name="onFailure">The action to take, if any, when value == null.</param>
        /// <returns>true if value is null, false otherwise</returns>
        public static bool IsNull<T>(T value, Action onSuccess = null, Action onFailure = null)
        where T : class
        {
            return Passes(() => value == null, onSuccess, onFailure);
        }

        /// <summary>
        /// Check if the provided value is not null and, if applicable, take an indicated action.
        /// </summary>
        /// <typeparam name="T">The type of the data being evaluated</typeparam>
        /// <param name="value">The value being evaluated</param>
        /// <param name="onSuccess">The action to take, if any, when value != null.</param>
        /// <param name="onFailure">The action to take, if any, when value != null.</param>
        /// <returns>true if value is null, false otherwise</returns>
        public static bool IsNotNull<T>(T value, Action onSuccess = null, Action onFailure = null)
        where T : class
        {
            return Passes(() => value != null, onSuccess, onFailure);
        }

        #endregion Boolean and Null checks

        #region collection operations

        /// <summary>
        /// Checks if a collection of type <typeparamref name="T"/> lacks entries.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in T</typeparam>
        /// <param name="list">The collection to check</param>
        /// <param name="onSuccess">The action to take, if any, when the collection is empty.</param>
        /// <param name="onFailure">The action to take, if any, when the collection is not empty.</param>
        /// <returns>True if list is empty, false otherwise</returns>
        public static bool IsEmpty<T>(IEnumerable<T> list, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => !HasItems(list), onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if a collection of type <typeparamref name="T"/> has entries.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in T</typeparam>
        /// <param name="list">The collection to check</param>
        /// <param name="onSuccess">The action to take, if any, when the collection has entries.</param>
        /// <param name="onFailure">The action to take, if any, when the collection is empty.</param>
        /// <returns>False if list is empty, true otherwise</returns>
        public static bool HasItems<T>(IEnumerable<T> list, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => list.Any(), onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if a collection of type <typeparamref name="T"/> contains a specific item.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in T</typeparam>
        /// <param name="list">The collection to check</param>
        /// <param name="target">The item to look for.</param>
        /// <param name="onSuccess">The action to take, if any, when the item is found.</param>
        /// <param name="onFailure">The action to take, if any, when the item is not found.</param>
        /// <returns>True if the item is found, false otherwise.</returns>
        public static bool Contains<T>(IEnumerable<T> list, T target, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => list.Contains(target), onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if a collection of type <typeparamref name="T"/> does not contain a specific item.
        /// </summary>
        /// <typeparam name="T">The type of the data stored in T</typeparam>
        /// <param name="list">The collection to check</param>
        /// <param name="target">The item to look for.</param>
        /// <param name="onSuccess">The action to take, if any, when the item is found.</param>
        /// <param name="onFailure">The action to take, if any, when the item is not found.</param>
        /// <returns>True if the item is not found, false otherwise.</returns>
        public static bool DoesNotContain<T>(IEnumerable<T> list, T target, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => !Contains(list, target), onSuccess, onFailure);
        }

        #endregion collection operations

        #region string operations

        /// <summary>
        /// Checks if a string is empty and not-null
        /// </summary>
        /// <param name="value">the value to check.</param>
        /// <param name="onSuccess">The action to take if the string is empty</param>
        /// <param name="onFailure">The action to take if the string is not empty</param>
        /// <returns>True if the string is empty, false otherwise.</returns>
        public static bool IsEmpty(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value != null && value.Length == 0, onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if a string has 1 or more characters in it.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="onSuccess">The action to take if the string is not empty</param>
        /// <param name="onFailure">The action to take if the string is empty</param>
        /// <returns>True if the string is not empty, false otherwise.</returns>
        public static bool HasData(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value != null && value.Length > 0, onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if the string has only whitespace and is not empty nor null.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="onSuccess">The action to take if the string is not empty.</param>
        /// <param name="onFailure">
        /// The action to take if the string is null, empty, or has nonwhitespace characters.
        /// </param>
        /// <returns>True if the string is non-zero length and only contains whitespace.</returns>
        public static bool IsWhitespace(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => HasData(value) && value.TrimStart() == "", onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if the string is not-null and is either empty or has non-whitespace characters.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="onSuccess">The action to take if the string is not empty.</param>
        /// <param name="onFailure">
        /// The action to take if the string is null, empty, or has nonwhitespace characters.
        /// </param>
        /// <returns>True if the string is non-null, and non-whitespace, false otherwise.</returns>
        public static bool IsNotWhitespace(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => !IsNull(value) && !IsWhitespace(value), onSuccess, onFailure);
        }

        #endregion string operations

        #region range and relational operations

        /// <summary>
        /// Checks if <paramref name="left"/> and <paramref name="right"/> are the same instance of
        /// an object.
        /// </summary>
        /// <param name="left">The lefthand side of the comparison</param>
        /// <param name="right">The righthand side of the comparison</param>
        /// <param name="onSuccess">The action to take if they're the same instance.</param>
        /// <param name="onFailure">The action to take if they're not the same instance.</param>
        /// <returns>true if left and right are the same instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="left"/> or <paramref name="right"/> are null.
        /// </exception>
        public static bool AreSameObject(object left, object right, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => ReferenceEquals(left, right), onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if <paramref name="left"/> is greater than <paramref name="right"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">The lefthand side of the comparison</param>
        /// <param name="right">The righthand side of the comparison</param>
        /// <param name="onSuccess">
        /// The action to take if <paramref name="left"/> is greater than <paramref name="right"/>
        /// </param>
        /// <param name="onFailure">
        /// The action to take if <paramref name="left"/> is not greater than <paramref name="right"/>
        /// </param>
        /// <returns>True if left is greater than right.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="left"/> or <paramref name="right"/> are null.
        /// </exception>
        public static bool IsGreaterThan<T>(T left, T right, Action onSuccess = null, Action onFailure = null)
        where T : IComparable<T>
        {
            EnforceNonNull(left, right);
            return Passes(() => left.CompareTo(right) > 0, onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if <paramref name="left"/> is less than <paramref name="right"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">The lefthand side of the comparison</param>
        /// <param name="right">The righthand side of the comparison</param>
        /// <param name="onSuccess">
        /// The action to take if <paramref name="left"/> is less than <paramref name="right"/>
        /// </param>
        /// <param name="onFailure">
        /// The action to take if <paramref name="left"/> is not less than <paramref name="right"/>
        /// </param>
        /// <returns>True if left is less than right.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="left"/> or <paramref name="right"/> are null.
        /// </exception>
        public static bool IsLessThan<T>(T left, T right, Action onSuccess = null, Action onFailure = null)
        where T : IComparable<T>
        {
            EnforceNonNull(left, right);
            return Passes(() => left.CompareTo(right) < 0, onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if <paramref name="left"/> is equivalent to <paramref name="right"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">The lefthand side of the comparison</param>
        /// <param name="right">The righthand side of the comparison</param>
        /// <param name="onSuccess">
        /// The action to take if <paramref name="left"/> is equivalent to <paramref name="right"/>
        /// </param>
        /// <param name="onFailure">
        /// The action to take if <paramref name="left"/> is not equivalent to <paramref name="right"/>
        /// </param>
        /// <returns>True if left is is equivalent to right.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="left"/> or <paramref name="right"/> are null.
        /// </exception>
        public static bool AreEqual<T>(T left, T right, Action onSuccess = null, Action onFailure = null)
        where T : IComparable<T>
        {
            EnforceNonNull(left, right);
            return Passes(() => left.CompareTo(right) == 0, onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is within the range defined by [ <paramref
        /// name="min"/>, <paramref name="max"/> ]. Or in other words: <paramref name="value"/> ∈ [
        /// <paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <typeparam name="T">
        /// The type of data being compared. It must implement IComparable&lt; <typeparamref name="T"/>&gt;
        /// </typeparam>
        /// <param name="value">The value to compare</param>
        /// <param name="min">The lower, inclusive, extent of the range.</param>
        /// <param name="max">The upper, inclusive extent of the range.</param>
        /// <param name="onSuccess">
        /// The action to take if <paramref name="value"/> ∈ [ <paramref name="min"/>, <paramref
        /// name="max"/> ]
        /// </param>
        /// <param name="onFailure">
        /// The action to take if <paramref name="value"/> ∉ [ <paramref name="min"/>, <paramref
        /// name="max"/> ]
        /// </param>
        /// <returns>
        /// True if <paramref name="value"/> ∈ [ <paramref name="min"/>, <paramref name="max"/> ]
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="value"/>, <paramref name="min"/>, or <paramref name="max"/> are null.
        /// </exception>
        public static bool InRange<T>(T value, T min, T max, Action onSuccess = null, Action onFailure = null)
        where T : IComparable<T>
        {
            EnforceNonNull(value, min, max);
            return Passes(() => min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0, onSuccess, onFailure);
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is NOT within the range defined by [ <paramref
        /// name="min"/>, <paramref name="max"/> ]. Or in other words: <paramref name="value"/> ∉ [
        /// <paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <typeparam name="T">
        /// The type of data being compared. It must implement IComparable&lt; <typeparamref name="T"/>&gt;
        /// </typeparam>
        /// <param name="value">The value to compare</param>
        /// <param name="min">The lower, inclusive, extent of the range.</param>
        /// <param name="max">The upper, inclusive extent of the range.</param>
        /// <param name="onSuccess">
        /// The action to take if <paramref name="value"/> ∉ [ <paramref name="min"/>, <paramref
        /// name="max"/> ]
        /// </param>
        /// <param name="onFailure">
        /// The action to take if <paramref name="value"/> ∈ [ <paramref name="min"/>, <paramref
        /// name="max"/> ]
        /// </param>
        /// <returns>
        /// True if <paramref name="value"/> ∉ [ <paramref name="min"/>, <paramref name="max"/> ]
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="value"/>, <paramref name="min"/>, or <paramref name="max"/> are null.
        /// </exception>
        public static bool NotInRange<T>(T value, T min, T max, Action onSuccess = null, Action onFailure = null)
        where T : IComparable<T>
        {
            EnforceNonNull(value, min, max);
            return Passes(() => value.CompareTo(min) <= 0 || value.CompareTo(max) >= 0, onSuccess, onFailure);
        }

        #endregion range and relational operations

        #region custom and multi-condition T

        /// <summary>
        /// Evaluates a predicate ( <paramref name="condition"/> ) on a value ( <paramref
        /// name="value"/> ) and returns the result of the evaluation.
        /// </summary>
        /// <typeparam name="T">The datatype being evaluated</typeparam>
        /// <param name="condition">The condition being evaluated</param>
        /// <param name="value">The value being evaluated</param>
        /// <param name="onSuccess">
        /// The action to perform if <paramref name="condition"/> returns true when passed <paramref name="value"/>
        /// </param>
        /// <param name="onFailure">
        /// The action to perform if <paramref name="condition"/> returns false when passed <paramref name="value"/>
        /// </param>
        /// <returns>
        /// The result of executing <paramref name="condition"/>( <paramref name="value"/> )
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="condition"/> is null.</exception>
        public static bool Passes<T>(Signature<T> condition, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            return Passes(() => condition(value), onSuccess, onFailure);
        }

        /// <summary>
        /// Evaluates a predicate ( <paramref name="condition"/> ) on a value ( <paramref
        /// name="value"/> ) and returns the logical complement of the result of the evaluation.
        /// </summary>
        /// <typeparam name="T">The datatype being evaluated</typeparam>
        /// <param name="condition">The condition being evaluated</param>
        /// <param name="value">The value being evaluated</param>
        /// <param name="onSuccess">
        /// The action to perform if <paramref name="condition"/> returns false when passed <paramref name="value"/>
        /// </param>
        /// <param name="onFailure">
        /// The action to perform if <paramref name="condition"/> returns true when passed <paramref name="value"/>
        /// </param>
        /// <returns>
        /// The logical complement of the result of executing <paramref name="condition"/>( <paramref
        /// name="value"/> )
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="condition"/> is null.</exception>
        public static bool Fails<T>(Signature<T> condition, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            return Passes(() => !condition(value), onSuccess, onFailure);
        }

        /// <summary>
        /// Evaluates a parameterless predicate ( <paramref name="condition"/> ) and returns the
        /// result of the evaluation.
        /// </summary>
        /// <param name="condition">The condition being evaluated</param>
        /// <param name="onSuccess">
        /// The action to perform if <paramref name="condition"/> returns true
        /// </param>
        /// <param name="onFailure">
        /// The action to perform if <paramref name="condition"/> returns false
        /// </param>
        /// <returns>The result of executing <paramref name="condition"/>()</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="condition"/> is null.</exception>
        public static bool Passes(Func<bool> condition, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            var result = condition();

            if (result)
            {
                onSuccess?.Invoke();
            }
            else
            {
                onFailure?.Invoke();
            }

            return result;
        }

        /// <summary>
        /// Evaluates a parameterless predicate ( <paramref name="condition"/> ) and returns the
        /// logical complement of the result of the evaluation.
        /// </summary>
        /// <param name="condition">The condition being evaluated</param>
        /// <param name="onSuccess">
        /// The action to perform if <paramref name="condition"/> returns false
        /// </param>
        /// <param name="onFailure">
        /// The action to perform if <paramref name="condition"/> returns true
        /// </param>
        /// <returns>The logical complement of the result of executing <paramref name="condition"/>()</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="condition"/> is null.</exception>
        public static bool Fails(Func<bool> condition, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            return Passes(() => !condition(), onSuccess, onFailure);
        }

        /// <summary>
        /// Evaluates a set of predicates ( <paramref name="conditions"/> ) on a value to determine
        /// if all predicates return true.
        /// </summary>
        /// <typeparam name="T">The type of data being evaluated</typeparam>
        /// <param name="conditions">The set of predicates to evaluate</param>
        /// <param name="value">The value to evaluate the predicates on.</param>
        /// <param name="onSuccess">The action to perform if all predicates return true.</param>
        /// <param name="onFailure">The action to perform if any predicate return false.</param>
        /// <returns>True if all predicates return true</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="conditions"/> is null or any individual entry is null.
        /// </exception>
        public static bool PassesAll<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null,
                                        Action onFailure = null)
        {
            EnforceNonNull(conditions);
            EnforceAllEntriesNonNull(conditions);
            bool result = true;
            int i = 0;

            foreach (var c in conditions)
            {
                result = result && (c == null ? result : c(value));
                i++;

                if (!result)
                {
                    break;
                }
            }

            return Passes(() => result, onSuccess, onFailure);
        }

        /// <summary>
        /// Evaluates a set of predicates ( <paramref name="conditions"/> ) on a value to determine
        /// if all predicates return false.
        /// </summary>
        /// <typeparam name="T">The type of data being evaluated</typeparam>
        /// <param name="conditions">The set of predicates to evaluate</param>
        /// <param name="value">The value to evaluate the predicates on.</param>
        /// <param name="onSuccess">The action to perform if all predicates return false.</param>
        /// <param name="onFailure">The action to perform if any predicate return true.</param>
        /// <returns>True if all predicates return false</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="conditions"/> is null or any individual entry is null.
        /// </exception>
        public static bool FailsAll<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null,
                                       Action onFailure = null)
        {
            EnforceNonNull<IEnumerable<Signature<T>>>(conditions);
            EnforceAllEntriesNonNull(conditions);
            bool result = false;
            int i = 0;

            foreach (var c in conditions)
            {
                result = result || (c == null ? result : c(value));
                i++;

                if (result)
                {
                    break;
                }
            }

            return Passes(() => !result, onSuccess, onFailure);
        }

        /// <summary>
        /// Evaluates a set of predicates ( <paramref name="conditions"/> ) on a value to determine
        /// if any predicates return true.
        /// </summary>
        /// <typeparam name="T">The type of data being evaluated</typeparam>
        /// <param name="conditions">The set of predicates to evaluate</param>
        /// <param name="value">The value to evaluate the predicates on.</param>
        /// <param name="onSuccess">The action to perform if any predicate return true.</param>
        /// <param name="onFailure">The action to perform if all predicates return false.</param>
        /// <returns>True if any predicate return true</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="conditions"/> is null or any individual entry is null.
        /// </exception>
        public static bool PassesAny<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null,
                                        Action onFailure = null)
        {
            EnforceNonNull(conditions);
            EnforceAllEntriesNonNull(conditions);
            return !FailsAll(conditions, value, onFailure, onSuccess);
        }

        /// <summary>
        /// Evaluates a set of predicates ( <paramref name="conditions"/> ) on a value to determine
        /// if any predicates return false.
        /// </summary>
        /// <typeparam name="T">The type of data being evaluated</typeparam>
        /// <param name="conditions">The set of predicates to evaluate</param>
        /// <param name="value">The value to evaluate the predicates on.</param>
        /// <param name="onSuccess">The action to perform if any predicate return false.</param>
        /// <param name="onFailure">The action to perform if all predicates return true.</param>
        /// <returns>True if any predicate return false</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="conditions"/> is null or any individual entry is null.
        /// </exception>
        public static bool FailsAny<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null,
                                       Action onFailure = null)
        {
            EnforceNonNull(conditions);
            EnforceAllEntriesNonNull(conditions);
            return !PassesAll(conditions, value, onFailure, onSuccess);
        }

        #endregion custom and multi-condition T

        private static void EnforceNonNull<T>(params T[] values)
        {
            foreach (var val in values)
            {
                var v = val as object;

                if (v is null)
                {
                    throw new ArgumentNullException();
                }
            }
        }

        private static void EnforceAllEntriesNonNull<T>(IEnumerable<Signature<T>> conditions)
        {
            if (conditions.Any(c => c == null))
            {
                throw new ArgumentNullException(nameof(conditions), "All entries must be non-null");
            }
        }
    }
}