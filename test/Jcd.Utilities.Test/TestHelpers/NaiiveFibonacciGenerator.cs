using System.Numerics;
using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Test.TestHelpers
{
   /// <summary>
   ///    Generates all Fibonacci numbers equal to or less than the maxValue
   /// </summary>
   public class NaiiveFibonacciGenerator : CaptureAndTransitionGenerator<NaiiveFibonacciGenerator.State, BigInteger>
   {
      public NaiiveFibonacciGenerator(BigInteger maxValue)
         : base(new State {n0 = 0, n1 = 1, nth = 0},
                (State state, out bool @continue) =>
                {
                   var t = state.n0;
                   state.n0 = state.n1;
                   state.n1 += t;
                   state.nth++;
                   @continue = state.n0 <= maxValue;

                   return state.n1 - state.n0;
                })
      {
         CurrentState.nth = 0;
      }

      public class State
      {
         // ReSharper disable once InconsistentNaming
         public BigInteger n0;

         // ReSharper disable once InconsistentNaming
         public BigInteger n1;

         // ReSharper disable once InconsistentNaming
         // ReSharper disable once NotAccessedField.Global
         public int nth;
      }
   }
}
