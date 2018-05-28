using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jcd.Utilities.Generators
{
    public class Generator<TState, TResult> : IEnumerable<TResult>
    {
        public delegate TResult StateTransitionFunction(TState state, out bool @continue);
        protected TState state;
        protected StateTransitionFunction generator;
        public Generator(TState initial, StateTransitionFunction generator)
        {
            state = initial;
            this.generator = generator;
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            bool @continue;
            do
            {
                yield return generator(state, out @continue);
            } while (@continue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
