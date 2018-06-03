using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class Int64SequenceGenerator : Generator<Int64SequenceState, long>
   {
      public Int64SequenceGenerator(long from, long to, long by = 1)
      : base(new Int64SequenceState {current = from, stop = to, step = by},
             (Int64SequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = (state.step < 0 && state.current >= state.stop) ||
                     (state.step > 0 && state.current <= state.stop);
         return result;
      })
      {
      }

      public Int64SequenceGenerator(Int64SequenceState initialState, StateTransitionFunction stateTransition) : base(
         initialState,
         stateTransition)
      {
      }
   }
}