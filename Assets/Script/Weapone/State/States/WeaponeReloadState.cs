using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class WeaponeReloadState : WeaponeStateBase
    {
        private float loadingCount = 0;//장전 카운트

        public override void Enter(Weapone weapone)
        {
            base.Enter(weapone);
            loadingCount = weapone._reloadingSpeed;
        }

        public override void StateUpdate()
        {
            loadingCount -= Time.deltaTime;
            if (loadingCount <= 0)
                _weapone.StateTransition(WeaponeStateType.Idle);
        }

        public override void Exit()
        {
            //장전 처리
            if (loadingCount <= 0)
            {
                _weapone.MagazineCapacity = _weapone._maxMagazineCapacity;
            }
                
        }

    }
}