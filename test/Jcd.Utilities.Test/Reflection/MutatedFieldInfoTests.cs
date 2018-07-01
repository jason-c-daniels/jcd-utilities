using Jcd.Utilities.Reflection;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Jcd.Utilities.Test.Reflection
{
    public class MutatedFieldInfoTests
    {
      FieldInfo GetFieldInfo()
      {
         return typeof(TestClassB).EnumerateFields().First();
      }

      MutatedFieldInfo GetSut()
      {
         return new MutatedFieldInfo(GetFieldInfo());
      }

      /// <summary>
      /// Validate that Mutate Name ReturnsMutatedValue When MutatingName.
      /// </summary>
      [Fact]
      public void Mutate_WhenMutatingName_NameReturnsMutatedValue()
      {
         var expectedValue = "Harold";
         var sut = GetSut();
         sut.Mutate("Name", expectedValue);
         Assert.Equal(expectedValue, sut.Name);
      }

      /// <summary>
      /// Validate that Name Returns original value when no mutation has been performed.
      /// </summary>
      [Fact]
      public void Name_GetWhenNoMutation_NameReturnsOriginalValue()
      {
         var expectedValue = GetFieldInfo().Name;
         var sut = GetSut();
         Assert.Equal(expectedValue, sut.Name);
      }


      /// <summary>
      /// Validate that Mutate throws ArgumentException When PassedWrongDataType.
      /// </summary>
      [Fact]
      public void Mutate_WhenPassedWrongDataType_throwsArgumentException()
      {
         var sut = GetSut();
         Assert.Throws<ArgumentException>(() => sut.Mutate("Name", 25));
      }


   }
}
