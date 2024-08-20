using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class ShadowSummon : SkillStateBase
    {
        public List<Transform> summonPositionList = new List<Transform>();
        public GameObject shadowPre = null; // 소환할 그림자 프리팹
        private bool isSummon = false;

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            ((EnemyController)_character).SetSturnCnt = 0;
            isSummon = false;
        }

        protected override void HandleFirstDeal()
        {
            // 선딜이 필요한 경우 이곳에 구현
        }

        protected override void HandleSkill()
        {
            // 스킬이 한 번만 실행되도록 체크
            if (!isSummon)
            {
                shadowPre.GetComponentInChildren<ShadowDeathState>()._summoner = (EnemyController)_character;
                // 소환 위치 리스트에 있는 각 위치에 그림자 소환
                foreach (Transform summonPos in summonPositionList)
                {
                    if (summonPos != null && shadowPre != null)
                    {
                        
                        
                        // 그림자 프리팹을 해당 위치에 생성
                        Instantiate(shadowPre, summonPos.position, summonPos.rotation);
                    }
                }
                isSummon = true; // 소환이 완료되면 플래그를 true로 설정
            }
        }

        protected override void HandleLastDeal()
        {
            // 후딜이 필요한 경우 이곳에 구현
        }

        protected override void OnSkillEnd()
        {
            // 스킬 종료 시 필요한 동작을 이곳에 구현
        }

        public override void Exit()
        {
            base.Exit();
            // 스킬이 종료되면 추가적인 동작이 필요할 경우 이곳에 구현
        }
    }
}
