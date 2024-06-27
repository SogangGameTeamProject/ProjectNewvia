using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class CharacterStateBase : MonoBehaviour, CharacterState
    {
        protected CharacterInit _character = null;

        //상태 전환 시 처리
        public virtual void Enter(CharacterInit character)
        {
            if (_character == null)
                _character = character;
        }

        public abstract void StateUpdate();

        //상태 종료 시 처리
        public abstract void Exit();
    }
}
