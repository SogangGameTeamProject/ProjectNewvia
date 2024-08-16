using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DeathState : CharacterStateBase
    {
        public float deathTime = 0.5f;
        private float deathTimer = 0;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            deathTimer = 0;
            //콜라이더 제거
            Collider[] colliders = GetComponentsInChildren<Collider>();

            // Iterate through each Collider and destroy it
            foreach (Collider collider in colliders)
            {
                Destroy(collider);
            }
        }

        public override void StateUpdate()
        {
            //죽음 딜레이
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathTime)
            {
                OnDeath();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        //죽음 구현
        protected virtual void OnDeath()
        {
            Destroy(_character.gameObject);
        }
    }
 }