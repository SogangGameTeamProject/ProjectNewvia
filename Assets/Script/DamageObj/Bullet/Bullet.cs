using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Newvia
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rBody = null;
        [SerializeField]
        private float _range = 10f;//사거리
        [SerializeField]
        private float _nockBackPower = 30f;
        private Vector2 startPos = Vector2.zero;//발사 시작 위치

        [SerializeField]
        private LayerMask hitLayers;
        [SerializeField]
        private LayerMask lightLayers;

        private int LightCnt = 0;
        private void Update()
        {
            //레이 케스트로 빛 충돌 처리
            RaycastHit2D hits = Physics2D.CircleCast(transform.position, 1, Vector2.zero, 0, lightLayers);

            if (hits.collider == null)
                OnBecameInvisible();

            //레이 케스트로 충알 충돌 판정 처리
            // Raycast를 통한 피격 판정
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 0.5f, hitLayers);
            // 무언가에 맞았다면
            if (hitInfo.collider != null)
            {
                //피격 가능 오브젝트면 피격
                IHit hitObj = null;
                hitInfo.collider.gameObject.TryGetComponent<IHit>(out hitObj);
                if (hitObj != null)
                    hitObj.OnHit();

                //피격체 넉백 가능 여부 체크 후 넉백
                Rigidbody2D rigidbody2D = null;
                hitInfo.collider.gameObject.TryGetComponent<Rigidbody2D>(out rigidbody2D);
                CharacterInit character = null;
                hitInfo.collider.gameObject.TryGetComponent<CharacterInit>(out character);

                if (rigidbody2D && character)
                {
                    //무적이 체크
                    if(!character.isInvincible)
                        rigidbody2D.AddForce((_rBody.velocity).normalized * _nockBackPower, ForceMode2D.Impulse);
                }

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
