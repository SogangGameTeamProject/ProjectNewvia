using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MoveCommand : PlayerCommandInit 
    {
    

        public override void Execute(PlayerController player)
        {
            base.Execute(player);
            //입력 예외처리 구현 부분
            if (player.runningStateType != runnigStatetype &&
                player.runningStateType == CharacterStateType.Idle)
            {
                _player.StateTransition(runnigStatetype);
            }
            
        }
    }
}