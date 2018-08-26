using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class UInt32SequenceGenerator : CaptureAndTransitionGenerator<UInt32SequenceState, uint>
   {
      public UInt32SequenceGenerator(uint from, uint to, uint by = 1)
         : base(new UInt32SequenceState {current = from, stop = to, step = by},
                (UInt32SequenceState state, out bool @continue) =>
                {
                   var result = state.current;
                   state.current += state.step;

                   @continue = ((state.step < 0) && (state.current >= state.stop)) ||
                               ((state.step > 0) && (state.current <= state.stop));

                   return result;
                })
      {
      }

      public UInt32SequenceGenerator(UInt32SequenceState initialState, StateTransitionFunction stateTransition)
         :
         base(initialState,
              stateTransition)
      {
      }
   }
}
