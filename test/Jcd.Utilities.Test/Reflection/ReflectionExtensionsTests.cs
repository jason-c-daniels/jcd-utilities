using System.Collections.Generic;
using System.Text;
using Xunit;
using Jcd.Utilities.Reflection;
using System.Linq;
using System.Reflection;

namespace Jcd.Utilities.Test.Reflection
{
   public partial class ReflectionExtensionsTests
   {

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumerateProperties_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceProperties()
      {
         var props = typeof(TestClassB).EnumerateProperties().ToList();
         Assert.Equal(3, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumerateProperties_WhenCalledWithDefaultParameters_EnumeratesPublicInstancePropertiesAndSkipsIndicated()
      {
         var props = typeof(TestClassB).EnumerateProperties(skip: pi => pi.Name == "Prop1").ToList();
         Assert.Equal(2, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties Enumerates AllProperties When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumerateProperties_WhenBindingsSetToReturnAll_EnumeratesAllProperties()
      {
         var props = typeof(TestClassB).EnumerateProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         Assert.Equal(7, props.Count);
      }
      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFields()
      {
         var fields = typeof(TestClassB).EnumerateFields().ToList();
         Assert.Equal(3, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFieldsAndSkipsIndicated()
      {
         var fields = typeof(TestClassB).EnumerateFields(skip: pi => pi.Name == "Field1").ToList();
         Assert.Equal(2, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenBindingsSetToReturnAll_EnumeratesAllFields()
      {
         var fields = typeof(TestClassB).EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         // 7 inheritable fields, plus 3 inheritable backing fields.
         Assert.Equal(10, fields.Count);
      }
      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class, skip backign fields.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenBindingsSetToReturnAll_EnumeratesAllFieldsExceptBacking()
      {
         var fields = typeof(TestClassB).EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, skip: (fi) => fi.Name[0] == '<').ToList();
         Assert.Equal(7, fields.Count);
      }
   }
}
