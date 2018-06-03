using System.Collections;
using System.Collections.Generic;

namespace Jcd.Utilities.Generators
{
    /// <summary>
    /// A base class to help with implementing state-transition based enumeration.
    /// </summary>
    /// <typeparam name="TState">The type of the state data</typeparam>
    /// <typeparam name="TResult">The type of the transition result data.</typeparam>
    public class Generator<TState, TResult> : IEnumerable<TResult>
       where TState : class
    {
        #region Protected Fields

        protected TState state;

        protected StateTransitionFunction transitionFunction;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a state transition based IEnumerable data generator.
        /// </summary>
        /// <param name="initial">The initial state.</param>
        /// <param name="transitionFunction">The state transition function.</param>
        public Generator(TState initial, StateTransitionFunction transitionFunction)
        {
            state = initial;
            this.transitionFunction = transitionFunction;
        }

        #endregion Public Constructors

        #region Public Delegates

        /// <summary>
        /// The state transition function signature.
        /// </summary>
        /// <param name="state">The data to manipulate</param>
        /// <param name="continue">A flag indicating if there are more states to transition to.</param>
        /// <returns>The result of the state transition operation</returns>
        public delegate TResult StateTransitionFunction(TState state, out bool @continue);

        #endregion Public Delegates

        #region Public Methods

        /// <summary>
        /// Retrieves an enmerator that yields data from calling transitionFunction. This is
        /// guaranteed to be called once for the initial state.
        /// </summary>
        /// <returns>The result of transitionFunction(state, out @continue)</returns>
        public IEnumerator<TResult> GetEnumerator()
        {
            bool @continue;

            do
            {
                yield return transitionFunction(state, out @continue);
            } while (@continue);
        }

        /// <summary>
        /// Retrieves an enmerator that yields data from calling transitionFunction(state, out
        /// @continue). This is guaranteed to be called once for the initial state.
        /// </summary>
        /// <returns>The result of transitionFunction(state, out @continue)</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Public Methods
    }
}