using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class ChkeState : ActionNode
    {
        private EnemyController _enemy = null;
        public CharacterStateType chkState = CharacterStateType.Null;//체크할 상태
        private CharacterStateType nowSate = CharacterStateType.Null;//현재 상태
        protected override void OnStart()
        {
            if (!_enemy)
                _enemy = context.gameObject.GetComponent<EnemyController>();

            if (_enemy)
                nowSate = _enemy.runningStateType;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (_enemy && chkState == nowSate)
                return State.Success;
            else
                return State.Failure;
        }
    }
}