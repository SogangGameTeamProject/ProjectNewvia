using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Newvia
{
    public class SubWeaponeIdleState : SubWeaponeStateBase
    {
        

        public override void Enter(SubWeapone subWeapone)
        {
            base.Enter(subWeapone);
        }
        public override void Exit()
        {

        }

        public override void StateUpdate()
        {
            //플레이어 탐지, 있으면 추적 상태로 전환
            if (_subWeapone._playerController)
                _subWeapone.StateTransition(SubWeaponeStateType.Walk);
        }

        
    }
}
