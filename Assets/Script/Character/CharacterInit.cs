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
            _stateContext = new CharacterStateContext(this);//stateContext �� �ʱ�ȭ
        }


        //���� ĳ������ ���� ��ȯ
        public virtual void StateTransition(CharacterState state)
        {
            _stateContext.Transition(state);
        }
    }
}

