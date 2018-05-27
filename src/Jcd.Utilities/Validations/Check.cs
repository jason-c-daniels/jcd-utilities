using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcd.Utilities.Validations
{
    public static class Check
    {
        #region Boolean and Null checks
        public static bool IsTrue(bool value)
        {
            return value;
        }

        public static bool IsFalse(bool value)
        {
            return !value;
        }

        public static bool IsNull<T>(T value)
            where T : class
        {
            return value == null;
        }

        public static bool IsNotNull<T>(T value)
            where T : class
        {
            return value != null;
        }
        #endregion

        #region collection operations
        public static bool IsEmpty<T>(IEnumerable<T> list)
        {
            return !HasData(list);
        }

        public static bool HasData<T>(IEnumerable<T> list)
        {
            return list.Any();
        }

        public static bool Contains<T>(IEnumerable<T> list,T target)
        {
            return list.Contains(target);
        }

        public static bool DoesNotContain<T>(IEnumerable<T> list, T target)
        {
            return !Contains(list,target);
        }
        #endregion

        #region string operations

        public static bool IsEmpty(string value)
        {
            return value.Length == 0;
        }
        public static bool HasData(string value)
        {
            return value.Length > 0;
        }
        public static bool IsWhitespace(string value)
        {
            return value?.TrimStart() == "" && HasData(value);
        }
        public static bool IsNotWhitespace(string value)
        {
            return !IsWhitespace(value);
        }

        #endregion

        #region range and relational operations

        public static bool AreSameObject(object lhs, object rhs)
        {
            return ReferenceEquals(lhs,rhs);
        }

        public static bool IsGreaterThan<T>(T lhs, T rhs)
            where T : IComparable<T>
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool IsLessThan<T>(T lhs, T rhs)
            where T : IComparable<T>
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool AreEqual<T>(T lhs, T rhs)
            where T : IComparable<T>
        {
            return lhs.CompareTo(rhs) == 0;
        }

        public static bool InRange<T>(T value, T min, T max)
            where T : IComparable<T>
        {
            return min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0;
        }

        public static bool NotInRange<T>(T value, T min, T max)
            where T : IComparable<T>
        {
            return value.CompareTo(min) <= 0 || value.CompareTo(max) >= 0;
        }
        #endregion

        #region custom and multi-condition operations
        public static bool Passes<T>(Func<T,bool> condition, T value)
        {
            Argument.IsNotNull(condition, nameof(condition));
            return condition(value);
        }

        public static bool Fails<T>(Func<T, bool> condition, T value)
        {
            return !Passes(condition,value);
        }

        public static bool PassesAll<T>(IEnumerable<Func<T, bool>> conditions, T value)
        {
            Argument.IsNotNull(conditions, nameof(conditions));
            bool result = true;
            int i = 0;
            foreach (var c in conditions)
            {
                result = result && (c==null ? result : c(value));
                i++;
                if (!result) break;
            }
            return result;
        }

        public static bool FailsAll<T>(IEnumerable<Func<T, bool>> conditions, T value)
        {
            Argument.IsNotNull(conditions, nameof(conditions));
            bool result = false;
            int i = 0;
            foreach (var c in conditions)
            {
                result = result || (c == null ? result : c(value));
                i++;
                if (result) break;
            }
            return !result;
        }

        public static bool PassesAny<T>(IEnumerable<Func<T, bool>> conditions, T value)
        {
            return !FailsAll(conditions,value);
        }

        public static bool FailsAny<T>(IEnumerable<Func<T, bool>> conditions, T value)
        {
            return !PassesAll(conditions, value);
        }

        #endregion
    }
}