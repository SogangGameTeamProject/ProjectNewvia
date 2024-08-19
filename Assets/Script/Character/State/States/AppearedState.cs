using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class AppearedState : CharacterStateBase
    {
        private float elapsedTime = 0;
        public float appearedTime = 2f;
        public CharacterStateType nextState = CharacterStateType.Idle;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            //값 초기화
            elapsedTime = 0;
            
        }

        public override void StateUpdate()
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= appearedTime)
                _character.StateTransition(nextState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
 }
