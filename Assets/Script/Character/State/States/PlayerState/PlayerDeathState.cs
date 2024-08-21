using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerDeathState : DeathState
    {
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            PlayerController player = (PlayerController)_character;
            if (player)
                player._weapone.SetActive(false);
        }
    }
 }