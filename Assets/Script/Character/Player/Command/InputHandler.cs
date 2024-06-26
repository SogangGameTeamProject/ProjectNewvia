using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class InputHandler : MonoBehaviour
    {
        PlayerController _playerController = null;
        public PlayerCommandInit _moveCommand;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            HandleInput();
        }


        //입력 처리 메소드
        private void HandleInput()
        {
            //이동 입력 처리
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if(_moveCommand)
                    _moveCommand.Execute(_playerController);
            }
        }
    }

}

