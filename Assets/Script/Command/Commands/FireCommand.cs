using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class FireCommand : WeaponeCommandInit
    {
        public override void Execute(PlayerController player)
        {
            base.Execute(player);
            //입력 예외처리 구현 부분
            if (_weapone.runningStateType != runnigStatetype &&
                //플레이어 상태 체크
                (_player.runningStateType == CharacterStateType.Idle || _player.runningStateType == CharacterStateType.Move) &&
                //무기 상태 체크
                (_weapone.runningStateType == WeaponeStateType.Idle || _weapone.runningStateType == WeaponeStateType.Relod) &&
                _weapone.MagazineCapacity >= _weapone._fireCost
                )
            {
                _weapone.MagazineCapacity--;
                _weapone.StateTransition(runnigStatetype);
            }
            
        }
    }
}