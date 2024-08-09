using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class InputHandler : MonoBehaviour
    {
        private GameManager _gameManager;
        PlayerController _playerController = null;
        public PlayerCommandInit _moveCommand;
        public PlayerCommandInit _dashCommand;
        public WeaponeCommandInit _fireCommand;
        public WeaponeCommandInit _reloadCommand;
        public SubWeaponeCommandInit _skillCommand;

        public KeyCode dashKey;
        public KeyCode fireKey;
        public KeyCode reloadKey;
        public KeyCode skillKey;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            HandleInput();
        }


        //입력 처리 메소드
        private void HandleInput()
        {
            if (_gameManager.flowType != GameFlowType.Proceeding)
                return;
            //이동 입력 처리
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if(_moveCommand)
                    _moveCommand.Execute(_playerController);
            }
            //대쉬 입력 처리
            if (Input.GetKeyDown(dashKey))
            {
                if (_dashCommand)
                    _dashCommand.Execute(_playerController);
            }
            //무기 발사 입력 처리
            if (Input.GetKeyDown(fireKey))
            {
                if (_fireCommand)
                    _fireCommand.Execute(_playerController);
            }
            //무기 재장전 입력 처리
            if (Input.GetKeyDown(reloadKey))
            {
                if (_reloadCommand)
                    _reloadCommand.Execute(_playerController);
            }
            //스킬 입력 처리
            if(Input.GetKeyDown(skillKey))
            {
                if (_skillCommand)
                    _skillCommand.Execute(_playerController);
            }
        }
    }

}

