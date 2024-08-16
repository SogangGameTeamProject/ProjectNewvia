using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Newvia
{
    public class MonsterSpawnManager : Singleton<MonsterSpawnManager>
    {
        private GameManager _gameManager = null;

        public List<Monster> monsterList; // 스폰할 몬스터 리스트

        //보스 스폰 정보
        public bool isBoss = false;
        public Transform bossSpawnPosition = null;
        //스포너 정보값
        [System.Serializable]
        public class Spawner {
            public Transform pos;//위치
            public float weight { get; set; }//가중치
        }

        public List<Spawner> spawnerList;   //스포너 정보를 저장하는 리스트
        public int maxSpawnNum { get; set; }//최대로 소환할 몬스터 수
        private int spawnCnt { get; set; }//소환한 몬스터 수
        public int spawnMonsterNum { get; set; }//주기별 스폰할 몬스터 수
        public int maxFieldMonsterNum { get; set; }//필드에 존재 가능한 최대 몬스터 수
        public int nowFieldMonsterCnt { get; set; }//현재 필드의 몬스터 카운트
        public float minSpawnTime { get; set; }//최소 스폰 딜레이 시간
        public float maxSpawnTime { get; set; }//최대 스폰 딜레이 시간

        public float increaseSpawnerNum = 3;//가중치 증가할 스포너 수
        public float defaultWeight = 1;//기본 가중치
        public float weightIncrease = 5f;//스포너 가중치 증가량

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        //몬스터 스폰 활성화 
        public void OnSpawner(bool isBoss, List<Monster> monsterList, int maxSpawnNum, int spawnMonsterNum,
            int maxFieldMonsterNum, float minSpawnTime, float maxSpawnTime)
        {
            //스포너 설정 초기화
            this.isBoss = isBoss;
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
                    // a와 b 사이의 랜덤 시간 대기
                    float spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
                    yield return new WaitForSeconds(spawnInterval);
                    //보스 몬스터 스폰
                    if (isBoss)
                        SpawnBossMonster();
                    //일반 몬스터 랜덤 스폰
                    else
                        SpawnRandomMonster();
                    
                }
                yield return null;
            }
        }

        //보스 몬스터 스폰 이벤트 처리
        public void SpawnBossMonster()
        {
            spawnCnt++;
            nowFieldMonsterCnt++;
            Instantiate(GetRandomMonster().prefab, bossSpawnPosition.position, Quaternion.identity, null);
        }

        //몬스터를 생성하는 함수
        public void SpawnRandomMonster()
        {
            PlayerController player = GameObject.FindAnyObjectByType<PlayerController>();
            if(player != null)
            {
                // 스포너와 거리를 저장할 리스트
                var spawnerDistances = spawnerList
                    .Select(spawner => 
                    new { spawner, distance = Vector2.Distance(spawner.pos.position, player.transform.position) })
                    .OrderBy(item => item.distance) // 거리로 정렬
                    .ToList();

                // 가까운 스포너 n개에 가중치 3 부여
                for (int i = 0; i < spawnerDistances.Count; i++)
                {
                    if (i < increaseSpawnerNum)
                    {
                        spawnerDistances[i].spawner.weight = weightIncrease;
                    }
                    else
                    {
                        spawnerDistances[i].spawner.weight = defaultWeight;
                    }
                }

                int overSpawnNum = (spawnCnt + spawnMonsterNum) - maxSpawnNum;//오버 스폰 수
               
                int roofNum = overSpawnNum > 0 ? spawnMonsterNum - overSpawnNum : spawnMonsterNum;//스폰 반복 횟수
                Debug.Log("spawnCnt:" + spawnCnt + ", spawnMonsterNum:" + spawnMonsterNum + ", overSpawnNum:" + overSpawnNum + ", roofNum: " + roofNum);
                // 가중치에 따라 랜덤으로 스포너 선택하고 몬스터 스폰
                for (int i = 0; i < roofNum; i++)
                {
                    if (nowFieldMonsterCnt+1 > maxFieldMonsterNum)
                        return;
                    Spawner selectedSpawner = GetRandomSpawnerByWeight();
                    if (selectedSpawner != null)
                    {
                        nowFieldMonsterCnt++;
                        spawnCnt++;
                        Instantiate(GetRandomMonster().prefab, selectedSpawner.pos.position, Quaternion.identity, null);
                    }
                }
            }
        }

        //가중치에 따른 랜덤 스포너 선택
        Spawner GetRandomSpawnerByWeight()
        {
            float totalWeight = spawnerList.Sum(spawner => spawner.weight);
            float randomWeightPoint = Random.Range(0, totalWeight);

            float currentWeightSum = 0;
            foreach (var spawner in spawnerList)
            {
                currentWeightSum += spawner.weight;
                if (randomWeightPoint <= currentWeightSum)
                {
                    return spawner;
                }
            }

            return null;
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
