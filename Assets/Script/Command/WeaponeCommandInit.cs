using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Newvia
{
    public class WeaponeCommandInit : MonoBehaviour, ICommand
    {
        protected PlayerController _player;
        protected Weapone _weapone;
        [SerializeField]
        protected WeaponeStateType runnigStatetype = WeaponeStateType.Null;//커맨드 입력 시 실행할 상태

        public virtual void Execute(PlayerController player)
        {
            if (_player == null)
                _player = player;
            if (_weapone == null)
                _player._weapone.TryGetComponent<Weapone>(out _weapone);
        }
    }
}

