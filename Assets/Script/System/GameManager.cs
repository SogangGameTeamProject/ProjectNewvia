using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Newvia
{
    public class GameManager : Singleton<GameManager>
    {
        private MonsterSpawnManager _monsterSpawnManager = null;
        public int killCount { get; set; }//킬 카운트
        public int nowMonsterCount {  get; set; }//몬스터 카운트
        public GameFlowType flowType { get; set; }


        private void Start()
        {
            _monsterSpawnManager = MonsterSpawnManager.Instance;
            GameFlowEventBus.Publish(GameFlowType.GameStart);
        }

        private void OnEnable()
        {
            GameFlowEventBus.Subscribe(GameFlowType.GameStart, GameStart);
            GameFlowEventBus.Subscribe(GameFlowType.Proceeding, Proceeding);
            GameFlowEventBus.Subscribe(GameFlowType.GameClear, GameClear);
            GameFlowEventBus.Subscribe(GameFlowType.GameOver, GameOver);
            GameFlowEventBus.Subscribe(GameFlowType.Pause, Pause);
            GameFlowEventBus.Subscribe(GameFlowType.CutScene, CutScene);
        }

        

        private void OnDisable()
        {
            GameFlowEventBus.Unsubscribe(GameFlowType.GameStart, GameStart);
            GameFlowEventBus.Unsubscribe(GameFlowType.Proceeding, Proceeding);
            GameFlowEventBus.Unsubscribe(GameFlowType.GameClear, GameClear);
            GameFlowEventBus.Unsubscribe(GameFlowType.GameOver, GameOver);
            GameFlowEventBus.Unsubscribe(GameFlowType.Pause, Pause);
            GameFlowEventBus.Unsubscribe(GameFlowType.CutScene, CutScene);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("TestScene");
            }

        }

        //이벤트 별 처리 함수
        private void GameStart()
        {
            flowType = GameFlowType.GameStart;
            GameFlowEventBus.Publish(GameFlowType.Proceeding);
        }
        private void Proceeding()
        {
            Debug.Log("게임 진행 중");
            flowType = GameFlowType.Proceeding;
        }

        private void GameClear()
        {
            flowType = GameFlowType.GameClear;
        }

        private void GameOver()
        {
            flowType = GameFlowType.GameOver;
        }

        private void Pause()
        {
            flowType = GameFlowType.Pause;
        }

        private void CutScene()
        {
            flowType = GameFlowType.CutScene;
        }
        
    }

}
