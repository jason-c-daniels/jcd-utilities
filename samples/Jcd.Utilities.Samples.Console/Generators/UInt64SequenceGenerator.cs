using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class UInt64SequenceGenerator : Generator<UInt64SequenceState, ulong>
   {
      public UInt64SequenceGenerator(ulong from, ulong to, ulong by = 1)
         : base(new UInt64SequenceState {current = from, stop = to, step = by},
                (UInt64SequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = state.step < 0 && state.current >= state.stop ||
                     state.step > 0 && state.current <= state.stop;
         return result;
      })
      {
      }

      public UInt64SequenceGenerator(UInt64SequenceState initialState, StateTransitionFunction stateTransition) :
         base(initialState,
              stateTransition)
      {
      }
   }
}