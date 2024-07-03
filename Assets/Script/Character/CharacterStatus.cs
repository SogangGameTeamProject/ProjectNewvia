using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [CreateAssetMenu(fileName = "New Chacter", menuName = "Status/Create Character", order = 0)]
    public class CharacterStatus : ScriptableObject
    {
        [Header("캐릭터 스테이터스 값 설정")]
        [Tooltip("캐릭터 명")]
        public string characterName = "";
        [Tooltip("최대체력")]
        public int hp = 1;
        [Tooltip("공격력")]
        public int power = 1;
        [Tooltip("이동속도")]
        public float moveSpeed = 10;
    }
}