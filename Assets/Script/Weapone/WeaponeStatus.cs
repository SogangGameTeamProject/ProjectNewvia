using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Newvia
{
    [CreateAssetMenu(fileName = "New Weapone", menuName = "WeaponeStatus/Create Weapone", order = 0)]
    public class WeaponeStatus : ScriptableObject
    {
        [Header("무기 스테이터스 값 설정")]
        [Tooltip("무기 이름")]
        public string WeaponeName = "";
        [Tooltip("최대장탄수")]
        public float MaxMagazineCapacity = 8;//최대장탄수
        [Tooltip("발사 시 소모량")]
        public float FireCost = 1;//발사 코스트
        [Tooltip("발사 속도(초)")]
        public float RateOfFire = 0.5f;//발사 속도(초)
        [Tooltip("발사 시 이동속도 감소량(%)")]
        public float FireSpeedReduction = 0;//발사 시 이동속도 감소량
        [Tooltip("재장전 속도(초)")]
        public float ReloadingSpeed = 0.75f;//재장전 속도(초)
        [Tooltip("장전 시 이동속도 감소량(%)")]
        public float ReloadingSpeedReduction = 0.5f;//장전시 이동적도 감소량(%)
    }
}

