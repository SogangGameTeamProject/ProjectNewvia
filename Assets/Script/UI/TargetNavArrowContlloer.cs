using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class TargetNavArrowContlloer : MonoBehaviour
    {
        public Transform target; // 화살표가 가리킬 타겟

        void Update()
        {
            if (target == null)
            {
                // 타겟이 사라지면 화살표도 사라지게 함
                Destroy(gameObject);
                return;
            }

            // 타겟 방향 계산
            Vector2 direction = target.position - transform.position;

            // 화살표가 회전해야 하는 각도 계산 (기본적으로 위쪽을 향한다고 가정)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // 화살표 회전 적용
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}

