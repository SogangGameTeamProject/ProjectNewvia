using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [CreateAssetMenu(fileName = "New SubWeapone", menuName = "SubWeaponeStatus/Create SubWeapone", order = 0)]
    public class SubWeaponeStatus : ScriptableObject
    {
        [Header("무기 스테이터스 값 설정")]
        [Tooltip("무기 이름")]
        public string SubWeapone = "";

        [Tooltip("최대 충전량")]
        public int MaximumCharge = 2;
        [Tooltip("최대 충전량")]
        public int EnergyConsumed = 5;
    }

}
