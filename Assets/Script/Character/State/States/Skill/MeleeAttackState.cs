using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MeleeAttackState : SkillStateBase
    {
        public GameObject _damageZone = null;//활성화할 데미지 존 오브젝트

        protected override void HandleFirstDeal()
        {
            
        }

        protected override void HandleSkill()
        {
            _damageZone.SetActive(true);
        }

        protected override void HandleLastDeal()
        {
            _damageZone.SetActive(false);
        }

        protected override void OnSkillEnd()
        {
            _damageZone.SetActive(false);
        }
    }
}

