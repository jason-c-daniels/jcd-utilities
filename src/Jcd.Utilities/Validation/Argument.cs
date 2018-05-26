using System;
using System.Collections.Generic;
using System.Text;

namespace Jcd.Utilities.Validation
{
    public static class Argument
    {
        const string UnspecifiedParamName = "[unspecified]";

        #region Boolean and Null checks
        public static void IsTrue(bool value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (!value) throw new ArgumentException(message ?? $"Expected {name} to be true, but it was false.", name);
        }

        public static void IsFalse(bool value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (value) throw new ArgumentException(message ?? $"Expected {name} to be false, but it was true.", name);
        }

        public static void IsNotNull<T>(T value, string name = null, string message = null)
            where T : class
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.IsNull(value)) throw new ArgumentNullException(name, message ?? $"Argument {name} was null. Expected non-null.");
        }

        public static void IsNull<T>(T value, string name = null, string message = null)
            where T : class
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.IsNotNull(value)) throw new ArgumentException(message ?? $"Argument {name} was non-null. Expected null.", name);
        }
        #endregion

        #region collection operations
        public static void IsEmpty<T>(IEnumerable<T> list, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.HasData(list)) throw new ArgumentException(message ?? $"Expected '{name}' to be empty but contained data.", name);
        }

        public static void HasData<T>(IEnumerable<T> list, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.IsEmpty(list)) throw new ArgumentException(message ?? $"Expected '{name}' to have data but was empty.", name);
        }

        public static void Contains<T>(IEnumerable<T> list, T target, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(list, nameof(list));
            if (Check.DoesNotContain(list,target)) throw new ArgumentException(message ?? $"Expected value {target} not found in collection: {name}", name);
        }

        public static void DoesNotContain<T>(IEnumerable<T> list, T target, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.Contains(list, target)) throw new ArgumentException(message ?? $"Invalid value {target} found in collection: {name}.", name);
        }
        #endregion

        #region string operations
        public static void IsEmpty(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(value, name);
            if (Check.HasData(value)) throw new ArgumentException(message ?? $"Expected {name} to be an empty string.", name);
        }
        public static void HasData(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(value, name);
            if (Check.IsEmpty(value)) throw new ArgumentException(message ?? $"Expected {name} to have data.", name);
        }
        public static void IsWhitespace(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(value, name);
            if (Check.IsNull(message)) message = $"Expected {name} to be non-zero length whitespace.";
            Passes((v) => v.Length > 0, value, name, message);
            if (Check.IsNotWhitespace(value)) throw new ArgumentException(message, name);
        }
        public static void IsWhitespaceOrEmpty(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(value, name);
            PassesAny(new Func<string,bool>[]{ Check.IsWhitespace, Check.IsEmpty}, value,name,message ?? $"Expected {name} to be whitespace or empty.");
        }
        public static void IsNullOrWhitespace(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            PassesAny(new Func<string, bool>[] { Check.IsNull , Check.IsWhitespace }, value, name, message ?? $"Expected {name} to be null or whitespace");
        }
        public static void IsNullWhitespaceOrEmpty(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            PassesAny(new Func<string, bool>[] { Check.IsNull, Check.IsEmpty, Check.IsWhitespace }, value, name, message ?? $"Expected {name} to be null, empty, or whitespace.");
        }

        public static void IsNullOrEmpty(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            PassesAny(new Func<string, bool>[] { Check.IsNull, Check.IsEmpty}, value, name, message ?? $"Expected {name} to be null or empty.");
        }

        public static void IsNotWhitespace(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.IsWhitespace(value)) throw new ArgumentException(message ?? $"Invalid argument: {name} is all whitespace.", name);
        }
        public static void IsNotWhitespaceOrEmpty(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            FailsAll(new Func<string, bool>[] { Check.IsEmpty, Check.IsWhitespace }, value, name, message ?? $"Expected {name} to be non-empty and non-whitespace.");
        }
        public static void IsNotNullOrWhitespace(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            FailsAll(new Func<string, bool>[] { Check.IsNull, Check.IsEmpty, Check.IsWhitespace }, value, name, message ?? $"Expected {name} to be null, empty, or whitespace.");
        }
        public static void IsNotNullWhitespaceOrEmpty(string value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            FailsAll(new Func<string, bool>[] { Check.IsNull, Check.IsEmpty, Check.IsWhitespace }, value, name, message ?? $"Expected {name} to be non-null, non-empty, and non-whitespace.");
        }
        #endregion

        #region range and relational operations

        public static void AreSameObject(object value, object comparison, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            if (!Check.AreSameObject(value, comparison)) throw new ArgumentException(message ?? $"Object for {name} was not the expected instance.");
        }

        public static void IsGreaterThan<T>(T value, T comparison, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (!Check.IsGreaterThan(value, comparison)) throw new ArgumentException(message ?? $"Value for {name} ({value}) was expected to be greater than {comparison}");
        }

        public static void IsGreaterThanOrEqual<T>(T value, T comparison, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (value.CompareTo(comparison) <0) throw new ArgumentException(message ?? $"Value for {name} ({value}) was expected to be greater than or equal to {comparison}");
        }

        public static void IsLessThan<T>(T value, T comparison, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (!Check.IsLessThan(value, comparison)) throw new ArgumentException(message ?? $"Value for {name} ({value}) was expected to be less than {comparison}");
        }

        public static void IsLessThanOrEqual<T>(T value, T comparison, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (value.CompareTo(comparison) > 0) throw new ArgumentException(message ?? $"Value for {name} ({value}) was expected to be less than {comparison}");
        }

        public static void AreEqual<T>(T value, T comparison, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (!Check.AreEqual(value, comparison)) throw new ArgumentException(message ?? $"Value for {name} ({value}) is not equal to {comparison}");
        }

        public static void InRange<T>(T value, T min, T max, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.NotInRange(value, min, max)) throw new ArgumentOutOfRangeException(name, value, message ?? $"Argument {name} ({value}) had an invalid value. Expected value within range of {min} to {max}.");
        }

        public static void NotInRange<T>(T value, T min, T max, string name = null, string message = null)
            where T : IComparable<T>
        {
            if (name == null) name = UnspecifiedParamName;
            if (Check.InRange(value, min, max)) throw new ArgumentOutOfRangeException(name, value, message ?? $"Argument {name} ({value}) had an invalid value. Expected value outside of the range of {min} to {max}.");
        }

        #endregion

        #region custom and multi-condition operations
        public static void Passes<T>(Func<T, bool> condition, T value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(condition, nameof(condition));
            if (Check.Fails(condition,value)) throw new ArgumentException(message ?? $"The custom condition for {name} failed. Expected success.", name);
        }

        public static void Fails<T>(Func<T, bool> condition, T value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(condition, nameof(condition));
            if (Check.Passes(condition, value)) throw new ArgumentException(message ?? $"The custom condition for {name} passed. Expected failure.", name);
        }

        public static void PassesAll<T>(IEnumerable<Func<T, bool>> conditions, T value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(conditions, nameof(conditions));                    
            if (Check.FailsAny(conditions,value)) throw new ArgumentException(message ?? $"One or more preconditions failed for {name}. Expected all to succeed.", name);
        }

        public static void PassesAny<T>(IEnumerable<Func<T, bool>> conditions, T value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(conditions, nameof(conditions));
            if (Check.FailsAll(conditions, value)) throw new ArgumentException(message ?? $"All preconditions failed for {name}. Expected at least one to succeed.", name);
        }

        public static void FailsAll<T>(IEnumerable<Func<T, bool>> conditions, T value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(conditions, nameof(conditions));
            if (Check.PassesAny(conditions, value)) throw new ArgumentException(message ?? $"One or more preconditions passed for {name}. Expected all to fail.", name);
        }

        public static void FailsAny<T>(IEnumerable<Func<T, bool>> conditions, T value, string name = null, string message = null)
        {
            if (name == null) name = UnspecifiedParamName;
            IsNotNull(conditions, nameof(conditions));
            if (Check.PassesAll(conditions, value)) throw new ArgumentException(message ?? $"All preconditions passed for {name}. Expected all to fail.", name);
        }
        #endregion
    }
}
