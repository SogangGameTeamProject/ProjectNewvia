using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class AltarController : MonoBehaviour
    {
        public int soulsRequired = 10; // 필요한 영혼의 수
        public string soulTag = "Soul";
        private int soulsCollected = 0;
        public GameObject altarLight; // 재단의 불

        void Start()
        {
            altarLight.SetActive(false); // 초기에는 불이 꺼져 있음
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(soulTag))
            {
                soulsCollected++;
                Destroy(other.gameObject); // 영혼 파괴

                if (soulsCollected >= soulsRequired)
                {
                    altarLight.SetActive(true); // 불 켜기
                }
            }
        }
    }
}

