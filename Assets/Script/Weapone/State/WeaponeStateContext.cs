using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

namespace Newvia
{
    public class WeaponeStateContext: MonoBehaviour
    {
        public WeaponeState CurrentState { get; private set; }
        private Weapone _weapone;

        // event to notify other objects of the state change
        public event Action<WeaponeState> stateChanged;

        public WeaponeStateContext(Weapone weapone)
        {
            _weapone = weapone;
        }

        // set the starting state
        public void Initialize(WeaponeState state)
        {
            CurrentState = state;
            state.Enter(_weapone);

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }

        // exit this state and enter another
        public void TransitionTo(WeaponeState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter(_weapone);

            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
            
        }

        // allow the StateMachine to update this state
        public void StateUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.StateUpdate();
            }
        }
    }
}