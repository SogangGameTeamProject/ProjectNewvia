using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class MonsterSpawnManager : Singleton<MonsterSpawnManager>
    {
        private GameManager _gameManager = null;

        public List<Monster> monsterList; // 스폰할 몬스터 리스트
        public List<Transform> spawnPoints;   // 몬스터가 스폰될 위치 리스트
        public int maxSpawnNum = 30;//최대로 소환할 몬스터 수
        private int spawnCnt = 0;//소환한 몬스터 수
        public int spawnMonsterNum = 3;//주기별 스폰할 몬스터 수
        public int maxFieldMonsterNum = 15;//필드에 존재 가능한 최대 몬스터 수
        public int nowFieldMonsterCnt { get; set; }//현재 필드의 몬스터 카운트
        public float minSpawnTime = 3f;//최소 스폰 딜레이 시간
        public float maxSpawnTime = 5f;//최대 스폰 딜레이 시간

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        //몬스터 스폰 활성화 
        public void OnSpawner(List<Monster> monsterList, int maxSpawnNum, int spawnMonsterNum,
            int maxFieldMonsterNum, float minSpawnTime, float maxSpawnTime)
        {
            //스포너 설정 초기화
            this.monsterList = monsterList;
            this.maxSpawnNum = maxSpawnNum;
            this.spawnMonsterNum = spawnMonsterNum;
            this.maxFieldMonsterNum = maxFieldMonsterNum;
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            spawnCnt = 0;
            nowFieldMonsterCnt = 0;

            StartCoroutine(SpawnMonstersRoutine());
        }

        //주기별로 폰스터를 스폰하는 코루틴 함수
        private IEnumerator SpawnMonstersRoutine()
        {
            while (true)
            {
                //웨이브 종료 처리
                if (spawnCnt >= maxSpawnNum && nowFieldMonsterCnt == 0 
                    && _gameManager.flowType == GameFlowType.Proceeding)
                {
                    GameFlowEventBus.Publish(GameFlowType.NextWave);
                    yield break;
                }
                else if((nowFieldMonsterCnt < maxFieldMonsterNum && spawnCnt < maxSpawnNum) 
                    && _gameManager.flowType == GameFlowType.Proceeding)
                {
                    Debug.Log("스폰 처리중");
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
                    if (nowFieldMonsterCnt >= maxFieldMonsterNum || j >= spawnMonsterNum || spawnCnt >= maxSpawnNum)
                        break;
                    if (j < spawnPoints.Count)
                    {
                        nowFieldMonsterCnt++;
                        spawnCnt++;
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
            foreach (Monster monster in monsterList)
            {
                totalWeight += monster.weight;
            }

            // 0에서 총 가중치 사이의 랜덤 값 생성
            float randomValue = Random.Range(0, totalWeight);

            // 랜덤 값을 기준으로 몬스터 선택
            float cumulativeWeight = 0f;
            foreach (Monster monster in monsterList)
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
