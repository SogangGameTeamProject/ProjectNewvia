using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DeathState : CharacterStateBase
    {
        public override void Enter(CharacterInit character)
        {
            Destroy(character.gameObject);
        }

        public override void StateUpdate()
        {

        }

        public override void Exit()
        {

        }
    }
 }
