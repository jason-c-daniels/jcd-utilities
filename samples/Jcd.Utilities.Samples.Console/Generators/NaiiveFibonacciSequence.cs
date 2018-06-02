using Jcd.Utilities.Generators;
using System.Numerics;

namespace Jcd.Utilities.Samples.ConsoleApp.Generators
{
    public class NaiiveFibonacciSequence : Generator<NaiiveFibonacciSequence.State, BigInteger>
    {
        public class State
        {
            public BigInteger n0;
            public BigInteger n1;
            public int nth;
            public int count;
        }

        public NaiiveFibonacciSequence(int start, int count) : base(new State { count = count, n0 = 0, n1 = 1, nth = 0 },
                 (State state, out bool @continue) =>
                 {
                     BigInteger t = state.n0;
                     state.n0 = state.n1;
                     state.n1 += t;
                     state.nth++;
                     @continue = state.nth < state.count;
                     return state.n1 - state.n0;
                 })
        {
            // silently enumerate to the start.
            for (var i = 0; i < start - 1; i++)
            {
                transitionFunction(state, out bool ignore);
            }

            // reset nth to 0 so we can keep simple logic in the state transition function
            state.nth = 0;
        }
    }
}