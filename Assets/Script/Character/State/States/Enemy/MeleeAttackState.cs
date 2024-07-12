using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public class MeleeAttackState : CharacterStateBase
    {
        public float _firstDeal = 0.25f;//선딜
        public float _lastDeal = 0.25f;//후딜
        private float _stateCounter = 0;//상태 경과 시간 카운터
        public GameObject _damageZone = null;//활성화할 데미지 존 오브젝트
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            character.GetComponent<NavMeshAgent>().velocity = Vector2.zero;
            _stateCounter = 0;
            _damageZone.SetActive(false);
        }

        public override void Exit()
        {
            _damageZone.SetActive(false);
        }

        public override void StateUpdate()
        {
            _stateCounter += Time.deltaTime;

            if (_stateCounter >= _firstDeal && _stateCounter < _firstDeal + _lastDeal)
            {
                _damageZone.SetActive(true); // 선딜이 끝나면 데미지 존 활성화
            }
            else if (_stateCounter >= _firstDeal + _lastDeal)
            {
                _damageZone.SetActive(false); // 후딜이 끝나면 데미지 존 비활성화
                _character.StateTransition(CharacterStateType.Idle);
            }
        }
    }
}

