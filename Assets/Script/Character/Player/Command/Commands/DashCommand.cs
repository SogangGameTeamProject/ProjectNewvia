using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DashCommand : PlayerCommandInit 
    {
        public override void Execute(PlayerController player)
        {
            
            CharacterStateType runningSateType = player.runningStateType;
            //입력 예외처리 구현 부분
            if (runningSateType != runnigStatetype &&
                (runningSateType == CharacterStateType.Idle || runningSateType == CharacterStateType.Move) &&
                player.dashStaminaCoast <= player.NowStamina
                )
            {
                
                player.NowStamina -= player.dashStaminaCoast;
                base.Execute(player);
            }
        }
    }
}