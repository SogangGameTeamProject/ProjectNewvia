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
            public WeaponeStateType stateType;
            public WeaponeStateBase state;
        }
        [SerializeField]
        private List<stateInfo> _stateList = new List<stateInfo>();
        public WeaponeStateType runningStateType { get; private set; }

        //무기 스테이터스
        [SerializeField]
        private WeaponeStatus _statusInit = null;
        public float _maxMagazineCapacity { get; set; }
        public float _fireCost { get; set; }
        public float _rateOfFire { get; set; }
        public float _fireSpeedReduction { get; set; }
        public float _reloadingSpeed { get; set; }
        public float _reloadingSpeedReduction { get; set; }
        public float _bulletPower { get; set; }
        public float _bulletRange { get; set; }
        public float _magazineCapacity { get; set; }

        public Transform _firePoint = null;

        private void Start()
        {
            transform.parent.TryGetComponent<PlayerController>(out _playerController);
            _stateContext = new WeaponeStateContext(this);
            StateInit(WeaponeStateType.Idle);
            SettingInitStatus();//캐릭터 스테이터스 초기화
        }

        private void Update()
        {
            _stateContext.StateUpdate();
            //마우스 방향에 따른 무기 회전 및 캐릭터 좌우 반전
            RotateWeapon();
            RotatePlayer();
        }

        //무기 초기 스테이터스 값 적용하는 함수
        protected virtual void SettingInitStatus()
        {
            if (_statusInit)
            {
                _maxMagazineCapacity = _statusInit.MaxMagazineCapacity;
                _fireCost = _statusInit.FireCost;
                _rateOfFire = _statusInit.RateOfFire;
                _fireSpeedReduction = _statusInit.FireSpeedReduction;
                _reloadingSpeed = _statusInit.ReloadingSpeed;
                _reloadingSpeedReduction = _statusInit.ReloadingSpeedReduction;
                _bulletPower = _statusInit.BulletPower;
                _bulletRange = _statusInit.BulletRange;
                _magazineCapacity = _maxMagazineCapacity;
            }
            
        }

        //캐릭터 상태 초기화
        public void StateInit(WeaponeStateType type)
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
        public void StateTransition(WeaponeStateType type)
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