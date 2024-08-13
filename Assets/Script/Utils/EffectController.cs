using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class EffectController : MonoBehaviour
    {
        // 이펙트가 활성화된 후 삭제될 때까지의 시간
        public float lifeTime = 2.0f; // 기본적으로 2초 후 삭제

        private void OnEnable()
        {
            // lifeTime 이후에 Destroy 함수 호출
            Invoke("DestroyEffect", lifeTime);
        }

        // 이펙트를 삭제하는 함수
        private void DestroyEffect()
        {
            // 게임 오브젝트를 삭제
            Destroy(gameObject);
        }
    }
}