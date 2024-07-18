using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class TurnTowardsPlayer : ActionNode
    {
        private GameObject _playerObj = null;
        protected override void OnStart()
        {
            _playerObj = GameObject.FindWithTag("Player");
            if (_playerObj)
            {
                Vector3 direction = (_playerObj.transform.position - context.transform.position).normalized;
                CharacterInit character = context.gameObject.GetComponent<CharacterInit>();
                if (direction.x > 0)
                    character.CharacterDirection = CharacterDirection.right;
                else if (direction.x < 0)
                    character.CharacterDirection = CharacterDirection.left;
            }
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }

}
