using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class ByteSequenceGenerator : CaptureAndTransitionGenerator<ByteSequenceState, byte>
   {
      public ByteSequenceGenerator(byte from, byte to, byte by = 1)
      : base(new ByteSequenceState {current = from, stop = to, step = by},
             (ByteSequenceState state, out bool @continue) =>
      {
         var result = state.current;
         state.current += state.step;
         @continue = (state.step < 0 && state.current >= state.stop) ||
                     (state.step > 0 && state.current <= state.stop);
         return result;
      })
      {
      }

      public ByteSequenceGenerator(ByteSequenceState initialState, StateTransitionFunction stateTransition) : base(
         initialState,
         stateTransition)
      {
      }
   }
}