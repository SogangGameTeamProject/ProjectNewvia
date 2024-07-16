using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class CoolTimeCheck : ActionNode
    {
        public float _coolTime = 5f;//쿨타임
        private float _lastEndTime = -Mathf.Infinity; // 마지막 종료 시간

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            // 현재 시간이 마지막 종료 시간 + 쿨타임보다 크거나 같은지 확인
            if (Time.time >= _lastEndTime + _coolTime)
            {
                // 쿨타임이 지난 경우
                _lastEndTime = Time.time; // 마지막 종료 시간을 현재 시간으로 업데이트
                return State.Success;
            }
            else
            {
                // 쿨타임이 지나지 않은 경우
                return State.Failure;
            }
        }
    }
}
