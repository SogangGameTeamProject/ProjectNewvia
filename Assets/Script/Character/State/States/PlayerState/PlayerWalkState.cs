using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerWalkState : CharacterStateBase
    {
        private Rigidbody2D _rigidbody2D = null;

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            
            if(_rigidbody2D == null)
                _rigidbody2D = character.GetComponent<Rigidbody2D>();
        }

        public override void StateUpdate()
        {
            //이동 구현 부분
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float speed = _character.moveSpeed;

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
            
            if (movement != Vector3.zero)
                _rigidbody2D.velocity = movement*_character.moveSpeed;
            //이동 멈춤 체크 시 Idle상태로 전환
            else
                _character.StateTransition(CharacterStateType.Idle);
        }

        public override void Exit()
        {
        }
    }

}

