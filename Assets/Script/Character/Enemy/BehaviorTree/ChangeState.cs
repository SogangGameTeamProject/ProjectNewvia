using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class ChangeState : ActionNode
    {
        private EnemyController _enemy = null;
        public CharacterStateType _chageState = CharacterStateType.Null;//변경할 상태
        private CharacterStateType _nowState = CharacterStateType.Null;//현제 상태
        
        protected override void OnStart()
        {
            if (!_enemy)
                _enemy = context.gameObject.GetComponent<EnemyController>();

            if (_enemy)
                _enemy.StateTransition(_chageState);
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            _nowState = _enemy.runningStateType;//현재 상태 갱신
            
            if (_chageState == _nowState)
                return State.Running;
            else
                return State.Success;
        }
    }
}

