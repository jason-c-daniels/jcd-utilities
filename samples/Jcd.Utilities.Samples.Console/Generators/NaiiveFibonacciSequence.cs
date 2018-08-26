using System.Numerics;

using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class NaiiveFibonacciSequence : CaptureAndTransitionGenerator<NaiiveFibonacciSequence.State, BigInteger>
   {
      public NaiiveFibonacciSequence(int start, int count)
         : base(new State {count = count, n0 = 0, n1 = 1, nth = 0},
                (State state, out bool @continue) =>
                {
                   var t = state.n0;
                   state.n0 = state.n1;
                   state.n1 += t;
                   state.nth++;
                   @continue = state.nth < state.count;

                   return state.n1 - state.n0;
                })
      {
         // silently enumerate to the start.
         for (var i = 0; i < (start - 1); i++)
         {
            TransitionFunction(CurrentState, out var ignore);
         }

         // reset nth to 0 so we can keep simple logic in the state transition function
         CurrentState.nth = 0;
      }

      public class State
      {
         public int count;
         public BigInteger n0;
         public BigInteger n1;
         public int nth;
      }
   }
}
