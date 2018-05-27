using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
    public class CheckTests
    {
        class IntHolder : IComparable<IntHolder>
        {
            public int Value;
            public IntHolder() { }
            public IntHolder(int v) { Value = v; }
            public int CompareTo(IntHolder other)
            {
                return Value.CompareTo(other.Value);
            }
        }

        #region boolean tests
        [Fact]
        public void IsTrue_PassFalse_ExpectFalseAndOnFailureCalled()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.IsTrue(false, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }
        [Fact]
        public void IsTrue_PassTrue_ExpectTrueAndOnSuccessCalled()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.IsTrue(true, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsFalse_PassTrue_ExpectFalseAndOnFailureCalled()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.IsFalse(true, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }
        [Fact]
        public void IsFalse_PassFalse_ExpectTrueAndOnSuccessCalled()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.IsFalse(false, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }
        #endregion

        #region null tests
        [Fact]
        public void IsNull_PassObject_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.IsNull(new object(), () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void IsNull_PassNull_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.IsNull((object)null, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsNotNull_PassObject_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.IsNotNull(new object(), () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsNotNull_PassNull_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.IsNotNull((object)null, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        #endregion

        #region collection tests

        [Fact]
        public void IsEmpty_NullCollection_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            List<int> list = null;
            Assert.Throws<ArgumentNullException>(() => Check.IsEmpty(list, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        public void IsEmpty_EmptyCollection_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>();
            Assert.True(Check.IsEmpty(list, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsEmpty_NonEmptyCollection_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>(new[] { 1 });
            Assert.False(Check.IsEmpty(list, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void HasItems_NullCollection_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            List<int> list = null;
            Assert.Throws<ArgumentNullException>(() => Check.HasItems(list, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        public void HasItems_EmptyCollection_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>();
            Assert.False(Check.HasItems(list, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void HasItems_NonEmptyCollection_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>(new[] { 1 });
            Assert.True(Check.HasItems(list, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void Contains_NullCollection_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            List<int> list = null;
            Assert.Throws<ArgumentNullException>(() => Check.Contains(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        public void Contains_EmptyCollection_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>();
            Assert.False(Check.Contains(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void Contains_NonEmptyCollectionItemNotFound_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>(new[] { 2, 3 });
            Assert.False(Check.Contains(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void Contains_NonEmptyCollectionItemFound_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>(new[] { 1, 2, 3 });
            Assert.True(Check.Contains(list, 2, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void DoesNotContain_NullCollection_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            List<int> list = null;
            Assert.Throws<ArgumentNullException>(() => Check.DoesNotContain(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        public void DoesNotContain_EmptyCollection_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>();
            Assert.True(Check.DoesNotContain(list, 2, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void DoesNotContain_NonEmptyCollectionItemNotFound_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>(new[] { 1, 2, 3 });
            Assert.True(Check.DoesNotContain(list, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void DoesNotContain_NonEmptyCollectionItemFound_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var list = new List<int>(new[] { 1, 2, 3 });
            Assert.False(Check.DoesNotContain(list, 2, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }
        #endregion

        #region string operations
        [Fact]
        public void IsEmpty_EmptyString_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.IsEmpty("", () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsEmpty_NonEmptyString_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.IsEmpty("123", () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void IsEmpty_NullString_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.IsEmpty((string)null, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void HasData_NonEmptyString_ExpectF()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.HasData("123", () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void HasData_EmptyString_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.HasData("", () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void HasData_NullString_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.HasData((string)null, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        #endregion

        #region range, equivalence, and relational tests
        [Fact]
        public void AreSameObject_BothNull_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.AreSameObject(null, null, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void AreSameObject_OnlyOneNull_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var o = new object();
            Assert.False(Check.AreSameObject(o, null, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");

            Assert.False(Check.AreSameObject(null, o, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void AreSameObject_BothSameObject_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var o = new object();
            var o2 = o;
            Assert.True(Check.AreSameObject(o, o2, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.True(Check.AreSameObject(o2, o, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void AreSameObject_BothDifferentObjects_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var o = new object();
            var o2 = new object();
            Assert.False(Check.AreSameObject(o, o2, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");

            Assert.False(Check.AreSameObject(o2, o, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void IsLessThan_NullArgs_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var x = new IntHolder(1);
            IntHolder y = null; ;
            Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");

            Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, x, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, y, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsLessThan_LeftIsLessThanRight_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            int x = 1, y = 2;
            Assert.True(Check.IsLessThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"IsLessThan({x},{y}) failed.");
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsLessThan_LeftIsGreaterOrEqualToRight_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            int x = 2, y = 1;
            Assert.False(Check.IsLessThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"IsLessThan({x},{y}) failed.");
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
            y = 2;
            Assert.False(Check.IsLessThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"IsLessThan({y},{x}) failed.");
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void IsGreaterThan_NullArgs_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var x = new IntHolder(1);
            IntHolder y = null; ;
            Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, x, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, y, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsGreaterThan_LeftIsGreaterThanRight_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            int x = 2, y = 1;
            Assert.True(Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"AreEqual({x},{y}) failed.");
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void IsGreaterThan_LeftIsLessOrEqualToRight_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            int x = 1, y = 2;
            Assert.False(Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"IsGreaterThan({x},{y}) failed.");
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
            y = 2;
            Assert.False(Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"IsGreaterThan({y},{x}) failed.");
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void AreEqual_NullArgs_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var x = new IntHolder(1);
            IntHolder y = null; ;
            Assert.Throws<ArgumentNullException>(() => Check.AreEqual(x, y, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");

            Assert.Throws<ArgumentNullException>(() => Check.AreEqual(y, x, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.Throws<ArgumentNullException>(() => Check.AreEqual(y, y, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void AreEqual_LeftIsEqualToRight_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            int x = 1, y = x;
            Assert.True(Check.AreEqual(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"AreEqual({x},{y}) failed.");
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void AreEqual_LeftIsNotEqualToRight_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            int x = 1, y = 2;
            Assert.False(Check.AreEqual(x, y, () => onSuccessCalled = true, () => onFailureCalled = true), $"AreEqual({x},{y}) failed.");
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");

            Assert.False(Check.AreEqual(y, x, () => onSuccessCalled = true, () => onFailureCalled = true), $"AreEqual({y},{x}) failed.");
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void InRange_NullArgs_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var v0 = new IntHolder(1);
            var v1 = new IntHolder(5);
            IntHolder nv = null;
            Assert.Throws<ArgumentNullException>(() => Check.InRange(nv, v0, v1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");

            Assert.Throws<ArgumentNullException>(() => Check.InRange(v0, nv, v1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.Throws<ArgumentNullException>(() => Check.InRange(v1, v0, nv, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void InRange_ValueIsInRange_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.InRange(2, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void InRange_ValueIsNotInRange_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            // greater
            Assert.False(Check.InRange(21, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");

            // lower
            Assert.False(Check.InRange(-1, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        public void NotInRange_NullArgs_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            var v0 = new IntHolder(1);
            var v1 = new IntHolder(5);
            IntHolder nv = null;
            Assert.Throws<ArgumentNullException>(() => Check.NotInRange(nv, v0, v1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");

            Assert.Throws<ArgumentNullException>(() => Check.NotInRange(v0, nv, v1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            Assert.Throws<ArgumentNullException>(() => Check.NotInRange(v1, v0, nv, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void NotInRange_ValueIsNotInRange_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            // greater
            Assert.True(Check.NotInRange(21, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");

            // lower
            Assert.True(Check.NotInRange(-1, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        public void NotInRange_ValueIsNotInRange_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.NotInRange(2, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }


        #endregion

        #region custom and multi-condition operations tests

        [Fact]
        void Passes_T_DelegateIsNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.Passes(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void Passes_T_DelegateReturnsTrue_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.Passes((v, s, f) => true, 1, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        void Passes_T_DelegateReturnsFalse_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.Passes((v, s, f) => false, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        void Fails_T_DelegateIsNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.Fails(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void Fails_T_DelegateReturnsTrue_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.Fails((v,s,f) => true, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        void Fails_T_DelegateReturnsFalse_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.Fails((v, s, f) => false, 1, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        void Passes_DelegateIsNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.Passes(null, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void Passes_DelegateReturnsTrue_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.Passes(() => true, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }

        [Fact]
        void Passes_DelegateReturnsFalse_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.Passes(() => false, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }


        [Fact]
        void Fails_DelegateIsNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.Fails(null, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void Fails_DelegateReturnsTrue_ExpectFalse()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.False(Check.Fails(()=>true, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
        }

        [Fact]
        void Fails_DelegateReturnsFalse_ExpectTrue()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.True(Check.Fails(() => false, () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
            Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
        }


        [Fact]
        void PassesAll_DelegateListNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.PassesAll(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void PassesAll_FirstDelegateNull_ExpectItToBeIgnored()
        {
            throw new NotImplementedException();
        }

        void PassesAll_AllDelegatesReturnTrue_ExpectTrue()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void PassesAll_OneDelegateReturnsFalse_ExpectFalse()
        {
            throw new NotImplementedException();
        }


        [Fact]
        void PassesAny_DelegateListNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.PassesAny(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void PassesAny_FirstDelegateNull_ExpectItToBeIgnored()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void PassesAny_OneDelegateReturnsTrue_ExpectTrue()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void PassesAny_AllDelegatesReturnFalse_ExpectFalse()
        {
            throw new NotImplementedException();
        }


        [Fact]
        void FailsAll_DelegateListNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.FailsAll(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void FailsAll_FirstDelegateNull_ExpectItToBeIgnored()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void FailsAll_AllDelegatesReturnTrue_ExpectTrue()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void FailsAll_OneDelegateReturnsFalse_ExpectFalse()
        {
            throw new NotImplementedException();
        }


        [Fact]
        void FailsAny_DelegateListNull_ExpectArgumentNullException()
        {
            bool onFailureCalled = false;
            bool onSuccessCalled = false;
            Assert.Throws<ArgumentNullException>(() => Check.FailsAny(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
            Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
            Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
        }

        [Fact]
        void FailsAny_FirstDelegateNull_ExpectItToBeIgnored()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void FailsAny_OneDelegateReturnsTrue_ExpectTrue()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void FailsAny_AllDelegatesReturnFalse_ExpectFalse()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region misc tests
        [Fact]
        public void VariousMethods_LackingHandlers_NoExceptionsThrown()
        {
            var o1 = new object();
            var o2 = new object();
            var @null = (object)null;
            var el = new List<int>();
            var il = new List<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            Check.IsTrue(true);
            Check.IsTrue(false);
            Check.IsFalse(true);
            Check.IsFalse(false);
            Check.IsNull(o1);
            Check.IsNull(@null);
            Check.IsNotNull(o2);
            Check.IsNotNull(@null);
            Check.AreSameObject(null, o1);
            Check.AreSameObject(o1, null);
            Check.AreSameObject(o1, o1);
            Check.AreSameObject(o1, o2);

            Check.AreEqual(1, 1);
            Check.AreEqual(1, 0);
            Check.AreEqual(0, 1);
            Check.IsGreaterThan(1, 0);
            Check.IsGreaterThan(0, 0);
            Check.IsGreaterThan(0, 1);
            Check.IsLessThan(1, 0);
            Check.IsLessThan(0, 0);
            Check.IsLessThan(0, 1);
            Check.InRange(0, 1, 5);
            Check.InRange(0, 1, 6);
            Check.InRange(1, 1, 5);
            Check.InRange(5, 1, 5);
            Check.InRange(5, 1, 5);
            Check.NotInRange(0, 1, 5);
            Check.NotInRange(0, 1, 6);
            Check.NotInRange(1, 1, 5);
            Check.NotInRange(5, 1, 5);
            Check.NotInRange(5, 1, 5);

            Check.Contains(el, 1);
            Check.Contains(il, 5);
            Check.Contains(il, 15);
            Check.DoesNotContain(el, 5);
            Check.DoesNotContain(il, 5);
            Check.DoesNotContain(il, 15);
            Check.HasItems(el);
            Check.HasItems(il);
            Check.IsEmpty(el);
            Check.IsEmpty(il);
            Check.IsEmpty("");
            Check.IsEmpty(" ");
            Check.IsEmpty(null);
            Check.HasData("");
            Check.HasData(" ");
            Check.HasData(null);

            Check.IsWhitespace("");
            Check.IsWhitespace(" ");
            Check.IsWhitespace(null);
            Check.IsNotWhitespace("");
            Check.IsNotWhitespace(" ");
            Check.IsNotWhitespace(null);

            Check.Fails(() => true);
            Check.Fails(() => false);
            Check.Passes(() => false);
            Check.Passes(() => true);
            Check.Passes((int value, Action onSuccess, Action onFailure) => true, 1);
            Check.Passes((int value, Action onSuccess, Action onFailure) => false, 1);
            Check.Fails((int value, Action onSuccess, Action onFailure) => true, 1);
            Check.Fails((int value, Action onSuccess, Action onFailure) => false, 1);
            Check.FailsAll(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, "");
            Check.FailsAll(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, "");
            Check.PassesAll(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, "");
            Check.PassesAll(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, "");
            Check.FailsAny(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, "");
            Check.FailsAny(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, "");
            Check.PassesAny(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, "");
            Check.PassesAny(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, null);
            Check.FailsAll(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, null);
            Check.FailsAll(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, null);
            Check.PassesAll(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, null);
            Check.PassesAll(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, null);
            Check.FailsAny(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, null);
            Check.FailsAny(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, null);
            Check.PassesAny(new Check.Signature<string>[] { Check.IsNull, Check.IsEmpty }, null);
            Check.PassesAny(new Check.Signature<string>[] { Check.IsWhitespace, Check.IsEmpty }, null);
        }
        #endregion
    }
}
