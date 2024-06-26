using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class CharacterStateBase : MonoBehaviour, CharacterState
    {
        protected CharacterInit _character;

        // pass in any parameters you need in the constructors
        public CharacterStateBase(CharacterInit character)
        {
            this._character = character;
        }

        //상태 전환 시 처리
        public abstract void Enter();

        public abstract void StateUpdate();

        //상태 종료 시 처리
        public abstract void Exit();
    }
}
