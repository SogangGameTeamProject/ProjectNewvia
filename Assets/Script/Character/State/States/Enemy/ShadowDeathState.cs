using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class ShadowDeathState : EnemyDeathState
    {
        public EnemyController _summoner = null;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            ((EnemyController)_summoner).SetSturnCnt++;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            
        }


    }
 }
