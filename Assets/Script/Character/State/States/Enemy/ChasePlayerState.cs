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
        [SerializeField]
        private float _chaseTime = 1f;//추적 시간
        private float _timeCounter = 0f;//추적 시간 카운터

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
            _timeCounter = 0;
        }

        public override void StateUpdate()
        {
            //플레이어 추적 구현
            if (_target != null)
            {
                _agent.SetDestination(_target.position);

                //이동 방향에 따른 
                float moveDirection = _agent.velocity.x;
                if (moveDirection > 0)
                    _character.transform.localScale = new Vector3(1, 1, 1);
                else if (moveDirection < 0)
                    _character.transform.localScale = new Vector3(-1, 1, 1);
            }

            //추적 종료 체크
            _timeCounter += Time.deltaTime;
            if (_timeCounter <= _chaseTime)
                _character.StateTransition(CharacterStateType.Idle);
        }

        public override void Exit()
        {
            
        }
    }
}

