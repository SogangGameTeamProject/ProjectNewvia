using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class FinishAttackState : SkillStateBase
    {
        //2단 줌
        private bool isSecondZoom = false;
        public float secondZoomStartTime = 1f;
        public float secondZoomScale = 110f;

        //3단 줌
        private bool isThirdZoom = false;
        public float thirdZoomStartTime = 2f;
        public float thirdZoomScale = 90f;

        private bool isChaging = false;
        public bool isFinishiAni = false;
        public bool isSecontSahke = false;

        public AudioClip attakSound = null;
        private bool isPlayAttackSound = false;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            isChaging = false;
            isFinishiAni = false;
        }

        protected override void HandleFirstDeal()
        {
            if (elapsedTime >= secondZoomStartTime && !isSecondZoom)
            {
                isSecondZoom = true;
                Debug.Log("두번쨰 줌인");
                StartCoroutine(ZoomIn(secondZoomScale));
            }
                
            if (elapsedTime >= thirdZoomStartTime && !isThirdZoom)
            {
                isThirdZoom = true;
                Debug.Log("세번째 줌인");
                StartCoroutine(ZoomIn(thirdZoomScale));
            }
                

        }

        protected override void HandleSkill()
        {
            EndCutScene();//컷씬 종료

        }

        protected override void HandleLastDeal()
        {
            if (isShake && !isSecontSahke)
            {
                isSecontSahke = true;
                StartCoroutine(CameraShake());
            }

            //러쉬 사운드 재생
            if (_soundManger && attakSound && !isPlayAttackSound)
            {
                isPlayAttackSound = true;
                _soundManger.PlaySFX(attakSound);
            }

            if (!isChaging)
                isChaging = true;

            StartCutScene();//컷씬 시작
            //피니쉬 애니메이션 재생
            GameManager.Instance.OnPlayFinishAni();
        }

        protected override void OnSkillEnd()
        {
            if (isChaging)
            {
                //플레이어 사망 처리
                PlayerController player = GameObject.FindObjectOfType<PlayerController>();
                if (player)
                {
                    player.GetComponent<IHit>().OnHit();
                }
            }
            
        }
    }
}

