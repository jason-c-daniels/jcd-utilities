using Jcd.Utilities.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Jcd.Utilities.Test.Validations
{
   public class CheckTests
   {
      private static readonly Check.Signature<int> NullDelegate = null;

      private static bool FalseDelegate<T>(T value, Action onSuccess = null, Action onFailure = null)
      {
         return false;
      }

      private static bool TrueDelegate<T>(T value, Action onSuccess = null, Action onFailure = null)
      {
         return true;
      }

      [Fact]
      public void AreEqual_WhenGivenNullArgs_ThrowsNoException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var x = new IntHolder(1);
         IntHolder y = null;
         Check.AreEqual(x, y, () => onSuccessCalled = true, () => onFailureCalled = true);
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onFailure was called when it shouldn't have been.");
         onFailureCalled = onSuccessCalled = false;
         Check.AreEqual(y, x, () => onSuccessCalled = true, () => onFailureCalled = true);
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onFailure was called when it shouldn't have been");
         onFailureCalled = onSuccessCalled = false;
         Check.AreEqual(y, y, () => onSuccessCalled = true, () => onFailureCalled = true);
         Assert.True(onSuccessCalled, "onSuccess was not called when it should have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void AreEqual_WhenLeftIsEqualToRight_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         int x = 1, y = x;
         Assert.True(Check.AreEqual(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                     $"AreEqual({x},{y}) failed.");
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void AreEqual_WhenLeftIsNotEqualToRight_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         int x = 1, y = 2;
         Assert.False(Check.AreEqual(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                      $"AreEqual({x},{y}) failed.");
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
         Assert.False(Check.AreEqual(y, x, () => onSuccessCalled = true, () => onFailureCalled = true),
                      $"AreEqual({y},{x}) failed.");
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void AreSameObject_WhenBothAreNull_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.AreSameObject(null, null, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void AreSameObject_WhenBothAreSameObject_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var o = new object();
         var o2 = o;
         Assert.True(
            Check.AreSameObject(o, o2, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
         Assert.True(
            Check.AreSameObject(o2, o, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void AreSameObject_WhenBothDifferentObjects_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var o = new object();
         var o2 = new object();
         Assert.False(Check.AreSameObject(o, o2, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
         Assert.False(Check.AreSameObject(o2, o, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void AreSameObject_WhenOnlyOneIsNull_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var o = new object();
         Assert.False(Check.AreSameObject(o, null, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
         Assert.False(Check.AreSameObject(null, o, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Contains_WhenGivenEmptyCollection_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>();
         Assert.False(Check.Contains(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Contains_WhenGivenNonEmptyCollectionAndItemFound_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>(new[] { 1, 2, 3 });
         Assert.True(Check.Contains(list, 2, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void Contains_WhenGivenNonEmptyCollectionAndItemNotFound_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>(new[] { 2, 3 });
         Assert.False(Check.Contains(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Contains_WhenGivenNullCollection_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         List<int> list = null;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.Contains(list, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void DoesNotContain_WhenGivenEmptyCollection_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>();
         Assert.True(Check.DoesNotContain(list, 2, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void DoesNotContain_WhenGivenNonEmptyCollectionAndItemFound_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>(new[] { 1, 2, 3 });
         Assert.False(Check.DoesNotContain(list, 2, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void DoesNotContain_WhenGivenNonEmptyCollectionAndItemNotFound_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>(new[] { 1, 2, 3 });
         Assert.True(Check.DoesNotContain(list, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void DoesNotContain_WhenGivenNullCollection_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         List<int> list = null;
         Assert.Throws<ArgumentNullException>(() => Check.DoesNotContain(list, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void Fails_T_WhenDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.Fails(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void Fails_T_WhenDelegateReturnsFalse_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.Fails((v, s, f) => false, 1, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void Fails_T_WhenDelegateReturnsTrue_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.Fails((v, s, f) => true, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Fails_WhenDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.Fails(null, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void Fails_WhenDelegateReturnsFalse_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.Fails(() => false, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void Fails_WhenDelegateReturnsTrue_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.Fails(() => true, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void FailsAll_WhenAllDelegatesReturnFalse_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { FalseDelegate, FalseDelegate, FalseDelegate, FalseDelegate };
         Assert.True(Check.FailsAll(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void FailsAll_WhenDelegateListIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.FailsAll(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void FailsAll_WhenFirstDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { NullDelegate, FalseDelegate, TrueDelegate, FalseDelegate, TrueDelegate };
         Assert.Throws<ArgumentNullException>(() => Check.FailsAll(delegates, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void FailsAll_WhenOneDelegateReturnsTrue_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { FalseDelegate, TrueDelegate, FalseDelegate, TrueDelegate };
         Assert.False(Check.FailsAll(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void FailsAny_WhenAllDelegatesReturnFalse_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { FalseDelegate, FalseDelegate, FalseDelegate, FalseDelegate };
         Assert.True(Check.FailsAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void FailsAny_WhenAllDelegatesReturnTrue_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { TrueDelegate, TrueDelegate, TrueDelegate };
         Assert.False(Check.FailsAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void FailsAny_WhenDelegateListIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.FailsAny(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void FailsAny_WhenFirstDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { NullDelegate, TrueDelegate, TrueDelegate, FalseDelegate, TrueDelegate };
         Assert.Throws<ArgumentNullException>(() => Check.FailsAny(delegates, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void FailsAny_WhenOneDelegateReturnsFalse_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { TrueDelegate, TrueDelegate, FalseDelegate, TrueDelegate };
         Assert.True(Check.FailsAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void HasItems_WhenGivenEmptyCollection_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>();
         Assert.False(Check.HasItems(list, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void HasItems_WhenGivenNonEmptyCollection_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>(new[] { 1 });
         Assert.True(Check.HasItems(list, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void HasItems_WhenGivenNullCollection_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         List<int> list = null;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.HasItems(list, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void InRange_WhenGivenNullArgs_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var v0 = new IntHolder(1);
         var v1 = new IntHolder(5);
         IntHolder nv = null;
         Assert.Throws<ArgumentNullException>(() => Check.InRange(nv, v0, v1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
         Assert.Throws<ArgumentNullException>(() => Check.InRange(v0, nv, v1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
         Assert.Throws<ArgumentNullException>(() => Check.InRange(v1, v0, nv, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void InRange_WhenValueIsInRange_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.InRange(2, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void InRange_WhenValueIsNotInRange_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
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
      public void IsEmpty_WhenGivenEmptyCollection_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>();
         Assert.True(Check.IsEmpty(list, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsEmpty_WhenGivenEmptyString_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsEmpty("", () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsEmpty_WhenGivenNonEmptyCollection_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var list = new List<int>(new[] { 1 });
         Assert.False(Check.IsEmpty(list, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsEmpty_WhenGivenNonEmptyString_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsEmpty("123", () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsEmpty_WhenGivenNullCollection_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         List<int> list = null;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.IsEmpty(list, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void IsEmpty_WhenGivenNullString_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsEmpty(null, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsFalse_WhenGivenFalse_ReturnsTrueAndOnSuccessCalled()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsFalse(false, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsFalse_WhenGivenTrue_ReturnsFalseAndOnFailureCalled()
      {
         bool onFailureCalled;
         bool onSuccessCalled;
         onFailureCalled = false;
         onSuccessCalled = false;
         Assert.False(Check.IsFalse(true, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsGreaterThan_WhenLeftIsGreaterThanRight_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         int x = 2, y = 1;
         Assert.True(Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                     $"AreEqual({x},{y}) failed.");
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsGreaterThan_WhenLeftIsLessOrEqualToRight_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         int x = 1, y = 2;
         Assert.False(Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                      $"IsGreaterThan({x},{y}) failed.");
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
         y = 2;
         Assert.False(Check.IsGreaterThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                      $"IsGreaterThan({y},{x}) failed.");
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsGreaterThan_WhenNullArgs_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var x = new IntHolder(1);
         IntHolder y = null;
         Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(x, y, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
         Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, x, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
         Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, y, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsLessThan_WhenGivenNullArgs_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var x = new IntHolder(1);
         IntHolder y = null;
         Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(x, y, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
         Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, x, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
         Assert.Throws<ArgumentNullException>(() => Check.IsGreaterThan(y, y, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsLessThan_WhenLeftIsGreaterOrEqualToRight_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         int x = 2, y = 1;
         Assert.False(Check.IsLessThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                      $"IsLessThan({x},{y}) failed.");
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
         y = 2;
         Assert.False(Check.IsLessThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                      $"IsLessThan({y},{x}) failed.");
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsLessThan_WhenLeftIsLessThanRight_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         int x = 1, y = 2;
         Assert.True(Check.IsLessThan(x, y, () => onSuccessCalled = true, () => onFailureCalled = true),
                     $"IsLessThan({x},{y}) failed.");
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsNotEmpty_WhenGivenEmptyString_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsNotEmpty("", () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsNotEmpty_WhenGivenNonEmptyString_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsNotEmpty("123", () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsNotEmpty_WhenGivenNullString_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsNotEmpty(null, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsNotNull_WhenGivenAnObject_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsNotNull(new object(), () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsNotNull_WhenGivenNull_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsNotNull((object)null, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsNull_WhenGivenAnObject_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsNull(new object(), () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsNull_WhenGivenNull_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsNull((object)null, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsTrue_WhenGivenFalse_ReturnsFalseAndOnFailureCalled()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsTrue(false, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsTrue_WhenGivenTrue_ReturnsTrueAndOnSuccessCalled()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsTrue(true, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void NotInRange_ValueIsNotInRange_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
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
      public void NotInRange_WhenGivenNullArgs_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         var v0 = new IntHolder(1);
         var v1 = new IntHolder(5);
         IntHolder nv = null;
         Assert.Throws<ArgumentNullException>(() => Check.NotInRange(nv, v0, v1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
         Assert.Throws<ArgumentNullException>(() => Check.NotInRange(v0, nv, v1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
         Assert.Throws<ArgumentNullException>(() => Check.NotInRange(v1, v0, nv, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void NotInRange_WhenValueIsNotInRange_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.NotInRange(2, 1, 5, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Passes_T_WhenDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.Passes(null, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void Passes_T_WhenDelegateReturnsFalse_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(
            Check.Passes((v, s, f) => false, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Passes_T_WhenDelegateReturnsTrue_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.Passes((v, s, f) => true, 1, () =>
         {
            onSuccessCalled = true;
         },
         () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void Passes_WhenDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() =>
                                              Check.Passes(null, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void Passes_WhenDelegateReturnsFalse_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.Passes(() => false, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void Passes_WhenDelegateReturnsTrue_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.Passes(() => true, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void PassesAll_WhenAllDelegatesReturnTrue_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { TrueDelegate, TrueDelegate, TrueDelegate, TrueDelegate };
         Assert.True(Check.PassesAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void PassesAll_WhenDelegateListIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() => Check.PassesAll(null, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void PassesAll_WhenFirstDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { NullDelegate, TrueDelegate, TrueDelegate, TrueDelegate, TrueDelegate };
         Assert.Throws<ArgumentNullException>(() => Check.PassesAny(delegates, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void PassesAll_WhenOneDelegateReturnsFalse_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { TrueDelegate, TrueDelegate, FalseDelegate, TrueDelegate };
         Assert.False(Check.PassesAll(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void PassesAny_WhenAllDelegatesReturnFalse_ReturnsFalse()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { FalseDelegate, FalseDelegate };
         Assert.False(Check.PassesAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void PassesAny_WhenAllDelegatesReturnTrue_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { TrueDelegate, TrueDelegate, TrueDelegate, TrueDelegate };
         Assert.True(Check.PassesAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void PassesAny_WhenDelegateListIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.Throws<ArgumentNullException>(() => Check.PassesAny(null, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void PassesAny_WhenFirstDelegateIsNull_ThrowsArgumentNullException()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates =
         {NullDelegate, FalseDelegate, FalseDelegate, TrueDelegate, FalseDelegate};
         Assert.Throws<ArgumentNullException>(() => Check.PassesAny(delegates, 1, () => onSuccessCalled = true,
                                              () => onFailureCalled = true));
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been.");
      }

      [Fact]
      public void PassesAny_WhenOneDelegateReturnsTrue_ReturnsTrue()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Check.Signature<int>[] delegates = { FalseDelegate, FalseDelegate, TrueDelegate, FalseDelegate };
         Assert.True(Check.PassesAny(delegates, 1, () => onSuccessCalled = true, () => onFailureCalled = true));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Theory]
      [InlineData("")]
      [InlineData(null)]
      [InlineData("abc")]
      public void IsWhitespace_WhenGivenNonWhitespace_ReturnsFalseAndOnFailureCalled(string data)
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsWhitespace(data, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void IsWhitespace_WhenGivenWhitespace_ReturnsTrueAndOnSuccessCalled()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsWhitespace("   ", () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Theory]
      [InlineData("")]
      [InlineData(null)]
      [InlineData("abc")]
      public void IsNotWhitespace_WhenGivenNonWhitespace_ReturnsTrueAndOnSuccessCalled(string data)
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.True(Check.IsNotWhitespace(data, () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onSuccessCalled, "onSuccess was not called when it was expected to be called.");
         Assert.False(onFailureCalled, "onFailure was called when it shouldn't have been");
      }

      [Fact]
      public void IsNotWhitespace_WhenGivenWhitespace_ReturnsFalseAndOnFailureCalled()
      {
         var onFailureCalled = false;
         var onSuccessCalled = false;
         Assert.False(Check.IsNotWhitespace("   ", () =>
         {
            onSuccessCalled = true;
         }, () =>
         {
            onFailureCalled = true;
         }));
         Assert.True(onFailureCalled, "onFailure was not called when it was expected to be called.");
         Assert.False(onSuccessCalled, "onSuccess was called when it shouldn't have been");
      }

      [Fact]
      public void VariousMethods_WhenNoHandlersAreProvided_NoExceptionsAreThrown()
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
         Check.IsNotEmpty("");
         Check.IsNotEmpty(" ");
         Check.IsNotEmpty(null);
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
         Check.Passes((value, onSuccess, onFailure) => true, 1);
         Check.Passes((value, onSuccess, onFailure) => false, 1);
         Check.Fails((value, onSuccess, onFailure) => true, 1);
         Check.Fails((value, onSuccess, onFailure) => false, 1);
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
   }
}