using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MonsterSpawnerController : MonoBehaviour
    {
        //몬스터 정보
        [System.Serializable]
        public class Monster
        {
            public GameObject prefab; // 몬스터의 프리팹
            public float weight;      // 몬스터의 가중치
        }

        public List<Monster> monsters; // 스폰할 몬스터 리스트
        public Transform spawnPoint;   // 몬스터가 스폰될 위치
        public float minSpawnTime = 2f; // 스폰 시간 간격의 최소값 (초)
        public float maxSpawnTime = 5f; // 스폰 시간 간격의 최대값 (초)

        private void Start()
        {
            // 몬스터 스폰을 시작하는 코루틴 호출
            StartCoroutine(SpawnMonstersRoutine());
        }

        private IEnumerator SpawnMonstersRoutine()
        {
            while (true)
            {
                // a와 b 사이의 랜덤 시간 대기
                float spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(spawnInterval);

                // 랜덤 몬스터 스폰
                SpawnRandomMonster();
            }
        }

        public void SpawnRandomMonster()
        {
            Monster selectedMonster = GetRandomMonster();
            if (selectedMonster != null)
            {
                Instantiate(selectedMonster.prefab, spawnPoint.position, spawnPoint.rotation);
            }
        }

        private Monster GetRandomMonster()
        {
            // 총 가중치를 계산
            float totalWeight = 0f;
            foreach (Monster monster in monsters)
            {
                totalWeight += monster.weight;
            }

            // 0에서 총 가중치 사이의 랜덤 값 생성
            float randomValue = Random.Range(0, totalWeight);

            // 랜덤 값을 기준으로 몬스터 선택
            float cumulativeWeight = 0f;
            foreach (Monster monster in monsters)
            {
                cumulativeWeight += monster.weight;
                if (randomValue < cumulativeWeight)
                {
                    return monster;
                }
            }

            // 기본적으로 null 반환 (이 경우는 발생하지 않아야 함)
            return null;
        }
    }

}
