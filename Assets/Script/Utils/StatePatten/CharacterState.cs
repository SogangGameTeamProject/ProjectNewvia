namespace Newvia
{
    public interface CharacterState
    {
        void Handle(CharacterInit character, params object[] datas);
    }
}