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

        //가상 카메라 관련 전역 변수
        public string virtualCameraName = "VirtualCamera";
        private CinemachineVirtualCamera mainCamera = null;
        CinemachineBasicMultiChannelPerlin mainCameraNoise = null;

        // 컷씬 관련 전역 변수
        [SerializeField]
        protected bool stateCutScene = false; // 컷씬 사용 여부
        protected bool isCutScene = false;    // 컷씬 재생 여부
        private Transform existingTarget = null;

        // 줌 관련 변수
        [SerializeField]
        protected float zoomInFOV = 30f;       // 줌 인 시의 Field of View
        [SerializeField]
        protected float zoomSpeed = 2f;        // 줌 속도
        private float originalFOV = 0f;        // 원래의 Field of View

        //화면 흔들림
        public bool isShake = false;//화면 흔들림 여부
        public float shakeDuration = 0f; // 흔들림 시간
        public float shakeAmplitude = 1.2f; // 흔들림 정도
        public float shakeFrequency = 2.0f; // 흔들림 빈도

        //사운드 재생
        protected SoundManger _soundManger;
        public AudioClip bgmClip = null;
        public AudioClip sfxClip = null;

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

            //가상 카메라 값 초기화
            if (mainCamera == null)
                mainCamera = GameObject.Find(virtualCameraName).GetComponent<CinemachineVirtualCamera>();
            if (mainCamera && mainCameraNoise == null)
                mainCameraNoise = mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            //사운드 재생
            if (_soundManger == null)
                _soundManger = SoundManger.Instance;
            if (_soundManger && bgmClip)
                _soundManger.PlayBGM(bgmClip);
            if (_soundManger && sfxClip)
                _soundManger.PlaySFX(sfxClip);

            if (isShake)
                StartCoroutine(CameraShake());

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
                if (mainCamera)
                {
                    existingTarget = mainCamera.Follow;
                    originalFOV = mainCamera.m_Lens.FieldOfView; // 원래 FOV 저장
                    mainCamera.Follow = _character.transform;
                    StartCoroutine(ZoomIn(zoomInFOV)); // 줌 인 시작
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
        public IEnumerator ZoomIn(float zoomInFOV)
        {
            while (mainCamera.m_Lens.FieldOfView > zoomInFOV)
            {
                mainCamera.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime;
                yield return null;
            }
            mainCamera.m_Lens.FieldOfView = zoomInFOV; // 최종 FOV 설정
        }

        // 줌 아웃 코루틴
        public IEnumerator ZoomOut()
        {
            while (mainCamera.m_Lens.FieldOfView < originalFOV)
            {
                mainCamera.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime;
                yield return null;
            }
            mainCamera.m_Lens.FieldOfView = originalFOV; // 원래 FOV로 복원
        }

        public IEnumerator CameraShake()
        {
            mainCameraNoise.m_AmplitudeGain = shakeAmplitude;
            mainCameraNoise.m_FrequencyGain = shakeFrequency;

            yield return new WaitForSeconds(shakeDuration);

            mainCameraNoise.m_AmplitudeGain = 0f;
        }

        private void OnDestroy()
        {
            if(mainCamera && originalFOV > 0)
                mainCamera.m_Lens.FieldOfView = originalFOV;
            if (isShake && mainCameraNoise)
                mainCameraNoise.m_AmplitudeGain = 0f;
        }
    }

}
