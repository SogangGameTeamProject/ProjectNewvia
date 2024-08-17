    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Newvia
{
    public class NormalExplosion : DamageZoneBase
    {
        public float duration = 1f;// 지속 시간
        public float delayTime = 0.2f;//폭발 지연 시간
        private bool isExplosion = false;//폭발 여부
        private float elapsedTime = 0;

        private void Start()
        {
            // 모든 콜라이더 비활성화
            Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (!isExplosion && elapsedTime > delayTime)
            {
                // 모든 콜라이더 활성화
                Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
                foreach (Collider2D col in colliders)
                {
                    col.enabled = true;
                }
            }
            
            if (elapsedTime > duration)
                Destroy(gameObject);
        }
    }

}
