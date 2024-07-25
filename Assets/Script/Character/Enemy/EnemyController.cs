using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public class EnemyController : CharacterInit
    {
        private NavMeshAgent _agent;
        protected override void Start()
        {
            base.Start();

            
        }

        protected override void SettingInitStatus()
        {
            base.SettingInitStatus();
            SetAgent();
        }

        protected void SetAgent()
        {
            //네비매쉬 에이전트 값 초기화
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            _agent.updateRotation = false;
            _agent.speed = moveSpeed;
        }

        public override void OnHit(int hitType = 0)
        {
            base.OnHit(hitType);
            if(!isInvincible && hitType == 0)
            {
                //플레이어 서브웨폰 게이지 증가
                SubWeapone subWeapone = GameObject.FindObjectOfType<SubWeapone>();
                if (subWeapone)
                    subWeapone.SetNowEnergy++;
            }
        }
    }
}

