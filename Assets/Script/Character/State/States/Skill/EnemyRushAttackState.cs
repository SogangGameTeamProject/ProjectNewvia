using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Newvia
{
    public class EnemyRushAttackState : SkillStateBase
    {
        public GameObject _damageZone = null; // 활성화할 데미지 존 오브젝트
        private Rigidbody2D _rBody = null;
        private Vector2 _targetPosition;
        private bool isRushing = false;
        [SerializeField]
        private float rushPower = 30f;
        [SerializeField]
        private float dashDistance = 10f;
        public LayerMask wallLayerMask; // 벽 레이어 마스크
        [SerializeField]
        private float wallCheckDistance = 3.5f;//벽 충돌 체크 거리
        private Vector2 direction = Vector2.zero;//타겟 방향

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
            // 플레이어의 Transform을 찾아서 위치 저장
            Transform playerTransform = GameObject.FindWithTag("Player").transform;
            if (playerTransform != null)
            {
                _targetPosition = playerTransform.position;
            }
        }

        protected override void HandleFirstDeal()
        {
            //돌진 방향 갱신
            if (_targetPosition != null)
            {
                direction = (_targetPosition - (Vector2)_character.transform.position).normalized;
            }
        }

        protected override void HandleSkill()
        {
            _damageZone.SetActive(true);

            if (_targetPosition != null)
            {
                // 플레이어 위치로 돌진하기 시작
                _rBody.velocity = direction * rushPower; // 돌진 속도 설정
                isRushing = true;

                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, wallCheckDistance, wallLayerMask);
                // 돌진 종료 체크
                if (Vector2.Distance(_character.transform.position, _targetPosition + direction * dashDistance) <= 0.5f
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
            _damageZone.SetActive(false);
            isRushing = false;
            _rBody.velocity = Vector2.zero;
        }

        protected override void OnSkillEnd()
        {
            _damageZone.SetActive(false);
            // 돌진 종료 시 속도 0으로 설정
            _rBody.velocity = Vector2.zero;
            isRushing = false;
        }
    }
}
