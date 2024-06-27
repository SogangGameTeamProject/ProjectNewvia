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
            Debug.Log("EnterMove");
        }

        public override void StateUpdate()
        {
            Debug.Log("UdateMove");
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float speed = _character.moveSpeed;

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
            if (movement != Vector3.zero)
                _character.transform.position += movement * speed * Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("ExitMove");
        }
    }

}

