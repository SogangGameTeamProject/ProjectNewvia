using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

namespace Newvia
{
    public class SubWeaponeStateContext : MonoBehaviour
    {
        public SubWeaponeState CurrentState { get; private set; }
        private SubWeapone _subWeapone;

        // event to notify other objects of the state change
        public event Action<SubWeaponeState> stateChanged;

        public SubWeaponeStateContext(SubWeapone subWeapone)
        {
            _subWeapone = subWeapone;
        }

        // set the starting state
        public void Initialize(SubWeaponeState state)
        {
            CurrentState = state;
            state.Enter(_subWeapone);

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }

        // exit this state and enter another
        public void TransitionTo(SubWeaponeState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter(_subWeapone);

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
