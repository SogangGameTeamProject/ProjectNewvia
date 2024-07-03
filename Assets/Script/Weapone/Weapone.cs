using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class Weapone : Subject
    {
        private PlayerController _playerController;
        private WeaponeStateContext _stateContext;//상태 콘텍스트
        //상태 관리를 위한 리스트
        [System.Serializable]
        private class stateInfo
        {
            public WeaponStateType stateType;
            public WeaponeStateBase state;
        }
        [SerializeField]
        private List<stateInfo> _stateList = new List<stateInfo>();
        public WeaponStateType runningStateType { get; private set; }

        //무기 스테이터스
        [SerializeField]
        private WeaponeStatus _statusInit = null;

        private void Start()
        {
            transform.parent.TryGetComponent<PlayerController>(out _playerController);
            _stateContext = new WeaponeStateContext(this);
            StateInit(WeaponStateType.Idle);
            SettingInitStatus();//캐릭터 스테이터스 초기화
        }

        private void Update()
        {
            //마우스 방향에 따른 무기 회전 및 캐릭터 좌우 반전
            RotateWeapon();
            RotatePlayer();
        }

        //무기 초기 스테이터스 값 적용하는 함수
        protected virtual void SettingInitStatus()
        {
            
        }

        //캐릭터 상태 초기화
        public void StateInit(WeaponStateType type)
        {
            WeaponeState state = null;
            stateInfo findState = _stateList.Find(state => state.stateType == type);
            if (findState != null)
            {
                state = findState.state.GetComponent<WeaponeState>();
                runningStateType = findState.stateType;
                _stateContext.Initialize(state);
            }
        }

        //캐릭터상태 전환
        public void StateTransition(WeaponStateType type)
        {
            WeaponeState state = null;
            stateInfo findState = _stateList.Find(state => state.stateType == type);
            if (findState != null)
            {
                state = findState.state.GetComponent<WeaponeState>();
                runningStateType = findState.stateType;
                _stateContext.TransitionTo(state);
            }
        }

        //마우스 방향에 따른 무기 회전
        private void RotateWeapon()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the weapon
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Flip the player based on mouse position
            if (mousePosition.x < _playerController.transform.position.x)
            {
                _playerController.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _playerController.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        //마우스 방향에 따른 플레이어 좌우 전환
        private void RotatePlayer()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - _playerController.transform.position;

            // Flip the player based on mouse position
            if (mousePosition.x < _playerController.transform.position.x)
            {
                _playerController.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _playerController.transform.localScale = new Vector3(1, 1, 1);
            }

            // Ensure the weapon is on the correct side
            if (_playerController.transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(-1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}