using System;

namespace Jcd.Utilities.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            int i = 3333;
            var encoder = NumericEncoders.Quaternary;
            Console.WriteLine($"{encoder.ToString(i)} == {Convert.ToString(encoder.ToInt32(encoder.ToString(i)), encoder.Base)}");
        }
    }
}
