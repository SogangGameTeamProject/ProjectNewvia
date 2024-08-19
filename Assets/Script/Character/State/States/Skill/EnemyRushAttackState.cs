using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class EnemyRushAttackState : SkillStateBase
    {
        public GameObject _damageZone = null; // 활성화할 데미지 존 오브젝트
        private Rigidbody2D _rBody = null;
        protected Vector2 _targetPosition;
        private bool isRushing = false;
        [SerializeField]
        private float rushPower = 30f;
        [SerializeField]
        private float dashDistance = 10f;//대쉬거리
        public LayerMask wallLayerMask; // 벽 레이어 마스크
        [SerializeField]
        private float wallCheckDistance = 3.5f;//벽 충돌 체크 거리
        [SerializeField]
        private float wallCastY = 1.25f;//벽 체크 케스트 시작 y위치 값
        protected Vector2 direction = Vector2.zero;//타겟 방향
        //스킬 사거리 표시
        public GameObject skillRangeDisplay = null;

        public override void Exit()
        {
            base.Exit();
        }

        public override void Enter(CharacterInit character)
        {
            base.Enter(character);
            if (_rBody == null)
                _rBody = _character.GetComponent<Rigidbody2D>();

            isRushing = false;
        }

        protected override void HandleFirstDeal()
        {
            // 플레이어의 Transform을 찾아서 위치 저장
            GameObject player = GameObject.FindWithTag("Player");

            Transform playerTransform = null;
            if (player != null)
                playerTransform = player.transform;

            if (playerTransform != null)
            {
                _targetPosition = playerTransform.position;
            }

            //돌진 방향 갱신
            if (_targetPosition != null)
            {
                direction = (_targetPosition - (Vector2)_character.transform.position).normalized;
                
            }

            //스킬 사거리 표시 구현
            if (skillRangeDisplay)
            {
                skillRangeDisplay.SetActive(true);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if(_character.CharacterDirection == CharacterDirection.left)
                {
                    angle = 180 + angle;
                }

                // 오브젝트의 회전 설정 (기본적으로 오른쪽(0도) 방향이므로 추가적인 보정 필요 없음)
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        protected override void HandleSkill()
        {
            //스킬 사거리 표시 제거
            if (skillRangeDisplay)
                skillRangeDisplay.SetActive(false);

            
            
            if (_targetPosition != null)
            {
                //돌진 방향에 따른 캐릭터 방향 조정
                if (direction.x > 0)
                    _character.CharacterDirection = CharacterDirection.right;
                else if (direction.x < 0)
                    _character.CharacterDirection = CharacterDirection.left;
                // 플레이어 위치로 돌진하기 시작
                if (!isRushing)
                {
                    _rBody.AddForce(direction * rushPower, ForceMode2D.Impulse);
                    isRushing = true;
                }

                RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, wallCheckDistance, Vector2.up, wallCastY, wallLayerMask);
                // 돌진 종료 체크
                if (Vector2.Distance(_character.transform.position, _targetPosition + direction * dashDistance) <= 1f
                    || hitInfo.collider != null
                    )
                {
                    // 목표 위치에 도달하면 후딜 처리로 넘어감
                    elapsedTime = firstDealDuration + skillDuration;
                }
            }
            
        }

        protected override void HandleLastDeal()
        {
            _damageZone.SetActive(true);
            isRushing = false;
            _rBody.velocity = Vector2.zero;
        }

        protected override void OnSkillEnd()
        {
            //스킬 사거리 표시 제거
            if (skillRangeDisplay)
                skillRangeDisplay.SetActive(false);
            _damageZone.SetActive(false);
            // 돌진 종료 시 속도 0으로 설정
            _rBody.velocity = Vector2.zero;
            isRushing = false;
        }
    }
}
