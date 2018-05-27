using Jcd.Utilities.Validations;
using System;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
    public class CheckTests
    {
        #region boolean tests
        [Fact]
        public void IsTrue_PassFalse_ExpectFalse()
        {
            Assert.False(Check.IsTrue(false));
        }
        [Fact]
        public void IsTrue_PassTrue_ExpectTrue()
        {
            Assert.True(Check.IsTrue(true));
        }
        [Fact]
        public void IsFalse_PassFalse_ExpectTrue()
        {
            Assert.True(Check.IsFalse(false));
        }
        [Fact]
        public void IsFalse_PassTrue_ExpectFalse()
        {
            Assert.False(Check.IsFalse(true));
        }
        #endregion

        #region null tests
        [Fact]
        public void IsNull_PassObject_ExpectFalse()
        {
            Assert.False(Check.IsNull(new object()));
        }
        [Fact]
        public void IsNull_PassNull_ExpectTrue()
        {
            Assert.True(Check.IsNull((object)null));
        }
        [Fact]
        public void IsNotNull_PassObject_ExpectTrue()
        {
            Assert.True(Check.IsNotNull(new object()));
        }
        [Fact]
        public void IsNotNull_PassNull_ExpectFalse()
        {
            Assert.False(Check.IsNotNull((object)null));
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
