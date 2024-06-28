namespace Newvia
{
    public enum CharacterStateType
    {
        Null = -1,
        //기본 상태
        Idle = 0, Move = 1, Hit = 2, Death = 3,
        //
        AttackSkill = 101,
        MovementSkill = 201,
        UtilitySkill = 301
    }
}

