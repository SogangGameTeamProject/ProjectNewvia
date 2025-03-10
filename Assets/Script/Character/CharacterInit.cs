using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Newvia
{
    public class CharacterInit : Subject, IHit
    {
        private GameManager _gameManager;
        private CharacterStateContext _stateContext;//상태 콘텍스트
        public CharacterStateType startState = CharacterStateType.Idle;
        public CharacterStateType runningStateType;
        //상태 관리를 위한 리스트
        [System.Serializable]
        private class stateInfo
        {
            public CharacterStateType stateType;
            public CharacterStateBase state;
        }
        [SerializeField]
        private List<stateInfo> _stateList = new List<stateInfo>();

        [SerializeField]
        protected CharacterStatus _statusInit = null;//초기 설정된 능력치 값
        public int maxHp {  get; private set; }//최대 체력
        public int nowHp { get; private set; }//현재 체력
        //현제 체력 설정
        private int NowHp
        {
            get
            {
                return nowHp;
            }
            set
            {
                if (!isInvincible)
                {
                    nowHp = Mathf.Clamp(value, 0, maxHp);
                    if (nowHp == 0)
                        StateTransition(CharacterStateType.Death);
                }
            }
        }
        public int power {  get; private set; }//공격력
        public float moveSpeed {  get; private set; }//스피드

        public bool isInvincible = false;//무적 여부
        //캐릭터 방향
        private CharacterDirection characterDirection = CharacterDirection.right;
        public CharacterDirection CharacterDirection
        {
            get
            {
                return characterDirection;
            }
            set
            {
                characterDirection = value;
                if (characterDirection == CharacterDirection.right)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                    
            }
        }

        protected virtual void Start()
        {
            _gameManager = GameManager.Instance;
            _stateContext = new CharacterStateContext(this);
            StateInit(startState);
            SettingInitStatus();//캐릭터 스테이터스 초기화
        }

        private void FixedUpdate()
        {
            _stateContext.StateUpdate();
        }

        //캐릭터 초기 스테이터스 값 적용하는 함수
        protected virtual void SettingInitStatus()
        {
            if (_statusInit)
            {
                maxHp = _statusInit.hp;
                nowHp = maxHp;
                power = _statusInit.power;
                moveSpeed = _statusInit.moveSpeed;
            }
        }

        public virtual void OnHit(int hitType = 0)
        {
            
            if(NowHp > 0 && !isInvincible)
            {
                NowHp--;
            }
                
        }

        //캐릭터 상태 초기화
        public virtual void StateInit(CharacterStateType type)
        {
            CharacterState state = null;
            stateInfo findState = _stateList.Find(state => state.stateType == type);
            if (findState != null)
            {
                state = findState.state.GetComponent<CharacterState>();
                runningStateType = findState.stateType;
                _stateContext.Initialize(state);
            }
        }

        //캐릭터상태 전환
        public virtual void StateTransition(CharacterStateType type)
        {
            CharacterState state = null;
            stateInfo findState = _stateList.Find(state => state.stateType == type);
            if (findState != null)
            {
                state = findState.state.GetComponent<CharacterState>();
                runningStateType = findState.stateType;
                _stateContext.TransitionTo(state);
            }
        }
    }
}

