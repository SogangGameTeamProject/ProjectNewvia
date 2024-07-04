using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 20f;//총알 속도
        [SerializeField]
        private float _damage = 1f;//총알 데미지



        void Update()
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}
