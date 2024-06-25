using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class PlayerCommandInit : MonoBehaviour, ICommand
    {
        protected PlayerController _player;
        [SerializeField]
        protected StateInit runnigState = null;//Ŀ�ǵ� �Է� �� ������ ����

        public PlayerCommandInit(PlayerController player)
        {
            _player = player;
        }

        public virtual void Execute()
        {
            if(runnigState)
                _player.StateTransition(runnigState);
        }
    }
}

