using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public abstract class SubWeaponeStateBase : MonoBehaviour, SubWeaponeState
    {
        protected SubWeapone _subWeapone = null;
        public virtual void Enter(SubWeapone subWeapone)
        {
            if (_subWeapone == null)
                _subWeapone = subWeapone;
            _subWeapone.GetComponent<NavMeshAgent>().velocity = Vector2.zero;
        }

        //상태 종료 시 처리
        public abstract void Exit();

        public abstract void StateUpdate();
    }
}

