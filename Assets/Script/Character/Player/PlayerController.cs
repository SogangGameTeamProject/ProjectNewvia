using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerController : CharacterInit
    {
        public float dashStaminaCoast { get; set; }
        public float staminaRecoveryAmount { get; set; }
        public float staminaRecoveryCycle = 0.25f;
        public float maxStamina { get; set; }
        private float nowStamina { get; set; }
        public float NowStamina
        {
            get
            {
                return nowStamina;
            }
            set
            {
                nowStamina = Mathf.Clamp(value, 0, maxStamina);
            }
        }

        public GameObject _weaponePre = null;//장착할 무기 프리펩
        public GameObject _weapone { get; set;}//장착한 무기
        public Transform _weaponMountingLocation = null;//무기 장착 위치


        protected override void Start()
        {
            base.Start();
            StartCoroutine(StaminaRecovery());
            //메인 웨폰 및 서브 웨폰 장착
            if (_weaponePre)
            {
                _weapone = Instantiate(_weaponePre, _weaponMountingLocation.position, Quaternion.identity, this.transform);
            }
                
        }

        protected override void SettingInitStatus()
        {
            base.SettingInitStatus();

            //플레이어 능력치값 초기화
            dashStaminaCoast = ((PlayerStatus)_statusInit).DashStaminaCoast;
            staminaRecoveryAmount = ((PlayerStatus)_statusInit).StaminaRecoveryAmount;
            maxStamina = ((PlayerStatus)_statusInit).MaxStamina;
            nowStamina = maxStamina;
        }

        //스태미나 회복 구현 코루틴 함수
        IEnumerator StaminaRecovery()
        {
            while (true)
            {
                yield return new WaitForSeconds(staminaRecoveryCycle);
                NowStamina += staminaRecoveryAmount;
            }
        }
    }
}

