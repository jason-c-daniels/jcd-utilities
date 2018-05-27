using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcd.Utilities.Validations
{
    public static class Check
    {
        public delegate bool Signature<T>(T value, Action onSuccess=null, Action onFailure=null);

        #region Boolean and Null checks
        public static bool IsTrue(bool value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => value, onSuccess, onFailure);
        }

        public static bool IsFalse(bool value, Action onSuccess = null, Action onFailure = null)
        {
            return Passes(() => !value, onSuccess, onFailure);
        }

        public static bool IsNull<T>(T value, Action onSuccess = null, Action onFailure = null)
            where T : class
        {
            return Passes(() => value == null, onSuccess, onFailure);
        }

        public static bool IsNotNull<T>(T value, Action onSuccess = null, Action onFailure = null)
            where T : class
        {
            return Passes(() => value != null, onSuccess, onFailure);
        }
        #endregion

        #region collection operations
        public static bool IsEmpty<T>(IEnumerable<T> list, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => !HasItems(list), onSuccess, onFailure);
        }

        public static bool HasItems<T>(IEnumerable<T> list, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => list.Any(), onSuccess, onFailure);
        }

        public static bool Contains<T>(IEnumerable<T> list, T target, Action onSuccess = null, Action onFailure = null)
        {
            EnforceNonNull(list);
            return Passes(() => list.Contains(target), onSuccess, onFailure);
        }

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