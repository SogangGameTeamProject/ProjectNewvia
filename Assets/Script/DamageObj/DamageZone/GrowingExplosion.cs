using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class GrowingExplosion : DamageZoneBase
    {
        public float maxScale = 5f; // 최대 크기
        public float expansionRate = 1f; // 확장 속도
        private Vector3 initialScale; // 초기 크기

        void Start()
        {
            initialScale = transform.localScale; // 초기 크기 저장
        }

        void Update()
        {
            if (transform.localScale.x < maxScale)
            {
                float scaleIncrease = expansionRate * Time.deltaTime;
                transform.localScale += new Vector3(scaleIncrease, scaleIncrease, 0);
            }
            else
            {
                Destroy(gameObject); // 최대 크기에 도달하면 오브젝트 삭제
            }
        }
    }
}