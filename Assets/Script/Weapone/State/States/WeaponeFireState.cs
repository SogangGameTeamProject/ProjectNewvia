using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class WeaponeFireState : WeaponeStateBase
    {
        [SerializeField]
        private BulletPool _bulletPool = null;
        private Bullet _bullet = null;
        private float countTimer = 0;
        public List<GameObject> fireEffect;
        
        public override void Enter(Weapone weapone)
        {
            base.Enter(weapone);
            countTimer = 0;

            //발사 이펙트 랜덤 출력
            // 배열이 비어있지 않은지 확인
            if (fireEffect.Count > 0)
            {
                // 랜덤으로 인덱스 선택
                int randomIndex = Random.Range(0, fireEffect.Count);

                // 랜덤하게 선택된 이펙트를 생성
                GameObject selectedEffect = fireEffect[randomIndex];
                Instantiate(selectedEffect, weapone.transform.parent.position, Quaternion.identity);
            }

            

            //총알 발사
            _bullet = _bulletPool.GetBullet();
            _bullet.transform.position = weapone._firePoint.position;
            _bullet.transform.rotation = weapone.transform.rotation;
            _bullet.OnFire(weapone._firePoint.position, weapone.transform.rotation
                , weapone._bulletPower, weapone._bulletRange
                );
        }

        public override void StateUpdate()
        {
            countTimer += Time.deltaTime;
            if (countTimer >= _weapone._rateOfFire)
                _weapone.StateTransition(WeaponeStateType.Idle);
        }

        public override void Exit()
        {
            
        }
    }
}

