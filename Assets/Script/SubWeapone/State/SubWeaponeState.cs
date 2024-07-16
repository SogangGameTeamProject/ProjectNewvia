namespace Newvia
{
    public interface SubWeaponeState
    {
        public void Enter(SubWeapone subWeapone)
        {
            // code that runs when we first enter the state
        }

        public void StateUpdate()
        {
            // per-frame logic, include condition to transition to a new state
        }

        public void Exit()
        {
            // code that runs when we exit the state
        }
    }

}
