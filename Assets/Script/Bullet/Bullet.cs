using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rBody = null;
        [SerializeField]
        private float _range = 10f;//사거리

        private Vector2 startPos = Vector2.zero;//발사 시작 위치

        [SerializeField]
        private LayerMask hitLayers;

        private void Update()
        {
            //레이 케스트로 충알 충돌 판정 처리
            // Raycast를 통한 피격 판정
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 0.5f, hitLayers);
            // 무언가에 맞았다면
            if (hitInfo.collider != null)
            {
                IHit hitObj = null;
                hitInfo.collider.gameObject.TryGetComponent<IHit>(out hitObj);
                if (hitObj != null)
                    hitObj.OnHit();

                // 총알 비활성화
                OnBecameInvisible();
            }
            if((Vector2.Distance(startPos, (Vector2)this.transform.position) >= _range))
                OnBecameInvisible();
        }

        public void OnFire(Vector3 firePos, Quaternion bulletRotate, float speed, float range)
        {
            transform.position = firePos;
            transform.rotation = bulletRotate;
            _range = range;

            if (_rBody == null)
                _rBody = this.gameObject.GetComponent<Rigidbody2D>();
            startPos = this.transform.position;
            _rBody.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
            transform.parent.GetComponent<BulletPool>().ReleaseBullet(this);
        }
    }
}
