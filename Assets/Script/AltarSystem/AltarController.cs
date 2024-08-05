using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class AltarController : MonoBehaviour
    {
        public int soulsRequired = 10; // 필요한 영혼의 수
        public bool isTurnOn = false;
        public string soulTag = "Soul";
        [SerializeField]
        private int soulsCollected = 0;
        private Animator _animation = null;
        public string turnOnAniPara = null;

        void Start()
        {
            _animation = GetComponent<Animator>();
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(soulTag))
            {
                soulsCollected++;
                Destroy(other.gameObject); // 영혼 파괴

                if (soulsCollected >= soulsRequired && !isTurnOn)
                {
                    isTurnOn = true;
                    _animation.SetTrigger(turnOnAniPara);
                }
            }
        }
    }
}

