using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Jcd.Utilities.Reflection;
using System.Linq;
using System.Reflection;

namespace Jcd.Utilities.Test.Reflection
{
   public class ReflectionExtensionsTests
   {

      class ClassA
      {
         public int Prop1 { get; set; } = 5;
         public string Prop2 { get; set; } = "hello";
         protected string Prop3 { get; set; } = "hello1";
         static string Prop4 { get; set; } = "hello2";
         public static string Prop5 { get; set; } = "hello3";
         public int Field1 = 5;
         public string Field2 = "hello";
         protected string Field3 = "hello1";
         static string Field4 = "hello2";
         public static string Field5 = "hello3";
      }
      class ClassB : ClassA
      {
         public DateTime Prop6 { get; set; } = DateTime.Now;
         protected static string Prop7 { get; set; } = "hello4";
         static string Prop8 { get; set; } = "hello5";
         public DateTime Field6 = DateTime.Now;
         protected static string Field7 = "hello4";
         static string Field8 = "hello5";
      }

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumerateProperties_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceProperties()
      {
         var props = typeof(ClassB).EnumerateProperties().ToList();
         Assert.Equal(3, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumerateProperties_WhenCalledWithDefaultParameters_EnumeratesPublicInstancePropertiesAndSkipsIndicated()
      {
         var props = typeof(ClassB).EnumerateProperties(skip: pi => pi.Name == "Prop1").ToList();
         Assert.Equal(2, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties Enumerates AllProperties When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumerateProperties_WhenBindingsSetToReturnAll_EnumeratesAllProperties()
      {
         var props = typeof(ClassB).EnumerateProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         Assert.Equal(7, props.Count);
      }
      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFields()
      {
         var fields = typeof(ClassB).EnumerateFields().ToList();
         Assert.Equal(3, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFieldsAndSkipsIndicated()
      {
         var fields = typeof(ClassB).EnumerateFields(skip: pi => pi.Name == "Field1").ToList();
         Assert.Equal(2, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenBindingsSetToReturnAll_EnumeratesAllFields()
      {
         var fields = typeof(ClassB).EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         // 7 inheritable fields, plus 3 inheritable backing fields.
         Assert.Equal(10, fields.Count);
      }
      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class, skip backign fields.
      /// </summary>
      [Fact]
      public void EnumerateFields_WhenBindingsSetToReturnAll_EnumeratesAllFieldsExceptBacking()
      {
         var fields = typeof(ClassB).EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, skip: (fi) => fi.Name[0] == '<').ToList();
         Assert.Equal(7, fields.Count);
      }
   }
}
