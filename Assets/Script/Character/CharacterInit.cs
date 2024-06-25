using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public abstract class CharacterInit : Subject
    {
        private CharacterStateContext _stateContext;

        public virtual void Start()
        {

        }


        //현재 캐릭터의 상태 전환
        public abstract void StateTransition(StateBase state);
    }
}

