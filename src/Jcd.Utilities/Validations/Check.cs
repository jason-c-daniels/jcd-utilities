using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcd.Utilities.Validations
{
    /// <summary>
    /// A helper class to assist in making certain classes of validations more human readable. 
    /// </summary>
    /// <remarks>
    /// The mthods in this helper build from a basic set of rudimentary validations, to aggregates like PassesAll.
    /// These are in turn used within the Argument enforcement helper, Argument and its derived methods,
    /// Keeping those methods as readable as possible. Additionally, while Argument is used for enforcing a non-null for 
    /// its consumers it could not be used within this class without injecting the potential for infinite recursion. To 
    /// work around that a sole private helper, EnforceNonNull was implemented and used judiciously.
    /// </remarks>
    public static class Check
    {
        /// <summary>
        /// The signature for delegates used in Passes<typeparamref name="T"/>, Fails<typeparamref name="T"/>, and aggregates of these methods (e.g. PassesAll). Most helpers implement this signature.
        /// </summary>
        /// <typeparam name="T">The data type to perform a validation on.</typeparam>
        /// <param name="value">the value to validate</param>
        /// <param name="onSuccess">The action to take, if any, when the delegate detects a "success" condition.</param>
        /// <param name="onFailure">The action to take, if any, when the delegate detects a "failure" condition.</param>
        /// <returns>true if successful, false otherwise.</returns>
        public delegate bool Signature<T>(T value, Action onSuccess=null, Action onFailure=null);

        #region Boolean and Null checks

        /// <summary>
        /// Returns the value of value, and executes any success or failure conditions.
        /// </summary>
        /// <remarks>
        /// This is a rudimentary helper method. While public it has little value outside of this or a similar framework.
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
        /// This is a rudimentary helper method. While public it has little value outside of this or a similar framework.
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
        #endregion

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
        /// <returns>True if the item is found, false otherwise.</returns>
        public static bool DoesNotContain<T>(IEnumerable<T> list, T target, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => !Contains(list, target), onSuccess, onFailure);
        }
        #endregion

        #region string operations

        public static bool IsEmpty(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value !=null && value.Length == 0, onSuccess, onFailure);
        }
        public static bool HasData(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value != null && value.Length > 0, onSuccess, onFailure);
        }
        public static bool IsWhitespace(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value?.TrimStart() == "" && HasData(value), onSuccess, onFailure);
        }
        public static bool IsNotWhitespace(string value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => !IsWhitespace(value), onSuccess, onFailure);
        }

        #endregion

        #region range and relational operations

        public static bool AreSameObject(object lhs, object rhs, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => ReferenceEquals(lhs, rhs), onSuccess, onFailure);
        }

        public static bool IsGreaterThan<T>(T lhs, T rhs, Action onSuccess = null, Action onFailure = null)
            where T : IComparable<T>
        {
            EnforceNonNull(lhs, rhs);
            return Passes(() => lhs.CompareTo(rhs) > 0, onSuccess, onFailure);
        }

        public static bool IsLessThan<T>(T lhs, T rhs, Action onSuccess = null, Action onFailure = null)
            where T : IComparable<T>
        {
            EnforceNonNull(lhs, rhs);
            return Passes(() => lhs.CompareTo(rhs) < 0, onSuccess, onFailure);
        }

        public static bool AreEqual<T>(T lhs, T rhs, Action onSuccess = null, Action onFailure = null)
            where T : IComparable<T>
        {
            EnforceNonNull(lhs, rhs);
            return Passes(() => lhs.CompareTo(rhs) == 0, onSuccess, onFailure);
        }

        public static bool InRange<T>(T value, T min, T max, Action onSuccess = null, Action onFailure = null)
            where T : IComparable<T>
        {
            EnforceNonNull(value, min, max);
            return Passes(() => min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0, onSuccess, onFailure);
        }

        public static bool NotInRange<T>(T value, T min, T max, Action onSuccess = null, Action onFailure = null)
            where T : IComparable<T>
        {
            EnforceNonNull(value, min, max);
            return Passes(() => value.CompareTo(min) <= 0 || value.CompareTo(max) >= 0, onSuccess, onFailure);
        }
        #endregion

        #region custom and multi-condition T
        public static bool Passes<T>(Signature<T> condition, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            return Passes(()=>condition(value), onSuccess, onFailure);
        }

        public static bool Fails<T>(Signature<T> condition, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            return Passes(() => !condition(value), onSuccess, onFailure);
        }

        public static bool Passes(Func<bool> condition, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            var result = condition();
            if (result) onSuccess?.Invoke(); else onFailure?.Invoke();
            return result;
        }

        public static bool Fails(Func<bool> condition, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(condition);
            return Passes(() => !condition(), onSuccess, onFailure);
        }

        public static bool PassesAll<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(conditions);
            bool result = true;
            int i = 0;
            foreach (var c in conditions)
            {
                result = result && (c == null ? result : c(value));
                i++;
                if (!result) break;
            }
            return Passes(() => result, onSuccess, onFailure);
        }

        public static bool FailsAll<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull<IEnumerable<Signature<T>>>(conditions);
            bool result = false;
            int i = 0;
            foreach (var c in conditions)
            {
                result = result || (c == null ? result : c(value));
                i++;
                if (result) break;
            }
            return Passes(() => !result, onSuccess, onFailure);
        }

        public static bool PassesAny<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(conditions);
            return !FailsAll(conditions, value, onFailure, onSuccess);
        }

        public static bool FailsAny<T>(IEnumerable<Signature<T>> conditions, T value, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(conditions);
            return !PassesAll(conditions, value, onFailure, onSuccess);
        }

        #endregion

        private static void EnforceNonNull<T>(params T[] values)
        {
            foreach (var val in values)
            {
                var v = val as object;
                if (v is null) throw new ArgumentNullException();
            }
        }

    }
}