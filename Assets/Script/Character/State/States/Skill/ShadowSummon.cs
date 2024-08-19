using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class ShadowSummon : SkillStateBase
    {
        public List<Transform> summonPositionList = new List<Transform>();
        public GameObject shadowPre = null;//소환할 그림자 프리펩
        private bool isSummon = false;

        public override void Exit()
        {
            base.Exit();
        }

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            isSummon = false;
        }

        protected override void HandleFirstDeal()
        {
            
        }

        protected override void HandleSkill()
        {
           
        }

        protected override void HandleLastDeal()
        {
            
        }

        protected override void OnSkillEnd()
        {
            
        }
    }
}
