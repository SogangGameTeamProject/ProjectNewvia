using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [CreateAssetMenu(fileName = "New Chacter", menuName = "Status/Create Player", order = 1)]
    public class PlayerStatus : CharacterStatus
    {
        [Tooltip("최대 스테미나")]
        public float MaxStamina = 3;
        [Tooltip("스테미나 회복량")]
        public float StaminaRecoveryAmount = 0.05f;
        [Tooltip("대쉬 시 스테미나 소모량")]
        public float DashStaminaCoast = 3;
    }
}

