namespace Jcd.Utilities.Test.Reflection
{
   class TestClassA
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
}
