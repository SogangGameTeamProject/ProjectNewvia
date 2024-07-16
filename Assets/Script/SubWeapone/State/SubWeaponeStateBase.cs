using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public abstract class SubWeaponeStateBase : MonoBehaviour, SubWeaponeState
    {
        private SubWeapone _subWeapone = null;
        public virtual void Enter(SubWeapone subWeapone)
        {
            if (_subWeapone == null)
                _subWeapone = subWeapone;
        }

        //상태 종료 시 처리
        public abstract void Exit();

        public abstract void StateUpdate();
    }
}

