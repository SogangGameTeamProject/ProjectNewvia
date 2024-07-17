using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class SubWeaponeBoomSkill : SubWeaponeStateBase
    {
        public GameObject _boomObjPre;//폭발 오브젝트 프리펩
        private GameObject _boomObj;//폭발 오브젝트
        public override void Enter(SubWeapone subWeapone)
        {
            base.Enter(subWeapone);
            _boomObj = Instantiate(_boomObjPre, this.transform.position, Quaternion.identity, null);
        }

        public override void Exit()
        {
            
        }

        public override void StateUpdate()
        {
            if (_boomObj == null)
                _subWeapone.StateTransition(SubWeaponeStateType.Idle);
        }
    }
}

