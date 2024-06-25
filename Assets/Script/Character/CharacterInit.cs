using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class CharacterInit : Subject
    {
        private CharacterStateContext _stateContext;

        public void Start()
        {
            _stateContext = new CharacterStateContext(this);//stateContext 값 초기화
        }


        //현재 캐릭터의 상태 전환
        public virtual void StateTransition(CharacterState state)
        {
            _stateContext.Transition(state);
        }
    }
}

