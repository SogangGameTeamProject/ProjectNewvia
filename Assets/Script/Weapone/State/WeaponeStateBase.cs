using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class WeaponeStateBase : MonoBehaviour, WeaponeState
    {
        protected Weapone _weapone;
        //사운드 재생
        private SoundManger _soundManger;
        public AudioClip bgmClip = null;
        public AudioClip sfxClip = null;

        //상태 전환 시 처리
        public virtual void Enter(Weapone weapone)
        {
            if (_weapone == null)
                _weapone = weapone;

            //사운드 재생
            if (_soundManger == null)
                _soundManger = SoundManger.Instance;
            if (_soundManger && bgmClip)
                _soundManger.PlayBGM(bgmClip);
            if (_soundManger && sfxClip)
                _soundManger.PlaySFX(sfxClip);
        }

        public abstract void StateUpdate();

        //상태 종료 시 처리
        public abstract void Exit();
    }
}