using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class Int32SequenceGenerator : Generator<Int32SequenceState, int>
   {
      public Int32SequenceGenerator(int from, int to, int by = 1)
         : base(new Int32SequenceState {current = from, stop = to, step = by},
                (Int32SequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = state.step < 0 && state.current >= state.stop ||
                     state.step > 0 && state.current <= state.stop;
         return result;
      })
      {
      }

      public Int32SequenceGenerator(Int32SequenceState initialState, StateTransitionFunction stateTransition) : base(
            initialState,
            stateTransition)
      {
      }
   }
}