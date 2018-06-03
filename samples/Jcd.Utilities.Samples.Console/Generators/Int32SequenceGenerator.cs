using Jcd.Utilities.Generators;
using System;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class Int32SequenceGenerator : Generator<Int32SequenceState, Int32>
   {
      public Int32SequenceGenerator(Int32 from, Int32 to, Int32 by = 1)
         : base(new Int32SequenceState { current = from, stop = to, step = by }, (Int32SequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
         return result;
      })
      {
      }

      public Int32SequenceGenerator(Int32SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
               stateTransition)
      {
      }
   }
}