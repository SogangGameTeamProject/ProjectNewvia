using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class ChkGameFlow : ActionNode
    {
        private GameManager _gameManager = null;
        public GameFlowType chkFlowType;
        private GameFlowType nowFlowType;
        protected override void OnStart()
        {
            if(_gameManager == null)
                _gameManager = GameManager.Instance;
            if (_gameManager)
                nowFlowType = _gameManager.flowType;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (chkFlowType == nowFlowType)
                return State.Success;
            else
                return State.Failure;
        }
    }
}
