using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class FinishAttackState : SkillStateBase
    {
        public bool isChaging = false;

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            isChaging = false;
        }

        protected override void HandleFirstDeal()
        {
            
        }

        protected override void HandleSkill()
        {
            EndCutScene();//컷씬 종료

        }

        protected override void HandleLastDeal()
        {
            if (!isChaging)
                isChaging = true;

            StartCutScene();//컷씬 시작

        }

        protected override void OnSkillEnd()
        {
            if (isChaging)
            {
                //플레이어 사망 처리
                PlayerController player = GameObject.FindObjectOfType<PlayerController>();
                if (player)
                {
                    player.GetComponent<IHit>().OnHit();
                }
            }
            
        }
    }
}

