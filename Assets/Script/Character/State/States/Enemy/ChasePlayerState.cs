using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public class ChasePlayerState : CharacterStateBase
    {
        private NavMeshAgent _agent = null;
        private Transform _target = null;//타겟 위치

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);

            //네비메쉬 에이전트 값 초기화
            if (_agent == null)
            {
                _agent = character.GetComponent<NavMeshAgent>();
            }

            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj && !_target)
                _target = playerObj.transform;
        }

        public override void StateUpdate()
        {
            //플레이어 추적 구현
            if (_target != null)
            {
                _agent.SetDestination(_target.position);

                //이동 방향에 따른 방향 전환
                float moveDirection = _agent.velocity.x;
                if (moveDirection > 0)
                    _character.CharacterDirection = CharacterDirection.right;
                else if (moveDirection < 0)
                    _character.CharacterDirection = CharacterDirection.left;
            }
        }

        public override void Exit()
        {
            if (_agent != null)
            {
                _agent.isStopped = true; // 추적 상태 종료 시 이동을 멈춤
                _agent.ResetPath(); // 목적지를 초기화하여 이동 경로를 제거
                _agent.velocity = Vector3.zero;
            }
        }
    }
}

