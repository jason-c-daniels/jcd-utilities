using Jcd.Utilities.Generators;
using System;

namespace Jcd.Utilities.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            int i = 3333;
            var encoder = IntegerEncoders.Hexdecimal;
            Console.WriteLine($"{encoder.Format(i)} == {Convert.ToString(encoder.ParseInt32(encoder.Format(i)), encoder.Base)}");
            foreach(var j in new IntSequenceGenerator(10,-20,-1))
            {
                Console.Write($"{j},");
            }
            Console.WriteLine();
            foreach(var k in new Generator<Int16SequenceState,short>(
                new Int16SequenceState { current = 100, stop = 0, step = -1 }, 
                (Int16SequenceState state, out bool @continue) =>
                {
                    var result = state.current;
                    state.current += state.step;
                    @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
                    return result;
                })
            )
            {
                Console.Write($"{k} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            var gen = new FibonacciGenerator(150000, 10);
            Console.WriteLine("Go!");
            foreach (var l in gen)
            {
                Console.Write("x");
            }
            Console.WriteLine();
        }
    }
}
