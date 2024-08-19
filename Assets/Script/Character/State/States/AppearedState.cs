using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class AppearedState : CharacterStateBase
    {
        private float elapsedTime = 0;
        public float appearedTime = 2f;
        public CharacterStateType nextState = CharacterStateType.Idle;
        public string virtualCameraName = "VirtualCamera";
        private CinemachineVirtualCamera mainCamera = null;
        private Transform existingTarget = null;
        private bool isAppeared = false;
        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            //값 초기화
            elapsedTime = 0;
            isAppeared = true;
            GameFlowEventBus.Publish(GameFlowType.Pause);
            //가상카메라를 해당 캐릭터를 비추게 설정
            mainCamera = GameObject.Find(virtualCameraName).GetComponent<CinemachineVirtualCamera>();
            if (mainCamera)
            {
                existingTarget = mainCamera.Follow;
                mainCamera.Follow = _character.transform;
            }
            
        }

        public override void StateUpdate()
        {
            
        }

        private void Update()
        {
            if (isAppeared)
            {
                //시간 경과 후 상태로 전환
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= appearedTime)
                    _character.StateTransition(nextState);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
            //가상카메라 타켓 다시 기존 대상으로 재설정
            if (mainCamera)
            {
                mainCamera.Follow = existingTarget;
            }
            //상태 종료 시 처리
            isAppeared = false;
            GameFlowEventBus.Publish(GameFlowType.Proceeding);
        }
    }
 }
