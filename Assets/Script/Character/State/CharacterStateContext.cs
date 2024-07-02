using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

namespace Newvia
{
    public class CharacterStateContext
    {
        public CharacterState CurrentState { get; private set; }
        private CharacterInit _character;

        // event to notify other objects of the state change
        public event Action<CharacterState> stateChanged;


        public CharacterStateContext(CharacterInit character)
        {
            _character = character;
        }

        // set the starting state
        public void Initialize(CharacterState state)
        {
            CurrentState = state;
            state.Enter(_character);

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }

        // exit this state and enter another
        public void TransitionTo(CharacterState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter(_character);

            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
        }
        
        public void StateUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.StateUpdate();
            }
        }
    }
}