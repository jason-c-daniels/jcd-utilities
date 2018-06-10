using Jcd.Utilities.Generators;
using Jcd.Utilities.Samples.ConsoleApp.Generators;
using System;
using System.Diagnostics;
using System.Threading;

namespace Jcd.Utilities.Samples.ConsoleApp
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");
         var i = long.MinValue;
         var encoder = IntegerEncoders.Hexdecimal;
         var parsed = encoder.ParseInt64(encoder.Format(i));
         Console.WriteLine($"{encoder.Format(i)} == {Convert.ToString(parsed, encoder.Base)}");

         foreach (var j in new Int32SequenceGenerator(10, -20, -1))
         {
            Console.Write($"{j},");
         }

         Console.WriteLine();
         CaptureAndTransitionGenerator<Int16SequenceState, short> numberGenerator = new
         CaptureAndTransitionGenerator<Int16SequenceState, short>(
            new Int16SequenceState { current = 100, stop = 0, step = -1 },
            (Int16SequenceState state, out bool @continue) =>
         {
            var result = state.current;
            state.current += state.step;
            @continue = (state.step < 0 && state.current >= state.stop) // handle counting down.
                        || (state.step > 0 && state.current <= state.stop); // handle counting up.
            return result;
         });

         foreach (var k in numberGenerator)
         {
            Console.Write($"{k} ");
         }

         Console.WriteLine();
         Console.WriteLine();
         var enc = IntegerEncoders.Base32_Crockford;
         Console.WriteLine("Go!");

         foreach (var l in new NaiiveFibonacciSequence(1, 15000))
         {
            //Console.Write("x");
            var e = enc.Format(l);
            var d = enc.ParseBigInteger(e);

            if (l != d)
            {
               Console.WriteLine($"ERROR: {l} -> {e} -> {d}");
            }
         }

         Console.WriteLine();
         Console.WriteLine("Go! Go! Go!");
         var snum = int.MaxValue.ToString();
         var sw = new Stopwatch();
         sw.Reset();
         const int iter = 10000;

         for (var q = 0; q < iter; q++)
         {
            sw.Start();
            var z = int.Parse(snum);
            sw.Stop();
            Thread.Sleep(0);
         }

         var ip = sw.ElapsedMilliseconds;
         sw.Reset();

         for (var q = 0; q < iter; q++)
         {
            sw.Start();
            var z = IntegerEncoders.Decimal.ParseInt32(snum);
            sw.Stop();
            Thread.Sleep(0);
         }

         var iedp = sw.ElapsedMilliseconds;
         Console.WriteLine($"{snum} : int.Parse: {ip}   Decimal.ParseInt32: {iedp}");
         Console.ReadKey();
      }
   }
}