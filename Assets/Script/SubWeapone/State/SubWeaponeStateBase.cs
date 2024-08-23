using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Newvia
{
    public abstract class SubWeaponeStateBase : MonoBehaviour, SubWeaponeState
    {
        protected SubWeapone _subWeapone = null;
        //사운드 재생
        private SoundManger _soundManger;
        public AudioClip bgmClip = null;
        public AudioClip sfxClip = null;

        public virtual void Enter(SubWeapone subWeapone)
        {
            if (_subWeapone == null)
                _subWeapone = subWeapone;
            _subWeapone.GetComponent<NavMeshAgent>().velocity = Vector2.zero;

            //사운드 재생
            if (_soundManger == null)
                _soundManger = SoundManger.Instance;
            if (_soundManger && bgmClip)
                _soundManger.PlayBGM(bgmClip);
            if (_soundManger && sfxClip)
                _soundManger.PlaySFX(sfxClip);
        }

        //상태 종료 시 처리
        public abstract void Exit();

        public abstract void StateUpdate();
    }
}

