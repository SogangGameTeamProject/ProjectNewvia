using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DeathState : CharacterStateBase
    {
        public float deathTime = 0.5f;
        public bool vecZero = false;//사망시 넉백효과 제거 여부
        private float deathTimer = 0;
        
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            
            deathTimer = 0;
            //콜라이더 제거
            Collider2D[] colliders = _character.GetComponentsInChildren<Collider2D>();
            // Iterate through each Collider and destroy it
            foreach (Collider2D collider in colliders)
            {
                Destroy(collider);
            }
        }

        public override void StateUpdate()
        {
            if (vecZero)
                _character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //죽음 딜레이
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathTime)
            {
                Exit();
            }
        }

        public override void Exit()
        {
            base.Exit();
            OnDeath();
        }

        //죽음 구현
        protected virtual void OnDeath()
        {
            Destroy(_character.gameObject);
        }
    }
 }