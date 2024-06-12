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

        //���� ���� ������Ʈ
        public void Transition()
        {
            CurrentState.Handle(characterInit);
        }

        //���� ��ȯ
        public void Transition(CharacterState state, params object[] datas)
        {
            CurrentState = state;
            CurrentState.Handle(characterInit, datas);
        }
    }
}