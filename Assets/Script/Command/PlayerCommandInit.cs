using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerCommandInit : MonoBehaviour, ICommand
    {
        protected PlayerController _player;
        [SerializeField]
        protected CharacterStateType runnigStatetype = CharacterStateType.Null;//커맨드 입력 시 실행할 상태

        public virtual void Execute(PlayerController player)
        {
            if (_player == null)
                _player = player;
            if (runnigStatetype >= 0)
                _player.StateTransition(runnigStatetype);
        }
    }
}

