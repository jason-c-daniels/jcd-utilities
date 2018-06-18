using Jcd.Utilities.Generators;
using System.Numerics;

namespace Jcd.Utilities.Test.TestHelpers
{
   /// <summary>
   /// Generates all Fibonacci numbers equal to or greter than the minValue
   /// </summary>
   public class NegativeNaiiveFibonacciGenerator :
      CaptureAndTransitionGenerator<NegativeNaiiveFibonacciGenerator.State, BigInteger>
   {
      public NegativeNaiiveFibonacciGenerator(BigInteger minValue) : base(new State { n0 = 0, n1 = -1, nth = 0 },
            (State state, out bool @continue) =>
      {
         var t = state.n0;
         state.n0 = state.n1;
         state.n1 += t;
         state.nth++;
         @continue = state.n0 >= minValue;
         return state.n1 - state.n0;
      })
      {
         state.nth = 0;
      }

      public class State
      {
         public BigInteger n0;
         public BigInteger n1;
         public int nth;
      }
   }
}
