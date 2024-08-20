using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class EnemyDeathState : DeathState
    {
        public GameObject soulPre;
        public bool isDeathCount = true;//데스카운트 여부
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            if (isDeathCount)
            {
                GameManager.Instance.killCount++;
                MonsterSpawnManager.Instance.nowFieldMonsterCnt--;
            }
            
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
            if(soulPre)
                Instantiate(soulPre, this.transform.position, Quaternion.identity, null);
            base.OnDeath();
        }
    }
 }
