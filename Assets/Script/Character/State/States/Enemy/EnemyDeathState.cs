using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class EnemyDeathState : DeathState
    {
        public GameObject soulPre;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            GameManager.Instance.killCount++;
            MonsterSpawnManager.Instance.nowFieldMonsterCnt--;
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
            Instantiate(soulPre, this.transform.position, Quaternion.identity, null);
            base.OnDeath();
        }
    }
 }
