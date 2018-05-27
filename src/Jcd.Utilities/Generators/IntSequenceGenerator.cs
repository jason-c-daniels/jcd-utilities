using System;
using System.Numerics;

namespace Jcd.Utilities.Generators
{
    public class Int16SequenceGenerator : Generator<Int16SequenceState, Int16>
    {
        public Int16SequenceGenerator(Int16 from, Int16 to, Int16 by = 1)
            : base(new Int16SequenceState { current = from, stop = to, step = by }, (Int16SequenceState state, out bool @continue) =>
            {
                var result = state.current;
                state.current += state.step;
                @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
                return result;
            })
        {
        }
    }

    public class Int32SequenceGenerator : Generator<Int32SequenceState, Int32>
    {
        public Int32SequenceGenerator(Int32 from, Int32 to, Int32 by = 1)
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

    public class Int64SequenceGenerator : Generator<Int64SequenceState, Int64>
    {
        public Int64SequenceGenerator(Int64 from, Int64 to, Int64 by = 1)
            : base(new Int64SequenceState { current = from, stop = to, step = by }, (Int64SequenceState state, out bool @continue) =>
            {
                var result = state.current;
                state.current += state.step;
                @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
                return result;
            })
        {
        }
    }

    public class BigIntegerSequenceGenerator : Generator<BigIntegerSequenceState, BigInteger>
    {
        public BigIntegerSequenceGenerator(BigInteger from, BigInteger to, BigInteger by)
            : base(new BigIntegerSequenceState { current = from, stop = to, step = by }, (BigIntegerSequenceState state, out bool @continue) =>
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
