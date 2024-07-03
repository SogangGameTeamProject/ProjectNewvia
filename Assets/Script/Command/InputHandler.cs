using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class InputHandler : MonoBehaviour
    {
        PlayerController _playerController = null;
        public PlayerCommandInit _moveCommand;
        public PlayerCommandInit _dashCommand;

        public KeyCode dahKey;
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
            //대쉬 입력 처리
            if (Input.GetKeyDown(dahKey))
            {
                if (_dashCommand)
                    _dashCommand.Execute(_playerController);
            }
        }
    }

}

