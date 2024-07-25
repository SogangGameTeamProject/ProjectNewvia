using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class CharacterStateBase : MonoBehaviour, CharacterState
    {
        protected CharacterInit _character = null;
        protected Animator _animator = null;
        [SerializeField]
        protected string stateAniPara = null;//재생할 애니메이션의 파라미터

        //상태 전환 시 처리
        public virtual void Enter(CharacterInit character)
        {
            if (_character == null)
                _character = character;
            if (_animator == null)
                _animator = character.GetComponent<Animator>();
            if(_animator && stateAniPara != string.Empty)
                _animator.SetTrigger(stateAniPara);
        }

        public abstract void StateUpdate();

        //상태 종료 시 처리
        public virtual void Exit()
        {
 
        }
    }
}
