using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class WeaponeStateBase : MonoBehaviour, WeaponeState
    {
        protected Weapone _weapone;


        //상태 전환 시 처리
        protected virtual void Enter(Weapone weapone)
        {
            if (_weapone == null)
                _weapone = weapone;
        }

        protected abstract void StateUpdate();

        //상태 종료 시 처리
        protected abstract void Exit();
    }
}
