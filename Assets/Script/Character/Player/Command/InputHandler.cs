using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerCommandInit _moveCommand;

        private void Start()
        {
            PlayerController _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            HandleInput();
        }


        //입력 처리 메소드
        private void HandleInput()
        {
            //이동 입력 처리
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0 || verticalInput != 0)
            {
                if(_moveCommand)
                    _moveCommand.Execute();
            }
        }
    }

}

