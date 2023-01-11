using System;
using System.Collections.Generic;

namespace FiniteStateMachine
{
    //private StateContainer<State_Origin> StateContainer = new StateContainer<State_Origin>();
    //StateContainer.GetInstanceOfType<State_PlayerCombat>();
    [Serializable]
    public class StateContainer<T> where T : State_Origin<T> // Use if amount of state is becoming overwhelming in context class
    {
        List<T> m_states = new List<T>();

        public StateContainer()
        {
            //m_states.Add(new State_PlayerCombat() as T);
        }

        /// <summary>
        /// Will instantiate a new state if none is found for the type given
        /// May need to add more abstractions to state sub-origins so that specific enemy type can only get specific state
        /// </summary>
        public G GetInstanceOfType<G>() where G: State_Origin<T>
        {
            State_Origin<T> state = m_states.Find(x => x is G);
            if (state == null)
            {
                // If type is not castable as State_Origin, will throw a null
                state = Activator.CreateInstance(typeof(G)) as State_Origin<T>;
                if (state != null)
                {
                    m_states.Add(state as T);
                }
            }
            return state as G;
        }
    }

    /// <summary>
    /// Set and initialised on a per entity basis
    /// </summary>
    [Serializable]
    public class StateContainer { }
}

