using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Newvia
{
    public class RangeCheck : ActionNode
    {
        public float _chkRange = 5f;
        private Transform _target = null;
        public LayerMask _targetLayer;   // 타겟이 속한 레이어 설정
        protected override void OnStart()
        {
            GameObject targetObj = GameObject.FindWithTag("Player");
            if(targetObj)
                _target = targetObj.transform;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (!_target)
                return State.Failure;
            //타켓 방향으로 사거리만큼 레이케스트를 쏴서 맞으면 Success처리
            Vector2 targetPosition = _target.transform.position;
            Vector2 direction = ((Vector2)context.transform.position - targetPosition).normalized;

            // 레이캐스트를 쏘아서 히트 검사
            RaycastHit2D hit = Physics2D.Raycast(context.transform.position, direction, _chkRange, _targetLayer);

            if (hit.collider != null)
                return State.Success;
            else
                return State.Failure;
        }
    }
}