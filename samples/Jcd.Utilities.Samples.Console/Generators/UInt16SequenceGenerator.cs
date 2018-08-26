using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class UInt16SequenceGenerator : CaptureAndTransitionGenerator<UInt16SequenceState, ushort>
   {
      public UInt16SequenceGenerator(ushort from, ushort to, ushort by = 1)
         : base(new UInt16SequenceState {current = from, stop = to, step = by},
                (UInt16SequenceState state, out bool @continue) =>
                {
                   var result = state.current;
                   state.current += state.step;

                   @continue = ((state.step < 0) && (state.current >= state.stop)) ||
                               ((state.step > 0) && (state.current <= state.stop));

                   return result;
                })
      {
      }

      public UInt16SequenceGenerator(UInt16SequenceState initialState, StateTransitionFunction stateTransition)
         :
         base(initialState,
              stateTransition)
      {
      }
   }
}
