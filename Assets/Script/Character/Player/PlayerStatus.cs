using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    [CreateAssetMenu(fileName = "New Chacter", menuName = "Status/Create Player", order = 1)]
    public class PlayerStatus : CharacterStatus
    {
        public float DashStaminaCoast = 3;
        public float MaxStamina = 3;
        public float StaminaRecoveryAmount = 0.05f;
    }
}

