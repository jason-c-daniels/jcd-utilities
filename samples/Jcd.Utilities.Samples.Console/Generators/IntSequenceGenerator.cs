using Jcd.Utilities.Generators;
using System;
using System.Numerics;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
    public class ByteSequenceGenerator : Generator<ByteSequenceState, Byte>
    {
        public ByteSequenceGenerator(Byte from, Byte to, Byte by = 1)
           : base(new ByteSequenceState { current = from, stop = to, step = by }, (ByteSequenceState state, out bool @continue) =>
           {
               var result = state.current;
               state.current += state.step;
               @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
               return result;
           })
        {
        }

        public ByteSequenceGenerator(ByteSequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
        {
        }
    }

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

    public class UInt64SequenceGenerator : Generator<UInt64SequenceState, UInt64>
    {
        public UInt64SequenceGenerator(UInt64 from, UInt64 to, UInt64 by = 1)
           : base(new UInt64SequenceState { current = from, stop = to, step = by }, (UInt64SequenceState state, out bool @continue) =>
           {
               var result = state.current;
               state.current += state.step;
               @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
               return result;
           })
        {
        }

        public UInt64SequenceGenerator(UInt64SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
        {
        }
    }

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

        public Int16SequenceGenerator(Int16SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
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

        public Int32SequenceGenerator(Int32SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
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

        public Int64SequenceGenerator(Int64SequenceState initialState, StateTransitionFunction stateTransition) : base(initialState,
                 stateTransition)
        {
        }
    }

    public class BigIntegerSequenceGenerator : Generator<BigIntegerSequenceState, BigInteger>
    {
        public BigIntegerSequenceGenerator(BigInteger from, BigInteger to) : this(from, to, BigInteger.One)
        {
        }

        public BigIntegerSequenceGenerator(BigInteger from, BigInteger to, BigInteger by)
           : base(new BigIntegerSequenceState { current = from, stop = to, step = by }, (BigIntegerSequenceState state,
                  out bool @continue) =>
           {
               var result = state.current;
               state.current += state.step;
               @continue = (state.step < 0 && state.current >= state.stop) || (state.step > 0 && state.current <= state.stop);
               return result;
           })
        {
        }

        public BigIntegerSequenceGenerator(BigIntegerSequenceState initialState,
                                           StateTransitionFunction stateTransition) : base(initialState, stateTransition)
        {
        }
    }
}