using Jcd.Utilities.Validation;
using System;
using System.Collections.Generic;

namespace Jcd.Utilities.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            int i = 3333;
            var encoder = IntegerEncoders.Hexdecimal;
            Console.WriteLine($"{encoder.Format(i)} == {Convert.ToString(encoder.ParseInt32(encoder.Format(i)), encoder.Base)}");

            var foo = new List<int>();
            Check.IsEmpty(foo);

        }
    }
}
