using System.Collections.Generic;
using System.Text;
using Xunit;
using Jcd.Utilities.Reflection;
using System.Linq;
using System.Reflection;
using System;
using Jcd.Utilities.Test.TestHelpers;

namespace Jcd.Utilities.Test.Reflection
{
   public partial class ReflectionExtensionsTests
   {

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumeratePropertiesOnType_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceProperties()
      {
         var props = typeof(TestClassB).EnumerateProperties().ToList();
         Assert.Equal(3, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumeratePropertiesOnType_WhenCalledWithDefaultParameters_EnumeratesPublicInstancePropertiesAndSkipsIndicated()
      {
         var props = typeof(TestClassB).EnumerateProperties(skip: pi => pi.Name == "Prop1").ToList();
         Assert.Equal(2, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties Enumerates AllProperties When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumeratePropertiesOnType_WhenBindingsSetToReturnAll_EnumeratesAllProperties()
      {
         var props = typeof(TestClassB).EnumerateProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         Assert.Equal(7, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumeratePropertiesOnObject_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceProperties()
      {
         var props = new TestClassB().EnumerateProperties().ToList();
         Assert.Equal(3, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumeratePropertiesOnObject_WhenCalledWithDefaultParameters_EnumeratesPublicInstancePropertiesAndSkipsIndicated()
      {
         var props = new TestClassB().EnumerateProperties(skip: pi => pi.Name == "Prop1").ToList();
         Assert.Equal(2, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateProperties Enumerates AllProperties When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumeratePropertiesOnObject_WhenBindingsSetToReturnAll_EnumeratesAllProperties()
      {
         var props = new TestClassB().EnumerateProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         Assert.Equal(7, props.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnType_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFields()
      {
         var fields = typeof(TestClassB).EnumerateFields().ToList();
         Assert.Equal(3, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnType_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFieldsAndSkipsIndicated()
      {
         var fields = typeof(TestClassB).EnumerateFields(skip: pi => pi.Name == "Field1").ToList();
         Assert.Equal(2, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnType_WhenBindingsSetToReturnAll_EnumeratesAllFields()
      {
         var fields = typeof(TestClassB).EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         // 7 inheritable fields, plus 3 inheritable backing fields.
         Assert.Equal(10, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class, skip backign fields.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnType_WhenBindingsSetToReturnAll_EnumeratesAllFieldsExceptBacking()
      {
         var fields = typeof(TestClassB).EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, skip: (fi) => fi.Name[0] == '<').ToList();
         Assert.Equal(7, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties, inherited, when called with default parameters.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnObject_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFields()
      {
         var fields = new TestClassB().EnumerateFields().ToList();
         Assert.Equal(3, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields enumerates public instance properties and skips according to skip function.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnObject_WhenCalledWithDefaultParameters_EnumeratesPublicInstanceFieldsAndSkipsIndicated()
      {
         var fields = new TestClassB().EnumerateFields(skip: pi => pi.Name == "Field1").ToList();
         Assert.Equal(2, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnObject_WhenBindingsSetToReturnAll_EnumeratesAllFields()
      {
         var fields = new TestClassB().EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
         // 7 inheritable fields, plus 3 inheritable backing fields.
         Assert.Equal(10, fields.Count);
      }

      /// <summary>
      /// Validate that EnumerateFields Enumerates AllFields When BindingsSetToReturnAll, except private base class, skip backign fields.
      /// </summary>
      [Fact]
      public void EnumerateFieldsOnObject_WhenBindingsSetToReturnAll_EnumeratesAllFieldsExceptBacking()
      {
         var fields = new TestClassB().EnumerateFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, skip: (fi) => fi.Name[0] == '<').ToList();
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

      /// <summary>
      /// Validate that IsScalar returns true when type is scalar.
      /// </summary>
      [Theory]
      [MemberData(nameof(ScalarDataProvider.AllScalars), MemberType = typeof(ScalarDataProvider))]
      public void IsScalar_WhenTypeIsScalar_ReturnsTrue(object value)
      {
         Assert.True(value.IsScalar());
      }

      /// <summary>
      /// Validate that IsScalar returns false when type is not scalar.
      /// </summary>
      [Fact]
      public void IsScalar_WhenTypeIsNotScalar_ReturnsFalse()
      {
         Assert.False(new TestClassA().IsScalar());
      }

      /// <summary>
      /// Validate that IsScalar returns true when type is not scalar but exists in a custom hashset claiming its scalar.
      /// </summary>
      [Fact]
      public void IsScalar_WhenTypeNotIsScalarButExistsAsCustomScalar_ReturnsTrue()
      {
         Assert.True(new TestClassA().IsScalar(new HashSet<Type>(new[] { typeof(TestClassA) })));
         // Idiot testing to ensure we dibn't nuke the other scalar objects. Which might have been the case earlier. Okay it was the case.
         Assert.True(DateTime.Now.IsScalar(new HashSet<Type>(new[] { typeof(TestClassA) })));
      }

      /// <summary>
      /// Validate that IsKeyValuePair Returns False When object is not a KeyValuePair. 
      /// </summary>
      [Fact]
      public void IsKeyValuePair_WhenObjectIsNotAKeyValuePair_ReturnsFalse()
      {
         object kvp = new object();
         Assert.False(kvp.GetType().IsKeyValuePair());
      }

      /// <summary>
      /// Validate that IsKeyValuePair Returns False When object is not a KeyValuePair. 
      /// </summary>
      [Fact]
      public void IsKeyValuePair_WhenObjectIsAKeyValuePair_ReturnsTrue()
      {
         var kvp = new KeyValuePair<string, string>();
         var kvp2 = new { Key = "key", Value = "value", Pair = "Pear" };
         Assert.True(kvp.GetType().IsKeyValuePair());
         Assert.True(kvp2.GetType().IsKeyValuePair());
      }

      /// <summary>
      /// Validate that GetPropertyOrFieldValue Returns the value When object has property or field with the name. 
      /// </summary>
      [Fact]
      public void GetPropertyOrFieldValue_WhenObjectDoesntHavePropertyOrFieldWithTheName_ReturnsNull()
      {
         var kvp = new KeyValuePair<string, string>();
         var kvp2 = new { Key = "key", Value = "value", Pair = "Pear" };
         Assert.Null(kvp.GetPropertyOrFieldValue("Nada"));
      }

      /// <summary>
      /// Validate that GetPropertyOrFieldValue Returns the value When object has property or field with the name. 
      /// </summary>
      [Fact]
      public void GetPropertyOrFieldValue_WhenObjectHasPropertyOrFieldWithTheName_ReturnsValue()
      {
         var kvp = new { Key = "key", Value = "value", Pair = "Pear" };
         var val = kvp.GetPropertyOrFieldValue("Key");
         Assert.NotNull(val);
         Assert.Equal(kvp.Key,val);
      }
   }
}
