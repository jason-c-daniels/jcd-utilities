using System.Numerics;

using Jcd.Utilities.Generators;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
   public class BigIntegerSequenceGenerator : CaptureAndTransitionGenerator<BigIntegerSequenceState, BigInteger>
   {
      public BigIntegerSequenceGenerator(BigInteger from, BigInteger to)
         : this(from, to, BigInteger.One)
      {
      }

      public BigIntegerSequenceGenerator(BigInteger from, BigInteger to, BigInteger by)
         : base(new BigIntegerSequenceState {current = from, stop = to, step = by},
                (BigIntegerSequenceState state,
                 out bool @continue) =>
                {
                   var result = state.current;
                   state.current += state.step;

                   @continue = ((state.step < 0) && (state.current >= state.stop)) ||
                               ((state.step > 0) && (state.current <= state.stop));

                   return result;
                })
      {
      }

      public BigIntegerSequenceGenerator(BigIntegerSequenceState initialState,
                                         StateTransitionFunction stateTransition)
         : base(initialState, stateTransition)
      {
      }
   }
}
