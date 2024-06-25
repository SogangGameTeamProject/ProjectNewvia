using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class StateBase : MonoBehaviour, CharacterState
    {
        protected PlayerController player;

        // pass in any parameters you need in the constructors
        public StateBase(PlayerController player)
        {
            this.player = player;
        }

        //상태 전환 시 처리
        public abstract void Enter();

        public abstract void Update();

        //상태 종료 시 처리
        public abstract void Exit();
    }
}
