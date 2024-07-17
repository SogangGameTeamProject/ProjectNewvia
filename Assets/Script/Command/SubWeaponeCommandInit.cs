using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Newvia
{
    public class SubWeaponeCommandInit : MonoBehaviour, ICommand
    {
        protected PlayerController _player;
        protected SubWeapone _subWeapone;
        [SerializeField]
        protected SubWeaponeStateType runnigStatetype = SubWeaponeStateType.Null;//커맨드 입력 시 실행할 상태

        public virtual void Execute(PlayerController player)
        {
            if (_player == null)
                _player = player;
            if (_subWeapone == null)
                _player._subWeapone.TryGetComponent<SubWeapone>(out _subWeapone);
        }
    }
}

