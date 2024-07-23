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
        public LayerMask wallLayerMask; // 벽 레이어 마스크

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
            Transform playerTransform = GameObject.FindWithTag("Player").transform;
            if (playerTransform != null)
            {
                _targetPosition = playerTransform.position;
            }
        }

        protected override void HandleSkill()
        {
            _damageZone.SetActive(true);

            if (_targetPosition != null)
            {
                // 플레이어 위치로 돌진하기 시작
                Vector2 direction = (_targetPosition - (Vector2)_character.transform.position).normalized;
                _rBody.velocity = direction * rushPower; // 돌진 속도 설정
                isRushing = true;
            }
            // 목표 위치에 도달했는지 확인
            if (Vector2.Distance(_character.transform.position, _targetPosition) <= 0.5f)
            {
                // 목표 위치에 도달하면 후딜 처리로 넘어감
                elapsedTime = firstDealDuration + skillDuration;
            }
        }

        protected override void HandleLastDeal()
        {
            _damageZone.SetActive(false);
        }

        protected override void OnSkillEnd()
        {
            _damageZone.SetActive(false);
            // 돌진 종료 시 속도 0으로 설정
            _rBody.velocity = Vector2.zero;
            isRushing = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isRushing && ((1 << collision.gameObject.layer) & wallLayerMask) != 0)
            {
                elapsedTime = firstDealDuration + skillDuration;
            }
        }
    }
}
