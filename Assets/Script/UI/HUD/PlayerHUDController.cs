using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Newvia
{
    public class PlayerHUDController : Observer
    {
        private PlayerController _playerController = null;
        public Image staminaBar; // 스태미나 바 이미지
        private float maxStamina = 0; // 최대 스태미나
        private float currentStamina; // 현재 스태미나
        
        public void UpdateStamina()
        {
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            float fillAmount = currentStamina / maxStamina;
            staminaBar.fillAmount = fillAmount;
        }

        public override void Notify(Subject subject)
        {
            if (_playerController == null)
                _playerController = subject.GetComponent<PlayerController>();

            if (_playerController)
            {
                //스태미나 업데이트
                maxStamina = _playerController.maxStamina;
                currentStamina = _playerController.NowStamina;
                UpdateStamina();
            }
            
        }
    }
}

