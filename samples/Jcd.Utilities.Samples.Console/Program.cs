using System;
using System.Diagnostics;
using System.Threading;

using Jcd.Utilities.Formatting;
using Jcd.Utilities.Generators;
using Jcd.Utilities.Samples.ConsoleApp.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp
{
   // ReSharper disable once ClassNeverInstantiated.Global
   internal class Program
   {
      private static void Main()
      {
         Console.WriteLine("Hello World!");
         var i = long.MinValue;
         var encoder = IntegerEncoders.Hexadecimal;
         var parsed = encoder.ParseInt64(encoder.Format(i));
         Console.WriteLine($"{encoder.Format(i)} == {Convert.ToString(parsed, encoder.Base)}");

         foreach (var j in new Int32SequenceGenerator(10, -20, -1)) { Console.Write($"{j},"); }

         Console.WriteLine();

         var numberGenerator = new
            CaptureAndTransitionGenerator<Int16SequenceState, short>(
                                                                     new Int16SequenceState
                                                                     {
                                                                        current = 100,
                                                                        stop = 0,
                                                                        step = -1
                                                                     },
                                                                     (Int16SequenceState state, out bool @continue) =>
                                                                     {
                                                                        var result = state.current;
                                                                        state.current += state.step;

                                                                        @continue =
                                                                           ((state.step < 0) &&
                                                                            (state.current >= state.stop)
                                                                           ) // handle counting down.
                                                                         ||
                                                                           ((state.step > 0) &&
                                                                            (state.current <=
                                                                             state.stop)); // handle counting up.

                                                                        return result;
                                                                     });

         foreach (var k in numberGenerator) { Console.Write($"{k} "); }

         Console.WriteLine();
         Console.WriteLine();
         var enc = IntegerEncoders.Base32Crockford;
         Console.WriteLine("Go!");

         foreach (var l in new NaiiveFibonacciSequence(1, 15000))
         {
            //Console.Write("x");
            var e = enc.Format(l);
            var d = enc.ParseBigInteger(e);

            if (l != d) Console.WriteLine($"ERROR: {l} -> {e} -> {d}");
         }

         Console.WriteLine();
         Console.WriteLine("Go! Go! Go!");
         var signedNum = int.MaxValue.ToString();
         var sw = new Stopwatch();
         sw.Reset();
         const int iterations = 10000;

         for (var q = 0; q < iterations; q++)
         {
            sw.Start();
            parsed=int.Parse(signedNum);
            sw.Stop();
            Thread.Sleep(0);
            var ix = parsed;
         }

         var ip = sw.ElapsedMilliseconds;
         sw.Reset();

         for (var q = 0; q < iterations; q++)
         {
            sw.Start();
            IntegerEncoders.Decimal.ParseInt32(signedNum);
            sw.Stop();
            Thread.Sleep(0);
         }

         var elapsedDecimalParse = sw.ElapsedMilliseconds;
         Console.WriteLine($"{signedNum} : int.Parse: {ip}   Decimal.ParseInt32: {elapsedDecimalParse}");
         Console.ReadKey();
      }
   }
}
