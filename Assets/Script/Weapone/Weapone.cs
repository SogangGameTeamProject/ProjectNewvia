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
        public float _maxMagazineCapacity { get; private set; }
        public float _fireCost { get; private set; }
        public float _rateOfFire { get; private set; }
        public float _fireSpeedReduction { get; private set; }
        public float _reloadingSpeed { get; private set; }
        public float _reloadingSpeedReduction { get; private set; }
        public float _bulletPower { get; private set; }
        public float _bulletRange { get; private set; }
        public float _magazineCapacity { get; private set; }
        public float MagazineCapacity
        {
            get
            {
                return _magazineCapacity;
            }
            set
            {
                _magazineCapacity = Mathf.Clamp(value, 0, _maxMagazineCapacity);
                NotifyObservers();
            }
        }

        public Transform _firePoint = null;

        private WeaponeHUDController _hUDController = null;

        private void Awake()
        {
            _hUDController = FindObjectOfType<WeaponeHUDController>();
        }
        private void Start()
        {
            transform.parent.TryGetComponent<PlayerController>(out _playerController);
            _stateContext = new WeaponeStateContext(this);
            StateInit(WeaponeStateType.Idle);
            SettingInitStatus();//캐릭터 스테이터스 초기화
        }

        private void OnEnable()
        {
            if (_hUDController)
                Attach(_hUDController);
        }

        private void OnDisable()
        {
            if (_hUDController)
                Detach(_hUDController);
        }

        private void Update()
        {
            _stateContext.StateUpdate();
            //마우스 방향에 따른 무기 회전 및 캐릭터 좌우 반전
            RotateWeapon();
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
                MagazineCapacity = _maxMagazineCapacity;
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
            CharacterStateType runningStateType = _playerController.runningStateType;
            if (runningStateType == CharacterStateType.Idle ||
                runningStateType == CharacterStateType.Move ||
                runningStateType == CharacterStateType.AttackSkill
                )
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Rotate the weapon
                transform.rotation = Quaternion.Euler(0, 0, angle);

                // Flip the player based on mouse position
                if (mousePosition.x < _playerController.transform.position.x)
                {
                    this.transform.localScale = new Vector3(-1, -1, 1);
                    _playerController.CharacterDirection = CharacterDirection.left;
                }
                else
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                    _playerController.CharacterDirection = CharacterDirection.right;
                }
            }
        }
    }
}