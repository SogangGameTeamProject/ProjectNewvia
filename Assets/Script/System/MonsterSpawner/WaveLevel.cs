using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [System.Serializable]
    public class WaveLevel
    {
        public List<Monster> monsterList;//스폰할 몬스터 리스트
        public int maxSpawnNum = 30;//최대로 소환할 몬스터 수
        public int spawnMonsterNum = 3;//주기별 스폰할 몬스터 수
        public int maxFieldMonsterNum = 15;//필드에 존재 가능한 최대 몬스터 수
        public float minSpawnTime = 3f;//최소 스폰 딜레이 시간
        public float maxSpawnTime = 5f;//최대 스폰 딜레이 시간
    }
}