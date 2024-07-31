using System.Collections;
using UnityEngine;

namespace Newvia
{
    public class SoulController : MonoBehaviour
    {
        public string altarTag = "Altar"; // 재단 태그명
        public float speed = 2f; // 영혼의 이동 속도
        public float absorptionDistance = 20f; // 흡수되는 거리
        public float lifeTime = 5f; // 영혼의 생존 시간
        private Transform closestAltar;
        private static bool isQuitting = false; // 애플리케이션 종료 플래그

        void Start()
        {
            closestAltar = FindClosestAltar();

            if (closestAltar == null)
            {
                // 재단이 흡수 거리 내에 없다면 일정 시간 후에 소멸
                StartCoroutine(LifeTimer());
            }
        }

        void Update()
        {
            if (closestAltar != null)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, closestAltar.position, step);
            }
        }

        // 거리 내의 가장 가까운 재단 탐색
        Transform FindClosestAltar()
        {
            GameObject[] altars = GameObject.FindGameObjectsWithTag(altarTag);
            Transform closest = null;
            float minDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject altar in altars)
            {
                float distance = Vector3.Distance(currentPosition, altar.transform.position);
                if (distance <= absorptionDistance && distance < minDistance)
                {
                    minDistance = distance;
                    closest = altar.transform;
                }
            }

            return closest;
        }

        IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject); // 일정 시간 후 영혼 파괴
        }

        void OnApplicationQuit()
        {
            isQuitting = true; // 애플리케이션 종료 중임을 표시
        }

        void OnDestroy()
        {
            if (!isQuitting)
            {
                // 애플리케이션이 종료 중이 아닐 때만 영혼을 파괴합니다.
                Destroy(gameObject);
            }
        }
    }
}
