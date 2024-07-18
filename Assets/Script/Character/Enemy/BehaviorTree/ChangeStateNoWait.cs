using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
namespace Newvia
{
    public class ChangeStateNoWait : ActionNode
    {
        private EnemyController _enemy = null;
        public CharacterStateType _chageState = CharacterStateType.Null;//변경할 상태

        protected override void OnStart()
        {
            if (!_enemy)
                _enemy = context.gameObject.GetComponent<EnemyController>();

            if (_enemy && _enemy.runningStateType != _chageState)
                _enemy.StateTransition(_chageState);
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
