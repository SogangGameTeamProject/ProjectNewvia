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
            Debug.Log("이동 시작");
        }

        public override void StateUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float speed = _character.moveSpeed;

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
            Debug.Log(movement);
            if (movement != Vector3.zero)
                _character.transform.position += movement * speed * Time.deltaTime;
            else
                _character.StateTransition(CharacterStateType.Idle);
        }

        public override void Exit()
        {
            Debug.Log("이동 종료");
        }
    }

}

