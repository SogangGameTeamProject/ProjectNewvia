using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class CoolTimeCheck : ActionNode
    {
        public float _coolTime = 5f;//쿨타임
        private bool isCool = false;//쿨타임 여부
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}

