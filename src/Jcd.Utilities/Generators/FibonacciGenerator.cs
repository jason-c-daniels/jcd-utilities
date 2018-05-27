using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Jcd.Utilities.Generators
{
    public class FibonacciState {
        public BigInteger n0;
        public BigInteger n1;
        public int nth;
        public int count;
    };
    public class FibonacciGenerator : Generator<FibonacciState, BigInteger>
    {
        public  FibonacciGenerator(int start, int count) : base(new FibonacciState { count=count,n0=0,n1=1,nth=0 }, 
            (FibonacciState state, out bool @continue) => {
                BigInteger t = state.n0;
                state.n0 = state.n1;
                state.n1 += t;
                state.nth++;
                @continue = state.nth < state.count;
                return state.n1-state.n0;
            })
        {
            for (var i=0; i < start-1; i++)
                generator(state, out bool ignore);
            state.nth = 0;
        }
    }
}
