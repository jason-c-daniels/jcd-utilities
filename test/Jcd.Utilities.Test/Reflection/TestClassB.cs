using System;
#pragma warning disable 414

namespace Jcd.Utilities.Test.Reflection
{
   class TestClassB : TestClassA
   {
      public DateTime Prop6 { get; set; } = DateTime.Now;
      protected static string Prop7 { get; set; } = "hello4";
      // ReSharper disable once ArrangeTypeMemberModifiers
      // ReSharper disable once UnusedMember.Local
      static string Prop8 { get; set; } = "hello5";
      public DateTime Field6 = DateTime.Now;
      protected static string Field7 = "hello7";
      // ReSharper disable once ArrangeTypeMemberModifiers
      // ReSharper disable once InconsistentNaming
      static string Field8 = "hello8";
   }
}
