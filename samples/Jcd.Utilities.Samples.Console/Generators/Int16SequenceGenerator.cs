using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class Int16SequenceGenerator : Generator<Int16SequenceState, short>
   {
      public Int16SequenceGenerator(short from, short to, short by = 1)
         : base(new Int16SequenceState {current = from, stop = to, step = by},
                (Int16SequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = state.step < 0 && state.current >= state.stop ||
                     state.step > 0 && state.current <= state.stop;
         return result;
      })
      {
      }

      public Int16SequenceGenerator(Int16SequenceState initialState, StateTransitionFunction stateTransition) : base(
            initialState,
            stateTransition)
      {
      }
   }
}