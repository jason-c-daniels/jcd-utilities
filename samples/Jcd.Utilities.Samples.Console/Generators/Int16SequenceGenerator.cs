using Jcd.Utilities.Generators;
using System;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class Int16SequenceGenerator : Generator<Int16SequenceState, Int16>
   {
      public Int16SequenceGenerator(Int16 from, Int16 to, Int16 by = 1)
         : base(new Int16SequenceState { current = from, stop = to, step = by }, (Int16SequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
         return result;
      })
      {
      }

      public Int16SequenceGenerator(Int16SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
               stateTransition)
      {
      }
   }
}