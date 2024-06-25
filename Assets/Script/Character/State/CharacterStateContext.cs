using System;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;

namespace Newvia
{
    public class CharacterStateContext
    {
        public CharacterState CurrentState { get; private set; }

        // reference to the state objects
        public PlayerWalkState walkState;
        //public IdleState idleState;

        // event to notify other objects of the state change
        public event Action<CharacterState> stateChanged;

        // pass in necessary parameters into constructor 
        public CharacterStateContext(PlayerController player)
        {
            // create an instance for each state and pass in PlayerController
            this.walkState = new PlayerWalkState(player);
            //this.idleState = new IdleState(player);
        }

        // set the starting state
        public void Initialize(CharacterState state)
        {
            CurrentState = state;
            state.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }

        // exit this state and enter another
        public void TransitionTo(CharacterState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
        }

        // allow the StateMachine to update this state
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }
}