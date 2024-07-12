using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance { get; private set; }

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _initialCapacity = 20;

        private ObjectPool<Bullet> _pool;

        private void Awake()
        {
            Instance = this;
            _pool = new ObjectPool<Bullet>(_bulletPrefab, _initialCapacity, transform);
        }

        private void Start()
        {
            transform.SetParent(null);
        }

        public Bullet GetBullet()
        {
            return _pool.Get();
        }

        public void ReleaseBullet(Bullet bullet)
        {
            _pool.Release(bullet);
        }
    }
}