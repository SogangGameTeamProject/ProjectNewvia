using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class SkillCommand : SubWeaponeCommandInit
    {
        public override void Execute(PlayerController player)
        {
            base.Execute(player);
            Debug.Log(_player + ", " + _subWeapone);
            //입력 예외처리 구현 부분
            if ((_player.runningStateType == CharacterStateType.Idle || _player.runningStateType == CharacterStateType.Move) &&
                (_subWeapone.runningStateType == SubWeaponeStateType.Idle || _subWeapone.runningStateType == SubWeaponeStateType.Walk) &&
                _subWeapone._nowEnergy >= _subWeapone._energyConsumed
                )
            {
                _subWeapone.SetNowEnergy -= _subWeapone._energyConsumed;
                _subWeapone.StateTransition(runnigStatetype);
            }
            
        }
    }
}