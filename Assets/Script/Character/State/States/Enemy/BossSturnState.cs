using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class BossSturnState : ShadowSummon
    {
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            _character.isInvincible = false;
        }
        protected override void HandleLastDeal()
        {
            base.HandleLastDeal();
            _character.isInvincible = true;
        }
    }
 }
