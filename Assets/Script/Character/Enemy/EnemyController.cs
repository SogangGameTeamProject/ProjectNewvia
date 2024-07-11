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

            //네비매쉬 에이전트 값 초기화
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateUpAxis = false;
            _agent.updateRotation = false;
        }
    }
}

