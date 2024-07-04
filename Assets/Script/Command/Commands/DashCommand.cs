using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DashCommand : PlayerCommandInit 
    {
        public override void Execute(PlayerController player)
        {
            base.Execute(player);
            //입력 예외처리 구현 부분
            if (_player.runningStateType != runnigStatetype &&
                (_player.runningStateType == CharacterStateType.Idle || _player.runningStateType == CharacterStateType.Move) &&
                player.dashStaminaCoast <= player.NowStamina
                )
            {
                player.NowStamina -= player.dashStaminaCoast;
                _player.StateTransition(runnigStatetype);
            }
        }
    }
}