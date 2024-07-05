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
        public override void Enter(Weapone weapone)
        {
            base.Enter(weapone);
            countTimer = 0;

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

