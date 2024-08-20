using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public class RetreatPlayerState : CharacterStateBase
    {
        private NavMeshAgent _agent = null;
        private Transform _target = null;//타겟 위치
        public float fleeDistance = 10f;  // 도망갈 거리
        public float safeDistance = 15f;  // 플레이어와의 안전 거리
        public float wanderRadius = 20f;  // 순회할 반경
        public float wanderInterval = 3f; // 순회 지점을 변경할 시간 간격
        private float wanderTimer;

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

            wanderTimer = wanderInterval;
        }

        public override void StateUpdate()
        {
            //플레이어 추적 구현
            if (_target != null)
            {
                //도주
                wanderTimer += Time.deltaTime;
                float distanceToPlayer = Vector3.Distance(transform.position, _target.position);

                // 플레이어가 안전 거리 내에 있을 경우 도망
                if (distanceToPlayer < safeDistance)
                {
                    FleeFromPlayer();
                }
                else
                {
                    // 일정 시간마다 새로운 순회 지점 선택
                    if (wanderTimer >= wanderInterval)
                    {
                        Wander();
                        wanderTimer = 0f;
                    }
                }

                //이동 방향에 따른 방향 전환
                float moveDirection = _agent.velocity.x;
                if (moveDirection > 0)
                    _character.CharacterDirection = CharacterDirection.right;
                else if (moveDirection < 0)
                    _character.CharacterDirection = CharacterDirection.left;
            }
            else
                _character.StateTransition(CharacterStateType.Idle);
        }

        void FleeFromPlayer()
        {
            Vector3 fleeDirection = (transform.position - _target.position).normalized;
            Vector3 newGoal = transform.position + fleeDirection * fleeDistance;

            // NavMesh 경로 갱신
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newGoal, out hit, fleeDistance, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }
        }

        void Wander()
        {
            // 현재 위치에서 일정 반경 내의 임의의 위치 선택
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }
        }

        public override void Exit()
        {
            base.Exit();
            if (_agent != null)
            {
                _agent.isStopped = true; // 추적 상태 종료 시 이동을 멈춤
                _agent.ResetPath(); // 목적지를 초기화하여 이동 경로를 제거
                _agent.velocity = Vector3.zero;
            }
        }
    }
}

