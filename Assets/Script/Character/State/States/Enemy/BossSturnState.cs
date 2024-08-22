using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class BossSturnState : ShadowSummon
    {
        private GameObject navArrowPre = null;//생성된 네비 화살표 프리펩
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            _character.isInvincible = false;
            //보스를 타겟으로 한 navArrow생성
            PlayerController player = GameObject.FindObjectOfType<PlayerController>();
            if (player)
            {
                shadowNavArrow.GetComponent<TargetNavArrowContlloer>().target = this.transform;
                navArrowPre = Instantiate(shadowNavArrow, player.transform.position, Quaternion.identity, player.transform);
            }
        }
        protected override void HandleLastDeal()
        {
            base.HandleLastDeal();
            _character.isInvincible = true;
            //보스를 타겟으로 한 navArrow 제거
            if (navArrowPre)
            {
                Destroy(navArrowPre);
                navArrowPre = null;
            }
        }
    }
 }
