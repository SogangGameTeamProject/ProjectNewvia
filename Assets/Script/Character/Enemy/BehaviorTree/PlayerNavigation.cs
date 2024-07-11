using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class PlayerNavigation : ActionNode
    {
        private GameObject _target = null;
        protected override void OnStart()
        {
            _target = GameObject.FindWithTag("Player");
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            if (_target)
                return State.Success;
            else
                return State.Failure;
        }
    }
}

