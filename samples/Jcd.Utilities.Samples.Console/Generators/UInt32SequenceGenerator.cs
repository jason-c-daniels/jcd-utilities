using Jcd.Utilities.Generators;
using System;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
    public class UInt32SequenceGenerator : Generator<UInt32SequenceState, UInt32>
    {
        public UInt32SequenceGenerator(UInt32 from, UInt32 to, UInt32 by = 1)
           : base(new UInt32SequenceState { current = from, stop = to, step = by }, (UInt32SequenceState state, out bool @continue) =>
           {
               var result = state.current;
               state.current += state.step;
               @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
               return result;
           })
        {
        }

        public UInt32SequenceGenerator(UInt32SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
        {
        }
    }
}