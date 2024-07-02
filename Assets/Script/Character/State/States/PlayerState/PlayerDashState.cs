using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerDashState : CharacterStateBase
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
            
        }

        public override void Exit()
        {

        }
    }

}

