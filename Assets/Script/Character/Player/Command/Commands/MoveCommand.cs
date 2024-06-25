using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MoveCommand : PlayerCommandInit
    {
        public MoveCommand(PlayerController player) : base(player)
        {
        }

        public override void Execute()
        {
            //입력 예외처리 구현 부분


            base.Execute();
        }
    }
}