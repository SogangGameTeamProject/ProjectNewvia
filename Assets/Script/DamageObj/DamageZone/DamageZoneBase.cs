using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class DamageZoneBase : MonoBehaviour
    {
        public LayerMask targetLayer;//데미지 줄 대상레이어


        private void OnTriggerEnter2D(Collider2D other)
        {
            //피격 처리
            if ((targetLayer.value & 1 << other.gameObject.layer) != 0)
            {
                IHit iHit = null;
                other.gameObject.TryGetComponent<IHit>(out iHit);

                if (iHit != null)
                {
                    Debug.Log("공격 성공");
                    iHit.OnHit();
                }
                    
            }
        }
    }
}