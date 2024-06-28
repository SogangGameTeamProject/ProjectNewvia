using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DashCommand : PlayerCommandInit 
    {
        

        public override void Execute(PlayerController player)
        {
            //입력 예외처리 구현 부분
            if(player.runningStateType == runnigStatetype)
            {
                return;
            }
            base.Execute(player);
        }
    }
}