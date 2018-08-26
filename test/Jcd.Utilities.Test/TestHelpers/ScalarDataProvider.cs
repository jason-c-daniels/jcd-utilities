using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace Jcd.Utilities.Test.TestHelpers
{
   /// <summary>
   /// An XUnit data provider. This one provides lists of numeric data of a various sorts.
   /// </summary>
   public class ScalarDataProvider
   {

      /// <summary>
      /// Provides a set of BigIntegers
      /// </summary>
      public static IEnumerable<object[]> AllScalars()
      {
         var biMax = new BigInteger(ulong.MaxValue) * 2;
         Uri.TryCreate("http://google.com", UriKind.Absolute, out Uri uri);
         yield return new[] { (object)typeof(int) };
         yield return new[] { (object)null };
         yield return new[] { (object)biMax };
         yield return new[] { (object)BindingFlags.CreateInstance };
         yield return new[] { (object)Byte.MaxValue };
         yield return new[] { (object)Int16.MaxValue };
         yield return new[] { (object)Int32.MaxValue };
         yield return new[] { (object)Int64.MaxValue };
         yield return new[] { (object)Byte.MaxValue };
         yield return new[] { (object)UInt16.MaxValue };
         yield return new[] { (object)UInt32.MaxValue };
         yield return new[] { (object)UInt64.MaxValue };
         yield return new[] { (object)DateTime.Now };
         yield return new[] { (object)DateTimeOffset.Now };
         yield return new[] { (object)TimeSpan.MaxValue };
         yield return new[] { (object)uri };
         yield return new[] { (object)Guid.Empty };
         yield return new[] { (object)"a string" };
      }

   }
}