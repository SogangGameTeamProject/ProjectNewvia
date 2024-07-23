using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public abstract class SkillStateBase : CharacterStateBase
    {
        [SerializeField]
        protected float firstDealDuration = 0.25f; // 선딜
        [SerializeField]
        protected float lastDealDuration = 0.25f;  // 후딜
        [SerializeField]
        protected float skillDuration = 1.0f;     // 스킬 지속 시간

        protected float elapsedTime = 0.0f;

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            elapsedTime = 0.0f;
        }

        public override void Exit()
        {
            OnSkillEnd();
        }

        public override void StateUpdate()
        {

            elapsedTime += Time.deltaTime;

            // 선딜 처리
            if (elapsedTime < firstDealDuration)
            {
                // 선딜 중 처리
                HandleFirstDeal();
            }
            // 스킬 처리
            else if (elapsedTime < firstDealDuration + skillDuration)
            {
                // 스킬 중 처리
                HandleSkill();
            }
            // 후딜 처리
            else if (elapsedTime < firstDealDuration + skillDuration + lastDealDuration)
            {
                // 후딜 중 처리
                HandleLastDeal();
            }
            // 스킬 종료
            else
            {
                _character.StateTransition(CharacterStateType.Idle);
            }
        }

        //스킬 선딜 부분
        protected abstract void HandleFirstDeal();

        //스킬 사용 부분
        protected abstract void HandleSkill();

        //스킬 후딜 부분
        protected abstract void HandleLastDeal();

        //스킬 종료 시 처리
        protected abstract void OnSkillEnd();
    }
}
