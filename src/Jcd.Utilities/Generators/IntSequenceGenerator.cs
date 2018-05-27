using System;

namespace Jcd.Utilities.Generators
{
    public class IntSequenceGenerator : Generator<Int32SequenceState, int>
    {
        public IntSequenceGenerator(int from, int to, int by = 1)
            : base(new Int32SequenceState { current = from, stop = to, step = by }, (Int32SequenceState state, out bool @continue) =>
                {
                    var result = state.current;
                    state.current += state.step;
                    @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
                    return result;
                })
        {
        }
    }
}
