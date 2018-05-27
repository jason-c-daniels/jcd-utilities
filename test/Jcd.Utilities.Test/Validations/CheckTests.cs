using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
    public class CheckTests
    {
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
            Assert.True(Check.IsNull(new object(), () => { onSuccessCalled = true; }, () => { onFailureCalled = true; }));
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
            Check.Passes((int value, Action onSuccess, Action onFailure) => true,1);
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

        #region collection tests
        #endregion

        #region range, equivalence, and relational tests
        [Fact]
        public void AreSameObject_BothNull_ExpectTrue()
        {
            Assert.True(Check.AreSameObject(null, null));
        }
        [Fact]
        public void AreSameObject_OnlyOneNull_ExpectFalse()
        {
            var sut = new object();
            Assert.False(Check.AreSameObject(sut, null));
            Assert.False(Check.AreSameObject(null, sut));
        }
        [Fact]
        public void AreSameObject_BothSameObject_ExpectTrue()
        {
            var o = new object();
            var o2 = o;
            Assert.True(Check.AreSameObject(o, o2));
            Assert.True(Check.AreSameObject(o2, o));
        }
        [Fact]
        public void AreSameObject_BothDifferentObjects_ExpectFalse()
        {
            var o = new object();
            var o2 = new object();
            Assert.False(Check.AreSameObject(o, o2));
            Assert.False(Check.AreSameObject(o2, o));
        }

        #endregion

        #region custom and multi-condition operations tests
        #endregion
    }
}
