using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class InGameCameraController : Singleton<InGameCameraController>
    {
        public Transform target = null;//플레이어 위치
        public float minX, minY, maxX, maxY;
        public float smoothSpeed = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            if (target != null)
            {
                Vector3 targetPos = target.position;
                targetPos.z = transform.position.z;
                transform.position = targetPos;
            }

        }
        private void LateUpdate()
        {
            if (target != null)
            {
                //카메라 이동
                Vector3 desiredPosition = target.position;
                desiredPosition.z = transform.position.z;
                // 거리에 따라 속도를 조절
                float distance = Vector3.Distance(transform.position, desiredPosition);
                float adjustedSpeed =  smoothSpeed * distance;
                adjustedSpeed = Mathf.Clamp(adjustedSpeed, 0.025f, 1);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, adjustedSpeed);
                transform.position = smoothedPosition;

                //카메라 위치 제한
                this.transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minX, maxX),
                    Mathf.Clamp(transform.position.y, minY, maxY),
                    transform.position.z
                    );
            }
        }

    }

}
