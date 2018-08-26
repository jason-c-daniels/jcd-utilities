using Jcd.Utilities.Reflection;

using System.Linq;
using System.Reflection;

using Xunit;

namespace Jcd.Utilities.Test.Reflection
{
    public class FieldOrPropertyEnumeratorTests
    {
      /// <summary>
      /// Validate that Enumerate Enumerates AllPublicFieldsAndProperties When GivenAnObjectWithFieldsAndProperties.
      /// </summary>
      [Fact]
      public void Enumerate_WhenGivenAnObjectWithFieldsAndProperties_EnumeratesAllPublicFieldsAndProperties()
      {
         var obj = new TestClassB();
         var sut = new FieldOrPropertyEnumerator(obj);
         var list = sut.ToList();
         Assert.Equal(6, list.Count);
      }

      /// <summary>
      /// Validate that Enumerate Enumerates AllPublicFieldsAndProperties When GivenAnObjectWithFieldsAndProperties.
      /// </summary>
      [Fact]
      public void Enumerate_WhenGivenAnObjectWithFieldsAndPropertiesAndBingingFlags_EnumeratesAllSpecifiedFieldsAndProperties()
      {
         var obj = new TestClassB();
         var sut = new FieldOrPropertyEnumerator(obj, new FieldOrPropertyEnumerator.Settings {Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public });
         var list = sut.ToList();
         Assert.Equal(15, list.Count);
      }
   }
}
