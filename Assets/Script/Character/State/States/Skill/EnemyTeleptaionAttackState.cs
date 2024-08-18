using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class EnemyTeleptaionAttackState : EnemyRushAttackState
    {
        private bool useTelepoting = false;//텔레포트 사용 여주
        public float teleportDistance = 3.5f;//텔레포트 이동시 대상과의 거리

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            useTelepoting = false;
        }

        protected override void HandleFirstDeal()
        {
            base.HandleFirstDeal();
            //텔레포트 위치 갱신
        }

        protected override void HandleSkill()
        {
            //돌진 공격전 텔레포트 구현
            if (!useTelepoting) {
                useTelepoting = true;
                direction *= -1;
                Vector3 teleportDirection = direction;
                Vector3 teleportPosition = (Vector3)_targetPosition - (teleportDirection*teleportDistance);
                _character.transform.position = teleportPosition;

                //이동 후 타켓 방향에 따른 바라보는 방향 조정
                if (teleportPosition.x > _targetPosition.x)
                    _character.CharacterDirection = CharacterDirection.left;
                else
                    _character.CharacterDirection = CharacterDirection.right;
                


            }
            
            base.HandleSkill();
        }
    }
}
