#pragma warning disable 414
namespace Jcd.Utilities.Test.Reflection
{
   class TestClassA
   {
      public int Prop1 { get; set; } = 5;
      public string Prop2 { get; set; } = "hello";
      public int Field1 = 5;
      public readonly string Field2 = "hello";

      // ReSharper disable once UnusedMember.Global
      public static string Prop5 { get; set; } = "hello3";
      public static string Field5 = "hello3";

      protected string Prop3 { get; set; } = "hello1";
      protected string Field3 = "hello1";

      // ReSharper disable once ArrangeTypeMemberModifiers
      // ReSharper disable once UnusedMember.Local
      static string Prop4 { get; set; } = "hello2";
      // ReSharper disable once ArrangeTypeMemberModifiers
      // ReSharper disable once InconsistentNaming
      static string Field4 = "hello2";
   }
}
