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
        protected override void Enter(Weapone weapone)
        {
            base.Enter(weapone);
            _bullet = _bulletPool.GetBullet();
            _bullet.transform.rotation = weapone.transform.rotation;
        }

        protected override void StateUpdate()
        {
            countTimer += Time.deltaTime;
            if (countTimer >= _weapone._rateOfFire)
                _weapone.StateTransition(WeaponeStateType.Idle);
        }

        protected override void Exit()
        {
            
        }
    }
}

