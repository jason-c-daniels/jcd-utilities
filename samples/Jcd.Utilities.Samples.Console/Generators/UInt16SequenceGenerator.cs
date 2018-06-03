using Jcd.Utilities.Generators;
using System;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
    public class UInt16SequenceGenerator : Generator<UInt16SequenceState, UInt16>
    {
        public UInt16SequenceGenerator(UInt16 from, UInt16 to, UInt16 by = 1)
           : base(new UInt16SequenceState { current = from, stop = to, step = by }, (UInt16SequenceState state, out bool @continue) =>
           {
               var result = state.current;
               state.current += state.step;
               @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
               return result;
           })
        {
        }

        public UInt16SequenceGenerator(UInt16SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
        {
        }
    }
}