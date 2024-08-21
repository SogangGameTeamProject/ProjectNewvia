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
        [SerializeField]
        protected CharacterStateType chageState = CharacterStateType.Null;//상태 종료 시 변경할 상태
        protected float elapsedTime = 0.0f;

        [SerializeField]
        private string HandleSkilAniPara = null;
        private bool playHandleSkillAni = false;
        [SerializeField]
        private string HandleLastDealAniPara = null;
        private bool playHandleLastDeal = false;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            elapsedTime = 0.0f;
            playHandleSkillAni = false;
            playHandleLastDeal = false;
        }

        public override void Exit()
        {
            OnSkillEnd();
            base.Exit();
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
                if (!playHandleSkillAni && HandleSkilAniPara != string.Empty)
                {
                    _animator.SetTrigger(HandleSkilAniPara);
                    playHandleSkillAni = true;
                }
                // 스킬 중 처리
                HandleSkill();
            }
            // 후딜 처리
            else if (elapsedTime < firstDealDuration + skillDuration + lastDealDuration)
            {
                if (!playHandleLastDeal && HandleLastDealAniPara != string.Empty)
                {
                    _animator.SetTrigger(HandleLastDealAniPara);
                    playHandleLastDeal = true;
                }
                // 후딜 중 처리
                HandleLastDeal();
            }
            // 스킬 종료
            else
            {
                _character.StateTransition(chageState);
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
