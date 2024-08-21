using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class CharacterStateBase : MonoBehaviour, CharacterState
    {
        protected CharacterInit _character = null;
        protected Animator _animator = null;

        [SerializeField]
        protected string stateAniPara = null; // 재생할 애니메이션의 파라미터

        // 컷씬 관련 전역 변수
        [SerializeField]
        protected bool stateCutScene = false; // 컷씬 사용 여부
        protected bool isCutScene = false;    // 컷씬 재생 여부

        public string virtualCameraName = "VirtualCamera";
        private CinemachineVirtualCamera mainCamera = null;
        private Transform existingTarget = null;

        // 줌 관련 변수
        [SerializeField]
        protected float zoomInFOV = 30f;       // 줌 인 시의 Field of View
        [SerializeField]
        protected float zoomSpeed = 2f;        // 줌 속도
        private float originalFOV = 0f;        // 원래의 Field of View

        // 상태 전환 시 처리
        public virtual void Enter(CharacterInit character)
        {
            if (_character == null)
                _character = character;
            if (_animator == null)
                _animator = character.GetComponent<Animator>();
            if (_animator && stateAniPara != string.Empty)
            {
                _animator.SetTrigger(stateAniPara);
            }

            StartCutScene();
            
        }

        // 상태 업데이트를 처리
        public abstract void StateUpdate();

        // 상태 종료 시 처리
        public virtual void Exit()
        {
            if (_animator && stateAniPara != string.Empty)
                _animator.ResetTrigger(stateAniPara);

            EndCutScene();
        }

        //컷씬 시작
        public void StartCutScene()
        {
            // 컷씬 재생 여부에 따른 처리
            if (stateCutScene)
            {
                // 가상 카메라를 해당 캐릭터를 비추게 설정
                mainCamera = GameObject.Find(virtualCameraName).GetComponent<CinemachineVirtualCamera>();
                if (mainCamera)
                {
                    existingTarget = mainCamera.Follow;
                    originalFOV = mainCamera.m_Lens.FieldOfView; // 원래 FOV 저장
                    mainCamera.Follow = _character.transform;
                    StartCoroutine(ZoomIn()); // 줌 인 시작
                }

                isCutScene = true;
                GameFlowEventBus.Publish(GameFlowType.Pause);
            }
        }
        //컷씬 종료
        public void EndCutScene()
        {
            // 컷씬 재생 여부에 따른 처리
            if (stateCutScene)
            {
                // 가상 카메라 타켓 다시 기존 대상으로 재설정
                if (mainCamera)
                {
                    mainCamera.Follow = existingTarget;
                    StartCoroutine(ZoomOut()); // 줌 아웃 시작
                }
                // 상태 종료 처리
                isCutScene = false;
                GameFlowEventBus.Publish(GameFlowType.Proceeding);
            }
        }

        // 줌 인 코루틴
        private IEnumerator ZoomIn()
        {
            while (mainCamera.m_Lens.FieldOfView > zoomInFOV)
            {
                mainCamera.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime;
                yield return null;
            }
            mainCamera.m_Lens.FieldOfView = zoomInFOV; // 최종 FOV 설정
        }

        // 줌 아웃 코루틴
        private IEnumerator ZoomOut()
        {
            while (mainCamera.m_Lens.FieldOfView < originalFOV)
            {
                mainCamera.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime;
                yield return null;
            }
            mainCamera.m_Lens.FieldOfView = originalFOV; // 원래 FOV로 복원
        }

        private void OnDestroy()
        {
            if(mainCamera)
                mainCamera.m_Lens.FieldOfView = originalFOV;
        }
    }

}
