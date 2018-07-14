using System.Collections.Generic;
using System.Text;
using Xunit;
using Jcd.Utilities.Reflection;
using System.Linq;
using System.Reflection;
using System;

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

      /// <summary>
      /// Validate that ToPropertyInfoEnumeration returns PropertyInfo enumeration when given an object with public properties. 
      /// </summary>
      [Fact]
      public void ToPropertyInfoValuePairs_WhenGivenAnObjectWithPublicProperties_ReturnsPropertyInfoAndValueEnumeration()
      {
         var a = new TestClassA();
         var d=a.GetType().EnumerateProperties().ToPropertyInfoValuePairs(a).ToDictionary(k => k.Key.Name, v => v.Value);

         Assert.Equal(a.Prop1, d["Prop1"]);
         Assert.Equal(a.Prop2, d["Prop2"]);
      }

      /// <summary>
      /// Validate that ToPropertyInfoValuePairs Throws ArgumentNullException When item is null. 
      /// </summary>
      [Fact]
      public void ToPropertyInfoValuePairs_WhenItemIsNull_ThrowsArgumentNullException()
      {
         var a = new TestClassA();
         Assert.Throws<ArgumentNullException>(() => a.GetType().EnumerateProperties().ToPropertyInfoValuePairs(null).ToList());
      }

      /// <summary>
      /// Validate that ToPropertyInfoValuePairs Throws ArgumentNullException When items is null. 
      /// </summary>
      [Fact]
      public void ToPropertyInfoValuePairs_WhenItemsIsNull_ThrowsArgumentNullException()
      {
         var a = new TestClassA();
         Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.ToPropertyInfoValuePairs(null, a).ToList());
         Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.ToPropertyInfoValuePairs(null, null).ToList());
      }

      /// <summary>
      /// Validate that ToFieldInfoEnumeration returns FieldInfo enumeration when given an object with public properties. 
      /// </summary>
      [Fact]
      public void ToFieldInfoValuePairs_WhenGivenAnObjectWithPublicFields_ReturnsFieldInfoAndValueEnumeration()
      {
         var a = new TestClassA();
         var d = a.GetType().EnumerateFields().ToFieldInfoValuePairs(a).ToDictionary(k => k.Key.Name, v => v.Value);

         Assert.Equal(a.Field1, d["Field1"]);
         Assert.Equal(a.Field2, d["Field2"]);
      }

      /// <summary>
      /// Validate that ToFieldInfoValuePairs Throws ArgumentNullException When item is null. 
      /// </summary>
      [Fact]
      public void ToFieldInfoValuePairs_WhenItemIsNull_ThrowsArgumentNullException()
      {
         var a = new TestClassA();
         Assert.Throws<ArgumentNullException>(() => a.GetType().EnumerateFields().ToFieldInfoValuePairs(null).ToList());
      }

      /// <summary>
      /// Validate that ToFieldInfoValuePairs Throws ArgumentNullException When items is null. 
      /// </summary>
      [Fact]
      public void ToFieldInfoValuePairs_WhenItemsIsNull_ThrowsArgumentNullException()
      {
         var a = new TestClassA();
         Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.ToFieldInfoValuePairs(null, a).ToList());
         Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.ToFieldInfoValuePairs(null, null).ToList());
      }

      /// <summary>
      /// Validate that ToNameValuePairs Throws ArgumentNullException When items is null. 
      /// </summary>
      [Fact]
      public void ToNameValuePairs_WhenItemsIsNull_ThrowsArgumentNullException()
      {
         Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.ToNameValuePairs((IEnumerable<KeyValuePair<FieldInfo, object>>)null).ToList());
         Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.ToNameValuePairs((IEnumerable<KeyValuePair<PropertyInfo, object>>)null).ToList());
      }

      /// <summary>
      /// Validate that ToNameValuePairs returns IEnumerable of KeyValuePair of string and object when items is populated. 
      /// </summary>
      [Fact]
      public void ToNameValuePairs_WhenItemsIsPopulatedWithFields_ReturnsPopulatedKVPEnumeration()
      {
         var a = new TestClassB();
         var fields=typeof(TestClassB).EnumerateFields().ToFieldInfoValuePairs(a).ToNameValuePairs().ToDictionary(k=>k.Key,v=>v.Value);
         Assert.Equal(a.Field1, fields["Field1"]);
         Assert.Equal(a.Field2, fields["Field2"]);
         Assert.Equal(a.Field6, fields["Field6"]);
      }

      /// <summary>
      /// Validate that ToNameValuePairs returns IEnumerable of KeyValuePair of string and object when items is populated. 
      /// </summary>
      [Fact]
      public void ToNameValuePairs_WhenItemsIsPopulatedWithProperties_ReturnsPopulatedKVPEnumeration()
      {
         var a = new TestClassB();
         var props = typeof(TestClassB).EnumerateProperties().ToPropertyInfoValuePairs(a).ToNameValuePairs().ToDictionary(k => k.Key, v => v.Value);
         Assert.Equal(a.Field1, props["Prop1"]);
         Assert.Equal(a.Field2, props["Prop2"]);
      }

   }
}
