using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MonsterSpawnManager : Singleton<MonsterSpawnManager>
    {
        private GameManager _gameManager = null;
        
        //몬스터 정보
        [System.Serializable]
        public class Monster
        {
            public GameObject prefab; // 몬스터의 프리팹
            public float weight;      // 몬스터의 가중치
        }

        public List<Monster> monsters; // 스폰할 몬스터 리스트
        public List<Transform> spawnPoints;   // 몬스터가 스폰될 위치
        public int spawnMonsterNum = 3;
        public int maxMonsterNum = 15;
        public float minSpawnTime = 3f;
        public float maxSpawnTime = 5f;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            StartCoroutine(SpawnMonstersRoutine());
        }

        //
        private IEnumerator SpawnMonstersRoutine()
        {
            
            int nowMonsterCnt = _gameManager.nowMonsterCount;
            while (true)
            {
                if(nowMonsterCnt < maxMonsterNum && _gameManager.flowType == GameFlowType.Proceeding)
                {
                    // a와 b 사이의 랜덤 시간 대기
                    float spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
                    yield return new WaitForSeconds(spawnInterval);

                    // 랜덤 몬스터 스폰
                    SpawnRandomMonster();
                }
                yield return null;
            }
        }

        //몬스터를 스폰 함수     cnt: 스폰할 몬스터 수
        public void SpawnRandomMonster()
        {
            PlayerController player = GameObject.FindAnyObjectByType<PlayerController>();
            if(player != null)
            {
                //플레이어 위치 값을 기반으로 스포너 위치별 거리 구하기
                SortedDictionary<float, Transform> spawnList = new SortedDictionary<float, Transform>();
                
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    float distance = Vector2.Distance(player.transform.position, spawnPoints[i].position);
                    spawnList.Add(distance, spawnPoints[i]);
                }

                //구한 위치의 거리순으로 몬스터 랜덤 스폰
                int j = 0;
                foreach (Transform t in spawnList.Values)
                {
                    if (_gameManager.nowMonsterCount >= maxMonsterNum || j >= spawnMonsterNum)
                        break;
                    if (j < spawnPoints.Count)
                    {
                        _gameManager.nowMonsterCount++;
                        Instantiate(GetRandomMonster().prefab, t.position, Quaternion.identity, null);
                    }
                    j++;
                }
            }
        }

        //가중 치에 따른 몬스터 랜덤 선택
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
