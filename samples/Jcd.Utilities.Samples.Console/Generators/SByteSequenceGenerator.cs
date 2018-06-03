using Jcd.Utilities.Generators;
using System;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
    public class SByteSequenceGenerator : Generator<SByteSequenceState, SByte>
    {
        public SByteSequenceGenerator(SByte from, SByte to, SByte by = 1)
           : base(new SByteSequenceState { current = from, stop = to, step = by }, (SByteSequenceState state, out bool @continue) =>
           {
               var result = state.current;
               state.current += state.step;
               @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
               return result;
           })
        {
        }

        public SByteSequenceGenerator(SByteSequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
        {
        }
    }
}