namespace Newvia
{
    public enum CharacterStateType
    {
        Null = -1,
        //기본 상태
        Idle = 0, Move = 1, Hit = 2, Death = 3, Appeared = 4,
        //101 ~ 399 스킬 상태
        //101 ~ 199 공격 스킬 상태
        AttackSkill = 101, AttackSkill02 = 102,
        //201 ~ 299 이동 스킬 상태
        MovementSkill = 201,
        //301 ~ 399 유틸 스킬 상태
        UtilitySkill = 301
    }
}

