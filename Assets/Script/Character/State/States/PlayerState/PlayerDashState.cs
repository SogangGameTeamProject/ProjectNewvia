using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerDashState : CharacterStateBase
    {
        private Rigidbody2D _rigidbody2D = null;
        private Vector2 dashVec = Vector2.zero;
        [SerializeField]
        private float dashPower = 30f;
        [SerializeField]
        private float dashTime = 0.25f;
        Coroutine runningCoroutine = null;
        
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            
            if(_rigidbody2D == null)
                _rigidbody2D = character.GetComponent<Rigidbody2D>();

            //대쉬 방향 구하기
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            dashVec = new Vector2(moveHorizontal, moveVertical).normalized;
            if (dashVec == Vector2.zero)
                dashVec = Vector2.right;

            //대쉬 방향에 따른 방향 전환
            if(dashVec.x != 0)
                _character.CharacterDirection = (CharacterDirection)(Mathf.Round(dashVec.x));

            runningCoroutine = StartCoroutine(Dash());

            //무기 캔슬
            ((PlayerController)_character)._weapone.GetComponent<Weapone>().StateTransition(WeaponeStateType.Idle);
            _character.isInvincible = true;//무적 적용
        }

        public override void StateUpdate()
        {
            if (runningCoroutine == null)
                _character.StateTransition(CharacterStateType.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            _character.isInvincible = false;//무적 제거
            _rigidbody2D.velocity = Vector2.zero;
        }

        //대쉬 구현 코루틴
        IEnumerator Dash()
        {
            _rigidbody2D.AddForce(dashVec * dashPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dashTime);
            runningCoroutine = null;
        }
    }

}

