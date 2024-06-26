using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public abstract class CharacterInit : Subject
    {
        private CharacterStateContext _stateContext;
        [SerializeField]
        private CharacterStatus _statusInit = null;//초기 설정된 능력치 값
        public int maxHp {  get; private set; }//최대 체력
        public int nowHp { get; private set; }//현재 체력
        //현제 체력 설정
        public int setHp
        {
            set
            {
                nowHp = Mathf.Clamp(value, 0, maxHp);
            }
        }
        public int power {  get; private set; }//공격력
        public float moveSpeed {  get; private set; }//스피드


        public virtual void Start()
        {
            SettingInitStatus();//캐릭터 스테이터스 초기화
        }

        //캐릭터 초기 스테이터스 값 적용하는 함수
        private void SettingInitStatus()
        {
            if (_statusInit)
            {
                maxHp = _statusInit.hp;
                nowHp = maxHp;
                power = _statusInit.power;
                moveSpeed = _statusInit.moveSpeed;
            }
        }

        //현재 캐릭터의 상태 전환
        public abstract void StateTransition(StateBase state);
    }
}

