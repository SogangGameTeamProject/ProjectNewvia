using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Newvia
{
    public class SubWeapone : Subject
    {
        public PlayerController _playerController { get; private set; }
        private SubWeaponeStateContext _stateContext;//상태 콘텍스트
        //상태 관리를 위한 리스트
        [System.Serializable]
        private class stateInfo
        {
            public SubWeaponeStateType stateType;
            public SubWeaponeStateBase state;
        }
        [SerializeField]
        private List<stateInfo> _stateList = new List<stateInfo>();
        public SubWeaponeStateType runningStateType { get; private set; }

        //서브 웨폰 스테이터스
        [SerializeField]
        private SubWeaponeStatus _statusInit = null;
        public int _maximumCharge { get; private set; }
        public int _energyConsumed { get; private set; }
        public int _nowEnergy { get; private set; }
        public int SetNowEnergy
        {
            get
            {
                return _nowEnergy;
            }
            set
            {
                _nowEnergy = Mathf.Clamp(value, 0, (_energyConsumed * _maximumCharge));
                NotifyObservers();
            }
        }
        private NavMeshAgent agent;

        private SubWeaponeHUDController _subWeaponeHUD = null;

        private void Awake()
        {
            _subWeaponeHUD = GameObject.FindObjectOfType<SubWeaponeHUDController>();
        }

        private void Start()
        {
            _playerController = GameObject.FindObjectOfType<PlayerController>();
            _stateContext = new SubWeaponeStateContext(this);
            StateInit(SubWeaponeStateType.Idle);
            NavMeshInit();  
            SettingInitStatus();
            transform.rotation = Quaternion.identity;
        }

        private void OnEnable()
        {
            if (_subWeaponeHUD)
                Attach(_subWeaponeHUD);
        }

        private void OnDisable()
        {
            if (_subWeaponeHUD)
                Detach(_subWeaponeHUD);
        }

        private void Update()
        {
            _stateContext.StateUpdate();
        }

        //캐릭터 상태 초기화
        public void StateInit(SubWeaponeStateType type)
        {
            SubWeaponeState state = null;
            stateInfo findState = _stateList.Find(state => state.stateType == type);
            if (findState != null)
            {
                state = findState.state.GetComponent<SubWeaponeState>();
                runningStateType = findState.stateType;
                _stateContext.Initialize(state);
            }
        }

        private void NavMeshInit()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateUpAxis = false;
            agent.updateRotation = false;

        }

        //캐릭터상태 전환
        public void StateTransition(SubWeaponeStateType type)
        {
            SubWeaponeState state = null;
            stateInfo findState = _stateList.Find(state => state.stateType == type);
            if (findState != null)
            {
                state = findState.state.GetComponent<SubWeaponeState>();
                runningStateType = findState.stateType;
                _stateContext.TransitionTo(state);
            }
        }

        //서브 무기 초기 스테이터스 값 적용하는 함수
        protected virtual void SettingInitStatus()
        {
            if (_statusInit)
            {
                _maximumCharge = _statusInit.MaximumCharge;
                _energyConsumed = _statusInit.EnergyConsumed;
                _nowEnergy = 0;
                if (_subWeaponeHUD)
                    _subWeaponeHUD.CreateSkillHUD(this);//서브 웨폰 HUD 생성
            }
        }
    }
}