namespace Newvia
{
    public class CharacterStateContext
    {
        public CharacterState CurrentState
        {
            get; set;
        }

        private readonly CharacterInit characterInit;


        public CharacterStateContext(CharacterInit characterInit)
        {
            this.characterInit = characterInit;
        }

        //현재 상태 업데이트
        public void Transition()
        {
            CurrentState.Handle(characterInit);
        }

        //상태 전환
        public void Transition(CharacterState state)
        {
            CurrentState = state;
            CurrentState.Handle(characterInit);
        }
    }
}