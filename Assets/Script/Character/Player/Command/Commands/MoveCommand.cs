using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MoveCommand : PlayerCommandInit 
    {
    

        public override void Execute(PlayerController player)
        {
            //입력 예외처리 구현 부분
            if(player.runningStateType == runnigStatetype)
            {
                return;
            }
            Debug.Log("이동 입력");
            base.Execute(player);
        }
    }
}